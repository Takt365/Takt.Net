using FluentValidation;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Identity;

/// <summary>
/// 租户创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktTenant"/> 及 <see cref="Takt.Domain.Entities.TaktEntityBase.ConfigId"/> nvarchar(2) 对齐）。
/// </summary>
public class TaktTenantCreateDtoValidator : AbstractValidator<TaktTenantCreateDto>
{
    public TaktTenantCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TenantName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.tenant.name"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.tenant.name", 1, 100));

        RuleFor(x => x.TenantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.tenant.code"))
            .Must(v => TaktRegexHelper.IsMatch(TaktRegexHelper.Code, v))
            .WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternCode", "entity.tenant.code"));

        RuleFor(x => x.ConfigId)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.tenant.configid"))
            .Length(1, 2).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.tenant.configid", 1, 2));

        RuleFor(x => x)
            .Must(x => !x.StartTime.HasValue || !x.EndTime.HasValue || x.EndTime.Value >= x.StartTime.Value)
            .WithMessage(_ => TaktValidationMessages.EndBeforeStart(localizer, "entity.tenant.endtime", "entity.tenant.starttime"));
    }
}

/// <summary>
/// 租户更新 DTO 验证器。
/// </summary>
public class TaktTenantUpdateDtoValidator : AbstractValidator<TaktTenantUpdateDto>
{
    public TaktTenantUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTenantCreateDtoValidator(localizer));

        RuleFor(x => x.TenantId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.tenant.tenantid"));
    }
}

/// <summary>
/// 租户状态 DTO 验证器。
/// </summary>
public class TaktTenantStatusDtoValidator : AbstractValidator<TaktTenantStatusDto>
{
    public TaktTenantStatusDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TenantId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.tenant.tenantid"));

        RuleFor(x => x.TenantStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.tenant.status"));
    }
}
