// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Performance
// 文件名称：TaktPerformanceIndicatorValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PerformanceIndicator DTO 验证器（根据实体 TaktPerformanceIndicator 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Performance;

namespace Takt.Application.Validators.HumanResource.Performance;

/// <summary>
/// PerformanceIndicator创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktPerformanceIndicator"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPerformanceIndicatorCreateDtoValidator : AbstractValidator<TaktPerformanceIndicatorCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPerformanceIndicatorCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.IndicatorCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceindicator.indicatorcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performanceindicator.indicatorcode", 1, 50));

        RuleFor(x => x.IndicatorName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceindicator.indicatorname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.performanceindicator.indicatorname", 1, 100));

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceindicator.category"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performanceindicator.category", 1, 50));

        RuleFor(x => x.IndicatorType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceindicator.indicatortype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performanceindicator.indicatortype", 1, 50));

        RuleFor(x => x.IndicatorDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceindicator.indicatordescription"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.performanceindicator.indicatordescription", 1, 500));

        RuleFor(x => x.ScoringCriteria)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceindicator.scoringcriteria"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.performanceindicator.scoringcriteria", 1, 1000));

        RuleFor(x => x.DataSource)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceindicator.datasource"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.performanceindicator.datasource", 1, 200));

        RuleFor(x => x.EvaluationCycle)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceindicator.evaluationcycle"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performanceindicator.evaluationcycle", 1, 50));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.performanceindicator.status"));
    }
}

/// <summary>
/// PerformanceIndicator更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPerformanceIndicatorCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PerformanceIndicatorId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPerformanceIndicatorUpdateDtoValidator : AbstractValidator<TaktPerformanceIndicatorUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPerformanceIndicatorUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPerformanceIndicatorCreateDtoValidator(validationMessages));

        RuleFor(x => x.PerformanceIndicatorId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.performanceindicator.performanceindicatorid"));

        RuleFor(x => x.IndicatorCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performanceindicator.indicatorcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IndicatorCode));

        RuleFor(x => x.IndicatorName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.performanceindicator.indicatorname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.IndicatorName));

        RuleFor(x => x.Category)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performanceindicator.category", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Category));

        RuleFor(x => x.IndicatorType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performanceindicator.indicatortype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IndicatorType));

        RuleFor(x => x.IndicatorDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.performanceindicator.indicatordescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.IndicatorDescription));

        RuleFor(x => x.ScoringCriteria)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.performanceindicator.scoringcriteria", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ScoringCriteria));

        RuleFor(x => x.DataSource)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.performanceindicator.datasource", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DataSource));

        RuleFor(x => x.EvaluationCycle)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performanceindicator.evaluationcycle", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationCycle));
    }
}
