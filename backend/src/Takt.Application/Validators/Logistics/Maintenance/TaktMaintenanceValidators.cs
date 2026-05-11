// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Maintenance
// 文件名称：TaktMaintenanceValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Maintenance DTO 验证器（根据实体 TaktMaintenance 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Maintenance;

namespace Takt.Application.Validators.Logistics.Maintenance;

/// <summary>
/// Maintenance创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Maintenance.TaktMaintenance"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktMaintenanceCreateDtoValidator : AbstractValidator<TaktMaintenanceCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMaintenanceCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.EquipmentCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.maintenance.equipmentcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.maintenance.equipmentcode", 1, 50));

        RuleFor(x => x.MaintenanceType)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.maintenance.maintenancetype"));

        RuleFor(x => x.MaintenanceCompany)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenancecompany", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceCompany));

        RuleFor(x => x.MaintenanceTechnician)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenancetechnician", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceTechnician));

        RuleFor(x => x.MaintenanceContent)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenancecontent", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceContent));

        RuleFor(x => x.FaultDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.maintenance.faultdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.FaultDescription));

        RuleFor(x => x.Solution)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.maintenance.solution", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Solution));

        RuleFor(x => x.UsedParts)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.maintenance.usedparts", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.UsedParts));

        RuleFor(x => x.MaintenanceResult)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.maintenance.maintenanceresult"));

        RuleFor(x => x.MaintenanceStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.maintenance.maintenancestatus"));

        RuleFor(x => x.MaintenanceDocuments)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenancedocuments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceDocuments));

        RuleFor(x => x.MaintenanceImages)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenanceimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceImages));

        RuleFor(x => x.AcceptedSummary)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.maintenance.acceptedsummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AcceptedSummary));

        RuleFor(x => x.AcceptedBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.maintenance.acceptedby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AcceptedBy));
    }
}

/// <summary>
/// Maintenance更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktMaintenanceCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>MaintenanceId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktMaintenanceUpdateDtoValidator : AbstractValidator<TaktMaintenanceUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMaintenanceUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktMaintenanceCreateDtoValidator(validationMessages));

        RuleFor(x => x.MaintenanceId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.maintenance.maintenanceid"));

        RuleFor(x => x.EquipmentCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.maintenance.equipmentcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentCode));

        RuleFor(x => x.MaintenanceCompany)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenancecompany", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceCompany));

        RuleFor(x => x.MaintenanceTechnician)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenancetechnician", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceTechnician));

        RuleFor(x => x.MaintenanceContent)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenancecontent", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceContent));

        RuleFor(x => x.FaultDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.maintenance.faultdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.FaultDescription));

        RuleFor(x => x.Solution)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.maintenance.solution", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Solution));

        RuleFor(x => x.UsedParts)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.maintenance.usedparts", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.UsedParts));

        RuleFor(x => x.MaintenanceDocuments)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenancedocuments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceDocuments));

        RuleFor(x => x.MaintenanceImages)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.maintenance.maintenanceimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceImages));

        RuleFor(x => x.AcceptedSummary)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.maintenance.acceptedsummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AcceptedSummary));

        RuleFor(x => x.AcceptedBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.maintenance.acceptedby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AcceptedBy));
    }
}
