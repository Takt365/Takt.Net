// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.CompensationBenefits
// 文件名称：TaktTaxRule.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：税务规则实体，定义个人所得税计算规则、税率表、扣除标准等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.CompensationBenefits;

/// <summary>
/// 税务规则实体
/// </summary>
[SugarTable("takt_human_resource_tax_rule", "税务规则表")]
[SugarIndex("ix_takt_human_resource_tax_rule_rule_code", nameof(RuleCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_tax_rule_tax_year", nameof(TaxYear), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_tax_rule_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_tax_rule_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktTaxRule : TaktEntityBase
{
    /// <summary>
    /// 规则编码
    /// </summary>
    [SugarColumn(ColumnName = "rule_code", ColumnDescription = "规则编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string RuleCode { get; set; } = string.Empty;

    /// <summary>
    /// 规则名称
    /// </summary>
    [SugarColumn(ColumnName = "rule_name", ColumnDescription = "规则名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string RuleName { get; set; } = string.Empty;

    /// <summary>
    /// 税务年度
    /// </summary>
    [SugarColumn(ColumnName = "tax_year", ColumnDescription = "税务年度", ColumnDataType = "int", IsNullable = false)]
    public int TaxYear { get; set; }

    /// <summary>
    /// 税收起征点
    /// </summary>
    [SugarColumn(ColumnName = "tax_threshold", ColumnDescription = "税收起征点", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TaxThreshold { get; set; } = 0;

    /// <summary>
    /// 应纳税所得额下限
    /// </summary>
    [SugarColumn(ColumnName = "taxable_income_min", ColumnDescription = "应纳税所得额下限", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TaxableIncomeMin { get; set; } = 0;

    /// <summary>
    /// 应纳税所得额上限
    /// </summary>
    [SugarColumn(ColumnName = "taxable_income_max", ColumnDescription = "应纳税所得额上限", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TaxableIncomeMax { get; set; } = 0;

    /// <summary>
    /// 税率(%)
    /// </summary>
    [SugarColumn(ColumnName = "tax_rate", ColumnDescription = "税率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TaxRate { get; set; } = 0;

    /// <summary>
    /// 速算扣除数
    /// </summary>
    [SugarColumn(ColumnName = "quick_deduction", ColumnDescription = "速算扣除数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal QuickDeduction { get; set; } = 0;

    /// <summary>
    /// 专项扣除标准
    /// </summary>
    [SugarColumn(ColumnName = "special_deduction_standard", ColumnDescription = "专项扣除标准", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SpecialDeductionStandard { get; set; } = 0;

    /// <summary>
    /// 社保扣除比例(%)
    /// </summary>
    [SugarColumn(ColumnName = "social_security_deduction_rate", ColumnDescription = "社保扣除比例", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SocialSecurityDeductionRate { get; set; } = 0;

    /// <summary>
    /// 公积金扣除比例(%)
    /// </summary>
    [SugarColumn(ColumnName = "housing_fund_deduction_rate", ColumnDescription = "公积金扣除比例", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal HousingFundDeductionRate { get; set; } = 0;

    /// <summary>
    /// 计算公式
    /// </summary>
    [SugarColumn(ColumnName = "calculation_formula", ColumnDescription = "计算公式", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CalculationFormula { get; set; } = string.Empty;

    /// <summary>
    /// 规则说明
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "规则说明", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
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
