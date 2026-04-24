// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceExceptionValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceException DTO 验证器（根据实体 TaktAttendanceException 自动生成）
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
/// AttendanceException创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceException"/> 字段对齐）。
/// </summary>
public class TaktAttendanceExceptionCreateDtoValidator : AbstractValidator<TaktAttendanceExceptionCreateDto>
{
    public TaktAttendanceExceptionCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ExceptionType)
            .InclusiveBetween(1, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendanceexception.exceptiontype"));

        RuleFor(x => x.Summary)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendanceexception.summary"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendanceexception.summary", 1, 500));

        RuleFor(x => x.HandleStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendanceexception.handlestatus"));
    }
}

/// <summary>
/// AttendanceException更新 DTO 验证器。
/// </summary>
public class TaktAttendanceExceptionUpdateDtoValidator : AbstractValidator<TaktAttendanceExceptionUpdateDto>
{
    public TaktAttendanceExceptionUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAttendanceExceptionCreateDtoValidator(localizer));

        RuleFor(x => x.AttendanceExceptionId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.attendanceexception.attendanceexceptionid"));

        RuleFor(x => x.Summary)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendanceexception.summary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Summary));
    }
}
