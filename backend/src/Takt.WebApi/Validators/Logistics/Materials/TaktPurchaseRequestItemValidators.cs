// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Materials
// 文件名称：TaktPurchaseRequestItemValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchaseRequestItem DTO 验证器（根据实体 TaktPurchaseRequestItem 自动生成）
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
/// PurchaseRequestItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchaseRequestItem"/> 字段对齐）。
/// </summary>
public class TaktPurchaseRequestItemCreateDtoValidator : AbstractValidator<TaktPurchaseRequestItemCreateDto>
{
    public TaktPurchaseRequestItemCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RequestCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaserequestitem.requestcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaserequestitem.requestcode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaserequestitem.materialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaserequestitem.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaserequestitem.materialname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaserequestitem.materialname", 1, 200));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequestitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.RequestUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaserequestitem.requestunit"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaserequestitem.requestunit", 1, 20));
    }
}

/// <summary>
/// PurchaseRequestItem更新 DTO 验证器。
/// </summary>
public class TaktPurchaseRequestItemUpdateDtoValidator : AbstractValidator<TaktPurchaseRequestItemUpdateDto>
{
    public TaktPurchaseRequestItemUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPurchaseRequestItemCreateDtoValidator(localizer));

        RuleFor(x => x.PurchaseRequestItemId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaserequestitem.purchaserequestitemid"));

        RuleFor(x => x.RequestCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequestitem.requestcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequestitem.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequestitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequestitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.RequestUnit)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequestitem.requestunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestUnit));
    }
}
