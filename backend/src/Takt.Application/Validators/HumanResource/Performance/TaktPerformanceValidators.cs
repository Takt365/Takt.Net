// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Performance
// 文件名称：TaktPerformanceValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Performance DTO 验证器（根据实体 TaktPerformance 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Performance;

namespace Takt.Application.Validators.HumanResource.Performance;

/// <summary>
/// Performance创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktPerformance"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPerformanceCreateDtoValidator : AbstractValidator<TaktPerformanceCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPerformanceCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.EvaluationPeriod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performance.evaluationperiod"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performance.evaluationperiod", 1, 50));

        RuleFor(x => x.EvaluationCriteria)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performance.evaluationcriteria"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.performance.evaluationcriteria", 1, 500));

        RuleFor(x => x.Grade)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performance.grade"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.performance.grade", 1, 10));

        RuleFor(x => x.Comments)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performance.comments"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.performance.comments", 1, 1000));

        RuleFor(x => x.ImprovementSuggestions)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performance.improvementsuggestions"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.performance.improvementsuggestions", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.performance.status"));
    }
}

/// <summary>
/// Performance更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPerformanceCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PerformanceId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPerformanceUpdateDtoValidator : AbstractValidator<TaktPerformanceUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPerformanceUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPerformanceCreateDtoValidator(validationMessages));

        RuleFor(x => x.PerformanceId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.performance.performanceid"));

        RuleFor(x => x.EvaluationPeriod)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performance.evaluationperiod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationPeriod));

        RuleFor(x => x.EvaluationCriteria)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.performance.evaluationcriteria", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationCriteria));

        RuleFor(x => x.Grade)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.performance.grade", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.Grade));

        RuleFor(x => x.Comments)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.performance.comments", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Comments));

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.performance.improvementsuggestions", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestions));
    }
}
