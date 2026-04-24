// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.I18n
// 文件名称：TaktLanguageValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Language DTO 验证器（根据实体 TaktLanguage 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.I18n;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Tasks.I18n;

/// <summary>
/// Language创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.I18n.TaktLanguage"/> 字段对齐）。
/// </summary>
public class TaktLanguageCreateDtoValidator : AbstractValidator<TaktLanguageCreateDto>
{
    public TaktLanguageCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.LanguageName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.language.languagename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.language.languagename", 1, 100));

        RuleFor(x => x.CultureCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.language.culturecode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.language.culturecode", 1, 20));

        RuleFor(x => x.NativeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.language.nativename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.language.nativename", 1, 100));

        RuleFor(x => x.LanguageIcon)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.language.languageicon", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LanguageIcon));

        RuleFor(x => x.IsDefault)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.language.isdefault"));

        RuleFor(x => x.IsRtl)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.language.isrtl"));

        RuleFor(x => x.LanguageStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.language.languagestatus"));
    }
}

/// <summary>
/// Language更新 DTO 验证器。
/// </summary>
public class TaktLanguageUpdateDtoValidator : AbstractValidator<TaktLanguageUpdateDto>
{
    public TaktLanguageUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktLanguageCreateDtoValidator(localizer));

        RuleFor(x => x.LanguageId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.language.languageid"));

        RuleFor(x => x.LanguageName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.language.languagename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LanguageName));

        RuleFor(x => x.CultureCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.language.culturecode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.CultureCode));

        RuleFor(x => x.NativeName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.language.nativename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativeName));

        RuleFor(x => x.LanguageIcon)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.language.languageicon", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LanguageIcon));
    }
}
