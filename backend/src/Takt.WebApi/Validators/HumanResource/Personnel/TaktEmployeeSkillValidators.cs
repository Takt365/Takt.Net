// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeSkillValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeSkill DTO 验证器（根据实体 TaktEmployeeSkill 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeSkill创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeSkill"/> 字段对齐）。
/// </summary>
public class TaktEmployeeSkillCreateDtoValidator : AbstractValidator<TaktEmployeeSkillCreateDto>
{
    public TaktEmployeeSkillCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.SkillName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeeskill.skillname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeeskill.skillname", 1, 100));

        RuleFor(x => x.SkillLevel)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeeskill.skilllevel"));

        RuleFor(x => x.CertificateName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeskill.certificatename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateName));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeskill.certificateno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));
    }
}

/// <summary>
/// EmployeeSkill更新 DTO 验证器。
/// </summary>
public class TaktEmployeeSkillUpdateDtoValidator : AbstractValidator<TaktEmployeeSkillUpdateDto>
{
    public TaktEmployeeSkillUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeSkillCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeSkillId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeeskill.employeeskillid"));

        RuleFor(x => x.SkillName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeskill.skillname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillName));

        RuleFor(x => x.CertificateName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeskill.certificatename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateName));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeskill.certificateno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));
    }
}
