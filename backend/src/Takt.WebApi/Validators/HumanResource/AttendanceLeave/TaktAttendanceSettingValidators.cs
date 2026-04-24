// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSettingValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceSetting DTO 验证器（根据实体 TaktAttendanceSetting 自动生成）
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
/// AttendanceSetting创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceSetting"/> 字段对齐）。
/// </summary>
public class TaktAttendanceSettingCreateDtoValidator : AbstractValidator<TaktAttendanceSettingCreateDto>
{
    public TaktAttendanceSettingCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.SettingCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancesetting.settingcode"))
            .Length(1, 64).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancesetting.settingcode", 1, 64));

        RuleFor(x => x.SettingName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancesetting.settingname"))
            .Length(1, 128).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancesetting.settingname", 1, 128));

        RuleFor(x => x.WorkStartTime)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancesetting.workstarttime"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancesetting.workstarttime", 1, 8));

        RuleFor(x => x.WorkEndTime)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancesetting.workendtime"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancesetting.workendtime", 1, 8));

        RuleFor(x => x.IsDefault)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendancesetting.isdefault"));
    }
}

/// <summary>
/// AttendanceSetting更新 DTO 验证器。
/// </summary>
public class TaktAttendanceSettingUpdateDtoValidator : AbstractValidator<TaktAttendanceSettingUpdateDto>
{
    public TaktAttendanceSettingUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAttendanceSettingCreateDtoValidator(localizer));

        RuleFor(x => x.AttendanceSettingId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancesetting.attendancesettingid"));

        RuleFor(x => x.SettingCode)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancesetting.settingcode", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingCode));

        RuleFor(x => x.SettingName)
            .MaximumLength(128).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancesetting.settingname", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingName));

        RuleFor(x => x.WorkStartTime)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancesetting.workstarttime", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkStartTime));

        RuleFor(x => x.WorkEndTime)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancesetting.workendtime", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkEndTime));
    }
}
