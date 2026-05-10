// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Performance
// 文件名称：TaktReviewCycleValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ReviewCycle DTO 验证器（根据实体 TaktReviewCycle 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Performance;

/// <summary>
/// ReviewCycle创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Performance.TaktReviewCycle"/> 字段对齐）。
/// </summary>
public class TaktReviewCycleCreateDtoValidator : AbstractValidator<TaktReviewCycleCreateDto>
{
    public TaktReviewCycleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CycleCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reviewcycle.cyclecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reviewcycle.cyclecode", 1, 50));

        RuleFor(x => x.CycleName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reviewcycle.cyclename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reviewcycle.cyclename", 1, 100));

        RuleFor(x => x.CycleType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reviewcycle.cycletype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reviewcycle.cycletype", 1, 50));

        RuleFor(x => x.CycleSequence)
            .InclusiveBetween(1, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reviewcycle.cyclesequence"));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reviewcycle.applicabledepartment"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reviewcycle.applicabledepartment", 1, 100));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reviewcycle.description"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reviewcycle.description", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reviewcycle.status"));
    }
}

/// <summary>
/// ReviewCycle更新 DTO 验证器。
/// </summary>
public class TaktReviewCycleUpdateDtoValidator : AbstractValidator<TaktReviewCycleUpdateDto>
{
    public TaktReviewCycleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktReviewCycleCreateDtoValidator(localizer));

        RuleFor(x => x.ReviewCycleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.reviewcycle.reviewcycleid"));

        RuleFor(x => x.CycleCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reviewcycle.cyclecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CycleCode));

        RuleFor(x => x.CycleName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reviewcycle.cyclename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CycleName));

        RuleFor(x => x.CycleType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reviewcycle.cycletype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CycleType));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reviewcycle.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reviewcycle.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
