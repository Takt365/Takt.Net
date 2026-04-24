// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.News
// 文件名称：TaktNewsCommentLikeValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：NewsCommentLike DTO 验证器（根据实体 TaktNewsCommentLike 自动生成）
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
/// NewsCommentLike创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNewsCommentLike"/> 字段对齐）。
/// </summary>
public class TaktNewsCommentLikeCreateDtoValidator : AbstractValidator<TaktNewsCommentLikeCreateDto>
{
    public TaktNewsCommentLikeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
    }
}

/// <summary>
/// NewsCommentLike更新 DTO 验证器。
/// </summary>
public class TaktNewsCommentLikeUpdateDtoValidator : AbstractValidator<TaktNewsCommentLikeUpdateDto>
{
    public TaktNewsCommentLikeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktNewsCommentLikeCreateDtoValidator(localizer));

        RuleFor(x => x.NewsCommentLikeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.newscommentlike.newscommentlikeid"));

    }
}
