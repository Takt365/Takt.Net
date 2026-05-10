// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Talent
// 文件名称：TaktTalentValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Talent DTO 验证器（根据实体 TaktTalent 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Talent;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Talent;

/// <summary>
/// Talent创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Talent.TaktTalent"/> 字段对齐）。
/// </summary>
public class TaktTalentCreateDtoValidator : AbstractValidator<TaktTalentCreateDto>
{
    public TaktTalentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TalentLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.talent.talentlevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.talent.talentlevel", 1, 50));

        RuleFor(x => x.ProfessionalSkills)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.talent.professionalskills"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.talent.professionalskills", 1, 500));

        RuleFor(x => x.CoreCompetency)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.talent.corecompetency"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.talent.corecompetency", 1, 500));

        RuleFor(x => x.DevelopmentPotential)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.talent.developmentpotential"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.talent.developmentpotential", 1, 20));

        RuleFor(x => x.CareerPlan)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.talent.careerplan"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.talent.careerplan", 1, 1000));

        RuleFor(x => x.TalentTags)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.talent.talenttags"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.talent.talenttags", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.talent.status"));
    }
}

/// <summary>
/// Talent更新 DTO 验证器。
/// </summary>
public class TaktTalentUpdateDtoValidator : AbstractValidator<TaktTalentUpdateDto>
{
    public TaktTalentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTalentCreateDtoValidator(localizer));

        RuleFor(x => x.TalentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.talent.talentid"));

        RuleFor(x => x.TalentLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.talent.talentlevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TalentLevel));

        RuleFor(x => x.ProfessionalSkills)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.talent.professionalskills", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ProfessionalSkills));

        RuleFor(x => x.CoreCompetency)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.talent.corecompetency", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CoreCompetency));

        RuleFor(x => x.DevelopmentPotential)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.talent.developmentpotential", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.DevelopmentPotential));

        RuleFor(x => x.CareerPlan)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.talent.careerplan", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CareerPlan));

        RuleFor(x => x.TalentTags)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.talent.talenttags", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.TalentTags));
    }
}
