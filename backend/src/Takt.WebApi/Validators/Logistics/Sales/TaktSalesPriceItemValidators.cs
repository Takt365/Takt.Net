// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Sales
// 文件名称：TaktSalesPriceItemValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalesPriceItem DTO 验证器（根据实体 TaktSalesPriceItem 自动生成）
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
/// SalesPriceItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Sales.TaktSalesPriceItem"/> 字段对齐）。
/// </summary>
public class TaktSalesPriceItemCreateDtoValidator : AbstractValidator<TaktSalesPriceItemCreateDto>
{
    public TaktSalesPriceItemCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salespriceitem.materialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salespriceitem.materialcode", 1, 50));

        RuleFor(x => x.SalesUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salespriceitem.salesunit"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salespriceitem.salesunit", 1, 20));
    }
}

/// <summary>
/// SalesPriceItem更新 DTO 验证器。
/// </summary>
public class TaktSalesPriceItemUpdateDtoValidator : AbstractValidator<TaktSalesPriceItemUpdateDto>
{
    public TaktSalesPriceItemUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSalesPriceItemCreateDtoValidator(localizer));

        RuleFor(x => x.SalesPriceItemId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.salespriceitem.salespriceitemid"));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salespriceitem.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.SalesUnit)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salespriceitem.salesunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SalesUnit));
    }
}
