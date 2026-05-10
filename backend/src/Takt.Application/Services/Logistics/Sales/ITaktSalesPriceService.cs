// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Sales
// 文件名称：ITaktSalesPriceService.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：销售价格表应用服务接口（主子表），定义SalesPrice管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Sales;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Sales;

/// <summary>
/// 销售价格表应用服务接口（主子表）
/// </summary>
public interface ITaktSalesPriceService
{
    /// <summary>
    /// 获取销售价格表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSalesPriceDto>> GetSalesPriceListAsync(TaktSalesPriceQueryDto queryDto);

    /// <summary>
    /// 根据ID获取销售价格表（包含子表数据）
    /// </summary>
    /// <param name="id">销售价格表ID</param>
    /// <returns>销售价格表DTO</returns>
    Task<TaktSalesPriceDto?> GetSalesPriceByIdAsync(long id);

    /// <summary>
    /// 获取销售价格表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>销售价格表选项列表</returns>
    Task<List<TaktSelectOption>> GetSalesPriceOptionsAsync();

    /// <summary>
    /// 创建销售价格表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建销售价格表DTO</param>
    /// <returns>销售价格表DTO</returns>
    Task<TaktSalesPriceDto> CreateSalesPriceAsync(TaktSalesPriceCreateDto dto);

    /// <summary>
    /// 更新销售价格表（包含子表数据）
    /// </summary>
    /// <param name="id">销售价格表ID</param>
    /// <param name="dto">更新销售价格表DTO</param>
    /// <returns>销售价格表DTO</returns>
    Task<TaktSalesPriceDto> UpdateSalesPriceAsync(long id, TaktSalesPriceUpdateDto dto);

    /// <summary>
    /// 删除销售价格表(SalesPrice)（级联删除子表）
    /// </summary>
    /// <param name="id">销售价格表(SalesPrice)ID</param>
    /// <returns>任务</returns>
    Task DeleteSalesPriceByIdAsync(long id);

    /// <summary>
    /// 批量删除销售价格表(SalesPrice)（级联删除子表）
    /// </summary>
    /// <param name="ids">销售价格表(SalesPrice)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSalesPriceBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新销售价格表(SalesPrice)PriceStatus
    /// </summary>
    /// <param name="dto">销售价格表(SalesPrice)PriceStatusDTO</param>
    /// <returns>销售价格表(SalesPrice)DTO</returns>
    Task<TaktSalesPriceDto> UpdateSalesPricePriceStatusAsync(TaktSalesPricePriceStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSalesPriceTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入销售价格表(SalesPrice)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSalesPriceAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出销售价格表(SalesPrice)
    /// </summary>
    /// <param name="query">销售价格表(SalesPrice)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSalesPriceAsync(TaktSalesPriceQueryDto query, string? sheetName, string? fileName);
}

