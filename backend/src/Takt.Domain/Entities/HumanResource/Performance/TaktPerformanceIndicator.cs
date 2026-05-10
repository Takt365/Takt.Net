// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Performance
// 文件名称：TaktPerformanceIndicator.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效指标实体，定义KPI指标库、指标权重、评分标准等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Performance;

/// <summary>
/// 绩效指标实体
/// </summary>
[SugarTable("takt_human_resource_performance_indicator", "绩效指标表")]
[SugarIndex("ix_takt_human_resource_performance_indicator_indicator_code", nameof(IndicatorCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_performance_indicator_category", nameof(Category), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_indicator_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_indicator_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPerformanceIndicator : TaktEntityBase
{
    /// <summary>
    /// 指标编码
    /// </summary>
    [SugarColumn(ColumnName = "indicator_code", ColumnDescription = "指标编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string IndicatorCode { get; set; } = string.Empty;

    /// <summary>
    /// 指标名称
    /// </summary>
    [SugarColumn(ColumnName = "indicator_name", ColumnDescription = "指标名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string IndicatorName { get; set; } = string.Empty;

    /// <summary>
    /// 指标类别(业绩/能力/态度/管理/创新/质量/效率/安全)
    /// </summary>
    [SugarColumn(ColumnName = "category", ColumnDescription = "指标类别", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// 指标类型(定量/定性)
    /// </summary>
    [SugarColumn(ColumnName = "indicator_type", ColumnDescription = "指标类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string IndicatorType { get; set; } = string.Empty;

    /// <summary>
    /// 指标说明
    /// </summary>
    [SugarColumn(ColumnName = "indicator_description", ColumnDescription = "指标说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string IndicatorDescription { get; set; } = string.Empty;

    /// <summary>
    /// 评分标准说明
    /// </summary>
    [SugarColumn(ColumnName = "scoring_criteria", ColumnDescription = "评分标准说明", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ScoringCriteria { get; set; } = string.Empty;

    /// <summary>
    /// 目标值
    /// </summary>
    [SugarColumn(ColumnName = "target_value", ColumnDescription = "目标值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TargetValue { get; set; } = 0;

    /// <summary>
    /// 最低要求值
    /// </summary>
    [SugarColumn(ColumnName = "minimum_value", ColumnDescription = "最低要求值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal MinimumValue { get; set; } = 0;

    /// <summary>
    /// 卓越值
    /// </summary>
    [SugarColumn(ColumnName = "excellent_value", ColumnDescription = "卓越值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ExcellentValue { get; set; } = 0;

    /// <summary>
    /// 标准权重(%)
    /// </summary>
    [SugarColumn(ColumnName = "standard_weight", ColumnDescription = "标准权重", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal StandardWeight { get; set; } = 0;

    /// <summary>
    /// 数据来源
    /// </summary>
    [SugarColumn(ColumnName = "data_source", ColumnDescription = "数据来源", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DataSource { get; set; } = string.Empty;

    /// <summary>
    /// 考核周期(月度/季度/半年度/年度)
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_cycle", ColumnDescription = "考核周期", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EvaluationCycle { get; set; } = string.Empty;

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 状态(0=启用 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
