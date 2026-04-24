// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktLeaveValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Leave DTO 验证器（根据实体 TaktLeave 自动生成）
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
/// Leave创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktLeave"/> 字段对齐）。
/// </summary>
public class TaktLeaveCreateDtoValidator : AbstractValidator<TaktLeaveCreateDto>
{
    public TaktLeaveCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.LeaveType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.leave.leavetype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.leave.leavetype", 1, 50));

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.leave.reason"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.leave.reason", 1, 500));

        RuleFor(x => x.LeaveStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.leave.leavestatus"));
    }
}

/// <summary>
/// Leave更新 DTO 验证器。
/// </summary>
public class TaktLeaveUpdateDtoValidator : AbstractValidator<TaktLeaveUpdateDto>
{
    public TaktLeaveUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktLeaveCreateDtoValidator(localizer));

        RuleFor(x => x.LeaveId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.leave.leaveid"));

        RuleFor(x => x.LeaveType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.leave.leavetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LeaveType));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.leave.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));
    }
}
