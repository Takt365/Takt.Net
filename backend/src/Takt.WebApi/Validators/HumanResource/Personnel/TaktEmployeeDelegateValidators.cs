// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeDelegateValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeDelegate DTO 验证器（根据实体 TaktEmployeeDelegate 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeDelegate创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeDelegate"/> 字段对齐）。
/// </summary>
public class TaktEmployeeDelegateCreateDtoValidator : AbstractValidator<TaktEmployeeDelegateCreateDto>
{
    public TaktEmployeeDelegateCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
    }
}

/// <summary>
/// EmployeeDelegate更新 DTO 验证器。
/// </summary>
public class TaktEmployeeDelegateUpdateDtoValidator : AbstractValidator<TaktEmployeeDelegateUpdateDto>
{
    public TaktEmployeeDelegateUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeDelegateCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeDelegateId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeedelegate.employeedelegateid"));

    }
}
