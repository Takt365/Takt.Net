// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.I18n
// 文件名称：TaktLanguageValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Language DTO 验证器（根据实体 TaktLanguage 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.I18n;

namespace Takt.Application.Validators.Routine.Tasks.I18n;

/// <summary>
/// Language创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.I18n.TaktLanguage"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktLanguageCreateDtoValidator : AbstractValidator<TaktLanguageCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktLanguageCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.LanguageName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.language.languagename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.language.languagename", 1, 100));

        RuleFor(x => x.CultureCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.language.culturecode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.language.culturecode", 1, 20));

        RuleFor(x => x.NativeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.language.nativename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.language.nativename", 1, 100));

        RuleFor(x => x.LanguageIcon)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.language.languageicon", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LanguageIcon));

        RuleFor(x => x.IsDefault)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.language.isdefault"));

        RuleFor(x => x.IsRtl)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.language.isrtl"));

        RuleFor(x => x.LanguageStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.language.languagestatus"));
    }
}

/// <summary>
/// Language更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktLanguageCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>LanguageId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktLanguageUpdateDtoValidator : AbstractValidator<TaktLanguageUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktLanguageUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktLanguageCreateDtoValidator(validationMessages));

        RuleFor(x => x.LanguageId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.language.languageid"));

        RuleFor(x => x.LanguageName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.language.languagename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LanguageName));

        RuleFor(x => x.CultureCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.language.culturecode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.CultureCode));

        RuleFor(x => x.NativeName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.language.nativename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativeName));

        RuleFor(x => x.LanguageIcon)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.language.languageicon", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LanguageIcon));
    }
}
