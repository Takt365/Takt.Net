// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Workflow
// 文件名称：TaktFlowAddApproverValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowAddApprover DTO 验证器（根据实体 TaktFlowAddApprover 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Workflow;

namespace Takt.Application.Validators.Workflow;

/// <summary>
/// FlowAddApprover创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowAddApprover"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFlowAddApproverCreateDtoValidator : AbstractValidator<TaktFlowAddApproverCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowAddApproverCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ActivityId)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowaddapprover.activityid"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.flowaddapprover.activityid", 1, 100));

        RuleFor(x => x.ApproverUserName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowaddapprover.approverusername"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.flowaddapprover.approverusername", 1, 50));

        RuleFor(x => x.ApproveType)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.approvetype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveType));

        RuleFor(x => x.VerifyComment)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.verifycomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.VerifyComment));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.CreateUserName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.createusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CreateUserName));
    }
}

/// <summary>
/// FlowAddApprover更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFlowAddApproverCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FlowAddApproverId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFlowAddApproverUpdateDtoValidator : AbstractValidator<TaktFlowAddApproverUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowAddApproverUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFlowAddApproverCreateDtoValidator(validationMessages));

        RuleFor(x => x.FlowAddApproverId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.flowaddapprover.flowaddapproverid"));

        RuleFor(x => x.ActivityId)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.activityid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ActivityId));

        RuleFor(x => x.ApproverUserName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.approverusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverUserName));

        RuleFor(x => x.ApproveType)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.approvetype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveType));

        RuleFor(x => x.VerifyComment)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.verifycomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.VerifyComment));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.CreateUserName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowaddapprover.createusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CreateUserName));
    }
}
