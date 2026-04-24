// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.HelpDesk
// 文件名称：TaktSelfServiceValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SelfService DTO 验证器（根据实体 TaktSelfService 自动生成）
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
/// SelfService创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktSelfService"/> 字段对齐）。
/// </summary>
public class TaktSelfServiceCreateDtoValidator : AbstractValidator<TaktSelfServiceCreateDto>
{
    public TaktSelfServiceCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ServiceName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.selfservice.servicename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.selfservice.servicename", 1, 100));

        RuleFor(x => x.ServiceType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.selfservice.servicetype"));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.selfservice.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.LinkOrCode)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.selfservice.linkorcode", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkOrCode));

        RuleFor(x => x.IconUrl)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.selfservice.iconurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.IconUrl));

        RuleFor(x => x.SelfServiceStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.selfservice.selfservicestatus"));
    }
}

/// <summary>
/// SelfService更新 DTO 验证器。
/// </summary>
public class TaktSelfServiceUpdateDtoValidator : AbstractValidator<TaktSelfServiceUpdateDto>
{
    public TaktSelfServiceUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSelfServiceCreateDtoValidator(localizer));

        RuleFor(x => x.SelfServiceId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.selfservice.selfserviceid"));

        RuleFor(x => x.ServiceName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.selfservice.servicename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ServiceName));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.selfservice.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.LinkOrCode)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.selfservice.linkorcode", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkOrCode));

        RuleFor(x => x.IconUrl)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.selfservice.iconurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.IconUrl));
    }
}
