// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Identity
// 文件名称：TaktMenuValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Menu DTO 验证器（根据实体 TaktMenu 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Identity;

namespace Takt.Application.Validators.Identity;

/// <summary>
/// Menu创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktMenu"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktMenuCreateDtoValidator : AbstractValidator<TaktMenuCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMenuCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.MenuName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.menu.menuname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.menu.menuname", 1, 100));

        RuleFor(x => x.MenuCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.menu.menucode"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.menu.menucode", 1, 200));

        RuleFor(x => x.MenuL10nKey)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.menu.menul10nkey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuL10nKey));

        RuleFor(x => x.Path)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.menu.path", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Path));

        RuleFor(x => x.Component)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.menu.component", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Component));

        RuleFor(x => x.MenuIcon)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.menu.menuicon", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuIcon));

        RuleFor(x => x.MenuType)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.menu.menutype"));

        RuleFor(x => x.Permission)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.menu.permission", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Permission));

        RuleFor(x => x.IsVisible)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.menu.isvisible"));

        RuleFor(x => x.IsCache)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.menu.iscache"));

        RuleFor(x => x.IsExternal)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.menu.isexternal"));

        RuleFor(x => x.LinkUrl)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.menu.linkurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkUrl));

        RuleFor(x => x.MenuStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.menu.menustatus"));
    }
}

/// <summary>
/// Menu更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktMenuCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>MenuId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktMenuUpdateDtoValidator : AbstractValidator<TaktMenuUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMenuUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktMenuCreateDtoValidator(validationMessages));

        RuleFor(x => x.MenuId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.menu.menuid"));

        RuleFor(x => x.MenuName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.menu.menuname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuName));

        RuleFor(x => x.MenuCode)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.menu.menucode", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuCode));

        RuleFor(x => x.MenuL10nKey)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.menu.menul10nkey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuL10nKey));

        RuleFor(x => x.Path)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.menu.path", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Path));

        RuleFor(x => x.Component)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.menu.component", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Component));

        RuleFor(x => x.MenuIcon)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.menu.menuicon", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MenuIcon));

        RuleFor(x => x.Permission)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.menu.permission", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Permission));

        RuleFor(x => x.LinkUrl)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.menu.linkurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkUrl));
    }
}
