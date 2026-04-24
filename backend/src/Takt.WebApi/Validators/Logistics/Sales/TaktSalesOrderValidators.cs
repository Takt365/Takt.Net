// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Sales
// 文件名称：TaktSalesOrderValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalesOrder DTO 验证器（根据实体 TaktSalesOrder 自动生成）
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
/// SalesOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Sales.TaktSalesOrder"/> 字段对齐）。
/// </summary>
public class TaktSalesOrderCreateDtoValidator : AbstractValidator<TaktSalesOrderCreateDto>
{
    public TaktSalesOrderCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorder.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorder.ordercode", 1, 50));

        RuleFor(x => x.CustomerCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorder.customercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorder.customercode", 1, 50));

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorder.customername"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorder.customername", 1, 200));

        RuleFor(x => x.CustomerContact)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.customercontact", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerContact));

        RuleFor(x => x.CustomerPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.customerphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerPhone));

        RuleFor(x => x.CustomerAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.customeraddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerAddress));

        RuleFor(x => x.SalesUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorder.salesusername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorder.salesusername", 1, 50));

        RuleFor(x => x.SalesDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.salesdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SalesDeptName));

        RuleFor(x => x.OrderStatus)
            .InclusiveBetween(0, 6)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salesorder.orderstatus"));

        RuleFor(x => x.PaymentStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salesorder.paymentstatus"));

        RuleFor(x => x.PaymentMethod)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salesorder.paymentmethod"));

        RuleFor(x => x.DeliveryMethod)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salesorder.deliverymethod"));

        RuleFor(x => x.DeliveryAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.deliveryaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeliveryAddress));
    }
}

/// <summary>
/// SalesOrder更新 DTO 验证器。
/// </summary>
public class TaktSalesOrderUpdateDtoValidator : AbstractValidator<TaktSalesOrderUpdateDto>
{
    public TaktSalesOrderUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSalesOrderCreateDtoValidator(localizer));

        RuleFor(x => x.SalesOrderId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorder.salesorderid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));

        RuleFor(x => x.CustomerName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.customername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerName));

        RuleFor(x => x.CustomerContact)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.customercontact", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerContact));

        RuleFor(x => x.CustomerPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.customerphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerPhone));

        RuleFor(x => x.CustomerAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.customeraddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerAddress));

        RuleFor(x => x.SalesUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.salesusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SalesUserName));

        RuleFor(x => x.SalesDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.salesdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SalesDeptName));

        RuleFor(x => x.DeliveryAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorder.deliveryaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeliveryAddress));
    }
}
