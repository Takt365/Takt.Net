// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktPersonnelOperationRateValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PersonnelOperationRate DTO 验证器（根据实体 TaktPersonnelOperationRate 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// PersonnelOperationRate创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktPersonnelOperationRate"/> 字段对齐）。
/// </summary>
public class TaktPersonnelOperationRateCreateDtoValidator : AbstractValidator<TaktPersonnelOperationRateCreateDto>
{
    public TaktPersonnelOperationRateCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.personneloperationrate.plantcode"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.personneloperationrate.plantcode", 1, 8));

        RuleFor(x => x.TimeCategory)
            .InclusiveBetween(1, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.personneloperationrate.timecategory"));

        RuleFor(x => x.ProductionLine)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.personneloperationrate.productionline"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.personneloperationrate.productionline", 1, 20));

        RuleFor(x => x.ProductionLineName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.productionlinename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineName));

        RuleFor(x => x.ShiftNo)
            .InclusiveBetween(1, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.personneloperationrate.shiftno"));

        RuleFor(x => x.IdleReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.idlereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.IdleReason));

        RuleFor(x => x.TeamLeader)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.teamleader", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeader));

        RuleFor(x => x.Supervisor)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.supervisor", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Supervisor));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.personneloperationrate.status"));
    }
}

/// <summary>
/// PersonnelOperationRate更新 DTO 验证器。
/// </summary>
public class TaktPersonnelOperationRateUpdateDtoValidator : AbstractValidator<TaktPersonnelOperationRateUpdateDto>
{
    public TaktPersonnelOperationRateUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPersonnelOperationRateCreateDtoValidator(localizer));

        RuleFor(x => x.PersonnelOperationRateId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.personneloperationrate.personneloperationrateid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.plantcode", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.productionline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));

        RuleFor(x => x.ProductionLineName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.productionlinename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineName));

        RuleFor(x => x.IdleReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.idlereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.IdleReason));

        RuleFor(x => x.TeamLeader)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.teamleader", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeader));

        RuleFor(x => x.Supervisor)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.personneloperationrate.supervisor", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Supervisor));
    }
}
