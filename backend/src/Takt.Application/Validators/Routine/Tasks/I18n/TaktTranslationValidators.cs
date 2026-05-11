// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.I18n
// 文件名称：TaktTranslationValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Translation DTO 验证器（根据实体 TaktTranslation 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.I18n;

namespace Takt.Application.Validators.Routine.Tasks.I18n;

/// <summary>
/// Translation创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.I18n.TaktTranslation"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktTranslationCreateDtoValidator : AbstractValidator<TaktTranslationCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTranslationCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CultureCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.translation.culturecode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.translation.culturecode", 1, 20));

        RuleFor(x => x.ResourceKey)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.translation.resourcekey"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.translation.resourcekey", 1, 200));

        RuleFor(x => x.TranslationValue)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.translation.translationvalue"))
            .Length(1, 2000).WithMessage(_validationMessages.LengthBetween("entity.translation.translationvalue", 1, 2000));

        RuleFor(x => x.ResourceType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.translation.resourcetype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.translation.resourcetype", 1, 50));

        RuleFor(x => x.ResourceGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.translation.resourcegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResourceGroup));
    }
}

/// <summary>
/// Translation更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktTranslationCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>TranslationId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktTranslationUpdateDtoValidator : AbstractValidator<TaktTranslationUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTranslationUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktTranslationCreateDtoValidator(validationMessages));

        RuleFor(x => x.TranslationId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.translation.translationid"));

        RuleFor(x => x.CultureCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.translation.culturecode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.CultureCode));

        RuleFor(x => x.ResourceKey)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.translation.resourcekey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ResourceKey));

        RuleFor(x => x.TranslationValue)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.translation.translationvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TranslationValue));

        RuleFor(x => x.ResourceType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.translation.resourcetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResourceType));

        RuleFor(x => x.ResourceGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.translation.resourcegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResourceGroup));
    }
}
