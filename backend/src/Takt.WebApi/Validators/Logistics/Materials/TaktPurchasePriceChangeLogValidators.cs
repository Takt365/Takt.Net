// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Materials
// 文件名称：TaktPurchasePriceChangeLogValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchasePriceChangeLog DTO 验证器（根据实体 TaktPurchasePriceChangeLog 自动生成）
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
/// PurchasePriceChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchasePriceChangeLog"/> 字段对齐）。
/// </summary>
public class TaktPurchasePriceChangeLogCreateDtoValidator : AbstractValidator<TaktPurchasePriceChangeLogCreateDto>
{
    public TaktPurchasePriceChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ChangeUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchasepricechangelog.changeusername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchasepricechangelog.changeusername", 1, 50));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchasepricechangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// PurchasePriceChangeLog更新 DTO 验证器。
/// </summary>
public class TaktPurchasePriceChangeLogUpdateDtoValidator : AbstractValidator<TaktPurchasePriceChangeLogUpdateDto>
{
    public TaktPurchasePriceChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPurchasePriceChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.PurchasePriceChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.purchasepricechangelog.purchasepricechangelogid"));

        RuleFor(x => x.ChangeUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchasepricechangelog.changeusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeUserName));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchasepricechangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
