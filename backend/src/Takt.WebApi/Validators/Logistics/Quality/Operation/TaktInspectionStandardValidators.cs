// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Operation
// 文件名称：TaktInspectionStandardValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：InspectionStandard DTO 验证器（根据实体 TaktInspectionStandard 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Quality.Operation;

/// <summary>
/// InspectionStandard创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktInspectionStandard"/> 字段对齐）。
/// </summary>
public class TaktInspectionStandardCreateDtoValidator : AbstractValidator<TaktInspectionStandardCreateDto>
{
    public TaktInspectionStandardCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.inspectionstandard.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.inspectionstandard.plantcode"));

        RuleFor(x => x.StandardCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.inspectionstandard.standardcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.inspectionstandard.standardcode", 1, 50));

        RuleFor(x => x.StandardName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.inspectionstandard.standardname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.inspectionstandard.standardname", 1, 200));

        RuleFor(x => x.InspectionType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.inspectionstandard.inspectiontype"));

        RuleFor(x => x.MaterialCategoryCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.materialcategorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCategoryCode));

        RuleFor(x => x.MaterialCategoryName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.materialcategoryname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCategoryName));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.InspectionMethod)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.inspectionmethod", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionMethod));

        RuleFor(x => x.InspectionTools)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.inspectiontools", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionTools));

        RuleFor(x => x.JudgmentRules)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.judgmentrules", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgmentRules));

        RuleFor(x => x.IsEnabled)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.inspectionstandard.isenabled"));

        RuleFor(x => x.StandardStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.inspectionstandard.standardstatus"));

        RuleFor(x => x.StandardDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.standarddescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardDescription));
    }
}

/// <summary>
/// InspectionStandard更新 DTO 验证器。
/// </summary>
public class TaktInspectionStandardUpdateDtoValidator : AbstractValidator<TaktInspectionStandardUpdateDto>
{
    public TaktInspectionStandardUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktInspectionStandardCreateDtoValidator(localizer));

        RuleFor(x => x.InspectionStandardId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.inspectionstandard.inspectionstandardid"));

        RuleFor(x => x.StandardCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.standardcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardCode));

        RuleFor(x => x.StandardName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.standardname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardName));

        RuleFor(x => x.MaterialCategoryCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.materialcategorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCategoryCode));

        RuleFor(x => x.MaterialCategoryName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.materialcategoryname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCategoryName));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.InspectionMethod)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.inspectionmethod", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionMethod));

        RuleFor(x => x.InspectionTools)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.inspectiontools", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionTools));

        RuleFor(x => x.JudgmentRules)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.judgmentrules", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgmentRules));

        RuleFor(x => x.StandardDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.inspectionstandard.standarddescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardDescription));
    }
}
