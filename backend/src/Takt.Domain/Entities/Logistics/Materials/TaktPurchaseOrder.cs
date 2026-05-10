// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Material
// 文件名称：TaktPurchaseOrder.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购订单实体，定义采购订单领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Materials;

/// <summary>
/// Takt采购订单实体
/// </summary>
[SugarTable("takt_logistics_materials_purchase_order", "采购订单表")]
[SugarIndex("ix_takt_logistics_materials_purchase_order_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_order_code", nameof(OrderCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_supplier_code", nameof(SupplierCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_order_date", nameof(OrderDate), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_order_status", nameof(OrderStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_purchase_group", nameof(PurchaseGroup), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPurchaseOrder : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 订单编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "order_code", ColumnDescription = "订单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 供应商编码
    /// </summary>
    [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string SupplierCode { get; set; } = string.Empty;

    /// <summary>
    /// 供应商名称
    /// </summary>
    [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string SupplierName { get; set; } = string.Empty;

    /// <summary>
    /// 订单日期
    /// </summary>
    [SugarColumn(ColumnName = "order_date", ColumnDescription = "订单日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 要求到货日期
    /// </summary>
    [SugarColumn(ColumnName = "required_arrival_date", ColumnDescription = "要求到货日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? RequiredArrivalDate { get; set; }

    /// <summary>
    /// 实际到货日期
    /// </summary>
    [SugarColumn(ColumnName = "actual_arrival_date", ColumnDescription = "实际到货日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ActualArrivalDate { get; set; }

    /// <summary>
    /// 采购组代码
    /// </summary>
    [SugarColumn(ColumnName = "purchase_group", ColumnDescription = "采购组代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PurchaseGroup { get; set; }

    /// <summary>
    /// 订单总数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "total_quantity", ColumnDescription = "订单总数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal TotalQuantity { get; set; } = 0;

    /// <summary>
    /// 订单总金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "total_amount", ColumnDescription = "订单总金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TotalAmount { get; set; } = 0;

    /// <summary>
    /// 折扣金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "discount_amount", ColumnDescription = "折扣金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal DiscountAmount { get; set; } = 0;

    /// <summary>
    /// 税费（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "tax_amount", ColumnDescription = "税费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TaxAmount { get; set; } = 0;

    /// <summary>
    /// 订单实付金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "actual_amount", ColumnDescription = "订单实付金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ActualAmount { get; set; } = 0;

    /// <summary>
    /// 已入库数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "received_quantity", ColumnDescription = "已入库数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal ReceivedQuantity { get; set; } = 0;

    /// <summary>
    /// 已入库金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "received_amount", ColumnDescription = "已入库金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ReceivedAmount { get; set; } = 0;

    /// <summary>
    /// 已付款金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "paid_amount", ColumnDescription = "已付款金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PaidAmount { get; set; } = 0;

    /// <summary>
    /// 订单状态（1=启用，0=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "order_status", ColumnDescription = "订单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int OrderStatus { get; set; } = 1;

    /// <summary>
    /// 交货状态（0=未交货，1=部分交货，2=全部交货）
    /// </summary>
    [SugarColumn(ColumnName = "delivery_status", ColumnDescription = "交货状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DeliveryStatus { get; set; } = 0;

    /// <summary>
    /// 支付方式（0=现金，1=银行转账，2=支票，3=信用证，4=其他）
    /// </summary>
    [SugarColumn(ColumnName = "payment_method", ColumnDescription = "支付方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PaymentMethod { get; set; } = 0;

    /// <summary>
    /// 交货方式（0=自提，1=供应商送货，2=物流配送，3=快递）
    /// </summary>
    [SugarColumn(ColumnName = "delivery_method", ColumnDescription = "交货方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DeliveryMethod { get; set; } = 0;

    /// <summary>
    /// 交货地址
    /// </summary>
    [SugarColumn(ColumnName = "delivery_address", ColumnDescription = "交货地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? DeliveryAddress { get; set; }

    /// <summary>
    /// 订单明细列表（主子表关系，一个订单可以有多个明细）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktPurchaseOrderItem.PurchaseOrderId))]
    public List<TaktPurchaseOrderItem>? Items { get; set; }

    /// <summary>
    /// 采购订单变更记录列表（外键在子表 <see cref="TaktPurchaseOrderChangeLog.OrderId"/>）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktPurchaseOrderChangeLog.PurchaseOrderId))]
    public List<TaktPurchaseOrderChangeLog>? ChangeLogs { get; set; }
}
