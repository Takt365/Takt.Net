// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktSamplingSchemeValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SamplingScheme DTO 验证器（根据实体 TaktSamplingScheme 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// SamplingScheme创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktSamplingScheme"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSamplingSchemeCreateDtoValidator : AbstractValidator<TaktSamplingSchemeCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSamplingSchemeCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.samplingscheme.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.samplingscheme.plantcode"));

        RuleFor(x => x.SamplingSchemeCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.samplingscheme.samplingschemecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.samplingscheme.samplingschemecode", 1, 50));

        RuleFor(x => x.SamplingSchemeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.samplingscheme.samplingschemename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.samplingscheme.samplingschemename", 1, 200));

        RuleFor(x => x.SamplingSchemeType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.samplingscheme.samplingschemetype"));

        RuleFor(x => x.SamplingStandard)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.samplingscheme.samplingstandard"));

        RuleFor(x => x.InspectionLevel)
            .InclusiveBetween(0, 6)
            .WithMessage(_validationMessages.FormatInvalid("entity.samplingscheme.inspectionlevel"));

        RuleFor(x => x.InspectionStrictness)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.samplingscheme.inspectionstrictness"));

        RuleFor(x => x.IsTransferRuleEnabled)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.samplingscheme.istransferruleenabled"));

        RuleFor(x => x.TransferRuleConfig)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.samplingscheme.transferruleconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransferRuleConfig));

        RuleFor(x => x.SamplingSchemeStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.samplingscheme.samplingschemestatus"));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.samplingscheme.schemedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));
    }
}

/// <summary>
/// SamplingScheme更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSamplingSchemeCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SamplingSchemeId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSamplingSchemeUpdateDtoValidator : AbstractValidator<TaktSamplingSchemeUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSamplingSchemeUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSamplingSchemeCreateDtoValidator(validationMessages));

        RuleFor(x => x.SamplingSchemeId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.samplingscheme.samplingschemeid"));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.samplingscheme.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.SamplingSchemeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.samplingscheme.samplingschemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeName));

        RuleFor(x => x.TransferRuleConfig)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.samplingscheme.transferruleconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransferRuleConfig));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.samplingscheme.schemedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));
    }
}
