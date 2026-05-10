// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.Vocabulary
// 文件名称：TaktVocabularyValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Vocabulary DTO 验证器（根据实体 TaktVocabulary 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Vocabulary;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Tasks.Vocabulary;

/// <summary>
/// Vocabulary创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Vocabulary.TaktVocabulary"/> 字段对齐）。
/// </summary>
public class TaktVocabularyCreateDtoValidator : AbstractValidator<TaktVocabularyCreateDto>
{
    public TaktVocabularyCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.WordText)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.vocabulary.wordtext"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.vocabulary.wordtext", 1, 100));

        RuleFor(x => x.FilterLevel)
            .InclusiveBetween(1, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.vocabulary.filterlevel"));

        RuleFor(x => x.ReplaceText)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.vocabulary.replacetext", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ReplaceText));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.vocabulary.status"));
    }
}

/// <summary>
/// Vocabulary更新 DTO 验证器。
/// </summary>
public class TaktVocabularyUpdateDtoValidator : AbstractValidator<TaktVocabularyUpdateDto>
{
    public TaktVocabularyUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktVocabularyCreateDtoValidator(localizer));

        RuleFor(x => x.VocabularyId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.vocabulary.vocabularyid"));

        RuleFor(x => x.WordText)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.vocabulary.wordtext", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WordText));

        RuleFor(x => x.ReplaceText)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.vocabulary.replacetext", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ReplaceText));
    }
}
