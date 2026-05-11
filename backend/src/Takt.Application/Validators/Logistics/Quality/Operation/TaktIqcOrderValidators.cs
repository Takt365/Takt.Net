// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktIqcOrderValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IqcOrder DTO 验证器（根据实体 TaktIqcOrder 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// IqcOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIqcOrder"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktIqcOrderCreateDtoValidator : AbstractValidator<TaktIqcOrderCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIqcOrderCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorder.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.iqcorder.plantcode"));

        RuleFor(x => x.SourceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorder.sourcecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcorder.sourcecode", 1, 50));

        RuleFor(x => x.IqcOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorder.iqcordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcorder.iqcordercode", 1, 50));

        RuleFor(x => x.SupplierCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcorder.suppliercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcorder.suppliercode", 1, 50));

        RuleFor(x => x.JudgeStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.iqcorder.judgestatus"));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.JudgeDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.iqcorder.judgedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeDescription));
    }
}

/// <summary>
/// IqcOrder更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktIqcOrderCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>IqcOrderId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktIqcOrderUpdateDtoValidator : AbstractValidator<TaktIqcOrderUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIqcOrderUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktIqcOrderCreateDtoValidator(validationMessages));

        RuleFor(x => x.IqcOrderId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.iqcorder.iqcorderid"));

        RuleFor(x => x.SourceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorder.sourcecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SourceCode));

        RuleFor(x => x.IqcOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorder.iqcordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IqcOrderCode));

        RuleFor(x => x.SupplierCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorder.suppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierCode));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.JudgeDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.iqcorder.judgedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeDescription));
    }
}
