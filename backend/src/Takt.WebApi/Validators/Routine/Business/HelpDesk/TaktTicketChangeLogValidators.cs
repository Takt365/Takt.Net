// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.HelpDesk
// 文件名称：TaktTicketChangeLogValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TicketChangeLog DTO 验证器（根据实体 TaktTicketChangeLog 自动生成）
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
/// TicketChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktTicketChangeLog"/> 字段对齐）。
/// </summary>
public class TaktTicketChangeLogCreateDtoValidator : AbstractValidator<TaktTicketChangeLogCreateDto>
{
    public TaktTicketChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TicketNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketchangelog.ticketno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TicketNo));

        RuleFor(x => x.ChangeType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ticketchangelog.changetype"));

        RuleFor(x => x.ChangeSummary)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketchangelog.changesummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeSummary));

        RuleFor(x => x.ChangeField)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketchangelog.changefield", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeField));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// TicketChangeLog更新 DTO 验证器。
/// </summary>
public class TaktTicketChangeLogUpdateDtoValidator : AbstractValidator<TaktTicketChangeLogUpdateDto>
{
    public TaktTicketChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTicketChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.TicketChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ticketchangelog.ticketchangelogid"));

        RuleFor(x => x.TicketNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketchangelog.ticketno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TicketNo));

        RuleFor(x => x.ChangeSummary)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketchangelog.changesummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeSummary));

        RuleFor(x => x.ChangeField)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketchangelog.changefield", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeField));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
