// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktCompensationPlanValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CompensationPlan DTO 验证器（根据实体 TaktCompensationPlan 自动生成）
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
/// CompensationPlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktCompensationPlan"/> 字段对齐）。
/// </summary>
public class TaktCompensationPlanCreateDtoValidator : AbstractValidator<TaktCompensationPlanCreateDto>
{
    public TaktCompensationPlanCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlanCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.compensationplan.plancode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.compensationplan.plancode", 1, 50));

        RuleFor(x => x.PlanName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.compensationplan.planname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.compensationplan.planname", 1, 100));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.compensationplan.applicabledepartment"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.compensationplan.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.compensationplan.applicableposition"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.compensationplan.applicableposition", 1, 100));

        RuleFor(x => x.ApplicableLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.compensationplan.applicablelevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.compensationplan.applicablelevel", 1, 50));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.compensationplan.description"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.compensationplan.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.compensationplan.status"));
    }
}

/// <summary>
/// CompensationPlan更新 DTO 验证器。
/// </summary>
public class TaktCompensationPlanUpdateDtoValidator : AbstractValidator<TaktCompensationPlanUpdateDto>
{
    public TaktCompensationPlanUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktCompensationPlanCreateDtoValidator(localizer));

        RuleFor(x => x.CompensationPlanId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.compensationplan.compensationplanid"));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.compensationplan.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.PlanName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.compensationplan.planname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanName));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.compensationplan.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.compensationplan.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.ApplicableLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.compensationplan.applicablelevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableLevel));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.compensationplan.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
