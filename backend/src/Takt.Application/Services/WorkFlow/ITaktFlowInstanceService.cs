// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowInstanceService.cs
// 创建时间：2025-02-26
// 功能描述：流程实例服务接口（实例实体标准：列表/查询/导出；待办/我的/已办：列表/导出；流程操作）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程实例服务接口
/// </summary>
public interface ITaktFlowInstanceService
{
    // ========== 一、实例实体本身（标准顺序） ==========

    /// <summary>
    /// 获取流程实例列表（分页）
    /// </summary>
    Task<TaktPagedResult<TaktFlowInstanceDto>> GetFlowInstanceListAsync(TaktFlowInstanceQueryDto query);

    /// <summary>
    /// 根据ID获取流程实例详情
    /// </summary>
    Task<TaktFlowInstanceDetailDto?> GetFlowInstanceByIdAsync(long id);

    /// <summary>
    /// 更新流程实例（仅运行中且发起人可更新流程标题与表单数据）
    /// </summary>
    Task UpdateFlowInstanceAsync(TaktFlowInstanceUpdateDto dto);

    /// <summary>
    /// 软删除流程实例（单条，仅发起人可删）
    /// </summary>
    Task DeleteFlowInstanceByIdAsync(long id);

    /// <summary>
    /// 批量软删除流程实例（仅发起人可删）
    /// </summary>
    Task DeleteFlowInstanceBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 导出流程实例列表（Excel）
    /// </summary>
    Task<(string fileName, byte[] content)> ExportFlowInstanceAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName);

    /// <summary>
    /// 获取流程实例操作历史
    /// </summary>
    Task<List<TaktFlowOperationHistoryItemDto>> GetFlowInstanceOperationHistoriesAsync(long instanceId);

    /// <summary>
    /// 撤销当前节点审批（仅上一审批人为当前用户时可撤销）
    /// </summary>
    Task UndoFlowInstanceVerificationAsync(TaktFlowUndoVerificationDto dto);

    // ========== 二、待办（列表、导出） ==========

    /// <summary>
    /// 获取待办列表（分页）
    /// </summary>
    Task<TaktPagedResult<TaktFlowTodoItemDto>> GetFlowInstanceTodoListAsync(TaktFlowTodoQueryDto query);

    /// <summary>
    /// 导出待办列表（Excel）
    /// </summary>
    Task<(string fileName, byte[] content)> ExportFlowInstanceTodoAsync(TaktFlowTodoQueryDto query, string? sheetName, string? fileName);

    // ========== 三、我的流程（附加：列表、导出） ==========

    /// <summary>
    /// 获取我的流程列表（分页，仅我发起的）
    /// </summary>
    Task<TaktPagedResult<TaktFlowInstanceDto>> GetFlowInstanceMyListAsync(TaktFlowInstanceQueryDto query);

    /// <summary>
    /// 导出我的流程列表（Excel）
    /// </summary>
    Task<(string fileName, byte[] content)> ExportFlowInstanceMyAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName);

    // ========== 四、已办（附加：列表、导出） ==========

    /// <summary>
    /// 获取已办列表（分页，我参与过的流程）
    /// </summary>
    Task<TaktPagedResult<TaktFlowInstanceDto>> GetFlowInstanceProcessedListAsync(TaktFlowInstanceQueryDto query);

    /// <summary>
    /// 导出已办列表（Excel）
    /// </summary>
    Task<(string fileName, byte[] content)> ExportFlowInstanceProcessedAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName);

    // ========== 五、流程操作 ==========

    /// <summary>
    /// 由实例编码或实例ID解析出实例编码
    /// </summary>
    Task<string?> ResolveFlowInstanceCodeAsync(string? instanceCode, long? flowInstanceId);

    /// <summary>
    /// 启动流程（新建并启动，或从草稿实例启动）
    /// </summary>
    Task<TaktFlowStartResultDto> StartFlowInstanceAsync(TaktFlowStartDto dto);

    /// <summary>
    /// 创建草稿实例（不进入首节点，状态=草稿，可后续调用 Start 从草稿启动）
    /// </summary>
    Task<TaktFlowStartResultDto> CreateFlowInstanceDraftAsync(TaktFlowStartDto dto);

    /// <summary>
    /// 从草稿启动（将草稿实例变为运行中并进入首节点）
    /// </summary>
    Task<TaktFlowStartResultDto> StartFlowInstanceFromDraftAsync(long instanceId);

    /// <summary>
    /// 办结任务
    /// </summary>
    Task CompleteFlowInstanceTaskAsync(TaktFlowCompleteDto dto);

    /// <summary>
    /// 撤回流程
    /// </summary>
    Task RevokeFlowInstanceAsync(string instanceCode);

    /// <summary>
    /// 挂起流程
    /// </summary>
    Task SuspendFlowInstanceAsync(TaktFlowSuspendDto dto);

    /// <summary>
    /// 恢复流程
    /// </summary>
    Task ResumeFlowInstanceAsync(TaktFlowResumeDto dto);

    /// <summary>
    /// 终止流程
    /// </summary>
    Task TerminateFlowInstanceAsync(TaktFlowTerminateDto dto);

    /// <summary>
    /// 转办
    /// </summary>
    Task TransferFlowInstanceAsync(TaktFlowTransferDto dto);

    /// <summary>
    /// 加签
    /// </summary>
    Task AddFlowInstanceApproversAsync(TaktFlowAddApproversDto dto);

    /// <summary>
    /// 减签
    /// </summary>
    Task ReduceFlowInstanceApprovalAsync(TaktFlowReduceApprovalDto dto);
}
