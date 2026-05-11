// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Quality.Operation
// 文件名称：ITaktInspectionStandardService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：检验标准表应用服务接口（主子表），定义InspectionStandard管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Quality.Operation;

/// <summary>
/// 检验标准表应用服务接口（主子表）
/// </summary>
public interface ITaktInspectionStandardService
{
    /// <summary>
    /// 获取检验标准表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktInspectionStandardDto>> GetInspectionStandardListAsync(TaktInspectionStandardQueryDto queryDto);

    /// <summary>
    /// 根据ID获取检验标准表（包含子表数据）
    /// </summary>
    /// <param name="id">检验标准表ID</param>
    /// <returns>检验标准表DTO</returns>
    Task<TaktInspectionStandardDto?> GetInspectionStandardByIdAsync(long id);

    /// <summary>
    /// 获取检验标准表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>检验标准表选项列表</returns>
    Task<List<TaktSelectOption>> GetInspectionStandardOptionsAsync();

    /// <summary>
    /// 创建检验标准表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建检验标准表DTO</param>
    /// <returns>检验标准表DTO</returns>
    Task<TaktInspectionStandardDto> CreateInspectionStandardAsync(TaktInspectionStandardCreateDto dto);

    /// <summary>
    /// 更新检验标准表（包含子表数据）
    /// </summary>
    /// <param name="id">检验标准表ID</param>
    /// <param name="dto">更新检验标准表DTO</param>
    /// <returns>检验标准表DTO</returns>
    Task<TaktInspectionStandardDto> UpdateInspectionStandardAsync(long id, TaktInspectionStandardUpdateDto dto);

    /// <summary>
    /// 删除检验标准表(InspectionStandard)（级联删除子表）
    /// </summary>
    /// <param name="id">检验标准表(InspectionStandard)ID</param>
    /// <returns>任务</returns>
    Task DeleteInspectionStandardByIdAsync(long id);

    /// <summary>
    /// 批量删除检验标准表(InspectionStandard)（级联删除子表）
    /// </summary>
    /// <param name="ids">检验标准表(InspectionStandard)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteInspectionStandardBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新检验标准表(InspectionStandard)StandardStatus
    /// </summary>
    /// <param name="dto">检验标准表(InspectionStandard)StandardStatusDTO</param>
    /// <returns>检验标准表(InspectionStandard)DTO</returns>
    Task<TaktInspectionStandardDto> UpdateInspectionStandardStandardStatusAsync(TaktInspectionStandardStandardStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetInspectionStandardTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入检验标准表(InspectionStandard)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportInspectionStandardAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出检验标准表(InspectionStandard)
    /// </summary>
    /// <param name="query">检验标准表(InspectionStandard)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportInspectionStandardAsync(TaktInspectionStandardQueryDto query, string? sheetName, string? fileName);
}

