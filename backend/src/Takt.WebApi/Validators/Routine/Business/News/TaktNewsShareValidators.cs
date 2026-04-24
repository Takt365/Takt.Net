// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.News
// 文件名称：TaktNewsShareValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：NewsShare DTO 验证器（根据实体 TaktNewsShare 自动生成）
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
/// NewsShare创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNewsShare"/> 字段对齐）。
/// </summary>
public class TaktNewsShareCreateDtoValidator : AbstractValidator<TaktNewsShareCreateDto>
{
    public TaktNewsShareCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.SharePlatform)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.newsshare.shareplatform"));

        RuleFor(x => x.ShareRemark)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsshare.shareremark", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ShareRemark));
    }
}

/// <summary>
/// NewsShare更新 DTO 验证器。
/// </summary>
public class TaktNewsShareUpdateDtoValidator : AbstractValidator<TaktNewsShareUpdateDto>
{
    public TaktNewsShareUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktNewsShareCreateDtoValidator(localizer));

        RuleFor(x => x.NewsShareId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.newsshare.newsshareid"));

        RuleFor(x => x.ShareRemark)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsshare.shareremark", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ShareRemark));
    }
}
