// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingPlanValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TrainingPlan DTO 验证器（根据实体 TaktTrainingPlan 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.TrainingDevelopment;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.TrainingDevelopment;

/// <summary>
/// TrainingPlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.TrainingDevelopment.TaktTrainingPlan"/> 字段对齐）。
/// </summary>
public class TaktTrainingPlanCreateDtoValidator : AbstractValidator<TaktTrainingPlanCreateDto>
{
    public TaktTrainingPlanCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlanCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingplan.plancode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingplan.plancode", 1, 50));

        RuleFor(x => x.PlanName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingplan.planname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingplan.planname", 1, 200));

        RuleFor(x => x.PlanType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingplan.plantype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingplan.plantype", 1, 50));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingplan.applicabledepartment"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingplan.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingplan.applicableposition"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingplan.applicableposition", 1, 100));

        RuleFor(x => x.ApplicableLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingplan.applicablelevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingplan.applicablelevel", 1, 50));

        RuleFor(x => x.TrainingObjectives)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingplan.trainingobjectives"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingplan.trainingobjectives", 1, 1000));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingplan.description"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingplan.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.trainingplan.status"));
    }
}

/// <summary>
/// TrainingPlan更新 DTO 验证器。
/// </summary>
public class TaktTrainingPlanUpdateDtoValidator : AbstractValidator<TaktTrainingPlanUpdateDto>
{
    public TaktTrainingPlanUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTrainingPlanCreateDtoValidator(localizer));

        RuleFor(x => x.TrainingPlanId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingplan.trainingplanid"));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingplan.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.PlanName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingplan.planname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanName));

        RuleFor(x => x.PlanType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingplan.plantype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanType));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingplan.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingplan.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.ApplicableLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingplan.applicablelevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableLevel));

        RuleFor(x => x.TrainingObjectives)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingplan.trainingobjectives", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingObjectives));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingplan.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
