// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Serial
// 文件名称：TaktProductSerialInboundValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProductSerialInbound DTO 验证器（根据实体 TaktProductSerialInbound 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Serial;

namespace Takt.Application.Validators.Logistics.Serial;

/// <summary>
/// ProductSerialInbound创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Serial.TaktProductSerialInbound"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktProductSerialInboundCreateDtoValidator : AbstractValidator<TaktProductSerialInboundCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductSerialInboundCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialinbound.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.productserialinbound.plantcode"));

        RuleFor(x => x.InboundNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialinbound.inboundno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialinbound.inboundno", 1, 50));

        RuleFor(x => x.InboundType)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.productserialinbound.inboundtype"));

        RuleFor(x => x.WarehouseCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialinbound.warehousecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialinbound.warehousecode", 1, 50));

        RuleFor(x => x.LocationCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialinbound.locationcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialinbound.locationcode", 1, 50));

        RuleFor(x => x.RelatedCompany)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialinbound.relatedcompany"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialinbound.relatedcompany", 1, 50));
    }
}

/// <summary>
/// ProductSerialInbound更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktProductSerialInboundCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ProductSerialInboundId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktProductSerialInboundUpdateDtoValidator : AbstractValidator<TaktProductSerialInboundUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductSerialInboundUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktProductSerialInboundCreateDtoValidator(validationMessages));

        RuleFor(x => x.ProductSerialInboundId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.productserialinbound.productserialinboundid"));

        RuleFor(x => x.InboundNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialinbound.inboundno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InboundNo));

        RuleFor(x => x.WarehouseCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialinbound.warehousecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WarehouseCode));

        RuleFor(x => x.LocationCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialinbound.locationcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LocationCode));

        RuleFor(x => x.RelatedCompany)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialinbound.relatedcompany", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedCompany));
    }
}
