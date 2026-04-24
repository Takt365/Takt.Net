// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Workflow
// 文件名称：TaktFlowInstanceValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowInstance DTO 验证器（根据实体 TaktFlowInstance 自动生成）
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
/// FlowInstance创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowInstance"/> 字段对齐）。
/// </summary>
public class TaktFlowInstanceCreateDtoValidator : AbstractValidator<TaktFlowInstanceCreateDto>
{
    public TaktFlowInstanceCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.InstanceCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowinstance.instancecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowinstance.instancecode", 1, 50));

        RuleFor(x => x.SchemeKey)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowinstance.schemekey"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowinstance.schemekey", 1, 100));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowinstance.schemename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowinstance.schemename", 1, 200));

        RuleFor(x => x.BusinessKey)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.businesskey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessKey));

        RuleFor(x => x.BusinessType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.businesstype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessType));

        RuleFor(x => x.StartUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowinstance.startusername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowinstance.startusername", 1, 50));

        RuleFor(x => x.StartDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.startdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.StartDeptName));

        RuleFor(x => x.CurrentNodeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.currentnodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentNodeName));

        RuleFor(x => x.ActivityName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.activityname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ActivityName));

        RuleFor(x => x.PreviousNodeId)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.previousnodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PreviousNodeId));

        RuleFor(x => x.MakerList)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.makerlist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MakerList));

        RuleFor(x => x.InstanceStatus)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowinstance.instancestatus"));

        RuleFor(x => x.IsSuspended)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowinstance.issuspended"));

        RuleFor(x => x.SuspendReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.suspendreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SuspendReason));

        RuleFor(x => x.Priority)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowinstance.priority"));

        RuleFor(x => x.ProcessTitle)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.processtitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessTitle));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));
    }
}

/// <summary>
/// FlowInstance更新 DTO 验证器。
/// </summary>
public class TaktFlowInstanceUpdateDtoValidator : AbstractValidator<TaktFlowInstanceUpdateDto>
{
    public TaktFlowInstanceUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktFlowInstanceCreateDtoValidator(localizer));

        RuleFor(x => x.FlowInstanceId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.flowinstance.flowinstanceid"));

        RuleFor(x => x.InstanceCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.instancecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InstanceCode));

        RuleFor(x => x.SchemeKey)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.schemekey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeKey));

        RuleFor(x => x.SchemeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.schemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.BusinessKey)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.businesskey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessKey));

        RuleFor(x => x.BusinessType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.businesstype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessType));

        RuleFor(x => x.StartUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.startusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StartUserName));

        RuleFor(x => x.StartDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.startdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.StartDeptName));

        RuleFor(x => x.CurrentNodeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.currentnodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentNodeName));

        RuleFor(x => x.ActivityName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.activityname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ActivityName));

        RuleFor(x => x.PreviousNodeId)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.previousnodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PreviousNodeId));

        RuleFor(x => x.MakerList)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.makerlist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MakerList));

        RuleFor(x => x.SuspendReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.suspendreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SuspendReason));

        RuleFor(x => x.ProcessTitle)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.processtitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessTitle));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowinstance.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));
    }
}
