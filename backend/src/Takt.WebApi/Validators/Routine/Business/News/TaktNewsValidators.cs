// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.News
// 文件名称：TaktNewsValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：News DTO 验证器（根据实体 TaktNews 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.News;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Business.News;

/// <summary>
/// News创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNews"/> 字段对齐）。
/// </summary>
public class TaktNewsCreateDtoValidator : AbstractValidator<TaktNewsCreateDto>
{
    public TaktNewsCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.NewsCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.news.newscode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.news.newscode", 1, 50));

        RuleFor(x => x.NewsCategory)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.news.newscategory"));

        RuleFor(x => x.NewsTitle)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.news.newstitle"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.news.newstitle", 1, 200));

        RuleFor(x => x.NewsSummary)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.news.newssummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsSummary));

        RuleFor(x => x.NewsContent)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.news.newscontent"));

        RuleFor(x => x.NewsCoverImage)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.news.newscoverimage", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsCoverImage));

        RuleFor(x => x.IsTop)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.news.istop"));

        RuleFor(x => x.IsRecommended)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.news.isrecommended"));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.news.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PublisherName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.news.publishername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.news.publishername", 1, 50));

        RuleFor(x => x.NewsStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.news.newsstatus"));
    }
}

/// <summary>
/// News更新 DTO 验证器。
/// </summary>
public class TaktNewsUpdateDtoValidator : AbstractValidator<TaktNewsUpdateDto>
{
    public TaktNewsUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktNewsCreateDtoValidator(localizer));

        RuleFor(x => x.NewsId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.news.newsid"));

        RuleFor(x => x.NewsCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.news.newscode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsCode));

        RuleFor(x => x.NewsTitle)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.news.newstitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsTitle));

        RuleFor(x => x.NewsSummary)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.news.newssummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsSummary));

        RuleFor(x => x.NewsCoverImage)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.news.newscoverimage", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsCoverImage));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.news.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PublisherName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.news.publishername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PublisherName));
    }
}
