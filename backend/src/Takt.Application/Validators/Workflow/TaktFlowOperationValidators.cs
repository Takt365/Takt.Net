// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Workflow
// 文件名称：TaktFlowOperationValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowOperation DTO 验证器（根据实体 TaktFlowOperation 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Workflow;

namespace Takt.Application.Validators.Workflow;

/// <summary>
/// FlowOperation创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowOperation"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFlowOperationCreateDtoValidator : AbstractValidator<TaktFlowOperationCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowOperationCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.InstanceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowoperation.instancecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.flowoperation.instancecode", 1, 50));

        RuleFor(x => x.SchemeKey)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowoperation.schemekey"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.flowoperation.schemekey", 1, 100));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowoperation.schemename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.flowoperation.schemename", 1, 200));

        RuleFor(x => x.OperationType)
            .InclusiveBetween(0, 10)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowoperation.operationtype"));

        RuleFor(x => x.NodeId)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowoperation.nodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NodeId));

        RuleFor(x => x.NodeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowoperation.nodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NodeName));

        RuleFor(x => x.OperationContent)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowoperation.operationcontent", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationContent));

        RuleFor(x => x.OperationComment)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowoperation.operationcomment", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationComment));

        RuleFor(x => x.OperationResult)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowoperation.operationresult"));

        RuleFor(x => x.ErrorMessage)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowoperation.errormessage", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMessage));
    }
}

/// <summary>
/// FlowOperation更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFlowOperationCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FlowOperationId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFlowOperationUpdateDtoValidator : AbstractValidator<TaktFlowOperationUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowOperationUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFlowOperationCreateDtoValidator(validationMessages));

        RuleFor(x => x.FlowOperationId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.flowoperation.flowoperationid"));

        RuleFor(x => x.InstanceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowoperation.instancecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InstanceCode));

        RuleFor(x => x.SchemeKey)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowoperation.schemekey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeKey));

        RuleFor(x => x.SchemeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowoperation.schemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.NodeId)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowoperation.nodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NodeId));

        RuleFor(x => x.NodeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowoperation.nodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NodeName));

        RuleFor(x => x.OperationContent)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowoperation.operationcontent", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationContent));

        RuleFor(x => x.OperationComment)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowoperation.operationcomment", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationComment));

        RuleFor(x => x.ErrorMessage)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowoperation.errormessage", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMessage));
    }
}
