// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCorrectionValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceCorrection DTO 验证器（根据实体 TaktAttendanceCorrection 自动生成）
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
/// AttendanceCorrection创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceCorrection"/> 字段对齐）。
/// </summary>
public class TaktAttendanceCorrectionCreateDtoValidator : AbstractValidator<TaktAttendanceCorrectionCreateDto>
{
    public TaktAttendanceCorrectionCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CorrectionKind)
            .InclusiveBetween(1, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendancecorrection.correctionkind"));

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancecorrection.reason"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancecorrection.reason", 1, 500));

        RuleFor(x => x.ApprovalStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendancecorrection.approvalstatus"));
    }
}

/// <summary>
/// AttendanceCorrection更新 DTO 验证器。
/// </summary>
public class TaktAttendanceCorrectionUpdateDtoValidator : AbstractValidator<TaktAttendanceCorrectionUpdateDto>
{
    public TaktAttendanceCorrectionUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAttendanceCorrectionCreateDtoValidator(localizer));

        RuleFor(x => x.AttendanceCorrectionId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancecorrection.attendancecorrectionid"));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));
    }
}
