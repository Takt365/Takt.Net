// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendancePunchValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendancePunch DTO 验证器（根据实体 TaktAttendancePunch 自动生成）
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
/// AttendancePunch创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendancePunch"/> 字段对齐）。
/// </summary>
public class TaktAttendancePunchCreateDtoValidator : AbstractValidator<TaktAttendancePunchCreateDto>
{
    public TaktAttendancePunchCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PunchType)
            .InclusiveBetween(1, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendancepunch.punchtype"));

        RuleFor(x => x.PunchSource)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendancepunch.punchsource"));

        RuleFor(x => x.PunchAddress)
            .MaximumLength(256).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancepunch.punchaddress", 256))
            .When(x => !string.IsNullOrWhiteSpace(x.PunchAddress));
    }
}

/// <summary>
/// AttendancePunch更新 DTO 验证器。
/// </summary>
public class TaktAttendancePunchUpdateDtoValidator : AbstractValidator<TaktAttendancePunchUpdateDto>
{
    public TaktAttendancePunchUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAttendancePunchCreateDtoValidator(localizer));

        RuleFor(x => x.AttendancePunchId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancepunch.attendancepunchid"));

        RuleFor(x => x.PunchAddress)
            .MaximumLength(256).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancepunch.punchaddress", 256))
            .When(x => !string.IsNullOrWhiteSpace(x.PunchAddress));
    }
}
