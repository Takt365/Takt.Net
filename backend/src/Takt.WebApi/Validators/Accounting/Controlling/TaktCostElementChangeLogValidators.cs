// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Controlling
// 文件名称：TaktCostElementChangeLogValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CostElementChangeLog DTO 验证器（根据实体 TaktCostElementChangeLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Accounting.Controlling;

/// <summary>
/// CostElementChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktCostElementChangeLog"/> 字段对齐）。
/// </summary>
public class TaktCostElementChangeLogCreateDtoValidator : AbstractValidator<TaktCostElementChangeLogCreateDto>
{
    public TaktCostElementChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CostElementCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.costelementchangelog.costelementcode"))
            .Must(TaktRegexHelper.IsValidCostElementCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.costelementchangelog.costelementcode"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelementchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelementchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelementchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// CostElementChangeLog更新 DTO 验证器。
/// </summary>
public class TaktCostElementChangeLogUpdateDtoValidator : AbstractValidator<TaktCostElementChangeLogUpdateDto>
{
    public TaktCostElementChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktCostElementChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.CostElementChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.costelementchangelog.costelementchangelogid"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelementchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelementchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelementchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
