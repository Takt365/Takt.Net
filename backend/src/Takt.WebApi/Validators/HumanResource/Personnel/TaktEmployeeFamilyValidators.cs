// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeFamilyValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeFamily DTO 验证器（根据实体 TaktEmployeeFamily 自动生成）
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
/// EmployeeFamily创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeFamily"/> 字段对齐）。
/// </summary>
public class TaktEmployeeFamilyCreateDtoValidator : AbstractValidator<TaktEmployeeFamilyCreateDto>
{
    public TaktEmployeeFamilyCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.MemberName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeefamily.membername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeefamily.membername", 1, 50));

        RuleFor(x => x.RelationType)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeefamily.relationtype"));

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeefamily.phonenumber", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.WorkUnit)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeefamily.workunit", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkUnit));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeefamily.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.IsEmergencyContact)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeefamily.isemergencycontact"));
    }
}

/// <summary>
/// EmployeeFamily更新 DTO 验证器。
/// </summary>
public class TaktEmployeeFamilyUpdateDtoValidator : AbstractValidator<TaktEmployeeFamilyUpdateDto>
{
    public TaktEmployeeFamilyUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeFamilyCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeFamilyId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeefamily.employeefamilyid"));

        RuleFor(x => x.MemberName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeefamily.membername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MemberName));

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeefamily.phonenumber", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.WorkUnit)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeefamily.workunit", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkUnit));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeefamily.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));
    }
}
