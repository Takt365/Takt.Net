// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality
// 文件名称：TaktInspectionStandardItem.cs
// 功能描述：检验标准明细实体
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Operation;

/// <summary>
/// 检验标准明细实体
/// </summary>
[SugarTable("takt_logistics_quality_inspection_standard_item", "检验标准明细表")]
[SugarIndex("ix_takt_logistics_quality_inspection_standard_item_unique", nameof(InspectionStandardId), OrderByType.Asc, nameof(LineNumber), OrderByType.Asc, nameof(ItemCode), OrderByType.Asc, nameof(ItemName), OrderByType.Asc, nameof(ItemType), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_inspection_standard_item_item_type", nameof(ItemType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_inspection_standard_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktInspectionStandardItem : TaktEntityBase
{
    /// <summary>
    /// 检验标准ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_standard_id", ColumnDescription = "检验标准ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InspectionStandardId { get; set; }

    /// <summary>
    /// 行号（检验标准明细行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 检验项目编码
    /// </summary>
    [SugarColumn(ColumnName = "item_code", ColumnDescription = "检验项目编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 检验项目名称
    /// </summary>
    [SugarColumn(ColumnName = "item_name", ColumnDescription = "检验项目名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 检验项目类型（0=外观，1=尺寸，2=性能，3=材质，4=功能，5=颜色，6=结构）
    /// </summary>
    [SugarColumn(ColumnName = "item_type", ColumnDescription = "检验项目类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ItemType { get; set; } = 0;

    /// <summary>
    /// 缺点等级（CR=严重，MA=主要，MI=次要）
    /// </summary>
    [SugarColumn(ColumnName = "defect_level", ColumnDescription = "缺点等级", ColumnDataType = "nvarchar", Length = 2, IsNullable = false)]
    public string DefectLevel { get; set; } = string.Empty;

    /// <summary>
    /// 检验方式（1=计数，2=计量）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_mode", ColumnDescription = "检验方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int InspectionMode { get; set; } = 1;

    /// <summary>
    /// 检验标准值
    /// </summary>
    [SugarColumn(ColumnName = "standard_value", ColumnDescription = "检验标准值", ColumnDataType = "nvarchar", Length = 500, IsNullable = false)]
    public string StandardValue { get; set; } = string.Empty;

    /// <summary>
    /// 检验上限值
    /// </summary>
    [SugarColumn(ColumnName = "upper_limit", ColumnDescription = "检验上限值", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string UpperLimit { get; set; } = string.Empty;

    /// <summary>
    /// 检验下限值
    /// </summary>
    [SugarColumn(ColumnName = "lower_limit", ColumnDescription = "检验下限值", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string LowerLimit { get; set; } = string.Empty;

    /// <summary>
    /// 检验工具
    /// </summary>
    [SugarColumn(ColumnName = "inspection_tool", ColumnDescription = "检验工具", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string InspectionTool { get; set; } = string.Empty;

    /// <summary>
    /// 检验方法说明
    /// </summary>
    [SugarColumn(ColumnName = "inspection_method_description", ColumnDescription = "检验方法说明", ColumnDataType = "nvarchar", Length = 500, IsNullable = false)]
    public string InspectionMethodDescription { get; set; } = string.Empty;

    /// <summary>
    /// 接收标准（AC值）
    /// </summary>
    [SugarColumn(ColumnName = "acceptance_criteria", ColumnDescription = "接收标准(AC值)", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string AcceptanceCriteria { get; set; } = string.Empty;

    /// <summary>
    /// 拒收标准（RE值）
    /// </summary>
    [SugarColumn(ColumnName = "rejection_criteria", ColumnDescription = "拒收标准(RE值)", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RejectionCriteria { get; set; } = string.Empty;

    /// <summary>
    /// 是否合格判定项目（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_qualified_basis", ColumnDescription = "是否合格判定项目", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsQualifiedBasis { get; set; } = 1;

    /// <summary>
    /// 检验标准（主表）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(InspectionStandardId))]
    public TaktInspectionStandard? Standard { get; set; }
}
