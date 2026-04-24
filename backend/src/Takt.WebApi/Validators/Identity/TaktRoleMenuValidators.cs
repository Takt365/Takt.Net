// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Identity
// 文件名称：TaktRoleMenuValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：RoleMenu DTO 验证器（根据实体 TaktRoleMenu 自动生成）
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
/// RoleMenu创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktRoleMenu"/> 字段对齐）。
/// </summary>
public class TaktRoleMenuCreateDtoValidator : AbstractValidator<TaktRoleMenuCreateDto>
{
    public TaktRoleMenuCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
    }
}

/// <summary>
/// RoleMenu更新 DTO 验证器。
/// </summary>
public class TaktRoleMenuUpdateDtoValidator : AbstractValidator<TaktRoleMenuUpdateDto>
{
    public TaktRoleMenuUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktRoleMenuCreateDtoValidator(localizer));

        RuleFor(x => x.RoleMenuId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.rolemenu.rolemenuid"));

    }
}
