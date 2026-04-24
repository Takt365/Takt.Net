// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Maintenance
// 文件名称：TaktEquipmentValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Equipment DTO 验证器（根据实体 TaktEquipment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Maintenance;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Maintenance;

/// <summary>
/// Equipment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Maintenance.TaktEquipment"/> 字段对齐）。
/// </summary>
public class TaktEquipmentCreateDtoValidator : AbstractValidator<TaktEquipmentCreateDto>
{
    public TaktEquipmentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.EquipmentCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.equipment.equipmentcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.equipment.equipmentcode", 1, 50));

        RuleFor(x => x.EquipmentName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.equipment.equipmentname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.equipment.equipmentname", 1, 200));

        RuleFor(x => x.EquipmentType)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.equipment.equipmenttype"));

        RuleFor(x => x.EquipmentModel)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentmodel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentModel));

        RuleFor(x => x.EquipmentSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentSpecification));

        RuleFor(x => x.EquipmentBrand)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentbrand", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentBrand));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.manufacturer", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.DealerBy)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.dealerby", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DealerBy));

        RuleFor(x => x.SerialNumber)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.serialnumber", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNumber));

        RuleFor(x => x.WorkshopBy)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.workshopby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopBy));

        RuleFor(x => x.ProductionLineBy)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.productionlineby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineBy));

        RuleFor(x => x.WorkstationBy)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.workstationby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkstationBy));

        RuleFor(x => x.DeptBy)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.deptby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptBy));

        RuleFor(x => x.EquipmentLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentLocation));

        RuleFor(x => x.ResponsibleUserBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.responsibleuserby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleUserBy));

        RuleFor(x => x.OperatorBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.operatorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperatorBy));

        RuleFor(x => x.TechnicalParameters)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.technicalparameters", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.TechnicalParameters));

        RuleFor(x => x.EquipmentImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentImages));

        RuleFor(x => x.EquipmentDocuments)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentdocuments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentDocuments));

        RuleFor(x => x.IsCritical)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.equipment.iscritical"));

        RuleFor(x => x.WarrantyStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.equipment.warrantystatus"));

        RuleFor(x => x.EquipmentStatus)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.equipment.equipmentstatus"));
    }
}

/// <summary>
/// Equipment更新 DTO 验证器。
/// </summary>
public class TaktEquipmentUpdateDtoValidator : AbstractValidator<TaktEquipmentUpdateDto>
{
    public TaktEquipmentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEquipmentCreateDtoValidator(localizer));

        RuleFor(x => x.EquipmentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.equipment.equipmentid"));

        RuleFor(x => x.EquipmentCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentCode));

        RuleFor(x => x.EquipmentName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentName));

        RuleFor(x => x.EquipmentModel)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentmodel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentModel));

        RuleFor(x => x.EquipmentSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentSpecification));

        RuleFor(x => x.EquipmentBrand)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentbrand", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentBrand));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.manufacturer", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.DealerBy)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.dealerby", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DealerBy));

        RuleFor(x => x.SerialNumber)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.serialnumber", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNumber));

        RuleFor(x => x.WorkshopBy)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.workshopby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopBy));

        RuleFor(x => x.ProductionLineBy)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.productionlineby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineBy));

        RuleFor(x => x.WorkstationBy)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.workstationby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkstationBy));

        RuleFor(x => x.DeptBy)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.deptby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptBy));

        RuleFor(x => x.EquipmentLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentLocation));

        RuleFor(x => x.ResponsibleUserBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.responsibleuserby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleUserBy));

        RuleFor(x => x.OperatorBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.operatorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperatorBy));

        RuleFor(x => x.TechnicalParameters)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.technicalparameters", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.TechnicalParameters));

        RuleFor(x => x.EquipmentImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentImages));

        RuleFor(x => x.EquipmentDocuments)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.equipment.equipmentdocuments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentDocuments));
    }
}
