// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Sales
// 文件名称：TaktSalesPriceScale.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt销售价格阶梯实体，定义销售价格阶梯领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Sales;

/// <summary>
/// Takt销售价格阶梯实体
/// </summary>
[SugarTable("takt_logistics_sales_price_scale", "销售价格阶梯表")]
[SugarIndex("ix_takt_logistics_sales_price_scale_item_id", nameof(ItemId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_scale_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_price_scale_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSalesPriceScale : TaktEntityBase
{
    /// <summary>
    /// 价格明细ID（关联销售价格明细表，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "item_id", ColumnDescription = "价格明细ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ItemId { get; set; }

    /// <summary>
    /// 起始数量（基本单位数量，包含此数量）
    /// </summary>
    [SugarColumn(ColumnName = "start_quantity", ColumnDescription = "起始数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal StartQuantity { get; set; } = 0;

    /// <summary>
    /// 结束数量（基本单位数量，包含此数量，0表示无上限）
    /// </summary>
    [SugarColumn(ColumnName = "end_quantity", ColumnDescription = "结束数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal EndQuantity { get; set; } = 0;

    /// <summary>
    /// 阶梯价格（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "scale_price", ColumnDescription = "阶梯价格", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ScalePrice { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
