using FluentValidation;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Interfaces;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Identity;

/// <summary>
/// 登录请求 DTO 验证器。
/// </summary>
public class TaktLoginDtoValidator : AbstractValidator<TaktLoginDto>
{
    public TaktLoginDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.user.name"))
            .Length(2, 64).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.user.name", 2, 64));

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.user.password"))
            .MinimumLength(6).WithMessage(TaktValidationMessages.LengthMin(localizer, "entity.user.password", 6))
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.user.password", 64));
    }
}

/// <summary>
/// 刷新令牌请求 DTO 验证器。
/// </summary>
public class TaktRefreshTokenDtoValidator : AbstractValidator<TaktRefreshTokenDto>
{
    public TaktRefreshTokenDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.auth.refreshtoken"));
    }
}
