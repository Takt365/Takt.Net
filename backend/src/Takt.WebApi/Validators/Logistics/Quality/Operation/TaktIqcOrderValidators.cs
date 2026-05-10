// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Operation
// 文件名称：TaktIqcOrderValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IqcOrder DTO 验证器（根据实体 TaktIqcOrder 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Quality.Operation;

/// <summary>
/// IqcOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIqcOrder"/> 字段对齐）。
/// </summary>
public class TaktIqcOrderCreateDtoValidator : AbstractValidator<TaktIqcOrderCreateDto>
{
    public TaktIqcOrderCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcorder.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcorder.ordercode", 1, 50));

        RuleFor(x => x.SourceCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcorder.sourcecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcorder.sourcecode", 1, 50));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.StandardCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcorder.standardcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcorder.standardcode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcorder.materialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcorder.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcorder.materialname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcorder.materialname", 1, 200));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.SupplierCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcorder.suppliercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcorder.suppliercode", 1, 50));

        RuleFor(x => x.SupplierName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcorder.suppliername"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcorder.suppliername", 1, 200));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.InspectionConclusion)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.iqcorder.inspectionconclusion"));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.InspectionRemark)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.inspectionremark", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionRemark));

        RuleFor(x => x.OrderStatus)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.iqcorder.orderstatus"));
    }
}

/// <summary>
/// IqcOrder更新 DTO 验证器。
/// </summary>
public class TaktIqcOrderUpdateDtoValidator : AbstractValidator<TaktIqcOrderUpdateDto>
{
    public TaktIqcOrderUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktIqcOrderCreateDtoValidator(localizer));

        RuleFor(x => x.IqcOrderId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcorder.iqcorderid"));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.SourceCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.sourcecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SourceCode));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.StandardCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.standardcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.SupplierCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.suppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierCode));

        RuleFor(x => x.SupplierName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.suppliername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierName));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.InspectionRemark)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcorder.inspectionremark", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionRemark));
    }
}
