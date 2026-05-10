// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCorrectionValidators.cs
// 创建时间：2026-05-10
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
        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.CorrectionKind)
            .InclusiveBetween(1, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendancecorrection.correctionkind"));

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancecorrection.reason"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancecorrection.reason", 1, 500));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.HandlingBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.handlingby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingBy));

        RuleFor(x => x.HandlingComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.handlingcomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingComment));

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

        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.HandlingBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.handlingby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingBy));

        RuleFor(x => x.HandlingComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancecorrection.handlingcomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingComment));
    }
}
