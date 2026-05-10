// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ApsSchedule DTO 验证器（根据实体 TaktApsSchedule 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Scheduling;

/// <summary>
/// ApsSchedule创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Scheduling.TaktApsSchedule"/> 字段对齐）。
/// </summary>
public class TaktApsScheduleCreateDtoValidator : AbstractValidator<TaktApsScheduleCreateDto>
{
    public TaktApsScheduleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ScheduleCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.apsschedule.schedulecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.apsschedule.schedulecode", 1, 50));

        RuleFor(x => x.ScheduleName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.apsschedule.schedulename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.apsschedule.schedulename", 1, 200));

        RuleFor(x => x.ScheduleType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsschedule.scheduletype"));

        RuleFor(x => x.PlanCycle)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsschedule.plancycle"));

        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.apsschedule.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsschedule.plantcode"));

        RuleFor(x => x.PlantName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.plantname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantName));

        RuleFor(x => x.WorkshopCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.workshopcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopCode));

        RuleFor(x => x.WorkshopName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.workshopname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopName));

        RuleFor(x => x.ProductionLineCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.productionlinecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineCode));

        RuleFor(x => x.ProductionLineName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.productionlinename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineName));

        RuleFor(x => x.ScheduleStrategy)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsschedule.schedulestrategy"));

        RuleFor(x => x.ScheduleAlgorithm)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsschedule.schedulealgorithm"));

        RuleFor(x => x.OptimizationObjective)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsschedule.optimizationobjective"));

        RuleFor(x => x.ScheduleStatus)
            .InclusiveBetween(0, 6)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsschedule.schedulestatus"));

        RuleFor(x => x.PlannerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.plannername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlannerName));

        RuleFor(x => x.PublishUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.publishusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PublishUserName));

        RuleFor(x => x.ScheduleDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.scheduledescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduleDescription));
    }
}

/// <summary>
/// ApsSchedule更新 DTO 验证器。
/// </summary>
public class TaktApsScheduleUpdateDtoValidator : AbstractValidator<TaktApsScheduleUpdateDto>
{
    public TaktApsScheduleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktApsScheduleCreateDtoValidator(localizer));

        RuleFor(x => x.ApsScheduleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.apsschedule.apsscheduleid"));

        RuleFor(x => x.ScheduleCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.schedulecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduleCode));

        RuleFor(x => x.ScheduleName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.schedulename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduleName));

        RuleFor(x => x.PlantName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.plantname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantName));

        RuleFor(x => x.WorkshopCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.workshopcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopCode));

        RuleFor(x => x.WorkshopName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.workshopname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopName));

        RuleFor(x => x.ProductionLineCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.productionlinecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineCode));

        RuleFor(x => x.ProductionLineName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.productionlinename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineName));

        RuleFor(x => x.PlannerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.plannername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlannerName));

        RuleFor(x => x.PublishUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.publishusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PublishUserName));

        RuleFor(x => x.ScheduleDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedule.scheduledescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduleDescription));
    }
}
