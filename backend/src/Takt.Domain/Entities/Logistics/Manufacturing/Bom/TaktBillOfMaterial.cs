// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing
// 文件名称：TaktBillOfMaterial.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt物料清单实体，定义物料清单（BOM）领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Bom;

/// <summary>
/// Takt物料清单实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_bom", "物料清单表")]
[SugarIndex("ix_takt_logistics_manufacturing_bom_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_parent_material_id", nameof(ParentMaterialId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_bom_code", nameof(BomCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_bom_version", nameof(BomVersion), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_effective_date", nameof(EffectiveDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_bom_status", nameof(BomStatus), OrderByType.Asc)]
public class TaktBillOfMaterial : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// BOM编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "bom_code", ColumnDescription = "BOM编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string BomCode { get; set; } = string.Empty;

    /// <summary>
    /// BOM名称
    /// </summary>
    [SugarColumn(ColumnName = "bom_name", ColumnDescription = "BOM名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string BomName { get; set; } = string.Empty;

    /// <summary>
    /// 父物料ID（成品/半成品，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_material_id", ColumnDescription = "父物料ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentMaterialId { get; set; }

    /// <summary>
    /// 父物料编码
    /// </summary>
    [SugarColumn(ColumnName = "parent_material_code", ColumnDescription = "父物料编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ParentMaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 父物料名称
    /// </summary>
    [SugarColumn(ColumnName = "parent_material_name", ColumnDescription = "父物料名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ParentMaterialName { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本号
    /// </summary>
    [SugarColumn(ColumnName = "bom_version", ColumnDescription = "BOM版本号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "1.0")]
    public string BomVersion { get; set; } = "1.0";

    /// <summary>
    /// BOM类型（0=标准BOM，1=工程BOM，2=制造BOM，3=成本BOM，4=销售BOM）
    /// </summary>
    [SugarColumn(ColumnName = "bom_type", ColumnDescription = "BOM类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int BomType { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EffectiveDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 失效日期（如果为空则表示永久有效）
    /// </summary>
    [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 父物料单位
    /// </summary>
    [SugarColumn(ColumnName = "parent_material_unit", ColumnDescription = "父物料单位", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "个")]
    public string ParentMaterialUnit { get; set; } = "个";

    /// <summary>
    /// 父物料数量（用于计算子物料用量）
    /// </summary>
    [SugarColumn(ColumnName = "parent_material_quantity", ColumnDescription = "父物料数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "1")]
    public decimal ParentMaterialQuantity { get; set; } = 1;

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_enabled", ColumnDescription = "是否启用", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsEnabled { get; set; } = 1;

    /// <summary>
    /// BOM状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    [SugarColumn(ColumnName = "bom_status", ColumnDescription = "BOM状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int BomStatus { get; set; } = 0;

    /// <summary>
    /// BOM描述
    /// </summary>
    [SugarColumn(ColumnName = "bom_description", ColumnDescription = "BOM描述", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? BomDescription { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
