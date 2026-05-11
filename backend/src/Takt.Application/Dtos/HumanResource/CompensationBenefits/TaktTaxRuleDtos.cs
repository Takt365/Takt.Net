// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.CompensationBenefits
// 文件名称：TaktTaxRuleDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：税务规则表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.CompensationBenefits;

/// <summary>
/// 税务规则表Dto
/// </summary>
public partial class TaktTaxRuleDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTaxRuleDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// 税务规则表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TaxRuleId { get; set; } = 0;

    /// <summary>
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }
    /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }
    /// <summary>
    /// 税务年度
    /// </summary>
    public int TaxYear { get; set; }
    /// <summary>
    /// 税收起征点
    /// </summary>
    public decimal TaxThreshold { get; set; }
    /// <summary>
    /// 应纳税所得额下限
    /// </summary>
    public decimal TaxableIncomeMin { get; set; }
    /// <summary>
    /// 应纳税所得额上限
    /// </summary>
    public decimal TaxableIncomeMax { get; set; }
    /// <summary>
    /// 税率
    /// </summary>
    public decimal TaxRate { get; set; }
    /// <summary>
    /// 速算扣除数
    /// </summary>
    public decimal QuickDeduction { get; set; }
    /// <summary>
    /// 专项扣除标准
    /// </summary>
    public decimal SpecialDeductionStandard { get; set; }
    /// <summary>
    /// 社保扣除比例
    /// </summary>
    public decimal SocialSecurityDeductionRate { get; set; }
    /// <summary>
    /// 公积金扣除比例
    /// </summary>
    public decimal HousingFundDeductionRate { get; set; }
    /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }
    /// <summary>
    /// 规则说明
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 税务规则表查询DTO
/// </summary>
public partial class TaktTaxRuleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTaxRuleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 规则编码
    /// </summary>
    public string? RuleCode { get; set; }
    /// <summary>
    /// 规则名称
    /// </summary>
    public string? RuleName { get; set; }
    /// <summary>
    /// 税务年度
    /// </summary>
    public int? TaxYear { get; set; }
    /// <summary>
    /// 税收起征点
    /// </summary>
    public decimal? TaxThreshold { get; set; }
    /// <summary>
    /// 应纳税所得额下限
    /// </summary>
    public decimal? TaxableIncomeMin { get; set; }
    /// <summary>
    /// 应纳税所得额上限
    /// </summary>
    public decimal? TaxableIncomeMax { get; set; }
    /// <summary>
    /// 税率
    /// </summary>
    public decimal? TaxRate { get; set; }
    /// <summary>
    /// 速算扣除数
    /// </summary>
    public decimal? QuickDeduction { get; set; }
    /// <summary>
    /// 专项扣除标准
    /// </summary>
    public decimal? SpecialDeductionStandard { get; set; }
    /// <summary>
    /// 社保扣除比例
    /// </summary>
    public decimal? SocialSecurityDeductionRate { get; set; }
    /// <summary>
    /// 公积金扣除比例
    /// </summary>
    public decimal? HousingFundDeductionRate { get; set; }
    /// <summary>
    /// 计算公式
    /// </summary>
    public string? CalculationFormula { get; set; }
    /// <summary>
    /// 规则说明
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 生效日期开始时间
    /// </summary>
    public DateTime? EffectiveDateStart { get; set; }
    /// <summary>
    /// 生效日期结束时间
    /// </summary>
    public DateTime? EffectiveDateEnd { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建税务规则表DTO
/// </summary>
public partial class TaktTaxRuleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTaxRuleCreateDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }

        /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

        /// <summary>
    /// 税务年度
    /// </summary>
    public int TaxYear { get; set; }

        /// <summary>
    /// 税收起征点
    /// </summary>
    public decimal TaxThreshold { get; set; }

        /// <summary>
    /// 应纳税所得额下限
    /// </summary>
    public decimal TaxableIncomeMin { get; set; }

        /// <summary>
    /// 应纳税所得额上限
    /// </summary>
    public decimal TaxableIncomeMax { get; set; }

        /// <summary>
    /// 税率
    /// </summary>
    public decimal TaxRate { get; set; }

        /// <summary>
    /// 速算扣除数
    /// </summary>
    public decimal QuickDeduction { get; set; }

        /// <summary>
    /// 专项扣除标准
    /// </summary>
    public decimal SpecialDeductionStandard { get; set; }

        /// <summary>
    /// 社保扣除比例
    /// </summary>
    public decimal SocialSecurityDeductionRate { get; set; }

        /// <summary>
    /// 公积金扣除比例
    /// </summary>
    public decimal HousingFundDeductionRate { get; set; }

        /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }

        /// <summary>
    /// 规则说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

}

