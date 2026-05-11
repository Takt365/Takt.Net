// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Talent
// 文件名称：TaktTalentValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Talent DTO 验证器（根据实体 TaktTalent 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Talent;

namespace Takt.Application.Validators.HumanResource.Talent;

/// <summary>
/// Talent创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Talent.TaktTalent"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktTalentCreateDtoValidator : AbstractValidator<TaktTalentCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTalentCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.TalentLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.talent.talentlevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.talent.talentlevel", 1, 50));

        RuleFor(x => x.ProfessionalSkills)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.talent.professionalskills"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.talent.professionalskills", 1, 500));

        RuleFor(x => x.CoreCompetency)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.talent.corecompetency"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.talent.corecompetency", 1, 500));

        RuleFor(x => x.DevelopmentPotential)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.talent.developmentpotential"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.talent.developmentpotential", 1, 20));

        RuleFor(x => x.CareerPlan)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.talent.careerplan"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.talent.careerplan", 1, 1000));

        RuleFor(x => x.TalentTags)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.talent.talenttags"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.talent.talenttags", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.talent.status"));
    }
}

/// <summary>
/// Talent更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktTalentCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>TalentId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktTalentUpdateDtoValidator : AbstractValidator<TaktTalentUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTalentUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktTalentCreateDtoValidator(validationMessages));

        RuleFor(x => x.TalentId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.talent.talentid"));

        RuleFor(x => x.TalentLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.talent.talentlevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TalentLevel));

        RuleFor(x => x.ProfessionalSkills)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.talent.professionalskills", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ProfessionalSkills));

        RuleFor(x => x.CoreCompetency)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.talent.corecompetency", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CoreCompetency));

        RuleFor(x => x.DevelopmentPotential)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.talent.developmentpotential", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.DevelopmentPotential));

        RuleFor(x => x.CareerPlan)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.talent.careerplan", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CareerPlan));

        RuleFor(x => x.TalentTags)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.talent.talenttags", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.TalentTags));
    }
}
