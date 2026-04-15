using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Personnel;

/// <summary>
/// 员工创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployee"/> 字段对齐；员工编码由服务端按编号规则生成，创建时不校验请求中的 EmployeeCode）。
/// </summary>
public class TaktEmployeeCreateDtoValidator : AbstractValidator<TaktEmployeeCreateDto>
{
    public TaktEmployeeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RealName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employee.realname"))
            .Length(2, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employee.realname", 2, 50))
            .Must(TaktRegexHelper.IsValidFullName).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternFullName", "entity.employee.realname"));

        RuleFor(x => x.FormerName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.formername", 100))
            .Must(TaktRegexHelper.IsValidFullName).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternFullName", "entity.employee.formername"))
            .When(x => !string.IsNullOrWhiteSpace(x.FormerName));

        RuleFor(x => x.FullName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.fullname", 100))
            .Must(TaktRegexHelper.IsValidFullName).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternFullName", "entity.employee.fullname"))
            .When(x => !string.IsNullOrWhiteSpace(x.FullName));

        RuleFor(x => x.NativeName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.nativename", 100))
            .Must(TaktRegexHelper.IsValidFullName).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternFullName", "entity.employee.nativename"))
            .When(x => !string.IsNullOrWhiteSpace(x.NativeName));

        RuleFor(x => x.DisplayName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.displayname", 100))
            .Must(TaktRegexHelper.IsValidFullName).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternFullName", "entity.employee.displayname"))
            .When(x => !string.IsNullOrWhiteSpace(x.DisplayName));

        RuleFor(x => x.IdCard)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employee.idcard"))
            .Must(TaktRegexHelper.IsValidIdCard).WithMessage(TaktValidationMessages.IdCardInvalid(localizer, "entity.employee.idcard"));

        RuleFor(x => x.Email)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternEmail", "entity.employee.email"))
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.email", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Phone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternPhone", "entity.employee.phone"))
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Nationality)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.nationality", 50));
    }
}

/// <summary>
/// 员工更新 DTO 验证器。
/// </summary>
public class TaktEmployeeUpdateDtoValidator : AbstractValidator<TaktEmployeeUpdateDto>
{
    public TaktEmployeeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employee.id"));

        RuleFor(x => x.EmployeeCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employee.employeecode"))
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.employeecode", 50));
    }
}
