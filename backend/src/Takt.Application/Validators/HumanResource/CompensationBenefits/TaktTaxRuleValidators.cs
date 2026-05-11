// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktTaxRuleValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaxRule DTO 验证器（根据实体 TaktTaxRule 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;

namespace Takt.Application.Validators.HumanResource.CompensationBenefits;

/// <summary>
/// TaxRule创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktTaxRule"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktTaxRuleCreateDtoValidator : AbstractValidator<TaktTaxRuleCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTaxRuleCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.RuleCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.taxrule.rulecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.taxrule.rulecode", 1, 50));

        RuleFor(x => x.RuleName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.taxrule.rulename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.taxrule.rulename", 1, 100));

        RuleFor(x => x.CalculationFormula)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.taxrule.calculationformula"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.taxrule.calculationformula", 1, 500));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.taxrule.description"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.taxrule.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.taxrule.status"));
    }
}

/// <summary>
/// TaxRule更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktTaxRuleCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>TaxRuleId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktTaxRuleUpdateDtoValidator : AbstractValidator<TaktTaxRuleUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTaxRuleUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktTaxRuleCreateDtoValidator(validationMessages));

        RuleFor(x => x.TaxRuleId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.taxrule.taxruleid"));

        RuleFor(x => x.RuleCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.taxrule.rulecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleCode));

        RuleFor(x => x.RuleName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.taxrule.rulename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleName));

        RuleFor(x => x.CalculationFormula)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.taxrule.calculationformula", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CalculationFormula));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.taxrule.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
