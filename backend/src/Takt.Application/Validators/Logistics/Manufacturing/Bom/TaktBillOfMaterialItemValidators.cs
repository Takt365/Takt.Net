// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：BillOfMaterialItem DTO 验证器（根据实体 TaktBillOfMaterialItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;

namespace Takt.Application.Validators.Logistics.Manufacturing.Bom;

/// <summary>
/// BillOfMaterialItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktBillOfMaterialItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktBillOfMaterialItemCreateDtoValidator : AbstractValidator<TaktBillOfMaterialItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktBillOfMaterialItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.BomCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterialitem.bomcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.billofmaterialitem.bomcode", 1, 50));

        RuleFor(x => x.ChildMaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterialitem.childmaterialcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.billofmaterialitem.childmaterialcode", 1, 50));

        RuleFor(x => x.ChildMaterialName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterialitem.childmaterialname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.billofmaterialitem.childmaterialname", 1, 200));

        RuleFor(x => x.ChildMaterialSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.billofmaterialitem.childmaterialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialSpecification));

        RuleFor(x => x.ChildMaterialUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterialitem.childmaterialunit"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.billofmaterialitem.childmaterialunit", 1, 20));

        RuleFor(x => x.IsSubstitute)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.billofmaterialitem.issubstitute"));

        RuleFor(x => x.IsRequired)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.billofmaterialitem.isrequired"));

        RuleFor(x => x.IsPhantom)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.billofmaterialitem.isphantom"));

        RuleFor(x => x.IsCritical)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.billofmaterialitem.iscritical"));
    }
}

/// <summary>
/// BillOfMaterialItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktBillOfMaterialItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>BillOfMaterialItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktBillOfMaterialItemUpdateDtoValidator : AbstractValidator<TaktBillOfMaterialItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktBillOfMaterialItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktBillOfMaterialItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.BillOfMaterialItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.billofmaterialitem.billofmaterialitemid"));

        RuleFor(x => x.BomCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.billofmaterialitem.bomcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BomCode));

        RuleFor(x => x.ChildMaterialCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.billofmaterialitem.childmaterialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialCode));

        RuleFor(x => x.ChildMaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.billofmaterialitem.childmaterialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialName));

        RuleFor(x => x.ChildMaterialSpecification)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.billofmaterialitem.childmaterialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialSpecification));

        RuleFor(x => x.ChildMaterialUnit)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.billofmaterialitem.childmaterialunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialUnit));
    }
}
