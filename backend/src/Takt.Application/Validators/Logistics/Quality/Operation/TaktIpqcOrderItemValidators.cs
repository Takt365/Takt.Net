// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IpqcOrderItem DTO 验证器（根据实体 TaktIpqcOrderItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// IpqcOrderItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIpqcOrderItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktIpqcOrderItemCreateDtoValidator : AbstractValidator<TaktIpqcOrderItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIpqcOrderItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.IpqcOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorderitem.ipqcordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ipqcorderitem.ipqcordercode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorderitem.materialcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ipqcorderitem.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorderitem.materialname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.ipqcorderitem.materialname", 1, 200));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.StandardCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorderitem.standardcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ipqcorderitem.standardcode", 1, 50));

        RuleFor(x => x.SamplingSchemeCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorderitem.samplingschemecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ipqcorderitem.samplingschemecode", 1, 50));

        RuleFor(x => x.InspectionMethod)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.ipqcorderitem.inspectionmethod"));

        RuleFor(x => x.JudgeStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.ipqcorderitem.judgestatus"));

        RuleFor(x => x.SampleSerialNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.sampleserialno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SampleSerialNo));

        RuleFor(x => x.InspectionDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.inspectiondescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionDescription));

        RuleFor(x => x.InspectorBy)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorderitem.inspectorby"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ipqcorderitem.inspectorby", 1, 50));
    }
}

/// <summary>
/// IpqcOrderItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktIpqcOrderItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>IpqcOrderItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktIpqcOrderItemUpdateDtoValidator : AbstractValidator<TaktIpqcOrderItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIpqcOrderItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktIpqcOrderItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.IpqcOrderItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ipqcorderitem.ipqcorderitemid"));

        RuleFor(x => x.IpqcOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.ipqcordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IpqcOrderCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.StandardCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.standardcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardCode));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.SampleSerialNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.sampleserialno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SampleSerialNo));

        RuleFor(x => x.InspectionDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.inspectiondescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionDescription));

        RuleFor(x => x.InspectorBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorderitem.inspectorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectorBy));
    }
}
