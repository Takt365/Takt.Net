// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCorrectionValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceCorrection DTO 验证器（根据实体 TaktAttendanceCorrection 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;

namespace Takt.Application.Validators.HumanResource.AttendanceLeave;

/// <summary>
/// AttendanceCorrection创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceCorrection"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAttendanceCorrectionCreateDtoValidator : AbstractValidator<TaktAttendanceCorrectionCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceCorrectionCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.CorrectionKind)
            .InclusiveBetween(1, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.attendancecorrection.correctionkind"));

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendancecorrection.reason"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.attendancecorrection.reason", 1, 500));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.HandlingBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.handlingby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingBy));

        RuleFor(x => x.HandlingComment)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.handlingcomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingComment));

        RuleFor(x => x.ApprovalStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.attendancecorrection.approvalstatus"));
    }
}

/// <summary>
/// AttendanceCorrection更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAttendanceCorrectionCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AttendanceCorrectionId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAttendanceCorrectionUpdateDtoValidator : AbstractValidator<TaktAttendanceCorrectionUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceCorrectionUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAttendanceCorrectionCreateDtoValidator(validationMessages));

        RuleFor(x => x.AttendanceCorrectionId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.attendancecorrection.attendancecorrectionid"));

        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.HandlingBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.handlingby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingBy));

        RuleFor(x => x.HandlingComment)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.attendancecorrection.handlingcomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingComment));
    }
}
