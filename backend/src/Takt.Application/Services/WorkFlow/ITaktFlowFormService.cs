// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowFormService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：流程表单表应用服务接口，定义FlowForm管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 流程表单表应用服务接口
/// </summary>
public interface ITaktFlowFormService
{
    /// <summary>
    /// 获取流程表单表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFlowFormDto>> GetFlowFormListAsync(TaktFlowFormQueryDto queryDto);

    /// <summary>
    /// 根据ID获取流程表单表
    /// </summary>
    /// <param name="id">流程表单表ID</param>
    /// <returns>流程表单表DTO</returns>
    Task<TaktFlowFormDto?> GetFlowFormByIdAsync(long id);

    /// <summary>
    /// 获取流程表单表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>流程表单表选项列表</returns>
    Task<List<TaktSelectOption>> GetFlowFormOptionsAsync();

    /// <summary>
    /// 创建流程表单表
    /// </summary>
    /// <param name="dto">创建流程表单表DTO</param>
    /// <returns>流程表单表DTO</returns>
    Task<TaktFlowFormDto> CreateFlowFormAsync(TaktFlowFormCreateDto dto);

    /// <summary>
    /// 更新流程表单表
    /// </summary>
    /// <param name="id">流程表单表ID</param>
    /// <param name="dto">更新流程表单表DTO</param>
    /// <returns>流程表单表DTO</returns>
    Task<TaktFlowFormDto> UpdateFlowFormAsync(long id, TaktFlowFormUpdateDto dto);

    /// <summary>
    /// 删除流程表单表(FlowForm)
    /// </summary>
    /// <param name="id">流程表单表(FlowForm)ID</param>
    /// <returns>任务</returns>
    Task DeleteFlowFormByIdAsync(long id);

    /// <summary>
    /// 批量删除流程表单表(FlowForm)
    /// </summary>
    /// <param name="ids">流程表单表(FlowForm)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteFlowFormBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新流程表单表(FlowForm)FormStatus
    /// </summary>
    /// <param name="dto">流程表单表(FlowForm)FormStatusDTO</param>
    /// <returns>流程表单表(FlowForm)DTO</returns>
    Task<TaktFlowFormDto> UpdateFlowFormFormStatusAsync(TaktFlowFormFormStatusDto dto);

    /// <summary>
    /// 更新流程表单表(FlowForm)排序
    /// </summary>
    /// <param name="dto">流程表单表(FlowForm)排序DTO</param>
    /// <returns>流程表单表(FlowForm)DTO</returns>
    Task<TaktFlowFormDto> UpdateFlowFormSortAsync(TaktFlowFormSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetFlowFormTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入流程表单表(FlowForm)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportFlowFormAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出流程表单表(FlowForm)
    /// </summary>
    /// <param name="query">流程表单表(FlowForm)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportFlowFormAsync(TaktFlowFormQueryDto query, string? sheetName, string? fileName);
}

