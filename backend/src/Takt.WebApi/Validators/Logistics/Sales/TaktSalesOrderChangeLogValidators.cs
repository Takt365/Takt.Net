// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Sales
// 文件名称：TaktSalesOrderChangeLogValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalesOrderChangeLog DTO 验证器（根据实体 TaktSalesOrderChangeLog 自动生成）
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
/// SalesOrderChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Sales.TaktSalesOrderChangeLog"/> 字段对齐）。
/// </summary>
public class TaktSalesOrderChangeLogCreateDtoValidator : AbstractValidator<TaktSalesOrderChangeLogCreateDto>
{
    public TaktSalesOrderChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorderchangelog.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorderchangelog.ordercode", 1, 50));

        RuleFor(x => x.ChangeField)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorderchangelog.changefield"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorderchangelog.changefield", 1, 100));

        RuleFor(x => x.OldValue)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderchangelog.oldvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OldValue));

        RuleFor(x => x.NewValue)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderchangelog.newvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.NewValue));

        RuleFor(x => x.ChangeUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorderchangelog.changeusername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salesorderchangelog.changeusername", 1, 50));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// SalesOrderChangeLog更新 DTO 验证器。
/// </summary>
public class TaktSalesOrderChangeLogUpdateDtoValidator : AbstractValidator<TaktSalesOrderChangeLogUpdateDto>
{
    public TaktSalesOrderChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSalesOrderChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.SalesOrderChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.salesorderchangelog.salesorderchangelogid"));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderchangelog.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.ChangeField)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderchangelog.changefield", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeField));

        RuleFor(x => x.OldValue)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderchangelog.oldvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OldValue));

        RuleFor(x => x.NewValue)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderchangelog.newvalue", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.NewValue));

        RuleFor(x => x.ChangeUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderchangelog.changeusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeUserName));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salesorderchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
