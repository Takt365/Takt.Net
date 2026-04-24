// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.WordFilter
// 文件名称：TaktSensitiveWordsValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SensitiveWords DTO 验证器（根据实体 TaktSensitiveWords 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.WordFilter;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Tasks.WordFilter;

/// <summary>
/// SensitiveWords创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.WordFilter.TaktSensitiveWords"/> 字段对齐）。
/// </summary>
public class TaktSensitiveWordsCreateDtoValidator : AbstractValidator<TaktSensitiveWordsCreateDto>
{
    public TaktSensitiveWordsCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.WordText)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.sensitivewords.wordtext"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.sensitivewords.wordtext", 1, 100));

        RuleFor(x => x.FilterLevel)
            .InclusiveBetween(1, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.sensitivewords.filterlevel"));

        RuleFor(x => x.ReplaceText)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.sensitivewords.replacetext", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ReplaceText));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.sensitivewords.status"));
    }
}

/// <summary>
/// SensitiveWords更新 DTO 验证器。
/// </summary>
public class TaktSensitiveWordsUpdateDtoValidator : AbstractValidator<TaktSensitiveWordsUpdateDto>
{
    public TaktSensitiveWordsUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSensitiveWordsCreateDtoValidator(localizer));

        RuleFor(x => x.SensitiveWordsId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.sensitivewords.sensitivewordsid"));

        RuleFor(x => x.WordText)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.sensitivewords.wordtext", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WordText));

        RuleFor(x => x.ReplaceText)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.sensitivewords.replacetext", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ReplaceText));
    }
}
