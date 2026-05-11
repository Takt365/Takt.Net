// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.TrainingDevelopment
// 文件名称：TaktSkillAssessmentValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SkillAssessment DTO 验证器（根据实体 TaktSkillAssessment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.TrainingDevelopment;

namespace Takt.Application.Validators.HumanResource.TrainingDevelopment;

/// <summary>
/// SkillAssessment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.TrainingDevelopment.TaktSkillAssessment"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSkillAssessmentCreateDtoValidator : AbstractValidator<TaktSkillAssessmentCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSkillAssessmentCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.SkillCategory)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.skillcategory"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.skillcategory", 1, 50));

        RuleFor(x => x.SkillName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.skillname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.skillname", 1, 100));

        RuleFor(x => x.SkillDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.skilldescription"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.skilldescription", 1, 500));

        RuleFor(x => x.AssessmentMethod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.assessmentmethod"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.assessmentmethod", 1, 50));

        RuleFor(x => x.SkillLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.skilllevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.skilllevel", 1, 50));

        RuleFor(x => x.PreviousLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.previouslevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.previouslevel", 1, 50));

        RuleFor(x => x.NewLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.newlevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.newlevel", 1, 50));

        RuleFor(x => x.IsPassed)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.skillassessment.ispassed"));

        RuleFor(x => x.CertificateNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.certificateno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.certificateno", 1, 50));

        RuleFor(x => x.AssessmentComments)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.assessmentcomments"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.assessmentcomments", 1, 1000));

        RuleFor(x => x.StrengthsAnalysis)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.strengthsanalysis"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.strengthsanalysis", 1, 500));

        RuleFor(x => x.ImprovementSuggestions)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.skillassessment.improvementsuggestions"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.skillassessment.improvementsuggestions", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.skillassessment.status"));
    }
}

/// <summary>
/// SkillAssessment更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSkillAssessmentCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SkillAssessmentId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSkillAssessmentUpdateDtoValidator : AbstractValidator<TaktSkillAssessmentUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSkillAssessmentUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSkillAssessmentCreateDtoValidator(validationMessages));

        RuleFor(x => x.SkillAssessmentId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.skillassessment.skillassessmentid"));

        RuleFor(x => x.SkillCategory)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.skillassessment.skillcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillCategory));

        RuleFor(x => x.SkillName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.skillassessment.skillname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillName));

        RuleFor(x => x.SkillDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.skillassessment.skilldescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillDescription));

        RuleFor(x => x.AssessmentMethod)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.skillassessment.assessmentmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssessmentMethod));

        RuleFor(x => x.SkillLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.skillassessment.skilllevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillLevel));

        RuleFor(x => x.PreviousLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.skillassessment.previouslevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PreviousLevel));

        RuleFor(x => x.NewLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.skillassessment.newlevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NewLevel));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.skillassessment.certificateno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));

        RuleFor(x => x.AssessmentComments)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.skillassessment.assessmentcomments", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.AssessmentComments));

        RuleFor(x => x.StrengthsAnalysis)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.skillassessment.strengthsanalysis", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.StrengthsAnalysis));

        RuleFor(x => x.ImprovementSuggestions)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.skillassessment.improvementsuggestions", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestions));
    }
}
