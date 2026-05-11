// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Material
// 文件名称：TaktPurchaseOrderItem.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购订单明细实体，定义采购订单明细领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Materials;

/// <summary>
/// Takt采购订单明细实体
/// </summary>
[SugarTable("takt_logistics_materials_purchase_order_item", "采购订单明细表")]
[SugarIndex("ix_takt_logistics_materials_purchase_order_item_purchase_order_id", nameof(PurchaseOrderId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_item_order_line", nameof(PurchaseOrderId), OrderByType.Asc, nameof(LineNumber), OrderByType.Asc, nameof(MaterialCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_item_purchase_order_code", nameof(PurchaseOrderCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_item_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPurchaseOrderItem : TaktEntityBase
{
    /// <summary>
    /// 采购订单ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "purchase_order_id", ColumnDescription = "采购订单ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PurchaseOrderId { get; set; }

    /// <summary>
    /// 采购订单编码（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "purchase_order_code", ColumnDescription = "采购订单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string PurchaseOrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 行号/项号（订单明细行号，与PurchaseOrderId和MaterialCode组成联合唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 来源请购编码
    /// </summary>
    [SugarColumn(ColumnName = "request_code", ColumnDescription = "来源请购编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RequestCode { get; set; }

    /// <summary>
    /// 来源请购行号
    /// </summary>
    [SugarColumn(ColumnName = "request_line_number", ColumnDescription = "来源请购行号", ColumnDataType = "int", IsNullable = true)]
    public int? RequestLineNumber { get; set; }

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料名称
    /// </summary>
    [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string MaterialName { get; set; } = string.Empty;

    /// <summary>
    /// 物料规格
    /// </summary>
    [SugarColumn(ColumnName = "material_specification", ColumnDescription = "物料规格", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? MaterialSpecification { get; set; }

    /// <summary>
    /// 采购单位
    /// </summary>
    [SugarColumn(ColumnName = "purchase_unit", ColumnDescription = "采购单位", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "个")]
    public string PurchaseUnit { get; set; } = "个";

    /// <summary>
    /// 订购数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "order_quantity", ColumnDescription = "订购数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal OrderQuantity { get; set; } = 0;

    /// <summary>
    /// 已入库数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "received_quantity", ColumnDescription = "已入库数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal ReceivedQuantity { get; set; } = 0;

    /// <summary>
    /// 单价（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal UnitPrice { get; set; } = 0;

    /// <summary>
    /// 折扣率（0-100，表示折扣百分比）
    /// </summary>
    [SugarColumn(ColumnName = "discount_rate", ColumnDescription = "折扣率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal DiscountRate { get; set; } = 0;

    /// <summary>
    /// 折扣金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "discount_amount", ColumnDescription = "折扣金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal DiscountAmount { get; set; } = 0;

    /// <summary>
    /// 税费率（0-100，表示税费百分比）
    /// </summary>
    [SugarColumn(ColumnName = "tax_rate", ColumnDescription = "税费率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TaxRate { get; set; } = 0;

    /// <summary>
    /// 税费（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "tax_amount", ColumnDescription = "税费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TaxAmount { get; set; } = 0;

    /// <summary>
    /// 小计金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "subtotal_amount", ColumnDescription = "小计金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SubtotalAmount { get; set; } = 0;

    /// <summary>
    /// 行交货状态（0=未交货，1=部分交货，2=全部交货）
    /// </summary>
    [SugarColumn(ColumnName = "delivery_status", ColumnDescription = "行交货状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DeliveryStatus { get; set; } = 0;
}
