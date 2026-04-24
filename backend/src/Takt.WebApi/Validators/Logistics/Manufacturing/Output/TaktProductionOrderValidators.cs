// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktProductionOrderValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProductionOrder DTO 验证器（根据实体 TaktProductionOrder 自动生成）
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
/// ProductionOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktProductionOrder"/> 字段对齐）。
/// </summary>
public class TaktProductionOrderCreateDtoValidator : AbstractValidator<TaktProductionOrderCreateDto>
{
    public TaktProductionOrderCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.productionorder.plantcode"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.productionorder.plantcode", 1, 8));

        RuleFor(x => x.ProdOrderType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.productionorder.prodordertype"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.productionorder.prodordertype", 1, 10));

        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.productionorder.prodordercode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.productionorder.prodordercode", 1, 20));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.productionorder.materialcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.productionorder.materialcode", 1, 20));

        RuleFor(x => x.UnitOfMeasure)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.productionorder.unitofmeasure"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.productionorder.unitofmeasure", 1, 10));

        RuleFor(x => x.Priority)
            .InclusiveBetween(1, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.productionorder.priority"));

        RuleFor(x => x.WorkCenter)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.workcenter", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenter));

        RuleFor(x => x.ProdLine)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.prodline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.ProdBatch)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.prodbatch", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdBatch));

        RuleFor(x => x.SerialNo)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.serialno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNo));

        RuleFor(x => x.RoutingCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.routingcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.RoutingCode));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.productionorder.status"));
    }
}

/// <summary>
/// ProductionOrder更新 DTO 验证器。
/// </summary>
public class TaktProductionOrderUpdateDtoValidator : AbstractValidator<TaktProductionOrderUpdateDto>
{
    public TaktProductionOrderUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktProductionOrderCreateDtoValidator(localizer));

        RuleFor(x => x.ProductionOrderId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.productionorder.productionorderid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.plantcode", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.ProdOrderType)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.prodordertype", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderType));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.materialcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.UnitOfMeasure)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.unitofmeasure", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.UnitOfMeasure));

        RuleFor(x => x.WorkCenter)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.workcenter", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenter));

        RuleFor(x => x.ProdLine)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.prodline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.ProdBatch)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.prodbatch", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdBatch));

        RuleFor(x => x.SerialNo)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.serialno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNo));

        RuleFor(x => x.RoutingCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionorder.routingcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.RoutingCode));
    }
}
