// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Scheduling
// 文件名称：TaktApsScheduleChangeLogValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ApsScheduleChangeLog DTO 验证器（根据实体 TaktApsScheduleChangeLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Scheduling;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Scheduling;

/// <summary>
/// ApsScheduleChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Scheduling.TaktApsScheduleChangeLog"/> 字段对齐）。
/// </summary>
public class TaktApsScheduleChangeLogCreateDtoValidator : AbstractValidator<TaktApsScheduleChangeLogCreateDto>
{
    public TaktApsScheduleChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedulechangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeType)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.apsschedulechangelog.changetype"));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedulechangelog.changereason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedulechangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));
    }
}

/// <summary>
/// ApsScheduleChangeLog更新 DTO 验证器。
/// </summary>
public class TaktApsScheduleChangeLogUpdateDtoValidator : AbstractValidator<TaktApsScheduleChangeLogUpdateDto>
{
    public TaktApsScheduleChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktApsScheduleChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.ApsScheduleChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.apsschedulechangelog.apsschedulechangelogid"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedulechangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedulechangelog.changereason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.apsschedulechangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));
    }
}
