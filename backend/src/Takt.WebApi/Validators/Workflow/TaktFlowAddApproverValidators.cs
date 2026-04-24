// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Workflow
// 文件名称：TaktFlowAddApproverValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowAddApprover DTO 验证器（根据实体 TaktFlowAddApprover 自动生成）
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
/// FlowAddApprover创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowAddApprover"/> 字段对齐）。
/// </summary>
public class TaktFlowAddApproverCreateDtoValidator : AbstractValidator<TaktFlowAddApproverCreateDto>
{
    public TaktFlowAddApproverCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ActivityId)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowaddapprover.activityid"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowaddapprover.activityid", 1, 100));

        RuleFor(x => x.ApproverUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowaddapprover.approverusername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowaddapprover.approverusername", 1, 50));

        RuleFor(x => x.ApproveType)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.approvetype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveType));

        RuleFor(x => x.VerifyComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.verifycomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.VerifyComment));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.CreateUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.createusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CreateUserName));
    }
}

/// <summary>
/// FlowAddApprover更新 DTO 验证器。
/// </summary>
public class TaktFlowAddApproverUpdateDtoValidator : AbstractValidator<TaktFlowAddApproverUpdateDto>
{
    public TaktFlowAddApproverUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktFlowAddApproverCreateDtoValidator(localizer));

        RuleFor(x => x.FlowAddApproverId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.flowaddapprover.flowaddapproverid"));

        RuleFor(x => x.ActivityId)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.activityid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ActivityId));

        RuleFor(x => x.ApproverUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.approverusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverUserName));

        RuleFor(x => x.ApproveType)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.approvetype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveType));

        RuleFor(x => x.VerifyComment)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.verifycomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.VerifyComment));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.CreateUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowaddapprover.createusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CreateUserName));
    }
}
