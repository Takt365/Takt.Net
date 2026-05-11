// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Performance
// 文件名称：TaktPerformancePlanValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PerformancePlan DTO 验证器（根据实体 TaktPerformancePlan 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Performance;

namespace Takt.Application.Validators.HumanResource.Performance;

/// <summary>
/// PerformancePlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktPerformancePlan"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPerformancePlanCreateDtoValidator : AbstractValidator<TaktPerformancePlanCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPerformancePlanCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlanCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceplan.plancode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performanceplan.plancode", 1, 50));

        RuleFor(x => x.PlanName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceplan.planname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.performanceplan.planname", 1, 100));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceplan.applicabledepartment"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.performanceplan.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceplan.applicableposition"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.performanceplan.applicableposition", 1, 100));

        RuleFor(x => x.ApplicableLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceplan.applicablelevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performanceplan.applicablelevel", 1, 50));

        RuleFor(x => x.CycleType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceplan.cycletype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performanceplan.cycletype", 1, 50));

        RuleFor(x => x.ScoringStandard)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceplan.scoringstandard"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.performanceplan.scoringstandard", 1, 50));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.performanceplan.description"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.performanceplan.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.performanceplan.status"));
    }
}

/// <summary>
/// PerformancePlan更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPerformancePlanCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PerformancePlanId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPerformancePlanUpdateDtoValidator : AbstractValidator<TaktPerformancePlanUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPerformancePlanUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPerformancePlanCreateDtoValidator(validationMessages));

        RuleFor(x => x.PerformancePlanId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.performanceplan.performanceplanid"));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performanceplan.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.PlanName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.performanceplan.planname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanName));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.performanceplan.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.performanceplan.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.ApplicableLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performanceplan.applicablelevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableLevel));

        RuleFor(x => x.CycleType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performanceplan.cycletype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CycleType));

        RuleFor(x => x.ScoringStandard)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.performanceplan.scoringstandard", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ScoringStandard));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.performanceplan.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
