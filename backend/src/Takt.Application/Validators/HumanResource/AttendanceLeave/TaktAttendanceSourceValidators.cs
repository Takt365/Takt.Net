// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSourceValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceSource DTO 验证器（根据实体 TaktAttendanceSource 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;

namespace Takt.Application.Validators.HumanResource.AttendanceLeave;

/// <summary>
/// AttendanceSource创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceSource"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAttendanceSourceCreateDtoValidator : AbstractValidator<TaktAttendanceSourceCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceSourceCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.EnrollNumber)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendancesource.enrollnumber"))
            .Length(1, 64).WithMessage(_validationMessages.LengthBetween("entity.attendancesource.enrollnumber", 1, 64));

        RuleFor(x => x.VerifyMode)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.attendancesource.verifymode"));

        RuleFor(x => x.ExternalRecordKey)
            .MaximumLength(128).WithMessage(_validationMessages.LengthMax("entity.attendancesource.externalrecordkey", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.ExternalRecordKey));

        RuleFor(x => x.DownloadBatchNo)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancesource.downloadbatchno", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.DownloadBatchNo));
    }
}

/// <summary>
/// AttendanceSource更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAttendanceSourceCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AttendanceSourceId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAttendanceSourceUpdateDtoValidator : AbstractValidator<TaktAttendanceSourceUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceSourceUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAttendanceSourceCreateDtoValidator(validationMessages));

        RuleFor(x => x.AttendanceSourceId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.attendancesource.attendancesourceid"));

        RuleFor(x => x.EnrollNumber)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancesource.enrollnumber", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.EnrollNumber));

        RuleFor(x => x.ExternalRecordKey)
            .MaximumLength(128).WithMessage(_validationMessages.LengthMax("entity.attendancesource.externalrecordkey", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.ExternalRecordKey));

        RuleFor(x => x.DownloadBatchNo)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.attendancesource.downloadbatchno", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.DownloadBatchNo));
    }
}
