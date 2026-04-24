// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Materials
// 文件名称：TaktPurchasePriceValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchasePrice DTO 验证器（根据实体 TaktPurchasePrice 自动生成）
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
/// PurchasePrice创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchasePrice"/> 字段对齐）。
/// </summary>
public class TaktPurchasePriceCreateDtoValidator : AbstractValidator<TaktPurchasePriceCreateDto>
{
    public TaktPurchasePriceCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseprice.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.SupplierCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseprice.suppliercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaseprice.suppliercode", 1, 50));

        RuleFor(x => x.PriceType)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaseprice.pricetype"));

        RuleFor(x => x.PriceStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaseprice.pricestatus"));

        RuleFor(x => x.IsEnabled)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaseprice.isenabled"));
    }
}

/// <summary>
/// PurchasePrice更新 DTO 验证器。
/// </summary>
public class TaktPurchasePriceUpdateDtoValidator : AbstractValidator<TaktPurchasePriceUpdateDto>
{
    public TaktPurchasePriceUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPurchasePriceCreateDtoValidator(localizer));

        RuleFor(x => x.PurchasePriceId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseprice.purchasepriceid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseprice.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.SupplierCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseprice.suppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierCode));
    }
}
