// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputDetailValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AssyOutputDetail DTO 验证器（根据实体 TaktAssyOutputDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// AssyOutputDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktAssyOutputDetail"/> 字段对齐）。
/// </summary>
public class TaktAssyOutputDetailCreateDtoValidator : AbstractValidator<TaktAssyOutputDetailCreateDto>
{
    public TaktAssyOutputDetailCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TimePeriod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.assyoutputdetail.timeperiod"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.assyoutputdetail.timeperiod", 1, 20));

        RuleFor(x => x.DowntimeReason)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutputdetail.downtimereason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeReason));

        RuleFor(x => x.DowntimeDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutputdetail.downtimedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeDescription));

        RuleFor(x => x.UnachievedReason)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutputdetail.unachievedreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedReason));

        RuleFor(x => x.UnachievedDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutputdetail.unachieveddescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedDescription));
    }
}

/// <summary>
/// AssyOutputDetail更新 DTO 验证器。
/// </summary>
public class TaktAssyOutputDetailUpdateDtoValidator : AbstractValidator<TaktAssyOutputDetailUpdateDto>
{
    public TaktAssyOutputDetailUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAssyOutputDetailCreateDtoValidator(localizer));

        RuleFor(x => x.AssyOutputDetailId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.assyoutputdetail.assyoutputdetailid"));

        RuleFor(x => x.TimePeriod)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutputdetail.timeperiod", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.TimePeriod));

        RuleFor(x => x.DowntimeReason)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutputdetail.downtimereason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeReason));

        RuleFor(x => x.DowntimeDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutputdetail.downtimedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeDescription));

        RuleFor(x => x.UnachievedReason)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutputdetail.unachievedreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedReason));

        RuleFor(x => x.UnachievedDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assyoutputdetail.unachieveddescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedDescription));
    }
}
