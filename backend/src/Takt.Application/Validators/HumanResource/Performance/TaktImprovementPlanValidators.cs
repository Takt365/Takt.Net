// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Performance
// 文件名称：TaktImprovementPlanValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ImprovementPlan DTO 验证器（根据实体 TaktImprovementPlan 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Performance;

namespace Takt.Application.Validators.HumanResource.Performance;

/// <summary>
/// ImprovementPlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktImprovementPlan"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktImprovementPlanCreateDtoValidator : AbstractValidator<TaktImprovementPlanCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktImprovementPlanCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlanTitle)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.improvementplan.plantitle"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.improvementplan.plantitle", 1, 200));

        RuleFor(x => x.ImprovementArea)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.improvementplan.improvementarea"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.improvementplan.improvementarea", 1, 50));

        RuleFor(x => x.CurrentSituation)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.improvementplan.currentsituation"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.improvementplan.currentsituation", 1, 1000));

        RuleFor(x => x.ImprovementGoal)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.improvementplan.improvementgoal"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.improvementplan.improvementgoal", 1, 500));

        RuleFor(x => x.ImprovementActions)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.improvementplan.improvementactions"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.improvementplan.improvementactions", 1, 1000));

        RuleFor(x => x.RequiredResources)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.improvementplan.requiredresources"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.improvementplan.requiredresources", 1, 500));

        RuleFor(x => x.MidtermCheckResult)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.improvementplan.midtermcheckresult"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.improvementplan.midtermcheckresult", 1, 500));

        RuleFor(x => x.ResultDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.improvementplan.resultdescription"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.improvementplan.resultdescription", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.improvementplan.status"));
    }
}

/// <summary>
/// ImprovementPlan更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktImprovementPlanCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ImprovementPlanId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktImprovementPlanUpdateDtoValidator : AbstractValidator<TaktImprovementPlanUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktImprovementPlanUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktImprovementPlanCreateDtoValidator(validationMessages));

        RuleFor(x => x.ImprovementPlanId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.improvementplan.improvementplanid"));

        RuleFor(x => x.PlanTitle)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.improvementplan.plantitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanTitle));

        RuleFor(x => x.ImprovementArea)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.improvementplan.improvementarea", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementArea));

        RuleFor(x => x.CurrentSituation)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.improvementplan.currentsituation", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentSituation));

        RuleFor(x => x.ImprovementGoal)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.improvementplan.improvementgoal", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementGoal));

        RuleFor(x => x.ImprovementActions)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.improvementplan.improvementactions", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementActions));

        RuleFor(x => x.RequiredResources)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.improvementplan.requiredresources", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RequiredResources));

        RuleFor(x => x.MidtermCheckResult)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.improvementplan.midtermcheckresult", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.MidtermCheckResult));

        RuleFor(x => x.ResultDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.improvementplan.resultdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ResultDescription));
    }
}
