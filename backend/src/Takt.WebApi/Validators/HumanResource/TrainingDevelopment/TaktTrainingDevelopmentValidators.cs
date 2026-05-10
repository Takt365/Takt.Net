// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingDevelopmentValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TrainingDevelopment DTO 验证器（根据实体 TaktTrainingDevelopment 自动生成）
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
/// TrainingDevelopment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.TrainingDevelopment.TaktTrainingDevelopment"/> 字段对齐）。
/// </summary>
public class TaktTrainingDevelopmentCreateDtoValidator : AbstractValidator<TaktTrainingDevelopmentCreateDto>
{
    public TaktTrainingDevelopmentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingdevelopment.coursename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingdevelopment.coursename", 1, 200));

        RuleFor(x => x.TrainingType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingdevelopment.trainingtype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingdevelopment.trainingtype", 1, 50));

        RuleFor(x => x.Instructor)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingdevelopment.instructor"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingdevelopment.instructor", 1, 50));

        RuleFor(x => x.TrainingLocation)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingdevelopment.traininglocation"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingdevelopment.traininglocation", 1, 100));

        RuleFor(x => x.IsPassed)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.trainingdevelopment.ispassed"));

        RuleFor(x => x.CertificateNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingdevelopment.certificateno"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingdevelopment.certificateno", 1, 50));

        RuleFor(x => x.TrainingEvaluation)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingdevelopment.trainingevaluation"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingdevelopment.trainingevaluation", 1, 500));

        RuleFor(x => x.ImprovementSuggestions)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingdevelopment.improvementsuggestions"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingdevelopment.improvementsuggestions", 1, 500));

        RuleFor(x => x.DevelopmentPlan)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingdevelopment.developmentplan"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingdevelopment.developmentplan", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.trainingdevelopment.status"));
    }
}

/// <summary>
/// TrainingDevelopment更新 DTO 验证器。
/// </summary>
public class TaktTrainingDevelopmentUpdateDtoValidator : AbstractValidator<TaktTrainingDevelopmentUpdateDto>
{
    public TaktTrainingDevelopmentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTrainingDevelopmentCreateDtoValidator(localizer));

        RuleFor(x => x.TrainingDevelopmentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingdevelopment.trainingdevelopmentid"));

        RuleFor(x => x.CourseName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingdevelopment.coursename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseName));

        RuleFor(x => x.TrainingType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingdevelopment.trainingtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingType));

        RuleFor(x => x.Instructor)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingdevelopment.instructor", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Instructor));

        RuleFor(x => x.TrainingLocation)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingdevelopment.traininglocation", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingLocation));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingdevelopment.certificateno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));

        RuleFor(x => x.TrainingEvaluation)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingdevelopment.trainingevaluation", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingEvaluation));

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingdevelopment.improvementsuggestions", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestions));

        RuleFor(x => x.DevelopmentPlan)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingdevelopment.developmentplan", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.DevelopmentPlan));
    }
}
