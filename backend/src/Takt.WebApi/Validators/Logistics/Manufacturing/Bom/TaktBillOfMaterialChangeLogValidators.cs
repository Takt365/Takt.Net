// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialChangeLogValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：BillOfMaterialChangeLog DTO 验证器（根据实体 TaktBillOfMaterialChangeLog 自动生成）
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
/// BillOfMaterialChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktBillOfMaterialChangeLog"/> 字段对齐）。
/// </summary>
public class TaktBillOfMaterialChangeLogCreateDtoValidator : AbstractValidator<TaktBillOfMaterialChangeLogCreateDto>
{
    public TaktBillOfMaterialChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.BomCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterialchangelog.bomcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.billofmaterialchangelog.bomcode", 1, 50));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// BillOfMaterialChangeLog更新 DTO 验证器。
/// </summary>
public class TaktBillOfMaterialChangeLogUpdateDtoValidator : AbstractValidator<TaktBillOfMaterialChangeLogUpdateDto>
{
    public TaktBillOfMaterialChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktBillOfMaterialChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.BillOfMaterialChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.billofmaterialchangelog.billofmaterialchangelogid"));

        RuleFor(x => x.BomCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialchangelog.bomcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BomCode));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.billofmaterialchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
