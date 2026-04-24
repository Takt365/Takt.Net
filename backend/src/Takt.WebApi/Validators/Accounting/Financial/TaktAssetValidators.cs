// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Financial
// 文件名称：TaktAssetValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Asset DTO 验证器（根据实体 TaktAsset 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Accounting.Financial;

/// <summary>
/// Asset创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktAsset"/> 字段对齐）。
/// </summary>
public class TaktAssetCreateDtoValidator : AbstractValidator<TaktAssetCreateDto>
{
    public TaktAssetCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.AssetCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.asset.assetcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.asset.assetcode", 1, 50));

        RuleFor(x => x.AssetName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.asset.assetname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.asset.assetname", 1, 200));

        RuleFor(x => x.AssetCategoryName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.assetcategoryname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetCategoryName));

        RuleFor(x => x.AssetType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.asset.assettype"));

        RuleFor(x => x.CostCenterName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.costcentername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.AssetLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.assetlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetLocation));

        RuleFor(x => x.DepreciationMethod)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.asset.depreciationmethod"));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.AssetStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.asset.assetstatus"));
    }
}

/// <summary>
/// Asset更新 DTO 验证器。
/// </summary>
public class TaktAssetUpdateDtoValidator : AbstractValidator<TaktAssetUpdateDto>
{
    public TaktAssetUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAssetCreateDtoValidator(localizer));

        RuleFor(x => x.AssetId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.asset.assetid"));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.AssetCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.assetcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetCode));

        RuleFor(x => x.AssetName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.assetname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetName));

        RuleFor(x => x.AssetCategoryName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.assetcategoryname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetCategoryName));

        RuleFor(x => x.CostCenterName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.costcentername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.AssetLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.assetlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetLocation));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.asset.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
