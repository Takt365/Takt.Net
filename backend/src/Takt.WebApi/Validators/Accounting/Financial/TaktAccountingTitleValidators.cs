// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Financial
// 文件名称：TaktAccountingTitleValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AccountingTitle DTO 验证器（根据实体 TaktAccountingTitle 自动生成）
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
/// AccountingTitle创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktAccountingTitle"/> 字段对齐）。
/// </summary>
public class TaktAccountingTitleCreateDtoValidator : AbstractValidator<TaktAccountingTitleCreateDto>
{
    public TaktAccountingTitleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitle.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.TitleCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.accountingtitle.titlecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.accountingtitle.titlecode", 1, 50));

        RuleFor(x => x.TitleName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.accountingtitle.titlename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.accountingtitle.titlename", 1, 200));

        RuleFor(x => x.TitleType)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.titletype"));

        RuleFor(x => x.BalanceDirection)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.balancedirection"));

        RuleFor(x => x.IsLeaf)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.isleaf"));

        RuleFor(x => x.IsAuxiliary)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.isauxiliary"));

        RuleFor(x => x.AuxiliaryType)
            .InclusiveBetween(0, 6)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.auxiliarytype"));

        RuleFor(x => x.IsQuantity)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.isquantity"));

        RuleFor(x => x.IsCurrency)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.iscurrency"));

        RuleFor(x => x.IsCash)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.iscash"));

        RuleFor(x => x.IsBank)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.isbank"));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitle.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.TitleStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.accountingtitle.titlestatus"));
    }
}

/// <summary>
/// AccountingTitle更新 DTO 验证器。
/// </summary>
public class TaktAccountingTitleUpdateDtoValidator : AbstractValidator<TaktAccountingTitleUpdateDto>
{
    public TaktAccountingTitleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAccountingTitleCreateDtoValidator(localizer));

        RuleFor(x => x.AccountingTitleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.accountingtitle.accountingtitleid"));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitle.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.TitleCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitle.titlecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TitleCode));

        RuleFor(x => x.TitleName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitle.titlename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TitleName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.accountingtitle.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
