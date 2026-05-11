// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Accounting.Financial
// 文件名称：TaktAccountingTitleValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AccountingTitle DTO 验证器（根据实体 TaktAccountingTitle 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Financial;

namespace Takt.Application.Validators.Accounting.Financial;

/// <summary>
/// AccountingTitle创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktAccountingTitle"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAccountingTitleCreateDtoValidator : AbstractValidator<TaktAccountingTitleCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAccountingTitleCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.accountingtitle.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.companycode"));

        RuleFor(x => x.TitleCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.accountingtitle.titlecode"))
            .Must(TaktRegexHelper.IsValidTitleCode).WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.titlecode"));

        RuleFor(x => x.TitleName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.accountingtitle.titlename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.accountingtitle.titlename", 1, 200));

        RuleFor(x => x.TitleType)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.titletype"));

        RuleFor(x => x.BalanceDirection)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.balancedirection"));

        RuleFor(x => x.IsLeaf)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.isleaf"));

        RuleFor(x => x.IsAuxiliary)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.isauxiliary"));

        RuleFor(x => x.AuxiliaryType)
            .InclusiveBetween(0, 6)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.auxiliarytype"));

        RuleFor(x => x.IsQuantity)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.isquantity"));

        RuleFor(x => x.IsCurrency)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.iscurrency"));

        RuleFor(x => x.IsCash)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.iscash"));

        RuleFor(x => x.IsBank)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.isbank"));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.accountingtitle.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.TitleStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.accountingtitle.titlestatus"));
    }
}

/// <summary>
/// AccountingTitle更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAccountingTitleCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AccountingTitleId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAccountingTitleUpdateDtoValidator : AbstractValidator<TaktAccountingTitleUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAccountingTitleUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAccountingTitleCreateDtoValidator(validationMessages));

        RuleFor(x => x.AccountingTitleId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.accountingtitle.accountingtitleid"));

        RuleFor(x => x.TitleName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.accountingtitle.titlename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TitleName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.accountingtitle.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
