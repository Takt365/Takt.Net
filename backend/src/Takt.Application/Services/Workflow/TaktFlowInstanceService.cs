// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowInstanceService.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程实例服务实现
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Mapster;
using SqlSugar;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services;
using Takt.Application.Services.Workflow.FlowEngine;
using Takt.Domain.Entities.Routine.Announcement;
using Takt.Domain.Entities.Routine.DocsCenter;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Constants;
using Takt.Shared.Enums;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流流程实例服务
/// </summary>
/// <remarks>
/// 流程处理已知的、使用 InstanceId 参与审批的业务实体：公告(TaktAnnouncement)、文控文档(TaktDocument)。
/// 整个流程：发起时按 ProcessKey+BusinessKey 回写实体.InstanceId；结束时按 InstanceId 查找并更新业务状态。
/// 新增同类实体时需在 SetBusinessFlowInstanceIdAsync 与 UpdateBusinessStatusAsync 中同步添加。
/// </remarks>
public class TaktFlowInstanceService : TaktServiceBase, ITaktFlowInstanceService
{
    private readonly ITaktRepository<TaktFlowInstance> _instanceRepository;
    private readonly ITaktRepository<TaktFlowScheme> _schemeRepository;
    private readonly ITaktRepository<TaktFlowExecution> _executionRepository;
    private readonly ITaktRepository<TaktFlowOperation> _operationRepository;
    private readonly ITaktRepository<TaktAnnouncement> _announcementRepository;
    private readonly ITaktRepository<TaktDocument> _documentRepository;
    private readonly ITaktFlowSchemeService _schemeService;
    private readonly ITaktFlowNotificationService? _notificationService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstanceService(
        ITaktRepository<TaktFlowInstance> instanceRepository,
        ITaktRepository<TaktFlowScheme> schemeRepository,
        ITaktRepository<TaktFlowExecution> executionRepository,
        ITaktRepository<TaktFlowOperation> operationRepository,
        ITaktRepository<TaktAnnouncement> announcementRepository,
        ITaktRepository<TaktDocument> documentRepository,
        ITaktFlowSchemeService schemeService,
        ITaktFlowNotificationService? notificationService = null,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _instanceRepository = instanceRepository;
        _schemeRepository = schemeRepository;
        _executionRepository = executionRepository;
        _operationRepository = operationRepository;
        _announcementRepository = announcementRepository;
        _documentRepository = documentRepository;
        _schemeService = schemeService;
        _notificationService = notificationService;
    }

    /// <summary>
    /// 分页查询流程实例
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowInstanceDto>> GetListAsync(TaktFlowInstanceQueryDto query)
    {
        var predicate = BuildQueryPredicate(query);
        var (data, total) = await _instanceRepository.GetPagedAsync(query.PageIndex, query.PageSize, predicate).ConfigureAwait(false);
        var list = data.Adapt<List<TaktFlowInstanceDto>>();
        var schemeIds = list.Select(d => d.SchemeId).Distinct().ToList();
        var schemeNames = new Dictionary<long, string>();
        foreach (var sid in schemeIds)
        {
            schemeNames[sid] = await GetProcessNameBySchemeIdAsync(sid).ConfigureAwait(false);
        }
        foreach (var d in list)
            d.ProcessName = schemeNames.TryGetValue(d.SchemeId, out var name) ? name : "";
        return TaktPagedResult<TaktFlowInstanceDto>.Create(list, total, query.PageIndex, query.PageSize);
    }

    /// <summary>
    /// 根据ID获取流程实例
    /// </summary>
    /// <param name="id">实例ID</param>
    /// <returns>流程实例 DTO，不存在返回 null</returns>
    public async Task<TaktFlowInstanceDto?> GetByIdAsync(long id)
    {
        var entity = await _instanceRepository.GetByIdAsync(id).ConfigureAwait(false);
        if (entity == null) return null;
        var dto = entity.Adapt<TaktFlowInstanceDto>();
        dto.ProcessName = await GetProcessNameBySchemeIdAsync(entity.SchemeId).ConfigureAwait(false);
        return dto;
    }

