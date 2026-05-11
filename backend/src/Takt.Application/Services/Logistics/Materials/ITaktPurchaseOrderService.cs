// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Logistics.Materials
// 文件名称：ITaktPurchaseOrderService.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：采购订单表应用服务接口（主子表），定义PurchaseOrder管理的业务操作
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Logistics.Materials;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Materials;

/// <summary>
/// 采购订单表应用服务接口（主子表）
/// </summary>
public interface ITaktPurchaseOrderService
{
    /// <summary>
    /// 获取采购订单表列表（分页）
    /// </summary>
    /// <param name="queryDto">查询DTO</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktPurchaseOrderDto>> GetPurchaseOrderListAsync(TaktPurchaseOrderQueryDto queryDto);

    /// <summary>
    /// 根据ID获取采购订单表（包含子表数据）
    /// </summary>
    /// <param name="id">采购订单表ID</param>
    /// <returns>采购订单表DTO</returns>
    Task<TaktPurchaseOrderDto?> GetPurchaseOrderByIdAsync(long id);

    /// <summary>
    /// 获取采购订单表选项列表（用于下拉框等）
    /// </summary>
    /// <returns>采购订单表选项列表</returns>
    Task<List<TaktSelectOption>> GetPurchaseOrderOptionsAsync();

    /// <summary>
    /// 创建采购订单表（包含子表数据）
    /// </summary>
    /// <param name="dto">创建采购订单表DTO</param>
    /// <returns>采购订单表DTO</returns>
    Task<TaktPurchaseOrderDto> CreatePurchaseOrderAsync(TaktPurchaseOrderCreateDto dto);

    /// <summary>
    /// 更新采购订单表（包含子表数据）
    /// </summary>
    /// <param name="id">采购订单表ID</param>
    /// <param name="dto">更新采购订单表DTO</param>
    /// <returns>采购订单表DTO</returns>
    Task<TaktPurchaseOrderDto> UpdatePurchaseOrderAsync(long id, TaktPurchaseOrderUpdateDto dto);

    /// <summary>
    /// 删除采购订单表(PurchaseOrder)（级联删除子表）
    /// </summary>
    /// <param name="id">采购订单表(PurchaseOrder)ID</param>
    /// <returns>任务</returns>
    Task DeletePurchaseOrderByIdAsync(long id);

    /// <summary>
    /// 批量删除采购订单表(PurchaseOrder)（级联删除子表）
    /// </summary>
    /// <param name="ids">采购订单表(PurchaseOrder)ID列表</param>
    /// <returns>任务</returns>
    Task DeletePurchaseOrderBatchAsync(IEnumerable<long> ids);

    /// <summary>
    /// 更新采购订单表(PurchaseOrder)OrderStatus
    /// </summary>
    /// <param name="dto">采购订单表(PurchaseOrder)OrderStatusDTO</param>
    /// <returns>采购订单表(PurchaseOrder)DTO</returns>
    Task<TaktPurchaseOrderDto> UpdatePurchaseOrderOrderStatusAsync(TaktPurchaseOrderOrderStatusDto dto);

    /// <summary>
    /// 更新采购订单表(PurchaseOrder)DeliveryStatus
    /// </summary>
    /// <param name="dto">采购订单表(PurchaseOrder)DeliveryStatusDTO</param>
    /// <returns>采购订单表(PurchaseOrder)DTO</returns>
    Task<TaktPurchaseOrderDto> UpdatePurchaseOrderDeliveryStatusAsync(TaktPurchaseOrderDeliveryStatusDto dto);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> GetPurchaseOrderTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入采购订单表(PurchaseOrder)
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果（成功数量、失败数量、错误信息列表）</returns>
    Task<(int success, int fail, List<string> errors)> ImportPurchaseOrderAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出采购订单表(PurchaseOrder)
    /// </summary>
    /// <param name="query">采购订单表(PurchaseOrder)查询DTO（包含查询条件）</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件信息（文件名和内容）</returns>
    Task<(string fileName, byte[] content)> ExportPurchaseOrderAsync(TaktPurchaseOrderQueryDto query, string? sheetName, string? fileName);
}

