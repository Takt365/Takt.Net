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
/// <remarks>
/// 标准顺序：GetListAsync → GetByIdAsync → UpdateAsync → DeleteAsync → ExportAsync；其它接口（操作历史、撤销、待办/我的/已办、流程操作）按模块顺序
/// </remarks>
public interface ITaktFlowInstanceService
{
    // ========== 一、实例实体本身（标准顺序） ==========

    /// <summary>
    /// 获取流程实例列表（分页）
    /// </summary>
    Task<TaktPagedResult<TaktFlowInstanceDto>> GetListAsync(TaktFlowInstanceQueryDto query);

    /// <summary>
    /// 根据ID获取流程实例详情
    /// </summary>
    Task<TaktFlowInstanceDetailDto?> GetByIdAsync(long id);

    /// <summary>
    /// 更新流程实例（仅运行中且发起人可更新流程标题与表单数据）
    /// </summary>
    Task UpdateAsync(TaktFlowInstanceUpdateDto dto);

    /// <summary>
    /// 软删除流程实例（单条，仅发起人可删）
    /// </summary>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量软删除流程实例（仅发起人可删）
    /// </summary>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 导出流程实例列表（Excel）
    /// </summary>
    Task<(string fileName, byte[] content)> ExportAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName);

    /// <summary>
    /// 获取流程实例操作历史
    /// </summary>
    Task<List<TaktFlowOperationHistoryItemDto>> GetInstanceOperationHistoriesAsync(long instanceId);

    /// <summary>
    /// 撤销当前节点审批（仅上一审批人为当前用户时可撤销）
    /// </summary>
    Task UndoVerificationAsync(TaktFlowUndoVerificationDto dto);

    // ========== 二、待办（列表、导出） ==========

    /// <summary>
    /// 获取待办列表（分页）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <returns>待办分页结果</returns>
    Task<TaktPagedResult<TaktFlowTodoItemDto>> GetTodoListAsync(TaktFlowTodoQueryDto query);

    /// <summary>
    /// 导出待办列表（Excel）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> ExportTodoAsync(TaktFlowTodoQueryDto query, string? sheetName, string? fileName);

    // ========== 三、我的流程（附加：列表、导出） ==========

    /// <summary>
    /// 获取我的流程列表（分页，仅我发起的）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFlowInstanceDto>> GetMyListAsync(TaktFlowInstanceQueryDto query);

    /// <summary>
    /// 导出我的流程列表（Excel）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> ExportMyAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName);

    // ========== 四、已办（附加：列表、导出） ==========

    /// <summary>
    /// 获取已办列表（分页，我参与过的流程）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFlowInstanceDto>> GetProcessedListAsync(TaktFlowInstanceQueryDto query);

    /// <summary>
    /// 导出已办列表（Excel）
    /// </summary>
    /// <param name="query">查询DTO</param>
    /// <param name="sheetName">工作表名称（可选）</param>
    /// <param name="fileName">文件名（可选）</param>
    /// <returns>文件名与文件二进制内容</returns>
    Task<(string fileName, byte[] content)> ExportProcessedAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName);

    // ========== 五、流程操作 ==========

    /// <summary>
    /// 由实例编码或实例ID解析出实例编码
    /// </summary>
    /// <param name="instanceCode">实例编码（可选）</param>
    /// <param name="flowInstanceId">实例ID（可选），与 instanceCode 二选一</param>
    /// <returns>解析得到的实例编码，未找到时返回 null</returns>
    Task<string?> ResolveInstanceCodeAsync(string? instanceCode, long? flowInstanceId);

    /// <summary>
    /// 启动流程（新建并启动，或从草稿实例启动）
    /// </summary>
    /// <param name="dto">启动DTO；若 FlowInstanceId 有值且该实例为草稿，则从草稿启动</param>
    /// <returns>启动结果DTO</returns>
    Task<TaktFlowStartResultDto> StartAsync(TaktFlowStartDto dto);

    /// <summary>
    /// 创建草稿实例（不进入首节点，状态=草稿，可后续调用 Start 从草稿启动）
    /// </summary>
    /// <param name="dto">同启动DTO（ProcessKey、标题、表单数据等）</param>
    /// <returns>草稿实例信息（含 InstanceId、InstanceCode，用于后续从草稿启动）</returns>
    Task<TaktFlowStartResultDto> CreateDraftAsync(TaktFlowStartDto dto);

    /// <summary>
    /// 从草稿启动（将草稿实例变为运行中并进入首节点）
    /// </summary>
    /// <param name="instanceId">草稿实例ID</param>
    /// <returns>启动结果DTO</returns>
    Task<TaktFlowStartResultDto> StartFromDraftAsync(long instanceId);

    /// <summary>
    /// 办结任务
    /// </summary>
    /// <param name="dto">办结DTO</param>
    Task CompleteTaskAsync(TaktFlowCompleteDto dto);

    /// <summary>
    /// 撤回流程
    /// </summary>
    /// <param name="instanceCode">实例编码</param>
    Task RevokeAsync(string instanceCode);

    /// <summary>
    /// 挂起流程
    /// </summary>
    /// <param name="dto">挂起DTO</param>
    Task SuspendAsync(TaktFlowSuspendDto dto);

    /// <summary>
    /// 恢复流程
    /// </summary>
    /// <param name="dto">恢复DTO</param>
    Task ResumeAsync(TaktFlowResumeDto dto);

    /// <summary>
    /// 终止流程
    /// </summary>
    /// <param name="dto">终止DTO</param>
    Task TerminateAsync(TaktFlowTerminateDto dto);

    /// <summary>
    /// 转办
    /// </summary>
    /// <param name="dto">转办DTO</param>
    Task TransferAsync(TaktFlowTransferDto dto);

    /// <summary>
    /// 加签
    /// </summary>
    /// <param name="dto">加签DTO</param>
    Task AddApproversAsync(TaktFlowAddApproversDto dto);

    /// <summary>
    /// 减签
    /// </summary>
    /// <param name="dto">减签DTO</param>
    Task ReduceApprovalAsync(TaktFlowReduceApprovalDto dto);
}
