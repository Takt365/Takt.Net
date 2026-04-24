// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.News
// 文件名称：TaktNewsFavoriteValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：NewsFavorite DTO 验证器（根据实体 TaktNewsFavorite 自动生成）
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
/// NewsFavorite创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNewsFavorite"/> 字段对齐）。
/// </summary>
public class TaktNewsFavoriteCreateDtoValidator : AbstractValidator<TaktNewsFavoriteCreateDto>
{
    public TaktNewsFavoriteCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.FavoriteTags)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsfavorite.favoritetags", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FavoriteTags));
    }
}

/// <summary>
/// NewsFavorite更新 DTO 验证器。
/// </summary>
public class TaktNewsFavoriteUpdateDtoValidator : AbstractValidator<TaktNewsFavoriteUpdateDto>
{
    public TaktNewsFavoriteUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktNewsFavoriteCreateDtoValidator(localizer));

        RuleFor(x => x.NewsFavoriteId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.newsfavorite.newsfavoriteid"));

        RuleFor(x => x.FavoriteTags)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsfavorite.favoritetags", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FavoriteTags));
    }
}
