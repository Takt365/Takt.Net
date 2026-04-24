// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktShiftScheduleValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ShiftSchedule DTO 验证器（根据实体 TaktShiftSchedule 自动生成）
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
/// ShiftSchedule创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktShiftSchedule"/> 字段对齐）。
/// </summary>
public class TaktShiftScheduleCreateDtoValidator : AbstractValidator<TaktShiftScheduleCreateDto>
{
    public TaktShiftScheduleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ScheduleType)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.shiftschedule.scheduletype"));
    }
}

/// <summary>
/// ShiftSchedule更新 DTO 验证器。
/// </summary>
public class TaktShiftScheduleUpdateDtoValidator : AbstractValidator<TaktShiftScheduleUpdateDto>
{
    public TaktShiftScheduleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktShiftScheduleCreateDtoValidator(localizer));

        RuleFor(x => x.ShiftScheduleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.shiftschedule.shiftscheduleid"));

    }
}
