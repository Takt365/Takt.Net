// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.News
// 文件名称：TaktNewsAttachmentValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：NewsAttachment DTO 验证器（根据实体 TaktNewsAttachment 自动生成）
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
/// NewsAttachment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNewsAttachment"/> 字段对齐）。
/// </summary>
public class TaktNewsAttachmentCreateDtoValidator : AbstractValidator<TaktNewsAttachmentCreateDto>
{
    public TaktNewsAttachmentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.newsattachment.filename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.newsattachment.filename", 1, 200));

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.newsattachment.filepath"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.newsattachment.filepath", 1, 500));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsattachment.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.FileExtension)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsattachment.fileextension", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FileExtension));
    }
}

/// <summary>
/// NewsAttachment更新 DTO 验证器。
/// </summary>
public class TaktNewsAttachmentUpdateDtoValidator : AbstractValidator<TaktNewsAttachmentUpdateDto>
{
    public TaktNewsAttachmentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktNewsAttachmentCreateDtoValidator(localizer));

        RuleFor(x => x.NewsAttachmentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.newsattachment.newsattachmentid"));

        RuleFor(x => x.FileName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsattachment.filename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileName));

        RuleFor(x => x.FilePath)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsattachment.filepath", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FilePath));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsattachment.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.FileExtension)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.newsattachment.fileextension", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FileExtension));
    }
}
