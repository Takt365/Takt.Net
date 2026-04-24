// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Financial
// 文件名称：TaktCountersignFormValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CountersignForm DTO 验证器（根据实体 TaktCountersignForm 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Accounting.Financial;

/// <summary>
/// CountersignForm创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktCountersignForm"/> 字段对齐）。
/// </summary>
public class TaktCountersignFormCreateDtoValidator : AbstractValidator<TaktCountersignFormCreateDto>
{
    public TaktCountersignFormCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.CountersignCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.countersignform.countersigncode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.countersignform.countersigncode", 1, 50));

        RuleFor(x => x.ApplicantName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.applicantname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantName));

        RuleFor(x => x.ApplicationDept)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.applicationdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicationDept));

        RuleFor(x => x.CostBearerDept)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.costbearerdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostBearerDept));

        RuleFor(x => x.IsBudget)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.countersignform.isbudget"));

        RuleFor(x => x.BudgetItem)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.budgetitem", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.BudgetItem));

        RuleFor(x => x.CountersignTitle)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.countersigntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CountersignTitle));

        RuleFor(x => x.CountersignStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.countersignform.countersignstatus"));
    }
}

/// <summary>
/// CountersignForm更新 DTO 验证器。
/// </summary>
public class TaktCountersignFormUpdateDtoValidator : AbstractValidator<TaktCountersignFormUpdateDto>
{
    public TaktCountersignFormUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktCountersignFormCreateDtoValidator(localizer));

        RuleFor(x => x.CountersignFormId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.countersignform.countersignformid"));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.CountersignCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.countersigncode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CountersignCode));

        RuleFor(x => x.ApplicantName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.applicantname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantName));

        RuleFor(x => x.ApplicationDept)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.applicationdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicationDept));

        RuleFor(x => x.CostBearerDept)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.costbearerdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostBearerDept));

        RuleFor(x => x.BudgetItem)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.budgetitem", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.BudgetItem));

        RuleFor(x => x.CountersignTitle)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.countersignform.countersigntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CountersignTitle));
    }
}
