// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IpqcOrder DTO 验证器（根据实体 TaktIpqcOrder 自动生成）
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
/// IpqcOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIpqcOrder"/> 字段对齐）。
/// </summary>
public class TaktIpqcOrderCreateDtoValidator : AbstractValidator<TaktIpqcOrderCreateDto>
{
    public TaktIpqcOrderCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorder.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ipqcorder.ordercode", 1, 50));

        RuleFor(x => x.SourceCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorder.sourcecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ipqcorder.sourcecode", 1, 50));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.StandardCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorder.standardcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ipqcorder.standardcode", 1, 50));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorder.materialcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ipqcorder.materialcode", 1, 50));

        RuleFor(x => x.MaterialName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorder.materialname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ipqcorder.materialname", 1, 200));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.ProcessCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorder.processcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ipqcorder.processcode", 1, 50));

        RuleFor(x => x.ProcessName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorder.processname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ipqcorder.processname", 1, 200));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.InspectionConclusion)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ipqcorder.inspectionconclusion"));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.InspectionRemark)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.inspectionremark", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionRemark));

        RuleFor(x => x.OrderStatus)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ipqcorder.orderstatus"));
    }
}

/// <summary>
/// IpqcOrder更新 DTO 验证器。
/// </summary>
public class TaktIpqcOrderUpdateDtoValidator : AbstractValidator<TaktIpqcOrderUpdateDto>
{
    public TaktIpqcOrderUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktIpqcOrderCreateDtoValidator(localizer));

        RuleFor(x => x.IpqcOrderId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorder.ipqcorderid"));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.SourceCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.sourcecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SourceCode));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.StandardCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.standardcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.materialcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.MaterialName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.materialname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialName));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.ProcessCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.processcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessCode));

        RuleFor(x => x.ProcessName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.processname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessName));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.InspectionRemark)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorder.inspectionremark", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionRemark));
    }
}
