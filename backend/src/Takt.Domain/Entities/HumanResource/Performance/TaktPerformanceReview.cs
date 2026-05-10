// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Performance
// 文件名称：TaktPerformanceReview.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效评审实体，记录绩效评估过程、评审结果、面谈记录等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Performance;

/// <summary>
/// 绩效评审实体
/// </summary>
[SugarTable("takt_human_resource_performance_review", "绩效评审表")]
[SugarIndex("ix_takt_human_resource_performance_review_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_review_review_date", nameof(ReviewDate), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_performance_review_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_review_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPerformanceReview : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 评审周期(如：2025-Q1, 2025-H1, 2025-Annual)
    /// </summary>
    [SugarColumn(ColumnName = "review_period", ColumnDescription = "评审周期", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ReviewPeriod { get; set; } = string.Empty;

    /// <summary>
    /// 评审日期
    /// </summary>
    [SugarColumn(ColumnName = "review_date", ColumnDescription = "评审日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ReviewDate { get; set; }

    /// <summary>
    /// 绩效方案ID
    /// </summary>
    [SugarColumn(ColumnName = "performance_plan_id", ColumnDescription = "绩效方案ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PerformancePlanId { get; set; }

    /// <summary>
    /// 自评分数
    /// </summary>
    [SugarColumn(ColumnName = "self_score", ColumnDescription = "自评分数", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SelfScore { get; set; } = 0;

    /// <summary>
    /// 自评说明
    /// </summary>
    [SugarColumn(ColumnName = "self_evaluation_notes", ColumnDescription = "自评说明", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SelfEvaluationNotes { get; set; } = string.Empty;

    /// <summary>
    /// 主管评分
    /// </summary>
    [SugarColumn(ColumnName = "supervisor_score", ColumnDescription = "主管评分", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SupervisorScore { get; set; } = 0;

    /// <summary>
    /// 主管评语
    /// </summary>
    [SugarColumn(ColumnName = "supervisor_comments", ColumnDescription = "主管评语", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SupervisorComments { get; set; } = string.Empty;

    /// <summary>
    /// 综合得分
    /// </summary>
    [SugarColumn(ColumnName = "final_score", ColumnDescription = "综合得分", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal FinalScore { get; set; } = 0;

    /// <summary>
    /// 绩效等级(A/B/C/D/E)
    /// </summary>
    [SugarColumn(ColumnName = "performance_grade", ColumnDescription = "绩效等级", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PerformanceGrade { get; set; } = string.Empty;

    /// <summary>
    /// 评审人ID
    /// </summary>
    [SugarColumn(ColumnName = "reviewer_id", ColumnDescription = "评审人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ReviewerId { get; set; }

    /// <summary>
    /// 面谈日期
    /// </summary>
    [SugarColumn(ColumnName = "interview_date", ColumnDescription = "面谈日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime InterviewDate { get; set; }

    /// <summary>
    /// 面谈记录
    /// </summary>
    [SugarColumn(ColumnName = "interview_notes", ColumnDescription = "面谈记录", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string InterviewNotes { get; set; } = string.Empty;

    /// <summary>
    /// 员工反馈
    /// </summary>
    [SugarColumn(ColumnName = "employee_feedback", ColumnDescription = "员工反馈", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EmployeeFeedback { get; set; } = string.Empty;

    /// <summary>
    /// 改进建议
    /// </summary>
    [SugarColumn(ColumnName = "improvement_suggestions", ColumnDescription = "改进建议", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ImprovementSuggestions { get; set; } = string.Empty;

    /// <summary>
    /// 下次评审日期
    /// </summary>
    [SugarColumn(ColumnName = "next_review_date", ColumnDescription = "下次评审日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime NextReviewDate { get; set; }

    /// <summary>
    /// 状态(0=待自评 1=自评中 2=待主管评审 3=评审中 4=已完成 5=已确认)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
