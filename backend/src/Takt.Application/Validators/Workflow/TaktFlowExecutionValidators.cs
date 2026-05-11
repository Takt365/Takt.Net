// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Workflow
// 文件名称：TaktFlowExecutionValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowExecution DTO 验证器（根据实体 TaktFlowExecution 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Workflow;

namespace Takt.Application.Validators.Workflow;

/// <summary>
/// FlowExecution创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowExecution"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFlowExecutionCreateDtoValidator : AbstractValidator<TaktFlowExecutionCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowExecutionCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.InstanceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowexecution.instancecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.flowexecution.instancecode", 1, 50));

        RuleFor(x => x.SchemeKey)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowexecution.schemekey"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.flowexecution.schemekey", 1, 100));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowexecution.schemename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.flowexecution.schemename", 1, 200));

        RuleFor(x => x.FromNodeId)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowexecution.fromnodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromNodeId));

        RuleFor(x => x.FromNodeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowexecution.fromnodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FromNodeName));

        RuleFor(x => x.ToNodeId)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowexecution.tonodeid"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.flowexecution.tonodeid", 1, 100));

        RuleFor(x => x.ToNodeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowexecution.tonodename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.flowexecution.tonodename", 1, 200));

        RuleFor(x => x.TransitionType)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowexecution.transitiontype"));

        RuleFor(x => x.TransitionUserName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowexecution.transitionusername"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.flowexecution.transitionusername", 1, 50));

        RuleFor(x => x.TransitionDeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowexecution.transitiondeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionDeptName));

        RuleFor(x => x.TransitionComment)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowexecution.transitioncomment", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionComment));

        RuleFor(x => x.TransitionAttachments)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowexecution.transitionattachments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionAttachments));
    }
}

/// <summary>
/// FlowExecution更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFlowExecutionCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FlowExecutionId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFlowExecutionUpdateDtoValidator : AbstractValidator<TaktFlowExecutionUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowExecutionUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFlowExecutionCreateDtoValidator(validationMessages));

        RuleFor(x => x.FlowExecutionId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.flowexecution.flowexecutionid"));

        RuleFor(x => x.InstanceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowexecution.instancecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InstanceCode));

        RuleFor(x => x.SchemeKey)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowexecution.schemekey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeKey));

        RuleFor(x => x.SchemeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowexecution.schemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.FromNodeId)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowexecution.fromnodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromNodeId));

        RuleFor(x => x.FromNodeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowexecution.fromnodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FromNodeName));

        RuleFor(x => x.ToNodeId)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowexecution.tonodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ToNodeId));

        RuleFor(x => x.ToNodeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowexecution.tonodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ToNodeName));

        RuleFor(x => x.TransitionUserName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowexecution.transitionusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionUserName));

        RuleFor(x => x.TransitionDeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowexecution.transitiondeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionDeptName));

        RuleFor(x => x.TransitionComment)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowexecution.transitioncomment", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionComment));

        RuleFor(x => x.TransitionAttachments)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowexecution.transitionattachments", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.TransitionAttachments));
    }
}
