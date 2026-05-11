// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryAdjustmentValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalaryAdjustment DTO 验证器（根据实体 TaktSalaryAdjustment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;

namespace Takt.Application.Validators.HumanResource.CompensationBenefits;

/// <summary>
/// SalaryAdjustment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktSalaryAdjustment"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSalaryAdjustmentCreateDtoValidator : AbstractValidator<TaktSalaryAdjustmentCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSalaryAdjustmentCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.AdjustmentType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salaryadjustment.adjustmenttype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salaryadjustment.adjustmenttype", 1, 50));

        RuleFor(x => x.AdjustmentReason)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salaryadjustment.adjustmentreason"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.salaryadjustment.adjustmentreason", 1, 500));

        RuleFor(x => x.PreviousSalaryLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salaryadjustment.previoussalarylevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salaryadjustment.previoussalarylevel", 1, 50));

        RuleFor(x => x.NewSalaryLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salaryadjustment.newsalarylevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salaryadjustment.newsalarylevel", 1, 50));

        RuleFor(x => x.ApprovalComments)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salaryadjustment.approvalcomments"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.salaryadjustment.approvalcomments", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.salaryadjustment.status"));
    }
}

/// <summary>
/// SalaryAdjustment更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSalaryAdjustmentCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SalaryAdjustmentId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSalaryAdjustmentUpdateDtoValidator : AbstractValidator<TaktSalaryAdjustmentUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSalaryAdjustmentUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSalaryAdjustmentCreateDtoValidator(validationMessages));

        RuleFor(x => x.SalaryAdjustmentId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.salaryadjustment.salaryadjustmentid"));

        RuleFor(x => x.AdjustmentType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salaryadjustment.adjustmenttype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AdjustmentType));

        RuleFor(x => x.AdjustmentReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.salaryadjustment.adjustmentreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AdjustmentReason));

        RuleFor(x => x.PreviousSalaryLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salaryadjustment.previoussalarylevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PreviousSalaryLevel));

        RuleFor(x => x.NewSalaryLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salaryadjustment.newsalarylevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NewSalaryLevel));

        RuleFor(x => x.ApprovalComments)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.salaryadjustment.approvalcomments", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ApprovalComments));
    }
}
