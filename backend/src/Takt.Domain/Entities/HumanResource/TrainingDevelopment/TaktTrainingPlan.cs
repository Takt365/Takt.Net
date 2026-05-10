// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingPlan.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：培训计划实体，定义年度/季度培训计划、培训安排、预算等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训计划实体
/// </summary>
[SugarTable("takt_human_resource_training_plan", "培训计划表")]
[SugarIndex("ix_takt_human_resource_training_plan_plan_code", nameof(PlanCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_training_plan_plan_year", nameof(PlanYear), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_training_plan_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_training_plan_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktTrainingPlan : TaktEntityBase
{
    /// <summary>
    /// 计划编码
    /// </summary>
    [SugarColumn(ColumnName = "plan_code", ColumnDescription = "计划编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlanCode { get; set; } = string.Empty;

    /// <summary>
    /// 计划名称
    /// </summary>
    [SugarColumn(ColumnName = "plan_name", ColumnDescription = "计划名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlanName { get; set; } = string.Empty;

    /// <summary>
    /// 计划年度
    /// </summary>
    [SugarColumn(ColumnName = "plan_year", ColumnDescription = "计划年度", ColumnDataType = "int", IsNullable = false)]
    public int PlanYear { get; set; }

    /// <summary>
    /// 计划类型(年度/季度/月度/专项)
    /// </summary>
    [SugarColumn(ColumnName = "plan_type", ColumnDescription = "计划类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlanType { get; set; } = string.Empty;

    /// <summary>
    /// 适用部门
    /// </summary>
    [SugarColumn(ColumnName = "applicable_department", ColumnDescription = "适用部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicableDepartment { get; set; } = string.Empty;

    /// <summary>
    /// 适用岗位
    /// </summary>
    [SugarColumn(ColumnName = "applicable_position", ColumnDescription = "适用岗位", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicablePosition { get; set; } = string.Empty;

    /// <summary>
    /// 适用职级
    /// </summary>
    [SugarColumn(ColumnName = "applicable_level", ColumnDescription = "适用职级", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicableLevel { get; set; } = string.Empty;

    /// <summary>
    /// 计划开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "计划开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 计划结束日期
    /// </summary>
    [SugarColumn(ColumnName = "end_date", ColumnDescription = "计划结束日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 培训目标
    /// </summary>
    [SugarColumn(ColumnName = "training_objectives", ColumnDescription = "培训目标", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TrainingObjectives { get; set; } = string.Empty;

    /// <summary>
    /// 计划培训人数
    /// </summary>
    [SugarColumn(ColumnName = "planned_headcount", ColumnDescription = "计划培训人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PlannedHeadcount { get; set; } = 0;

    /// <summary>
    /// 实际培训人数
    /// </summary>
    [SugarColumn(ColumnName = "actual_headcount", ColumnDescription = "实际培训人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ActualHeadcount { get; set; } = 0;

    /// <summary>
    /// 计划总课时（小时）
    /// </summary>
    [SugarColumn(ColumnName = "planned_total_hours", ColumnDescription = "计划总课时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PlannedTotalHours { get; set; } = 0;

    /// <summary>
    /// 实际总课时（小时）
    /// </summary>
    [SugarColumn(ColumnName = "actual_total_hours", ColumnDescription = "实际总课时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ActualTotalHours { get; set; } = 0;

    /// <summary>
    /// 培训预算（元）
    /// </summary>
    [SugarColumn(ColumnName = "training_budget", ColumnDescription = "培训预算", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TrainingBudget { get; set; } = 0;

    /// <summary>
    /// 实际花费（元）
    /// </summary>
    [SugarColumn(ColumnName = "actual_cost", ColumnDescription = "实际花费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ActualCost { get; set; } = 0;

    /// <summary>
    /// 计划说明
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "计划说明", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 审批人ID
    /// </summary>
    [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ApproverId { get; set; }

    /// <summary>
    /// 状态(0=草稿 1=待审批 2=已批准 3=执行中 4=已完成 5=已取消)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
