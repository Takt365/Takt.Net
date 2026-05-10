// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Statistics.Kanban
// 文件名称：TaktKanbanSchemeValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：KanbanScheme DTO 验证器（根据实体 TaktKanbanScheme 自动生成）
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
/// KanbanScheme创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Kanban.TaktKanbanScheme"/> 字段对齐）。
/// </summary>
public class TaktKanbanSchemeCreateDtoValidator : AbstractValidator<TaktKanbanSchemeCreateDto>
{
    public TaktKanbanSchemeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.SchemeCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.schemecode"))
            .Length(1, 30).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanscheme.schemecode", 1, 30));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.schemename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanscheme.schemename", 1, 100));

        RuleFor(x => x.KanbanType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.kanbantype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanscheme.kanbantype", 1, 50));

        RuleFor(x => x.SchemeDescription)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.schemedescription"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanscheme.schemedescription", 1, 500));

        RuleFor(x => x.DataSourceConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.datasourceconfig"));

        RuleFor(x => x.LayoutConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.layoutconfig"));

        RuleFor(x => x.ComponentConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.componentconfig"));

        RuleFor(x => x.RefreshStrategy)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.refreshstrategy"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanscheme.refreshstrategy", 1, 20));

        RuleFor(x => x.ThemeStyle)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.themestyle"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanscheme.themestyle", 1, 20));

        RuleFor(x => x.IsFullscreen)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.kanbanscheme.isfullscreen"));

        RuleFor(x => x.EnableAlert)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.kanbanscheme.enablealert"));

        RuleFor(x => x.AlertConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.alertconfig"));

        RuleFor(x => x.FilterConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.filterconfig"));

        RuleFor(x => x.SortConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.sortconfig"));

        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.kanbanscheme.plantcode"));

        RuleFor(x => x.WorkshopCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.workshopcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanscheme.workshopcode", 1, 50));

        RuleFor(x => x.LineCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.linecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.kanbanscheme.linecode", 1, 50));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.kanbanscheme.status"));

        RuleFor(x => x.IsPublic)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.kanbanscheme.ispublic"));

        RuleFor(x => x.CreatorIds)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.creatorids"));

        RuleFor(x => x.AccessConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.accessconfig"));
    }
}

/// <summary>
/// KanbanScheme更新 DTO 验证器。
/// </summary>
public class TaktKanbanSchemeUpdateDtoValidator : AbstractValidator<TaktKanbanSchemeUpdateDto>
{
    public TaktKanbanSchemeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktKanbanSchemeCreateDtoValidator(localizer));

        RuleFor(x => x.KanbanSchemeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.kanbanscheme.kanbanschemeid"));

        RuleFor(x => x.SchemeCode)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanscheme.schemecode", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeCode));

        RuleFor(x => x.SchemeName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanscheme.schemename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.KanbanType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanscheme.kanbantype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.KanbanType));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanscheme.schemedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));

        RuleFor(x => x.RefreshStrategy)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanscheme.refreshstrategy", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.RefreshStrategy));

        RuleFor(x => x.ThemeStyle)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanscheme.themestyle", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ThemeStyle));

        RuleFor(x => x.WorkshopCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanscheme.workshopcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopCode));

        RuleFor(x => x.LineCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.kanbanscheme.linecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LineCode));
    }
}
