// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.HelpDesk
// 文件名称：TaktTicketCategoryAssignValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TicketCategoryAssign DTO 验证器（根据实体 TaktTicketCategoryAssign 自动生成）
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
/// TicketCategoryAssign创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktTicketCategoryAssign"/> 字段对齐）。
/// </summary>
public class TaktTicketCategoryAssignCreateDtoValidator : AbstractValidator<TaktTicketCategoryAssignCreateDto>
{
    public TaktTicketCategoryAssignCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CategoryCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ticketcategoryassign.categorycode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ticketcategoryassign.categorycode", 1, 50));

        RuleFor(x => x.AssigneeName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketcategoryassign.assigneename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssigneeName));
    }
}

/// <summary>
/// TicketCategoryAssign更新 DTO 验证器。
/// </summary>
public class TaktTicketCategoryAssignUpdateDtoValidator : AbstractValidator<TaktTicketCategoryAssignUpdateDto>
{
    public TaktTicketCategoryAssignUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTicketCategoryAssignCreateDtoValidator(localizer));

        RuleFor(x => x.TicketCategoryAssignId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ticketcategoryassign.ticketcategoryassignid"));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketcategoryassign.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.AssigneeName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketcategoryassign.assigneename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssigneeName));
    }
}
