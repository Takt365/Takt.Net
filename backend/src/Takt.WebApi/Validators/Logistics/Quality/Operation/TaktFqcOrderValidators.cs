// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Operation
// 文件名称：TaktFqcOrderValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FqcOrder DTO 验证器（根据实体 TaktFqcOrder 自动生成）
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
/// FqcOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktFqcOrder"/> 字段对齐）。
/// </summary>
public class TaktFqcOrderCreateDtoValidator : AbstractValidator<TaktFqcOrderCreateDto>
{
    public TaktFqcOrderCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcorder.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcorder.ordercode", 1, 50));

        RuleFor(x => x.SourceCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcorder.sourcecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcorder.sourcecode", 1, 50));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.StandardCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcorder.standardcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcorder.standardcode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcorder.materialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcorder.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcorder.materialname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcorder.materialname", 1, 200));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.CustomerCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcorder.customercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcorder.customercode", 1, 50));

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcorder.customername"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcorder.customername", 1, 200));

        RuleFor(x => x.DeliveryOrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.deliveryordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeliveryOrderCode));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.InspectionConclusion)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.fqcorder.inspectionconclusion"));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.InspectionRemark)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.inspectionremark", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionRemark));

        RuleFor(x => x.OrderStatus)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.fqcorder.orderstatus"));
    }
}

/// <summary>
/// FqcOrder更新 DTO 验证器。
/// </summary>
public class TaktFqcOrderUpdateDtoValidator : AbstractValidator<TaktFqcOrderUpdateDto>
{
    public TaktFqcOrderUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktFqcOrderCreateDtoValidator(localizer));

        RuleFor(x => x.FqcOrderId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcorder.fqcorderid"));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.SourceCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.sourcecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SourceCode));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.StandardCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.standardcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));

        RuleFor(x => x.CustomerName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.customername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerName));

        RuleFor(x => x.DeliveryOrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.deliveryordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeliveryOrderCode));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.InspectionRemark)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcorder.inspectionremark", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionRemark));
    }
}
