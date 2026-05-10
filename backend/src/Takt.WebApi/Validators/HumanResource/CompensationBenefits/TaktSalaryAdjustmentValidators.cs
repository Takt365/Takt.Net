// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryAdjustmentValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalaryAdjustment DTO 验证器（根据实体 TaktSalaryAdjustment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.CompensationBenefits;

/// <summary>
/// SalaryAdjustment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktSalaryAdjustment"/> 字段对齐）。
/// </summary>
public class TaktSalaryAdjustmentCreateDtoValidator : AbstractValidator<TaktSalaryAdjustmentCreateDto>
{
    public TaktSalaryAdjustmentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.AdjustmentType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salaryadjustment.adjustmenttype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salaryadjustment.adjustmenttype", 1, 50));

        RuleFor(x => x.AdjustmentReason)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salaryadjustment.adjustmentreason"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salaryadjustment.adjustmentreason", 1, 500));

        RuleFor(x => x.PreviousSalaryLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salaryadjustment.previoussalarylevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salaryadjustment.previoussalarylevel", 1, 50));

        RuleFor(x => x.NewSalaryLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salaryadjustment.newsalarylevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salaryadjustment.newsalarylevel", 1, 50));

        RuleFor(x => x.ApprovalComments)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salaryadjustment.approvalcomments"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salaryadjustment.approvalcomments", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salaryadjustment.status"));
    }
}

/// <summary>
/// SalaryAdjustment更新 DTO 验证器。
/// </summary>
public class TaktSalaryAdjustmentUpdateDtoValidator : AbstractValidator<TaktSalaryAdjustmentUpdateDto>
{
    public TaktSalaryAdjustmentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSalaryAdjustmentCreateDtoValidator(localizer));

        RuleFor(x => x.SalaryAdjustmentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.salaryadjustment.salaryadjustmentid"));

        RuleFor(x => x.AdjustmentType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salaryadjustment.adjustmenttype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AdjustmentType));

        RuleFor(x => x.AdjustmentReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salaryadjustment.adjustmentreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AdjustmentReason));

        RuleFor(x => x.PreviousSalaryLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salaryadjustment.previoussalarylevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PreviousSalaryLevel));

        RuleFor(x => x.NewSalaryLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salaryadjustment.newsalarylevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NewSalaryLevel));

        RuleFor(x => x.ApprovalComments)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salaryadjustment.approvalcomments", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ApprovalComments));
    }
}
