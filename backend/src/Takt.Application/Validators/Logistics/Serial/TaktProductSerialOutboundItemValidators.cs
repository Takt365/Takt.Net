// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Serial
// 文件名称：TaktProductSerialOutboundItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProductSerialOutboundItem DTO 验证器（根据实体 TaktProductSerialOutboundItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Serial;

namespace Takt.Application.Validators.Logistics.Serial;

/// <summary>
/// ProductSerialOutboundItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Serial.TaktProductSerialOutboundItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktProductSerialOutboundItemCreateDtoValidator : AbstractValidator<TaktProductSerialOutboundItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductSerialOutboundItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.OutboundNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbounditem.outboundno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbounditem.outboundno", 1, 50));

        RuleFor(x => x.OutboundSerialNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbounditem.outboundserialno"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbounditem.outboundserialno", 1, 100));

        RuleFor(x => x.ReferenceInboundNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialoutbounditem.referenceinboundno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialoutbounditem.referenceinboundno", 1, 50));
    }
}

/// <summary>
/// ProductSerialOutboundItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktProductSerialOutboundItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ProductSerialOutboundItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktProductSerialOutboundItemUpdateDtoValidator : AbstractValidator<TaktProductSerialOutboundItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductSerialOutboundItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktProductSerialOutboundItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.ProductSerialOutboundItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.productserialoutbounditem.productserialoutbounditemid"));

        RuleFor(x => x.OutboundNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialoutbounditem.outboundno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundNo));

        RuleFor(x => x.OutboundSerialNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.productserialoutbounditem.outboundserialno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundSerialNo));

        RuleFor(x => x.ReferenceInboundNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialoutbounditem.referenceinboundno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ReferenceInboundNo));
    }
}
