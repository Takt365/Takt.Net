// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.NumberingRule
// 文件名称：TaktNumberingRuleValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：NumberingRule DTO 验证器（根据实体 TaktNumberingRule 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.NumberingRule;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Tasks.NumberingRule;

/// <summary>
/// NumberingRule创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.NumberingRule.TaktNumberingRule"/> 字段对齐）。
/// </summary>
public class TaktNumberingRuleCreateDtoValidator : AbstractValidator<TaktNumberingRuleCreateDto>
{
    public TaktNumberingRuleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RuleCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.numberingrule.rulecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.numberingrule.rulecode", 1, 50));

        RuleFor(x => x.RuleName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.numberingrule.rulename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.numberingrule.rulename", 1, 100));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.DeptCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.numberingrule.deptcode"))
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.deptcode", 50));

        RuleFor(x => x.Prefix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.prefix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Prefix));

        RuleFor(x => x.DateFormat)
            .MaximumLength(32).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.dateformat", 32))
            .When(x => !string.IsNullOrWhiteSpace(x.DateFormat));

        RuleFor(x => x.Suffix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.suffix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Suffix));

        RuleFor(x => x.RuleStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.numberingrule.rulestatus"));
    }
}

/// <summary>
/// NumberingRule更新 DTO 验证器。
/// </summary>
public class TaktNumberingRuleUpdateDtoValidator : AbstractValidator<TaktNumberingRuleUpdateDto>
{
    public TaktNumberingRuleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktNumberingRuleCreateDtoValidator(localizer));

        RuleFor(x => x.NumberingRuleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.numberingrule.numberingruleid"));

        RuleFor(x => x.RuleCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.rulecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleCode));

        RuleFor(x => x.RuleName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.rulename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleName));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.Prefix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.prefix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Prefix));

        RuleFor(x => x.DateFormat)
            .MaximumLength(32).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.dateformat", 32))
            .When(x => !string.IsNullOrWhiteSpace(x.DateFormat));

        RuleFor(x => x.Suffix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numberingrule.suffix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Suffix));
    }
}
