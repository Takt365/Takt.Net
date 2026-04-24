// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Overtime DTO 验证器（根据实体 TaktOvertime 自动生成）
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
/// Overtime创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktOvertime"/> 字段对齐）。
/// </summary>
public class TaktOvertimeCreateDtoValidator : AbstractValidator<TaktOvertimeCreateDto>
{
    public TaktOvertimeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.overtime.reason"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.overtime.reason", 1, 500));

        RuleFor(x => x.OvertimeStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.overtime.overtimestatus"));
    }
}

/// <summary>
/// Overtime更新 DTO 验证器。
/// </summary>
public class TaktOvertimeUpdateDtoValidator : AbstractValidator<TaktOvertimeUpdateDto>
{
    public TaktOvertimeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktOvertimeCreateDtoValidator(localizer));

        RuleFor(x => x.OvertimeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.overtime.overtimeid"));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));
    }
}
