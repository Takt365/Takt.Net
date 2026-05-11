// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceExceptionValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AttendanceException DTO 验证器（根据实体 TaktAttendanceException 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;

namespace Takt.Application.Validators.HumanResource.AttendanceLeave;

/// <summary>
/// AttendanceException创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktAttendanceException"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAttendanceExceptionCreateDtoValidator : AbstractValidator<TaktAttendanceExceptionCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceExceptionCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ExceptionType)
            .InclusiveBetween(1, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.attendanceexception.exceptiontype"));

        RuleFor(x => x.Summary)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.attendanceexception.summary"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.attendanceexception.summary", 1, 500));

        RuleFor(x => x.HandleStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.attendanceexception.handlestatus"));
    }
}

/// <summary>
/// AttendanceException更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAttendanceExceptionCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AttendanceExceptionId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAttendanceExceptionUpdateDtoValidator : AbstractValidator<TaktAttendanceExceptionUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAttendanceExceptionUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAttendanceExceptionCreateDtoValidator(validationMessages));

        RuleFor(x => x.AttendanceExceptionId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.attendanceexception.attendanceexceptionid"));

        RuleFor(x => x.Summary)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.attendanceexception.summary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Summary));
    }
}
