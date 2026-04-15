using FluentValidation;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Organization;

/// <summary>
/// 部门创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Organization.TaktDept"/> 及 TaktRegexHelper.DeptCode 对齐）。
/// </summary>
public class TaktDeptCreateDtoValidator : AbstractValidator<TaktDeptCreateDto>
{
    public TaktDeptCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DeptName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.name"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.dept.name", 1, 100));

        RuleFor(x => x.DeptCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.code"))
            .Must(v => TaktRegexHelper.IsMatch(TaktRegexHelper.DeptCode, v))
            .WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternDeptCode", "entity.dept.code"));

        RuleFor(x => x.ParentId)
            .GreaterThanOrEqualTo(0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dept.parentid"));

        RuleFor(x => x.DeptType)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dept.type"));

        RuleFor(x => x.DataScope)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dept.datascope"));

        When(x => x.DataScope == 4, () =>
        {
            RuleFor(x => x.CustomScope)
                .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.customscope"))
                .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.customscope", 2000));
        });

        RuleFor(x => x.DeptHeadId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.headid"));

        RuleFor(x => x.CostCenterCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.costcenter", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterCode));

        RuleFor(x => x.DeptPhone)
            .Must(v => TaktRegexHelper.IsMatch(TaktRegexHelper.TelCn, v!))
            .WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternTelCn", "entity.dept.phone"))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptPhone));

        RuleFor(x => x.DeptMail)
            .Must(TaktRegexHelper.IsValidEmail)
            .WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternEmail", "entity.dept.mail"))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptMail));

        RuleFor(x => x.DeptAddr)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.addr", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptAddr));
    }
}

/// <summary>
/// 部门更新 DTO 验证器。
/// </summary>
public class TaktDeptUpdateDtoValidator : AbstractValidator<TaktDeptUpdateDto>
{
    public TaktDeptUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktDeptCreateDtoValidator(localizer));

        RuleFor(x => x.DeptId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.deptid"));
    }
}

/// <summary>
/// 部门状态 DTO 验证器。
/// </summary>
public class TaktDeptStatusDtoValidator : AbstractValidator<TaktDeptStatusDto>
{
    public TaktDeptStatusDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DeptId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.deptid"));

        RuleFor(x => x.DeptStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dept.status"));
    }
}

/// <summary>
/// 部门分配用户 DTO 验证器。
/// </summary>
public class TaktDeptAssignUsersDtoValidator : AbstractValidator<TaktDeptAssignUsersDto>
{
    public TaktDeptAssignUsersDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DeptId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.deptid"));
    }
}
