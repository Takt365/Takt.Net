// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Performance
// 文件名称：TaktPerformanceReviewValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PerformanceReview DTO 验证器（根据实体 TaktPerformanceReview 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Performance;

namespace Takt.Application.Validators.HumanResource.Performance;

/// <summary>
/// PerformanceReview创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktPerformanceReview"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPerformanceReviewCreateDtoValidator : AbstractValidator<TaktPerformanceReviewCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPerformanceReviewCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ReviewPeriod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performancereview.reviewperiod"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performancereview.reviewperiod", 1, 50));

        RuleFor(x => x.SelfEvaluationNotes)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performancereview.selfevaluationnotes"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.performancereview.selfevaluationnotes", 1, 1000));

        RuleFor(x => x.SupervisorComments)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performancereview.supervisorcomments"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.performancereview.supervisorcomments", 1, 1000));

        RuleFor(x => x.PerformanceGrade)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performancereview.performancegrade"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.performancereview.performancegrade", 1, 10));

        RuleFor(x => x.InterviewNotes)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performancereview.interviewnotes"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.performancereview.interviewnotes", 1, 1000));

        RuleFor(x => x.EmployeeFeedback)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performancereview.employeefeedback"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.performancereview.employeefeedback", 1, 500));

        RuleFor(x => x.ImprovementSuggestions)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performancereview.improvementsuggestions"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.performancereview.improvementsuggestions", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.performancereview.status"));
    }
}

/// <summary>
/// PerformanceReview更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPerformanceReviewCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PerformanceReviewId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPerformanceReviewUpdateDtoValidator : AbstractValidator<TaktPerformanceReviewUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPerformanceReviewUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPerformanceReviewCreateDtoValidator(validationMessages));

        RuleFor(x => x.PerformanceReviewId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.performancereview.performancereviewid"));

        RuleFor(x => x.ReviewPeriod)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performancereview.reviewperiod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ReviewPeriod));

        RuleFor(x => x.SelfEvaluationNotes)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.performancereview.selfevaluationnotes", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.SelfEvaluationNotes));

        RuleFor(x => x.SupervisorComments)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.performancereview.supervisorcomments", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.SupervisorComments));

        RuleFor(x => x.PerformanceGrade)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.performancereview.performancegrade", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.PerformanceGrade));

        RuleFor(x => x.InterviewNotes)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.performancereview.interviewnotes", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InterviewNotes));

        RuleFor(x => x.EmployeeFeedback)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.performancereview.employeefeedback", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EmployeeFeedback));

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.performancereview.improvementsuggestions", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestions));
    }
}
