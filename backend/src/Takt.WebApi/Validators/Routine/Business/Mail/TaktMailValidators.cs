// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.Mail
// 文件名称：TaktMailValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Mail DTO 验证器（根据实体 TaktMail 自动生成）
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
/// Mail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.Mail.TaktMail"/> 字段对齐）。
/// </summary>
public class TaktMailCreateDtoValidator : AbstractValidator<TaktMailCreateDto>
{
    public TaktMailCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MailCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.mail.mailcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.mail.mailcode", 1, 50));

        RuleFor(x => x.MailSubject)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.mail.mailsubject"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.mail.mailsubject", 1, 200));

        RuleFor(x => x.MailContent)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.mail.mailcontent"));

        RuleFor(x => x.MailType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mail.mailtype"));

        RuleFor(x => x.SenderName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.mail.sendername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.mail.sendername", 1, 50));

        RuleFor(x => x.SenderEmail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.senderemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SenderEmail));

        RuleFor(x => x.RecipientList)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.mail.recipientlist"))
            .Length(1, 2000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.mail.recipientlist", 1, 2000));

        RuleFor(x => x.CcList)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.cclist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CcList));

        RuleFor(x => x.BccList)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.bcclist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BccList));

        RuleFor(x => x.IsImportant)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mail.isimportant"));

        RuleFor(x => x.IsUrgent)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mail.isurgent"));

        RuleFor(x => x.IsReadReceiptRequired)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mail.isreadreceiptrequired"));

        RuleFor(x => x.IsReadReceiptSent)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mail.isreadreceiptsent"));

        RuleFor(x => x.MailStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.mail.mailstatus"));

        RuleFor(x => x.SendFailureReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.sendfailurereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SendFailureReason));
    }
}

/// <summary>
/// Mail更新 DTO 验证器。
/// </summary>
public class TaktMailUpdateDtoValidator : AbstractValidator<TaktMailUpdateDto>
{
    public TaktMailUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktMailCreateDtoValidator(localizer));

        RuleFor(x => x.MailId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.mail.mailid"));

        RuleFor(x => x.MailCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.mailcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MailCode));

        RuleFor(x => x.MailSubject)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.mailsubject", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MailSubject));

        RuleFor(x => x.SenderName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.sendername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SenderName));

        RuleFor(x => x.SenderEmail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.senderemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SenderEmail));

        RuleFor(x => x.RecipientList)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.recipientlist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.RecipientList));

        RuleFor(x => x.CcList)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.cclist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CcList));

        RuleFor(x => x.BccList)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.bcclist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BccList));

        RuleFor(x => x.SendFailureReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.mail.sendfailurereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SendFailureReason));
    }
}
