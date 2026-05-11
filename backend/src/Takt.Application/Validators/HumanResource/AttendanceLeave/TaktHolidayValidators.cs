// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktHolidayValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Holiday DTO 验证器（根据实体 TaktHoliday 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;

namespace Takt.Application.Validators.HumanResource.AttendanceLeave;

/// <summary>
/// Holiday创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktHoliday"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktHolidayCreateDtoValidator : AbstractValidator<TaktHolidayCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktHolidayCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.Region)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.holiday.region"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.holiday.region", 1, 10));

        RuleFor(x => x.HolidayName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.holiday.holidayname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.holiday.holidayname", 1, 100));

        RuleFor(x => x.HolidayType)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.holiday.holidaytype"));

        RuleFor(x => x.IsWorkingDay)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.holiday.isworkingday"));

        RuleFor(x => x.HolidayGreeting)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.holiday.holidaygreeting"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.holiday.holidaygreeting", 1, 200));

        RuleFor(x => x.HolidayQuote)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.holiday.holidayquote"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.holiday.holidayquote", 1, 500));

        RuleFor(x => x.HolidayTheme)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.holiday.holidaytheme"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.holiday.holidaytheme", 1, 20));
    }
}

/// <summary>
/// Holiday更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktHolidayCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>HolidayId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktHolidayUpdateDtoValidator : AbstractValidator<TaktHolidayUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktHolidayUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktHolidayCreateDtoValidator(validationMessages));

        RuleFor(x => x.HolidayId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.holiday.holidayid"));

        RuleFor(x => x.Region)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.holiday.region", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.Region));

        RuleFor(x => x.HolidayName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.holiday.holidayname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.HolidayName));

        RuleFor(x => x.HolidayGreeting)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.holiday.holidaygreeting", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.HolidayGreeting));

        RuleFor(x => x.HolidayQuote)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.holiday.holidayquote", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HolidayQuote));

        RuleFor(x => x.HolidayTheme)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.holiday.holidaytheme", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.HolidayTheme));
    }
}
