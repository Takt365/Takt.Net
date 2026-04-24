// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgeValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Knowledge DTO 验证器（根据实体 TaktKnowledge 自动生成）
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
/// Knowledge创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktKnowledge"/> 字段对齐）。
/// </summary>
public class TaktKnowledgeCreateDtoValidator : AbstractValidator<TaktKnowledgeCreateDto>
{
    public TaktKnowledgeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.knowledge.title"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.knowledge.title", 1, 200));

        RuleFor(x => x.Summary)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledge.summary", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Summary));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledge.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.Tags)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledge.tags", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Tags));

        RuleFor(x => x.KnowledgeStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.knowledge.knowledgestatus"));

        RuleFor(x => x.IsPublished)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.knowledge.ispublished"));
    }
}

/// <summary>
/// Knowledge更新 DTO 验证器。
/// </summary>
public class TaktKnowledgeUpdateDtoValidator : AbstractValidator<TaktKnowledgeUpdateDto>
{
    public TaktKnowledgeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktKnowledgeCreateDtoValidator(localizer));

        RuleFor(x => x.KnowledgeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.knowledge.knowledgeid"));

        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledge.title", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Title));

        RuleFor(x => x.Summary)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledge.summary", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Summary));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledge.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.Tags)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledge.tags", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Tags));
    }
}
