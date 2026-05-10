// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Performance
// 文件名称：TaktPerformanceGoalValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PerformanceGoal DTO 验证器（根据实体 TaktPerformanceGoal 自动生成）
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
/// PerformanceGoal创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktPerformanceGoal"/> 字段对齐）。
/// </summary>
public class TaktPerformanceGoalCreateDtoValidator : AbstractValidator<TaktPerformanceGoalCreateDto>
{
    public TaktPerformanceGoalCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.GoalPeriod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancegoal.goalperiod"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancegoal.goalperiod", 1, 50));

        RuleFor(x => x.GoalDescription)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancegoal.goaldescription"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancegoal.goaldescription", 1, 500));

        RuleFor(x => x.AchievementNotes)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancegoal.achievementnotes"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancegoal.achievementnotes", 1, 1000));

        RuleFor(x => x.FailureReason)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancegoal.failurereason"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancegoal.failurereason", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.performancegoal.status"));
    }
}

/// <summary>
/// PerformanceGoal更新 DTO 验证器。
/// </summary>
public class TaktPerformanceGoalUpdateDtoValidator : AbstractValidator<TaktPerformanceGoalUpdateDto>
{
    public TaktPerformanceGoalUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPerformanceGoalCreateDtoValidator(localizer));

        RuleFor(x => x.PerformanceGoalId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.performancegoal.performancegoalid"));

        RuleFor(x => x.GoalPeriod)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancegoal.goalperiod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.GoalPeriod));

        RuleFor(x => x.GoalDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancegoal.goaldescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.GoalDescription));

        RuleFor(x => x.AchievementNotes)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancegoal.achievementnotes", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.AchievementNotes));

        RuleFor(x => x.FailureReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancegoal.failurereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FailureReason));
    }
}
