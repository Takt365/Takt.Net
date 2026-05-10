// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Performance
// 文件名称：TaktPerformancePlanValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PerformancePlan DTO 验证器（根据实体 TaktPerformancePlan 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Performance;

/// <summary>
/// PerformancePlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktPerformancePlan"/> 字段对齐）。
/// </summary>
public class TaktPerformancePlanCreateDtoValidator : AbstractValidator<TaktPerformancePlanCreateDto>
{
    public TaktPerformancePlanCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlanCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceplan.plancode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceplan.plancode", 1, 50));

        RuleFor(x => x.PlanName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceplan.planname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceplan.planname", 1, 100));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceplan.applicabledepartment"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceplan.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceplan.applicableposition"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceplan.applicableposition", 1, 100));

        RuleFor(x => x.ApplicableLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceplan.applicablelevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceplan.applicablelevel", 1, 50));

        RuleFor(x => x.CycleType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceplan.cycletype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceplan.cycletype", 1, 50));

        RuleFor(x => x.ScoringStandard)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceplan.scoringstandard"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceplan.scoringstandard", 1, 50));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceplan.description"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceplan.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.performanceplan.status"));
    }
}

/// <summary>
/// PerformancePlan更新 DTO 验证器。
/// </summary>
public class TaktPerformancePlanUpdateDtoValidator : AbstractValidator<TaktPerformancePlanUpdateDto>
{
    public TaktPerformancePlanUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPerformancePlanCreateDtoValidator(localizer));

        RuleFor(x => x.PerformancePlanId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceplan.performanceplanid"));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceplan.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.PlanName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceplan.planname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanName));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceplan.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceplan.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.ApplicableLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceplan.applicablelevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableLevel));

        RuleFor(x => x.CycleType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceplan.cycletype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CycleType));

        RuleFor(x => x.ScoringStandard)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceplan.scoringstandard", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ScoringStandard));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceplan.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
