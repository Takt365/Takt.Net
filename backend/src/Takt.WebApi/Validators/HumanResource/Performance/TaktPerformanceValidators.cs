// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Performance
// 文件名称：TaktPerformanceValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Performance DTO 验证器（根据实体 TaktPerformance 自动生成）
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
/// Performance创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktPerformance"/> 字段对齐）。
/// </summary>
public class TaktPerformanceCreateDtoValidator : AbstractValidator<TaktPerformanceCreateDto>
{
    public TaktPerformanceCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.EvaluationPeriod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performance.evaluationperiod"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performance.evaluationperiod", 1, 50));

        RuleFor(x => x.EvaluationCriteria)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performance.evaluationcriteria"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performance.evaluationcriteria", 1, 500));

        RuleFor(x => x.Grade)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performance.grade"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performance.grade", 1, 10));

        RuleFor(x => x.Comments)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performance.comments"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performance.comments", 1, 1000));

        RuleFor(x => x.ImprovementSuggestions)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performance.improvementsuggestions"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performance.improvementsuggestions", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.performance.status"));
    }
}

/// <summary>
/// Performance更新 DTO 验证器。
/// </summary>
public class TaktPerformanceUpdateDtoValidator : AbstractValidator<TaktPerformanceUpdateDto>
{
    public TaktPerformanceUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPerformanceCreateDtoValidator(localizer));

        RuleFor(x => x.PerformanceId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.performance.performanceid"));

        RuleFor(x => x.EvaluationPeriod)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performance.evaluationperiod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationPeriod));

        RuleFor(x => x.EvaluationCriteria)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performance.evaluationcriteria", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationCriteria));

        RuleFor(x => x.Grade)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performance.grade", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.Grade));

        RuleFor(x => x.Comments)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performance.comments", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Comments));

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performance.improvementsuggestions", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestions));
    }
}
