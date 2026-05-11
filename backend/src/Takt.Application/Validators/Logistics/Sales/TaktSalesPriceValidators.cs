// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Sales
// 文件名称：TaktSalesPriceValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalesPrice DTO 验证器（根据实体 TaktSalesPrice 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Sales;

namespace Takt.Application.Validators.Logistics.Sales;

/// <summary>
/// SalesPrice创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Sales.TaktSalesPrice"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSalesPriceCreateDtoValidator : AbstractValidator<TaktSalesPriceCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSalesPriceCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salesprice.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.salesprice.plantcode"));

        RuleFor(x => x.SalesPriceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salesprice.salespricecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salesprice.salespricecode", 1, 50));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salesprice.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));

        RuleFor(x => x.PriceType)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.salesprice.pricetype"));

        RuleFor(x => x.PriceStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.salesprice.pricestatus"));
    }
}

/// <summary>
/// SalesPrice更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSalesPriceCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SalesPriceId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSalesPriceUpdateDtoValidator : AbstractValidator<TaktSalesPriceUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSalesPriceUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSalesPriceCreateDtoValidator(validationMessages));

        RuleFor(x => x.SalesPriceId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.salesprice.salespriceid"));

        RuleFor(x => x.SalesPriceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salesprice.salespricecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SalesPriceCode));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salesprice.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));
    }
}
