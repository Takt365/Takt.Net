// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Performance
// 文件名称：TaktPerformanceGoal.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效目标实体，记录员工绩效目标设定、目标完成情况等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Performance;

/// <summary>
/// 绩效目标实体
/// </summary>
[SugarTable("takt_human_resource_performance_goal", "绩效目标表")]
[SugarIndex("ix_takt_human_resource_performance_goal_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_goal_goal_period", nameof(GoalPeriod), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_goal_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_goal_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPerformanceGoal : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 绩效指标ID
    /// </summary>
    [SugarColumn(ColumnName = "performance_indicator_id", ColumnDescription = "绩效指标ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PerformanceIndicatorId { get; set; }

    /// <summary>
    /// 目标周期(如：2025-Q1, 2025-H1, 2025-Annual)
    /// </summary>
    [SugarColumn(ColumnName = "goal_period", ColumnDescription = "目标周期", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string GoalPeriod { get; set; } = string.Empty;

    /// <summary>
    /// 目标描述
    /// </summary>
    [SugarColumn(ColumnName = "goal_description", ColumnDescription = "目标描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string GoalDescription { get; set; } = string.Empty;

    /// <summary>
    /// 目标值
    /// </summary>
    [SugarColumn(ColumnName = "target_value", ColumnDescription = "目标值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TargetValue { get; set; } = 0;

    /// <summary>
    /// 实际完成值
    /// </summary>
    [SugarColumn(ColumnName = "actual_value", ColumnDescription = "实际完成值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ActualValue { get; set; } = 0;

    /// <summary>
    /// 完成百分比(%)
    /// </summary>
    [SugarColumn(ColumnName = "completion_percentage", ColumnDescription = "完成百分比", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal CompletionPercentage { get; set; } = 0;

    /// <summary>
    /// 目标权重(%)
    /// </summary>
    [SugarColumn(ColumnName = "goal_weight", ColumnDescription = "目标权重", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal GoalWeight { get; set; } = 0;

    /// <summary>
    /// 开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 截止日期
    /// </summary>
    [SugarColumn(ColumnName = "due_date", ColumnDescription = "截止日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime DueDate { get; set; }

    /// <summary>
    /// 完成日期
    /// </summary>
    [SugarColumn(ColumnName = "completion_date", ColumnDescription = "完成日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime CompletionDate { get; set; }

    /// <summary>
    /// 目标达成说明
    /// </summary>
    [SugarColumn(ColumnName = "achievement_notes", ColumnDescription = "目标达成说明", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string AchievementNotes { get; set; } = string.Empty;

    /// <summary>
    /// 未达成原因
    /// </summary>
    [SugarColumn(ColumnName = "failure_reason", ColumnDescription = "未达成原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string FailureReason { get; set; } = string.Empty;

    /// <summary>
    /// 审批人ID
    /// </summary>
    [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ApproverId { get; set; }

    /// <summary>
    /// 状态(0=待确认 1=进行中 2=已完成 3=已审批 4=已驳回)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
