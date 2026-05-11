// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ApsScheduleItem DTO 验证器（根据实体 TaktApsScheduleItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;

namespace Takt.Application.Validators.Logistics.Manufacturing.Scheduling;

/// <summary>
/// ApsScheduleItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Scheduling.TaktApsScheduleItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktApsScheduleItemCreateDtoValidator : AbstractValidator<TaktApsScheduleItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktApsScheduleItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.WorkOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.apsscheduleitem.workordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.apsscheduleitem.workordercode", 1, 50));

        RuleFor(x => x.ProductCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.apsscheduleitem.productcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.apsscheduleitem.productcode", 1, 50));

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.apsscheduleitem.productname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.apsscheduleitem.productname", 1, 200));

        RuleFor(x => x.WorkCenterCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsscheduleitem.workcentercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenterCode));

        RuleFor(x => x.WorkCenterName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.apsscheduleitem.workcentername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenterName));

        RuleFor(x => x.ProcessCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.apsscheduleitem.processcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.apsscheduleitem.processcode", 1, 50));

        RuleFor(x => x.ProcessName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.apsscheduleitem.processname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.apsscheduleitem.processname", 1, 200));

        RuleFor(x => x.ProcessStandardSTUnit)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.apsscheduleitem.processstandardstunit"));

        RuleFor(x => x.ProcessStatus)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.apsscheduleitem.processstatus"));

        RuleFor(x => x.Priority)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.apsscheduleitem.priority"));
    }
}

/// <summary>
/// ApsScheduleItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktApsScheduleItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ApsScheduleItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktApsScheduleItemUpdateDtoValidator : AbstractValidator<TaktApsScheduleItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktApsScheduleItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktApsScheduleItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.ApsScheduleItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.apsscheduleitem.apsscheduleitemid"));

        RuleFor(x => x.WorkOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsscheduleitem.workordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkOrderCode));

        RuleFor(x => x.ProductCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsscheduleitem.productcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductCode));

        RuleFor(x => x.ProductName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.apsscheduleitem.productname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductName));

        RuleFor(x => x.WorkCenterCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsscheduleitem.workcentercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenterCode));

        RuleFor(x => x.WorkCenterName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.apsscheduleitem.workcentername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenterName));

        RuleFor(x => x.ProcessCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsscheduleitem.processcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessCode));

        RuleFor(x => x.ProcessName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.apsscheduleitem.processname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessName));
    }
}
