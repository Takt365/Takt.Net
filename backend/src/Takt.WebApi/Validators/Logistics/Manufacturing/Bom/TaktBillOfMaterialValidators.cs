// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：BillOfMaterial DTO 验证器（根据实体 TaktBillOfMaterial 自动生成）
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
/// BillOfMaterial创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktBillOfMaterial"/> 字段对齐）。
/// </summary>
public class TaktBillOfMaterialCreateDtoValidator : AbstractValidator<TaktBillOfMaterialCreateDto>
{
    public TaktBillOfMaterialCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.BomCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterial.bomcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterial.bomcode", 1, 50));

        RuleFor(x => x.BomName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterial.bomname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterial.bomname", 1, 200));

        RuleFor(x => x.ParentMaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterial.parentmaterialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterial.parentmaterialcode", 1, 50));

        RuleFor(x => x.ParentMaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterial.parentmaterialname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterial.parentmaterialname", 1, 200));

        RuleFor(x => x.BomVersion)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterial.bomversion"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterial.bomversion", 1, 20));

        RuleFor(x => x.BomType)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.billofmaterial.bomtype"));

        RuleFor(x => x.ParentMaterialUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterial.parentmaterialunit"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterial.parentmaterialunit", 1, 20));

        RuleFor(x => x.IsEnabled)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.billofmaterial.isenabled"));

        RuleFor(x => x.BomStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.billofmaterial.bomstatus"));

        RuleFor(x => x.BomDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.bomdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.BomDescription));
    }
}

/// <summary>
/// BillOfMaterial更新 DTO 验证器。
/// </summary>
public class TaktBillOfMaterialUpdateDtoValidator : AbstractValidator<TaktBillOfMaterialUpdateDto>
{
    public TaktBillOfMaterialUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktBillOfMaterialCreateDtoValidator(localizer));

        RuleFor(x => x.BillOfMaterialId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterial.billofmaterialid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.BomCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.bomcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BomCode));

        RuleFor(x => x.BomName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.bomname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.BomName));

        RuleFor(x => x.ParentMaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.parentmaterialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ParentMaterialCode));

        RuleFor(x => x.ParentMaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.parentmaterialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ParentMaterialName));

        RuleFor(x => x.BomVersion)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.bomversion", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BomVersion));

        RuleFor(x => x.ParentMaterialUnit)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.parentmaterialunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ParentMaterialUnit));

        RuleFor(x => x.BomDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterial.bomdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.BomDescription));
    }
}
