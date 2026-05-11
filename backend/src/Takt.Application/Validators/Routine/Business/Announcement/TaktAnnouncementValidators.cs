// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.Announcement
// 文件名称：TaktAnnouncementValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Announcement DTO 验证器（根据实体 TaktAnnouncement 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.Announcement;

namespace Takt.Application.Validators.Routine.Business.Announcement;

/// <summary>
/// Announcement创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.Announcement.TaktAnnouncement"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAnnouncementCreateDtoValidator : AbstractValidator<TaktAnnouncementCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAnnouncementCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.AnnouncementCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.announcement.announcementcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.announcement.announcementcode", 1, 50));

        RuleFor(x => x.AnnouncementTitle)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.announcement.announcementtitle"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.announcement.announcementtitle", 1, 200));

        RuleFor(x => x.AnnouncementType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.announcement.announcementtype"));

        RuleFor(x => x.AnnouncementContent)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.announcement.announcementcontent"));

        RuleFor(x => x.PublishScope)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.announcement.publishscope"));

        RuleFor(x => x.PublishScopeConfig)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.announcement.publishscopeconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.PublishScopeConfig));

        RuleFor(x => x.IsTop)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.announcement.istop"));

        RuleFor(x => x.IsUrgent)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.announcement.isurgent"));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.announcement.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PublisherName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.announcement.publishername"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.announcement.publishername", 1, 50));

        RuleFor(x => x.AnnouncementStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.announcement.announcementstatus"));
    }
}

/// <summary>
/// Announcement更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAnnouncementCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AnnouncementId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAnnouncementUpdateDtoValidator : AbstractValidator<TaktAnnouncementUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAnnouncementUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAnnouncementCreateDtoValidator(validationMessages));

        RuleFor(x => x.AnnouncementId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.announcement.announcementid"));

        RuleFor(x => x.AnnouncementCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.announcement.announcementcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AnnouncementCode));

        RuleFor(x => x.AnnouncementTitle)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.announcement.announcementtitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.AnnouncementTitle));

        RuleFor(x => x.PublishScopeConfig)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.announcement.publishscopeconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.PublishScopeConfig));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.announcement.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PublisherName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.announcement.publishername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PublisherName));
    }
}
