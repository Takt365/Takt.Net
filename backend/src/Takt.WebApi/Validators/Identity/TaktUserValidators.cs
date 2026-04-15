using FluentValidation;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Identity;

/// <summary>
/// 用户创建 DTO 验证器。
/// </summary>
public class TaktUserCreateDtoValidator : AbstractValidator<TaktUserCreateDto>
{
    public TaktUserCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.user.employeeid"));

        // 登录名以关联员工编码为准，创建时由服务端写入；请求中的 UserName 可省略，不作小写用户名规则校验
        RuleFor(x => x.UserName)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.name", 20));

        RuleFor(x => x.NickName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.nickname", 200))
            .Must(v => string.IsNullOrWhiteSpace(v) || TaktRegexHelper.NickName.IsMatch(v.Trim()))
            .WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternNickName", "entity.user.nickname"));

        RuleFor(x => x.PasswordHash)
            .Must(TaktRegexHelper.IsValidPassword).WithMessage(TaktValidationMessages.PatternPasswordStrong(localizer, "entity.user.password"))
            .When(x => !string.IsNullOrWhiteSpace(x.PasswordHash));

        RuleFor(x => x.UserEmail)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternEmail", "entity.user.email"))
            .When(x => !string.IsNullOrWhiteSpace(x.UserEmail));

        RuleFor(x => x.UserPhone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternPhone", "entity.user.phone"))
            .When(x => !string.IsNullOrWhiteSpace(x.UserPhone));
    }
}

/// <summary>
/// 用户更新 DTO 验证器。
/// </summary>
public class TaktUserUpdateDtoValidator : AbstractValidator<TaktUserUpdateDto>
{
    public TaktUserUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktUserCreateDtoValidator(localizer));

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.user.userid"));
    }
}

/// <summary>
/// 用户重置密码 DTO 验证器。
/// </summary>
public class TaktUserResetPwdDtoValidator : AbstractValidator<TaktUserResetPwdDto>
{
    public TaktUserResetPwdDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.user.userid"));
    }
}

/// <summary>
/// 用户修改密码 DTO 验证器。
/// </summary>
public class TaktUserChangePwdDtoValidator : AbstractValidator<TaktUserChangePwdDto>
{
    public TaktUserChangePwdDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.user.oldpassword"));

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.user.newpassword"))
            .Must(TaktRegexHelper.IsValidPassword).WithMessage(TaktValidationMessages.PatternPasswordStrong(localizer, "entity.user.newpassword"))
            .NotEqual(x => x.OldPassword).WithMessage(TaktValidationMessages.NotEqualFields(localizer, "entity.user.newpassword", "entity.user.oldpassword"));
    }
}
