// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Identity
// 文件名称：TaktUserValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：User DTO 验证器（根据实体 TaktUser 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Identity;

namespace Takt.Application.Validators.Identity;

/// <summary>
/// User创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktUser"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktUserCreateDtoValidator : AbstractValidator<TaktUserCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktUserCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.UserType)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.user.usertype"));

        RuleFor(x => x.UserEmail)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(_validationMessages.Pattern("validation.patternEmail", "entity.user.useremail"))
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.user.useremail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.UserEmail));

        RuleFor(x => x.UserPhone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(_validationMessages.Pattern("validation.patternPhone", "entity.user.userphone"))
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.user.userphone", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.UserPhone));

        RuleFor(x => x.PasswordHash)
            .Must(TaktRegexHelper.IsValidPassword).WithMessage(_validationMessages.PatternPasswordStrong("entity.user.passwordhash"))
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.user.passwordhash", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PasswordHash));

        RuleFor(x => x.LockReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.user.lockreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LockReason));

        RuleFor(x => x.LockBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.user.lockby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LockBy));

        RuleFor(x => x.UserStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.user.userstatus"));
    }
}

/// <summary>
/// User更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktUserCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>UserId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktUserUpdateDtoValidator : AbstractValidator<TaktUserUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktUserUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktUserCreateDtoValidator(validationMessages));

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.user.userid"));

        RuleFor(x => x.UserEmail)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(_validationMessages.Pattern("validation.patternEmail", "entity.user.useremail"))
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.user.useremail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.UserEmail));

        RuleFor(x => x.UserPhone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(_validationMessages.Pattern("validation.patternPhone", "entity.user.userphone"))
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.user.userphone", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.UserPhone));

        RuleFor(x => x.PasswordHash)
            .Must(TaktRegexHelper.IsValidPassword).WithMessage(_validationMessages.PatternPasswordStrong("entity.user.passwordhash"))
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.user.passwordhash", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PasswordHash));

        RuleFor(x => x.LockReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.user.lockreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LockReason));

        RuleFor(x => x.LockBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.user.lockby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LockBy));
    }
}
