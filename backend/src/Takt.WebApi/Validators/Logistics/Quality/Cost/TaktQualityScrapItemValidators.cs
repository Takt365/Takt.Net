// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityScrapItemValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityScrapItem DTO 验证器（根据实体 TaktQualityScrapItem 自动生成）
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
/// QualityScrapItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityScrapItem"/> 字段对齐）。
/// </summary>
public class TaktQualityScrapItemCreateDtoValidator : AbstractValidator<TaktQualityScrapItemCreateDto>
{
    public TaktQualityScrapItemCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityscrapitem.materialcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.qualityscrapitem.materialcode", 1, 20));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityscrapitem.materialname"))
            .Length(1, 60).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.qualityscrapitem.materialname", 1, 60));
    }
}

/// <summary>
/// QualityScrapItem更新 DTO 验证器。
/// </summary>
public class TaktQualityScrapItemUpdateDtoValidator : AbstractValidator<TaktQualityScrapItemUpdateDto>
{
    public TaktQualityScrapItemUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktQualityScrapItemCreateDtoValidator(localizer));

        RuleFor(x => x.QualityScrapItemId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityscrapitem.qualityscrapitemid"));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityscrapitem.materialcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(60).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityscrapitem.materialname", 60))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));
    }
}
