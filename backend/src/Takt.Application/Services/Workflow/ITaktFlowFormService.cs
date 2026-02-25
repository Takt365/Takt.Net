// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowFormService.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程表单服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流流程表单服务接口
/// </summary>
public interface ITaktFlowFormService
{
    /// <summary>
    /// 获取流程表单列表（分页）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFlowFormDto>> GetListAsync(TaktFlowFormQueryDto query);

    /// <summary>
    /// 根据ID获取流程表单
    /// </summary>
    /// <param name="id">表单ID</param>
    /// <returns>流程表单 DTO，不存在返回 null</returns>
    Task<TaktFlowFormDto?> GetByIdAsync(long id);

    /// <summary>
    /// 根据表单编码获取流程表单（用于流程方案绑定、发起流程时加载表单）
    /// </summary>
    /// <param name="formCode">表单编码</param>
    /// <returns>流程表单 DTO，不存在或未发布返回 null</returns>
    Task<TaktFlowFormDto?> GetByFormCodeAsync(string formCode);

    /// <summary>
    /// 获取流程表单选项列表（仅已发布，用于下拉框等）
    /// </summary>
    /// <returns>选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建流程表单
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>流程表单 DTO</returns>
    Task<TaktFlowFormDto> CreateAsync(TaktFlowFormCreateDto dto);

    /// <summary>
    /// 更新流程表单
    /// </summary>
    /// <param name="dto">更新 DTO</param>
    /// <returns>流程表单 DTO</returns>
    Task<TaktFlowFormDto> UpdateAsync(TaktFlowFormUpdateDto dto);

    /// <summary>
    /// 更新流程表单状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>流程表单 DTO</returns>
    Task<TaktFlowFormDto> UpdateStatusAsync(TaktFlowFormStatusDto dto);

    /// <summary>
    /// 软删除流程表单
    /// </summary>
    /// <param name="id">表单ID</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量软删除流程表单
    /// </summary>
    /// <param name="ids">表单ID列表</param>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入流程表单
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出流程表单
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktFlowFormQueryDto query, string? sheetName, string? fileName);
}
