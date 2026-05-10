// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Performance
// 文件名称：TaktImprovementPlanValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ImprovementPlan DTO 验证器（根据实体 TaktImprovementPlan 自动生成）
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
/// ImprovementPlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktImprovementPlan"/> 字段对齐）。
/// </summary>
public class TaktImprovementPlanCreateDtoValidator : AbstractValidator<TaktImprovementPlanCreateDto>
{
    public TaktImprovementPlanCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlanTitle)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.improvementplan.plantitle"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.improvementplan.plantitle", 1, 200));

        RuleFor(x => x.ImprovementArea)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.improvementplan.improvementarea"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.improvementplan.improvementarea", 1, 50));

        RuleFor(x => x.CurrentSituation)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.improvementplan.currentsituation"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.improvementplan.currentsituation", 1, 1000));

        RuleFor(x => x.ImprovementGoal)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.improvementplan.improvementgoal"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.improvementplan.improvementgoal", 1, 500));

        RuleFor(x => x.ImprovementActions)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.improvementplan.improvementactions"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.improvementplan.improvementactions", 1, 1000));

        RuleFor(x => x.RequiredResources)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.improvementplan.requiredresources"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.improvementplan.requiredresources", 1, 500));

        RuleFor(x => x.MidtermCheckResult)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.improvementplan.midtermcheckresult"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.improvementplan.midtermcheckresult", 1, 500));

        RuleFor(x => x.ResultDescription)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.improvementplan.resultdescription"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.improvementplan.resultdescription", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.improvementplan.status"));
    }
}

/// <summary>
/// ImprovementPlan更新 DTO 验证器。
/// </summary>
public class TaktImprovementPlanUpdateDtoValidator : AbstractValidator<TaktImprovementPlanUpdateDto>
{
    public TaktImprovementPlanUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktImprovementPlanCreateDtoValidator(localizer));

        RuleFor(x => x.ImprovementPlanId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.improvementplan.improvementplanid"));

        RuleFor(x => x.PlanTitle)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.improvementplan.plantitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanTitle));

        RuleFor(x => x.ImprovementArea)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.improvementplan.improvementarea", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementArea));

        RuleFor(x => x.CurrentSituation)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.improvementplan.currentsituation", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentSituation));

        RuleFor(x => x.ImprovementGoal)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.improvementplan.improvementgoal", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementGoal));

        RuleFor(x => x.ImprovementActions)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.improvementplan.improvementactions", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementActions));

        RuleFor(x => x.RequiredResources)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.improvementplan.requiredresources", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RequiredResources));

        RuleFor(x => x.MidtermCheckResult)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.improvementplan.midtermcheckresult", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.MidtermCheckResult));

        RuleFor(x => x.ResultDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.improvementplan.resultdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ResultDescription));
    }
}
