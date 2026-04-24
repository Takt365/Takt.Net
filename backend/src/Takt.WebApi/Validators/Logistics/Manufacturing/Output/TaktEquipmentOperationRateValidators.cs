// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktEquipmentOperationRateValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EquipmentOperationRate DTO 验证器（根据实体 TaktEquipmentOperationRate 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// EquipmentOperationRate创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktEquipmentOperationRate"/> 字段对齐）。
/// </summary>
public class TaktEquipmentOperationRateCreateDtoValidator : AbstractValidator<TaktEquipmentOperationRateCreateDto>
{
    public TaktEquipmentOperationRateCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.equipmentoperationrate.plantcode"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.equipmentoperationrate.plantcode", 1, 8));

        RuleFor(x => x.TimeCategory)
            .InclusiveBetween(1, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.equipmentoperationrate.timecategory"));

        RuleFor(x => x.EquipmentCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.equipmentoperationrate.equipmentcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.equipmentoperationrate.equipmentcode", 1, 20));

        RuleFor(x => x.EquipmentName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.equipmentoperationrate.equipmentname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.equipmentoperationrate.equipmentname", 1, 100));

        RuleFor(x => x.EquipmentType)
            .InclusiveBetween(1, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.equipmentoperationrate.equipmenttype"));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.productionline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));

        RuleFor(x => x.ShiftNo)
            .InclusiveBetween(1, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.equipmentoperationrate.shiftno"));

        RuleFor(x => x.DowntimeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.downtimereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeReason));

        RuleFor(x => x.EquipmentStatus)
            .InclusiveBetween(1, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.equipmentoperationrate.equipmentstatus"));

        RuleFor(x => x.EquipmentOperator)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.equipmentoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentOperator));

        RuleFor(x => x.EquipmentMaintainer)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.equipmentmaintainer", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentMaintainer));

        RuleFor(x => x.TeamLeader)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.teamleader", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeader));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.equipmentoperationrate.status"));
    }
}

/// <summary>
/// EquipmentOperationRate更新 DTO 验证器。
/// </summary>
public class TaktEquipmentOperationRateUpdateDtoValidator : AbstractValidator<TaktEquipmentOperationRateUpdateDto>
{
    public TaktEquipmentOperationRateUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEquipmentOperationRateCreateDtoValidator(localizer));

        RuleFor(x => x.EquipmentOperationRateId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.equipmentoperationrate.equipmentoperationrateid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.plantcode", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.EquipmentCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.equipmentcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentCode));

        RuleFor(x => x.EquipmentName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.equipmentname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentName));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.productionline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));

        RuleFor(x => x.DowntimeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.downtimereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeReason));

        RuleFor(x => x.EquipmentOperator)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.equipmentoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentOperator));

        RuleFor(x => x.EquipmentMaintainer)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.equipmentmaintainer", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentMaintainer));

        RuleFor(x => x.TeamLeader)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipmentoperationrate.teamleader", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeader));
    }
}
