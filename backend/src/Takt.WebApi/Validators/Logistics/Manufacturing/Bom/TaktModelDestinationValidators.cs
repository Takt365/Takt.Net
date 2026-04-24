// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktModelDestinationValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ModelDestination DTO 验证器（根据实体 TaktModelDestination 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Bom;

/// <summary>
/// ModelDestination创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktModelDestination"/> 字段对齐）。
/// </summary>
public class TaktModelDestinationCreateDtoValidator : AbstractValidator<TaktModelDestinationCreateDto>
{
    public TaktModelDestinationCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.modeldestination.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.modeldestination.materialname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.modeldestination.materialname", 1, 200));

        RuleFor(x => x.ModelName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.modeldestination.modelname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.modeldestination.modelname", 1, 200));

        RuleFor(x => x.DestinationName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.modeldestination.destinationname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.modeldestination.destinationname", 1, 200));
    }
}

/// <summary>
/// ModelDestination更新 DTO 验证器。
/// </summary>
public class TaktModelDestinationUpdateDtoValidator : AbstractValidator<TaktModelDestinationUpdateDto>
{
    public TaktModelDestinationUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktModelDestinationCreateDtoValidator(localizer));

        RuleFor(x => x.ModelDestinationId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.modeldestination.modeldestinationid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.modeldestination.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.modeldestination.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.ModelName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.modeldestination.modelname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ModelName));

        RuleFor(x => x.DestinationName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.modeldestination.destinationname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationName));
    }
}
