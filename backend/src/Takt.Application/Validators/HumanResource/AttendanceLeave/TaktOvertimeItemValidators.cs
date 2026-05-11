// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：OvertimeItem DTO 验证器（根据实体 TaktOvertimeItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.AttendanceLeave;

namespace Takt.Application.Validators.HumanResource.AttendanceLeave;

/// <summary>
/// OvertimeItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.AttendanceLeave.TaktOvertimeItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktOvertimeItemCreateDtoValidator : AbstractValidator<TaktOvertimeItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktOvertimeItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.EmployeeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.overtimeitem.employeename"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.overtimeitem.employeename", 1, 50));
    }
}

/// <summary>
/// OvertimeItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktOvertimeItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>OvertimeItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktOvertimeItemUpdateDtoValidator : AbstractValidator<TaktOvertimeItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktOvertimeItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktOvertimeItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.OvertimeItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.overtimeitem.overtimeitemid"));

        RuleFor(x => x.EmployeeName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.overtimeitem.employeename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EmployeeName));
    }
}
