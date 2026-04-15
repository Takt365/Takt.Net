// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing
// 文件名称：TaktBillOfMaterialItem.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt物料清单明细实体，定义物料清单明细领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Bom;

/// <summary>
/// Takt物料清单明细实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_bom_item", "物料清单明细表")]
[SugarIndex("ix_takt_logistics_manufacturing_bom_item_bom_id", nameof(BomId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_item_child_material_id", nameof(ChildMaterialId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_item_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktBillOfMaterialItem : TaktEntityBase
{
    /// <summary>
    /// BOM ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "bom_id", ColumnDescription = "BOM ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long BomId { get; set; }

    /// <summary>
    /// BOM编码
    /// </summary>
    [SugarColumn(ColumnName = "bom_code", ColumnDescription = "BOM编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string BomCode { get; set; } = string.Empty;

    /// <summary>
    /// 子物料ID（组成物料，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "child_material_id", ColumnDescription = "子物料ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ChildMaterialId { get; set; }

    /// <summary>
    /// 子物料编码
    /// </summary>
    [SugarColumn(ColumnName = "child_material_code", ColumnDescription = "子物料编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ChildMaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 子物料名称
    /// </summary>
    [SugarColumn(ColumnName = "child_material_name", ColumnDescription = "子物料名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ChildMaterialName { get; set; } = string.Empty;

    /// <summary>
    /// 子物料规格
    /// </summary>
    [SugarColumn(ColumnName = "child_material_specification", ColumnDescription = "子物料规格", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ChildMaterialSpecification { get; set; }

    /// <summary>
    /// 用量（每单位父物料需要的子物料数量）
    /// </summary>
    [SugarColumn(ColumnName = "usage_quantity", ColumnDescription = "用量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal UsageQuantity { get; set; } = 0;

    /// <summary>
    /// 子物料单位
    /// </summary>
    [SugarColumn(ColumnName = "child_material_unit", ColumnDescription = "子物料单位", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "个")]
    public string ChildMaterialUnit { get; set; } = "个";

    /// <summary>
    /// 损耗率（0-100，表示损耗百分比）
    /// </summary>
    [SugarColumn(ColumnName = "scrap_rate", ColumnDescription = "损耗率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ScrapRate { get; set; } = 0;

    /// <summary>
    /// 实际用量（用量 + 损耗，计算公式：实际用量 = 用量 * (1 + 损耗率/100)）
    /// </summary>
    [SugarColumn(ColumnName = "actual_usage_quantity", ColumnDescription = "实际用量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal ActualUsageQuantity { get; set; } = 0;

    /// <summary>
    /// 是否替代料（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_substitute", ColumnDescription = "是否替代料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsSubstitute { get; set; } = 0;

    /// <summary>
    /// 替代优先级（数字越小优先级越高，如果为替代料）
    /// </summary>
    [SugarColumn(ColumnName = "substitute_priority", ColumnDescription = "替代优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SubstitutePriority { get; set; } = 0;

    /// <summary>
    /// 是否必选（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_required", ColumnDescription = "是否必选", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsRequired { get; set; } = 1;

    /// <summary>
    /// 是否虚拟件（0=否，1=是，虚拟件不实际存在，仅用于BOM结构）
    /// </summary>
    [SugarColumn(ColumnName = "is_phantom", ColumnDescription = "是否虚拟件", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsPhantom { get; set; } = 0;

    /// <summary>
    /// 是否关键件（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_critical", ColumnDescription = "是否关键件", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsCritical { get; set; } = 0;

    /// <summary>
    /// 行号（BOM明细行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;
}
