// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDetailValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EcDetail DTO 验证器（根据实体 TaktEcDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// EcDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEcDetail"/> 字段对齐）。
/// </summary>
public class TaktEcDetailCreateDtoValidator : AbstractValidator<TaktEcDetailCreateDto>
{
    public TaktEcDetailCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.EcnNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNo));

        RuleFor(x => x.EcnModel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnmodel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnModel));

        RuleFor(x => x.EcnBomItem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnbomitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomItem));

        RuleFor(x => x.EcnBomSubItem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnbomsubitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomSubItem));

        RuleFor(x => x.EcnBomNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnbomno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomNo));

        RuleFor(x => x.EcnChange)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnchange", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnChange));

        RuleFor(x => x.EcnLocal)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnlocal", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnLocal));

        RuleFor(x => x.EcnNote)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnnote", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNote));

        RuleFor(x => x.EcnProcess)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnprocess", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnProcess));

        RuleFor(x => x.EcnOldItem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnolditem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldItem));

        RuleFor(x => x.EcnOldText)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnoldtext", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldText));

        RuleFor(x => x.EcnOldSet)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnoldset", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldSet));

        RuleFor(x => x.EcnNewItem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnnewitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewItem));

        RuleFor(x => x.EcnNewText)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnnewtext", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewText));

        RuleFor(x => x.EcnNewSet)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnnewset", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewSet));

        RuleFor(x => x.IsProcurement)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ecdetail.isprocurement"));

        RuleFor(x => x.IsCheck)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ischeck", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.IsCheck));

        RuleFor(x => x.EcnWarehouse)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnwarehouse", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnWarehouse));

        RuleFor(x => x.IsEndOfLine)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ecdetail.isendofline"));
    }
}

/// <summary>
/// EcDetail更新 DTO 验证器。
/// </summary>
public class TaktEcDetailUpdateDtoValidator : AbstractValidator<TaktEcDetailUpdateDto>
{
    public TaktEcDetailUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEcDetailCreateDtoValidator(localizer));

        RuleFor(x => x.EcDetailId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ecdetail.ecdetailid"));

        RuleFor(x => x.EcnNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNo));

        RuleFor(x => x.EcnModel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnmodel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnModel));

        RuleFor(x => x.EcnBomItem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnbomitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomItem));

        RuleFor(x => x.EcnBomSubItem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnbomsubitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomSubItem));

        RuleFor(x => x.EcnBomNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnbomno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomNo));

        RuleFor(x => x.EcnChange)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnchange", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnChange));

        RuleFor(x => x.EcnLocal)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnlocal", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnLocal));

        RuleFor(x => x.EcnNote)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnnote", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNote));

        RuleFor(x => x.EcnProcess)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnprocess", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnProcess));

        RuleFor(x => x.EcnOldItem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnolditem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldItem));

        RuleFor(x => x.EcnOldText)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnoldtext", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldText));

        RuleFor(x => x.EcnOldSet)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnoldset", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldSet));

        RuleFor(x => x.EcnNewItem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnnewitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewItem));

        RuleFor(x => x.EcnNewText)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnnewtext", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewText));

        RuleFor(x => x.EcnNewSet)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnnewset", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewSet));

        RuleFor(x => x.IsCheck)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ischeck", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.IsCheck));

        RuleFor(x => x.EcnWarehouse)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecdetail.ecnwarehouse", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnWarehouse));
    }
}
