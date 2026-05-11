// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Serial
// 文件名称：TaktProductSerialInboundItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProductSerialInboundItem DTO 验证器（根据实体 TaktProductSerialInboundItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Serial;

namespace Takt.Application.Validators.Logistics.Serial;

/// <summary>
/// ProductSerialInboundItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Serial.TaktProductSerialInboundItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktProductSerialInboundItemCreateDtoValidator : AbstractValidator<TaktProductSerialInboundItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductSerialInboundItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.InboundNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialinbounditem.inboundno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.productserialinbounditem.inboundno", 1, 50));

        RuleFor(x => x.InboundSerialNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productserialinbounditem.inboundserialno"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.productserialinbounditem.inboundserialno", 1, 100));
    }
}

/// <summary>
/// ProductSerialInboundItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktProductSerialInboundItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ProductSerialInboundItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktProductSerialInboundItemUpdateDtoValidator : AbstractValidator<TaktProductSerialInboundItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductSerialInboundItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktProductSerialInboundItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.ProductSerialInboundItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.productserialinbounditem.productserialinbounditemid"));

        RuleFor(x => x.InboundNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productserialinbounditem.inboundno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InboundNo));

        RuleFor(x => x.InboundSerialNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.productserialinbounditem.inboundserialno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.InboundSerialNo));
    }
}
