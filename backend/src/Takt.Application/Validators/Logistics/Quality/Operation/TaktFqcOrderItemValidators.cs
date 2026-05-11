// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktFqcOrderItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FqcOrderItem DTO 验证器（根据实体 TaktFqcOrderItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// FqcOrderItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktFqcOrderItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFqcOrderItemCreateDtoValidator : AbstractValidator<TaktFqcOrderItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFqcOrderItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.FqcOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcorderitem.fqcordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcorderitem.fqcordercode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcorderitem.materialcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcorderitem.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcorderitem.materialname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.fqcorderitem.materialname", 1, 200));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.StandardCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcorderitem.standardcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcorderitem.standardcode", 1, 50));

        RuleFor(x => x.SamplingSchemeCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcorderitem.samplingschemecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcorderitem.samplingschemecode", 1, 50));

        RuleFor(x => x.InspectionMethod)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.fqcorderitem.inspectionmethod"));

        RuleFor(x => x.JudgeStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.fqcorderitem.judgestatus"));

        RuleFor(x => x.SampleSerialNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.sampleserialno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SampleSerialNo));

        RuleFor(x => x.InspectionDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.inspectiondescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionDescription));

        RuleFor(x => x.InspectorBy)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcorderitem.inspectorby"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcorderitem.inspectorby", 1, 50));
    }
}

/// <summary>
/// FqcOrderItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFqcOrderItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FqcOrderItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFqcOrderItemUpdateDtoValidator : AbstractValidator<TaktFqcOrderItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFqcOrderItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFqcOrderItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.FqcOrderItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.fqcorderitem.fqcorderitemid"));

        RuleFor(x => x.FqcOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.fqcordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FqcOrderCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.StandardCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.standardcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardCode));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.SampleSerialNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.sampleserialno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SampleSerialNo));

        RuleFor(x => x.InspectionDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.inspectiondescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionDescription));

        RuleFor(x => x.InspectorBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorderitem.inspectorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectorBy));
    }
}
