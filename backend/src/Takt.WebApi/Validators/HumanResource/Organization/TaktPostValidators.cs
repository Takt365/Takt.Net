// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Organization
// 文件名称：TaktPostValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Post DTO 验证器（根据实体 TaktPost 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Organization;

/// <summary>
/// Post创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Organization.TaktPost"/> 字段对齐）。
/// </summary>
public class TaktPostCreateDtoValidator : AbstractValidator<TaktPostCreateDto>
{
    public TaktPostCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PostName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.post.postname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.post.postname", 1, 100));

        RuleFor(x => x.PostCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.post.postcode"))
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.postcode", 50));

        RuleFor(x => x.PostCategory)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.post.postcategory"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.post.postcategory", 1, 50));

        RuleFor(x => x.PostLevel)
            .InclusiveBetween(1, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.post.postlevel"));

        RuleFor(x => x.PostDuty)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.postduty", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PostDuty));

        RuleFor(x => x.DataScope)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.post.datascope"));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));

        RuleFor(x => x.PostStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.post.poststatus"));
    }
}

/// <summary>
/// Post更新 DTO 验证器。
/// </summary>
public class TaktPostUpdateDtoValidator : AbstractValidator<TaktPostUpdateDto>
{
    public TaktPostUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPostCreateDtoValidator(localizer));

        RuleFor(x => x.PostId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.post.postid"));

        RuleFor(x => x.PostName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.postname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PostName));

        RuleFor(x => x.PostCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.postcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PostCategory));

        RuleFor(x => x.PostDuty)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.postduty", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PostDuty));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.post.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));
    }
}
