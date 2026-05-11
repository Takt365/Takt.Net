// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Maintenance
// 文件名称：TaktEquipmentValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Equipment DTO 验证器（根据实体 TaktEquipment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Maintenance;

namespace Takt.Application.Validators.Logistics.Maintenance;

/// <summary>
/// Equipment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Maintenance.TaktEquipment"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEquipmentCreateDtoValidator : AbstractValidator<TaktEquipmentCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEquipmentCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.equipment.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.equipment.plantcode"));

        RuleFor(x => x.EquipmentCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.equipment.equipmentcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.equipment.equipmentcode", 1, 50));

        RuleFor(x => x.EquipmentName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.equipment.equipmentname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.equipment.equipmentname", 1, 200));

        RuleFor(x => x.EquipmentType)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.equipment.equipmenttype"));

        RuleFor(x => x.EquipmentModel)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentmodel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentModel));

        RuleFor(x => x.EquipmentSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentSpecification));

        RuleFor(x => x.EquipmentBrand)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentbrand", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentBrand));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.equipment.manufacturer", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.DealerBy)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.equipment.dealerby", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DealerBy));

        RuleFor(x => x.SerialNumber)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.serialnumber", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNumber));

        RuleFor(x => x.WorkshopBy)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.workshopby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopBy));

        RuleFor(x => x.ProductionLineBy)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.productionlineby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineBy));

        RuleFor(x => x.WorkstationBy)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.workstationby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkstationBy));

        RuleFor(x => x.DeptBy)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.deptby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptBy));

        RuleFor(x => x.EquipmentLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentLocation));

        RuleFor(x => x.ResponsibleUserBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipment.responsibleuserby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleUserBy));

        RuleFor(x => x.OperatorBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipment.operatorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperatorBy));

        RuleFor(x => x.TechnicalParameters)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.equipment.technicalparameters", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.TechnicalParameters));

        RuleFor(x => x.EquipmentImages)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentImages));

        RuleFor(x => x.EquipmentDocuments)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentdocuments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentDocuments));

        RuleFor(x => x.IsCritical)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.equipment.iscritical"));

        RuleFor(x => x.WarrantyStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.equipment.warrantystatus"));

        RuleFor(x => x.EquipmentStatus)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.equipment.equipmentstatus"));
    }
}

/// <summary>
/// Equipment更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEquipmentCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EquipmentId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEquipmentUpdateDtoValidator : AbstractValidator<TaktEquipmentUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEquipmentUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEquipmentCreateDtoValidator(validationMessages));

        RuleFor(x => x.EquipmentId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.equipment.equipmentid"));

        RuleFor(x => x.EquipmentCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentCode));

        RuleFor(x => x.EquipmentName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentName));

        RuleFor(x => x.EquipmentModel)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentmodel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentModel));

        RuleFor(x => x.EquipmentSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentSpecification));

        RuleFor(x => x.EquipmentBrand)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentbrand", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentBrand));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.equipment.manufacturer", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.DealerBy)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.equipment.dealerby", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DealerBy));

        RuleFor(x => x.SerialNumber)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.serialnumber", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNumber));

        RuleFor(x => x.WorkshopBy)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.workshopby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopBy));

        RuleFor(x => x.ProductionLineBy)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.productionlineby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineBy));

        RuleFor(x => x.WorkstationBy)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.workstationby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkstationBy));

        RuleFor(x => x.DeptBy)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipment.deptby", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptBy));

        RuleFor(x => x.EquipmentLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentLocation));

        RuleFor(x => x.ResponsibleUserBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipment.responsibleuserby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleUserBy));

        RuleFor(x => x.OperatorBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipment.operatorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperatorBy));

        RuleFor(x => x.TechnicalParameters)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.equipment.technicalparameters", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.TechnicalParameters));

        RuleFor(x => x.EquipmentImages)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentImages));

        RuleFor(x => x.EquipmentDocuments)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.equipment.equipmentdocuments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentDocuments));
    }
}
