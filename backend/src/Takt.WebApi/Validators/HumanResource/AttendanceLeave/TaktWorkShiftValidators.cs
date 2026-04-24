// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktWorkShiftValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：WorkShift DTO 验证器（根据实体 TaktWorkShift 自动生成）
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
/// WorkShift创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktWorkShift"/> 字段对齐）。
/// </summary>
public class TaktWorkShiftCreateDtoValidator : AbstractValidator<TaktWorkShiftCreateDto>
{
    public TaktWorkShiftCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ShiftCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.workshift.shiftcode"))
            .Length(1, 64).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.workshift.shiftcode", 1, 64));

        RuleFor(x => x.ShiftName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.workshift.shiftname"))
            .Length(1, 128).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.workshift.shiftname", 1, 128));

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.workshift.starttime"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.workshift.starttime", 1, 8));

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.workshift.endtime"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.workshift.endtime", 1, 8));

        RuleFor(x => x.CrossMidnight)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.workshift.crossmidnight"));
    }
}

/// <summary>
/// WorkShift更新 DTO 验证器。
/// </summary>
public class TaktWorkShiftUpdateDtoValidator : AbstractValidator<TaktWorkShiftUpdateDto>
{
    public TaktWorkShiftUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktWorkShiftCreateDtoValidator(localizer));

        RuleFor(x => x.WorkShiftId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.workshift.workshiftid"));

        RuleFor(x => x.ShiftCode)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.workshift.shiftcode", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.ShiftCode));

        RuleFor(x => x.ShiftName)
            .MaximumLength(128).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.workshift.shiftname", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.ShiftName));

        RuleFor(x => x.StartTime)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.workshift.starttime", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.StartTime));

        RuleFor(x => x.EndTime)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.workshift.endtime", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.EndTime));
    }
}
