// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Statistics.Logging
// 文件名称：TaktQuartzLogValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QuartzLog DTO 验证器（根据实体 TaktQuartzLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Statistics.Logging;

/// <summary>
/// QuartzLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Logging.TaktQuartzLog"/> 字段对齐）。
/// </summary>
public class TaktQuartzLogCreateDtoValidator : AbstractValidator<TaktQuartzLogCreateDto>
{
    public TaktQuartzLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.JobName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.quartzlog.jobname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.quartzlog.jobname", 1, 200));

        RuleFor(x => x.JobGroup)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.quartzlog.jobgroup"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.quartzlog.jobgroup", 1, 200));

        RuleFor(x => x.TriggerName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.quartzlog.triggername"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.quartzlog.triggername", 1, 200));

        RuleFor(x => x.TriggerGroup)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.quartzlog.triggergroup"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.quartzlog.triggergroup", 1, 200));

        RuleFor(x => x.ExecuteStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.quartzlog.executestatus"));

        RuleFor(x => x.ErrorMsg)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.quartzlog.errormsg", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMsg));
    }
}

/// <summary>
/// QuartzLog更新 DTO 验证器。
/// </summary>
public class TaktQuartzLogUpdateDtoValidator : AbstractValidator<TaktQuartzLogUpdateDto>
{
    public TaktQuartzLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktQuartzLogCreateDtoValidator(localizer));

        RuleFor(x => x.QuartzLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.quartzlog.quartzlogid"));

        RuleFor(x => x.JobName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.quartzlog.jobname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.JobName));

        RuleFor(x => x.JobGroup)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.quartzlog.jobgroup", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.JobGroup));

        RuleFor(x => x.TriggerName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.quartzlog.triggername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TriggerName));

        RuleFor(x => x.TriggerGroup)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.quartzlog.triggergroup", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TriggerGroup));

        RuleFor(x => x.ErrorMsg)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.quartzlog.errormsg", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMsg));
    }
}
