// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Services.Workflow
// 文件名称：ITaktFlowSchemeService.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程方案服务接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流流程方案服务接口
/// </summary>
public interface ITaktFlowSchemeService
{
    /// <summary>
    /// 获取流程方案列表（分页）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktFlowSchemeDto>> GetListAsync(TaktFlowSchemeQueryDto query);

    /// <summary>
    /// 根据ID获取流程方案
    /// </summary>
    /// <param name="id">方案ID</param>
    /// <returns>流程方案 DTO，不存在返回 null</returns>
    Task<TaktFlowSchemeDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取流程方案选项列表（用于下拉框等）
    /// </summary>
    /// <returns>选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建流程方案
    /// </summary>
    /// <param name="dto">创建 DTO</param>
    /// <returns>流程方案 DTO</returns>
    Task<TaktFlowSchemeDto> CreateAsync(TaktFlowSchemeCreateDto dto);

    /// <summary>
    /// 更新流程方案
    /// </summary>
    /// <param name="dto">更新 DTO</param>
    /// <returns>流程方案 DTO</returns>
    Task<TaktFlowSchemeDto> UpdateAsync(TaktFlowSchemeUpdateDto dto);

    /// <summary>
    /// 更新流程方案状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    /// <param name="dto">状态 DTO</param>
    /// <returns>流程方案 DTO</returns>
    Task<TaktFlowSchemeDto> UpdateStatusAsync(TaktFlowSchemeStatusDto dto);

    /// <summary>
    /// 根据流程Key获取已发布的流程方案
    /// </summary>
    /// <param name="processKey">流程Key</param>
    /// <returns>流程方案 DTO，不存在或未发布返回 null</returns>
    Task<TaktFlowSchemeDto?> GetByProcessKeyAsync(string processKey);

    /// <summary>
    /// 软删除流程方案
    /// </summary>
    /// <param name="id">方案ID</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量软删除流程方案
    /// </summary>
    /// <param name="ids">方案ID列表</param>
    Task DeleteAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入流程方案
    /// </summary>
    /// <param name="fileStream">Excel 文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出流程方案
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel 文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktFlowSchemeQueryDto query, string? sheetName, string? fileName);
}
