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
[SugarIndex("ix_takt_logistics_materials_purchase_price_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_supplier_code", nameof(SupplierCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_price_type", nameof(PriceType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_effective_from", nameof(EffectiveFrom), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_effective_to", nameof(EffectiveTo), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_price_price_status", nameof(PriceStatus), OrderByType.Asc)]
public class TaktPurchasePrice : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

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
    /// Effective from（生效起始）
    /// </summary>
    [SugarColumn(ColumnName = "effective_from", ColumnDescription = "Effective from", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EffectiveFrom { get; set; } = DateTime.Now;

    /// <summary>
    /// Effective to（生效截止，空表示长期有效）
    /// </summary>
    [SugarColumn(ColumnName = "effective_to", ColumnDescription = "Effective to", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EffectiveTo { get; set; }

    /// <summary>
    /// 价格状态（0=草稿，1=已生效，2=已失效，3=已停用）
    /// </summary>
    [SugarColumn(ColumnName = "price_status", ColumnDescription = "价格状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PriceStatus { get; set; } = 0;

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_enabled", ColumnDescription = "是否启用", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsEnabled { get; set; } = 1;

    /// <summary>
    /// 物料价格明细列表（主子表关系，一个供应商价格可以有多个物料价格）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktPurchasePriceItem.PriceId))]
    public List<TaktPurchasePriceItem>? Items { get; set; }
}