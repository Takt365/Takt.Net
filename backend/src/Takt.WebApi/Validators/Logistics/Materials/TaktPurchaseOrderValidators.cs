// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Materials
// 文件名称：TaktPurchaseOrderValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchaseOrder DTO 验证器（根据实体 TaktPurchaseOrder 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Materials;

/// <summary>
/// PurchaseOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchaseOrder"/> 字段对齐）。
/// </summary>
public class TaktPurchaseOrderCreateDtoValidator : AbstractValidator<TaktPurchaseOrderCreateDto>
{
    public TaktPurchaseOrderCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseorder.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaseorder.ordercode", 1, 50));

        RuleFor(x => x.RequestCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.requestcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestCode));

        RuleFor(x => x.SupplierCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseorder.suppliercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaseorder.suppliercode", 1, 50));

        RuleFor(x => x.SupplierName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseorder.suppliername"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaseorder.suppliername", 1, 200));

        RuleFor(x => x.SupplierContact)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.suppliercontact", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierContact));

        RuleFor(x => x.SupplierPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.supplierphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierPhone));

        RuleFor(x => x.SupplierAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.supplieraddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierAddress));

        RuleFor(x => x.PurchaseUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseorder.purchaseusername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaseorder.purchaseusername", 1, 50));

        RuleFor(x => x.PurchaseDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.purchasedeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseDeptName));

        RuleFor(x => x.OrderStatus)
            .InclusiveBetween(0, 6)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaseorder.orderstatus"));

        RuleFor(x => x.PaymentStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaseorder.paymentstatus"));

        RuleFor(x => x.PaymentMethod)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaseorder.paymentmethod"));

        RuleFor(x => x.DeliveryMethod)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaseorder.deliverymethod"));

        RuleFor(x => x.DeliveryAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.deliveryaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeliveryAddress));
    }
}

/// <summary>
/// PurchaseOrder更新 DTO 验证器。
/// </summary>
public class TaktPurchaseOrderUpdateDtoValidator : AbstractValidator<TaktPurchaseOrderUpdateDto>
{
    public TaktPurchaseOrderUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPurchaseOrderCreateDtoValidator(localizer));

        RuleFor(x => x.PurchaseOrderId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseorder.purchaseorderid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.RequestCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.requestcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestCode));

        RuleFor(x => x.SupplierCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.suppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierCode));

        RuleFor(x => x.SupplierName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.suppliername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierName));

        RuleFor(x => x.SupplierContact)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.suppliercontact", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierContact));

        RuleFor(x => x.SupplierPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.supplierphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierPhone));

        RuleFor(x => x.SupplierAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.supplieraddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierAddress));

        RuleFor(x => x.PurchaseUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.purchaseusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseUserName));

        RuleFor(x => x.PurchaseDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.purchasedeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseDeptName));

        RuleFor(x => x.DeliveryAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorder.deliveryaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeliveryAddress));
    }
}
