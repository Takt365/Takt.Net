// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingPlanValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TrainingPlan DTO 验证器（根据实体 TaktTrainingPlan 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.TrainingDevelopment;

namespace Takt.Application.Validators.HumanResource.TrainingDevelopment;

/// <summary>
/// TrainingPlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.TrainingDevelopment.TaktTrainingPlan"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktTrainingPlanCreateDtoValidator : AbstractValidator<TaktTrainingPlanCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTrainingPlanCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlanCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingplan.plancode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingplan.plancode", 1, 50));

        RuleFor(x => x.PlanName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingplan.planname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.trainingplan.planname", 1, 200));

        RuleFor(x => x.PlanType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingplan.plantype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingplan.plantype", 1, 50));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingplan.applicabledepartment"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.trainingplan.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingplan.applicableposition"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.trainingplan.applicableposition", 1, 100));

        RuleFor(x => x.ApplicableLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingplan.applicablelevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingplan.applicablelevel", 1, 50));

        RuleFor(x => x.TrainingObjectives)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingplan.trainingobjectives"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.trainingplan.trainingobjectives", 1, 1000));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingplan.description"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.trainingplan.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.trainingplan.status"));
    }
}

/// <summary>
/// TrainingPlan更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktTrainingPlanCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>TrainingPlanId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktTrainingPlanUpdateDtoValidator : AbstractValidator<TaktTrainingPlanUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTrainingPlanUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktTrainingPlanCreateDtoValidator(validationMessages));

        RuleFor(x => x.TrainingPlanId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.trainingplan.trainingplanid"));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingplan.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.PlanName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.trainingplan.planname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanName));

        RuleFor(x => x.PlanType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingplan.plantype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanType));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.trainingplan.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.trainingplan.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.ApplicableLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingplan.applicablelevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableLevel));

        RuleFor(x => x.TrainingObjectives)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.trainingplan.trainingobjectives", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingObjectives));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.trainingplan.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
