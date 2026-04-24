// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Statistics.Kanban
// 文件名称：TaktKanbanVisitPersonValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：KanbanVisitPerson DTO 验证器（根据实体 TaktKanbanVisitPerson 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Kanban;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Statistics.Kanban;

/// <summary>
/// KanbanVisitPerson创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Kanban.TaktKanbanVisitPerson"/> 字段对齐）。
/// </summary>
public class TaktKanbanVisitPersonCreateDtoValidator : AbstractValidator<TaktKanbanVisitPersonCreateDto>
{
    public TaktKanbanVisitPersonCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.Department)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanvisitperson.department", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Department));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanvisitperson.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.PersonName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanvisitperson.personname"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanvisitperson.personname", 1, 50));
    }
}

/// <summary>
/// KanbanVisitPerson更新 DTO 验证器。
/// </summary>
public class TaktKanbanVisitPersonUpdateDtoValidator : AbstractValidator<TaktKanbanVisitPersonUpdateDto>
{
    public TaktKanbanVisitPersonUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktKanbanVisitPersonCreateDtoValidator(localizer));

        RuleFor(x => x.KanbanVisitPersonId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanvisitperson.kanbanvisitpersonid"));

        RuleFor(x => x.Department)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanvisitperson.department", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Department));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanvisitperson.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.PersonName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanvisitperson.personname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PersonName));
    }
}
