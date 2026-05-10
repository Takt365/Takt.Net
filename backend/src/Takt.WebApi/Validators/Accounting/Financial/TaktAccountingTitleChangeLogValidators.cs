// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Financial
// 文件名称：TaktAccountingTitleChangeLogValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AccountingTitleChangeLog DTO 验证器（根据实体 TaktAccountingTitleChangeLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Accounting.Financial;

/// <summary>
/// AccountingTitleChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktAccountingTitleChangeLog"/> 字段对齐）。
/// </summary>
public class TaktAccountingTitleChangeLogCreateDtoValidator : AbstractValidator<TaktAccountingTitleChangeLogCreateDto>
{
    public TaktAccountingTitleChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TitleCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.accountingtitlechangelog.titlecode"))
            .Must(TaktRegexHelper.IsValidTitleCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitlechangelog.titlecode"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitlechangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitlechangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitlechangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// AccountingTitleChangeLog更新 DTO 验证器。
/// </summary>
public class TaktAccountingTitleChangeLogUpdateDtoValidator : AbstractValidator<TaktAccountingTitleChangeLogUpdateDto>
{
    public TaktAccountingTitleChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAccountingTitleChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.AccountingTitleChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.accountingtitlechangelog.accountingtitlechangelogid"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitlechangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitlechangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitlechangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
