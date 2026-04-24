// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.HelpDesk
// 文件名称：TaktTicketEvaluationValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TicketEvaluation DTO 验证器（根据实体 TaktTicketEvaluation 自动生成）
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
/// TicketEvaluation创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktTicketEvaluation"/> 字段对齐）。
/// </summary>
public class TaktTicketEvaluationCreateDtoValidator : AbstractValidator<TaktTicketEvaluationCreateDto>
{
    public TaktTicketEvaluationCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.Comment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketevaluation.comment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Comment));

        RuleFor(x => x.EvaluatorName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketevaluation.evaluatorname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluatorName));
    }
}

/// <summary>
/// TicketEvaluation更新 DTO 验证器。
/// </summary>
public class TaktTicketEvaluationUpdateDtoValidator : AbstractValidator<TaktTicketEvaluationUpdateDto>
{
    public TaktTicketEvaluationUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTicketEvaluationCreateDtoValidator(localizer));

        RuleFor(x => x.TicketEvaluationId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ticketevaluation.ticketevaluationid"));

        RuleFor(x => x.Comment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketevaluation.comment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Comment));

        RuleFor(x => x.EvaluatorName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ticketevaluation.evaluatorname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluatorName));
    }
}
