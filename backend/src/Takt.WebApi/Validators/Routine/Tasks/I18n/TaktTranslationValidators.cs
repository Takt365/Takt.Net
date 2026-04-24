// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.I18n
// 文件名称：TaktTranslationValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Translation DTO 验证器（根据实体 TaktTranslation 自动生成）
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
/// Translation创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.I18n.TaktTranslation"/> 字段对齐）。
/// </summary>
public class TaktTranslationCreateDtoValidator : AbstractValidator<TaktTranslationCreateDto>
{
    public TaktTranslationCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CultureCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.translation.culturecode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.translation.culturecode", 1, 20));

        RuleFor(x => x.ResourceKey)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.translation.resourcekey"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.translation.resourcekey", 1, 200));

        RuleFor(x => x.TranslationValue)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.translation.translationvalue"))
            .Length(1, 2000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.translation.translationvalue", 1, 2000));

        RuleFor(x => x.ResourceType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.translation.resourcetype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.translation.resourcetype", 1, 50));

        RuleFor(x => x.ResourceGroup)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.translation.resourcegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResourceGroup));
    }
}

/// <summary>
/// Translation更新 DTO 验证器。
/// </summary>
public class TaktTranslationUpdateDtoValidator : AbstractValidator<TaktTranslationUpdateDto>
{
    public TaktTranslationUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTranslationCreateDtoValidator(localizer));

        RuleFor(x => x.TranslationId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.translation.translationid"));

        RuleFor(x => x.CultureCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.translation.culturecode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.CultureCode));

        RuleFor(x => x.ResourceKey)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.translation.resourcekey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ResourceKey));

        RuleFor(x => x.TranslationValue)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.translation.translationvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TranslationValue));

        RuleFor(x => x.ResourceType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.translation.resourcetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResourceType));

        RuleFor(x => x.ResourceGroup)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.translation.resourcegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResourceGroup));
    }
}
