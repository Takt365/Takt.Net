// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktWorkShiftValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：WorkShift DTO 验证器（根据实体 TaktWorkShift 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;

namespace Takt.Application.Validators.HumanResource.AttendanceLeave;

/// <summary>
/// WorkShift创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktWorkShift"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktWorkShiftCreateDtoValidator : AbstractValidator<TaktWorkShiftCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktWorkShiftCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ShiftCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.workshift.shiftcode"))
            .Length(1, 64).WithMessage(_validationMessages.LengthBetween("entity.workshift.shiftcode", 1, 64));

        RuleFor(x => x.ShiftName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.workshift.shiftname"))
            .Length(1, 128).WithMessage(_validationMessages.LengthBetween("entity.workshift.shiftname", 1, 128));

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.workshift.starttime"))
            .Length(1, 8).WithMessage(_validationMessages.LengthBetween("entity.workshift.starttime", 1, 8));

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.workshift.endtime"))
            .Length(1, 8).WithMessage(_validationMessages.LengthBetween("entity.workshift.endtime", 1, 8));

        RuleFor(x => x.CrossMidnight)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.workshift.crossmidnight"));
    }
}

/// <summary>
/// WorkShift更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktWorkShiftCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>WorkShiftId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktWorkShiftUpdateDtoValidator : AbstractValidator<TaktWorkShiftUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktWorkShiftUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktWorkShiftCreateDtoValidator(validationMessages));

        RuleFor(x => x.WorkShiftId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.workshift.workshiftid"));

        RuleFor(x => x.ShiftCode)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.workshift.shiftcode", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.ShiftCode));

        RuleFor(x => x.ShiftName)
            .MaximumLength(128).WithMessage(_validationMessages.LengthMax("entity.workshift.shiftname", 128))
            .When(x => !string.IsNullOrWhiteSpace(x.ShiftName));

        RuleFor(x => x.StartTime)
            .MaximumLength(8).WithMessage(_validationMessages.LengthMax("entity.workshift.starttime", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.StartTime));

        RuleFor(x => x.EndTime)
            .MaximumLength(8).WithMessage(_validationMessages.LengthMax("entity.workshift.endtime", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.EndTime));
    }
}
