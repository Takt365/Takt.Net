// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktChangeoverValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Changeover DTO 验证器（根据实体 TaktChangeover 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;

namespace Takt.Application.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// Changeover创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktChangeover"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktChangeoverCreateDtoValidator : AbstractValidator<TaktChangeoverCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktChangeoverCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.changeover.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.changeover.plantcode"));

        RuleFor(x => x.ProductionCategory)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.changeover.productioncategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionCategory));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.changeover.productionline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));
    }
}

/// <summary>
/// Changeover更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktChangeoverCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ChangeoverId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktChangeoverUpdateDtoValidator : AbstractValidator<TaktChangeoverUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktChangeoverUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktChangeoverCreateDtoValidator(validationMessages));

        RuleFor(x => x.ChangeoverId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.changeover.changeoverid"));

        RuleFor(x => x.ProductionCategory)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.changeover.productioncategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionCategory));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.changeover.productionline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));
    }
}
