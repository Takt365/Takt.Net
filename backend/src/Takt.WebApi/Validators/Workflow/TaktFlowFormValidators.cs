// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Workflow
// 文件名称：TaktFlowFormValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowForm DTO 验证器（根据实体 TaktFlowForm 自动生成）
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
/// FlowForm创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowForm"/> 字段对齐）。
/// </summary>
public class TaktFlowFormCreateDtoValidator : AbstractValidator<TaktFlowFormCreateDto>
{
    public TaktFlowFormCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.FormCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowform.formcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowform.formcode", 1, 50));

        RuleFor(x => x.FormName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowform.formname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowform.formname", 1, 200));

        RuleFor(x => x.FormCategory)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowform.formcategory"));

        RuleFor(x => x.FormType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowform.formtype"));

        RuleFor(x => x.FormVersion)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowform.formversion"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowform.formversion", 1, 20));

        RuleFor(x => x.IsDatasource)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowform.isdatasource"));

        RuleFor(x => x.RelatedDataBaseName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowform.relateddatabasename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedDataBaseName));

        RuleFor(x => x.RelatedTableName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowform.relatedtablename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedTableName));

        RuleFor(x => x.RelatedFormField)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowform.relatedformfield", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedFormField));

        RuleFor(x => x.FormStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowform.formstatus"));
    }
}

/// <summary>
/// FlowForm更新 DTO 验证器。
/// </summary>
public class TaktFlowFormUpdateDtoValidator : AbstractValidator<TaktFlowFormUpdateDto>
{
    public TaktFlowFormUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktFlowFormCreateDtoValidator(localizer));

        RuleFor(x => x.FlowFormId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.flowform.flowformid"));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowform.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));

        RuleFor(x => x.FormName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowform.formname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FormName));

        RuleFor(x => x.FormVersion)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowform.formversion", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FormVersion));

        RuleFor(x => x.RelatedDataBaseName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowform.relateddatabasename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedDataBaseName));

        RuleFor(x => x.RelatedTableName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowform.relatedtablename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedTableName));

        RuleFor(x => x.RelatedFormField)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowform.relatedformfield", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedFormField));
    }
}
