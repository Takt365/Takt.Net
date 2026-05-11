// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktBenefitPlanValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：BenefitPlan DTO 验证器（根据实体 TaktBenefitPlan 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;

namespace Takt.Application.Validators.HumanResource.CompensationBenefits;

/// <summary>
/// BenefitPlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktBenefitPlan"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktBenefitPlanCreateDtoValidator : AbstractValidator<TaktBenefitPlanCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktBenefitPlanCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlanCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.benefitplan.plancode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.benefitplan.plancode", 1, 50));

        RuleFor(x => x.PlanName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.benefitplan.planname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.benefitplan.planname", 1, 100));

        RuleFor(x => x.BenefitType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.benefitplan.benefittype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.benefitplan.benefittype", 1, 50));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.benefitplan.applicabledepartment"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.benefitplan.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.benefitplan.applicableposition"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.benefitplan.applicableposition", 1, 100));

        RuleFor(x => x.ApplicableLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.benefitplan.applicablelevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.benefitplan.applicablelevel", 1, 50));

        RuleFor(x => x.DistributionMethod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.benefitplan.distributionmethod"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.benefitplan.distributionmethod", 1, 50));

        RuleFor(x => x.BenefitConditions)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.benefitplan.benefitconditions"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.benefitplan.benefitconditions", 1, 500));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.benefitplan.description"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.benefitplan.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.benefitplan.status"));
    }
}

/// <summary>
/// BenefitPlan更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktBenefitPlanCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>BenefitPlanId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktBenefitPlanUpdateDtoValidator : AbstractValidator<TaktBenefitPlanUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktBenefitPlanUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktBenefitPlanCreateDtoValidator(validationMessages));

        RuleFor(x => x.BenefitPlanId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.benefitplan.benefitplanid"));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.benefitplan.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.PlanName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.benefitplan.planname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanName));

        RuleFor(x => x.BenefitType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.benefitplan.benefittype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BenefitType));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.benefitplan.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.benefitplan.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.ApplicableLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.benefitplan.applicablelevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableLevel));

        RuleFor(x => x.DistributionMethod)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.benefitplan.distributionmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DistributionMethod));

        RuleFor(x => x.BenefitConditions)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.benefitplan.benefitconditions", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.BenefitConditions));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.benefitplan.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
