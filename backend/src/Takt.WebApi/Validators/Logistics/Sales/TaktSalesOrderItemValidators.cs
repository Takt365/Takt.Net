// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Sales
// 文件名称：TaktSalesOrderItemValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalesOrderItem DTO 验证器（根据实体 TaktSalesOrderItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Sales;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Sales;

/// <summary>
/// SalesOrderItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Sales.TaktSalesOrderItem"/> 字段对齐）。
/// </summary>
public class TaktSalesOrderItemCreateDtoValidator : AbstractValidator<TaktSalesOrderItemCreateDto>
{
    public TaktSalesOrderItemCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorderitem.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorderitem.ordercode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorderitem.materialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorderitem.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorderitem.materialname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorderitem.materialname", 1, 200));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.SalesUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorderitem.salesunit"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorderitem.salesunit", 1, 20));
    }
}

/// <summary>
/// SalesOrderItem更新 DTO 验证器。
/// </summary>
public class TaktSalesOrderItemUpdateDtoValidator : AbstractValidator<TaktSalesOrderItemUpdateDto>
{
    public TaktSalesOrderItemUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSalesOrderItemCreateDtoValidator(localizer));

        RuleFor(x => x.SalesOrderItemId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorderitem.salesorderitemid"));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderitem.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderitem.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.SalesUnit)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderitem.salesunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SalesUnit));
    }
}
