// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktEmployeeBenefitValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeBenefit DTO 验证器（根据实体 TaktEmployeeBenefit 自动生成）
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
/// EmployeeBenefit创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktEmployeeBenefit"/> 字段对齐）。
/// </summary>
public class TaktEmployeeBenefitCreateDtoValidator : AbstractValidator<TaktEmployeeBenefitCreateDto>
{
    public TaktEmployeeBenefitCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.BenefitType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeebenefit.benefittype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeebenefit.benefittype", 1, 50));

        RuleFor(x => x.BenefitName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeebenefit.benefitname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeebenefit.benefitname", 1, 100));

        RuleFor(x => x.DistributionMethod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeebenefit.distributionmethod"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeebenefit.distributionmethod", 1, 50));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeebenefit.description"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeebenefit.description", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeebenefit.status"));
    }
}

/// <summary>
/// EmployeeBenefit更新 DTO 验证器。
/// </summary>
public class TaktEmployeeBenefitUpdateDtoValidator : AbstractValidator<TaktEmployeeBenefitUpdateDto>
{
    public TaktEmployeeBenefitUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeBenefitCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeBenefitId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeebenefit.employeebenefitid"));

        RuleFor(x => x.BenefitType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeebenefit.benefittype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BenefitType));

        RuleFor(x => x.BenefitName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeebenefit.benefitname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.BenefitName));

        RuleFor(x => x.DistributionMethod)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeebenefit.distributionmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DistributionMethod));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeebenefit.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
