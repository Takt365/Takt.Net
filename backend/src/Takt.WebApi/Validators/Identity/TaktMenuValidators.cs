using FluentValidation;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Identity;

/// <summary>
/// 菜单创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktMenu"/> 字段长度及 TaktRegexHelper.MenuCode / Permission 对齐）。
/// </summary>
public class TaktMenuCreateDtoValidator : AbstractValidator<TaktMenuCreateDto>
{
    public TaktMenuCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MenuName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.name"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.menu.name", 1, 100));

        RuleFor(x => x.MenuCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.code"))
            .Must(v => TaktRegexHelper.IsMatch(TaktRegexHelper.MenuCode, v))
            .WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternMenuCode", "entity.menu.code"));

        RuleFor(x => x.ParentId)
            .GreaterThanOrEqualTo(0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.parentid"));

        RuleFor(x => x.MenuType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.type"));

        RuleFor(x => x.IsVisible)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.isvisible"));

        RuleFor(x => x.IsCache)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.iscache"));

        RuleFor(x => x.IsExternal)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.isexternal"));

        RuleFor(x => x.MenuL10nKey)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.l10nkey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuL10nKey));

        RuleFor(x => x.Path)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.path", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Path));

        RuleFor(x => x.Component)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.component", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Component));

        RuleFor(x => x.MenuIcon)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.icon", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuIcon));

        RuleFor(x => x.Permission)
            .Must(v => TaktRegexHelper.IsMatch(TaktRegexHelper.Permission, v!))
            .WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternPermission", "entity.menu.permission"))
            .When(x => !string.IsNullOrWhiteSpace(x.Permission));

        RuleFor(x => x.LinkUrl)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.linkurl"))
            .When(x => x.IsExternal == 1);

        RuleFor(x => x.LinkUrl)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.linkurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkUrl));
    }
}

/// <summary>
/// 菜单更新 DTO 验证器。
/// </summary>
public class TaktMenuUpdateDtoValidator : AbstractValidator<TaktMenuUpdateDto>
{
    public TaktMenuUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktMenuCreateDtoValidator(localizer));

        RuleFor(x => x.MenuId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.menuid"));
    }
}

/// <summary>
/// 菜单状态 DTO 验证器。
/// </summary>
public class TaktMenuStatusDtoValidator : AbstractValidator<TaktMenuStatusDto>
{
    public TaktMenuStatusDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MenuId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.menuid"));

        RuleFor(x => x.MenuStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.status"));
    }
}

/// <summary>
/// 菜单排序 DTO 验证器。
/// </summary>
public class TaktMenuOrderNumDtoValidator : AbstractValidator<TaktMenuOrderNumDto>
{
    public TaktMenuOrderNumDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MenuId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.menuid"));
    }
}

/// <summary>
/// 菜单可见性 DTO 验证器。
/// </summary>
public class TaktMenuVisibleDtoValidator : AbstractValidator<TaktMenuVisibleDto>
{
    public TaktMenuVisibleDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MenuId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.menuid"));

        RuleFor(x => x.IsVisible)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.isvisible"));
    }
}

/// <summary>
/// 菜单缓存 DTO 验证器。
/// </summary>
public class TaktMenuIsCacheDtoValidator : AbstractValidator<TaktMenuIsCacheDto>
{
    public TaktMenuIsCacheDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MenuId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.menuid"));

        RuleFor(x => x.IsCache)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.iscache"));
    }
}
