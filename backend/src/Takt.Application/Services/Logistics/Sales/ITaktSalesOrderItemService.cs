// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Sales
// 文件名称：ITaktSalesOrderItemService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：销售订单明细表应用服务接口，定义SalesOrderItem管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Sales;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Sales;

/// <summary>
/// 销售订单明细表应用服务接口
/// </summary>
public interface ITaktSalesOrderItemService
{
    /// <summary>
    /// 获取销售订单明细表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSalesOrderItemDto>> GetSalesOrderItemListAsync(TaktSalesOrderItemQueryDto queryDto);

    /// <summary>
    /// 根据ID获取销售订单明细表
    /// </summary>
    /// <param name="id">销售订单明细表ID</param>
    /// <returns>销售订单明细表DTO</returns>
    Task<TaktSalesOrderItemDto?> GetSalesOrderItemByIdAsync(long id);

    /// <summary>
    /// 获取销售订单明细表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>销售订单明细表选项列表</returns>
    Task<List<TaktSelectOption>> GetSalesOrderItemOptionsAsync();

    /// <summary>
    /// 创建销售订单明细表
    /// </summary>
    /// <param name="dto">创建销售订单明细表DTO</param>
    /// <returns>销售订单明细表DTO</returns>
    Task<TaktSalesOrderItemDto> CreateSalesOrderItemAsync(TaktSalesOrderItemCreateDto dto);

    /// <summary>
    /// 更新销售订单明细表
    /// </summary>
    /// <param name="id">销售订单明细表ID</param>
    /// <param name="dto">更新销售订单明细表DTO</param>
    /// <returns>销售订单明细表DTO</returns>
    Task<TaktSalesOrderItemDto> UpdateSalesOrderItemAsync(long id, TaktSalesOrderItemUpdateDto dto);

    /// <summary>
    /// 删除销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="id">销售订单明细表(SalesOrderItem)ID</param>
    /// <returns>任务</returns>
    Task DeleteSalesOrderItemByIdAsync(long id);

    /// <summary>
    /// 批量删除销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="ids">销售订单明细表(SalesOrderItem)ID列表</param>
    /// <returns>任务</returns>
    Task DeleteSalesOrderItemBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新销售订单明细表(SalesOrderItem)DeliveryStatus
    /// </summary>
    /// <param name="dto">销售订单明细表(SalesOrderItem)DeliveryStatusDTO</param>
    /// <returns>销售订单明细表(SalesOrderItem)DTO</returns>
    Task<TaktSalesOrderItemDto> UpdateSalesOrderItemDeliveryStatusAsync(TaktSalesOrderItemDeliveryStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetSalesOrderItemTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportSalesOrderItemAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出销售订单明细表(SalesOrderItem)
    /// </summary>
    /// <param name="query">销售订单明细表(SalesOrderItem)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportSalesOrderItemAsync(TaktSalesOrderItemQueryDto query, string? sheetName, string? fileName);
}

