// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Manufacturing.Bom
// 文件名称：ITaktBillOfMaterialChangeLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：BOM变更记录表应用服务接口（主子表），定义BillOfMaterialChangeLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Bom;

/// <summary>
/// BOM变更记录表应用服务接口（主子表）
/// </summary>
public interface ITaktBillOfMaterialChangeLogService
{
    /// <summary>
    /// 获取BOM变更记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktBillOfMaterialChangeLogDto>> GetBillOfMaterialChangeLogListAsync(TaktBillOfMaterialChangeLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取BOM变更记录表（包含子表数据）
    /// </summary>
    /// <param name="id">BOM变更记录表ID</param>
    /// <returns>BOM变更记录表DTO</returns>
    Task<TaktBillOfMaterialChangeLogDto?> GetBillOfMaterialChangeLogByIdAsync(long id);

    /// <summary>
    /// 获取BOM变更记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>BOM变更记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetBillOfMaterialChangeLogOptionsAsync();

    /// <summary>
    /// 创建BOM变更记录表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建BOM变更记录表DTO</param>
    /// <returns>BOM变更记录表DTO</returns>
    Task<TaktBillOfMaterialChangeLogDto> CreateBillOfMaterialChangeLogAsync(TaktBillOfMaterialChangeLogCreateDto dto);

    /// <summary>
    /// 更新BOM变更记录表（包含子表数据）
    /// </summary>
    /// <param name="id">BOM变更记录表ID</param>
    /// <param name="dto">更新BOM变更记录表DTO</param>
    /// <returns>BOM变更记录表DTO</returns>
    Task<TaktBillOfMaterialChangeLogDto> UpdateBillOfMaterialChangeLogAsync(long id, TaktBillOfMaterialChangeLogUpdateDto dto);

    /// <summary>
    /// 删除BOM变更记录表(BillOfMaterialChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="id">BOM变更记录表(BillOfMaterialChangeLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteBillOfMaterialChangeLogByIdAsync(long id);

    /// <summary>
    /// 批量删除BOM变更记录表(BillOfMaterialChangeLog)（级联删除子表）
    /// </summary>
    /// <param name="ids">BOM变更记录表(BillOfMaterialChangeLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteBillOfMaterialChangeLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetBillOfMaterialChangeLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportBillOfMaterialChangeLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    /// <param name="query">BOM变更记录表(BillOfMaterialChangeLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportBillOfMaterialChangeLogAsync(TaktBillOfMaterialChangeLogQueryDto query, string? sheetName, string? fileName);
}

