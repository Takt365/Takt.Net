// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality
// 文件名称：TaktSamplingSchemeValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SamplingScheme DTO 验证器（根据实体 TaktSamplingScheme 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Quality;

/// <summary>
/// SamplingScheme创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.TaktSamplingScheme"/> 字段对齐）。
/// </summary>
public class TaktSamplingSchemeCreateDtoValidator : AbstractValidator<TaktSamplingSchemeCreateDto>
{
    public TaktSamplingSchemeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.samplingscheme.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.SchemeCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.samplingscheme.schemecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.samplingscheme.schemecode", 1, 50));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.samplingscheme.schemename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.samplingscheme.schemename", 1, 200));

        RuleFor(x => x.SchemeType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.samplingscheme.schemetype"));

        RuleFor(x => x.SamplingStandard)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.samplingscheme.samplingstandard"));

        RuleFor(x => x.InspectionLevel)
            .InclusiveBetween(0, 6)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.samplingscheme.inspectionlevel"));

        RuleFor(x => x.InspectionStrictness)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.samplingscheme.inspectionstrictness"));

        RuleFor(x => x.IsTransferRuleEnabled)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.samplingscheme.istransferruleenabled"));

        RuleFor(x => x.TransferRuleConfig)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.samplingscheme.transferruleconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransferRuleConfig));

        RuleFor(x => x.IsEnabled)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.samplingscheme.isenabled"));

        RuleFor(x => x.SchemeStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.samplingscheme.schemestatus"));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.samplingscheme.schemedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));
    }
}

/// <summary>
/// SamplingScheme更新 DTO 验证器。
/// </summary>
public class TaktSamplingSchemeUpdateDtoValidator : AbstractValidator<TaktSamplingSchemeUpdateDto>
{
    public TaktSamplingSchemeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSamplingSchemeCreateDtoValidator(localizer));

        RuleFor(x => x.SamplingSchemeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.samplingscheme.samplingschemeid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.samplingscheme.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.SchemeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.samplingscheme.schemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeCode));

        RuleFor(x => x.SchemeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.samplingscheme.schemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.TransferRuleConfig)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.samplingscheme.transferruleconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransferRuleConfig));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.samplingscheme.schemedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));
    }
}
