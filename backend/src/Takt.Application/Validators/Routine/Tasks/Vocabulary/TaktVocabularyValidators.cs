// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.Vocabulary
// 文件名称：TaktVocabularyValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Vocabulary DTO 验证器（根据实体 TaktVocabulary 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Vocabulary;

namespace Takt.Application.Validators.Routine.Tasks.Vocabulary;

/// <summary>
/// Vocabulary创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Vocabulary.TaktVocabulary"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktVocabularyCreateDtoValidator : AbstractValidator<TaktVocabularyCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktVocabularyCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.WordText)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.vocabulary.wordtext"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.vocabulary.wordtext", 1, 100));

        RuleFor(x => x.FilterLevel)
            .InclusiveBetween(1, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.vocabulary.filterlevel"));

        RuleFor(x => x.ReplaceText)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.vocabulary.replacetext", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ReplaceText));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.vocabulary.status"));
    }
}

/// <summary>
/// Vocabulary更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktVocabularyCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>VocabularyId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktVocabularyUpdateDtoValidator : AbstractValidator<TaktVocabularyUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktVocabularyUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktVocabularyCreateDtoValidator(validationMessages));

        RuleFor(x => x.VocabularyId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.vocabulary.vocabularyid"));

        RuleFor(x => x.WordText)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.vocabulary.wordtext", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WordText));

        RuleFor(x => x.ReplaceText)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.vocabulary.replacetext", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ReplaceText));
    }
}
