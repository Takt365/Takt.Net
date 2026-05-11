// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Sales
// 文件名称：ITaktSalesPriceChangeLogService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：销售价格变更记录表应用服务接口，定义SalesPriceChangeLog管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Sales;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Sales;

/// <summary>
/// 销售价格变更记录表应用服务接口
/// </summary>
public interface ITaktSalesPriceChangeLogService
{
    /// <summary>
    /// 获取销售价格变更记录表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSalesPriceChangeLogDto>> GetSalesPriceChangeLogListAsync(TaktSalesPriceChangeLogQueryDto queryDto);

    /// <summary>
    /// 根据ID获取销售价格变更记录表
    /// </summary>
    /// <param name="id">销售价格变更记录表ID</param>
    /// <returns>销售价格变更记录表DTO</returns>
    Task<TaktSalesPriceChangeLogDto?> GetSalesPriceChangeLogByIdAsync(long id);

    /// <summary>
    /// 获取销售价格变更记录表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>销售价格变更记录表选项列表</returns>
    Task<List<TaktSelectOption>> GetSalesPriceChangeLogOptionsAsync();

    /// <summary>
    /// 创建销售价格变更记录表
    /// </summary>
    /// <param name="dto">创建销售价格变更记录表DTO</param>
    /// <returns>销售价格变更记录表DTO</returns>
    Task<TaktSalesPriceChangeLogDto> CreateSalesPriceChangeLogAsync(TaktSalesPriceChangeLogCreateDto dto);

    /// <summary>
    /// 更新销售价格变更记录表
    /// </summary>
    /// <param name="id">销售价格变更记录表ID</param>
    /// <param name="dto">更新销售价格变更记录表DTO</param>
    /// <returns>销售价格变更记录表DTO</returns>
    Task<TaktSalesPriceChangeLogDto> UpdateSalesPriceChangeLogAsync(long id, TaktSalesPriceChangeLogUpdateDto dto);

    /// <summary>
    /// 删除销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    /// <param name="id">销售价格变更记录表(SalesPriceChangeLog)ID</param>
    /// <returns>任务</returns>
    Task DeleteSalesPriceChangeLogByIdAsync(long id);

    /// <summary>
    /// 批量删除销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    /// <param name="ids">销售价格变更记录表(SalesPriceChangeLog)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSalesPriceChangeLogBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSalesPriceChangeLogTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSalesPriceChangeLogAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    /// <param name="query">销售价格变更记录表(SalesPriceChangeLog)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSalesPriceChangeLogAsync(TaktSalesPriceChangeLogQueryDto query, string? sheetName, string? fileName);
}

