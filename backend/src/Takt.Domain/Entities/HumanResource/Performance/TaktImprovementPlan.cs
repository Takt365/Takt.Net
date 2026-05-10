// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Performance
// 文件名称：TaktImprovementPlan.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：改进计划实体，记录绩效改进计划、改进措施、跟踪进度等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Performance;

/// <summary>
/// 改进计划实体
/// </summary>
[SugarTable("takt_human_resource_improvement_plan", "改进计划表")]
[SugarIndex("ix_takt_human_resource_improvement_plan_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_improvement_plan_plan_date", nameof(PlanDate), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_improvement_plan_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_improvement_plan_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktImprovementPlan : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 绩效评审ID
    /// </summary>
    [SugarColumn(ColumnName = "performance_review_id", ColumnDescription = "绩效评审ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PerformanceReviewId { get; set; }

    /// <summary>
    /// 改进计划标题
    /// </summary>
    [SugarColumn(ColumnName = "plan_title", ColumnDescription = "改进计划标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlanTitle { get; set; } = string.Empty;

    /// <summary>
    /// 改进领域(技能提升/工作效率/沟通能力/团队协作/管理能力/其他)
    /// </summary>
    [SugarColumn(ColumnName = "improvement_area", ColumnDescription = "改进领域", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ImprovementArea { get; set; } = string.Empty;

    /// <summary>
    /// 当前状况描述
    /// </summary>
    [SugarColumn(ColumnName = "current_situation", ColumnDescription = "当前状况描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CurrentSituation { get; set; } = string.Empty;

    /// <summary>
    /// 改进目标
    /// </summary>
    [SugarColumn(ColumnName = "improvement_goal", ColumnDescription = "改进目标", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ImprovementGoal { get; set; } = string.Empty;

    /// <summary>
    /// 改进措施
    /// </summary>
    [SugarColumn(ColumnName = "improvement_actions", ColumnDescription = "改进措施", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ImprovementActions { get; set; } = string.Empty;

    /// <summary>
    /// 所需资源支持
    /// </summary>
    [SugarColumn(ColumnName = "required_resources", ColumnDescription = "所需资源支持", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string RequiredResources { get; set; } = string.Empty;

    /// <summary>
    /// 计划制定日期
    /// </summary>
    [SugarColumn(ColumnName = "plan_date", ColumnDescription = "计划制定日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime PlanDate { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 目标完成日期
    /// </summary>
    [SugarColumn(ColumnName = "target_completion_date", ColumnDescription = "目标完成日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime TargetCompletionDate { get; set; }

    /// <summary>
    /// 实际完成日期
    /// </summary>
    [SugarColumn(ColumnName = "actual_completion_date", ColumnDescription = "实际完成日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ActualCompletionDate { get; set; }

    /// <summary>
    /// 进度百分比(%)
    /// </summary>
    [SugarColumn(ColumnName = "progress_percentage", ColumnDescription = "进度百分比", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ProgressPercentage { get; set; } = 0;

    /// <summary>
    /// 中期检查日期
    /// </summary>
    [SugarColumn(ColumnName = "midterm_check_date", ColumnDescription = "中期检查日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime MidtermCheckDate { get; set; }

    /// <summary>
    /// 中期检查结果
    /// </summary>
    [SugarColumn(ColumnName = "midterm_check_result", ColumnDescription = "中期检查结果", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string MidtermCheckResult { get; set; } = string.Empty;

    /// <summary>
    /// 改进结果说明
    /// </summary>
    [SugarColumn(ColumnName = "result_description", ColumnDescription = "改进结果说明", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ResultDescription { get; set; } = string.Empty;

    /// <summary>
    /// 指导老师ID
    /// </summary>
    [SugarColumn(ColumnName = "mentor_id", ColumnDescription = "指导老师ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long MentorId { get; set; }

    /// <summary>
    /// 审批人ID
    /// </summary>
    [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ApproverId { get; set; }

    /// <summary>
    /// 状态(0=待审批 1=进行中 2=中期检查 3=已完成 4=已评估 5=已关闭)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
