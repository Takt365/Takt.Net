// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Controlling
// 文件名称：TaktProfitCenterChangeLogValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProfitCenterChangeLog DTO 验证器（根据实体 TaktProfitCenterChangeLog 自动生成）
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
/// ProfitCenterChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktProfitCenterChangeLog"/> 字段对齐）。
/// </summary>
public class TaktProfitCenterChangeLogCreateDtoValidator : AbstractValidator<TaktProfitCenterChangeLogCreateDto>
{
    public TaktProfitCenterChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ProfitCenterCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.profitcenterchangelog.profitcentercode"))
            .Must(TaktRegexHelper.IsValidProfitCenterCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.profitcenterchangelog.profitcentercode"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenterchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenterchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenterchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// ProfitCenterChangeLog更新 DTO 验证器。
/// </summary>
public class TaktProfitCenterChangeLogUpdateDtoValidator : AbstractValidator<TaktProfitCenterChangeLogUpdateDto>
{
    public TaktProfitCenterChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktProfitCenterChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.ProfitCenterChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.profitcenterchangelog.profitcenterchangelogid"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenterchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenterchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenterchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
