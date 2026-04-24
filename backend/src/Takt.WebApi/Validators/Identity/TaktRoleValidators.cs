// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Identity
// 文件名称：TaktRoleValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Role DTO 验证器（根据实体 TaktRole 自动生成）
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
/// Role创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktRole"/> 字段对齐）。
/// </summary>
public class TaktRoleCreateDtoValidator : AbstractValidator<TaktRoleCreateDto>
{
    public TaktRoleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.role.rolename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.role.rolename", 1, 100));

        RuleFor(x => x.RoleCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.role.rolecode"))
            .Must(v => TaktRegexHelper.IsMatch(TaktRegexHelper.RoleCode, v)).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternRoleCode", "entity.role.rolecode"));

        RuleFor(x => x.DataScope)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.role.datascope"));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.role.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));

        RuleFor(x => x.RoleStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.role.rolestatus"));
    }
}

/// <summary>
/// Role更新 DTO 验证器。
/// </summary>
public class TaktRoleUpdateDtoValidator : AbstractValidator<TaktRoleUpdateDto>
{
    public TaktRoleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktRoleCreateDtoValidator(localizer));

        RuleFor(x => x.RoleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.role.roleid"));

        RuleFor(x => x.RoleName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.role.rolename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RoleName));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.role.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));
    }
}
