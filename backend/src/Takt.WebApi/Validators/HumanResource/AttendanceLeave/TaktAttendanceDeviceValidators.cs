// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceDeviceValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceDevice DTO 验证器（根据实体 TaktAttendanceDevice 自动生成）
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
/// AttendanceDevice创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceDevice"/> 字段对齐）。
/// </summary>
public class TaktAttendanceDeviceCreateDtoValidator : AbstractValidator<TaktAttendanceDeviceCreateDto>
{
    public TaktAttendanceDeviceCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DeviceCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancedevice.devicecode"))
            .Length(1, 64).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancedevice.devicecode", 1, 64));

        RuleFor(x => x.DeviceName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancedevice.devicename"))
            .Length(1, 128).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancedevice.devicename", 1, 128));

        RuleFor(x => x.DeviceType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancedevice.devicetype"))
            .Length(1, 64).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancedevice.devicetype", 1, 64));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.manufacturer", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.IpAddress)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.ipaddress", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.IpAddress));

        RuleFor(x => x.DeviceModel)
            .MaximumLength(128).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.devicemodel", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceModel));

        RuleFor(x => x.ApiSecret)
            .MaximumLength(256).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.apisecret", 256))
            .When(x => !string.IsNullOrWhiteSpace(x.ApiSecret));

        RuleFor(x => x.DeviceStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendancedevice.devicestatus"));
    }
}

/// <summary>
/// AttendanceDevice更新 DTO 验证器。
/// </summary>
public class TaktAttendanceDeviceUpdateDtoValidator : AbstractValidator<TaktAttendanceDeviceUpdateDto>
{
    public TaktAttendanceDeviceUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAttendanceDeviceCreateDtoValidator(localizer));

        RuleFor(x => x.AttendanceDeviceId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancedevice.attendancedeviceid"));

        RuleFor(x => x.DeviceCode)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.devicecode", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceCode));

        RuleFor(x => x.DeviceName)
            .MaximumLength(128).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.devicename", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceName));

        RuleFor(x => x.DeviceType)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.devicetype", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceType));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.manufacturer", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.IpAddress)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.ipaddress", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.IpAddress));

        RuleFor(x => x.DeviceModel)
            .MaximumLength(128).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.devicemodel", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceModel));

        RuleFor(x => x.ApiSecret)
            .MaximumLength(256).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancedevice.apisecret", 256))
            .When(x => !string.IsNullOrWhiteSpace(x.ApiSecret));
    }
}
