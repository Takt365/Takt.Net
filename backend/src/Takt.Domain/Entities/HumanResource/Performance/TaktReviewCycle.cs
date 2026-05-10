// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Performance
// 文件名称：TaktReviewCycle.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：评审周期实体，定义绩效考核周期安排、时间节点、状态等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Performance;

/// <summary>
/// 评审周期实体
/// </summary>
[SugarTable("takt_human_resource_review_cycle", "评审周期表")]
[SugarIndex("ix_takt_human_resource_review_cycle_cycle_code", nameof(CycleCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_review_cycle_cycle_year", nameof(CycleYear), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_review_cycle_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_review_cycle_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktReviewCycle : TaktEntityBase
{
    /// <summary>
    /// 周期编码
    /// </summary>
    [SugarColumn(ColumnName = "cycle_code", ColumnDescription = "周期编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CycleCode { get; set; } = string.Empty;

    /// <summary>
    /// 周期名称
    /// </summary>
    [SugarColumn(ColumnName = "cycle_name", ColumnDescription = "周期名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CycleName { get; set; } = string.Empty;

    /// <summary>
    /// 周期类型(月度/季度/半年度/年度)
    /// </summary>
    [SugarColumn(ColumnName = "cycle_type", ColumnDescription = "周期类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CycleType { get; set; } = string.Empty;

    /// <summary>
    /// 周期年度
    /// </summary>
    [SugarColumn(ColumnName = "cycle_year", ColumnDescription = "周期年度", ColumnDataType = "int", IsNullable = false)]
    public int CycleYear { get; set; }

    /// <summary>
    /// 周期序号(如：Q1=1, Q2=2, H1=1, H2=2, Annual=1)
    /// </summary>
    [SugarColumn(ColumnName = "cycle_sequence", ColumnDescription = "周期序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CycleSequence { get; set; } = 0;

    /// <summary>
    /// 开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    [SugarColumn(ColumnName = "end_date", ColumnDescription = "结束日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 目标设定开始日期
    /// </summary>
    [SugarColumn(ColumnName = "goal_setting_start_date", ColumnDescription = "目标设定开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime GoalSettingStartDate { get; set; }

    /// <summary>
    /// 目标设定截止日期
    /// </summary>
    [SugarColumn(ColumnName = "goal_setting_due_date", ColumnDescription = "目标设定截止日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime GoalSettingDueDate { get; set; }

    /// <summary>
    /// 自评开始日期
    /// </summary>
    [SugarColumn(ColumnName = "self_evaluation_start_date", ColumnDescription = "自评开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime SelfEvaluationStartDate { get; set; }

    /// <summary>
    /// 自评截止日期
    /// </summary>
    [SugarColumn(ColumnName = "self_evaluation_due_date", ColumnDescription = "自评截止日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime SelfEvaluationDueDate { get; set; }

    /// <summary>
    /// 主管评审开始日期
    /// </summary>
    [SugarColumn(ColumnName = "supervisor_review_start_date", ColumnDescription = "主管评审开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime SupervisorReviewStartDate { get; set; }

    /// <summary>
    /// 主管评审截止日期
    /// </summary>
    [SugarColumn(ColumnName = "supervisor_review_due_date", ColumnDescription = "主管评审截止日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime SupervisorReviewDueDate { get; set; }

    /// <summary>
    /// 面谈截止日期
    /// </summary>
    [SugarColumn(ColumnName = "interview_due_date", ColumnDescription = "面谈截止日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime InterviewDueDate { get; set; }

    /// <summary>
    /// 结果确认截止日期
    /// </summary>
    [SugarColumn(ColumnName = "result_confirmation_due_date", ColumnDescription = "结果确认截止日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ResultConfirmationDueDate { get; set; }

    /// <summary>
    /// 适用部门
    /// </summary>
    [SugarColumn(ColumnName = "applicable_department", ColumnDescription = "适用部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicableDepartment { get; set; } = string.Empty;

    /// <summary>
    /// 周期说明
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "周期说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 状态(0=待启动 1=目标设定中 2=进行中 3=评审中 4=已完成 5=已归档)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
