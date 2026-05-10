// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.CompensationBenefits
// 文件名称：TaktCompensationPlan.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：薪酬方案实体，定义不同岗位/职级的薪酬组合方案
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.CompensationBenefits;

/// <summary>
/// 薪酬方案实体
/// </summary>
[SugarTable("takt_human_resource_compensation_plan", "薪酬方案表")]
[SugarIndex("ix_takt_human_resource_compensation_plan_plan_code", nameof(PlanCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_compensation_plan_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_compensation_plan_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktCompensationPlan : TaktEntityBase
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
    /// 薪酬结构ID
    /// </summary>
    [SugarColumn(ColumnName = "salary_structure_id", ColumnDescription = "薪酬结构ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SalaryStructureId { get; set; }

    /// <summary>
    /// 基本工资占比(%)
    /// </summary>
    [SugarColumn(ColumnName = "base_salary_ratio", ColumnDescription = "基本工资占比", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal BaseSalaryRatio { get; set; } = 0;

    /// <summary>
    /// 绩效薪资占比(%)
    /// </summary>
    [SugarColumn(ColumnName = "performance_salary_ratio", ColumnDescription = "绩效薪资占比", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PerformanceSalaryRatio { get; set; } = 0;

    /// <summary>
    /// 津贴占比(%)
    /// </summary>
    [SugarColumn(ColumnName = "allowance_ratio", ColumnDescription = "津贴占比", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AllowanceRatio { get; set; } = 0;

    /// <summary>
    /// 年度调薪比例(%)
    /// </summary>
    [SugarColumn(ColumnName = "annual_adjustment_ratio", ColumnDescription = "年度调薪比例", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AnnualAdjustmentRatio { get; set; } = 0;

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
