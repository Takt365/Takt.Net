// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ApsSchedule DTO 验证器（根据实体 TaktApsSchedule 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;

namespace Takt.Application.Validators.Logistics.Manufacturing.Scheduling;

/// <summary>
/// ApsSchedule创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Scheduling.TaktApsSchedule"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktApsScheduleCreateDtoValidator : AbstractValidator<TaktApsScheduleCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktApsScheduleCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.apsschedule.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.apsschedule.plantcode"));

        RuleFor(x => x.ScheduleCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.apsschedule.schedulecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.apsschedule.schedulecode", 1, 50));

        RuleFor(x => x.ScheduleName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.apsschedule.schedulename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.apsschedule.schedulename", 1, 200));

        RuleFor(x => x.ScheduleType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.apsschedule.scheduletype"));

        RuleFor(x => x.PlanCycle)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.apsschedule.plancycle"));

        RuleFor(x => x.WorkshopCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsschedule.workshopcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopCode));

        RuleFor(x => x.WorkshopName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.apsschedule.workshopname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopName));

        RuleFor(x => x.ProductionLineCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsschedule.productionlinecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineCode));

        RuleFor(x => x.ProductionLineName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.apsschedule.productionlinename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineName));

        RuleFor(x => x.ScheduleStrategy)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.apsschedule.schedulestrategy"));

        RuleFor(x => x.ScheduleAlgorithm)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.apsschedule.schedulealgorithm"));

        RuleFor(x => x.OptimizationObjective)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.apsschedule.optimizationobjective"));

        RuleFor(x => x.ScheduleStatus)
            .InclusiveBetween(0, 6)
            .WithMessage(_validationMessages.FormatInvalid("entity.apsschedule.schedulestatus"));

        RuleFor(x => x.PlannerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsschedule.plannername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlannerName));

        RuleFor(x => x.PublishUserName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsschedule.publishusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PublishUserName));

        RuleFor(x => x.ScheduleDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.apsschedule.scheduledescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduleDescription));
    }
}

/// <summary>
/// ApsSchedule更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktApsScheduleCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ApsScheduleId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktApsScheduleUpdateDtoValidator : AbstractValidator<TaktApsScheduleUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktApsScheduleUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktApsScheduleCreateDtoValidator(validationMessages));

        RuleFor(x => x.ApsScheduleId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.apsschedule.apsscheduleid"));

        RuleFor(x => x.ScheduleCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsschedule.schedulecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduleCode));

        RuleFor(x => x.ScheduleName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.apsschedule.schedulename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduleName));

        RuleFor(x => x.WorkshopCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsschedule.workshopcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopCode));

        RuleFor(x => x.WorkshopName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.apsschedule.workshopname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopName));

        RuleFor(x => x.ProductionLineCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsschedule.productionlinecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineCode));

        RuleFor(x => x.ProductionLineName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.apsschedule.productionlinename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineName));

        RuleFor(x => x.PlannerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsschedule.plannername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlannerName));

        RuleFor(x => x.PublishUserName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.apsschedule.publishusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PublishUserName));

        RuleFor(x => x.ScheduleDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.apsschedule.scheduledescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduleDescription));
    }
}
