// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaRepairValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PcbaRepair DTO 验证器（根据实体 TaktPcbaRepair 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Defect;

/// <summary>
/// PcbaRepair创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Defect.TaktPcbaRepair"/> 字段对齐）。
/// </summary>
public class TaktPcbaRepairCreateDtoValidator : AbstractValidator<TaktPcbaRepairCreateDto>
{
    public TaktPcbaRepairCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbarepair.plantcode"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbarepair.plantcode", 1, 8));

        RuleFor(x => x.ProdCategory)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbarepair.prodcategory"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbarepair.prodcategory", 1, 20));

        RuleFor(x => x.ProdLine)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbarepair.prodline"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbarepair.prodline", 1, 20));

        RuleFor(x => x.ShiftNo)
            .InclusiveBetween(1, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.pcbarepair.shiftno"));

        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbarepair.prodordercode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbarepair.prodordercode", 1, 20));

        RuleFor(x => x.ModelCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbarepair.modelcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbarepair.modelcode", 1, 20));

        RuleFor(x => x.BatchNo)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepair.batchno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbarepair.materialcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbarepair.materialcode", 1, 20));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.pcbarepair.status"));
    }
}

/// <summary>
/// PcbaRepair更新 DTO 验证器。
/// </summary>
public class TaktPcbaRepairUpdateDtoValidator : AbstractValidator<TaktPcbaRepairUpdateDto>
{
    public TaktPcbaRepairUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPcbaRepairCreateDtoValidator(localizer));

        RuleFor(x => x.PcbaRepairId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbarepair.pcbarepairid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepair.plantcode", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.ProdCategory)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepair.prodcategory", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdCategory));

        RuleFor(x => x.ProdLine)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepair.prodline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepair.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.ModelCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepair.modelcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ModelCode));

        RuleFor(x => x.BatchNo)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepair.batchno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepair.materialcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));
    }
}
