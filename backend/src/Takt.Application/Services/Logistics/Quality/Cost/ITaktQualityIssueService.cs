// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Cost
// 文件名称：ITaktQualityIssueService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：品质问题应对主表应用服务接口（主子表），定义QualityIssue管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Cost;

/// <summary>
/// 品质问题应对主表应用服务接口（主子表）
/// </summary>
public interface ITaktQualityIssueService
{
    /// <summary>
    /// 获取品质问题应对主表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktQualityIssueDto>> GetQualityIssueListAsync(TaktQualityIssueQueryDto queryDto);

    /// <summary>
    /// 根据ID获取品质问题应对主表（包含子表数据）
    /// </summary>
    /// <param name="id">品质问题应对主表ID</param>
    /// <returns>品质问题应对主表DTO</returns>
    Task<TaktQualityIssueDto?> GetQualityIssueByIdAsync(long id);

    /// <summary>
    /// 获取品质问题应对主表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>品质问题应对主表选项列表</returns>
    Task<List<TaktSelectOption>> GetQualityIssueOptionsAsync();

    /// <summary>
    /// 创建品质问题应对主表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建品质问题应对主表DTO</param>
    /// <returns>品质问题应对主表DTO</returns>
    Task<TaktQualityIssueDto> CreateQualityIssueAsync(TaktQualityIssueCreateDto dto);

    /// <summary>
    /// 更新品质问题应对主表（包含子表数据）
    /// </summary>
    /// <param name="id">品质问题应对主表ID</param>
    /// <param name="dto">更新品质问题应对主表DTO</param>
    /// <returns>品质问题应对主表DTO</returns>
    Task<TaktQualityIssueDto> UpdateQualityIssueAsync(long id, TaktQualityIssueUpdateDto dto);

    /// <summary>
    /// 删除品质问题应对主表(QualityIssue)（级联删除子表）
    /// </summary>
    /// <param name="id">品质问题应对主表(QualityIssue)ID</param>
    /// <returns>任务</returns>
    Task DeleteQualityIssueByIdAsync(long id);

    /// <summary>
    /// 批量删除品质问题应对主表(QualityIssue)（级联删除子表）
    /// </summary>
    /// <param name="ids">品质问题应对主表(QualityIssue)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteQualityIssueBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetQualityIssueTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入品质问题应对主表(QualityIssue)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportQualityIssueAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出品质问题应对主表(QualityIssue)
    /// </summary>
    /// <param name="query">品质问题应对主表(QualityIssue)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportQualityIssueAsync(TaktQualityIssueQueryDto query, string? sheetName, string? fileName);
}

