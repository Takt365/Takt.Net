// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktIqcOrderItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IqcOrderItem DTO 验证器（根据实体 TaktIqcOrderItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// IqcOrderItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIqcOrderItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktIqcOrderItemCreateDtoValidator : AbstractValidator<TaktIqcOrderItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIqcOrderItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.IqcOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorderitem.iqcordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcorderitem.iqcordercode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorderitem.materialcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcorderitem.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorderitem.materialname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.iqcorderitem.materialname", 1, 200));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.StandardCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorderitem.standardcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcorderitem.standardcode", 1, 50));

        RuleFor(x => x.SamplingSchemeCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorderitem.samplingschemecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcorderitem.samplingschemecode", 1, 50));

        RuleFor(x => x.InspectionMethod)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.iqcorderitem.inspectionmethod"));

        RuleFor(x => x.JudgeStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.iqcorderitem.judgestatus"));

        RuleFor(x => x.SampleSerialNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.sampleserialno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SampleSerialNo));

        RuleFor(x => x.InspectionDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.inspectiondescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionDescription));

        RuleFor(x => x.InspectorBy)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorderitem.inspectorby"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcorderitem.inspectorby", 1, 50));
    }
}

/// <summary>
/// IqcOrderItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktIqcOrderItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>IqcOrderItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktIqcOrderItemUpdateDtoValidator : AbstractValidator<TaktIqcOrderItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIqcOrderItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktIqcOrderItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.IqcOrderItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.iqcorderitem.iqcorderitemid"));

        RuleFor(x => x.IqcOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.iqcordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IqcOrderCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.StandardCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.standardcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardCode));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.SampleSerialNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.sampleserialno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SampleSerialNo));

        RuleFor(x => x.InspectionDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.inspectiondescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionDescription));

        RuleFor(x => x.InspectorBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorderitem.inspectorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectorBy));
    }
}
