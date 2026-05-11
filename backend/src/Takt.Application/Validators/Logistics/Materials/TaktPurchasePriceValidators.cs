// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Materials
// 文件名称：TaktPurchasePriceValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchasePrice DTO 验证器（根据实体 TaktPurchasePrice 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;

namespace Takt.Application.Validators.Logistics.Materials;

/// <summary>
/// PurchasePrice创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchasePrice"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPurchasePriceCreateDtoValidator : AbstractValidator<TaktPurchasePriceCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchasePriceCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaseprice.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.purchaseprice.plantcode"));

        RuleFor(x => x.PurchasePriceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaseprice.purchasepricecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchaseprice.purchasepricecode", 1, 50));

        RuleFor(x => x.SupplierCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaseprice.suppliercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchaseprice.suppliercode", 1, 50));

        RuleFor(x => x.PriceType)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.purchaseprice.pricetype"));

        RuleFor(x => x.PriceStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.purchaseprice.pricestatus"));
    }
}

/// <summary>
/// PurchasePrice更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPurchasePriceCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PurchasePriceId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPurchasePriceUpdateDtoValidator : AbstractValidator<TaktPurchasePriceUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchasePriceUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPurchasePriceCreateDtoValidator(validationMessages));

        RuleFor(x => x.PurchasePriceId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.purchaseprice.purchasepriceid"));

        RuleFor(x => x.PurchasePriceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaseprice.purchasepricecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchasePriceCode));

        RuleFor(x => x.SupplierCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaseprice.suppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierCode));
    }
}
