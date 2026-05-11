// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.HelpDesk
// 文件名称：TaktTicketValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Ticket DTO 验证器（根据实体 TaktTicket 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.HelpDesk;

namespace Takt.Application.Validators.Routine.Business.HelpDesk;

/// <summary>
/// Ticket创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktTicket"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktTicketCreateDtoValidator : AbstractValidator<TaktTicketCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTicketCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.TicketNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ticket.ticketno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ticket.ticketno", 1, 50));

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ticket.title"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.ticket.title", 1, 200));

        RuleFor(x => x.TicketStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.ticket.ticketstatus"));

        RuleFor(x => x.Priority)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.ticket.priority"));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticket.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.TicketSource)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.ticket.ticketsource"));

        RuleFor(x => x.SubmitterName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticket.submittername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SubmitterName));

        RuleFor(x => x.AssigneeName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticket.assigneename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssigneeName));

        RuleFor(x => x.ApplicantDeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ticket.applicantdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantDeptName));

        RuleFor(x => x.Applicant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticket.applicant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Applicant));
    }
}

/// <summary>
/// Ticket更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktTicketCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>TicketId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktTicketUpdateDtoValidator : AbstractValidator<TaktTicketUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTicketUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktTicketCreateDtoValidator(validationMessages));

        RuleFor(x => x.TicketId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ticket.ticketid"));

        RuleFor(x => x.TicketNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticket.ticketno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TicketNo));

        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ticket.title", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Title));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticket.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.SubmitterName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticket.submittername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SubmitterName));

        RuleFor(x => x.AssigneeName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticket.assigneename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssigneeName));

        RuleFor(x => x.ApplicantDeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ticket.applicantdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantDeptName));

        RuleFor(x => x.Applicant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticket.applicant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Applicant));
    }
}
