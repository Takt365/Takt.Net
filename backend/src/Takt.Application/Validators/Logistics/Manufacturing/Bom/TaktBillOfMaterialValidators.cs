// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：BillOfMaterial DTO 验证器（根据实体 TaktBillOfMaterial 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;

namespace Takt.Application.Validators.Logistics.Manufacturing.Bom;

/// <summary>
/// BillOfMaterial创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktBillOfMaterial"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktBillOfMaterialCreateDtoValidator : AbstractValidator<TaktBillOfMaterialCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktBillOfMaterialCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterial.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.billofmaterial.plantcode"));

        RuleFor(x => x.BomCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterial.bomcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.billofmaterial.bomcode", 1, 50));

        RuleFor(x => x.BomName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterial.bomname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.billofmaterial.bomname", 1, 200));

        RuleFor(x => x.ParentMaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterial.parentmaterialcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.billofmaterial.parentmaterialcode", 1, 50));

        RuleFor(x => x.ParentMaterialName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterial.parentmaterialname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.billofmaterial.parentmaterialname", 1, 200));

        RuleFor(x => x.BomVersion)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterial.bomversion"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.billofmaterial.bomversion", 1, 20));

        RuleFor(x => x.BomType)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.billofmaterial.bomtype"));

        RuleFor(x => x.ParentMaterialUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.billofmaterial.parentmaterialunit"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.billofmaterial.parentmaterialunit", 1, 20));

        RuleFor(x => x.IsEnabled)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.billofmaterial.isenabled"));

        RuleFor(x => x.BomStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.billofmaterial.bomstatus"));

        RuleFor(x => x.BomDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.billofmaterial.bomdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.BomDescription));
    }
}

/// <summary>
/// BillOfMaterial更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktBillOfMaterialCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>BillOfMaterialId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktBillOfMaterialUpdateDtoValidator : AbstractValidator<TaktBillOfMaterialUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktBillOfMaterialUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktBillOfMaterialCreateDtoValidator(validationMessages));

        RuleFor(x => x.BillOfMaterialId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.billofmaterial.billofmaterialid"));

        RuleFor(x => x.BomCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.billofmaterial.bomcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BomCode));

        RuleFor(x => x.BomName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.billofmaterial.bomname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.BomName));

        RuleFor(x => x.ParentMaterialCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.billofmaterial.parentmaterialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ParentMaterialCode));

        RuleFor(x => x.ParentMaterialName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.billofmaterial.parentmaterialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ParentMaterialName));

        RuleFor(x => x.BomVersion)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.billofmaterial.bomversion", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BomVersion));

        RuleFor(x => x.ParentMaterialUnit)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.billofmaterial.parentmaterialunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ParentMaterialUnit));

        RuleFor(x => x.BomDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.billofmaterial.bomdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.BomDescription));
    }
}
