// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSettingValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceSetting DTO 验证器（根据实体 TaktAttendanceSetting 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;

namespace Takt.Application.Validators.HumanResource.AttendanceLeave;

/// <summary>
/// AttendanceSetting创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceSetting"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAttendanceSettingCreateDtoValidator : AbstractValidator<TaktAttendanceSettingCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceSettingCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.SettingCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendancesetting.settingcode"))
            .Length(1, 64).WithMessage(_validationMessages.LengthBetween("entity.attendancesetting.settingcode", 1, 64));

        RuleFor(x => x.SettingName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendancesetting.settingname"))
            .Length(1, 128).WithMessage(_validationMessages.LengthBetween("entity.attendancesetting.settingname", 1, 128));

        RuleFor(x => x.WorkStartTime)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendancesetting.workstarttime"))
            .Length(1, 8).WithMessage(_validationMessages.LengthBetween("entity.attendancesetting.workstarttime", 1, 8));

        RuleFor(x => x.WorkEndTime)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendancesetting.workendtime"))
            .Length(1, 8).WithMessage(_validationMessages.LengthBetween("entity.attendancesetting.workendtime", 1, 8));

        RuleFor(x => x.IsDefault)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.attendancesetting.isdefault"));
    }
}

/// <summary>
/// AttendanceSetting更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAttendanceSettingCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AttendanceSettingId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAttendanceSettingUpdateDtoValidator : AbstractValidator<TaktAttendanceSettingUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceSettingUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAttendanceSettingCreateDtoValidator(validationMessages));

        RuleFor(x => x.AttendanceSettingId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.attendancesetting.attendancesettingid"));

        RuleFor(x => x.SettingCode)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancesetting.settingcode", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingCode));

        RuleFor(x => x.SettingName)
            .MaximumLength(128).WithMessage(_validationMessages.LengthMax("entity.attendancesetting.settingname", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingName));

        RuleFor(x => x.WorkStartTime)
            .MaximumLength(8).WithMessage(_validationMessages.LengthMax("entity.attendancesetting.workstarttime", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkStartTime));

        RuleFor(x => x.WorkEndTime)
            .MaximumLength(8).WithMessage(_validationMessages.LengthMax("entity.attendancesetting.workendtime", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkEndTime));
    }
}
