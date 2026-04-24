// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.HelpDesk
// 文件名称：TaktTicketValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Ticket DTO 验证器（根据实体 TaktTicket 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.HelpDesk;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Business.HelpDesk;

/// <summary>
/// Ticket创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktTicket"/> 字段对齐）。
/// </summary>
public class TaktTicketCreateDtoValidator : AbstractValidator<TaktTicketCreateDto>
{
    public TaktTicketCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TicketNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ticket.ticketno"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ticket.ticketno", 1, 50));

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ticket.title"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ticket.title", 1, 200));

        RuleFor(x => x.TicketStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ticket.ticketstatus"));

        RuleFor(x => x.Priority)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ticket.priority"));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.TicketSource)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ticket.ticketsource"));

        RuleFor(x => x.SubmitterName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.submittername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SubmitterName));

        RuleFor(x => x.AssigneeName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.assigneename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssigneeName));

        RuleFor(x => x.ApplicantDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.applicantdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantDeptName));

        RuleFor(x => x.Applicant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.applicant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Applicant));
    }
}

/// <summary>
/// Ticket更新 DTO 验证器。
/// </summary>
public class TaktTicketUpdateDtoValidator : AbstractValidator<TaktTicketUpdateDto>
{
    public TaktTicketUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTicketCreateDtoValidator(localizer));

        RuleFor(x => x.TicketId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ticket.ticketid"));

        RuleFor(x => x.TicketNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.ticketno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TicketNo));

        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.title", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Title));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.SubmitterName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.submittername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SubmitterName));

        RuleFor(x => x.AssigneeName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.assigneename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssigneeName));

        RuleFor(x => x.ApplicantDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.applicantdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantDeptName));

        RuleFor(x => x.Applicant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticket.applicant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Applicant));
    }
}
