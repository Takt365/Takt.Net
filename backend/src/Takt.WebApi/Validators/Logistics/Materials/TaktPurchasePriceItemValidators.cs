// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Materials
// 文件名称：TaktPurchasePriceItemValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchasePriceItem DTO 验证器（根据实体 TaktPurchasePriceItem 自动生成）
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
/// PurchasePriceItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchasePriceItem"/> 字段对齐）。
/// </summary>
public class TaktPurchasePriceItemCreateDtoValidator : AbstractValidator<TaktPurchasePriceItemCreateDto>
{
    public TaktPurchasePriceItemCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchasepriceitem.materialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchasepriceitem.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchasepriceitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchasepriceitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.PurchaseUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchasepriceitem.purchaseunit"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchasepriceitem.purchaseunit", 1, 20));
    }
}

/// <summary>
/// PurchasePriceItem更新 DTO 验证器。
/// </summary>
public class TaktPurchasePriceItemUpdateDtoValidator : AbstractValidator<TaktPurchasePriceItemUpdateDto>
{
    public TaktPurchasePriceItemUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPurchasePriceItemCreateDtoValidator(localizer));

        RuleFor(x => x.PurchasePriceItemId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.purchasepriceitem.purchasepriceitemid"));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchasepriceitem.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchasepriceitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchasepriceitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.PurchaseUnit)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchasepriceitem.purchaseunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseUnit));
    }
}
