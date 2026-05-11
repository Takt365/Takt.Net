// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Statistics.Kanban
// 文件名称：TaktKanbanSchemeValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：KanbanScheme DTO 验证器（根据实体 TaktKanbanScheme 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Kanban;

namespace Takt.Application.Validators.Statistics.Kanban;

/// <summary>
/// KanbanScheme创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Kanban.TaktKanbanScheme"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktKanbanSchemeCreateDtoValidator : AbstractValidator<TaktKanbanSchemeCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktKanbanSchemeCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.SchemeCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.schemecode"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.kanbanscheme.schemecode", 1, 30));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.schemename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.kanbanscheme.schemename", 1, 100));

        RuleFor(x => x.KanbanType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.kanbantype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.kanbanscheme.kanbantype", 1, 50));

        RuleFor(x => x.SchemeDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.schemedescription"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.kanbanscheme.schemedescription", 1, 500));

        RuleFor(x => x.DataSourceConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.datasourceconfig"));

        RuleFor(x => x.LayoutConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.layoutconfig"));

        RuleFor(x => x.ComponentConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.componentconfig"));

        RuleFor(x => x.RefreshStrategy)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.refreshstrategy"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.kanbanscheme.refreshstrategy", 1, 20));

        RuleFor(x => x.ThemeStyle)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.themestyle"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.kanbanscheme.themestyle", 1, 20));

        RuleFor(x => x.IsFullscreen)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.kanbanscheme.isfullscreen"));

        RuleFor(x => x.EnableAlert)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.kanbanscheme.enablealert"));

        RuleFor(x => x.AlertConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.alertconfig"));

        RuleFor(x => x.FilterConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.filterconfig"));

        RuleFor(x => x.SortConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.sortconfig"));

        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.kanbanscheme.plantcode"));

        RuleFor(x => x.WorkshopCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.workshopcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.kanbanscheme.workshopcode", 1, 50));

        RuleFor(x => x.LineCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.linecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.kanbanscheme.linecode", 1, 50));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.kanbanscheme.status"));

        RuleFor(x => x.IsPublic)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.kanbanscheme.ispublic"));

        RuleFor(x => x.CreatorIds)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.creatorids"));

        RuleFor(x => x.AccessConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.kanbanscheme.accessconfig"));
    }
}

/// <summary>
/// KanbanScheme更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktKanbanSchemeCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>KanbanSchemeId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktKanbanSchemeUpdateDtoValidator : AbstractValidator<TaktKanbanSchemeUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktKanbanSchemeUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktKanbanSchemeCreateDtoValidator(validationMessages));

        RuleFor(x => x.KanbanSchemeId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.kanbanscheme.kanbanschemeid"));

        RuleFor(x => x.SchemeCode)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.kanbanscheme.schemecode", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeCode));

        RuleFor(x => x.SchemeName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.kanbanscheme.schemename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.KanbanType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.kanbanscheme.kanbantype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.KanbanType));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.kanbanscheme.schemedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));

        RuleFor(x => x.RefreshStrategy)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.kanbanscheme.refreshstrategy", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.RefreshStrategy));

        RuleFor(x => x.ThemeStyle)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.kanbanscheme.themestyle", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ThemeStyle));

        RuleFor(x => x.WorkshopCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.kanbanscheme.workshopcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkshopCode));

        RuleFor(x => x.LineCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.kanbanscheme.linecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LineCode));
    }
}
