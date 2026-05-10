// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.CompensationBenefits
// 文件名称：TaktBenefitPlan.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：福利方案实体，定义公司提供的各类福利套餐
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.CompensationBenefits;

/// <summary>
/// 福利方案实体
/// </summary>
[SugarTable("takt_human_resource_benefit_plan", "福利方案表")]
[SugarIndex("ix_takt_human_resource_benefit_plan_plan_code", nameof(PlanCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_benefit_plan_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_benefit_plan_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktBenefitPlan : TaktEntityBase
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
    /// 福利类型(五险一金/补充医疗/商业保险/年假/体检/节日福利/餐补/交通补/通讯补/其他)
    /// </summary>
    [SugarColumn(ColumnName = "benefit_type", ColumnDescription = "福利类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string BenefitType { get; set; } = string.Empty;

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
    /// 福利标准金额
    /// </summary>
    [SugarColumn(ColumnName = "benefit_standard_amount", ColumnDescription = "福利标准金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal BenefitStandardAmount { get; set; } = 0;

    /// <summary>
    /// 发放方式(按月/按季/按年/一次性/实报实销)
    /// </summary>
    [SugarColumn(ColumnName = "distribution_method", ColumnDescription = "发放方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DistributionMethod { get; set; } = string.Empty;

    /// <summary>
    /// 福利条件说明
    /// </summary>
    [SugarColumn(ColumnName = "benefit_conditions", ColumnDescription = "福利条件说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string BenefitConditions { get; set; } = string.Empty;

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
