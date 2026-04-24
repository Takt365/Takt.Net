// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeEducationValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeEducation DTO 验证器（根据实体 TaktEmployeeEducation 自动生成）
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
/// EmployeeEducation创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeEducation"/> 字段对齐）。
/// </summary>
public class TaktEmployeeEducationCreateDtoValidator : AbstractValidator<TaktEmployeeEducationCreateDto>
{
    public TaktEmployeeEducationCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.EducationLevel)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeeeducation.educationlevel"));

        RuleFor(x => x.SchoolName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeeeducation.schoolname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeeeducation.schoolname", 1, 200));

        RuleFor(x => x.MajorName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeeducation.majorname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MajorName));

        RuleFor(x => x.DegreeLevel)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeeeducation.degreelevel"));

        RuleFor(x => x.IsHighest)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeeeducation.ishighest"));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeeducation.certificateno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));
    }
}

/// <summary>
/// EmployeeEducation更新 DTO 验证器。
/// </summary>
public class TaktEmployeeEducationUpdateDtoValidator : AbstractValidator<TaktEmployeeEducationUpdateDto>
{
    public TaktEmployeeEducationUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeEducationCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeEducationId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeeeducation.employeeeducationid"));

        RuleFor(x => x.SchoolName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeeducation.schoolname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchoolName));

        RuleFor(x => x.MajorName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeeducation.majorname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MajorName));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeeducation.certificateno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));
    }
}
