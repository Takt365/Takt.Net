// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleItemValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ApsScheduleItem DTO 验证器（根据实体 TaktApsScheduleItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Scheduling;

/// <summary>
/// ApsScheduleItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Scheduling.TaktApsScheduleItem"/> 字段对齐）。
/// </summary>
public class TaktApsScheduleItemCreateDtoValidator : AbstractValidator<TaktApsScheduleItemCreateDto>
{
    public TaktApsScheduleItemCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.WorkOrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.apsscheduleitem.workordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.apsscheduleitem.workordercode", 1, 50));

        RuleFor(x => x.ProductCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.apsscheduleitem.productcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.apsscheduleitem.productcode", 1, 50));

        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.apsscheduleitem.productname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.apsscheduleitem.productname", 1, 200));

        RuleFor(x => x.WorkCenterCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.workcentercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenterCode));

        RuleFor(x => x.WorkCenterName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.workcentername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenterName));

        RuleFor(x => x.ProcessCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.apsscheduleitem.processcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.apsscheduleitem.processcode", 1, 50));

        RuleFor(x => x.ProcessName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.apsscheduleitem.processname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.apsscheduleitem.processname", 1, 200));

        RuleFor(x => x.ProcessStandardSTUnit)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsscheduleitem.processstandardstunit"));

        RuleFor(x => x.EquipmentCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.equipmentcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentCode));

        RuleFor(x => x.EquipmentName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.equipmentname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentName));

        RuleFor(x => x.TeamCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.teamcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamCode));

        RuleFor(x => x.TeamName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.teamname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamName));

        RuleFor(x => x.ProcessStatus)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsscheduleitem.processstatus"));

        RuleFor(x => x.Priority)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsscheduleitem.priority"));
    }
}

/// <summary>
/// ApsScheduleItem更新 DTO 验证器。
/// </summary>
public class TaktApsScheduleItemUpdateDtoValidator : AbstractValidator<TaktApsScheduleItemUpdateDto>
{
    public TaktApsScheduleItemUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktApsScheduleItemCreateDtoValidator(localizer));

        RuleFor(x => x.ApsScheduleItemId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.apsscheduleitem.apsscheduleitemid"));

        RuleFor(x => x.WorkOrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.workordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkOrderCode));

        RuleFor(x => x.ProductCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.productcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductCode));

        RuleFor(x => x.ProductName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.productname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductName));

        RuleFor(x => x.WorkCenterCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.workcentercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenterCode));

        RuleFor(x => x.WorkCenterName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.workcentername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenterName));

        RuleFor(x => x.ProcessCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.processcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessCode));

        RuleFor(x => x.ProcessName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.processname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessName));

        RuleFor(x => x.EquipmentCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.equipmentcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentCode));

        RuleFor(x => x.EquipmentName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.equipmentname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentName));

        RuleFor(x => x.TeamCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.teamcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamCode));

        RuleFor(x => x.TeamName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsscheduleitem.teamname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamName));
    }
}
