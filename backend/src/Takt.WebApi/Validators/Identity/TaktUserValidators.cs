// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Identity
// 文件名称：TaktUserValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：User DTO 验证器（根据实体 TaktUser 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Identity;

/// <summary>
/// User创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktUser"/> 字段对齐）。
/// </summary>
public class TaktUserCreateDtoValidator : AbstractValidator<TaktUserCreateDto>
{
    public TaktUserCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.UserType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.user.usertype"));

        RuleFor(x => x.UserEmail)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternEmail", "entity.user.useremail"))
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.useremail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.UserEmail));

        RuleFor(x => x.UserPhone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternPhone", "entity.user.userphone"))
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.userphone", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.UserPhone));

        RuleFor(x => x.PasswordHash)
            .Must(TaktRegexHelper.IsValidPassword).WithMessage(TaktValidationMessages.PatternPasswordStrong(localizer, "entity.user.passwordhash"))
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.passwordhash", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PasswordHash));

        RuleFor(x => x.LockReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.lockreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LockReason));

        RuleFor(x => x.LockBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.lockby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LockBy));

        RuleFor(x => x.UserStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.user.userstatus"));
    }
}

/// <summary>
/// User更新 DTO 验证器。
/// </summary>
public class TaktUserUpdateDtoValidator : AbstractValidator<TaktUserUpdateDto>
{
    public TaktUserUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktUserCreateDtoValidator(localizer));

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.user.userid"));

        RuleFor(x => x.UserEmail)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternEmail", "entity.user.useremail"))
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.useremail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.UserEmail));

        RuleFor(x => x.UserPhone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternPhone", "entity.user.userphone"))
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.userphone", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.UserPhone));

        RuleFor(x => x.PasswordHash)
            .Must(TaktRegexHelper.IsValidPassword).WithMessage(TaktValidationMessages.PatternPasswordStrong(localizer, "entity.user.passwordhash"))
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.passwordhash", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PasswordHash));

        RuleFor(x => x.LockReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.lockreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LockReason));

        RuleFor(x => x.LockBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.lockby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LockBy));
    }
}
