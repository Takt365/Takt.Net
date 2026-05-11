// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktModelDestinationValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ModelDestination DTO 验证器（根据实体 TaktModelDestination 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;

namespace Takt.Application.Validators.Logistics.Manufacturing.Bom;

/// <summary>
/// ModelDestination创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktModelDestination"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktModelDestinationCreateDtoValidator : AbstractValidator<TaktModelDestinationCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktModelDestinationCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.modeldestination.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.modeldestination.plantcode"));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.modeldestination.materialname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.modeldestination.materialname", 1, 200));

        RuleFor(x => x.ModelName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.modeldestination.modelname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.modeldestination.modelname", 1, 200));

        RuleFor(x => x.DestinationName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.modeldestination.destinationname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.modeldestination.destinationname", 1, 200));
    }
}

/// <summary>
/// ModelDestination更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktModelDestinationCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ModelDestinationId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktModelDestinationUpdateDtoValidator : AbstractValidator<TaktModelDestinationUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktModelDestinationUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktModelDestinationCreateDtoValidator(validationMessages));

        RuleFor(x => x.ModelDestinationId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.modeldestination.modeldestinationid"));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.modeldestination.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.ModelName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.modeldestination.modelname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ModelName));

        RuleFor(x => x.DestinationName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.modeldestination.destinationname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationName));
    }
}