/// <summary>
/// Takt更新税务规则表DTO
/// </summary>
public partial class TaktTaxRuleUpdateDto : TaktTaxRuleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTaxRuleUpdateDto()
    {
    }

        /// <summary>
    /// 税务规则表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TaxRuleId { get; set; } = 0;
}

/// <summary>
/// 税务规则表状态DTO
/// </summary>
public partial class TaktTaxRuleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTaxRuleStatusDto()
    {
    }

        /// <summary>
    /// 税务规则表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TaxRuleId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 税务规则表导入模板DTO
/// </summary>
public partial class TaktTaxRuleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTaxRuleTemplateDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }

        /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

        /// <summary>
    /// 税务年度
    /// </summary>
    public int TaxYear { get; set; }

        /// <summary>
    /// 税收起征点
    /// </summary>
    public decimal TaxThreshold { get; set; }

        /// <summary>
    /// 应纳税所得额下限
    /// </summary>
    public decimal TaxableIncomeMin { get; set; }

        /// <summary>
    /// 应纳税所得额上限
    /// </summary>
    public decimal TaxableIncomeMax { get; set; }

        /// <summary>
    /// 税率
    /// </summary>
    public decimal TaxRate { get; set; }

        /// <summary>
    /// 速算扣除数
    /// </summary>
    public decimal QuickDeduction { get; set; }

        /// <summary>
    /// 专项扣除标准
    /// </summary>
    public decimal SpecialDeductionStandard { get; set; }

        /// <summary>
    /// 社保扣除比例
    /// </summary>
    public decimal SocialSecurityDeductionRate { get; set; }

        /// <summary>
    /// 公积金扣除比例
    /// </summary>
    public decimal HousingFundDeductionRate { get; set; }

        /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }

        /// <summary>
    /// 规则说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 税务规则表导入DTO
/// </summary>
public partial class TaktTaxRuleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTaxRuleImportDto()
    {
        RuleCode = string.Empty;
        RuleName = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }

        /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

        /// <summary>
    /// 税务年度
    /// </summary>
    public int TaxYear { get; set; }

        /// <summary>
    /// 税收起征点
    /// </summary>
    public decimal TaxThreshold { get; set; }

        /// <summary>
    /// 应纳税所得额下限
    /// </summary>
    public decimal TaxableIncomeMin { get; set; }

        /// <summary>
    /// 应纳税所得额上限
    /// </summary>
    public decimal TaxableIncomeMax { get; set; }

        /// <summary>
    /// 税率
    /// </summary>
    public decimal TaxRate { get; set; }

        /// <summary>
    /// 速算扣除数
    /// </summary>
    public decimal QuickDeduction { get; set; }

        /// <summary>
    /// 专项扣除标准
    /// </summary>
    public decimal SpecialDeductionStandard { get; set; }

        /// <summary>
    /// 社保扣除比例
    /// </summary>
    public decimal SocialSecurityDeductionRate { get; set; }

        /// <summary>
    /// 公积金扣除比例
    /// </summary>
    public decimal HousingFundDeductionRate { get; set; }

        /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }

        /// <summary>
    /// 规则说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 税务规则表导出DTO
/// </summary>
public partial class TaktTaxRuleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTaxRuleExportDto()
    {
        CreatedAt = DateTime.Now;
        RuleCode = string.Empty;
        RuleName = string.Empty;
        CalculationFormula = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 规则编码
    /// </summary>
    public string RuleCode { get; set; }

        /// <summary>
    /// 规则名称
    /// </summary>
    public string RuleName { get; set; }

        /// <summary>
    /// 税务年度
    /// </summary>
    public int TaxYear { get; set; }

        /// <summary>
    /// 税收起征点
    /// </summary>
    public decimal TaxThreshold { get; set; }

        /// <summary>
    /// 应纳税所得额下限
    /// </summary>
    public decimal TaxableIncomeMin { get; set; }

        /// <summary>
    /// 应纳税所得额上限
    /// </summary>
    public decimal TaxableIncomeMax { get; set; }

        /// <summary>
    /// 税率
    /// </summary>
    public decimal TaxRate { get; set; }

        /// <summary>
    /// 速算扣除数
    /// </summary>
    public decimal QuickDeduction { get; set; }

        /// <summary>
    /// 专项扣除标准
    /// </summary>
    public decimal SpecialDeductionStandard { get; set; }

        /// <summary>
    /// 社保扣除比例
    /// </summary>
    public decimal SocialSecurityDeductionRate { get; set; }

        /// <summary>
    /// 公积金扣除比例
    /// </summary>
    public decimal HousingFundDeductionRate { get; set; }

        /// <summary>
    /// 计算公式
    /// </summary>
    public string CalculationFormula { get; set; }

        /// <summary>
    /// 规则说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}