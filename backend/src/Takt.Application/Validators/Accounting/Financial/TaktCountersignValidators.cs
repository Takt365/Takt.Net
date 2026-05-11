// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Accounting.Financial
// 文件名称：TaktCountersignValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Countersign DTO 验证器（根据实体 TaktCountersign 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Financial;

namespace Takt.Application.Validators.Accounting.Financial;

/// <summary>
/// Countersign创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktCountersign"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktCountersignCreateDtoValidator : AbstractValidator<TaktCountersignCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCountersignCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.countersign.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.countersign.companycode"));

        RuleFor(x => x.CountersignCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.countersign.countersigncode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.countersign.countersigncode", 1, 50));

        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.countersign.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.ApplicationDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.countersign.applicationdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicationDept));

        RuleFor(x => x.CostBearerDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.countersign.costbearerdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostBearerDept));

        RuleFor(x => x.IsBudget)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.countersign.isbudget"));

        RuleFor(x => x.BudgetItem)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.countersign.budgetitem", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.BudgetItem));

        RuleFor(x => x.CountersignTitle)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.countersign.countersigntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CountersignTitle));

        RuleFor(x => x.CountersignStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.countersign.countersignstatus"));
    }
}

/// <summary>
/// Countersign更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktCountersignCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>CountersignId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktCountersignUpdateDtoValidator : AbstractValidator<TaktCountersignUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCountersignUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktCountersignCreateDtoValidator(validationMessages));

        RuleFor(x => x.CountersignId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.countersign.countersignid"));

        RuleFor(x => x.CountersignCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.countersign.countersigncode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CountersignCode));

        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.countersign.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.ApplicationDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.countersign.applicationdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicationDept));

        RuleFor(x => x.CostBearerDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.countersign.costbearerdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostBearerDept));

        RuleFor(x => x.BudgetItem)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.countersign.budgetitem", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.BudgetItem));

        RuleFor(x => x.CountersignTitle)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.countersign.countersigntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CountersignTitle));
    }
}
