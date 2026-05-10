// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingActivityValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TrainingActivity DTO 验证器（根据实体 TaktTrainingActivity 自动生成）
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
/// TrainingActivity创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.TrainingDevelopment.TaktTrainingActivity"/> 字段对齐）。
/// </summary>
public class TaktTrainingActivityCreateDtoValidator : AbstractValidator<TaktTrainingActivityCreateDto>
{
    public TaktTrainingActivityCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ActivityCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.activitycode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.activitycode", 1, 50));

        RuleFor(x => x.ActivityName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.activityname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.activityname", 1, 200));

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.starttime"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.starttime", 1, 10));

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.endtime"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.endtime", 1, 10));

        RuleFor(x => x.TrainingLocation)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.traininglocation"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.traininglocation", 1, 200));

        RuleFor(x => x.Instructor)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.instructor"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.instructor", 1, 50));

        RuleFor(x => x.ContentSummary)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.contentsummary"))
            .Length(1, 2000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.contentsummary", 1, 2000));

        RuleFor(x => x.TrainingMaterials)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.trainingmaterials"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.trainingmaterials", 1, 1000));

        RuleFor(x => x.EffectivenessEvaluation)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.effectivenessevaluation"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.effectivenessevaluation", 1, 1000));

        RuleFor(x => x.ParticipantFeedback)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.participantfeedback"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.participantfeedback", 1, 1000));

        RuleFor(x => x.ImprovementSuggestions)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.improvementsuggestions"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingactivity.improvementsuggestions", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.trainingactivity.status"));
    }
}

/// <summary>
/// TrainingActivity更新 DTO 验证器。
/// </summary>
public class TaktTrainingActivityUpdateDtoValidator : AbstractValidator<TaktTrainingActivityUpdateDto>
{
    public TaktTrainingActivityUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTrainingActivityCreateDtoValidator(localizer));

        RuleFor(x => x.TrainingActivityId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingactivity.trainingactivityid"));

        RuleFor(x => x.ActivityCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.activitycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ActivityCode));

        RuleFor(x => x.ActivityName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.activityname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ActivityName));

        RuleFor(x => x.StartTime)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.starttime", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.StartTime));

        RuleFor(x => x.EndTime)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.endtime", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.EndTime));

        RuleFor(x => x.TrainingLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.traininglocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingLocation));

        RuleFor(x => x.Instructor)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.instructor", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Instructor));

        RuleFor(x => x.ContentSummary)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.contentsummary", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ContentSummary));

        RuleFor(x => x.TrainingMaterials)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.trainingmaterials", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingMaterials));

        RuleFor(x => x.EffectivenessEvaluation)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.effectivenessevaluation", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.EffectivenessEvaluation));

        RuleFor(x => x.ParticipantFeedback)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.participantfeedback", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ParticipantFeedback));

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingactivity.improvementsuggestions", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestions));
    }
}
