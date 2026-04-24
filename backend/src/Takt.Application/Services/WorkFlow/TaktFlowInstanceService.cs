// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：TaktFlowInstanceService.cs
// 创建时间：2025-02-26
// 功能描述：流程实例服务（启动、办结、撤回、待办、我的流程），待办按 Instance.MakerList + TaktFlowAddApprover
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Globalization;
using Mapster;
using Newtonsoft.Json.Linq;
using SqlSugar;
using DynamicExpresso;
using Takt.Application.Dtos.Workflow;
using Takt.Application.Services;
using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.HumanResource.Personnel;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程实例服务。实现流程启动、办结、撤回、挂起、恢复、终止、转办、加签、减签及待办/已办/实例详情查询；待办由实例 MakerList 与加签表共同判定。
/// </summary>
public class TaktFlowInstanceService : TaktServiceBase, ITaktFlowInstanceService
{
    private readonly ITaktRepository<TaktFlowScheme> _schemeRepository;
    private readonly ITaktRepository<TaktFlowInstance> _instanceRepository;
    private readonly ITaktRepository<TaktFlowAddApprover> _addApproverRepository;
    private readonly ITaktRepository<TaktFlowExecution> _executionRepository;
    private readonly ITaktRepository<TaktFlowOperation> _operationRepository;
    private readonly ITaktRepository<TaktEmployee> _employeeRepository;
    private readonly ITaktRepository<TaktUserRole> _userRoleRepository;
    private readonly ITaktRepository<TaktUserDept> _userDeptRepository;
    private readonly ITaktRepository<TaktUser> _userRepository;

