// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceDeviceValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceDevice DTO 验证器（根据实体 TaktAttendanceDevice 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;

namespace Takt.Application.Validators.HumanResource.AttendanceLeave;

/// <summary>
/// AttendanceDevice创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceDevice"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAttendanceDeviceCreateDtoValidator : AbstractValidator<TaktAttendanceDeviceCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceDeviceCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.DeviceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendancedevice.devicecode"))
            .Length(1, 64).WithMessage(_validationMessages.LengthBetween("entity.attendancedevice.devicecode", 1, 64));

        RuleFor(x => x.DeviceName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendancedevice.devicename"))
            .Length(1, 128).WithMessage(_validationMessages.LengthBetween("entity.attendancedevice.devicename", 1, 128));

        RuleFor(x => x.DeviceType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendancedevice.devicetype"))
            .Length(1, 64).WithMessage(_validationMessages.LengthBetween("entity.attendancedevice.devicetype", 1, 64));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.manufacturer", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.IpAddress)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.ipaddress", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.IpAddress));

        RuleFor(x => x.DeviceModel)
            .MaximumLength(128).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.devicemodel", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceModel));

        RuleFor(x => x.ApiSecret)
            .MaximumLength(256).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.apisecret", 256))
            .When(x => !string.IsNullOrWhiteSpace(x.ApiSecret));

        RuleFor(x => x.DeviceStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.attendancedevice.devicestatus"));
    }
}

/// <summary>
/// AttendanceDevice更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAttendanceDeviceCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AttendanceDeviceId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAttendanceDeviceUpdateDtoValidator : AbstractValidator<TaktAttendanceDeviceUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceDeviceUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAttendanceDeviceCreateDtoValidator(validationMessages));

        RuleFor(x => x.AttendanceDeviceId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.attendancedevice.attendancedeviceid"));

        RuleFor(x => x.DeviceCode)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.devicecode", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceCode));

        RuleFor(x => x.DeviceName)
            .MaximumLength(128).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.devicename", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceName));

        RuleFor(x => x.DeviceType)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.devicetype", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceType));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.manufacturer", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.IpAddress)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.ipaddress", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.IpAddress));

        RuleFor(x => x.DeviceModel)
            .MaximumLength(128).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.devicemodel", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceModel));

        RuleFor(x => x.ApiSecret)
            .MaximumLength(256).WithMessage(_validationMessages.LengthMax("entity.attendancedevice.apisecret", 256))
            .When(x => !string.IsNullOrWhiteSpace(x.ApiSecret));
    }
}
