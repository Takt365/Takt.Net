// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Performance
// 文件名称：TaktPerformanceReviewValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PerformanceReview DTO 验证器（根据实体 TaktPerformanceReview 自动生成）
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
/// PerformanceReview创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktPerformanceReview"/> 字段对齐）。
/// </summary>
public class TaktPerformanceReviewCreateDtoValidator : AbstractValidator<TaktPerformanceReviewCreateDto>
{
    public TaktPerformanceReviewCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ReviewPeriod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancereview.reviewperiod"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancereview.reviewperiod", 1, 50));

        RuleFor(x => x.SelfEvaluationNotes)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancereview.selfevaluationnotes"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancereview.selfevaluationnotes", 1, 1000));

        RuleFor(x => x.SupervisorComments)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancereview.supervisorcomments"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancereview.supervisorcomments", 1, 1000));

        RuleFor(x => x.PerformanceGrade)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancereview.performancegrade"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancereview.performancegrade", 1, 10));

        RuleFor(x => x.InterviewNotes)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancereview.interviewnotes"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancereview.interviewnotes", 1, 1000));

        RuleFor(x => x.EmployeeFeedback)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancereview.employeefeedback"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancereview.employeefeedback", 1, 500));

        RuleFor(x => x.ImprovementSuggestions)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.performancereview.improvementsuggestions"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.performancereview.improvementsuggestions", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.performancereview.status"));
    }
}

/// <summary>
/// PerformanceReview更新 DTO 验证器。
/// </summary>
public class TaktPerformanceReviewUpdateDtoValidator : AbstractValidator<TaktPerformanceReviewUpdateDto>
{
    public TaktPerformanceReviewUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPerformanceReviewCreateDtoValidator(localizer));

        RuleFor(x => x.PerformanceReviewId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.performancereview.performancereviewid"));

        RuleFor(x => x.ReviewPeriod)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancereview.reviewperiod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ReviewPeriod));

        RuleFor(x => x.SelfEvaluationNotes)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancereview.selfevaluationnotes", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.SelfEvaluationNotes));

        RuleFor(x => x.SupervisorComments)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancereview.supervisorcomments", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.SupervisorComments));

        RuleFor(x => x.PerformanceGrade)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancereview.performancegrade", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.PerformanceGrade));

        RuleFor(x => x.InterviewNotes)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancereview.interviewnotes", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InterviewNotes));

        RuleFor(x => x.EmployeeFeedback)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancereview.employeefeedback", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EmployeeFeedback));

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.performancereview.improvementsuggestions", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestions));
    }
}
