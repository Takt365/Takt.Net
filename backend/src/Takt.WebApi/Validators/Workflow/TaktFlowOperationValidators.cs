// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Workflow
// 文件名称：TaktFlowOperationValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowOperation DTO 验证器（根据实体 TaktFlowOperation 自动生成）
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
/// FlowOperation创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowOperation"/> 字段对齐）。
/// </summary>
public class TaktFlowOperationCreateDtoValidator : AbstractValidator<TaktFlowOperationCreateDto>
{
    public TaktFlowOperationCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.InstanceCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowoperation.instancecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowoperation.instancecode", 1, 50));

        RuleFor(x => x.SchemeKey)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowoperation.schemekey"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowoperation.schemekey", 1, 100));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.flowoperation.schemename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.flowoperation.schemename", 1, 200));

        RuleFor(x => x.OperationType)
            .InclusiveBetween(0, 10)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowoperation.operationtype"));

        RuleFor(x => x.NodeId)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.nodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NodeId));

        RuleFor(x => x.NodeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.nodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NodeName));

        RuleFor(x => x.OperationContent)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.operationcontent", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationContent));

        RuleFor(x => x.OperationComment)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.operationcomment", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationComment));

        RuleFor(x => x.OperationResult)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.flowoperation.operationresult"));

        RuleFor(x => x.ErrorMessage)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.errormessage", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMessage));
    }
}

/// <summary>
/// FlowOperation更新 DTO 验证器。
/// </summary>
public class TaktFlowOperationUpdateDtoValidator : AbstractValidator<TaktFlowOperationUpdateDto>
{
    public TaktFlowOperationUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktFlowOperationCreateDtoValidator(localizer));

        RuleFor(x => x.FlowOperationId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.flowoperation.flowoperationid"));

        RuleFor(x => x.InstanceCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.instancecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InstanceCode));

        RuleFor(x => x.SchemeKey)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.schemekey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeKey));

        RuleFor(x => x.SchemeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.schemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.NodeId)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.nodeid", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NodeId));

        RuleFor(x => x.NodeName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.nodename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NodeName));

        RuleFor(x => x.OperationContent)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.operationcontent", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationContent));

        RuleFor(x => x.OperationComment)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.operationcomment", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationComment));

        RuleFor(x => x.ErrorMessage)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.flowoperation.errormessage", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMessage));
    }
}
