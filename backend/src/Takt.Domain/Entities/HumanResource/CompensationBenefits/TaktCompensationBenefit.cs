// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.CompensationBenefits
// 文件名称：TaktCompensationBenefit.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：薪酬福利实体，记录员工薪酬结构、福利待遇等信息
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.CompensationBenefits;

/// <summary>
/// 薪酬福利实体
/// </summary>
[SugarTable("takt_human_resource_compensation_benefit", "薪酬福利表")]
[SugarIndex("ix_takt_human_resource_compensation_benefit_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_compensation_benefit_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_compensation_benefit_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktCompensationBenefit : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 基本工资
    /// </summary>
    [SugarColumn(ColumnName = "base_salary", ColumnDescription = "基本工资", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal BaseSalary { get; set; } = 0;

    /// <summary>
    /// 岗位津贴
    /// </summary>
    [SugarColumn(ColumnName = "position_allowance", ColumnDescription = "岗位津贴", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PositionAllowance { get; set; } = 0;

    /// <summary>
    /// 绩效奖金
    /// </summary>
    [SugarColumn(ColumnName = "performance_bonus", ColumnDescription = "绩效奖金", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PerformanceBonus { get; set; } = 0;

    /// <summary>
    /// 加班费
    /// </summary>
    [SugarColumn(ColumnName = "overtime_pay", ColumnDescription = "加班费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal OvertimePay { get; set; } = 0;

    /// <summary>
    /// 交通补贴
    /// </summary>
    [SugarColumn(ColumnName = "transport_allowance", ColumnDescription = "交通补贴", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TransportAllowance { get; set; } = 0;

    /// <summary>
    /// 餐费补贴
    /// </summary>
    [SugarColumn(ColumnName = "meal_allowance", ColumnDescription = "餐费补贴", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal MealAllowance { get; set; } = 0;

    /// <summary>
    /// 住房补贴
    /// </summary>
    [SugarColumn(ColumnName = "housing_allowance", ColumnDescription = "住房补贴", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal HousingAllowance { get; set; } = 0;

    /// <summary>
    /// 社保缴纳基数
    /// </summary>
    [SugarColumn(ColumnName = "social_security_base", ColumnDescription = "社保缴纳基数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SocialSecurityBase { get; set; } = 0;

    /// <summary>
    /// 公积金缴纳基数
    /// </summary>
    [SugarColumn(ColumnName = "housing_fund_base", ColumnDescription = "公积金缴纳基数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal HousingFundBase { get; set; } = 0;

    /// <summary>
    /// 其他福利说明
    /// </summary>
    [SugarColumn(ColumnName = "other_benefits", ColumnDescription = "其他福利说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string OtherBenefits { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// 状态(0=正常 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
