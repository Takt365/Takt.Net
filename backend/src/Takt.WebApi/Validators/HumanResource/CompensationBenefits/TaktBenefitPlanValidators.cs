// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktBenefitPlanValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：BenefitPlan DTO 验证器（根据实体 TaktBenefitPlan 自动生成）
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
/// BenefitPlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktBenefitPlan"/> 字段对齐）。
/// </summary>
public class TaktBenefitPlanCreateDtoValidator : AbstractValidator<TaktBenefitPlanCreateDto>
{
    public TaktBenefitPlanCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlanCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.plancode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.benefitplan.plancode", 1, 50));

        RuleFor(x => x.PlanName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.planname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.benefitplan.planname", 1, 100));

        RuleFor(x => x.BenefitType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.benefittype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.benefitplan.benefittype", 1, 50));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.applicabledepartment"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.benefitplan.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.applicableposition"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.benefitplan.applicableposition", 1, 100));

        RuleFor(x => x.ApplicableLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.applicablelevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.benefitplan.applicablelevel", 1, 50));

        RuleFor(x => x.DistributionMethod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.distributionmethod"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.benefitplan.distributionmethod", 1, 50));

        RuleFor(x => x.BenefitConditions)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.benefitconditions"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.benefitplan.benefitconditions", 1, 500));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.description"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.benefitplan.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.benefitplan.status"));
    }
}

/// <summary>
/// BenefitPlan更新 DTO 验证器。
/// </summary>
public class TaktBenefitPlanUpdateDtoValidator : AbstractValidator<TaktBenefitPlanUpdateDto>
{
    public TaktBenefitPlanUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktBenefitPlanCreateDtoValidator(localizer));

        RuleFor(x => x.BenefitPlanId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.benefitplan.benefitplanid"));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.benefitplan.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.PlanName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.benefitplan.planname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanName));

        RuleFor(x => x.BenefitType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.benefitplan.benefittype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BenefitType));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.benefitplan.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.benefitplan.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.ApplicableLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.benefitplan.applicablelevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableLevel));

        RuleFor(x => x.DistributionMethod)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.benefitplan.distributionmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DistributionMethod));

        RuleFor(x => x.BenefitConditions)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.benefitplan.benefitconditions", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BenefitConditions));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.benefitplan.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
