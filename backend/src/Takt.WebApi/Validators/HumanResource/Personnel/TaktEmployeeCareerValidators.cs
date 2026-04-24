// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeCareerValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeCareer DTO 验证器（根据实体 TaktEmployeeCareer 自动生成）
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
/// EmployeeCareer创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeCareer"/> 字段对齐）。
/// </summary>
public class TaktEmployeeCareerCreateDtoValidator : AbstractValidator<TaktEmployeeCareerCreateDto>
{
    public TaktEmployeeCareerCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DeptName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeecareer.deptname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeecareer.deptname", 1, 100));

        RuleFor(x => x.PostName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.postname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PostName));

        RuleFor(x => x.JobLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.joblevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JobLevel));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.WorkLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.worklocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkLocation));

        RuleFor(x => x.WorkNature)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeecareer.worknature"));

        RuleFor(x => x.EmploymentType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeecareer.employmenttype"));

        RuleFor(x => x.IsPrimary)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeecareer.isprimary"));

        RuleFor(x => x.DirectManagerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.directmanagername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DirectManagerName));
    }
}

/// <summary>
/// EmployeeCareer更新 DTO 验证器。
/// </summary>
public class TaktEmployeeCareerUpdateDtoValidator : AbstractValidator<TaktEmployeeCareerUpdateDto>
{
    public TaktEmployeeCareerUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeCareerCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeCareerId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeecareer.employeecareerid"));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PostName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.postname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PostName));

        RuleFor(x => x.JobLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.joblevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JobLevel));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.WorkLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.worklocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkLocation));

        RuleFor(x => x.DirectManagerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecareer.directmanagername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DirectManagerName));
    }
}
