// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Material
// 文件名称：TaktMaterial.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工厂物料实体，定义工厂物料领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Materials;

/// <summary>
/// Takt工厂物料实体
/// </summary>
[SugarTable("takt_logistics_materials_plant_material", "工厂物料表")]
[SugarIndex("ix_takt_logistics_materials_plant_material_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_plant_material_material_code", nameof(MaterialCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_materials_plant_material_material_type", nameof(MaterialType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_plant_material_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_plant_material_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_plant_material_material_status", nameof(MaterialStatus), OrderByType.Asc)]
public class TaktPlantMaterial : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料名称
    /// </summary>
    [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", ColumnDataType = "nvarchar", Length = 40, IsNullable = false)]
    public string MaterialName { get; set; } = string.Empty;

    /// <summary>
    /// 行业领域
    /// </summary>
    [SugarColumn(ColumnName = "industry_sector", ColumnDescription = "行业领域", ColumnDataType = "nvarchar", Length = 1, IsNullable = true)]
    public string? IndustrySector { get; set; }

    /// <summary>
    /// 品目阶层
    /// </summary>
    [SugarColumn(ColumnName = "material_hierarchy", ColumnDescription = "品目阶层", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? MaterialHierarchy { get; set; }

    /// <summary>
    /// 品目组代码
    /// </summary>
    [SugarColumn(ColumnName = "material_group_code", ColumnDescription = "品目组代码", ColumnDataType = "nvarchar", Length = 3, IsNullable = true)]
    public string? MaterialGroupCode { get; set; }

    /// <summary>
    /// 物料类型（0=原材料，1=半成品，2=成品，3=辅料，4=包装材料，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "material_type", ColumnDescription = "物料类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MaterialType { get; set; } = 0;

    /// <summary>
    /// 物料规格
    /// </summary>
    [SugarColumn(ColumnName = "material_specification", ColumnDescription = "物料规格", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? MaterialSpecification { get; set; }

    /// <summary>
    /// 物料型号
    /// </summary>
    [SugarColumn(ColumnName = "material_model", ColumnDescription = "物料型号", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? MaterialModel { get; set; }

    /// <summary>
    /// 物料品牌。从字典 material_brand 选择，DictValue 存入本字段；不允许自由文本。
    /// </summary>
    [SugarColumn(ColumnName = "material_brand", ColumnDescription = "物料品牌", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? MaterialBrand { get; set; }

    /// <summary>
    /// 基本单位（主单位）。从字典 sys_base_unit（国际标准单位）选择，DictValue 存入本字段（如 PCE、KGM、MTR）；不允许自由文本。
    /// </summary>
    [SugarColumn(ColumnName = "base_unit", ColumnDescription = "基本单位", ColumnDataType = "nvarchar", Length = 4, IsNullable = false, DefaultValue = "PCE")]
    public string BaseUnit { get; set; } = "PCE";

    /// <summary>
    /// 采购组
    /// </summary>
    [SugarColumn(ColumnName = "purchase_group", ColumnDescription = "采购组", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PurchaseGroup { get; set; }

    /// <summary>
    /// 采购类型。从字典 material_purchase_type 选择，DictValue（0/1/2/3）对应 int 存本字段。
    /// </summary>
    [SugarColumn(ColumnName = "purchase_type", ColumnDescription = "采购类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int PurchaseType { get; set; } = 1;

    /// <summary>
    /// 特殊采购。从字典 material_special_procurement 选择，DictValue（0/1/2/3）对应 int 存本字段。
    /// </summary>
    [SugarColumn(ColumnName = "special_procurement", ColumnDescription = "特殊采购", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SpecialProcurement { get; set; } = 0;

    /// <summary>
    /// 是否散装（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_bulk", ColumnDescription = "是否散装", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsBulk { get; set; } = 0;

    /// <summary>
    /// 最小起订量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "min_order_quantity", ColumnDescription = "最小起订量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal MinOrderQuantity { get; set; } = 0;

    /// <summary>
    /// 舍入值（基本单位数量，用于数量舍入，允许小数）
    /// </summary>
    [SugarColumn(ColumnName = "rounding_value", ColumnDescription = "舍入值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal RoundingValue { get; set; } = 0;

    /// <summary>
    /// 计划交货时间（天数）
    /// </summary>
    [SugarColumn(ColumnName = "planned_delivery_time_days", ColumnDescription = "计划交货时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PlannedDeliveryTimeDays { get; set; } = 0;

    /// <summary>
    /// 自制生产天数（内部生产所需天数）
    /// </summary>
    [SugarColumn(ColumnName = "in_house_production_days", ColumnDescription = "自制生产天数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InHouseProductionDays { get; set; } = 0;

    /// <summary>
    /// 供应商名称
    /// </summary>
    [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", ColumnDataType = "nvarchar", Length = 40, IsNullable = true)]
    public string? SupplierName { get; set; }

    /// <summary>
    /// 制造商
    /// </summary>
    [SugarColumn(ColumnName = "manufacturer", ColumnDescription = "制造商", ColumnDataType = "nvarchar", Length = 40, IsNullable = true)]
    public string? Manufacturer { get; set; }

    /// <summary>
    /// 制造商零件编号
    /// </summary>
    [SugarColumn(ColumnName = "manufacturer_part_number", ColumnDescription = "制造商零件编号", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ManufacturerPartNumber { get; set; }

    /// <summary>
    /// 原产地（国家代码）。ISO 3166-1 alpha-2，如 CN、US；不由字典维护。
    /// </summary>
    [SugarColumn(ColumnName = "country_of_origin", ColumnDescription = "原产地", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? CountryOfOrigin { get; set; }

    /// <summary>
    /// 币种代码。ISO 4217，如 CNY、USD；不由字典维护。
    /// </summary>
    [SugarColumn(ColumnName = "currency_code", ColumnDescription = "币种代码", ColumnDataType = "nvarchar", Length = 3, IsNullable = true, DefaultValue = "CNY")]
    public string CurrencyCode { get; set; } = "CNY";

    /// <summary>
    /// 价格控制。从字典 material_price_control 选择，DictValue（0/1/2）对应 int 存本字段。
    /// </summary>
    [SugarColumn(ColumnName = "price_control", ColumnDescription = "价格控制", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PriceControl { get; set; } = 0;

    /// <summary>
    /// 价格单位（价格对应的单位数量，如：1表示每1个，1000表示每1000个）
    /// </summary>
    [SugarColumn(ColumnName = "price_unit", ColumnDescription = "价格单位", ColumnDataType = "int", IsNullable = false, DefaultValue = "1000")]
    public int PriceUnit { get; set; } = 1000;

    /// <summary>
    /// 评估类别代码。从字典 material_valuation_category 选择，DictValue 存本字段。
    /// </summary>
    [SugarColumn(ColumnName = "valuation_category", ColumnDescription = "评估类别代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? ValuationCategory { get; set; }

    /// <summary>
    /// 差异码
    /// </summary>
    [SugarColumn(ColumnName = "difference_code", ColumnDescription = "差异码", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? DifferenceCode { get; set; }

    /// <summary>
    /// 利润中心
    /// </summary>
    [SugarColumn(ColumnName = "profit_center", ColumnDescription = "利润中心", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? ProfitCenter { get; set; }

    /// <summary>
    /// 当前库存（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "current_stock", ColumnDescription = "当前库存", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal CurrentStock { get; set; } = 0;

    /// <summary>
    /// 生产库存地点（仓库名称）。从字典 material_stock_location 选择，DictValue 存本字段；可后台按实际仓库名称扩展。
    /// </summary>
    [SugarColumn(ColumnName = "production_stock_location", ColumnDescription = "生产库存地点", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ProductionStockLocation { get; set; }

    /// <summary>
    /// 采购库存地点（仓库名称）。从字典 material_stock_location 选择，DictValue 存本字段；可后台按实际仓库名称扩展。
    /// </summary>
    [SugarColumn(ColumnName = "purchasing_stock_location", ColumnDescription = "采购库存地点", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PurchasingStockLocation { get; set; }

    /// <summary>
    /// 是否检验（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_required", ColumnDescription = "是否检验", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InspectionRequired { get; set; } = 0;

    /// <summary>
    /// 是否批次管理（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_batch", ColumnDescription = "是否批次管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsBatch { get; set; } = 0;

    /// <summary>
    /// 是否保质期管理（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_expiry", ColumnDescription = "是否保质期管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsExpiry { get; set; } = 0;

    /// <summary>
    /// 保质期天数（如果启用保质期管理）
    /// </summary>
    [SugarColumn(ColumnName = "expiry_days", ColumnDescription = "保质期天数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ExpiryDays { get; set; } = 0;

    /// <summary>
    /// 物料状态（0=正常，1=停用，2=待审核，3=已审核）
    /// </summary>
    [SugarColumn(ColumnName = "material_status", ColumnDescription = "物料状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MaterialStatus { get; set; } = 0;

    /// <summary>
    /// 物料属性。从字典 material_attributes 选择，DictValue（0/1/2/3）对应 int 存本字段。
    /// </summary>
    [SugarColumn(ColumnName = "material_attributes", ColumnDescription = "物料属性", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MaterialAttributes { get; set; } = 0;

    /// <summary>
    /// 物料描述
    /// </summary>
    [SugarColumn(ColumnName = "material_description", ColumnDescription = "物料描述", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? MaterialDescription { get; set; }

    /// <summary>
    /// 停产状态（EOL）。从字典 material_is_end_of_life 选择，DictValue（01/02/Z0/ZM/ZP/ZQ/ZW/ZX/ZY/ZZ）存本字段。
    /// </summary>
    [SugarColumn(ColumnName = "is_end_of_life", ColumnDescription = "停产状态", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? IsEndOfLife { get; set; }

    /// <summary>
    /// 停产日期
    /// </summary>
    [SugarColumn(ColumnName = "end_of_life_date", ColumnDescription = "停产日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EndOfLifeDate { get; set; }
}
