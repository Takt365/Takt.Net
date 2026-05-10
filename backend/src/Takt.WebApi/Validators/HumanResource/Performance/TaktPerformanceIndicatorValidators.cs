// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Performance
// 文件名称：TaktPerformanceIndicatorValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PerformanceIndicator DTO 验证器（根据实体 TaktPerformanceIndicator 自动生成）
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
/// PerformanceIndicator创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktPerformanceIndicator"/> 字段对齐）。
/// </summary>
public class TaktPerformanceIndicatorCreateDtoValidator : AbstractValidator<TaktPerformanceIndicatorCreateDto>
{
    public TaktPerformanceIndicatorCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.IndicatorCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceindicator.indicatorcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceindicator.indicatorcode", 1, 50));

        RuleFor(x => x.IndicatorName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceindicator.indicatorname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceindicator.indicatorname", 1, 100));

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceindicator.category"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceindicator.category", 1, 50));

        RuleFor(x => x.IndicatorType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceindicator.indicatortype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceindicator.indicatortype", 1, 50));

        RuleFor(x => x.IndicatorDescription)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceindicator.indicatordescription"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceindicator.indicatordescription", 1, 500));

        RuleFor(x => x.ScoringCriteria)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceindicator.scoringcriteria"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceindicator.scoringcriteria", 1, 1000));

        RuleFor(x => x.DataSource)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceindicator.datasource"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceindicator.datasource", 1, 200));

        RuleFor(x => x.EvaluationCycle)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceindicator.evaluationcycle"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performanceindicator.evaluationcycle", 1, 50));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.performanceindicator.status"));
    }
}

/// <summary>
/// PerformanceIndicator更新 DTO 验证器。
/// </summary>
public class TaktPerformanceIndicatorUpdateDtoValidator : AbstractValidator<TaktPerformanceIndicatorUpdateDto>
{
    public TaktPerformanceIndicatorUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPerformanceIndicatorCreateDtoValidator(localizer));

        RuleFor(x => x.PerformanceIndicatorId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.performanceindicator.performanceindicatorid"));

        RuleFor(x => x.IndicatorCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceindicator.indicatorcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IndicatorCode));

        RuleFor(x => x.IndicatorName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceindicator.indicatorname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndicatorName));

        RuleFor(x => x.Category)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceindicator.category", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Category));

        RuleFor(x => x.IndicatorType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceindicator.indicatortype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IndicatorType));

        RuleFor(x => x.IndicatorDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceindicator.indicatordescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.IndicatorDescription));

        RuleFor(x => x.ScoringCriteria)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceindicator.scoringcriteria", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ScoringCriteria));

        RuleFor(x => x.DataSource)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceindicator.datasource", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DataSource));

        RuleFor(x => x.EvaluationCycle)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performanceindicator.evaluationcycle", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationCycle));
    }
}
