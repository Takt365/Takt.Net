// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Performance
// 文件名称：TaktReviewCycleValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ReviewCycle DTO 验证器（根据实体 TaktReviewCycle 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Performance;

namespace Takt.Application.Validators.HumanResource.Performance;

/// <summary>
/// ReviewCycle创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktReviewCycle"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktReviewCycleCreateDtoValidator : AbstractValidator<TaktReviewCycleCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktReviewCycleCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CycleCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reviewcycle.cyclecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reviewcycle.cyclecode", 1, 50));

        RuleFor(x => x.CycleName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reviewcycle.cyclename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.reviewcycle.cyclename", 1, 100));

        RuleFor(x => x.CycleType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reviewcycle.cycletype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reviewcycle.cycletype", 1, 50));

        RuleFor(x => x.CycleSequence)
            .InclusiveBetween(1, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.reviewcycle.cyclesequence"));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reviewcycle.applicabledepartment"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.reviewcycle.applicabledepartment", 1, 100));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reviewcycle.description"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.reviewcycle.description", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reviewcycle.status"));
    }
}

/// <summary>
/// ReviewCycle更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktReviewCycleCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ReviewCycleId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktReviewCycleUpdateDtoValidator : AbstractValidator<TaktReviewCycleUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktReviewCycleUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktReviewCycleCreateDtoValidator(validationMessages));

        RuleFor(x => x.ReviewCycleId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.reviewcycle.reviewcycleid"));

        RuleFor(x => x.CycleCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reviewcycle.cyclecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CycleCode));

        RuleFor(x => x.CycleName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.reviewcycle.cyclename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CycleName));

        RuleFor(x => x.CycleType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reviewcycle.cycletype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CycleType));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.reviewcycle.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.reviewcycle.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
