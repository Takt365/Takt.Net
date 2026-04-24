// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Materials
// 文件名称：TaktPurchaseOrderChangeLogValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchaseOrderChangeLog DTO 验证器（根据实体 TaktPurchaseOrderChangeLog 自动生成）
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
/// PurchaseOrderChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchaseOrderChangeLog"/> 字段对齐）。
/// </summary>
public class TaktPurchaseOrderChangeLogCreateDtoValidator : AbstractValidator<TaktPurchaseOrderChangeLogCreateDto>
{
    public TaktPurchaseOrderChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseorderchangelog.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaseorderchangelog.ordercode", 1, 50));

        RuleFor(x => x.ChangeField)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseorderchangelog.changefield"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaseorderchangelog.changefield", 1, 100));

        RuleFor(x => x.OldValue)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorderchangelog.oldvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OldValue));

        RuleFor(x => x.NewValue)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorderchangelog.newvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.NewValue));

        RuleFor(x => x.ChangeUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseorderchangelog.changeusername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaseorderchangelog.changeusername", 1, 50));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorderchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// PurchaseOrderChangeLog更新 DTO 验证器。
/// </summary>
public class TaktPurchaseOrderChangeLogUpdateDtoValidator : AbstractValidator<TaktPurchaseOrderChangeLogUpdateDto>
{
    public TaktPurchaseOrderChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPurchaseOrderChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.PurchaseOrderChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaseorderchangelog.purchaseorderchangelogid"));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorderchangelog.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.ChangeField)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorderchangelog.changefield", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeField));

        RuleFor(x => x.OldValue)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorderchangelog.oldvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OldValue));

        RuleFor(x => x.NewValue)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorderchangelog.newvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.NewValue));

        RuleFor(x => x.ChangeUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorderchangelog.changeusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeUserName));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaseorderchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
