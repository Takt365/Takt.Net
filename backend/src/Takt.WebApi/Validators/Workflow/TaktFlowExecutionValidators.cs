// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Workflow
// 文件名称：TaktFlowExecutionValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowExecution DTO 验证器（根据实体 TaktFlowExecution 自动生成）
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
/// FlowExecution创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowExecution"/> 字段对齐）。
/// </summary>
public class TaktFlowExecutionCreateDtoValidator : AbstractValidator<TaktFlowExecutionCreateDto>
{
    public TaktFlowExecutionCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.InstanceCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowexecution.instancecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowexecution.instancecode", 1, 50));

        RuleFor(x => x.SchemeKey)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowexecution.schemekey"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowexecution.schemekey", 1, 100));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowexecution.schemename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowexecution.schemename", 1, 200));

        RuleFor(x => x.FromNodeId)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.fromnodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromNodeId));

        RuleFor(x => x.FromNodeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.fromnodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FromNodeName));

        RuleFor(x => x.ToNodeId)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowexecution.tonodeid"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowexecution.tonodeid", 1, 100));

        RuleFor(x => x.ToNodeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowexecution.tonodename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowexecution.tonodename", 1, 200));

        RuleFor(x => x.TransitionType)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowexecution.transitiontype"));

        RuleFor(x => x.TransitionUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowexecution.transitionusername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowexecution.transitionusername", 1, 50));

        RuleFor(x => x.TransitionDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.transitiondeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionDeptName));

        RuleFor(x => x.TransitionComment)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.transitioncomment", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionComment));

        RuleFor(x => x.TransitionAttachments)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.transitionattachments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionAttachments));
    }
}

/// <summary>
/// FlowExecution更新 DTO 验证器。
/// </summary>
public class TaktFlowExecutionUpdateDtoValidator : AbstractValidator<TaktFlowExecutionUpdateDto>
{
    public TaktFlowExecutionUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktFlowExecutionCreateDtoValidator(localizer));

        RuleFor(x => x.FlowExecutionId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.flowexecution.flowexecutionid"));

        RuleFor(x => x.InstanceCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.instancecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InstanceCode));

        RuleFor(x => x.SchemeKey)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.schemekey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeKey));

        RuleFor(x => x.SchemeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.schemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.FromNodeId)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.fromnodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromNodeId));

        RuleFor(x => x.FromNodeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.fromnodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FromNodeName));

        RuleFor(x => x.ToNodeId)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.tonodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ToNodeId));

        RuleFor(x => x.ToNodeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.tonodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ToNodeName));

        RuleFor(x => x.TransitionUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.transitionusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionUserName));

        RuleFor(x => x.TransitionDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.transitiondeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionDeptName));

        RuleFor(x => x.TransitionComment)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.transitioncomment", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionComment));

        RuleFor(x => x.TransitionAttachments)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowexecution.transitionattachments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionAttachments));
    }
}
