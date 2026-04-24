// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialItemValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：BillOfMaterialItem DTO 验证器（根据实体 TaktBillOfMaterialItem 自动生成）
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
/// BillOfMaterialItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktBillOfMaterialItem"/> 字段对齐）。
/// </summary>
public class TaktBillOfMaterialItemCreateDtoValidator : AbstractValidator<TaktBillOfMaterialItemCreateDto>
{
    public TaktBillOfMaterialItemCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.BomCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterialitem.bomcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterialitem.bomcode", 1, 50));

        RuleFor(x => x.ChildMaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterialitem.childmaterialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterialitem.childmaterialcode", 1, 50));

        RuleFor(x => x.ChildMaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterialitem.childmaterialname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterialitem.childmaterialname", 1, 200));

        RuleFor(x => x.ChildMaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialitem.childmaterialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialSpecification));

        RuleFor(x => x.ChildMaterialUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterialitem.childmaterialunit"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterialitem.childmaterialunit", 1, 20));

        RuleFor(x => x.IsSubstitute)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.billofmaterialitem.issubstitute"));

        RuleFor(x => x.IsRequired)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.billofmaterialitem.isrequired"));

        RuleFor(x => x.IsPhantom)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.billofmaterialitem.isphantom"));

        RuleFor(x => x.IsCritical)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.billofmaterialitem.iscritical"));
    }
}

/// <summary>
/// BillOfMaterialItem更新 DTO 验证器。
/// </summary>
public class TaktBillOfMaterialItemUpdateDtoValidator : AbstractValidator<TaktBillOfMaterialItemUpdateDto>
{
    public TaktBillOfMaterialItemUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktBillOfMaterialItemCreateDtoValidator(localizer));

        RuleFor(x => x.BillOfMaterialItemId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterialitem.billofmaterialitemid"));

        RuleFor(x => x.BomCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialitem.bomcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BomCode));

        RuleFor(x => x.ChildMaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialitem.childmaterialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialCode));

        RuleFor(x => x.ChildMaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialitem.childmaterialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialName));

        RuleFor(x => x.ChildMaterialSpecification)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialitem.childmaterialspecification", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialSpecification));

        RuleFor(x => x.ChildMaterialUnit)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialitem.childmaterialunit", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ChildMaterialUnit));
    }
}
