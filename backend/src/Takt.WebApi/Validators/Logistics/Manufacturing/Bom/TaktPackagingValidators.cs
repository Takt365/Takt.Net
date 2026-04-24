// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktPackagingValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Packaging DTO 验证器（根据实体 TaktPackaging 自动生成）
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
/// Packaging创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktPackaging"/> 字段对齐）。
/// </summary>
public class TaktPackagingCreateDtoValidator : AbstractValidator<TaktPackagingCreateDto>
{
    public TaktPackagingCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.packaging.materialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.packaging.materialcode", 1, 50));

        RuleFor(x => x.HsCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.hscode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.HsCode));

        RuleFor(x => x.HsName)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.hsname", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HsName));

        RuleFor(x => x.AdditionalCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.additionalcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.AdditionalCode));

        RuleFor(x => x.OriginCountryRegionCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.origincountryregioncode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.OriginCountryRegionCode));

        RuleFor(x => x.OriginCountryRegionName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.origincountryregionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OriginCountryRegionName));

        RuleFor(x => x.DestinationCountryRegionCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.destinationcountryregioncode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationCountryRegionCode));

        RuleFor(x => x.DestinationCountryRegionName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.destinationcountryregionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationCountryRegionName));

        RuleFor(x => x.RegulatoryConditionCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.regulatoryconditioncode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegulatoryConditionCode));

        RuleFor(x => x.TariffRateType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.tariffratetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TariffRateType));

        RuleFor(x => x.WeightUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.packaging.weightunit"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.packaging.weightunit", 1, 10));

        RuleFor(x => x.VolumeUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.packaging.volumeunit"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.packaging.volumeunit", 1, 10));

        RuleFor(x => x.SizeDimension)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.sizedimension", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SizeDimension));

        RuleFor(x => x.PackagingType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.packaging.packagingtype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.packaging.packagingtype", 1, 50));

        RuleFor(x => x.PackingUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.packaging.packingunit"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.packaging.packingunit", 1, 20));

        RuleFor(x => x.PackagingSpec)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.packagingspec", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingSpec));

        RuleFor(x => x.PackagingDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.packagingdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingDescription));
    }
}

/// <summary>
/// Packaging更新 DTO 验证器。
/// </summary>
public class TaktPackagingUpdateDtoValidator : AbstractValidator<TaktPackagingUpdateDto>
{
    public TaktPackagingUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPackagingCreateDtoValidator(localizer));

        RuleFor(x => x.PackagingId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.packaging.packagingid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.plantcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.HsCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.hscode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.HsCode));

        RuleFor(x => x.HsName)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.hsname", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HsName));

        RuleFor(x => x.AdditionalCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.additionalcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.AdditionalCode));

        RuleFor(x => x.OriginCountryRegionCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.origincountryregioncode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.OriginCountryRegionCode));

        RuleFor(x => x.OriginCountryRegionName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.origincountryregionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OriginCountryRegionName));

        RuleFor(x => x.DestinationCountryRegionCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.destinationcountryregioncode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationCountryRegionCode));

        RuleFor(x => x.DestinationCountryRegionName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.destinationcountryregionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationCountryRegionName));

        RuleFor(x => x.RegulatoryConditionCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.regulatoryconditioncode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegulatoryConditionCode));

        RuleFor(x => x.TariffRateType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.tariffratetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TariffRateType));

        RuleFor(x => x.WeightUnit)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.weightunit", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.WeightUnit));

        RuleFor(x => x.VolumeUnit)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.volumeunit", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.VolumeUnit));

        RuleFor(x => x.SizeDimension)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.sizedimension", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SizeDimension));

        RuleFor(x => x.PackagingType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.packagingtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingType));

        RuleFor(x => x.PackingUnit)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.packingunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.PackingUnit));

        RuleFor(x => x.PackagingSpec)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.packagingspec", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingSpec));

        RuleFor(x => x.PackagingDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.packaging.packagingdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingDescription));
    }
}
