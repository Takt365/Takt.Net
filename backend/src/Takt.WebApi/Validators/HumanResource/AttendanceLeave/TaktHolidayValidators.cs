// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktHolidayValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Holiday DTO 验证器（根据实体 TaktHoliday 自动生成）
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
/// Holiday创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktHoliday"/> 字段对齐）。
/// </summary>
public class TaktHolidayCreateDtoValidator : AbstractValidator<TaktHolidayCreateDto>
{
    public TaktHolidayCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.Region)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.holiday.region"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.holiday.region", 1, 10));

        RuleFor(x => x.HolidayName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.holiday.holidayname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.holiday.holidayname", 1, 100));

        RuleFor(x => x.HolidayType)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.holiday.holidaytype"));

        RuleFor(x => x.IsWorkingDay)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.holiday.isworkingday"));

        RuleFor(x => x.HolidayGreeting)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.holiday.holidaygreeting"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.holiday.holidaygreeting", 1, 200));

        RuleFor(x => x.HolidayQuote)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.holiday.holidayquote"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.holiday.holidayquote", 1, 500));

        RuleFor(x => x.HolidayTheme)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.holiday.holidaytheme"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.holiday.holidaytheme", 1, 20));
    }
}

/// <summary>
/// Holiday更新 DTO 验证器。
/// </summary>
public class TaktHolidayUpdateDtoValidator : AbstractValidator<TaktHolidayUpdateDto>
{
    public TaktHolidayUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktHolidayCreateDtoValidator(localizer));

        RuleFor(x => x.HolidayId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.holiday.holidayid"));

        RuleFor(x => x.Region)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.holiday.region", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.Region));

        RuleFor(x => x.HolidayName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.holiday.holidayname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.HolidayName));

        RuleFor(x => x.HolidayGreeting)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.holiday.holidaygreeting", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.HolidayGreeting));

        RuleFor(x => x.HolidayQuote)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.holiday.holidayquote", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HolidayQuote));

        RuleFor(x => x.HolidayTheme)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.holiday.holidaytheme", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.HolidayTheme));
    }
}
