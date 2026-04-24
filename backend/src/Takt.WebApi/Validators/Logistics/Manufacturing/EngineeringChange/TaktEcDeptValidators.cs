// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDeptValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EcDept DTO 验证器（根据实体 TaktEcDept 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// EcDept创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEcDept"/> 字段对齐）。
/// </summary>
public class TaktEcDeptCreateDtoValidator : AbstractValidator<TaktEcDeptCreateDto>
{
    public TaktEcDeptCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DeptCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ecdept.deptcode"))
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.deptcode", 50));

        RuleFor(x => x.IsImplemented)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ecdept.isimplemented"));

        RuleFor(x => x.Content)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.content", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.Content));

        RuleFor(x => x.ScheduledBatch)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.scheduledbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduledBatch));

        RuleFor(x => x.PoRemainder)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.poremainder", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PoRemainder));

        RuleFor(x => x.Balance)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.balance", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Balance));

        RuleFor(x => x.OldProductHandling)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.oldproducthandling", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.OldProductHandling));

        RuleFor(x => x.Supplier)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.supplier", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Supplier));

        RuleFor(x => x.PurchaseOrderNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.purchaseorderno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseOrderNo));

        RuleFor(x => x.IqcOrderNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.iqcorderno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IqcOrderNo));

        RuleFor(x => x.OutboundBatch)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.outboundbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundBatch));

        RuleFor(x => x.ProductionBatch)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.productionbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionBatch));

        RuleFor(x => x.OutboundOrderNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.outboundorderno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundOrderNo));

        RuleFor(x => x.ProductionTeam)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.productionteam", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionTeam));

        RuleFor(x => x.InspectionBatch)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.inspectionbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionBatch));

        RuleFor(x => x.SamplingNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.samplingno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingNo));

        RuleFor(x => x.IsSopUpdated)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ecdept.issopupdated"));
    }
}

/// <summary>
/// EcDept更新 DTO 验证器。
/// </summary>
public class TaktEcDeptUpdateDtoValidator : AbstractValidator<TaktEcDeptUpdateDto>
{
    public TaktEcDeptUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEcDeptCreateDtoValidator(localizer));

        RuleFor(x => x.EcDeptId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ecdept.ecdeptid"));

        RuleFor(x => x.Content)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.content", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.Content));

        RuleFor(x => x.ScheduledBatch)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.scheduledbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduledBatch));

        RuleFor(x => x.PoRemainder)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.poremainder", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PoRemainder));

        RuleFor(x => x.Balance)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.balance", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Balance));

        RuleFor(x => x.OldProductHandling)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.oldproducthandling", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.OldProductHandling));

        RuleFor(x => x.Supplier)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.supplier", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Supplier));

        RuleFor(x => x.PurchaseOrderNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.purchaseorderno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseOrderNo));

        RuleFor(x => x.IqcOrderNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.iqcorderno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IqcOrderNo));

        RuleFor(x => x.OutboundBatch)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.outboundbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundBatch));

        RuleFor(x => x.ProductionBatch)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.productionbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionBatch));

        RuleFor(x => x.OutboundOrderNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.outboundorderno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundOrderNo));

        RuleFor(x => x.ProductionTeam)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.productionteam", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionTeam));

        RuleFor(x => x.InspectionBatch)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.inspectionbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionBatch));

        RuleFor(x => x.SamplingNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdept.samplingno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingNo));
    }
}
