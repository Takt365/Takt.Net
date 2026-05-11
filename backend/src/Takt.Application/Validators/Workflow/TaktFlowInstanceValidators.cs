// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Workflow
// 文件名称：TaktFlowInstanceValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowInstance DTO 验证器（根据实体 TaktFlowInstance 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Workflow;

namespace Takt.Application.Validators.Workflow;

/// <summary>
/// FlowInstance创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowInstance"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFlowInstanceCreateDtoValidator : AbstractValidator<TaktFlowInstanceCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowInstanceCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.InstanceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowinstance.instancecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.flowinstance.instancecode", 1, 50));

        RuleFor(x => x.SchemeKey)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowinstance.schemekey"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.flowinstance.schemekey", 1, 100));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowinstance.schemename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.flowinstance.schemename", 1, 200));

        RuleFor(x => x.BusinessKey)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowinstance.businesskey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessKey));

        RuleFor(x => x.BusinessType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowinstance.businesstype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessType));

        RuleFor(x => x.StartUserName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowinstance.startusername"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.flowinstance.startusername", 1, 50));

        RuleFor(x => x.StartDeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowinstance.startdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.StartDeptName));

        RuleFor(x => x.CurrentNodeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowinstance.currentnodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentNodeName));

        RuleFor(x => x.ActivityName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowinstance.activityname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ActivityName));

        RuleFor(x => x.PreviousNodeId)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowinstance.previousnodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PreviousNodeId));

        RuleFor(x => x.MakerList)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowinstance.makerlist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MakerList));

        RuleFor(x => x.InstanceStatus)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowinstance.instancestatus"));

        RuleFor(x => x.IsSuspended)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowinstance.issuspended"));

        RuleFor(x => x.SuspendReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.flowinstance.suspendreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SuspendReason));

        RuleFor(x => x.Priority)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowinstance.priority"));

        RuleFor(x => x.ProcessTitle)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowinstance.processtitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessTitle));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowinstance.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));
    }
}

/// <summary>
/// FlowInstance更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFlowInstanceCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FlowInstanceId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFlowInstanceUpdateDtoValidator : AbstractValidator<TaktFlowInstanceUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowInstanceUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFlowInstanceCreateDtoValidator(validationMessages));

        RuleFor(x => x.FlowInstanceId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.flowinstance.flowinstanceid"));

        RuleFor(x => x.InstanceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowinstance.instancecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InstanceCode));

        RuleFor(x => x.SchemeKey)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowinstance.schemekey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeKey));

        RuleFor(x => x.SchemeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowinstance.schemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.BusinessKey)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowinstance.businesskey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessKey));

        RuleFor(x => x.BusinessType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowinstance.businesstype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BusinessType));

        RuleFor(x => x.StartUserName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowinstance.startusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StartUserName));

        RuleFor(x => x.StartDeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowinstance.startdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.StartDeptName));

        RuleFor(x => x.CurrentNodeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowinstance.currentnodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentNodeName));

        RuleFor(x => x.ActivityName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowinstance.activityname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ActivityName));

        RuleFor(x => x.PreviousNodeId)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowinstance.previousnodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PreviousNodeId));

        RuleFor(x => x.MakerList)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowinstance.makerlist", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MakerList));

        RuleFor(x => x.SuspendReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.flowinstance.suspendreason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SuspendReason));

        RuleFor(x => x.ProcessTitle)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowinstance.processtitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessTitle));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowinstance.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));
    }
}
