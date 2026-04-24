// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.Announcement
// 文件名称：TaktAnnouncementValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Announcement DTO 验证器（根据实体 TaktAnnouncement 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.Announcement;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Business.Announcement;

/// <summary>
/// Announcement创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.Announcement.TaktAnnouncement"/> 字段对齐）。
/// </summary>
public class TaktAnnouncementCreateDtoValidator : AbstractValidator<TaktAnnouncementCreateDto>
{
    public TaktAnnouncementCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.AnnouncementCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.announcement.announcementcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.announcement.announcementcode", 1, 50));

        RuleFor(x => x.AnnouncementTitle)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.announcement.announcementtitle"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.announcement.announcementtitle", 1, 200));

        RuleFor(x => x.AnnouncementType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.announcement.announcementtype"));

        RuleFor(x => x.AnnouncementContent)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.announcement.announcementcontent"));

        RuleFor(x => x.PublishScope)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.announcement.publishscope"));

        RuleFor(x => x.PublishScopeConfig)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.announcement.publishscopeconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.PublishScopeConfig));

        RuleFor(x => x.IsTop)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.announcement.istop"));

        RuleFor(x => x.IsUrgent)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.announcement.isurgent"));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.announcement.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PublisherName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.announcement.publishername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.announcement.publishername", 1, 50));

        RuleFor(x => x.AnnouncementStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.announcement.announcementstatus"));
    }
}

/// <summary>
/// Announcement更新 DTO 验证器。
/// </summary>
public class TaktAnnouncementUpdateDtoValidator : AbstractValidator<TaktAnnouncementUpdateDto>
{
    public TaktAnnouncementUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAnnouncementCreateDtoValidator(localizer));

        RuleFor(x => x.AnnouncementId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.announcement.announcementid"));

        RuleFor(x => x.AnnouncementCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.announcement.announcementcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AnnouncementCode));

        RuleFor(x => x.AnnouncementTitle)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.announcement.announcementtitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.AnnouncementTitle));

        RuleFor(x => x.PublishScopeConfig)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.announcement.publishscopeconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.PublishScopeConfig));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.announcement.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PublisherName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.announcement.publishername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PublisherName));
    }
}
