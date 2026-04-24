// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Sales
// 文件名称：TaktSalesPriceValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalesPrice DTO 验证器（根据实体 TaktSalesPrice 自动生成）
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
/// SalesPrice创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Sales.TaktSalesPrice"/> 字段对齐）。
/// </summary>
public class TaktSalesPriceCreateDtoValidator : AbstractValidator<TaktSalesPriceCreateDto>
{
    public TaktSalesPriceCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesprice.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesprice.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));

        RuleFor(x => x.PriceType)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salesprice.pricetype"));

        RuleFor(x => x.PriceStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salesprice.pricestatus"));

        RuleFor(x => x.IsEnabled)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salesprice.isenabled"));
    }
}

/// <summary>
/// SalesPrice更新 DTO 验证器。
/// </summary>
public class TaktSalesPriceUpdateDtoValidator : AbstractValidator<TaktSalesPriceUpdateDto>
{
    public TaktSalesPriceUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSalesPriceCreateDtoValidator(localizer));

        RuleFor(x => x.SalesPriceId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.salesprice.salespriceid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesprice.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesprice.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));
    }
}
