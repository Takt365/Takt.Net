// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Materials
// 文件名称：TaktPlantMaterialValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PlantMaterial DTO 验证器（根据实体 TaktPlantMaterial 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Materials;

/// <summary>
/// PlantMaterial创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPlantMaterial"/> 字段对齐）。
/// </summary>
public class TaktPlantMaterialCreateDtoValidator : AbstractValidator<TaktPlantMaterialCreateDto>
{
    public TaktPlantMaterialCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.plantmaterial.plantcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.plantmaterial.plantcode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.plantmaterial.materialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.plantmaterial.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.plantmaterial.materialname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.plantmaterial.materialname", 1, 200));

        RuleFor(x => x.IndustrySector)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.industrysector", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustrySector));

        RuleFor(x => x.MaterialHierarchyName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialhierarchyname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialHierarchyName));

        RuleFor(x => x.MaterialGroupCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialgroupcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialGroupCode));

        RuleFor(x => x.MaterialType)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plantmaterial.materialtype"));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.MaterialModel)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialmodel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialModel));

        RuleFor(x => x.MaterialBrand)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialbrand", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialBrand));

        RuleFor(x => x.BaseUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.plantmaterial.baseunit"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.plantmaterial.baseunit", 1, 20));

        RuleFor(x => x.PurchaseGroup)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.purchasegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseGroup));

        RuleFor(x => x.PurchaseType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plantmaterial.purchasetype"));

        RuleFor(x => x.SpecialProcurement)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plantmaterial.specialprocurement"));

        RuleFor(x => x.IsBulk)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plantmaterial.isbulk"));

        RuleFor(x => x.SupplierName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.suppliername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierName));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.manufacturer", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.ManufacturerPartNumber)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.manufacturerpartnumber", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ManufacturerPartNumber));

        RuleFor(x => x.CurrencyCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.plantmaterial.currencycode"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.plantmaterial.currencycode", 1, 10));

        RuleFor(x => x.PriceControl)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plantmaterial.pricecontrol"));

        RuleFor(x => x.ValuationCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.valuationcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ValuationCategory));

        RuleFor(x => x.DifferenceCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.differencecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DifferenceCode));

        RuleFor(x => x.ProfitCenter)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.profitcenter", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProfitCenter));

        RuleFor(x => x.ProductionLocation)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.productionlocation", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLocation));

        RuleFor(x => x.PurchasingLocation)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.purchasinglocation", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchasingLocation));

        RuleFor(x => x.InspectionRequired)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plantmaterial.inspectionrequired"));

        RuleFor(x => x.IsBatch)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plantmaterial.isbatch"));

        RuleFor(x => x.IsExpiry)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plantmaterial.isexpiry"));

        RuleFor(x => x.MaterialStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.plantmaterial.materialstatus"));

        RuleFor(x => x.MaterialAttributes)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialattributes", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialAttributes));

        RuleFor(x => x.MaterialDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialDescription));

        RuleFor(x => x.IsEndOfLife)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.isendoflife", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.IsEndOfLife));
    }
}

/// <summary>
/// PlantMaterial更新 DTO 验证器。
/// </summary>
public class TaktPlantMaterialUpdateDtoValidator : AbstractValidator<TaktPlantMaterialUpdateDto>
{
    public TaktPlantMaterialUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPlantMaterialCreateDtoValidator(localizer));

        RuleFor(x => x.PlantMaterialId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.plantmaterial.plantmaterialid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.IndustrySector)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.industrysector", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustrySector));

        RuleFor(x => x.MaterialHierarchyName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialhierarchyname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialHierarchyName));

        RuleFor(x => x.MaterialGroupCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialgroupcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialGroupCode));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.MaterialModel)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialmodel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialModel));

        RuleFor(x => x.MaterialBrand)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialbrand", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialBrand));

        RuleFor(x => x.BaseUnit)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.baseunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BaseUnit));

        RuleFor(x => x.PurchaseGroup)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.purchasegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseGroup));

        RuleFor(x => x.SupplierName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.suppliername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierName));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.manufacturer", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.ManufacturerPartNumber)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.manufacturerpartnumber", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ManufacturerPartNumber));

        RuleFor(x => x.CurrencyCode)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.currencycode", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrencyCode));

        RuleFor(x => x.ValuationCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.valuationcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ValuationCategory));

        RuleFor(x => x.DifferenceCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.differencecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DifferenceCode));

        RuleFor(x => x.ProfitCenter)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.profitcenter", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProfitCenter));

        RuleFor(x => x.ProductionLocation)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.productionlocation", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLocation));

        RuleFor(x => x.PurchasingLocation)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.purchasinglocation", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchasingLocation));

        RuleFor(x => x.MaterialAttributes)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialattributes", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialAttributes));

        RuleFor(x => x.MaterialDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.materialdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialDescription));

        RuleFor(x => x.IsEndOfLife)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.plantmaterial.isendoflife", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.IsEndOfLife));
    }
}
