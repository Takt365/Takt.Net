// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowInstanceService.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程实例服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流流程实例服务接口
/// </summary>
public interface ITaktFlowInstanceService
{
    /// <summary>
    /// 分页查询流程实例
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFlowInstanceDto>> GetListAsync(TaktFlowInstanceQueryDto query);

    /// <summary>
    /// 根据ID获取流程实例
    /// </summary>
    /// <param name="id">实例ID</param>
    /// <returns>流程实例 DTO，不存在返回 null</returns>
    Task<TaktFlowInstanceDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取流程实例选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表（如运行中实例的 InstanceId + 显示名）</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建并启动流程实例（创建后即处于运行中）
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>流程实例 DTO</returns>
    Task<TaktFlowInstanceDto> CreateAsync(TaktFlowInstanceCreateDto dto);

    /// <summary>
    /// 更新流程实例（仅运行中可更新流程标题、优先级等有限字段）
    /// </summary>
    /// <param name="dto">更新 DTO</param>
    /// <returns>流程实例 DTO</returns>
    Task<TaktFlowInstanceDto> UpdateAsync(TaktFlowInstanceUpdateDto dto);

    /// <summary>
    /// 更新流程实例状态（0=运行中，1=已完成，2=已终止，3=挂起等）
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>流程实例 DTO</returns>
    Task<TaktFlowInstanceDto> UpdateStatusAsync(TaktFlowInstanceStatusDto dto);

    /// <summary>
    /// 启动流程（当前实现为创建即启动，此方法仅做校验并返回实例）
    /// </summary>
    /// <param name="dto">启动 DTO</param>
    /// <returns>流程实例 DTO</returns>
    Task<TaktFlowInstanceDto> StartAsync(TaktFlowInstanceStartDto dto);

    /// <summary>
    /// 审批流程（通过则完成，不通过则终止，并回写业务表状态）
    /// </summary>
    /// <param name="dto">审批 DTO</param>
    /// <returns>流程实例 DTO</returns>
    Task<TaktFlowInstanceDto> ApproveAsync(TaktFlowInstanceApproveDto dto);

    /// <summary>
    /// 撤回流程
    /// </summary>
    /// <param name="dto">撤回 DTO</param>
    /// <returns>流程实例 DTO</returns>
    Task<TaktFlowInstanceDto> RecallAsync(TaktFlowInstanceRecallDto dto);

    /// <summary>
    /// 节点指定/转办
    /// </summary>
    /// <param name="dto">节点指定/转办 DTO</param>
    /// <returns>流程实例 DTO</returns>
    Task<TaktFlowInstanceDto> NodeDesignateAsync(TaktFlowInstanceNodeDesignateDto dto);

    /// <summary>
    /// 分页查询流程实例流转历史
    /// </summary>
    /// <param name="query">查询条件（含实例ID）</param>
    /// <returns>流转历史分页结果</returns>
    Task<TaktPagedResult<TaktFlowInstanceHistoryDto>> GetHistoryListAsync(TaktFlowInstanceHistoryQueryDto query);

    /// <summary>
    /// 退回（运行中流程退回到上一节点或指定节点，受方案 IsReturnable 控制）
    /// </summary>
    /// <param name="dto">退回 DTO</param>
    /// <returns>流程实例 DTO</returns>
    Task<TaktFlowInstanceDto> ReturnAsync(TaktFlowInstanceReturnDto dto);

    /// <summary>
    /// 待办列表（运行中且 AssigneeId 为空或为当前用户的实例，分页）
    /// </summary>
    Task<TaktPagedResult<TaktFlowInstanceDto>> GetTodoListAsync(TaktFlowInstanceQueryDto query);

    /// <summary>
    /// 认领（将运行中且未认领的实例设为当前用户处理）
    /// </summary>
    Task<TaktFlowInstanceDto> ClaimAsync(TaktFlowInstanceClaimDto dto);

    /// <summary>
    /// 超时自动推进（后台任务调用；以系统身份推进通过或驳回）
    /// </summary>
    /// <param name="instanceId">实例ID</param>
    /// <param name="pass">true=通过，false=驳回</param>
    Task<TaktFlowInstanceDto> AdvanceByTimeoutAsync(long instanceId, bool pass);

    /// <summary>
    /// 加签（在当前节点增加审批人/分支；需任务与候选人模型，暂未实现）
    /// </summary>
    Task<TaktFlowInstanceDto> AddSignAsync(TaktFlowInstanceAddSignDto dto);

    /// <summary>
    /// 减签（在当前节点减少审批人/分支；需任务与候选人模型，暂未实现）
    /// </summary>
    Task<TaktFlowInstanceDto> ReduceSignAsync(TaktFlowInstanceReduceSignDto dto);

    /// <summary>
    /// 撤销审批（将流程从已完成/已终止恢复到上一节点运行中，仅简单流程支持）
    /// </summary>
    /// <param name="dto">撤销审批 DTO</param>
    /// <returns>流程实例 DTO</returns>
    Task<TaktFlowInstanceDto> UndoVerificationAsync(TaktFlowInstanceUndoVerificationDto dto);

    /// <summary>
    /// 软删除流程实例
    /// </summary>
    /// <param name="id">实例ID</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量软删除流程实例
    /// </summary>
    /// <param name="ids">实例ID列表</param>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入流程实例
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出流程实例
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktFlowInstanceQueryDto query, string? sheetName, string? fileName);
}
