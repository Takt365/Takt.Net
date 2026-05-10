// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeItemValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：OvertimeItem DTO 验证器（根据实体 TaktOvertimeItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.AttendanceLeave;

/// <summary>
/// OvertimeItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktOvertimeItem"/> 字段对齐）。
/// </summary>
public class TaktOvertimeItemCreateDtoValidator : AbstractValidator<TaktOvertimeItemCreateDto>
{
    public TaktOvertimeItemCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.EmployeeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.overtimeitem.employeename"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.overtimeitem.employeename", 1, 50));
    }
}

/// <summary>
/// OvertimeItem更新 DTO 验证器。
/// </summary>
public class TaktOvertimeItemUpdateDtoValidator : AbstractValidator<TaktOvertimeItemUpdateDto>
{
    public TaktOvertimeItemUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktOvertimeItemCreateDtoValidator(localizer));

        RuleFor(x => x.OvertimeItemId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.overtimeitem.overtimeitemid"));

        RuleFor(x => x.EmployeeName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtimeitem.employeename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EmployeeName));
    }
}
