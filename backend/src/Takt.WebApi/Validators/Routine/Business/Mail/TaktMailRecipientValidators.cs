// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.Mail
// 文件名称：TaktMailRecipientValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：MailRecipient DTO 验证器（根据实体 TaktMailRecipient 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.Mail;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Business.Mail;

/// <summary>
/// MailRecipient创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.Mail.TaktMailRecipient"/> 字段对齐）。
/// </summary>
public class TaktMailRecipientCreateDtoValidator : AbstractValidator<TaktMailRecipientCreateDto>
{
    public TaktMailRecipientCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RecipientName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.mailrecipient.recipientname"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.mailrecipient.recipientname", 1, 50));

        RuleFor(x => x.RecipientEmail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mailrecipient.recipientemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RecipientEmail));

        RuleFor(x => x.RecipientType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mailrecipient.recipienttype"));

        RuleFor(x => x.ReadStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mailrecipient.readstatus"));

        RuleFor(x => x.IsRecipientDeleted)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mailrecipient.isrecipientdeleted"));

        RuleFor(x => x.IsStarred)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mailrecipient.isstarred"));

        RuleFor(x => x.IsFlagged)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mailrecipient.isflagged"));
    }
}

/// <summary>
/// MailRecipient更新 DTO 验证器。
/// </summary>
public class TaktMailRecipientUpdateDtoValidator : AbstractValidator<TaktMailRecipientUpdateDto>
{
    public TaktMailRecipientUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktMailRecipientCreateDtoValidator(localizer));

        RuleFor(x => x.MailRecipientId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.mailrecipient.mailrecipientid"));

        RuleFor(x => x.RecipientName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mailrecipient.recipientname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RecipientName));

        RuleFor(x => x.RecipientEmail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mailrecipient.recipientemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RecipientEmail));
    }
}
