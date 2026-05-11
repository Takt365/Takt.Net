// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingDevelopmentValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TrainingDevelopment DTO 验证器（根据实体 TaktTrainingDevelopment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.TrainingDevelopment;

namespace Takt.Application.Validators.HumanResource.TrainingDevelopment;

/// <summary>
/// TrainingDevelopment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.TrainingDevelopment.TaktTrainingDevelopment"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktTrainingDevelopmentCreateDtoValidator : AbstractValidator<TaktTrainingDevelopmentCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTrainingDevelopmentCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingdevelopment.coursename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.trainingdevelopment.coursename", 1, 200));

        RuleFor(x => x.TrainingType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingdevelopment.trainingtype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingdevelopment.trainingtype", 1, 50));

        RuleFor(x => x.Instructor)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingdevelopment.instructor"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingdevelopment.instructor", 1, 50));

        RuleFor(x => x.TrainingLocation)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingdevelopment.traininglocation"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.trainingdevelopment.traininglocation", 1, 100));

        RuleFor(x => x.IsPassed)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.trainingdevelopment.ispassed"));

        RuleFor(x => x.CertificateNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingdevelopment.certificateno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingdevelopment.certificateno", 1, 50));

        RuleFor(x => x.TrainingEvaluation)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingdevelopment.trainingevaluation"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.trainingdevelopment.trainingevaluation", 1, 500));

        RuleFor(x => x.ImprovementSuggestions)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingdevelopment.improvementsuggestions"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.trainingdevelopment.improvementsuggestions", 1, 500));

        RuleFor(x => x.DevelopmentPlan)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingdevelopment.developmentplan"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.trainingdevelopment.developmentplan", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.trainingdevelopment.status"));
    }
}

/// <summary>
/// TrainingDevelopment更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktTrainingDevelopmentCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>TrainingDevelopmentId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktTrainingDevelopmentUpdateDtoValidator : AbstractValidator<TaktTrainingDevelopmentUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTrainingDevelopmentUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktTrainingDevelopmentCreateDtoValidator(validationMessages));

        RuleFor(x => x.TrainingDevelopmentId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.trainingdevelopment.trainingdevelopmentid"));

        RuleFor(x => x.CourseName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.trainingdevelopment.coursename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseName));

        RuleFor(x => x.TrainingType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingdevelopment.trainingtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingType));

        RuleFor(x => x.Instructor)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingdevelopment.instructor", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Instructor));

        RuleFor(x => x.TrainingLocation)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.trainingdevelopment.traininglocation", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingLocation));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingdevelopment.certificateno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));

        RuleFor(x => x.TrainingEvaluation)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.trainingdevelopment.trainingevaluation", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingEvaluation));

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.trainingdevelopment.improvementsuggestions", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestions));

        RuleFor(x => x.DevelopmentPlan)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.trainingdevelopment.developmentplan", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.DevelopmentPlan));
    }
}
