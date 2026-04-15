using FluentValidation;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Identity;

/// <summary>
/// 角色创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktRole"/> 及 TaktRegexHelper.RoleCode 对齐）。
/// </summary>
public class TaktRoleCreateDtoValidator : AbstractValidator<TaktRoleCreateDto>
{
    public TaktRoleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.role.name"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.role.name", 1, 100));

        RuleFor(x => x.RoleCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.role.code"))
            .Must(v => TaktRegexHelper.IsMatch(TaktRegexHelper.RoleCode, v))
            .WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternRoleCode", "entity.role.code"));

        RuleFor(x => x.DataScope)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.role.datascope"));

        When(x => x.DataScope == 4, () =>
        {
            RuleFor(x => x.CustomScope)
                .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.role.customscope"))
                .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.role.customscope", 2000));
        });
    }
}

/// <summary>
/// 角色更新 DTO 验证器。
/// </summary>
public class TaktRoleUpdateDtoValidator : AbstractValidator<TaktRoleUpdateDto>
{
    public TaktRoleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktRoleCreateDtoValidator(localizer));

        RuleFor(x => x.RoleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.role.roleid"));
    }
}

/// <summary>
/// 角色状态 DTO 验证器。
/// </summary>
public class TaktRoleStatusDtoValidator : AbstractValidator<TaktRoleStatusDto>
{
    public TaktRoleStatusDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.role.roleid"));

        RuleFor(x => x.RoleStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.role.status"));
    }
}

/// <summary>
/// 角色分配菜单 DTO 验证器。
/// </summary>
public class TaktRoleAssignMenusDtoValidator : AbstractValidator<TaktRoleAssignMenusDto>
{
    public TaktRoleAssignMenusDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.role.roleid"));
    }
}

/// <summary>
/// 角色分配部门 DTO 验证器。
/// </summary>
public class TaktRoleAssignDeptsDtoValidator : AbstractValidator<TaktRoleAssignDeptsDto>
{
    public TaktRoleAssignDeptsDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.role.roleid"));
    }
}
