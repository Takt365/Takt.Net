// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Organization
// 文件名称：TaktDeptDelegateValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：DeptDelegate DTO 验证器（根据实体 TaktDeptDelegate 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Organization;

/// <summary>
/// DeptDelegate创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Organization.TaktDeptDelegate"/> 字段对齐）。
/// </summary>
public class TaktDeptDelegateCreateDtoValidator : AbstractValidator<TaktDeptDelegateCreateDto>
{
    public TaktDeptDelegateCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
    }
}

/// <summary>
/// DeptDelegate更新 DTO 验证器。
/// </summary>
public class TaktDeptDelegateUpdateDtoValidator : AbstractValidator<TaktDeptDelegateUpdateDto>
{
    public TaktDeptDelegateUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktDeptDelegateCreateDtoValidator(localizer));

        RuleFor(x => x.DeptDelegateId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.deptdelegate.deptdelegateid"));

    }
}
