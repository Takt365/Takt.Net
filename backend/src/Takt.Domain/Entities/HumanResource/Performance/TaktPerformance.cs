// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Performance
// 文件名称：TaktPerformance.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效考核实体，记录员工绩效评估、考核指标、评分等信息
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Performance;

/// <summary>
/// 绩效考核实体
/// </summary>
[SugarTable("takt_human_resource_performance", "绩效考核表")]
[SugarIndex("ix_takt_human_resource_performance_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_evaluation_date", nameof(EvaluationDate), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_performance_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPerformance : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 考核周期(如：2025-Q1, 2025-H1, 2025-Annual)
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_period", ColumnDescription = "考核周期", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EvaluationPeriod { get; set; } = string.Empty;

    /// <summary>
    /// 考核日期
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_date", ColumnDescription = "考核日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EvaluationDate { get; set; }

    /// <summary>
    /// 考核指标
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_criteria", ColumnDescription = "考核指标", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EvaluationCriteria { get; set; } = string.Empty;

    /// <summary>
    /// 考核得分
    /// </summary>
    [SugarColumn(ColumnName = "score", ColumnDescription = "考核得分", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal Score { get; set; } = 0;

    /// <summary>
    /// 绩效等级(A/B/C/D/E)
    /// </summary>
    [SugarColumn(ColumnName = "grade", ColumnDescription = "绩效等级", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Grade { get; set; } = string.Empty;

    /// <summary>
    /// 自评得分
    /// </summary>
    [SugarColumn(ColumnName = "self_score", ColumnDescription = "自评得分", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SelfScore { get; set; } = 0;

    /// <summary>
    /// 主管评分
    /// </summary>
    [SugarColumn(ColumnName = "supervisor_score", ColumnDescription = "主管评分", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SupervisorScore { get; set; } = 0;

    /// <summary>
    /// 考核评语
    /// </summary>
    [SugarColumn(ColumnName = "comments", ColumnDescription = "考核评语", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Comments { get; set; } = string.Empty;

    /// <summary>
    /// 改进建议
    /// </summary>
    [SugarColumn(ColumnName = "improvement_suggestions", ColumnDescription = "改进建议", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ImprovementSuggestions { get; set; } = string.Empty;

    /// <summary>
    /// 考核人ID
    /// </summary>
    [SugarColumn(ColumnName = "evaluator_id", ColumnDescription = "考核人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EvaluatorId { get; set; }

    /// <summary>
    /// 状态(0=待评估 1=评估中 2=已完成 3=已确认)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
