// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Materials
// 文件名称：TaktPurchasePriceItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchasePriceItem DTO 验证器（根据实体 TaktPurchasePriceItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;

namespace Takt.Application.Validators.Logistics.Materials;

/// <summary>
/// PurchasePriceItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchasePriceItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPurchasePriceItemCreateDtoValidator : AbstractValidator<TaktPurchasePriceItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchasePriceItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PurchasePriceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchasepriceitem.purchasepricecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchasepriceitem.purchasepricecode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchasepriceitem.materialcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchasepriceitem.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchasepriceitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchasepriceitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.PurchaseUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchasepriceitem.purchaseunit"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.purchasepriceitem.purchaseunit", 1, 20));
    }
}

/// <summary>
/// PurchasePriceItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPurchasePriceItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PurchasePriceItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPurchasePriceItemUpdateDtoValidator : AbstractValidator<TaktPurchasePriceItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchasePriceItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPurchasePriceItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.PurchasePriceItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.purchasepriceitem.purchasepriceitemid"));

        RuleFor(x => x.PurchasePriceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchasepriceitem.purchasepricecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchasePriceCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchasepriceitem.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchasepriceitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchasepriceitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.PurchaseUnit)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.purchasepriceitem.purchaseunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseUnit));
    }
}
