// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Sales
// 文件名称：TaktSalesPriceItem.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt销售价格明细实体，定义客户物料价格领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Sales;

/// <summary>
/// Takt销售价格明细实体（客户物料价格明细表）
/// </summary>
[SugarTable("takt_logistics_sales_price_item", "销售价格明细表")]
[SugarIndex("ix_takt_logistics_sales_price_item_price_id", nameof(PriceId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_item_material_code", nameof(MaterialCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_item_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSalesPriceItem : TaktEntityBase
{
    /// <summary>
    /// 价格ID（关联销售价格表，序列化为string以避免Javascript精度问题）
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
    /// 销售单位
    /// </summary>
    [SugarColumn(ColumnName = "sales_unit", ColumnDescription = "销售单位", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "个")]
    public string SalesUnit { get; set; } = "个";

    /// <summary>
    /// 销售价格（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "sales_price", ColumnDescription = "销售价格", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SalesPrice { get; set; } = 0;

    /// <summary>
    /// 最小订购量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "min_order_quantity", ColumnDescription = "最小订购量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal MinOrderQuantity { get; set; } = 0;

    /// <summary>
    /// 最大订购量（基本单位数量，0表示无限制）
    /// </summary>
    [SugarColumn(ColumnName = "max_order_quantity", ColumnDescription = "最大订购量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal MaxOrderQuantity { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 价格阶梯列表（主子表关系，一个物料价格可以有多个阶梯）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktSalesPriceScale.ItemId))]
    public List<TaktSalesPriceScale>? Scales { get; set; }
}
