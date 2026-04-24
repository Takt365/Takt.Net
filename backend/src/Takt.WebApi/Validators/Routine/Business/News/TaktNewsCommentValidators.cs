// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.News
// 文件名称：TaktNewsCommentValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：NewsComment DTO 验证器（根据实体 TaktNewsComment 自动生成）
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
/// NewsComment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNewsComment"/> 字段对齐）。
/// </summary>
public class TaktNewsCommentCreateDtoValidator : AbstractValidator<TaktNewsCommentCreateDto>
{
    public TaktNewsCommentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.UserAvatar)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newscomment.useravatar", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAvatar));

        RuleFor(x => x.ReplyToUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newscomment.replytousername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ReplyToUserName));

        RuleFor(x => x.CommentContent)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.newscomment.commentcontent"))
            .Length(1, 2000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.newscomment.commentcontent", 1, 2000));

        RuleFor(x => x.CommentLevel)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.newscomment.commentlevel"));

        RuleFor(x => x.ApprovalStatus)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newscomment.approvalstatus", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ApprovalStatus));

        RuleFor(x => x.CommentStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.newscomment.commentstatus"));
    }
}

/// <summary>
/// NewsComment更新 DTO 验证器。
/// </summary>
public class TaktNewsCommentUpdateDtoValidator : AbstractValidator<TaktNewsCommentUpdateDto>
{
    public TaktNewsCommentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktNewsCommentCreateDtoValidator(localizer));

        RuleFor(x => x.NewsCommentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.newscomment.newscommentid"));

        RuleFor(x => x.UserAvatar)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newscomment.useravatar", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAvatar));

        RuleFor(x => x.ReplyToUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newscomment.replytousername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ReplyToUserName));

        RuleFor(x => x.CommentContent)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newscomment.commentcontent", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CommentContent));

        RuleFor(x => x.ApprovalStatus)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newscomment.approvalstatus", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ApprovalStatus));
    }
}
