// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Statistics.Report
// 文件名称：ITaktReportSchemeService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：报表方案表应用服务接口，定义ReportScheme管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Statistics.Report;
using Takt.Shared.Models;

namespace Takt.Application.Services.Statistics.Report;

/// <summary>
/// 报表方案表应用服务接口
/// </summary>
public interface ITaktReportSchemeService
{
    /// <summary>
    /// 获取报表方案表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktReportSchemeDto>> GetReportSchemeListAsync(TaktReportSchemeQueryDto queryDto);

    /// <summary>
    /// 根据ID获取报表方案表
    /// </summary>
    /// <param name="id">报表方案表ID</param>
    /// <returns>报表方案表DTO</returns>
    Task<TaktReportSchemeDto?> GetReportSchemeByIdAsync(long id);

    /// <summary>
    /// 获取报表方案表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>报表方案表选项列表</returns>
    Task<List<TaktSelectOption>> GetReportSchemeOptionsAsync();

    /// <summary>
    /// 创建报表方案表
    /// </summary>
    /// <param name="dto">创建报表方案表DTO</param>
    /// <returns>报表方案表DTO</returns>
    Task<TaktReportSchemeDto> CreateReportSchemeAsync(TaktReportSchemeCreateDto dto);

    /// <summary>
    /// 更新报表方案表
    /// </summary>
    /// <param name="id">报表方案表ID</param>
    /// <param name="dto">更新报表方案表DTO</param>
    /// <returns>报表方案表DTO</returns>
    Task<TaktReportSchemeDto> UpdateReportSchemeAsync(long id, TaktReportSchemeUpdateDto dto);

    /// <summary>
    /// 删除报表方案表(ReportScheme)
    /// </summary>
    /// <param name="id">报表方案表(ReportScheme)ID</param>
    /// <returns>任务</returns>
    Task DeleteReportSchemeByIdAsync(long id);

    /// <summary>
    /// 批量删除报表方案表(ReportScheme)
    /// </summary>
    /// <param name="ids">报表方案表(ReportScheme)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteReportSchemeBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新报表方案表(ReportScheme)Status
    /// </summary>
    /// <param name="dto">报表方案表(ReportScheme)StatusDTO</param>
    /// <returns>报表方案表(ReportScheme)DTO</returns>
    Task<TaktReportSchemeDto> UpdateReportSchemeStatusAsync(TaktReportSchemeStatusDto dto);

    /// <summary>
    /// 更新报表方案表(ReportScheme)排序
    /// </summary>
    /// <param name="dto">报表方案表(ReportScheme)排序DTO</param>
    /// <returns>报表方案表(ReportScheme)DTO</returns>
    Task<TaktReportSchemeDto> UpdateReportSchemeSortAsync(TaktReportSchemeSortDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetReportSchemeTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入报表方案表(ReportScheme)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportReportSchemeAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出报表方案表(ReportScheme)
    /// </summary>
    /// <param name="query">报表方案表(ReportScheme)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportReportSchemeAsync(TaktReportSchemeQueryDto query, string? sheetName, string? fileName);
}

