// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryComponentValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalaryComponent DTO 验证器（根据实体 TaktSalaryComponent 自动生成）
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
/// SalaryComponent创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktSalaryComponent"/> 字段对齐）。
/// </summary>
public class TaktSalaryComponentCreateDtoValidator : AbstractValidator<TaktSalaryComponentCreateDto>
{
    public TaktSalaryComponentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ComponentCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarycomponent.componentcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarycomponent.componentcode", 1, 50));

        RuleFor(x => x.ComponentName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarycomponent.componentname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarycomponent.componentname", 1, 100));

        RuleFor(x => x.ComponentType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarycomponent.componenttype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarycomponent.componenttype", 1, 50));

        RuleFor(x => x.CalculationMethod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarycomponent.calculationmethod"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarycomponent.calculationmethod", 1, 50));

        RuleFor(x => x.CalculationFormula)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarycomponent.calculationformula"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarycomponent.calculationformula", 1, 500));

        RuleFor(x => x.IsTaxable)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salarycomponent.istaxable"));

        RuleFor(x => x.IsSocialSecurityBase)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salarycomponent.issocialsecuritybase"));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarycomponent.description"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarycomponent.description", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salarycomponent.status"));
    }
}

/// <summary>
/// SalaryComponent更新 DTO 验证器。
/// </summary>
public class TaktSalaryComponentUpdateDtoValidator : AbstractValidator<TaktSalaryComponentUpdateDto>
{
    public TaktSalaryComponentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSalaryComponentCreateDtoValidator(localizer));

        RuleFor(x => x.SalaryComponentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.salarycomponent.salarycomponentid"));

        RuleFor(x => x.ComponentCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarycomponent.componentcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ComponentCode));

        RuleFor(x => x.ComponentName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarycomponent.componentname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ComponentName));

        RuleFor(x => x.ComponentType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarycomponent.componenttype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ComponentType));

        RuleFor(x => x.CalculationMethod)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarycomponent.calculationmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CalculationMethod));

        RuleFor(x => x.CalculationFormula)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarycomponent.calculationformula", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CalculationFormula));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarycomponent.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
