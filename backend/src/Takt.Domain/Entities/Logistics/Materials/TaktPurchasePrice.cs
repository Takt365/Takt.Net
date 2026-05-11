// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Materials
// 文件名称：TaktPurchasePrice.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购价格实体，定义采购价格领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Materials;

/// <summary>
/// Takt采购价格实体（供应商价格主表，一个供应商可以有多个物料价格）
/// </summary>
[SugarTable("takt_logistics_materials_purchase_price", "采购价格表")]
[SugarIndex("ix_takt_logistics_materials_purchase_price_price_unique", nameof(PlantCode), OrderByType.Asc, nameof(PurchasePriceCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_supplier_code", nameof(SupplierCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_price_type", nameof(PriceType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_effective_start_date", nameof(EffectiveStartDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_effective_end_date", nameof(EffectiveEndDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_price_status", nameof(PriceStatus), OrderByType.Asc)]
public class TaktPurchasePrice : TaktEntityBase
{
    /// <summary>
    /// 工厂代码（不可空）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 采购价格编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "purchase_price_code", ColumnDescription = "采购价格编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string PurchasePriceCode { get; set; } = string.Empty;

    /// <summary>
    /// 供应商编码
    /// </summary>
    [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string SupplierCode { get; set; } = string.Empty;

    /// <summary>
    /// 价格类型（0=标准价格，1=合同价格，2=临时价格，3=询价价格，4=历史价格）
    /// </summary>
    [SugarColumn(ColumnName = "price_type", ColumnDescription = "价格类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PriceType { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_start_date", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EffectiveStartDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 失效日期（空表示长期有效）
    /// </summary>
    [SugarColumn(ColumnName = "effective_end_date", ColumnDescription = "失效日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EffectiveEndDate { get; set; }

    /// <summary>
    /// 价格状态（1=启用，0=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "price_status", ColumnDescription = "价格状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int PriceStatus { get; set; } = 1;

    /// <summary>
    /// 物料价格明细列表（主子表关系，一个供应商价格可以有多个物料价格）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktPurchasePriceItem.PurchasePriceId))]
    public List<TaktPurchasePriceItem>? Items { get; set; }

    /// <summary>
    /// 采购价格变更记录列表（外键在子表 <see cref="TaktPurchasePriceChangeLog.PriceId"/>）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktPurchasePriceChangeLog.PurchasePriceId))]
    public List<TaktPurchasePriceChangeLog>? ChangeLogs { get; set; }
}