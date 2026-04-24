// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Sales
// 文件名称：TaktSalesPriceScaleValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalesPriceScale DTO 验证器（根据实体 TaktSalesPriceScale 自动生成）
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
/// SalesPriceScale创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Sales.TaktSalesPriceScale"/> 字段对齐）。
/// </summary>
public class TaktSalesPriceScaleCreateDtoValidator : AbstractValidator<TaktSalesPriceScaleCreateDto>
{
    public TaktSalesPriceScaleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
    }
}

/// <summary>
/// SalesPriceScale更新 DTO 验证器。
/// </summary>
public class TaktSalesPriceScaleUpdateDtoValidator : AbstractValidator<TaktSalesPriceScaleUpdateDto>
{
    public TaktSalesPriceScaleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSalesPriceScaleCreateDtoValidator(localizer));

        RuleFor(x => x.SalesPriceScaleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.salespricescale.salespricescaleid"));

    }
}
