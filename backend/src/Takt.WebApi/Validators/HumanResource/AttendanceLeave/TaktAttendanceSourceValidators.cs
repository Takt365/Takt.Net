// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSourceValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceSource DTO 验证器（根据实体 TaktAttendanceSource 自动生成）
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
/// AttendanceSource创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceSource"/> 字段对齐）。
/// </summary>
public class TaktAttendanceSourceCreateDtoValidator : AbstractValidator<TaktAttendanceSourceCreateDto>
{
    public TaktAttendanceSourceCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.EnrollNumber)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancesource.enrollnumber"))
            .Length(1, 64).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.attendancesource.enrollnumber", 1, 64));

        RuleFor(x => x.VerifyMode)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.attendancesource.verifymode"));

        RuleFor(x => x.ExternalRecordKey)
            .MaximumLength(128).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancesource.externalrecordkey", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.ExternalRecordKey));

        RuleFor(x => x.DownloadBatchNo)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancesource.downloadbatchno", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.DownloadBatchNo));
    }
}

/// <summary>
/// AttendanceSource更新 DTO 验证器。
/// </summary>
public class TaktAttendanceSourceUpdateDtoValidator : AbstractValidator<TaktAttendanceSourceUpdateDto>
{
    public TaktAttendanceSourceUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAttendanceSourceCreateDtoValidator(localizer));

        RuleFor(x => x.AttendanceSourceId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.attendancesource.attendancesourceid"));

        RuleFor(x => x.EnrollNumber)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancesource.enrollnumber", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.EnrollNumber));

        RuleFor(x => x.ExternalRecordKey)
            .MaximumLength(128).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancesource.externalrecordkey", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.ExternalRecordKey));

        RuleFor(x => x.DownloadBatchNo)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.attendancesource.downloadbatchno", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.DownloadBatchNo));
    }
}
