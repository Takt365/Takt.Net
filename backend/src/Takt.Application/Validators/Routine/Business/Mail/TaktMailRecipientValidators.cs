// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.Mail
// 文件名称：TaktMailRecipientValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：MailRecipient DTO 验证器（根据实体 TaktMailRecipient 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.Mail;

namespace Takt.Application.Validators.Routine.Business.Mail;

/// <summary>
/// MailRecipient创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.Mail.TaktMailRecipient"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktMailRecipientCreateDtoValidator : AbstractValidator<TaktMailRecipientCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMailRecipientCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.RecipientName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.mailrecipient.recipientname"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.mailrecipient.recipientname", 1, 50));

        RuleFor(x => x.RecipientEmail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.mailrecipient.recipientemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RecipientEmail));

        RuleFor(x => x.RecipientType)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.mailrecipient.recipienttype"));

        RuleFor(x => x.ReadStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.mailrecipient.readstatus"));

        RuleFor(x => x.IsRecipientDeleted)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.mailrecipient.isrecipientdeleted"));

        RuleFor(x => x.IsStarred)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.mailrecipient.isstarred"));

        RuleFor(x => x.IsFlagged)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.mailrecipient.isflagged"));
    }
}

/// <summary>
/// MailRecipient更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktMailRecipientCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>MailRecipientId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktMailRecipientUpdateDtoValidator : AbstractValidator<TaktMailRecipientUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMailRecipientUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktMailRecipientCreateDtoValidator(validationMessages));

        RuleFor(x => x.MailRecipientId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.mailrecipient.mailrecipientid"));

        RuleFor(x => x.RecipientName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.mailrecipient.recipientname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RecipientName));

        RuleFor(x => x.RecipientEmail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.mailrecipient.recipientemail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RecipientEmail));
    }
}
