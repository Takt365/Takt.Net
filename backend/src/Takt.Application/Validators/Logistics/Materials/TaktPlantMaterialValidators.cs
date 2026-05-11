// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Materials
// 文件名称：TaktPlantMaterialValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PlantMaterial DTO 验证器（根据实体 TaktPlantMaterial 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;

namespace Takt.Application.Validators.Logistics.Materials;

/// <summary>
/// PlantMaterial创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPlantMaterial"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPlantMaterialCreateDtoValidator : AbstractValidator<TaktPlantMaterialCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPlantMaterialCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.plantmaterial.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.plantcode"));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.plantmaterial.materialcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.plantmaterial.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.plantmaterial.materialname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.plantmaterial.materialname", 1, 200));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.MaterialDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialDescription));

        RuleFor(x => x.IndustrySector)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.industrysector", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustrySector));

        RuleFor(x => x.MaterialHierarchy)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialhierarchy", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialHierarchy));

        RuleFor(x => x.MaterialGroupCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialgroupcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialGroupCode));

        RuleFor(x => x.MaterialType)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.materialtype"));

        RuleFor(x => x.MaterialModel)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialmodel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialModel));

        RuleFor(x => x.MaterialBrand)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialbrand", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialBrand));

        RuleFor(x => x.BaseUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.plantmaterial.baseunit"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.plantmaterial.baseunit", 1, 20));

        RuleFor(x => x.PurchaseGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.purchasegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseGroup));

        RuleFor(x => x.PurchaseType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.purchasetype"));

        RuleFor(x => x.SpecialProcurement)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.specialprocurement"));

        RuleFor(x => x.IsBulk)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.isbulk"));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.manufacturer", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.ManufacturerPartNumber)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.manufacturerpartnumber", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ManufacturerPartNumber));

        RuleFor(x => x.CurrencyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.plantmaterial.currencycode"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.plantmaterial.currencycode", 1, 10));

        RuleFor(x => x.PriceControl)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.pricecontrol"));

        RuleFor(x => x.ValuationCategory)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.valuationcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ValuationCategory));

        RuleFor(x => x.DifferenceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.differencecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DifferenceCode));

        RuleFor(x => x.ProfitCenter)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.profitcenter", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProfitCenter));

        RuleFor(x => x.ProductionLocation)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.productionlocation", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLocation));

        RuleFor(x => x.PurchasingLocation)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.purchasinglocation", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchasingLocation));

        RuleFor(x => x.InspectionRequired)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.inspectionrequired"));

        RuleFor(x => x.IsBatch)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.isbatch"));

        RuleFor(x => x.IsExpiry)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.isexpiry"));

        RuleFor(x => x.MaterialStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.plantmaterial.materialstatus"));

        RuleFor(x => x.MaterialAttributes)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialattributes", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialAttributes));

        RuleFor(x => x.IsEndOfLife)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.isendoflife", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.IsEndOfLife));
    }
}

/// <summary>
/// PlantMaterial更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPlantMaterialCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PlantMaterialId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPlantMaterialUpdateDtoValidator : AbstractValidator<TaktPlantMaterialUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPlantMaterialUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPlantMaterialCreateDtoValidator(validationMessages));

        RuleFor(x => x.PlantMaterialId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.plantmaterial.plantmaterialid"));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.MaterialDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialDescription));

        RuleFor(x => x.IndustrySector)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.industrysector", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IndustrySector));

        RuleFor(x => x.MaterialHierarchy)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialhierarchy", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialHierarchy));

        RuleFor(x => x.MaterialGroupCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialgroupcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialGroupCode));

        RuleFor(x => x.MaterialModel)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialmodel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialModel));

        RuleFor(x => x.MaterialBrand)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialbrand", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialBrand));

        RuleFor(x => x.BaseUnit)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.baseunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BaseUnit));

        RuleFor(x => x.PurchaseGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.purchasegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseGroup));

        RuleFor(x => x.Manufacturer)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.manufacturer", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Manufacturer));

        RuleFor(x => x.ManufacturerPartNumber)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.manufacturerpartnumber", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ManufacturerPartNumber));

        RuleFor(x => x.CurrencyCode)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.currencycode", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrencyCode));

        RuleFor(x => x.ValuationCategory)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.valuationcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ValuationCategory));

        RuleFor(x => x.DifferenceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.differencecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DifferenceCode));

        RuleFor(x => x.ProfitCenter)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.profitcenter", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProfitCenter));

        RuleFor(x => x.ProductionLocation)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.productionlocation", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLocation));

        RuleFor(x => x.PurchasingLocation)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.purchasinglocation", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchasingLocation));

        RuleFor(x => x.MaterialAttributes)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.materialattributes", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialAttributes));

        RuleFor(x => x.IsEndOfLife)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.plantmaterial.isendoflife", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.IsEndOfLife));
    }
}
