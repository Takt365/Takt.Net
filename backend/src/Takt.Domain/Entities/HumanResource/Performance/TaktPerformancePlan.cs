// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Performance
// 文件名称：TaktPerformancePlan.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效方案实体，定义绩效考核方案、考核周期、评分标准等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Performance;

/// <summary>
/// 绩效方案实体
/// </summary>
[SugarTable("takt_human_resource_performance_plan", "绩效方案表")]
[SugarIndex("ix_takt_human_resource_performance_plan_plan_code", nameof(PlanCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_performance_plan_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_performance_plan_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPerformancePlan : TaktEntityBase
{
    /// <summary>
    /// 方案编码
    /// </summary>
    [SugarColumn(ColumnName = "plan_code", ColumnDescription = "方案编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlanCode { get; set; } = string.Empty;

    /// <summary>
    /// 方案名称
    /// </summary>
    [SugarColumn(ColumnName = "plan_name", ColumnDescription = "方案名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlanName { get; set; } = string.Empty;

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
    /// 考核周期类型(月度/季度/半年度/年度)
    /// </summary>
    [SugarColumn(ColumnName = "cycle_type", ColumnDescription = "考核周期类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CycleType { get; set; } = string.Empty;

    /// <summary>
    /// 评分标准(百分制/五分制/等级制)
    /// </summary>
    [SugarColumn(ColumnName = "scoring_standard", ColumnDescription = "评分标准", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ScoringStandard { get; set; } = string.Empty;

    /// <summary>
    /// 自评权重(%)
    /// </summary>
    [SugarColumn(ColumnName = "self_evaluation_weight", ColumnDescription = "自评权重", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SelfEvaluationWeight { get; set; } = 0;

    /// <summary>
    /// 主管评分权重(%)
    /// </summary>
    [SugarColumn(ColumnName = "supervisor_weight", ColumnDescription = "主管评分权重", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SupervisorWeight { get; set; } = 0;

    /// <summary>
    /// 同事评分权重(%)
    /// </summary>
    [SugarColumn(ColumnName = "peer_weight", ColumnDescription = "同事评分权重", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PeerWeight { get; set; } = 0;

    /// <summary>
    /// 方案说明
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "方案说明", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// 状态(0=启用 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
