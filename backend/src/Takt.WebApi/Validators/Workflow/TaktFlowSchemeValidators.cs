// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Workflow
// 文件名称：TaktFlowSchemeValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowScheme DTO 验证器（根据实体 TaktFlowScheme 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Workflow;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Workflow;

/// <summary>
/// FlowScheme创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowScheme"/> 字段对齐）。
/// </summary>
public class TaktFlowSchemeCreateDtoValidator : AbstractValidator<TaktFlowSchemeCreateDto>
{
    public TaktFlowSchemeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.SchemeKey)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowscheme.schemekey"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowscheme.schemekey", 1, 100));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowscheme.schemename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowscheme.schemename", 1, 200));

        RuleFor(x => x.SchemeCategory)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowscheme.schemecategory"));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowscheme.schemedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowscheme.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));

        RuleFor(x => x.SchemeStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowscheme.schemestatus"));
    }
}

/// <summary>
/// FlowScheme更新 DTO 验证器。
/// </summary>
public class TaktFlowSchemeUpdateDtoValidator : AbstractValidator<TaktFlowSchemeUpdateDto>
{
    public TaktFlowSchemeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktFlowSchemeCreateDtoValidator(localizer));

        RuleFor(x => x.FlowSchemeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.flowscheme.flowschemeid"));

        RuleFor(x => x.SchemeKey)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowscheme.schemekey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeKey));

        RuleFor(x => x.SchemeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowscheme.schemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowscheme.schemedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowscheme.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));
    }
}
