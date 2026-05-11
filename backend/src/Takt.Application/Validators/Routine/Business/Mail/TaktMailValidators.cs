// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.Mail
// 文件名称：TaktMailValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Mail DTO 验证器（根据实体 TaktMail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.Mail;

namespace Takt.Application.Validators.Routine.Business.Mail;

/// <summary>
/// Mail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.Mail.TaktMail"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktMailCreateDtoValidator : AbstractValidator<TaktMailCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMailCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.MailCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.mail.mailcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.mail.mailcode", 1, 50));

        RuleFor(x => x.MailSubject)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.mail.mailsubject"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.mail.mailsubject", 1, 200));

        RuleFor(x => x.MailContent)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.mail.mailcontent"));

        RuleFor(x => x.MailType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.mail.mailtype"));

        RuleFor(x => x.SenderName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.mail.sendername"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.mail.sendername", 1, 50));

        RuleFor(x => x.SenderEmail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.mail.senderemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SenderEmail));

        RuleFor(x => x.RecipientList)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.mail.recipientlist"))
            .Length(1, 2000).WithMessage(_validationMessages.LengthBetween("entity.mail.recipientlist", 1, 2000));

        RuleFor(x => x.CcList)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.mail.cclist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CcList));

        RuleFor(x => x.BccList)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.mail.bcclist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BccList));

        RuleFor(x => x.IsImportant)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.mail.isimportant"));

        RuleFor(x => x.IsUrgent)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.mail.isurgent"));

        RuleFor(x => x.IsReadReceiptRequired)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.mail.isreadreceiptrequired"));

        RuleFor(x => x.IsReadReceiptSent)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.mail.isreadreceiptsent"));

        RuleFor(x => x.MailStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.mail.mailstatus"));

        RuleFor(x => x.SendFailureReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.mail.sendfailurereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SendFailureReason));
    }
}

/// <summary>
/// Mail更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktMailCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>MailId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktMailUpdateDtoValidator : AbstractValidator<TaktMailUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMailUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktMailCreateDtoValidator(validationMessages));

        RuleFor(x => x.MailId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.mail.mailid"));

        RuleFor(x => x.MailCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.mail.mailcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MailCode));

        RuleFor(x => x.MailSubject)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.mail.mailsubject", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MailSubject));

        RuleFor(x => x.SenderName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.mail.sendername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SenderName));

        RuleFor(x => x.SenderEmail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.mail.senderemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SenderEmail));

        RuleFor(x => x.RecipientList)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.mail.recipientlist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.RecipientList));

        RuleFor(x => x.CcList)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.mail.cclist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CcList));

        RuleFor(x => x.BccList)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.mail.bcclist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.BccList));

        RuleFor(x => x.SendFailureReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.mail.sendfailurereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SendFailureReason));
    }
}
