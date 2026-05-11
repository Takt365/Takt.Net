// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Materials
// 文件名称：TaktPurchaseOrderValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchaseOrder DTO 验证器（根据实体 TaktPurchaseOrder 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;

namespace Takt.Application.Validators.Logistics.Materials;

/// <summary>
/// PurchaseOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchaseOrder"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPurchaseOrderCreateDtoValidator : AbstractValidator<TaktPurchaseOrderCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchaseOrderCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaseorder.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.purchaseorder.plantcode"));

        RuleFor(x => x.PurchaseOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaseorder.purchaseordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchaseorder.purchaseordercode", 1, 50));

        RuleFor(x => x.SupplierCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaseorder.suppliercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchaseorder.suppliercode", 1, 50));

        RuleFor(x => x.SupplierName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaseorder.suppliername"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.purchaseorder.suppliername", 1, 200));

        RuleFor(x => x.PurchaseGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaseorder.purchasegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseGroup));

        RuleFor(x => x.OrderStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.purchaseorder.orderstatus"));

        RuleFor(x => x.DeliveryStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.purchaseorder.deliverystatus"));

        RuleFor(x => x.PaymentMethod)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.purchaseorder.paymentmethod"));

        RuleFor(x => x.DeliveryMethod)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.purchaseorder.deliverymethod"));

        RuleFor(x => x.DeliveryAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.purchaseorder.deliveryaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeliveryAddress));
    }
}

/// <summary>
/// PurchaseOrder更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPurchaseOrderCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PurchaseOrderId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPurchaseOrderUpdateDtoValidator : AbstractValidator<TaktPurchaseOrderUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchaseOrderUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPurchaseOrderCreateDtoValidator(validationMessages));

        RuleFor(x => x.PurchaseOrderId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.purchaseorder.purchaseorderid"));

        RuleFor(x => x.PurchaseOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaseorder.purchaseordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseOrderCode));

        RuleFor(x => x.SupplierCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaseorder.suppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierCode));

        RuleFor(x => x.SupplierName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchaseorder.suppliername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierName));

        RuleFor(x => x.PurchaseGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaseorder.purchasegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseGroup));

        RuleFor(x => x.DeliveryAddress)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.purchaseorder.deliveryaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeliveryAddress));
    }
}
