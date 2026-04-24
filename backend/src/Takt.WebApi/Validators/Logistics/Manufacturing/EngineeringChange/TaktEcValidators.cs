// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Ec DTO 验证器（根据实体 TaktEc 自动生成）
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
/// Ec创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEc"/> 字段对齐）。
/// </summary>
public class TaktEcCreateDtoValidator : AbstractValidator<TaktEcCreateDto>
{
    public TaktEcCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ec.plantcode"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ec.plantcode", 1, 8));

        RuleFor(x => x.EcnNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ec.ecnno"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ec.ecnno", 1, 10));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ec.status"));

        RuleFor(x => x.EcnTitle)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.ecntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnTitle));

        RuleFor(x => x.EcnLeader)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.ecnleader", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnLeader));

        RuleFor(x => x.EcnDistinction)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.ecndistinction", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnDistinction));

        RuleFor(x => x.FlowInstanceStatus)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.flowinstancestatus", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FlowInstanceStatus));
    }
}

/// <summary>
/// Ec更新 DTO 验证器。
/// </summary>
public class TaktEcUpdateDtoValidator : AbstractValidator<TaktEcUpdateDto>
{
    public TaktEcUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEcCreateDtoValidator(localizer));

        RuleFor(x => x.EcId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ec.ecid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.plantcode", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.EcnNo)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.ecnno", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNo));

        RuleFor(x => x.EcnTitle)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.ecntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnTitle));

        RuleFor(x => x.EcnLeader)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.ecnleader", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnLeader));

        RuleFor(x => x.EcnDistinction)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.ecndistinction", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnDistinction));

        RuleFor(x => x.FlowInstanceStatus)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ec.flowinstancestatus", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FlowInstanceStatus));
    }
}
