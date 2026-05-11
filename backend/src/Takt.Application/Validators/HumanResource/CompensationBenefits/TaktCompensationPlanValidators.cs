// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktCompensationPlanValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CompensationPlan DTO 验证器（根据实体 TaktCompensationPlan 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;

namespace Takt.Application.Validators.HumanResource.CompensationBenefits;

/// <summary>
/// CompensationPlan创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktCompensationPlan"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktCompensationPlanCreateDtoValidator : AbstractValidator<TaktCompensationPlanCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCompensationPlanCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlanCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.compensationplan.plancode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.compensationplan.plancode", 1, 50));

        RuleFor(x => x.PlanName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.compensationplan.planname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.compensationplan.planname", 1, 100));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.compensationplan.applicabledepartment"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.compensationplan.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.compensationplan.applicableposition"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.compensationplan.applicableposition", 1, 100));

        RuleFor(x => x.ApplicableLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.compensationplan.applicablelevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.compensationplan.applicablelevel", 1, 50));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.compensationplan.description"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.compensationplan.description", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.compensationplan.status"));
    }
}

/// <summary>
/// CompensationPlan更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktCompensationPlanCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>CompensationPlanId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktCompensationPlanUpdateDtoValidator : AbstractValidator<TaktCompensationPlanUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCompensationPlanUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktCompensationPlanCreateDtoValidator(validationMessages));

        RuleFor(x => x.CompensationPlanId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.compensationplan.compensationplanid"));

        RuleFor(x => x.PlanCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.compensationplan.plancode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanCode));

        RuleFor(x => x.PlanName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.compensationplan.planname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PlanName));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.compensationplan.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.compensationplan.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.ApplicableLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.compensationplan.applicablelevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableLevel));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.compensationplan.description", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
