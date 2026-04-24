// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Statistics.Kanban
// 文件名称：TaktKanbanVisitValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：KanbanVisit DTO 验证器（根据实体 TaktKanbanVisit 自动生成）
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
/// KanbanVisit创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Kanban.TaktKanbanVisit"/> 字段对齐）。
/// </summary>
public class TaktKanbanVisitCreateDtoValidator : AbstractValidator<TaktKanbanVisitCreateDto>
{
    public TaktKanbanVisitCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanvisit.companyname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanvisit.companyname", 1, 200));
    }
}

/// <summary>
/// KanbanVisit更新 DTO 验证器。
/// </summary>
public class TaktKanbanVisitUpdateDtoValidator : AbstractValidator<TaktKanbanVisitUpdateDto>
{
    public TaktKanbanVisitUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktKanbanVisitCreateDtoValidator(localizer));

        RuleFor(x => x.KanbanVisitId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanvisit.kanbanvisitid"));

        RuleFor(x => x.CompanyName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanvisit.companyname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyName));
    }
}
