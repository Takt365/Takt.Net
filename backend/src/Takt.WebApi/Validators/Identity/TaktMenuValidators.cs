// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Identity
// 文件名称：TaktMenuValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Menu DTO 验证器（根据实体 TaktMenu 自动生成）
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
/// Menu创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktMenu"/> 字段对齐）。
/// </summary>
public class TaktMenuCreateDtoValidator : AbstractValidator<TaktMenuCreateDto>
{
    public TaktMenuCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MenuName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.menuname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.menu.menuname", 1, 100));

        RuleFor(x => x.MenuCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.menucode"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.menu.menucode", 1, 200));

        RuleFor(x => x.MenuL10nKey)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.menul10nkey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuL10nKey));

        RuleFor(x => x.Path)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.path", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Path));

        RuleFor(x => x.Component)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.component", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Component));

        RuleFor(x => x.MenuIcon)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.menuicon", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuIcon));

        RuleFor(x => x.MenuType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.menutype"));

        RuleFor(x => x.Permission)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.permission", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Permission));

        RuleFor(x => x.IsVisible)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.isvisible"));

        RuleFor(x => x.IsCache)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.iscache"));

        RuleFor(x => x.IsExternal)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.isexternal"));

        RuleFor(x => x.LinkUrl)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.linkurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkUrl));

        RuleFor(x => x.MenuStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.menu.menustatus"));
    }
}

/// <summary>
/// Menu更新 DTO 验证器。
/// </summary>
public class TaktMenuUpdateDtoValidator : AbstractValidator<TaktMenuUpdateDto>
{
    public TaktMenuUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktMenuCreateDtoValidator(localizer));

        RuleFor(x => x.MenuId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.menu.menuid"));

        RuleFor(x => x.MenuName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.menuname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuName));

        RuleFor(x => x.MenuCode)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.menucode", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuCode));

        RuleFor(x => x.MenuL10nKey)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.menul10nkey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuL10nKey));

        RuleFor(x => x.Path)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.path", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Path));

        RuleFor(x => x.Component)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.component", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Component));

        RuleFor(x => x.MenuIcon)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.menuicon", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuIcon));

        RuleFor(x => x.Permission)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.permission", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Permission));

        RuleFor(x => x.LinkUrl)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.menu.linkurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkUrl));
    }
}
