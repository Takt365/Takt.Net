// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktChangeoverValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Changeover DTO 验证器（根据实体 TaktChangeover 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// Changeover创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktChangeover"/> 字段对齐）。
/// </summary>
public class TaktChangeoverCreateDtoValidator : AbstractValidator<TaktChangeoverCreateDto>
{
    public TaktChangeoverCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.changeover.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.changeover.plantcode"));

        RuleFor(x => x.ProductionCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.changeover.productioncategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionCategory));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.changeover.productionline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));
    }
}

/// <summary>
/// Changeover更新 DTO 验证器。
/// </summary>
public class TaktChangeoverUpdateDtoValidator : AbstractValidator<TaktChangeoverUpdateDto>
{
    public TaktChangeoverUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktChangeoverCreateDtoValidator(localizer));

        RuleFor(x => x.ChangeoverId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.changeover.changeoverid"));

        RuleFor(x => x.ProductionCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.changeover.productioncategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionCategory));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.changeover.productionline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));
    }
}
