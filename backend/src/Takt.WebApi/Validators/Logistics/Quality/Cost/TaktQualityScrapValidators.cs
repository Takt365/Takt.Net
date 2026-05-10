// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityScrapValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityScrap DTO 验证器（根据实体 TaktQualityScrap 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Quality.Cost;

/// <summary>
/// QualityScrap创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityScrap"/> 字段对齐）。
/// </summary>
public class TaktQualityScrapCreateDtoValidator : AbstractValidator<TaktQualityScrapCreateDto>
{
    public TaktQualityScrapCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityscrap.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.qualityscrap.plantcode"));

        RuleFor(x => x.ScrapNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityscrap.scrapno"))
            .Length(1, 30).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.qualityscrap.scrapno", 1, 30));

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityscrap.model"))
            .Length(1, 255).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.qualityscrap.model", 1, 255));

        RuleFor(x => x.CostCurrency)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityscrap.costcurrency"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.qualityscrap.costcurrency", 1, 10));
    }
}

/// <summary>
/// QualityScrap更新 DTO 验证器。
/// </summary>
public class TaktQualityScrapUpdateDtoValidator : AbstractValidator<TaktQualityScrapUpdateDto>
{
    public TaktQualityScrapUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktQualityScrapCreateDtoValidator(localizer));

        RuleFor(x => x.QualityScrapId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityscrap.qualityscrapid"));

        RuleFor(x => x.ScrapNo)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityscrap.scrapno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.ScrapNo));

        RuleFor(x => x.Model)
            .MaximumLength(255).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityscrap.model", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.Model));

        RuleFor(x => x.CostCurrency)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityscrap.costcurrency", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCurrency));
    }
}