    /// <summary>
    /// 初始化流程实例服务。流程引擎通用，不包含任何按流程类型的业务逻辑；新增流程仅在库表配置 TaktFlowScheme（SchemeKey 等），无需改代码或发布。
    /// </summary>
    public TaktFlowInstanceService(
        ITaktRepository<TaktFlowScheme> schemeRepository,
        ITaktRepository<TaktFlowInstance> instanceRepository,
        ITaktRepository<TaktFlowAddApprover> addApproverRepository,
        ITaktRepository<TaktFlowExecution> executionRepository,
        ITaktRepository<TaktFlowOperation> operationRepository,
        ITaktRepository<TaktEmployee> employeeRepository,
        ITaktRepository<TaktUserRole> userRoleRepository,
        ITaktRepository<TaktUserDept> userDeptRepository,
        ITaktRepository<TaktUser> userRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _schemeRepository = schemeRepository;
        _instanceRepository = instanceRepository;
        _addApproverRepository = addApproverRepository;
        _executionRepository = executionRepository;
        _operationRepository = operationRepository;
        _employeeRepository = employeeRepository;
        _userRoleRepository = userRoleRepository;
        _userDeptRepository = userDeptRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// 获取用户展示名（来自员工档案，无则用户名）。
    /// </summary>
    private async Task<string> GetUserDisplayNameAsync(Takt.Domain.Entities.Identity.TaktUser user)
    {
        var emp = await _employeeRepository.GetByIdAsync(user.EmployeeId);
        var display = emp?.RealName?.Trim();
        return string.IsNullOrEmpty(display) ? user.UserName : display;
    }

    /// <summary>
    /// 通过用户ID获取用户展示名。
    /// </summary>
    private async Task<string> GetUserDisplayNameByIdAsync(long userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return $"用户{userId}";
        var emp = await _employeeRepository.GetByIdAsync(user.EmployeeId);
        var display = emp?.RealName?.Trim();
        return string.IsNullOrEmpty(display) ? user.UserName : display;
    }

    /// <summary>
    /// 使用调用方传入的业务键与业务类型，不做任何按流程类型的创建或解析。BusinessKey/BusinessType 由调用方传入，引擎不校验；新增流程零改代码。
    /// </summary>
    private static Task<(string? BusinessKey, string? BusinessType)> EnsureBusinessKeyAsync(string? dtoBusinessKey, string? dtoBusinessType)
    {
        return Task.FromResult((dtoBusinessKey, dtoBusinessType));
    }

    /// <summary>
    /// 流程实例状态变更时不回写业务表，由业务方按 FlowInstanceId 或 BusinessKey 查询实例状态并自行更新业务表（如 LeaveStatus）。
    /// </summary>
    private static Task SyncBusinessStatusAsync(TaktFlowInstance instance, int instanceStatus)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 不回写业务表 FlowInstanceId。业务方在调用 StartFlow/SaveDraft 成功后，将返回的 InstanceId 写入业务实体的 FlowInstanceId，完成匹配。
    /// </summary>
    private static Task SetBusinessFlowInstanceIdAsync(string? businessKey, string? businessType, long instanceId)
    {
        return Task.CompletedTask;
    }

    // ========== 一、实例实体本身（列表、查询、导出） ==========

    /// <summary>
    /// 获取流程实例分页列表
    /// </summary>
    public async Task<TaktPagedResult<TaktFlowInstanceDto>> GetFlowInstanceListAsync(TaktFlowInstanceQueryDto query)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null)
            return TaktPagedResult<TaktFlowInstanceDto>.Create(new List<TaktFlowInstanceDto>(), 0, query.PageIndex, query.PageSize);
        var exp = Expressionable.Create<TaktFlowInstance>()
            .And(x => x.IsDeleted == 0)
            .AndIF(query.MyStartedOnly == true, x => x.StartUserId == user.Id)
            .AndIF(!string.IsNullOrWhiteSpace(query.SchemeKey), x => x.SchemeKey == query.SchemeKey!)
            .AndIF(!string.IsNullOrWhiteSpace(query.InstanceCode), x => x.InstanceCode != null && x.InstanceCode.Contains(query.InstanceCode!))
            .AndIF(query.InstanceStatus.HasValue, x => x.InstanceStatus == query.InstanceStatus!.Value)
            .ToExpression();
        var (data, total) = await _instanceRepository.GetPagedAsync(query.PageIndex, query.PageSize, exp);
        var dtos = data.Adapt<List<TaktFlowInstanceDto>>();
        for (var i = 0; i < data.Count; i++)
            dtos[i].CurrentNodeId = data[i].CurrentNodeId;
        return TaktPagedResult<TaktFlowInstanceDto>.Create(dtos, total, query.PageIndex, query.PageSize);
    }

    /// <summary>
    /// 获取流程实例详情
    /// </summary>
    public async Task<TaktFlowInstanceDetailDto?> GetFlowInstanceByIdAsync(long instanceId)
    {
        return await GetInstanceDetailCoreAsync(instanceId);
    }

    /// <summary>
    /// 获取流程实例操作历史
    /// </summary>
    public async Task<List<TaktFlowOperationHistoryItemDto>> GetFlowInstanceOperationHistoriesAsync(long instanceId)
    {
        var list = await _operationRepository.FindAsync(o => o.InstanceId == instanceId && o.IsDeleted == 0);
        return list.OrderByDescending(o => o.CreatedAt).Select(o => new TaktFlowOperationHistoryItemDto
        {
            Content = o.OperationContent ?? "",
            CreateUserId = o.CreatedById,
            CreateUserName = "", // 需要从用户表查询，这里留空
            CreatedAt = o.CreatedAt
        }).ToList();
    }

    /// <summary>
    /// 导出流程实例
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportFlowInstanceAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName)
    {
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowInstance));
        var user = _userContext?.GetCurrentUser();
        if (user == null)
            return (excelFile, Array.Empty<byte>());
        var exp = Expressionable.Create<TaktFlowInstance>()
            .And(x => x.IsDeleted == 0)
            .AndIF(query.MyStartedOnly == true, x => x.StartUserId == user.Id)
            .AndIF(!string.IsNullOrWhiteSpace(query.SchemeKey), x => x.SchemeKey == query.SchemeKey!)
            .AndIF(!string.IsNullOrWhiteSpace(query.InstanceCode), x => x.InstanceCode != null && x.InstanceCode.Contains(query.InstanceCode!))
            .AndIF(query.InstanceStatus.HasValue, x => x.InstanceStatus == query.InstanceStatus!.Value)
            .ToExpression();
        var list = await _instanceRepository.FindAsync(exp);
        var dtos = list?.Adapt<List<TaktFlowInstanceExportDto>>() ?? new List<TaktFlowInstanceExportDto>();
        return await TaktExcelHelper.ExportAsync(dtos, excelSheet, excelFile);
    }

    /// <summary>
    /// 更新流程实例
    /// </summary>
    public async Task UpdateFlowInstanceAsync(TaktFlowInstanceUpdateDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null) ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await _instanceRepository.GetByIdAsync(dto.Id!.Value);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        // 0=运行中，5=草稿
        if (instance.InstanceStatus != 0 && instance.InstanceStatus != 5)
            ThrowBusinessException("validation.flowOnlyRunningOrDraftUpdatable");
        if (instance.StartUserId != user!.Id)
            ThrowBusinessException("validation.flowOnlyStarterCanUpdate");
        if (dto.ProcessTitle != null)
            instance.ProcessTitle = dto.ProcessTitle;
        if (dto.FrmData != null)
            instance.FrmData = dto.FrmData;
        await _instanceRepository.UpdateAsync(instance);
    }

    /// <summary>
    /// 撤销审批
    /// </summary>
    public async Task UndoFlowInstanceVerificationAsync(TaktFlowUndoVerificationDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null) ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await _instanceRepository.GetByIdAsync(dto.FlowInstanceId);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        if (instance.InstanceStatus != 0)
            ThrowBusinessException("validation.flowOnlyRunningCanUndoApproval");
        var lastOps = await _operationRepository.FindAsync(o => o.InstanceId == dto.FlowInstanceId && o.IsDeleted == 0 && o.OperationType == 1);
        var last = lastOps.OrderByDescending(o => o.CreatedAt).FirstOrDefault();
        if (last == null || last!.CreatedById != user!.Id)
            ThrowBusinessException("validation.flowOnlyPreviousApproverCanUndo");
        var executions = await _executionRepository.FindAsync(e => e.InstanceId == dto.FlowInstanceId && e.IsDeleted == 0);
        var lastExec = executions.OrderByDescending(e => e.TransitionTime).FirstOrDefault();
        if (lastExec == null)
            ThrowBusinessException("validation.flowNoTransitionHistoryCannotUndo");
        var scheme = await _schemeRepository.GetByIdAsync(instance.SchemeId);
        scheme = EnsureEntityExists(scheme, "validation.flowSchemeDefinitionNotFound");
        if (string.IsNullOrWhiteSpace(scheme.SchemeContent))
            ThrowBusinessException("validation.flowSchemeContentEmpty");
        var def = ParseProcessJson(scheme.SchemeContent);
        var fromNodeId = lastExec!.FromNodeId ?? "";
        var fromNodeName = lastExec!.FromNodeName ?? "";
        if (string.IsNullOrEmpty(fromNodeId))
            ThrowBusinessException("validation.flowCannotDetermineReturnNode");
        var fromNode = def.Nodes.FirstOrDefault(n => n.Id == fromNodeId);
        if (fromNode == null)
            ThrowBusinessException("validation.flowReturnNodeNotFound");
        var prevId = GetPreviousNodeId(def, fromNodeId);
        var assignees = await ResolveAssignees(fromNode, instance.StartUserId, instance.StartUserName ?? "");
        instance.PreviousNodeId = prevId;
        instance.CurrentNodeName = fromNodeId;
        instance.ActivityName = fromNodeName;
        instance.MakerList = string.Join(",", assignees.Select(a => a.UserId.ToString()));
        await _instanceRepository.UpdateAsync(instance);
        await RecordOperation(instance, 2, lastExec.ToNodeId, lastExec.ToNodeName, user!.Id, await GetUserDisplayNameAsync(user!), "撤销审批", null);
    }

    /// <summary>
    /// 删除流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    /// <returns>任务</returns>
    public async Task DeleteFlowInstanceByIdAsync(long id)
    {
        await DeleteFlowInstanceBatchAsync(new[] { id });
    }

    /// <summary>
    /// 批量删除流程实例
    /// </summary>
    public async Task DeleteFlowInstanceBatchAsync(IEnumerable<long> ids)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null) ThrowBusinessException("validation.loginRequiredFirst");
        if (ids == null) return;
        foreach (var id in ids.Distinct())
        {
            var instance = await _instanceRepository.GetByIdAsync(id);
            if (instance == null) continue;
            if (instance.StartUserId != user!.Id)
                throw new TaktLocalizedException("validation.flowOnlyStarterCanDeleteInstance", "Frontend", instance.InstanceCode);
            instance.IsDeleted = 1;
            instance.DeletedById = user.Id;
            instance.DeletedBy = await GetUserDisplayNameAsync(user!);
            instance.DeletedAt = DateTime.Now;
            await _instanceRepository.UpdateAsync(instance);
        }
    }

    // ========== 二、待办（列表、导出） ==========

    /// <summary>
    /// 获取待办列表
    /// </summary>
    public async Task<TaktPagedResult<TaktFlowTodoItemDto>> GetFlowInstanceTodoListAsync(TaktFlowTodoQueryDto query)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null)
            return TaktPagedResult<TaktFlowTodoItemDto>.Create(new List<TaktFlowTodoItemDto>(), 0, query.PageIndex, query.PageSize);
        var userIdStr = user.Id.ToString();
        var instanceExp = Expressionable.Create<TaktFlowInstance>()
            .And(i => i.IsDeleted == 0 && i.InstanceStatus == 0)
            .AndIF(!string.IsNullOrWhiteSpace(query.SchemeKey), i => i.SchemeKey == query.SchemeKey!)
            .ToExpression();
        var allRunning = await _instanceRepository.FindAsync(instanceExp);
        var fromMakerList = allRunning.Where(i =>
            i.MakerList == "1" || (!string.IsNullOrEmpty(i.MakerList) && i.MakerList!.Split(',').Any(s => s.Trim() == userIdStr))).ToList();
        var addApproverRows = await _addApproverRepository.FindAsync(a =>
            a.ApproverUserId == user.Id && a.Status == 0 && a.IsDeleted == 0);
        var addInstanceIds = addApproverRows.Select(a => a.InstanceId).Distinct().ToList();
        List<TaktFlowInstance> fromAddApprover;
        if (addInstanceIds.Count == 0)
        {
            fromAddApprover = new List<TaktFlowInstance>();
        }
        else
        {
            var addCandidates = (await _instanceRepository.FindAsync(i => addInstanceIds.Contains(i.Id) && i.IsDeleted == 0 && i.InstanceStatus == 0)).ToList();
            // 仅当加签记录上的节点仍为实例「当前节点」时才算待办；流程已离开该节点后不得仍凭旧加签行出现在待办
            fromAddApprover = addCandidates.Where(i =>
                addApproverRows.Any(a =>
                    a.InstanceId == i.Id &&
                    string.Equals(a.ActivityId, i.CurrentNodeName ?? "", StringComparison.Ordinal))).ToList();
        }
        var mergedIds = fromMakerList.Select(i => i.Id).Union(fromAddApprover.Select(i => i.Id)).Distinct().ToList();
        var instances = mergedIds.Count == 0
            ? new List<TaktFlowInstance>()
            : await _instanceRepository.FindAsync(i => mergedIds.Contains(i.Id));
        if (!string.IsNullOrWhiteSpace(query.SchemeKey))
            instances = instances.Where(i => i.SchemeKey == query.SchemeKey).ToList();
        var total = instances.Count;
        var paged = instances.OrderByDescending(i => i.StartTime).Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();
        var list = paged.Select(i => new TaktFlowTodoItemDto
        {
            InstanceId = i.Id,
            InstanceCode = i.InstanceCode ?? "",
            SchemeKey = i.SchemeKey ?? "",
            SchemeName = i.SchemeName ?? "",
            NodeId = i.CurrentNodeId,
            NodeName = i.ActivityName ?? "",
            ProcessTitle = i.ProcessTitle,
            StartUserName = i.StartUserName ?? "",
            StartTime = i.StartTime
        }).ToList();
        return TaktPagedResult<TaktFlowTodoItemDto>.Create(list, total, query.PageIndex, query.PageSize);
    }

    /// <summary>
    /// 导出待办流程列表
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportFlowInstanceTodoAsync(TaktFlowTodoQueryDto query, string? sheetName, string? fileName)
    {
        var paged = await GetFlowInstanceTodoListAsync(new TaktFlowTodoQueryDto
        {
            PageIndex = 1,
            PageSize = int.MaxValue,
            SchemeKey = query.SchemeKey
        });
        var list = paged.Data ?? new List<TaktFlowTodoItemDto>();
        var exportDtos = list.Select(i => new TaktFlowInstanceExportDto
        {
            InstanceCode = i.InstanceCode,
            SchemeKey = i.SchemeKey,
            SchemeName = i.SchemeName,
            ProcessTitle = i.ProcessTitle,
            StartUserName = i.StartUserName,
            StartTime = i.StartTime ?? DateTime.Now
        }).ToList();
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowInstance), "FlowTodo");
        return await TaktExcelHelper.ExportAsync(exportDtos, excelSheet, excelFile);
    }

    // ========== 三、我的流程（列表、导出） ==========

    /// <summary>
    /// 获取我发起的流程分页列表
    /// </summary>
    public async Task<TaktPagedResult<TaktFlowInstanceDto>> GetFlowInstanceMyListAsync(TaktFlowInstanceQueryDto query)
    {
        var q = new TaktFlowInstanceQueryDto
        {
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            SchemeKey = query.SchemeKey,
            InstanceCode = query.InstanceCode,
            InstanceStatus = query.InstanceStatus,
            MyStartedOnly = true
        };
        return await GetFlowInstanceListAsync(q);
    }

    /// <summary>
    /// 导出我发起的流程列表
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportFlowInstanceMyAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName)
    {
        var q = new TaktFlowInstanceQueryDto
        {
            PageIndex = 1,
            PageSize = int.MaxValue,
            SchemeKey = query.SchemeKey,
            InstanceCode = query.InstanceCode,
            InstanceStatus = query.InstanceStatus,
            MyStartedOnly = true
        };
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowInstance), "FlowMy");
        return await ExportFlowInstanceAsync(q, excelSheet, excelFile);
    }

    // ========== 四、已办（列表、导出） ==========

    /// <summary>
    /// 获取已处理的流程分页列表
    /// </summary>
    public async Task<TaktPagedResult<TaktFlowInstanceDto>> GetFlowInstanceProcessedListAsync(TaktFlowInstanceQueryDto query)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null)
            return TaktPagedResult<TaktFlowInstanceDto>.Create(new List<TaktFlowInstanceDto>(), 0, query.PageIndex, query.PageSize);
        var opList = await _operationRepository.FindAsync(o => o.CreatedById == user.Id && o.IsDeleted == 0);
        var execList = await _executionRepository.FindAsync(e => e.TransitionUserId == user.Id && e.IsDeleted == 0);
        var instanceIds = opList.Select(o => o.InstanceId).Union(execList.Select(e => e.InstanceId)).Distinct().ToList();
        if (instanceIds.Count == 0)
            return TaktPagedResult<TaktFlowInstanceDto>.Create(new List<TaktFlowInstanceDto>(), 0, query.PageIndex, query.PageSize);
        var instances = await _instanceRepository.FindAsync(i => instanceIds.Contains(i.Id) && i.IsDeleted == 0);
        if (!string.IsNullOrWhiteSpace(query.SchemeKey))
            instances = instances.Where(i => i.SchemeKey == query.SchemeKey).ToList();
        if (!string.IsNullOrWhiteSpace(query.InstanceCode))
            instances = instances.Where(i => i.InstanceCode != null && i.InstanceCode.Contains(query.InstanceCode)).ToList();
        if (query.InstanceStatus.HasValue)
            instances = instances.Where(i => i.InstanceStatus == query.InstanceStatus!.Value).ToList();
        instances = instances.OrderByDescending(i => i.EndTime ?? i.StartTime).ToList();
        var total = instances.Count;
        var paged = instances.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();
        var dtos = paged.Adapt<List<TaktFlowInstanceDto>>();
        for (var i = 0; i < paged.Count; i++)
            dtos[i].CurrentNodeId = paged[i].CurrentNodeId;
        return TaktPagedResult<TaktFlowInstanceDto>.Create(dtos, total, query.PageIndex, query.PageSize);
    }

    /// <summary>
    /// 导出已处理的流程列表
    /// </summary>
    public async Task<(string fileName, byte[] content)> ExportFlowInstanceProcessedAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName)
    {
        var paged = await GetFlowInstanceProcessedListAsync(new TaktFlowInstanceQueryDto
        {
            PageIndex = 1,
            PageSize = int.MaxValue,
            SchemeKey = query.SchemeKey,
            InstanceCode = query.InstanceCode,
            InstanceStatus = query.InstanceStatus
        });
        var list = paged.Data ?? new List<TaktFlowInstanceDto>();
        var exportDtos = list.Adapt<List<TaktFlowInstanceExportDto>>();
        var (excelSheet, excelFile) = await ResolveExcelExportNamesAsync(sheetName, fileName, nameof(TaktFlowInstance), "FlowProcessed");
        return await TaktExcelHelper.ExportAsync(exportDtos, excelSheet, excelFile);
    }

    // ========== 五、流程操作 ==========

    /// <summary>
    /// 启动流程实例
    /// </summary>
    public async Task<TaktFlowStartResultDto> StartFlowInstanceAsync(TaktFlowStartDto dto)
    {
        if (dto.FlowInstanceId.HasValue && dto.FlowInstanceId.Value > 0)
        {
            var draft = await _instanceRepository.GetByIdAsync(dto.FlowInstanceId.Value);
            // 5=草稿
            if (draft != null && draft.InstanceStatus == 5)
                return await StartFlowInstanceFromDraftAsync(draft.Id);
        }
        return await CreateAndStartAsync(dto);
    }

    /// <summary>
    /// 创建流程实例草稿
    /// </summary>
    public async Task<TaktFlowStartResultDto> CreateFlowInstanceDraftAsync(TaktFlowStartDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null)
            ThrowBusinessException("validation.loginRequiredFirst");
        var scheme = await _schemeRepository.GetAsync(x =>
            x.SchemeKey == dto.SchemeKey && x.IsDeleted == 0 && x.SchemeStatus == 1);
        if (scheme == null)
            throw new TaktLocalizedException("validation.flowPublishedSchemeNotFoundByProcessKey", "Frontend", dto.SchemeKey);
        if (string.IsNullOrWhiteSpace(scheme!.SchemeContent))
            ThrowBusinessException("validation.flowSchemeContentEmpty");
        var def = ParseProcessJson(scheme.SchemeContent);
        var startNode = def.Nodes.FirstOrDefault(n => n.Type.Equals("start", StringComparison.OrdinalIgnoreCase));
        if (startNode == null)
            ThrowBusinessException("validation.flowSchemeMissingStartNode");
        var (businessKey, businessType) = await EnsureBusinessKeyAsync(dto.BusinessKey, dto.BusinessType);
        if (string.IsNullOrEmpty(businessKey)) businessKey = dto.BusinessKey;
        if (string.IsNullOrEmpty(businessType)) businessType = dto.BusinessType;
        var instanceCode = $"WF{DateTime.Now:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";
        var instance = new TaktFlowInstance
        {
            InstanceCode = instanceCode,
            SchemeKey = scheme.SchemeKey,
            SchemeId = scheme.Id,
            SchemeName = scheme.SchemeName,
            FormId = scheme.FormId,
            FormCode = scheme.FormCode,
            BusinessKey = businessKey,
            BusinessType = businessType,
            StartUserId = user!.Id,
            StartUserName = await GetUserDisplayNameAsync(user!),
            StartDeptId = null,
            StartDeptName = null,
            StartTime = DateTime.Now,
            CurrentNodeName = startNode!.Id,
            ActivityName = startNode.Name,
            PreviousNodeId = null,
            FrmData = dto.FrmData,
            // 5=草稿
            InstanceStatus = 5,
            ProcessTitle = dto.ProcessTitle ?? $"{scheme!.SchemeName}-{instanceCode}"
        };
        await _instanceRepository.CreateAsync(instance);
        await SetBusinessFlowInstanceIdAsync(instance.BusinessKey, instance.BusinessType, instance.Id);
        await RecordOperation(instance, 0, startNode.Id, startNode.Name, user!.Id, await GetUserDisplayNameAsync(user!), "保存草稿", null);
        return new TaktFlowStartResultDto
        {
            InstanceCode = instance.InstanceCode,
            InstanceId = instance.Id,
            SchemeKey = scheme.SchemeKey,
            SchemeName = scheme.SchemeName
        };
    }

    /// <summary>
    /// 从草稿启动流程实例
    /// </summary>
    public async Task<TaktFlowStartResultDto> StartFlowInstanceFromDraftAsync(long instanceId)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null)
            ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await _instanceRepository.GetByIdAsync(instanceId);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        // 5=草稿
        if (instance.InstanceStatus != 5)
            ThrowBusinessException("validation.flowOnlyDraftCanStartFromDraft");
        if (instance.StartUserId != user!.Id)
            ThrowBusinessException("validation.flowOnlyStarterCanStartFromDraft");
        var scheme = await _schemeRepository.GetByIdAsync(instance.SchemeId);
        scheme = EnsureEntityExists(scheme, "validation.flowSchemeDefinitionNotFound");
        if (string.IsNullOrWhiteSpace(scheme.SchemeContent))
            ThrowBusinessException("validation.flowSchemeContentEmpty");
        var def = ParseProcessJson(scheme.SchemeContent);
        var startNode = def.Nodes.FirstOrDefault(n => n.Id == instance.CurrentNodeName && n.Type.Equals("start", StringComparison.OrdinalIgnoreCase))
            ?? def.Nodes.FirstOrDefault(n => n.Type.Equals("start", StringComparison.OrdinalIgnoreCase));
        if (startNode == null)
            ThrowBusinessException("validation.flowSchemeMissingStartNode");
        // 0=运行中
        instance.InstanceStatus = 0;
        await _instanceRepository.UpdateAsync(instance);
        await RecordOperation(instance!, 0, startNode!.Id, startNode.Name, user!.Id, await GetUserDisplayNameAsync(user!), "从草稿启动", null);
        var nextNodeId = GetNextNodeId(def, startNode.Id, instance.FrmData);
        if (!string.IsNullOrEmpty(nextNodeId))
        {
            var nextNode = def.Nodes.FirstOrDefault(n => n.Id == nextNodeId);
            while (nextNode != null && nextNode.Type.Equals("copy", StringComparison.OrdinalIgnoreCase))
            {
                instance!.PreviousNodeId = instance.CurrentNodeName;
                instance.CurrentNodeName = nextNode.Id;
                instance.ActivityName = nextNode.Name;
                instance.MakerList = null;
                await _instanceRepository.UpdateAsync(instance);
                await RecordHistory(instance, startNode!.Id, startNode.Name, nextNode.Id, nextNode.Name, user!.Id, await GetUserDisplayNameAsync(user!), null, null);
                await RecordOperation(instance, 1, nextNode.Id, nextNode.Name, user.Id, await GetUserDisplayNameAsync(user!), "抄送", null);
                nextNodeId = GetNextNodeId(def, nextNode.Id, instance.FrmData);
                nextNode = string.IsNullOrEmpty(nextNodeId) ? null : def.Nodes.FirstOrDefault(n => n.Id == nextNodeId);
            }
            if (nextNode != null && nextNode.Type.Equals("userTask", StringComparison.OrdinalIgnoreCase))
            {
                var assignees = await ResolveAssignees(nextNode, instance!.StartUserId, instance.StartUserName ?? "");
                instance.MakerList = assignees.Count > 0 ? string.Join(",", assignees.Select(a => a.UserId.ToString())) : instance.StartUserId.ToString();
                instance.PreviousNodeId = instance.CurrentNodeName;
                instance.CurrentNodeName = nextNode.Id;
                instance.ActivityName = nextNode.Name;
                await _instanceRepository.UpdateAsync(instance);
                await RecordHistory(instance, instance.CurrentNodeName ?? startNode!.Id, instance.ActivityName ?? "", nextNode.Id, nextNode.Name, user!.Id, await GetUserDisplayNameAsync(user!), null, null);
            }
            else if (nextNode != null && nextNode.Type.Equals("end", StringComparison.OrdinalIgnoreCase))
            {
                instance!.InstanceStatus = 1;
                instance.EndTime = DateTime.Now;
                instance.PreviousNodeId = instance.CurrentNodeName;
                instance.CurrentNodeName = nextNode.Id;
                instance.ActivityName = nextNode.Name;
                instance.MakerList = null;
                await _instanceRepository.UpdateAsync(instance);
                await SyncBusinessStatusAsync(instance, 1);
                await RecordHistory(instance, instance.PreviousNodeId ?? startNode!.Id, instance.ActivityName ?? "", nextNode.Id, nextNode.Name, user!.Id, await GetUserDisplayNameAsync(user!), null, null);
            }
        }
        return new TaktFlowStartResultDto
        {
            InstanceCode = instance.InstanceCode,
            InstanceId = instance.Id,
            SchemeKey = instance.SchemeKey,
            SchemeName = instance.SchemeName
        };
    }

    /// <summary>
    /// 私有方法：根据创建DTO生成并启动一个全新的流程实例
    /// </summary>
    private async Task<TaktFlowStartResultDto> CreateAndStartAsync(TaktFlowStartDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null)
            ThrowBusinessException("validation.loginRequiredFirst");
        var scheme = await _schemeRepository.GetAsync(x =>
            x.SchemeKey == dto.SchemeKey && x.IsDeleted == 0 && x.SchemeStatus == 1);
        if (scheme == null)
            throw new TaktLocalizedException("validation.flowPublishedSchemeNotFoundByProcessKey", "Frontend", dto.SchemeKey);
        if (string.IsNullOrWhiteSpace(scheme.SchemeContent))
            ThrowBusinessException("validation.flowSchemeContentEmpty");
        var def = ParseProcessJson(scheme.SchemeContent);
        var startNode = def.Nodes.FirstOrDefault(n => n.Type.Equals("start", StringComparison.OrdinalIgnoreCase));
        if (startNode == null)
            ThrowBusinessException("validation.flowSchemeMissingStartNode");
        var (businessKey, businessType) = await EnsureBusinessKeyAsync(dto.BusinessKey, dto.BusinessType);
        if (string.IsNullOrEmpty(businessKey)) businessKey = dto.BusinessKey;
        if (string.IsNullOrEmpty(businessType)) businessType = dto.BusinessType;
        var instanceCode = $"WF{DateTime.Now:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";
        var instance = new TaktFlowInstance
        {
            InstanceCode = instanceCode,
            SchemeKey = scheme.SchemeKey,
            SchemeId = scheme.Id,
            SchemeName = scheme.SchemeName,
            FormId = scheme.FormId,
            FormCode = scheme.FormCode,
            BusinessKey = businessKey,
            BusinessType = businessType,
            StartUserId = user!.Id,
            StartUserName = await GetUserDisplayNameAsync(user!),
            StartDeptId = null,
            StartDeptName = null,
            StartTime = DateTime.Now,
            CurrentNodeName = startNode!.Id,
            ActivityName = startNode.Name,
            PreviousNodeId = null,
            FrmData = dto.FrmData,
            // 0=运行中
            InstanceStatus = 0,
            ProcessTitle = dto.ProcessTitle ?? $"{scheme!.SchemeName}-{instanceCode}"
        };
        await _instanceRepository.CreateAsync(instance);
        await SetBusinessFlowInstanceIdAsync(instance.BusinessKey, instance.BusinessType, instance.Id);
        await RecordOperation(instance, 0, startNode.Id, startNode.Name, user!.Id, await GetUserDisplayNameAsync(user!), "启动流程", null);
        var nextNodeId = GetNextNodeId(def, startNode.Id, instance.FrmData);
        if (!string.IsNullOrEmpty(nextNodeId))
        {
            var nextNode = def.Nodes.FirstOrDefault(n => n.Id == nextNodeId);
            while (nextNode != null && nextNode.Type.Equals("copy", StringComparison.OrdinalIgnoreCase))
            {
                instance.PreviousNodeId = instance.CurrentNodeName;
                instance.CurrentNodeName = nextNode.Id;
                instance.ActivityName = nextNode.Name;
                instance.MakerList = null;
                await _instanceRepository.UpdateAsync(instance);
                await RecordHistory(instance, startNode.Id, startNode.Name, nextNode.Id, nextNode.Name, user.Id, await GetUserDisplayNameAsync(user!), null, null);
                await RecordOperation(instance, 1, nextNode.Id, nextNode.Name, user.Id, await GetUserDisplayNameAsync(user!), "抄送", null);
                nextNodeId = GetNextNodeId(def, nextNode.Id, instance.FrmData);
                nextNode = string.IsNullOrEmpty(nextNodeId) ? null : def.Nodes.FirstOrDefault(n => n.Id == nextNodeId);
            }
            if (nextNode != null && nextNode.Type.Equals("userTask", StringComparison.OrdinalIgnoreCase))
            {
                var assignees = await ResolveAssignees(nextNode, user.Id, await GetUserDisplayNameAsync(user!));
                instance.MakerList = assignees.Count > 0 ? string.Join(",", assignees.Select(a => a.UserId.ToString())) : instance.StartUserId.ToString();
                instance.PreviousNodeId = instance.CurrentNodeName;
                instance.CurrentNodeName = nextNode.Id;
                instance.ActivityName = nextNode.Name;
                await _instanceRepository.UpdateAsync(instance);
                await RecordHistory(instance, instance.CurrentNodeName ?? startNode.Id, instance.ActivityName ?? "", nextNode.Id, nextNode.Name, user.Id, await GetUserDisplayNameAsync(user!), null, null);
            }
            else if (nextNode != null && nextNode.Type.Equals("end", StringComparison.OrdinalIgnoreCase))
            {
                instance.InstanceStatus = 1;
                instance.EndTime = DateTime.Now;
                instance.PreviousNodeId = instance.CurrentNodeName;
                instance.CurrentNodeName = nextNode.Id;
                instance.ActivityName = nextNode.Name;
                instance.MakerList = null;
                await _instanceRepository.UpdateAsync(instance);
                await SyncBusinessStatusAsync(instance, 1);
                await RecordHistory(instance, instance.CurrentNodeName ?? startNode.Id, instance.ActivityName ?? "", nextNode.Id, nextNode.Name, user.Id, await GetUserDisplayNameAsync(user!), null, null);
            }
        }
        return new TaktFlowStartResultDto
        {
            InstanceCode = instance.InstanceCode,
            InstanceId = instance.Id,
            SchemeKey = scheme.SchemeKey,
            SchemeName = scheme.SchemeName
        };
    }

    /// <summary>
    /// 完成流程节点任务
    /// </summary>
    public async Task CompleteFlowInstanceTaskAsync(TaktFlowCompleteDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null)
            ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await ResolveInstanceAsync(dto.FlowInstanceId, dto.InstanceCode);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        if (instance.InstanceStatus != 0)
            ThrowBusinessException("validation.flowEndedOrWithdrawn");
        if (!await CanUserVerifyAsync(instance.Id, instance.CurrentNodeName, instance.MakerList, user!.Id))
            ThrowBusinessException("validation.flowNoPermissionForTask");
        var scheme = await _schemeRepository.GetByIdAsync(instance.SchemeId);
        scheme = EnsureEntityExists(scheme, "validation.flowSchemeDefinitionNotFound");
        if (string.IsNullOrWhiteSpace(scheme.SchemeContent))
            ThrowBusinessException("validation.flowSchemeContentEmpty");
        var def = ParseProcessJson(scheme.SchemeContent);
        var currentNode = def.Nodes.FirstOrDefault(n => n.Id == instance.CurrentNodeName);
        if (currentNode == null)
            ThrowBusinessException("validation.flowCurrentNodeInvalid");
        if (!string.IsNullOrEmpty(dto.FrmData))
            instance.FrmData = dto.FrmData;
        await RecordOperation(instance, 1, instance.CurrentNodeName, instance.ActivityName, user!.Id, await GetUserDisplayNameAsync(user!), dto.Comment ?? (dto.Approved == true ? "通过" : "驳回"), null);
        var completingActivityId = instance.CurrentNodeName ?? "";
        if (dto.Approved == true)
            await MarkCurrentUserAddApproverRecordsDoneAsync(instance.Id, completingActivityId, user!.Id, dto.Comment);
        else
            await MarkCurrentUserAddApproverRecordsRejectedAsync(instance.Id, completingActivityId, user!.Id, dto.Comment);
        if (dto.Approved == false)
        {
            await RejectToPreviousNode(instance, def, user.Id, await GetUserDisplayNameAsync(user!), dto.Comment, dto.NodeRejectStep?.ToString());
            return;
        }
        var nextNodeId = GetNextNodeId(def, instance.CurrentNodeName, instance.FrmData);
        if (string.IsNullOrEmpty(nextNodeId))
        {
            instance.InstanceStatus = 1;
            instance.EndTime = DateTime.Now;
            instance.CurrentNodeName = null;
            instance.ActivityName = null;
            instance.MakerList = null;
            await _instanceRepository.UpdateAsync(instance);
            await SyncBusinessStatusAsync(instance, 1);
            await RecordHistory(instance, instance.CurrentNodeName ?? "", instance.ActivityName ?? "", "end", "结束", user.Id, await GetUserDisplayNameAsync(user!), dto.Comment, null);
            return;
        }
        var nextNode = def.Nodes.FirstOrDefault(n => n.Id == nextNodeId);
        if (nextNode == null)
        {
            instance.InstanceStatus = 1;
            instance.EndTime = DateTime.Now;
            instance.CurrentNodeName = null;
            instance.ActivityName = null;
            instance.MakerList = null;
            await _instanceRepository.UpdateAsync(instance);
            await SyncBusinessStatusAsync(instance, 1);
            await RecordHistory(instance, instance.CurrentNodeName ?? "", instance.ActivityName ?? "", "end", "结束", user.Id, await GetUserDisplayNameAsync(user!), dto.Comment, null);
            return;
        }
        await RecordHistory(instance, instance.CurrentNodeName ?? "", instance.ActivityName ?? "", nextNode.Id, nextNode.Name, user.Id, await GetUserDisplayNameAsync(user!), dto.Comment, null);
        // 抄送节点：自动穿过，直至下一节点为 userTask 或 end
        while (nextNode.Type.Equals("copy", StringComparison.OrdinalIgnoreCase))
        {
            instance.PreviousNodeId = instance.CurrentNodeName;
            instance.CurrentNodeName = nextNode.Id;
            instance.ActivityName = nextNode.Name;
            instance.MakerList = null;
            await _instanceRepository.UpdateAsync(instance);
            await RecordOperation(instance, 1, nextNode.Id, nextNode.Name, user!.Id, await GetUserDisplayNameAsync(user!), "抄送", null);
            var nextId = GetNextNodeId(def, nextNode.Id, instance.FrmData);
            if (string.IsNullOrEmpty(nextId)) break;
            nextNode = def.Nodes.FirstOrDefault(n => n.Id == nextId);
            if (nextNode == null) break;
            await RecordHistory(instance, instance.CurrentNodeName ?? "", instance.ActivityName ?? "", nextNode.Id, nextNode.Name, user.Id, await GetUserDisplayNameAsync(user!), "抄送后流转", null);
        }
        if (nextNode == null)
            return;
        if (nextNode.Type.Equals("end", StringComparison.OrdinalIgnoreCase))
        {
            instance.InstanceStatus = 1;
            instance.EndTime = DateTime.Now;
            instance.PreviousNodeId = instance.CurrentNodeName;
            instance.CurrentNodeName = nextNode.Id;
            instance.ActivityName = nextNode.Name;
            instance.MakerList = null;
            await _instanceRepository.UpdateAsync(instance);
            await SyncBusinessStatusAsync(instance, 1);
            return;
        }
        if (nextNode.Type.Equals("userTask", StringComparison.OrdinalIgnoreCase))
        {
            var assignees = await ResolveAssignees(nextNode, instance.StartUserId, instance.StartUserName);
            if (assignees.Count == 0 && dto.SelectedAssigneeIds != null && dto.SelectedAssigneeIds.Count > 0)
            {
                if (dto.SelectedAssigneeIds.Count > 0)
                    instance.MakerList = string.Join(",", dto.SelectedAssigneeIds);
                else
                    instance.MakerList = instance.StartUserId.ToString();
            }
            else
            {
                instance.MakerList = assignees.Count > 0 ? string.Join(",", assignees.Select(a => a.UserId.ToString())) : instance.StartUserId.ToString();
            }
            instance.PreviousNodeId = instance.CurrentNodeName;
            instance.CurrentNodeName = nextNode.Id;
            instance.ActivityName = nextNode.Name;
            instance.CurrentNodeId = null;
            await _instanceRepository.UpdateAsync(instance);
        }
    }

    /// <summary>
    /// 根据 flowInstanceId 或 instanceCode 解析实例实体
    /// </summary>
    private async Task<TaktFlowInstance?> ResolveInstanceAsync(long? flowInstanceId, string? instanceCode)
    {
        if (flowInstanceId.HasValue && flowInstanceId.Value > 0)
            return await _instanceRepository.GetByIdAsync(flowInstanceId.Value);
        if (!string.IsNullOrWhiteSpace(instanceCode))
            return await _instanceRepository.GetAsync(x => x.InstanceCode == instanceCode && x.IsDeleted == 0);
        return null;
    }

    /// <summary>
    /// 办结通过时，将当前用户在当前节点上的未处理加签记录标为已通过，避免仍出现在待办（加签表 Status=0 的查询）
    /// </summary>
    private async Task MarkCurrentUserAddApproverRecordsDoneAsync(long instanceId, string activityId, long userId, string? comment)
    {
        if (string.IsNullOrEmpty(activityId)) return;
        var rows = await _addApproverRepository.FindAsync(a =>
            a.InstanceId == instanceId && a.ActivityId == activityId && a.ApproverUserId == userId && a.Status == 0 && a.IsDeleted == 0);
        foreach (var row in rows)
        {
            row.Status = 1;
            row.VerifyTime = DateTime.Now;
            row.VerifyComment = comment;
            await _addApproverRepository.UpdateAsync(row);
        }
    }

    /// <summary>
    /// 驳回时，将当前用户在当前节点上的未处理加签记录标为驳回，避免仍出现在待办
    /// </summary>
    private async Task MarkCurrentUserAddApproverRecordsRejectedAsync(long instanceId, string activityId, long userId, string? comment)
    {
        if (string.IsNullOrEmpty(activityId)) return;
        var rows = await _addApproverRepository.FindAsync(a =>
            a.InstanceId == instanceId && a.ActivityId == activityId && a.ApproverUserId == userId && a.Status == 0 && a.IsDeleted == 0);
        foreach (var row in rows)
        {
            row.Status = 3;
            row.VerifyTime = DateTime.Now;
            row.VerifyComment = comment;
            await _addApproverRepository.UpdateAsync(row);
        }
    }

    /// <summary>
    /// 判断用户是否有权限审批当前节点
    /// </summary>
    private async Task<bool> CanUserVerifyAsync(long instanceId, string? currentNodeId, string? makerList, long userId)
    {
        if (makerList == "1") return true;
        if (!string.IsNullOrEmpty(makerList) && makerList.Split(',').Any(s => s.Trim() == userId.ToString()))
            return true;
        var addApprovers = await _addApproverRepository.FindAsync(a =>
            a.InstanceId == instanceId && a.ActivityId == currentNodeId && a.Status == 0 && a.IsDeleted == 0);
        return addApprovers.Any(a => a.ApproverUserId == userId);
    }

    /// <summary>
    /// 根据实例代号或实例 ID 获取实例代号
    /// </summary>
    public async Task<string?> ResolveFlowInstanceCodeAsync(string? instanceCode, long? flowInstanceId)
    {
        if (!string.IsNullOrWhiteSpace(instanceCode))
            return instanceCode;
        if (flowInstanceId.HasValue)
        {
            var instance = await _instanceRepository.GetByIdAsync(flowInstanceId.Value);
            return instance?.InstanceCode;
        }
        return null;
    }

    /// <summary>
    /// 撤回流程实例至初始状态
    /// </summary>
    public async Task RevokeFlowInstanceAsync(string instanceCode)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null)
            ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await _instanceRepository.GetAsync(x => x.InstanceCode == instanceCode && x.IsDeleted == 0);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        if (instance.StartUserId != user!.Id)
            ThrowBusinessException("validation.flowOnlyStarterCanWithdraw");
        if (instance.InstanceStatus != 0)
            ThrowBusinessException("validation.flowEndedCannotWithdraw");
        var scheme = await _schemeRepository.GetByIdAsync(instance.SchemeId);
        scheme = EnsureEntityExists(scheme, "validation.flowSchemeDefinitionNotFound");
        if (string.IsNullOrWhiteSpace(scheme.SchemeContent))
            ThrowBusinessException("validation.flowSchemeContentEmpty");
        var def = ParseProcessJson(scheme.SchemeContent);
        var firstUserTask = GetFirstUserTaskNode(def, instance.FrmData);
        if (firstUserTask == null)
        {
            instance.InstanceStatus = 4;
            instance.EndTime = DateTime.Now;
            instance.CurrentNodeName = null;
            instance.ActivityName = null;
            instance.MakerList = null;
            instance.CurrentNodeId = null;
            await _instanceRepository.UpdateAsync(instance);
            await SyncBusinessStatusAsync(instance, 4);
            await RecordOperation(instance, 6, null, null, user!.Id, await GetUserDisplayNameAsync(user!), "撤回流程", null);
            return;
        }
        var revokeFromId = instance.CurrentNodeName ?? "";
        var revokeFromName = instance.ActivityName ?? "";
        instance.InstanceStatus = 0;
        instance.PreviousNodeId = GetPreviousNodeId(def, firstUserTask.Id);
        instance.CurrentNodeName = firstUserTask.Id;
        instance.ActivityName = firstUserTask.Name;
        instance.CurrentNodeId = null;
        instance.EndTime = null;
        var assignees = await ResolveAssignees(firstUserTask, instance.StartUserId, instance.StartUserName ?? "");
        instance.MakerList = string.Join(",", assignees.Select(a => a.UserId.ToString()));
        await _instanceRepository.UpdateAsync(instance);
        await RecordOperation(instance, 6, firstUserTask.Id, firstUserTask.Name, user!.Id, await GetUserDisplayNameAsync(user!), "撤回流程，回到首节点", null);
        await RecordHistory(instance, revokeFromId, revokeFromName, firstUserTask.Id, firstUserTask.Name, user.Id, await GetUserDisplayNameAsync(user!), "撤回流程，回到首节点", null, 5);
    }

    /// <summary>
    /// 挂起流程实例
    /// </summary>
    public async Task SuspendFlowInstanceAsync(TaktFlowSuspendDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null) ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await ResolveInstanceAsync(dto.FlowInstanceId, dto.InstanceCode);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        if (instance.InstanceStatus != 0)
            ThrowBusinessException("validation.flowOnlyRunningCanSuspend");
        if (instance.IsSuspended == 1)
            ThrowBusinessException("validation.flowAlreadySuspended");
        instance.IsSuspended = 1;
        instance.SuspendTime = DateTime.Now;
        instance.SuspendReason = dto.Reason;
        instance.InstanceStatus = 3;
        await _instanceRepository.UpdateAsync(instance);
        await RecordOperation(instance, 7, instance.CurrentNodeName, instance.ActivityName, user!.Id, await GetUserDisplayNameAsync(user!), "挂起流程", null);
    }

    /// <summary>
    /// 恢复挂起的流程实例
    /// </summary>
    public async Task ResumeFlowInstanceAsync(TaktFlowResumeDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null) ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await ResolveInstanceAsync(dto.FlowInstanceId, dto.InstanceCode);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        if (instance.InstanceStatus != 3 || instance.IsSuspended != 1)
            ThrowBusinessException("validation.flowOnlySuspendedCanResume");
        instance.IsSuspended = 0;
        instance.SuspendTime = null;
        instance.SuspendReason = null;
        instance.InstanceStatus = 0;
        await _instanceRepository.UpdateAsync(instance);
        await RecordOperation(instance, 8, instance.CurrentNodeName, instance.ActivityName, user!.Id, await GetUserDisplayNameAsync(user!), "恢复流程", null);
    }

    /// <summary>
    /// 终止流程实例
    /// </summary>
    public async Task TerminateFlowInstanceAsync(TaktFlowTerminateDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null) ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await ResolveInstanceAsync(dto.FlowInstanceId, dto.InstanceCode);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        if (instance.InstanceStatus != 0 && instance.InstanceStatus != 3)
            ThrowBusinessException("validation.flowRunningOrSuspendedCanTerminate");
        var nodeId = instance.CurrentNodeName;
        var nodeName = instance.ActivityName;
        instance.InstanceStatus = 2;
        instance.EndTime = DateTime.Now;
        instance.CurrentNodeName = null;
        instance.ActivityName = null;
        instance.MakerList = null;
        instance.IsSuspended = 0;
        instance.SuspendTime = null;
        instance.SuspendReason = null;
        await _instanceRepository.UpdateAsync(instance);
        await SyncBusinessStatusAsync(instance, 2);
        await RecordOperation(instance, 9, nodeId, nodeName, user!.Id, await GetUserDisplayNameAsync(user!), dto.Reason ?? "终止流程", null);
    }

    /// <summary>
    /// 转办流程任务
    /// </summary>
    public async Task TransferFlowInstanceAsync(TaktFlowTransferDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null) ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await ResolveInstanceAsync(dto.FlowInstanceId, dto.InstanceCode);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        if (instance.InstanceStatus != 0)
            ThrowBusinessException("validation.flowOnlyRunningCanReassign");
        if (!await CanUserVerifyAsync(instance.Id, instance.CurrentNodeName, instance.MakerList, user!.Id))
            ThrowBusinessException("validation.flowNoPermissionToReassign");
        var list = (instance.MakerList ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
        var myIdStr = user.Id.ToString();
        if (!list.Remove(myIdStr))
            list = list.Where(x => x != myIdStr).ToList();
        list.Add(dto.ToUserId.ToString());
        instance.MakerList = string.Join(",", list.Distinct());
        await _instanceRepository.UpdateAsync(instance);
        await RecordOperation(instance, 3, instance.CurrentNodeName, instance.ActivityName, user.Id, await GetUserDisplayNameAsync(user!),
            $"转办给 {dto.ToUserName}({dto.ToUserId})" + (string.IsNullOrEmpty(dto.Comment) ? "" : "：" + dto.Comment), null);
    }

    /// <summary>
    /// 加签
    /// </summary>
    public async Task AddFlowInstanceApproversAsync(TaktFlowAddApproversDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null) ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await ResolveInstanceAsync(dto.FlowInstanceId, dto.InstanceCode);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        if (instance.InstanceStatus != 0)
            ThrowBusinessException("validation.flowOnlyRunningCanAddSign");
        if (!await CanUserVerifyAsync(instance.Id, instance.CurrentNodeName, instance.MakerList, user!.Id))
            ThrowBusinessException("validation.flowNoPermissionToAddSign");
        if (dto.Approvers == null || dto.Approvers.Count == 0)
            ThrowBusinessException("validation.flowCountersignApproverRequired");
        var activityId = instance.CurrentNodeName ?? "";
        if (string.IsNullOrEmpty(activityId)) ThrowBusinessException("validation.flowCurrentNodeInvalid");
        var orderNo = 0;
        var approvers = dto.Approvers;
        var approverUserNames = new List<string>();
        foreach (var approverUserId in approvers!)
        {
            var approverName = await GetUserDisplayNameByIdAsync(approverUserId);
            approverUserNames.Add(approverName);
            
            var addApprover = new TaktFlowAddApprover
            {
                InstanceId = instance!.Id,
                ActivityId = activityId,
                ApproverUserId = approverUserId,
                ApproverUserName = approverName,
                ApproveType = dto.ApproveType?.ToString(),
                OrderNo = orderNo++,
                Status = 0,
                Reason = dto.Reason,
                CreateUserId = user.Id,
                CreateUserName = await GetUserDisplayNameAsync(user!),
                ReturnToSignNode = dto.ReturnToSignNode
            };
            await _addApproverRepository.CreateAsync(addApprover);
        }
        await RecordOperation(instance, 4, instance.CurrentNodeName, instance.ActivityName, user!.Id, await GetUserDisplayNameAsync(user!),
            "加签 " + string.Join(",", approverUserNames), null);
    }

    /// <summary>
    /// 减签
    /// </summary>
    public async Task ReduceFlowInstanceApprovalAsync(TaktFlowReduceApprovalDto dto)
    {
        var user = _userContext?.GetCurrentUser();
        if (user == null) ThrowBusinessException("validation.loginRequiredFirst");
        var instance = await ResolveInstanceAsync(dto.FlowInstanceId, dto.InstanceCode);
        instance = EnsureEntityExists(instance, "validation.flowInstanceNotFound");
        if (instance.InstanceStatus != 0)
            ThrowBusinessException("validation.flowOnlyRunningCanRemoveSign");
        if (!await CanUserVerifyAsync(instance.Id, instance.CurrentNodeName, instance.MakerList, user!.Id))
            ThrowBusinessException("validation.flowNoPermissionToRemoveSign");
        if (!dto.AddApproverId.HasValue)
            ThrowBusinessException("validation.flowAddApproverIdRequired");
        var addApprover = await _addApproverRepository.GetByIdAsync(dto.AddApproverId.Value);
        addApprover = EnsureEntityExists(addApprover, "validation.flowAddApproverNotFound");
        if (addApprover.InstanceId != instance.Id)
            ThrowBusinessException("validation.flowCountersignRecordWrongInstance");
        if (addApprover.Status != 0)
            ThrowBusinessException("validation.flowProcessedCountersignCannotRemove");
        addApprover.IsDeleted = 1;
        addApprover.DeletedById = user!.Id;
        addApprover.DeletedBy = await GetUserDisplayNameAsync(user!);
        addApprover.DeletedAt = DateTime.Now;
        await _addApproverRepository.UpdateAsync(addApprover);
        await RecordOperation(instance, 5, instance.CurrentNodeName, instance.ActivityName, user.Id, await GetUserDisplayNameAsync(user!),
            "减签 移除审批人 " + addApprover.ApproverUserName, null);
    }

    /// <summary>
    /// 获取流程实例详情的核心逻辑
    /// </summary>
    private async Task<TaktFlowInstanceDetailDto?> GetInstanceDetailCoreAsync(long instanceId)
    {
        var instance = await _instanceRepository.GetByIdAsync(instanceId);
        if (instance == null) return null;
        var detail = instance.Adapt<TaktFlowInstanceDetailDto>();
        detail!.CurrentNodeId = instance.CurrentNodeId;
        var executions = await _executionRepository.FindAsync(h => h.InstanceId == instanceId && h.IsDeleted == 0);
        detail.History = executions.OrderBy(h => h.TransitionTime).Select(h => new TaktFlowOperationHistoryItemDto
        {
            OperationId = h.Id,
            NodeName = h.ToNodeName ?? "",
            OperationType = "Transition",
            OperatorName = h.TransitionUserName,
            Comment = h.TransitionComment,
            OperationTime = h.TransitionTime
        }).ToList();
        var user = _userContext?.GetCurrentUser();
        if (user != null)
        {
            detail.CanVerify = await CanUserVerifyAsync(instance.Id, instance.CurrentNodeName, instance.MakerList, user.Id);
            if (instance.InstanceStatus == 0)
            {
                var lastOp = await _operationRepository.FindAsync(o => o.InstanceId == instanceId && o.IsDeleted == 0);
                var last = lastOp.OrderByDescending(o => o.CreatedAt).FirstOrDefault();
                detail.CanUndoVerify = last != null && last.CreatedById == user.Id;
            }
            else
                detail.CanUndoVerify = false;
        }
        else
            detail.CanVerify = detail.CanUndoVerify = false;
        var pendingAdds = await _addApproverRepository.FindAsync(a =>
            a.InstanceId == instanceId && a.IsDeleted == 0 && a.Status == 0);
        detail.PendingAddApprovers = pendingAdds
            .OrderBy(a => a.OrderNo ?? int.MaxValue)
            .ThenBy(a => a.Id)
            .Select(p => p.ApproverUserId)
            .ToList();
        return detail;
    }

    /// <summary>
    /// 解析流程 JSON 定义
    /// </summary>
    private static TaktFlowProcessDef ParseProcessJson(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            throw new InvalidOperationException("流程定义为空");
        try
        {
            return JsonConvert.DeserializeObject<TaktFlowProcessDef>(json) ?? new TaktFlowProcessDef();
        }
        catch (Exception ex)
        {
            TaktLogger.Warning(ex, "流程 JSON 解析失败");
            throw new InvalidOperationException("流程定义格式错误", ex);
        }
    }

    /// <summary>
    /// 获取下一个节点的 ID，支持条件网关动态计算
    /// </summary>
    private static string? GetNextNodeId(TaktFlowProcessDef def, string? currentNodeId, string? frmData = null)
    {
        if (string.IsNullOrEmpty(currentNodeId))
            return null;
        var edges = def.EffectiveEdges.Where(e => e.From == currentNodeId).OrderBy(e => e.Priority ?? int.MaxValue).ToList();
        if (edges.Count == 0) return null;
        if (edges.Count == 1) return edges[0].To;

        if (!string.IsNullOrWhiteSpace(frmData))
        {
            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(frmData) ?? new();
                var interpreter = new Interpreter();
                foreach (var kv in dict)
                {
                    if (kv.Value is JToken jt)
                    {
                        if (jt.Type == JTokenType.Integer) interpreter.SetVariable(kv.Key, jt.Value<int>());
                        else if (jt.Type == JTokenType.Float) interpreter.SetVariable(kv.Key, jt.Value<double>());
                        else if (jt.Type == JTokenType.Boolean) interpreter.SetVariable(kv.Key, jt.Value<bool>());
                        else interpreter.SetVariable(kv.Key, jt.ToString());
                    }
                    else
                    {
                        interpreter.SetVariable(kv.Key, kv.Value);
                    }
                }
                foreach (var edge in edges)
                {
                    if (string.IsNullOrWhiteSpace(edge.Condition)) continue;
                    try
                    {
                        var result = interpreter.Eval<bool>(edge.Condition);
                        if (result) return edge.To;
                    }
                    catch { /* ignore */ }
                }
            }
            catch { /* ignore form parse errors */ }
        }
        return edges.FirstOrDefault()?.To;
    }

    /// <summary>
    /// 获取前一个节点的 ID
    /// </summary>
    private static string? GetPreviousNodeId(TaktFlowProcessDef def, string? currentNodeId)
    {
        if (string.IsNullOrEmpty(currentNodeId))
            return null;
        var edge = def.EffectiveEdges.FirstOrDefault(e => e.To == currentNodeId);
        return edge?.From;
    }

    /// <summary>
    /// 获取第一个用户任务节点
    /// </summary>
    private static TaktFlowNode? GetFirstUserTaskNode(TaktFlowProcessDef def, string? frmData = null)
    {
        var startNode = def.Nodes.FirstOrDefault(n => n.Type.Equals("start", StringComparison.OrdinalIgnoreCase));
        if (startNode == null) return null;
        var nextId = GetNextNodeId(def, startNode.Id, frmData);
        if (string.IsNullOrEmpty(nextId)) return null;
        var next = def.Nodes.FirstOrDefault(n => n.Id == nextId);
        return next != null && next.Type.Equals("userTask", StringComparison.OrdinalIgnoreCase) ? next : null;
    }

    /// <summary>
    /// 解析节点处理人
    /// </summary>
    private async Task<List<(long UserId, string UserName)>> ResolveAssignees(TaktFlowNode? node, long starterUserId, string starterUserName)
    {
        if (node == null)
            return new List<(long, string)> { (starterUserId, starterUserName) };

        var result = new List<(long UserId, string UserName)>();
        var assigneeType = (node.AssigneeType ?? "starter").Trim();

        // 1. 发起人
        if (assigneeType.Equals("starter", StringComparison.OrdinalIgnoreCase))
        {
            result.Add((starterUserId, starterUserName));
        }
        // 2. 发起人自选：运行时由办结请求 SelectedAssigneeIds 提供，此处返回空
        else if (assigneeType.Equals("selfSelect", StringComparison.OrdinalIgnoreCase))
        {
            return new List<(long, string)>();
        }
        // 3. 指定角色、部门、用户等
        else if (assigneeType.Equals("user", StringComparison.OrdinalIgnoreCase) || assigneeType.Equals("assignee", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(assigneeType) || assigneeType == "role" || assigneeType == "dept")
        {
            if (node.AssigneeUserIds != null && node.AssigneeUserIds.Any())
            {
                foreach (var idStr in node.AssigneeUserIds)
                {
                    if (long.TryParse(idStr?.Trim(), out var uid) && uid > 0)
                    {
                        var u = await _userRepository.GetByIdAsync(uid);
                        if (u != null) result.Add((u.Id, await GetUserDisplayNameAsync(u)));
                    }
                }
            }
            if (node.Roles != null && node.Roles.Any())
            {
                var roleIds = node.Roles.Select(x => long.TryParse(x, out var rid) ? rid : 0).Where(x => x > 0).ToList();
                var userRoles = await _userRoleRepository.FindAsync(ur => roleIds.Contains(ur.RoleId));
                var userIds = userRoles.Select(ur => ur.UserId).Distinct().ToList();
                var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id));
                foreach (var u in users) result.Add((u.Id, await GetUserDisplayNameAsync(u)));
            }
            if (node.Departments != null && node.Departments.Any())
            {
                var deptIds = node.Departments.Select(x => long.TryParse(x, out var did) ? did : 0).Where(x => x > 0).ToList();
                var userDepts = await _userDeptRepository.FindAsync(ud => deptIds.Contains(ud.DeptId));
                var userIds = userDepts.Select(ud => ud.UserId).Distinct().ToList();
                var users = await _userRepository.FindAsync(u => userIds.Contains(u.Id));
                foreach (var u in users) result.Add((u.Id, await GetUserDisplayNameAsync(u)));
            }
        }
        else if (assigneeType.StartsWith("user:", StringComparison.OrdinalIgnoreCase))
        {
            var idStr = assigneeType.Substring(5).Trim();
            if (long.TryParse(idStr, out var uid))
                result.Add((uid, idStr)); // Name will be pulled later or just fallback to ID
        }

        if (result.Any())
        {
            // Distinct By UserId
            var distinctResult = new List<(long, string)>();
            var set = new HashSet<long>();
            foreach(var r in result)
            {
                if(set.Add(r.UserId))
                    distinctResult.Add(r);
            }
            return distinctResult;
        }

        return new List<(long, string)> { (starterUserId, starterUserName) };
    }

    /// <summary>
    /// 驳回到上一个节点或指定节点
    /// </summary>
    private async Task RejectToPreviousNode(TaktFlowInstance instance, TaktFlowProcessDef def, long userId, string userName, string? comment, string? nodeRejectStep = null)
    {
        TaktFlowNode? targetNode = null;
        if (!string.IsNullOrWhiteSpace(nodeRejectStep))
        {
            var specified = def.Nodes.FirstOrDefault(n => n.Id == nodeRejectStep && n.Type.Equals("userTask", StringComparison.OrdinalIgnoreCase));
            targetNode = specified;
        }
        if (targetNode == null)
        {
            var previousId = GetPreviousNodeId(def, instance.CurrentNodeName);
            if (!string.IsNullOrEmpty(previousId))
            {
                var prevNode = def.Nodes.FirstOrDefault(n => n.Id == previousId);
                if (prevNode != null && prevNode.Type.Equals("start", StringComparison.OrdinalIgnoreCase))
                    targetNode = GetFirstUserTaskNode(def, instance.FrmData);
                else if (prevNode != null && prevNode.Type.Equals("userTask", StringComparison.OrdinalIgnoreCase))
                    targetNode = prevNode;
            }
        }
        if (targetNode == null)
        {
            var fromId = instance.CurrentNodeName ?? "";
            var fromName = instance.ActivityName ?? "";
            instance.InstanceStatus = 2;
            instance.EndTime = DateTime.Now;
            instance.CurrentNodeName = null;
            instance.ActivityName = null;
            instance.MakerList = null;
            await _instanceRepository.UpdateAsync(instance);
            await SyncBusinessStatusAsync(instance, 2);
            await RecordHistory(instance, fromId, fromName, "end", "结束", userId, userName, comment, null, 0);
            return;
        }
        var rejectFromId = instance.CurrentNodeName ?? "";
        var rejectFromName = instance.ActivityName ?? "";
        instance.InstanceStatus = 0;
        instance.PreviousNodeId = GetPreviousNodeId(def, targetNode.Id);
        instance.CurrentNodeName = targetNode.Id;
        instance.ActivityName = targetNode.Name;
        instance.CurrentNodeId = null;
        instance.EndTime = null;
        var assignees = await ResolveAssignees(targetNode, instance.StartUserId, instance.StartUserName);
        instance.MakerList = string.Join(",", assignees.Select(a => a.UserId.ToString()));
        await _instanceRepository.UpdateAsync(instance);
        await RecordHistory(instance, rejectFromId, rejectFromName, targetNode.Id, targetNode.Name, userId, userName, comment ?? "驳回至" + targetNode.Name, null, 1);
    }

    /// <summary>
    /// 记录流程执行记录（节点流转）。TransitionType：0=正常流转，1=退回，2=转办，3=加签，4=减签，5=撤回
    /// </summary>
    private async Task RecordHistory(TaktFlowInstance instance, string fromId, string fromName, string toId, string toName, long userId, string userName, string? comment, long? taskId, int transitionType = 0)
    {
        var h = new TaktFlowExecution
        {
            InstanceId = instance.Id,
            SchemeId = instance.SchemeId,
            TaskId = taskId,
            InstanceCode = instance.InstanceCode,
            SchemeKey = instance.SchemeKey,
            SchemeName = instance.SchemeName,
            FromNodeId = fromId,
            FromNodeName = fromName,
            ToNodeId = toId,
            ToNodeName = toName,
            TransitionType = transitionType,
            TransitionTime = DateTime.Now,
            TransitionUserId = userId,
            TransitionUserName = userName,
            TransitionComment = comment
        };
        await _executionRepository.CreateAsync(h);
    }

    /// <summary>
    /// 记录流程操作历史。OperationType：0=启动，1=审批，2=撤销，3=转办，4=加签，5=减签，6=撤回，7=挂起，8=恢复，9=终止
    /// </summary>
    private async Task RecordOperation(TaktFlowInstance instance, int operationType, string? nodeId, string? nodeName, long operatorId, string operatorName, string? content, long? taskId)
    {
        var op = new TaktFlowOperation
        {
            InstanceId = instance.Id,
            SchemeId = instance.SchemeId,
            TaskId = taskId,
            InstanceCode = instance.InstanceCode,
            SchemeKey = instance.SchemeKey,
            SchemeName = instance.SchemeName,
            OperationType = operationType,
            NodeId = nodeId,
            NodeName = nodeName,
            OperationContent = content,
            OperationResult = 0
        };
        await _operationRepository.CreateAsync(op);
    }
}
