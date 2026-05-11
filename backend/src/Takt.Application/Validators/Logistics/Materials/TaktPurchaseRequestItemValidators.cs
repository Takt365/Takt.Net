// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Materials
// 文件名称：TaktPurchaseRequestItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchaseRequestItem DTO 验证器（根据实体 TaktPurchaseRequestItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;

namespace Takt.Application.Validators.Logistics.Materials;

/// <summary>
/// PurchaseRequestItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchaseRequestItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPurchaseRequestItemCreateDtoValidator : AbstractValidator<TaktPurchaseRequestItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchaseRequestItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PurchaseRequestCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaserequestitem.purchaserequestcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchaserequestitem.purchaserequestcode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaserequestitem.materialcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchaserequestitem.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaserequestitem.materialname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.purchaserequestitem.materialname", 1, 200));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.RequestUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaserequestitem.requestunit"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.purchaserequestitem.requestunit", 1, 20));

        RuleFor(x => x.ReferenceSupplierCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.referencesuppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ReferenceSupplierCode));

        RuleFor(x => x.ReferenceSupplierName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.referencesuppliername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ReferenceSupplierName));
    }
}

/// <summary>
/// PurchaseRequestItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPurchaseRequestItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PurchaseRequestItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPurchaseRequestItemUpdateDtoValidator : AbstractValidator<TaktPurchaseRequestItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchaseRequestItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPurchaseRequestItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.PurchaseRequestItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.purchaserequestitem.purchaserequestitemid"));

        RuleFor(x => x.PurchaseRequestCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.purchaserequestcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseRequestCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.MaterialSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.materialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialSpecification));

        RuleFor(x => x.RequestUnit)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.requestunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestUnit));

        RuleFor(x => x.ReferenceSupplierCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.referencesuppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ReferenceSupplierCode));

        RuleFor(x => x.ReferenceSupplierName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.purchaserequestitem.referencesuppliername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ReferenceSupplierName));
    }
}