    /// <summary>
    /// 获取流程实例选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表（如运行中实例的 InstanceId + 显示名）</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var list = await _instanceRepository.FindAsync(s => s.InstanceStatus == (int)TaktFlowInstanceStatus.Running && s.IsDeleted == 0).ConfigureAwait(false);
        return list
            .OrderByDescending(s => s.CreateTime)
            .Select(s => new TaktSelectOption { DictValue = s.Id, DictLabel = s.ProcessTitle ?? s.InstanceCode ?? s.Id.ToString() })
            .ToList();
    }

    /// <summary>
    /// 创建并启动流程实例（创建后即处于运行中；当前节点由流程方案 BPMN 2.0 XML 解析结果驱动，无定义时使用默认图）
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>流程实例 DTO</returns>
    public async Task<TaktFlowInstanceDto> CreateAsync(TaktFlowInstanceCreateDto dto)
    {
        EnsureAuthenticated();
        var user = GetCurrentUser();
        if (user == null)
            ThrowBusinessException("未登录");

        var schemeDto = await _schemeService.GetByProcessKeyAsync(dto.ProcessKey).ConfigureAwait(false);
        var scheme = EnsureEntityExists(
            schemeDto == null ? null : await _schemeRepository.GetByIdAsync(schemeDto.SchemeId).ConfigureAwait(false),
            "流程方案不存在或未发布，请检查 ProcessKey：" + dto.ProcessKey);

        var runtime = BuildRuntime(scheme);
        var firstUserNode = runtime.GetFirstUserNode();
        var startNode = runtime.GetStartNode();
        var currentNode = firstUserNode ?? startNode;
        var previousNode = firstUserNode != null ? startNode : null;

        var currentId = ParseNodeIdToLong(currentNode?.Id);
        var currentName = currentNode?.Name ?? "审批";
        var previousId = previousNode != null ? ParseNodeIdToLong(previousNode.Id) : (long?)0;

        var now = DateTime.Now;
        var instance = new TaktFlowInstance
        {
            InstanceCode = "FL" + now.ToString("yyyyMMddHHmmss") + new Random().Next(100, 999),
            ProcessKey = scheme.ProcessKey,
            SchemeId = scheme.Id,
            BusinessKey = dto.BusinessKey,
            CreateId = user!.Id,
            CurrentNodeId = currentId,
            CurrentNodeName = currentName,
            InstanceStatus = (int)TaktFlowInstanceStatus.Running,
            ProcessTitle = dto.ProcessTitle ?? scheme.ProcessName
        };
        instance.PreviousNodeId = previousId ?? 0;
        instance = await _instanceRepository.CreateAsync(instance).ConfigureAwait(false);
        instance.InstanceCode = "FL" + instance.Id;
        await _instanceRepository.UpdateAsync(instance).ConfigureAwait(false);

        await SetBusinessFlowInstanceIdAsync(dto.ProcessKey, dto.BusinessKey, instance.Id).ConfigureAwait(false);

        await WriteOperationAsync(instance, (int)TaktFlowOperationType.Start, currentId.ToString(), currentName, "启动流程", null).ConfigureAwait(false);

        var processNameForExecution = await GetProcessNameBySchemeIdAsync(instance.SchemeId).ConfigureAwait(false);
        var fromId = startNode != null ? startNode.Id : "0";
        var fromName = startNode?.Name ?? "开始";
        await WriteExecutionAsync(
            instance,
            processNameForExecution,
            runtime,
            fromId,
            fromName,
            currentId.ToString(),
            currentName,
            0,
            now,
            user!.Id,
            !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName ?? "",
            null,
            false).ConfigureAwait(false);

        _ = _notificationService?.NotifyInstanceStartedAsync(instance.Id, instance.SchemeId);

        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 更新流程实例（仅运行中可更新流程标题、优先级等有限字段）
    /// </summary>
    /// <param name="dto">更新 DTO</param>
    /// <returns>流程实例 DTO</returns>
    public async Task<TaktFlowInstanceDto> UpdateAsync(TaktFlowInstanceUpdateDto dto)
    {
        EnsureAuthenticated();
        var instance = await _instanceRepository.GetByIdAsync(dto.InstanceId).ConfigureAwait(false);
        instance = EnsureEntityExists(instance, "流程实例不存在");
        if (instance.InstanceStatus != (int)TaktFlowInstanceStatus.Running)
            ThrowBusinessException("仅运行中的流程可更新");

        if (dto.ProcessTitle != null)
            instance.ProcessTitle = dto.ProcessTitle;
        if (dto.Priority.HasValue)
            instance.Priority = dto.Priority.Value;
        await _instanceRepository.UpdateAsync(instance).ConfigureAwait(false);
        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 启动流程（当前实现为创建即启动，此方法仅做校验并返回实例）
    /// </summary>
    /// <param name="dto">启动 DTO</param>
    /// <returns>流程实例 DTO</returns>
    public async Task<TaktFlowInstanceDto> StartAsync(TaktFlowInstanceStartDto dto)
    {
        EnsureAuthenticated();
        var instance = await _instanceRepository.GetByIdAsync(dto.InstanceId).ConfigureAwait(false);
        instance = EnsureEntityExists(instance, "流程实例不存在");
        if (instance.InstanceStatus != (int)TaktFlowInstanceStatus.Running)
            ThrowBusinessException("流程已结束或已撤回，无法启动");
        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 审批流程（按流程方案 BPMN XML 解析后的连线条件计算下一节点；通过/驳回对应 Pass/Reject，至终态则完成/终止并回写业务表）
    /// </summary>
    /// <param name="dto">审批 DTO</param>
    /// <returns>流程实例 DTO</returns>
    public async Task<TaktFlowInstanceDto> ApproveAsync(TaktFlowInstanceApproveDto dto)
    {
        EnsureAuthenticated();
        var user = GetCurrentUser();
        if (user == null)
            ThrowBusinessException("未登录");

        var instance = await _instanceRepository.GetByIdAsync(dto.InstanceId).ConfigureAwait(false);
        instance = EnsureEntityExists(instance, "流程实例不存在");
        if (instance.InstanceStatus != (int)TaktFlowInstanceStatus.Running)
            ThrowBusinessException("流程已结束或已撤回，无法审批");

        var scheme = await _schemeRepository.GetByIdAsync(instance.SchemeId).ConfigureAwait(false);
        var runtime = scheme != null ? BuildRuntime(scheme) : new TaktFlowRuntime();
        if (runtime.Nodes.Count == 0)
            runtime.BuildDefaultGraph();

        var fromNodeId = (instance.CurrentNodeId ?? 1).ToString();
        var fromNodeName = instance.CurrentNodeName ?? "审批";
        var condition = dto.Pass ? TaktFlowLineCondition.Pass : TaktFlowLineCondition.Reject;
        var nextNode = runtime.GetNextNode(fromNodeId, condition);

        long toNodeId;
        string toNodeName;
        if (nextNode != null)
        {
            toNodeId = ParseNodeIdToLong(nextNode.Id);
            toNodeName = nextNode.Name ?? (dto.Pass ? "结束" : "驳回");
        }
        else
        {
            toNodeId = dto.Pass ? 2L : 3L;
            toNodeName = dto.Pass ? "结束" : "驳回";
        }

        var isEnd = nextNode == null || runtime.IsEndNode(nextNode.Id);
        if (isEnd)
            instance.InstanceStatus = dto.Pass ? (int)TaktFlowInstanceStatus.Completed : (int)TaktFlowInstanceStatus.Terminated;

        instance.PreviousNodeId = instance.CurrentNodeId;
        instance.CurrentNodeId = toNodeId;
        instance.CurrentNodeName = toNodeName;
        instance.AssigneeId = null;
        await _instanceRepository.UpdateAsync(instance).ConfigureAwait(false);

        var now = DateTime.Now;
        var processName = await GetProcessNameBySchemeIdAsync(instance.SchemeId).ConfigureAwait(false);
        await WriteExecutionAsync(
            instance,
            processName,
            runtime,
            fromNodeId,
            fromNodeName,
            toNodeId.ToString(),
            toNodeName,
            dto.Pass ? 0 : 1,
            now,
            user!.Id,
            !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName ?? "",
            dto.Comment,
            isEnd).ConfigureAwait(false);

        var opType = dto.Pass ? (int)TaktFlowOperationType.Complete : (int)TaktFlowOperationType.Terminate;
        var opContent = dto.Pass ? "审批通过，流程完成" : "审批驳回，流程终止";
        await WriteOperationAsync(instance, opType, fromNodeId, fromNodeName, opContent, dto.Comment).ConfigureAwait(false);

        if (isEnd)
        {
            await UpdateBusinessStatusAsync(instance, dto.Pass, now).ConfigureAwait(false);
            _ = _notificationService?.NotifyInstanceEndedAsync(instance.Id, instance.SchemeId, dto.Pass);
        }
        else
            _ = _notificationService?.NotifyNodeReachedAsync(instance.Id, instance.SchemeId, toNodeId.ToString(), toNodeName);

        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 撤回流程
    /// </summary>
    /// <param name="dto">撤回 DTO</param>
    /// <returns>流程实例 DTO</returns>
    public async Task<TaktFlowInstanceDto> RecallAsync(TaktFlowInstanceRecallDto dto)
    {
        EnsureAuthenticated();
        var user = GetCurrentUser();
        if (user == null)
            ThrowBusinessException("未登录");

        var instance = await _instanceRepository.GetByIdAsync(dto.InstanceId).ConfigureAwait(false);
        instance = EnsureEntityExists(instance, "流程实例不存在");
        if (instance.InstanceStatus != (int)TaktFlowInstanceStatus.Running)
            ThrowBusinessException("流程已结束，无法撤回");

        var scheme = await _schemeRepository.GetByIdAsync(instance.SchemeId).ConfigureAwait(false);
        if (scheme?.IsRevocable != 1)
            ThrowBusinessException("该流程不支持撤回");

        if (instance.CreateId != user!.Id)
            ThrowBusinessException("仅流程发起人可撤回");

        var recallFromNodeId = (instance.CurrentNodeId ?? 0).ToString();
        var recallFromNodeName = instance.CurrentNodeName ?? "审批";

        var runtime = BuildRuntime(scheme!);
        var startNode = runtime.GetStartNode();
        var startId = startNode != null ? ParseNodeIdToLong(startNode.Id) : 0L;
        var startName = startNode?.Name ?? "开始";

        instance.InstanceStatus = (int)TaktFlowInstanceStatus.Recalled;
        instance.PreviousNodeId = instance.CurrentNodeId;
        instance.CurrentNodeId = startId;
        instance.CurrentNodeName = startName;
        await _instanceRepository.UpdateAsync(instance).ConfigureAwait(false);

        await WriteOperationAsync(instance, (int)TaktFlowOperationType.Recall, recallFromNodeId, recallFromNodeName, "撤回流程", dto.Comment).ConfigureAwait(false);

        var processNameRecall = await GetProcessNameBySchemeIdAsync(instance.SchemeId).ConfigureAwait(false);
        await WriteExecutionAsync(
            instance,
            processNameRecall,
            runtime,
            recallFromNodeId,
            recallFromNodeName,
            startId.ToString(),
            startName,
            5,
            DateTime.Now,
            user!.Id,
            !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName ?? "",
            dto.Comment,
            false).ConfigureAwait(false);

        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 节点指定/转办
    /// </summary>
    /// <param name="dto">节点指定/转办 DTO</param>
    /// <returns>流程实例 DTO</returns>
    public async Task<TaktFlowInstanceDto> NodeDesignateAsync(TaktFlowInstanceNodeDesignateDto dto)
    {
        EnsureAuthenticated();
        var user = GetCurrentUser();
        if (user == null)
            ThrowBusinessException("未登录");

        var instance = await _instanceRepository.GetByIdAsync(dto.InstanceId).ConfigureAwait(false);
        instance = EnsureEntityExists(instance, "流程实例不存在");
        if (instance.InstanceStatus != (int)TaktFlowInstanceStatus.Running)
            ThrowBusinessException("仅运行中的流程可做节点指定/转办");

        var fromNodeId = (instance.CurrentNodeId ?? 0).ToString();
        var fromNodeName = instance.CurrentNodeName ?? "审批";
        var toNodeId = dto.ToNodeId ?? fromNodeId;
        var toNodeName = dto.ToNodeName ?? fromNodeName;

        instance.PreviousNodeId = instance.CurrentNodeId;
        instance.CurrentNodeId = long.TryParse(toNodeId, out var nid) ? nid : instance.CurrentNodeId;
        instance.CurrentNodeName = toNodeName;
        await _instanceRepository.UpdateAsync(instance).ConfigureAwait(false);

        var transitionType = dto.DesignateUserId.HasValue ? 2 : 0; // 2=转办
        var transitionUserId = dto.DesignateUserId ?? user!.Id;
        var transitionUserName = dto.DesignateUserId == user!.Id || !dto.DesignateUserId.HasValue
            ? (user.RealName ?? user.UserName ?? "")
            : ""; // 转办给他人时需外部传入姓名，此处简化

        var schemeForDesignate = await _schemeRepository.GetByIdAsync(instance.SchemeId).ConfigureAwait(false);
        var runtimeDesignate = schemeForDesignate != null ? BuildRuntime(schemeForDesignate) : null;
        var processNameDesignate = await GetProcessNameBySchemeIdAsync(instance.SchemeId).ConfigureAwait(false);
        await WriteExecutionAsync(
            instance,
            processNameDesignate,
            runtimeDesignate,
            fromNodeId,
            fromNodeName,
            toNodeId,
            toNodeName,
            transitionType,
            DateTime.Now,
            transitionUserId,
            string.IsNullOrEmpty(transitionUserName) ? "转办" : transitionUserName,
            dto.Comment,
            false).ConfigureAwait(false);

        await WriteOperationAsync(instance, (int)TaktFlowOperationType.Designate, fromNodeId, fromNodeName, "节点指定/转办", dto.Comment).ConfigureAwait(false);

        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 退回（运行中流程退回到上一节点或指定节点，受方案 IsReturnable 控制）
    /// </summary>
    /// <param name="dto">退回 DTO</param>
    /// <returns>流程实例 DTO</returns>
    public async Task<TaktFlowInstanceDto> ReturnAsync(TaktFlowInstanceReturnDto dto)
    {
        EnsureAuthenticated();
        var user = GetCurrentUser();
        if (user == null)
            ThrowBusinessException("未登录");

        var instance = await _instanceRepository.GetByIdAsync(dto.InstanceId).ConfigureAwait(false);
        instance = EnsureEntityExists(instance, "流程实例不存在");
        if (instance.InstanceStatus != (int)TaktFlowInstanceStatus.Running)
            ThrowBusinessException("仅运行中的流程可退回");

        var scheme = await _schemeRepository.GetByIdAsync(instance.SchemeId).ConfigureAwait(false);
        if (scheme?.IsReturnable != 1)
            ThrowBusinessException("该流程方案不支持退回");

        var runtime = BuildRuntime(scheme!);
        var fromNodeId = (instance.CurrentNodeId ?? 0).ToString();
        var fromNodeName = instance.CurrentNodeName ?? "审批";

        string toNodeId;
        string toNodeName;
        if (!string.IsNullOrWhiteSpace(dto.ToNodeId))
        {
            var incoming = runtime.GetIncomingNodeIds(fromNodeId);
            if (!incoming.Contains(dto.ToNodeId.Trim()))
                ThrowBusinessException("指定节点不是当前节点的上一节点，无法退回");
            toNodeId = dto.ToNodeId.Trim();
            var toNode = runtime.GetNode(toNodeId);
            toNodeName = dto.ToNodeName ?? toNode?.Name ?? toNodeId;
        }
        else
        {
            System.Linq.Expressions.Expression<Func<TaktFlowExecution, bool>> pred = h => h.InstanceId == dto.InstanceId;
            var all = await _executionRepository.FindAsync(pred).ConfigureAwait(false);
            var ordered = all.OrderByDescending(h => h.TransitionTime).ToList();
            var last = ordered.FirstOrDefault();
            if (last == null)
                ThrowBusinessException("无流转历史，无法退回到上一节点");
            toNodeId = last!.FromNodeId ?? "";
            toNodeName = last.FromNodeName ?? "";
            if (string.IsNullOrEmpty(toNodeId))
                ThrowBusinessException("无法确定上一节点");
        }

        var toNodeIdLong = ParseNodeIdToLong(toNodeId);
        instance.PreviousNodeId = instance.CurrentNodeId;
        instance.CurrentNodeId = toNodeIdLong;
        instance.CurrentNodeName = toNodeName;
        await _instanceRepository.UpdateAsync(instance).ConfigureAwait(false);

        var processNameReturn = await GetProcessNameBySchemeIdAsync(instance.SchemeId).ConfigureAwait(false);
        await WriteExecutionAsync(
            instance,
            processNameReturn,
            runtime,
            fromNodeId,
            fromNodeName,
            toNodeId,
            toNodeName,
            1,
            DateTime.Now,
            user!.Id,
            !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName ?? "",
            dto.Comment,
            false).ConfigureAwait(false);

        await WriteOperationAsync(instance, (int)TaktFlowOperationType.Return, fromNodeId, fromNodeName, "退回", dto.Comment).ConfigureAwait(false);

        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 待办列表（运行中且未认领或已认领给当前用户的实例）
    /// </summary>
    public async Task<TaktPagedResult<TaktFlowInstanceDto>> GetTodoListAsync(TaktFlowInstanceQueryDto query)
    {
        EnsureAuthenticated();
        var q = new TaktFlowInstanceQueryDto
        {
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            ProcessKey = query.ProcessKey,
            BusinessKey = query.BusinessKey,
            CreateId = query.CreateId,
            InstanceStatus = (int)TaktFlowInstanceStatus.Running,
            TodoOnly = true
        };
        return await GetListAsync(q).ConfigureAwait(false);
    }

    /// <summary>
    /// 认领（将运行中且未认领的实例设为当前用户处理）
    /// </summary>
    public async Task<TaktFlowInstanceDto> ClaimAsync(TaktFlowInstanceClaimDto dto)
    {
        EnsureAuthenticated();
        var user = GetCurrentUser();
        if (user == null)
            ThrowBusinessException("未登录");

        var instance = await _instanceRepository.GetByIdAsync(dto.InstanceId).ConfigureAwait(false);
        instance = EnsureEntityExists(instance, "流程实例不存在");
        if (instance.InstanceStatus != (int)TaktFlowInstanceStatus.Running)
            ThrowBusinessException("仅运行中的流程可认领");
        if (instance.AssigneeId.HasValue && instance.AssigneeId != user!.Id)
            ThrowBusinessException("该流程已被他人认领");

        instance.AssigneeId = user!.Id;
        await _instanceRepository.UpdateAsync(instance).ConfigureAwait(false);

        var nodeId = (instance.CurrentNodeId ?? 0).ToString();
        var nodeName = instance.CurrentNodeName ?? "审批";
        await WriteOperationAsync(instance, (int)TaktFlowOperationType.Designate, nodeId, nodeName, "认领", dto.Comment).ConfigureAwait(false);

        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 超时自动推进（后台任务调用；以系统身份推进通过或驳回）
    /// </summary>
    public async Task<TaktFlowInstanceDto> AdvanceByTimeoutAsync(long instanceId, bool pass)
    {
        var instance = await _instanceRepository.GetByIdAsync(instanceId).ConfigureAwait(false);
        if (instance == null || instance.InstanceStatus != (int)TaktFlowInstanceStatus.Running)
            return (await GetByIdAsync(instanceId).ConfigureAwait(false))!;

        var scheme = await _schemeRepository.GetByIdAsync(instance.SchemeId).ConfigureAwait(false);
        var runtime = scheme != null ? BuildRuntime(scheme) : new TaktFlowRuntime();
        if (runtime.Nodes.Count == 0)
            runtime.BuildDefaultGraph();

        var fromNodeId = (instance.CurrentNodeId ?? 1).ToString();
        var fromNodeName = instance.CurrentNodeName ?? "审批";
        var condition = pass ? TaktFlowLineCondition.Pass : TaktFlowLineCondition.Reject;
        var nextNode = runtime.GetNextNode(fromNodeId, condition);

        long toNodeId;
        string toNodeName;
        if (nextNode != null)
        {
            toNodeId = ParseNodeIdToLong(nextNode.Id);
            toNodeName = nextNode.Name ?? (pass ? "结束" : "驳回");
        }
        else
        {
            toNodeId = pass ? 2L : 3L;
            toNodeName = pass ? "结束" : "驳回";
        }

        var isEnd = nextNode == null || runtime.IsEndNode(nextNode.Id);
        if (isEnd)
            instance.InstanceStatus = pass ? (int)TaktFlowInstanceStatus.Completed : (int)TaktFlowInstanceStatus.Terminated;

        instance.PreviousNodeId = instance.CurrentNodeId;
        instance.CurrentNodeId = toNodeId;
        instance.CurrentNodeName = toNodeName;
        instance.AssigneeId = null;
        await _instanceRepository.UpdateAsync(instance).ConfigureAwait(false);

        var now = DateTime.Now;
        var processNameTimeout = await GetProcessNameBySchemeIdAsync(instance.SchemeId).ConfigureAwait(false);
        const long systemUserId = 0;
        const string systemUserName = "系统（超时）";
        await WriteExecutionAsync(
            instance,
            processNameTimeout,
            runtime,
            fromNodeId,
            fromNodeName,
            toNodeId.ToString(),
            toNodeName,
            pass ? 0 : 1,
            now,
            systemUserId,
            systemUserName,
            "超时自动" + (pass ? "通过" : "驳回"),
            isEnd).ConfigureAwait(false);

        var opType = pass ? (int)TaktFlowOperationType.Complete : (int)TaktFlowOperationType.Terminate;
        var opContent = "超时自动" + (pass ? "通过，流程完成" : "驳回，流程终止");
        await WriteOperationAsync(instance, opType, fromNodeId, fromNodeName, opContent, null, systemUserId, systemUserName).ConfigureAwait(false);

        if (isEnd)
        {
            await UpdateBusinessStatusAsync(instance, pass, now).ConfigureAwait(false);
            _ = _notificationService?.NotifyInstanceEndedAsync(instance.Id, instance.SchemeId, pass);
        }
        _ = _notificationService?.NotifyTimeoutAsync(instance.Id, instance.SchemeId, pass);

        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 加签（需任务与候选人模型，暂未实现）
    /// </summary>
    public async Task<TaktFlowInstanceDto> AddSignAsync(TaktFlowInstanceAddSignDto dto)
    {
        EnsureAuthenticated();
        ThrowBusinessException("加签功能需任务与候选人模型，暂未实现");
        await Task.CompletedTask.ConfigureAwait(false);
        return null!;
    }

    /// <summary>
    /// 减签（需任务与候选人模型，暂未实现）
    /// </summary>
    public async Task<TaktFlowInstanceDto> ReduceSignAsync(TaktFlowInstanceReduceSignDto dto)
    {
        EnsureAuthenticated();
        ThrowBusinessException("减签功能需任务与候选人模型，暂未实现");
        await Task.CompletedTask.ConfigureAwait(false);
        return null!;
    }

    /// <summary>
    /// 分页查询流程实例流转历史
    /// </summary>
    /// <param name="query">查询条件（含实例ID）</param>
    /// <returns>流转历史分页结果</returns>
    public async Task<TaktPagedResult<TaktFlowInstanceHistoryDto>> GetHistoryListAsync(TaktFlowInstanceHistoryQueryDto query)
    {
        System.Linq.Expressions.Expression<Func<TaktFlowExecution, bool>> predicate = h => h.InstanceId == query.InstanceId;
        var all = await _executionRepository.FindAsync(predicate).ConfigureAwait(false);
        var ordered = all.OrderByDescending(h => h.TransitionTime).ToList();
        var total = ordered.Count;
        var page = ordered
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();
        return TaktPagedResult<TaktFlowInstanceHistoryDto>.Create(
            page.Adapt<List<TaktFlowInstanceHistoryDto>>(),
            total,
            query.PageIndex,
            query.PageSize);
    }

    /// <summary>
    /// 撤销审批（将流程从已完成/已终止恢复到上一节点运行中，仅简单流程支持）
    /// </summary>
    /// <param name="dto">撤销审批 DTO</param>
    /// <returns>流程实例 DTO</returns>
    public async Task<TaktFlowInstanceDto> UndoVerificationAsync(TaktFlowInstanceUndoVerificationDto dto)
    {
        EnsureAuthenticated();
        var user = GetCurrentUser();
        if (user == null)
            ThrowBusinessException("未登录");

        var instance = await _instanceRepository.GetByIdAsync(dto.InstanceId).ConfigureAwait(false);
        instance = EnsureEntityExists(instance, "流程实例不存在");
        if (instance.InstanceStatus != (int)TaktFlowInstanceStatus.Completed && instance.InstanceStatus != (int)TaktFlowInstanceStatus.Terminated)
            ThrowBusinessException("仅已完成或已终止的流程可撤销审批");

        System.Linq.Expressions.Expression<Func<TaktFlowExecution, bool>> pred = h => h.InstanceId == dto.InstanceId;
        var all = await _executionRepository.FindAsync(pred).ConfigureAwait(false);
        var ordered = all.OrderByDescending(h => h.TransitionTime).ToList();
        var last = dto.HistoryId.HasValue
            ? ordered.FirstOrDefault(h => h.Id == dto.HistoryId.Value)
            : ordered.FirstOrDefault();
        if (last == null)
            ThrowBusinessException("未找到可撤销的审批记录");
        var lastRecord = last!;

        instance.InstanceStatus = (int)TaktFlowInstanceStatus.Running;
        instance.CurrentNodeId = long.TryParse(lastRecord.FromNodeId ?? "", out var fromId) ? fromId : null;
        instance.CurrentNodeName = lastRecord.FromNodeName ?? "";
        var prevHistory = ordered.Skip(1).FirstOrDefault(h => h.ToNodeId == lastRecord.FromNodeId);
        instance.PreviousNodeId = prevHistory != null && long.TryParse(prevHistory.FromNodeId, out var prevId) ? prevId : null;
        await _instanceRepository.UpdateAsync(instance).ConfigureAwait(false);

        await WriteOperationAsync(instance, (int)TaktFlowOperationType.UndoVerification, lastRecord.ToNodeId ?? "", lastRecord.ToNodeName ?? "", "撤销审批", dto.Comment).ConfigureAwait(false);

        var schemeUndo = await _schemeRepository.GetByIdAsync(instance.SchemeId).ConfigureAwait(false);
        var runtimeUndo = schemeUndo != null ? BuildRuntime(schemeUndo) : null;
        var processNameUndo = await GetProcessNameBySchemeIdAsync(instance.SchemeId).ConfigureAwait(false);
        await WriteExecutionAsync(
            instance,
            processNameUndo,
            runtimeUndo,
            lastRecord.ToNodeId ?? "",
            lastRecord.ToNodeName ?? "",
            lastRecord.FromNodeId ?? "",
            lastRecord.FromNodeName ?? "",
            0,
            DateTime.Now,
            user!.Id,
            !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName ?? "",
            dto.Comment,
            false).ConfigureAwait(false);

        return (await GetByIdAsync(instance.Id).ConfigureAwait(false))!;
    }

    /// <summary>
    /// 软删除流程实例
    /// </summary>
    /// <param name="id">实例ID</param>
    public async Task DeleteAsync(long id)
    {
        EnsureAuthenticated();
        var entity = await _instanceRepository.GetByIdAsync(id).ConfigureAwait(false);
        entity = EnsureEntityExists(entity, "流程实例不存在");
        await _instanceRepository.DeleteAsync(id).ConfigureAwait(false);
    }

    /// <summary>
    /// 批量软删除流程实例
    /// </summary>
    /// <param name="ids">实例ID列表</param>
    public async Task DeleteAsync(IEnumerable<long> ids)
    {
        EnsureAuthenticated();
        var list = ids?.Where(i => i > 0).Distinct().ToList();
        if (list == null || list.Count == 0)
            return;
        await _instanceRepository.DeleteAsync(list).ConfigureAwait(false);
    }

    /// <summary>
    /// 更新流程实例状态（挂起/恢复：仅允许运行中与已挂起互转，且受方案 IsSuspendable 控制）
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>流程实例 DTO</returns>
    public async Task<TaktFlowInstanceDto> UpdateStatusAsync(TaktFlowInstanceStatusDto dto)
    {
        EnsureAuthenticated();
        var entity = await _instanceRepository.GetByIdAsync(dto.InstanceId).ConfigureAwait(false);
        entity = EnsureEntityExists(entity, "流程实例不存在");

        var targetStatus = dto.InstanceStatus;
        var currentStatus = entity.InstanceStatus;

        if (targetStatus == (int)TaktFlowInstanceStatus.Suspended || targetStatus == (int)TaktFlowInstanceStatus.Running)
        {
            var scheme = await _schemeRepository.GetByIdAsync(entity.SchemeId).ConfigureAwait(false);
            if (scheme?.IsSuspendable != 1)
                ThrowBusinessException("该流程方案不支持挂起/恢复");

            if (targetStatus == (int)TaktFlowInstanceStatus.Suspended)
            {
                if (currentStatus != (int)TaktFlowInstanceStatus.Running)
                    ThrowBusinessException("仅运行中的流程可挂起");
                entity.InstanceStatus = (int)TaktFlowInstanceStatus.Suspended;
                await _instanceRepository.UpdateAsync(entity).ConfigureAwait(false);
                var nodeId = (entity.CurrentNodeId ?? 0).ToString();
                var nodeName = entity.CurrentNodeName ?? "审批";
                await WriteOperationAsync(entity, (int)TaktFlowOperationType.Suspend, nodeId, nodeName, "挂起流程", dto.Comment).ConfigureAwait(false);
            }
            else
            {
                if (currentStatus != (int)TaktFlowInstanceStatus.Suspended)
                    ThrowBusinessException("仅已挂起的流程可恢复");
                entity.InstanceStatus = (int)TaktFlowInstanceStatus.Running;
                await _instanceRepository.UpdateAsync(entity).ConfigureAwait(false);
                var nodeId = (entity.CurrentNodeId ?? 0).ToString();
                var nodeName = entity.CurrentNodeName ?? "审批";
                await WriteOperationAsync(entity, (int)TaktFlowOperationType.Resume, nodeId, nodeName, "恢复流程", dto.Comment).ConfigureAwait(false);
            }
            return (await GetByIdAsync(entity.Id).ConfigureAwait(false))!;
        }

        ThrowBusinessException("仅支持挂起(3)与恢复(0)，其他状态请通过审批/撤回等操作变更");
        return null!;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFlowInstanceTemplateDto>(
            sheetName: string.IsNullOrWhiteSpace(sheetName) ? "流程实例导入模板" : sheetName,
            fileName: string.IsNullOrWhiteSpace(fileName) ? "流程实例导入模板" : fileName
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// 导入流程实例
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    public async Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName)
    {
        var errors = new List<string>();
        var importData = await TaktExcelHelper.ImportAsync<TaktFlowInstanceImportDto>(
            fileStream,
            string.IsNullOrWhiteSpace(sheetName) ? "流程实例导入模板" : sheetName
        ).ConfigureAwait(false);
        if (importData == null || importData.Count == 0)
        {
            errors.Add("Excel 文件中没有数据");
            return (0, 0, errors);
        }
        errors.Add("流程实例不支持通过 Excel 批量导入，请使用创建接口发起流程");
        return (0, importData.Count, errors);
    }

    /// <summary>
    /// 导出流程实例
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件信息（文件名和内容）</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName)
    {
        var predicate = BuildQueryPredicate(query);
        var data = predicate != null
            ? await _instanceRepository.FindAsync(predicate).ConfigureAwait(false)
            : await _instanceRepository.GetAllAsync().ConfigureAwait(false);
        var dtos = data?.Adapt<List<TaktFlowInstanceExportDto>>() ?? new List<TaktFlowInstanceExportDto>();
        return await TaktExcelHelper.ExportAsync(
            dtos,
            string.IsNullOrWhiteSpace(sheetName) ? "流程实例数据" : sheetName,
            string.IsNullOrWhiteSpace(fileName) ? "流程实例导出" : fileName
        ).ConfigureAwait(false);
    }

    private System.Linq.Expressions.Expression<Func<TaktFlowInstance, bool>>? BuildQueryPredicate(TaktFlowInstanceQueryDto query)
    {
        var exp = Expressionable.Create<TaktFlowInstance>();
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(query.ProcessKey), s => s.ProcessKey == query.ProcessKey);
        exp = exp.AndIF(!string.IsNullOrWhiteSpace(query.BusinessKey), s => s.BusinessKey == query.BusinessKey);
        exp = exp.AndIF(query.InstanceStatus.HasValue, s => s.InstanceStatus == query.InstanceStatus!.Value);
        exp = exp.AndIF(query.CreateId.HasValue, s => s.CreateId == query.CreateId!.Value);
        if (query.TodoOnly == true)
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                exp = exp.And(s => s.InstanceStatus == (int)TaktFlowInstanceStatus.Running);
                exp = exp.And(s => s.AssigneeId == null || s.AssigneeId == user.Id);
            }
        }
        return exp.ToExpression();
    }

    private async Task<string> GetProcessNameBySchemeIdAsync(long schemeId)
    {
        var scheme = await _schemeRepository.GetByIdAsync(schemeId).ConfigureAwait(false);
        return scheme?.ProcessName ?? "";
    }

    /// <summary>
    /// 根据流程方案构建运行时图（符合 BPM 规范：流程定义仅用 BPMN 2.0 XML；无 BpmnXml 或解析失败时使用默认图）
    /// </summary>
    private static TaktFlowRuntime BuildRuntime(TaktFlowScheme scheme)
    {
        var runtime = new TaktFlowRuntime();
        if (runtime.LoadFromBpmnXml(scheme.BpmnXml, scheme.ProcessKey, scheme.ProcessName ?? ""))
            return runtime;
        runtime.BuildDefaultGraph();
        runtime.ProcessKey = scheme.ProcessKey;
        runtime.ProcessName = scheme.ProcessName ?? "";
        return runtime;
    }

    /// <summary>
    /// 将流程节点 ID（BPMN/默认图中多为字符串如 "0"/"1"）转为实例表使用的 long
    /// </summary>
    private static long ParseNodeIdToLong(string? nodeId)
    {
        if (string.IsNullOrEmpty(nodeId)) return 0;
        return long.TryParse(nodeId, out var id) ? id : 0;
    }

    /// <summary>
    /// 节点类型枚举转字符串（与 TaktFlowExecution.FromNodeType/ToNodeType 存储一致）
    /// </summary>
    private static string NodeTypeToString(TaktFlowNodeType nodeType)
    {
        return nodeType switch
        {
            TaktFlowNodeType.Start => "Start",
            TaktFlowNodeType.Approval => "Approval",
            TaktFlowNodeType.End => "End",
            TaktFlowNodeType.Rejected => "Rejected",
            _ => "Other"
        };
    }

    /// <summary>
    /// 写入一条流转历史（TaktFlowExecution），并补全 IsFinish、节点类型、耗时等字段
    /// </summary>
    private async Task WriteExecutionAsync(
        TaktFlowInstance instance,
        string processName,
        TaktFlowRuntime? runtime,
        string fromNodeId,
        string fromNodeName,
        string toNodeId,
        string toNodeName,
        int transitionType,
        DateTime transitionTime,
        long transitionUserId,
        string transitionUserName,
        string? transitionComment,
        bool isFinish,
        long? transitionDeptId = null,
        string? transitionDeptName = null,
        string? transitionAttachments = null)
    {
        var fromNodeType = runtime?.GetNode(fromNodeId) != null ? NodeTypeToString(runtime.GetNode(fromNodeId)!.NodeType) : null;
        var toNodeType = runtime?.GetNode(toNodeId) != null ? NodeTypeToString(runtime.GetNode(toNodeId)!.NodeType) : null;

        int elapsedMs = 0;
        var lastArrival = (await _executionRepository.FindAsync(e => e.InstanceId == instance.Id && e.ToNodeId == fromNodeId).ConfigureAwait(false))
            .OrderByDescending(e => e.TransitionTime)
            .FirstOrDefault();
        if (lastArrival != null)
            elapsedMs = (int)(transitionTime - lastArrival.TransitionTime).TotalMilliseconds;

        await _executionRepository.CreateAsync(new TaktFlowExecution
        {
            InstanceId = instance.Id,
            InstanceCode = instance.InstanceCode,
            ProcessKey = instance.ProcessKey,
            ProcessName = processName,
            FromNodeId = fromNodeId,
            FromNodeName = fromNodeName,
            FromNodeType = fromNodeType,
            ToNodeId = toNodeId,
            ToNodeName = toNodeName,
            ToNodeType = toNodeType,
            IsFinish = isFinish ? 1 : 0,
            TransitionType = transitionType,
            TransitionTime = transitionTime,
            TransitionUserId = transitionUserId,
            TransitionUserName = transitionUserName,
            TransitionDeptId = transitionDeptId,
            TransitionDeptName = transitionDeptName,
            TransitionComment = transitionComment,
            TransitionAttachments = transitionAttachments,
            ElapsedMilliseconds = elapsedMs
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// 写入一条操作记录（TaktFlowOperation），支持审计字段：部门、IP、设备、操作前后状态、失败信息
    /// </summary>
    private async Task WriteOperationAsync(
        TaktFlowInstance instance,
        int operationType,
        string nodeId,
        string nodeName,
        string content,
        string? comment,
        long? operatorId = null,
        string? operatorName = null,
        long? operatorDeptId = null,
        string? operatorDeptName = null,
        string? operationIp = null,
        string? operationDevice = null,
        string? beforeStatus = null,
        string? afterStatus = null,
        int operationResult = 0,
        string? errorMessage = null)
    {
        long opId;
        string opName;
        if (operatorId.HasValue && operatorName != null)
        {
            opId = operatorId.Value;
            opName = operatorName;
        }
        else
        {
            var user = GetCurrentUser();
            if (user == null) return;
            opId = user.Id;
            opName = !string.IsNullOrEmpty(user.RealName) ? user.RealName : user.UserName ?? "";
        }

        var processName = await GetProcessNameBySchemeIdAsync(instance.SchemeId).ConfigureAwait(false);
        await _operationRepository.CreateAsync(new TaktFlowOperation
        {
            InstanceId = instance.Id,
            InstanceCode = instance.InstanceCode,
            ProcessKey = instance.ProcessKey,
            ProcessName = processName,
            OperationType = operationType,
            OperationTime = DateTime.Now,
            OperatorId = opId,
            OperatorName = opName,
            OperatorDeptId = operatorDeptId,
            OperatorDeptName = operatorDeptName,
            NodeId = nodeId,
            NodeName = nodeName,
            OperationContent = content,
            OperationComment = comment,
            BeforeStatus = beforeStatus,
            AfterStatus = afterStatus,
            OperationIp = operationIp,
            OperationDevice = operationDevice,
            OperationResult = operationResult,
            ErrorMessage = errorMessage
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// 发起流程时：按 ProcessKey + BusinessKey 找到业务单并回写 InstanceId，建立“实体-流程”关联。
    /// 与 Domain 中带 InstanceId 的实体对应：公告、文控文档。
    /// </summary>
    private async Task SetBusinessFlowInstanceIdAsync(string? processKey, string? businessKey, long flowInstanceId)
    {
        if (string.IsNullOrEmpty(processKey) || string.IsNullOrEmpty(businessKey) || !long.TryParse(businessKey, out var businessId))
            return;
        if (processKey == TaktWorkflowProcessKeys.ProcessKeyAnnouncement)
        {
            var ann = await _announcementRepository.GetByIdAsync(businessId).ConfigureAwait(false);
            if (ann != null)
            {
                ann.InstanceId = flowInstanceId;
                await _announcementRepository.UpdateAsync(ann).ConfigureAwait(false);
            }
        }
        else if (processKey == TaktWorkflowProcessKeys.ProcessKeyDocument)
        {
            var doc = await _documentRepository.GetByIdAsync(businessId).ConfigureAwait(false);
            if (doc != null)
            {
                doc.InstanceId = flowInstanceId;
                await _documentRepository.UpdateAsync(doc).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// 流程结束（通过/驳回）时：按 InstanceId 查找已关联的业务单并更新状态。
    /// 遍历流程处理已知的、带 InstanceId 的实体（公告、文控文档），找到即更新并返回。
    /// </summary>
    private async Task UpdateBusinessStatusAsync(TaktFlowInstance instance, bool approved, DateTime approvedTime)
    {
        var ann = await _announcementRepository.GetAsync(a => a.InstanceId == instance.Id).ConfigureAwait(false);
        if (ann != null)
        {
            ann.AnnouncementStatus = approved ? 2 : 5;
            if (approved)
                ann.PublishTime = approvedTime;
            await _announcementRepository.UpdateAsync(ann).ConfigureAwait(false);
            return;
        }
        var doc = await _documentRepository.GetAsync(d => d.InstanceId == instance.Id).ConfigureAwait(false);
        if (doc != null)
        {
            doc.DocumentStatus = approved ? 4 : 3;
            if (approved)
                doc.ApprovedTime = approvedTime;
            await _documentRepository.UpdateAsync(doc).ConfigureAwait(false);
        }
    }
}
