// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Material
// 文件名称：TaktPurchasePriceItem.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购价格明细实体，定义供应商物料价格领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Materials;

/// <summary>
/// Takt采购价格明细实体（供应商物料价格明细表）
/// </summary>
[SugarTable("takt_logistics_materials_purchase_price_item", "采购价格明细表")]
[SugarIndex("ix_takt_logistics_materials_purchase_price_item_price_id", nameof(PriceId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_item_material_code", nameof(MaterialCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_item_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPurchasePriceItem : TaktEntityBase
{
    /// <summary>
    /// 价格ID（关联采购价格表，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "price_id", ColumnDescription = "价格ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PriceId { get; set; }

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料名称
    /// </summary>
    [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? MaterialName { get; set; }

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
    /// 采购价格（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "purchase_price", ColumnDescription = "采购价格", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PurchasePrice { get; set; } = 0;

    /// <summary>
    /// 最小采购量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "min_purchase_quantity", ColumnDescription = "最小采购量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal MinPurchaseQuantity { get; set; } = 0;

    /// <summary>
    /// 最大采购量（基本单位数量，0表示无限制）
    /// </summary>
    [SugarColumn(ColumnName = "max_purchase_quantity", ColumnDescription = "最大采购量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal MaxPurchaseQuantity { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 价格阶梯列表（主子表关系，一个物料价格可以有多个阶梯）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktPurchasePriceScale.ItemId))]
    public List<TaktPurchasePriceScale>? Scales { get; set; }
}
