// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Maintenance
// 文件名称：TaktMaintenanceValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Maintenance DTO 验证器（根据实体 TaktMaintenance 自动生成）
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
/// Maintenance创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Maintenance.TaktMaintenance"/> 字段对齐）。
/// </summary>
public class TaktMaintenanceCreateDtoValidator : AbstractValidator<TaktMaintenanceCreateDto>
{
    public TaktMaintenanceCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MaintenanceType)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.maintenance.maintenancetype"));

        RuleFor(x => x.MaintenanceCompany)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenancecompany", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceCompany));

        RuleFor(x => x.MaintenanceTechnician)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenancetechnician", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceTechnician));

        RuleFor(x => x.MaintenanceContent)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenancecontent", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceContent));

        RuleFor(x => x.FaultDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.faultdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.FaultDescription));

        RuleFor(x => x.Solution)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.solution", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Solution));

        RuleFor(x => x.UsedParts)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.usedparts", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.UsedParts));

        RuleFor(x => x.MaintenanceResult)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.maintenance.maintenanceresult"));

        RuleFor(x => x.MaintenanceStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.maintenance.maintenancestatus"));

        RuleFor(x => x.MaintenanceDocuments)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenancedocuments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceDocuments));

        RuleFor(x => x.MaintenanceImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenanceimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceImages));

        RuleFor(x => x.AcceptedSummary)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.acceptedsummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AcceptedSummary));

        RuleFor(x => x.AcceptedBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.acceptedby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AcceptedBy));
    }
}

/// <summary>
/// Maintenance更新 DTO 验证器。
/// </summary>
public class TaktMaintenanceUpdateDtoValidator : AbstractValidator<TaktMaintenanceUpdateDto>
{
    public TaktMaintenanceUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktMaintenanceCreateDtoValidator(localizer));

        RuleFor(x => x.MaintenanceId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.maintenance.maintenanceid"));

        RuleFor(x => x.MaintenanceCompany)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenancecompany", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceCompany));

        RuleFor(x => x.MaintenanceTechnician)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenancetechnician", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceTechnician));

        RuleFor(x => x.MaintenanceContent)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenancecontent", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceContent));

        RuleFor(x => x.FaultDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.faultdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.FaultDescription));

        RuleFor(x => x.Solution)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.solution", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Solution));

        RuleFor(x => x.UsedParts)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.usedparts", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.UsedParts));

        RuleFor(x => x.MaintenanceDocuments)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenancedocuments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceDocuments));

        RuleFor(x => x.MaintenanceImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.maintenanceimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaintenanceImages));

        RuleFor(x => x.AcceptedSummary)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.acceptedsummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AcceptedSummary));

        RuleFor(x => x.AcceptedBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.maintenance.acceptedby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AcceptedBy));
    }
}
