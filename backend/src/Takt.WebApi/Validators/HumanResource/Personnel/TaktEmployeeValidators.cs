// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Employee DTO 验证器（根据实体 TaktEmployee 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Personnel;

/// <summary>
/// Employee创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployee"/> 字段对齐）。
/// </summary>
public class TaktEmployeeCreateDtoValidator : AbstractValidator<TaktEmployeeCreateDto>
{
    public TaktEmployeeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.EmployeeCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employee.employeecode"))
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.employeecode", 50));

        RuleFor(x => x.RealName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employee.realname"))
            .Must(TaktRegexHelper.IsValidFullName).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternFullName", "entity.employee.realname"));

        RuleFor(x => x.FormerName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.formername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FormerName));

        RuleFor(x => x.FullName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.fullname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FullName));

        RuleFor(x => x.NativeName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.nativename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativeName));

        RuleFor(x => x.DisplayName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.displayname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DisplayName));

        RuleFor(x => x.Gender)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employee.gender"));

        RuleFor(x => x.IdCard)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employee.idcard"))
            .Must(TaktRegexHelper.IsValidIdCard).WithMessage(TaktValidationMessages.IdCardInvalid(localizer, "entity.employee.idcard"));

        RuleFor(x => x.Phone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternPhone", "entity.employee.phone"))
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.phone", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Email)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternEmail", "entity.employee.email"))
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.email", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Avatar)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.avatar", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Avatar));

        RuleFor(x => x.Nationality)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employee.nationality"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employee.nationality", 1, 50));

        RuleFor(x => x.PoliticalStatus)
            .InclusiveBetween(0, 12)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employee.politicalstatus"));

        RuleFor(x => x.MaritalStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employee.maritalstatus"));

        RuleFor(x => x.NativePlace)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.nativeplace", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativePlace));

        RuleFor(x => x.CurrentAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.currentaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentAddress));

        RuleFor(x => x.RegisteredAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.registeredaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegisteredAddress));

        RuleFor(x => x.EmployeeStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employee.employeestatus"));
    }
}

/// <summary>
/// Employee更新 DTO 验证器。
/// </summary>
public class TaktEmployeeUpdateDtoValidator : AbstractValidator<TaktEmployeeUpdateDto>
{
    public TaktEmployeeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employee.employeeid"));

        RuleFor(x => x.FormerName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.formername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FormerName));

        RuleFor(x => x.FullName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.fullname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FullName));

        RuleFor(x => x.NativeName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.nativename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativeName));

        RuleFor(x => x.DisplayName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.displayname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DisplayName));

        RuleFor(x => x.Phone)
            .Must(TaktRegexHelper.IsValidPhone).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternPhone", "entity.employee.phone"))
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.phone", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Email)
            .Must(TaktRegexHelper.IsValidEmail).WithMessage(TaktValidationMessages.Pattern(localizer, "validation.patternEmail", "entity.employee.email"))
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.email", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Avatar)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.avatar", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Avatar));

        RuleFor(x => x.Nationality)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.nationality", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Nationality));

        RuleFor(x => x.NativePlace)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.nativeplace", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.NativePlace));

        RuleFor(x => x.CurrentAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.currentaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CurrentAddress));

        RuleFor(x => x.RegisteredAddress)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employee.registeredaddress", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.RegisteredAddress));
    }
}
