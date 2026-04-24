// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgeChangeLogValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：KnowledgeChangeLog DTO 验证器（根据实体 TaktKnowledgeChangeLog 自动生成）
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
/// KnowledgeChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktKnowledgeChangeLog"/> 字段对齐）。
/// </summary>
public class TaktKnowledgeChangeLogCreateDtoValidator : AbstractValidator<TaktKnowledgeChangeLogCreateDto>
{
    public TaktKnowledgeChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.KnowledgeTitle)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledgechangelog.knowledgetitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.KnowledgeTitle));

        RuleFor(x => x.ChangeType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.knowledgechangelog.changetype"));

        RuleFor(x => x.ChangeSummary)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledgechangelog.changesummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeSummary));

        RuleFor(x => x.ChangeField)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledgechangelog.changefield", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeField));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledgechangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// KnowledgeChangeLog更新 DTO 验证器。
/// </summary>
public class TaktKnowledgeChangeLogUpdateDtoValidator : AbstractValidator<TaktKnowledgeChangeLogUpdateDto>
{
    public TaktKnowledgeChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktKnowledgeChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.KnowledgeChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.knowledgechangelog.knowledgechangelogid"));

        RuleFor(x => x.KnowledgeTitle)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledgechangelog.knowledgetitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.KnowledgeTitle));

        RuleFor(x => x.ChangeSummary)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledgechangelog.changesummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeSummary));

        RuleFor(x => x.ChangeField)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledgechangelog.changefield", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeField));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.knowledgechangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
