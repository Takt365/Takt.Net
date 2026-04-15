// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing
// 文件名称：TaktMaterialPackaging.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt物料包装信息实体，定义物料海关、尺寸、重量及包装信息
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Bom;

/// <summary>
/// Takt物料包装信息实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_packaging", "物料包装信息表")]
[SugarIndex("ix_takt_logistics_manufacturing_packaging_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_packaging_material_code", nameof(MaterialCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_packaging_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_packaging_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPackaging : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 物料编码（关联到物料表）
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 海关商品编码（HS Code）
    /// </summary>
    [SugarColumn(ColumnName = "hs_code", ColumnDescription = "海关商品编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? HsCode { get; set; }

    /// <summary>
    /// 商品名称（HS Name）
    /// </summary>
    [SugarColumn(ColumnName = "hs_name", ColumnDescription = "商品名称", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? HsName { get; set; }

    /// <summary>
    /// 附加编码（如 CIQ 检验检疫附加码，3位，用于满足特定监管要求）
    /// </summary>
    [SugarColumn(ColumnName = "additional_code", ColumnDescription = "附加编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? AdditionalCode { get; set; }

    /// <summary>
    /// 原产国/地区编码（用于关税和贸易统计）
    /// </summary>
    [SugarColumn(ColumnName = "origin_country_region_code", ColumnDescription = "原产国/地区编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? OriginCountryRegionCode { get; set; }

    /// <summary>
    /// 原产国/地区名称
    /// </summary>
    [SugarColumn(ColumnName = "origin_country_region_name", ColumnDescription = "原产国/地区名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OriginCountryRegionName { get; set; }

    /// <summary>
    /// 目的国/地区编码（用于出口报关和贸易分析）
    /// </summary>
    [SugarColumn(ColumnName = "destination_country_region_code", ColumnDescription = "目的国/地区编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? DestinationCountryRegionCode { get; set; }

    /// <summary>
    /// 目的国/地区名称
    /// </summary>
    [SugarColumn(ColumnName = "destination_country_region_name", ColumnDescription = "目的国/地区名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DestinationCountryRegionName { get; set; }

    /// <summary>
    /// 监管条件代码（如是否需要商检、许可证等，用于触发特定业务流程）
    /// </summary>
    [SugarColumn(ColumnName = "regulatory_condition_code", ColumnDescription = "监管条件代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RegulatoryConditionCode { get; set; }

    /// <summary>
    /// 税率/协定税率标识（记录适用的关税税率类型，便于成本核算）
    /// </summary>
    [SugarColumn(ColumnName = "tariff_rate_type", ColumnDescription = "税率/协定税率标识", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? TariffRateType { get; set; }

    /// <summary>
    /// 毛重（包含包装物的总重量，单位：千克）
    /// </summary>
    [SugarColumn(ColumnName = "gross_weight", ColumnDescription = "毛重", ColumnDataType = "decimal", Length = 18, DecimalDigits = 10, IsNullable = true)]
    public decimal? GrossWeight { get; set; }

    /// <summary>
    /// 净重（不含包装物的净重量，单位：千克）
    /// </summary>
    [SugarColumn(ColumnName = "net_weight", ColumnDescription = "净重", ColumnDataType = "decimal", Length = 18, DecimalDigits = 10, IsNullable = true)]
    public decimal? NetWeight { get; set; }

    /// <summary>
    /// 重量单位（如：KG、G、T等）
    /// </summary>
    [SugarColumn(ColumnName = "weight_unit", ColumnDescription = "重量单位", ColumnDataType = "nvarchar", Length = 10, IsNullable = false, DefaultValue = "KG")]
    public string WeightUnit { get; set; } = "KG";

    /// <summary>
    /// 业务量/容积（一个包装单位的体积，单位：立方米）
    /// </summary>
    [SugarColumn(ColumnName = "business_volume", ColumnDescription = "业务量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 6, IsNullable = true)]
    public decimal? BusinessVolume { get; set; }

    /// <summary>
    /// 体积单位（如：M3、L、ML等）
    /// </summary>
    [SugarColumn(ColumnName = "volume_unit", ColumnDescription = "体积单位", ColumnDataType = "nvarchar", Length = 10, IsNullable = false, DefaultValue = "M3")]
    public string VolumeUnit { get; set; } = "M3";

    /// <summary>
    /// 大小/量纲（尺寸量纲或大小规格）
    /// </summary>
    [SugarColumn(ColumnName = "size_dimension", ColumnDescription = "大小/量纲", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? SizeDimension { get; set; }

    /// <summary>
    /// 包装类型（如：箱、托盘、袋、桶等，VERP=销售包装）
    /// </summary>
    [SugarColumn(ColumnName = "packaging_type", ColumnDescription = "包装类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "VERP")]
    public string PackagingType { get; set; } = "VERP";

    /// <summary>
    /// 包装单位（CAR=卡通箱；其他如：个、件等）
    /// </summary>
    [SugarColumn(ColumnName = "packing_unit", ColumnDescription = "包装单位", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "CAR")]
    public string PackingUnit { get; set; } = "CAR";

    /// <summary>
    /// 每包装数量（一个包装包含的基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "quantity_per_packing", ColumnDescription = "每包装数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = true)]
    public decimal? QuantityPerPacking { get; set; }

    /// <summary>
    /// 包装规格
    /// </summary>
    [SugarColumn(ColumnName = "packaging_spec", ColumnDescription = "包装规格", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? PackagingSpec { get; set; }

    /// <summary>
    /// 包装描述
    /// </summary>
    [SugarColumn(ColumnName = "packaging_description", ColumnDescription = "包装描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? PackagingDescription { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
