// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktTaxRuleValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TaxRule DTO 验证器（根据实体 TaktTaxRule 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.CompensationBenefits;

/// <summary>
/// TaxRule创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktTaxRule"/> 字段对齐）。
/// </summary>
public class TaktTaxRuleCreateDtoValidator : AbstractValidator<TaktTaxRuleCreateDto>
{
    public TaktTaxRuleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RuleCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.taxrule.rulecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.taxrule.rulecode", 1, 50));

        RuleFor(x => x.RuleName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.taxrule.rulename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.taxrule.rulename", 1, 100));

        RuleFor(x => x.CalculationFormula)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.taxrule.calculationformula"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.taxrule.calculationformula", 1, 500));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.taxrule.description"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.taxrule.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.taxrule.status"));
    }
}

/// <summary>
/// TaxRule更新 DTO 验证器。
/// </summary>
public class TaktTaxRuleUpdateDtoValidator : AbstractValidator<TaktTaxRuleUpdateDto>
{
    public TaktTaxRuleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTaxRuleCreateDtoValidator(localizer));

        RuleFor(x => x.TaxRuleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.taxrule.taxruleid"));

        RuleFor(x => x.RuleCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.taxrule.rulecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleCode));

        RuleFor(x => x.RuleName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.taxrule.rulename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleName));

        RuleFor(x => x.CalculationFormula)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.taxrule.calculationformula", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CalculationFormula));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.taxrule.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
