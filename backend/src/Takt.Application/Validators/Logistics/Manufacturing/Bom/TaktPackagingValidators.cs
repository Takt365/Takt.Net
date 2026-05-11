// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktPackagingValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Packaging DTO 验证器（根据实体 TaktPackaging 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;

namespace Takt.Application.Validators.Logistics.Manufacturing.Bom;

/// <summary>
/// Packaging创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktPackaging"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPackagingCreateDtoValidator : AbstractValidator<TaktPackagingCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPackagingCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.packaging.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.packaging.plantcode"));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.packaging.materialcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.packaging.materialcode", 1, 50));

        RuleFor(x => x.HsCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.packaging.hscode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.HsCode));

        RuleFor(x => x.HsName)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.packaging.hsname", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HsName));

        RuleFor(x => x.AdditionalCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.packaging.additionalcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.AdditionalCode));

        RuleFor(x => x.OriginCountryRegionCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.packaging.origincountryregioncode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.OriginCountryRegionCode));

        RuleFor(x => x.OriginCountryRegionName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.packaging.origincountryregionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OriginCountryRegionName));

        RuleFor(x => x.DestinationCountryRegionCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.packaging.destinationcountryregioncode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationCountryRegionCode));

        RuleFor(x => x.DestinationCountryRegionName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.packaging.destinationcountryregionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationCountryRegionName));

        RuleFor(x => x.RegulatoryConditionCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.packaging.regulatoryconditioncode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegulatoryConditionCode));

        RuleFor(x => x.TariffRateType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.packaging.tariffratetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TariffRateType));

        RuleFor(x => x.WeightUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.packaging.weightunit"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.packaging.weightunit", 1, 10));

        RuleFor(x => x.VolumeUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.packaging.volumeunit"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.packaging.volumeunit", 1, 10));

        RuleFor(x => x.SizeDimension)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.packaging.sizedimension", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SizeDimension));

        RuleFor(x => x.PackagingType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.packaging.packagingtype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.packaging.packagingtype", 1, 50));

        RuleFor(x => x.PackingUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.packaging.packingunit"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.packaging.packingunit", 1, 20));

        RuleFor(x => x.PackagingSpec)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.packaging.packagingspec", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingSpec));

        RuleFor(x => x.PackagingDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.packaging.packagingdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingDescription));
    }
}

/// <summary>
/// Packaging更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPackagingCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PackagingId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPackagingUpdateDtoValidator : AbstractValidator<TaktPackagingUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPackagingUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPackagingCreateDtoValidator(validationMessages));

        RuleFor(x => x.PackagingId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.packaging.packagingid"));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.packaging.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.HsCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.packaging.hscode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.HsCode));

        RuleFor(x => x.HsName)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.packaging.hsname", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HsName));

        RuleFor(x => x.AdditionalCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.packaging.additionalcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.AdditionalCode));

        RuleFor(x => x.OriginCountryRegionCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.packaging.origincountryregioncode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.OriginCountryRegionCode));

        RuleFor(x => x.OriginCountryRegionName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.packaging.origincountryregionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OriginCountryRegionName));

        RuleFor(x => x.DestinationCountryRegionCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.packaging.destinationcountryregioncode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationCountryRegionCode));

        RuleFor(x => x.DestinationCountryRegionName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.packaging.destinationcountryregionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DestinationCountryRegionName));

        RuleFor(x => x.RegulatoryConditionCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.packaging.regulatoryconditioncode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RegulatoryConditionCode));

        RuleFor(x => x.TariffRateType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.packaging.tariffratetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TariffRateType));

        RuleFor(x => x.WeightUnit)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.packaging.weightunit", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.WeightUnit));

        RuleFor(x => x.VolumeUnit)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.packaging.volumeunit", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.VolumeUnit));

        RuleFor(x => x.SizeDimension)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.packaging.sizedimension", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SizeDimension));

        RuleFor(x => x.PackagingType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.packaging.packagingtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingType));

        RuleFor(x => x.PackingUnit)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.packaging.packingunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.PackingUnit));

        RuleFor(x => x.PackagingSpec)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.packaging.packagingspec", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingSpec));

        RuleFor(x => x.PackagingDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.packaging.packagingdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PackagingDescription));
    }
}
