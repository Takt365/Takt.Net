// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.TrainingDevelopment
// 文件名称：TaktSkillAssessmentValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SkillAssessment DTO 验证器（根据实体 TaktSkillAssessment 自动生成）
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
/// SkillAssessment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.TrainingDevelopment.TaktSkillAssessment"/> 字段对齐）。
/// </summary>
public class TaktSkillAssessmentCreateDtoValidator : AbstractValidator<TaktSkillAssessmentCreateDto>
{
    public TaktSkillAssessmentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.SkillCategory)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.skillcategory"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.skillcategory", 1, 50));

        RuleFor(x => x.SkillName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.skillname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.skillname", 1, 100));

        RuleFor(x => x.SkillDescription)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.skilldescription"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.skilldescription", 1, 500));

        RuleFor(x => x.AssessmentMethod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.assessmentmethod"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.assessmentmethod", 1, 50));

        RuleFor(x => x.SkillLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.skilllevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.skilllevel", 1, 50));

        RuleFor(x => x.PreviousLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.previouslevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.previouslevel", 1, 50));

        RuleFor(x => x.NewLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.newlevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.newlevel", 1, 50));

        RuleFor(x => x.IsPassed)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.skillassessment.ispassed"));

        RuleFor(x => x.CertificateNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.certificateno"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.certificateno", 1, 50));

        RuleFor(x => x.AssessmentComments)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.assessmentcomments"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.assessmentcomments", 1, 1000));

        RuleFor(x => x.StrengthsAnalysis)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.strengthsanalysis"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.strengthsanalysis", 1, 500));

        RuleFor(x => x.ImprovementSuggestions)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.improvementsuggestions"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.skillassessment.improvementsuggestions", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.skillassessment.status"));
    }
}

/// <summary>
/// SkillAssessment更新 DTO 验证器。
/// </summary>
public class TaktSkillAssessmentUpdateDtoValidator : AbstractValidator<TaktSkillAssessmentUpdateDto>
{
    public TaktSkillAssessmentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSkillAssessmentCreateDtoValidator(localizer));

        RuleFor(x => x.SkillAssessmentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.skillassessment.skillassessmentid"));

        RuleFor(x => x.SkillCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.skillcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillCategory));

        RuleFor(x => x.SkillName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.skillname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillName));

        RuleFor(x => x.SkillDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.skilldescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillDescription));

        RuleFor(x => x.AssessmentMethod)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.assessmentmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssessmentMethod));

        RuleFor(x => x.SkillLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.skilllevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillLevel));

        RuleFor(x => x.PreviousLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.previouslevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PreviousLevel));

        RuleFor(x => x.NewLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.newlevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NewLevel));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.certificateno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));

        RuleFor(x => x.AssessmentComments)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.assessmentcomments", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.AssessmentComments));

        RuleFor(x => x.StrengthsAnalysis)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.strengthsanalysis", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.StrengthsAnalysis));

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.skillassessment.improvementsuggestions", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestions));
    }
}
