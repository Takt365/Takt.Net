// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeValidators.cs
// 创建时间：2026-05-10
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
        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.Reason)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.reason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.HandlingBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.handlingby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingBy));

        RuleFor(x => x.HandlingComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.handlingcomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingComment));

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

        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.Reason)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.reason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.HandlingBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.handlingby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingBy));

        RuleFor(x => x.HandlingComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.overtime.handlingcomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingComment));
    }
}
