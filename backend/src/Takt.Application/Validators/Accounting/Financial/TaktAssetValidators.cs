// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Accounting.Financial
// 文件名称：TaktAssetValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Asset DTO 验证器（根据实体 TaktAsset 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Financial;

namespace Takt.Application.Validators.Accounting.Financial;

/// <summary>
/// Asset创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktAsset"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAssetCreateDtoValidator : AbstractValidator<TaktAssetCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAssetCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.asset.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.asset.companycode"));

        RuleFor(x => x.AssetCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.asset.assetcode"))
            .Must(TaktRegexHelper.IsValidAssetCode).WithMessage(_validationMessages.FormatInvalid("entity.asset.assetcode"));

        RuleFor(x => x.AssetName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.asset.assetname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.asset.assetname", 1, 200));

        RuleFor(x => x.AssetCategoryName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.asset.assetcategoryname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetCategoryName));

        RuleFor(x => x.AssetType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.asset.assettype"));

        RuleFor(x => x.CostCenterName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.asset.costcentername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.asset.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.AssetLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.asset.assetlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetLocation));

        RuleFor(x => x.DepreciationMethod)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.asset.depreciationmethod"));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.asset.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.AssetStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.asset.assetstatus"));
    }
}

/// <summary>
/// Asset更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAssetCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AssetId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAssetUpdateDtoValidator : AbstractValidator<TaktAssetUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAssetUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAssetCreateDtoValidator(validationMessages));

        RuleFor(x => x.AssetId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.asset.assetid"));

        RuleFor(x => x.AssetName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.asset.assetname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetName));

        RuleFor(x => x.AssetCategoryName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.asset.assetcategoryname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetCategoryName));

        RuleFor(x => x.CostCenterName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.asset.costcentername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.asset.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.AssetLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.asset.assetlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.AssetLocation));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.asset.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
