// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowOperationService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：流程操作历史表应用服务接口，定义FlowOperation管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程操作历史表应用服务接口
/// </summary>
public interface ITaktFlowOperationService
{
    /// <summary>
    /// 获取流程操作历史表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFlowOperationDto>> GetFlowOperationListAsync(TaktFlowOperationQueryDto queryDto);

    /// <summary>
    /// 根据ID获取流程操作历史表
    /// </summary>
    /// <param name="id">流程操作历史表ID</param>
    /// <returns>流程操作历史表DTO</returns>
    Task<TaktFlowOperationDto?> GetFlowOperationByIdAsync(long id);

    /// <summary>
    /// 获取流程操作历史表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>流程操作历史表选项列表</returns>
    Task<List<TaktSelectOption>> GetFlowOperationOptionsAsync();

    /// <summary>
    /// 创建流程操作历史表
    /// </summary>
    /// <param name="dto">创建流程操作历史表DTO</param>
    /// <returns>流程操作历史表DTO</returns>
    Task<TaktFlowOperationDto> CreateFlowOperationAsync(TaktFlowOperationCreateDto dto);

    /// <summary>
    /// 更新流程操作历史表
    /// </summary>
    /// <param name="id">流程操作历史表ID</param>
    /// <param name="dto">更新流程操作历史表DTO</param>
    /// <returns>流程操作历史表DTO</returns>
    Task<TaktFlowOperationDto> UpdateFlowOperationAsync(long id, TaktFlowOperationUpdateDto dto);

    /// <summary>
    /// 删除流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="id">流程操作历史表(FlowOperation)ID</param>
    /// <returns>任务</returns>
    Task DeleteFlowOperationByIdAsync(long id);

    /// <summary>
    /// 批量删除流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="ids">流程操作历史表(FlowOperation)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteFlowOperationBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetFlowOperationTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportFlowOperationAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出流程操作历史表(FlowOperation)
    /// </summary>
    /// <param name="query">流程操作历史表(FlowOperation)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportFlowOperationAsync(TaktFlowOperationQueryDto query, string? sheetName, string? fileName);
}

