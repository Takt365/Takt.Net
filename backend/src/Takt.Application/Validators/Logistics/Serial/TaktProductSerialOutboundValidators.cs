// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Serial
// 文件名称：TaktProductSerialOutboundValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProductSerialOutbound DTO 验证器（根据实体 TaktProductSerialOutbound 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Serial;

namespace Takt.Application.Validators.Logistics.Serial;

/// <summary>
/// ProductSerialOutbound创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Serial.TaktProductSerialOutbound"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktProductSerialOutboundCreateDtoValidator : AbstractValidator<TaktProductSerialOutboundCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductSerialOutboundCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbound.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.productserialoutbound.plantcode"));

        RuleFor(x => x.OutboundNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbound.outboundno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbound.outboundno", 1, 50));

        RuleFor(x => x.ShippingInvoiceNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbound.shippinginvoiceno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbound.shippinginvoiceno", 1, 50));

        RuleFor(x => x.Destination)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbound.destination"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbound.destination", 1, 200));

        RuleFor(x => x.ShippingMethod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbound.shippingmethod"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbound.shippingmethod", 1, 50));

        RuleFor(x => x.DestinationPort)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbound.destinationport"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbound.destinationport", 1, 200));

        RuleFor(x => x.OutboundType)
            .InclusiveBetween(0, 6)
            .WithMessage(_validationMessages.FormatInvalid("entity.productserialoutbound.outboundtype"));

        RuleFor(x => x.WarehouseCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbound.warehousecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbound.warehousecode", 1, 50));

        RuleFor(x => x.LocationCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbound.locationcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbound.locationcode", 1, 50));

        RuleFor(x => x.RelatedCompany)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbound.relatedcompany"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbound.relatedcompany", 1, 50));
    }
}

/// <summary>
/// ProductSerialOutbound更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktProductSerialOutboundCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ProductSerialOutboundId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktProductSerialOutboundUpdateDtoValidator : AbstractValidator<TaktProductSerialOutboundUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductSerialOutboundUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktProductSerialOutboundCreateDtoValidator(validationMessages));

        RuleFor(x => x.ProductSerialOutboundId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.productserialoutbound.productserialoutboundid"));

        RuleFor(x => x.OutboundNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialoutbound.outboundno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundNo));

        RuleFor(x => x.ShippingInvoiceNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialoutbound.shippinginvoiceno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ShippingInvoiceNo));

        RuleFor(x => x.Destination)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.productserialoutbound.destination", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Destination));

        RuleFor(x => x.ShippingMethod)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialoutbound.shippingmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ShippingMethod));

        RuleFor(x => x.DestinationPort)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.productserialoutbound.destinationport", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationPort));

        RuleFor(x => x.WarehouseCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialoutbound.warehousecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WarehouseCode));

        RuleFor(x => x.LocationCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialoutbound.locationcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LocationCode));

        RuleFor(x => x.RelatedCompany)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialoutbound.relatedcompany", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedCompany));
    }
}
