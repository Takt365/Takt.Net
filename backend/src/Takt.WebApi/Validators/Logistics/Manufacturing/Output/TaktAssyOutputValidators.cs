// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AssyOutput DTO 验证器（根据实体 TaktAssyOutput 自动生成）
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
/// AssyOutput创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktAssyOutput"/> 字段对齐）。
/// </summary>
public class TaktAssyOutputCreateDtoValidator : AbstractValidator<TaktAssyOutputCreateDto>
{
    public TaktAssyOutputCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.assyoutput.plantcode"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.assyoutput.plantcode", 1, 8));

        RuleFor(x => x.ProdCategory)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.assyoutput.prodcategory"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.assyoutput.prodcategory", 1, 20));

        RuleFor(x => x.ProdLine)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.assyoutput.prodline"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.assyoutput.prodline", 1, 20));

        RuleFor(x => x.ShiftNo)
            .InclusiveBetween(1, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.assyoutput.shiftno"));

        RuleFor(x => x.ProdOrderType)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.prodordertype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderType));

        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.assyoutput.prodordercode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.assyoutput.prodordercode", 1, 20));

        RuleFor(x => x.ModelCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.assyoutput.modelcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.assyoutput.modelcode", 1, 20));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.assyoutput.materialcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.assyoutput.materialcode", 1, 20));

        RuleFor(x => x.BatchNo)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.batchno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));
    }
}

/// <summary>
/// AssyOutput更新 DTO 验证器。
/// </summary>
public class TaktAssyOutputUpdateDtoValidator : AbstractValidator<TaktAssyOutputUpdateDto>
{
    public TaktAssyOutputUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAssyOutputCreateDtoValidator(localizer));

        RuleFor(x => x.AssyOutputId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.assyoutput.assyoutputid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.plantcode", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.ProdCategory)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.prodcategory", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdCategory));

        RuleFor(x => x.ProdLine)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.prodline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.ProdOrderType)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.prodordertype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderType));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.ModelCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.modelcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ModelCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.materialcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.BatchNo)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutput.batchno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));
    }
}
