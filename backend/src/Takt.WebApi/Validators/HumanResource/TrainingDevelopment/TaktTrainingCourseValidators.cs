// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingCourseValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TrainingCourse DTO 验证器（根据实体 TaktTrainingCourse 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.TrainingDevelopment;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.TrainingDevelopment;

/// <summary>
/// TrainingCourse创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.TrainingDevelopment.TaktTrainingCourse"/> 字段对齐）。
/// </summary>
public class TaktTrainingCourseCreateDtoValidator : AbstractValidator<TaktTrainingCourseCreateDto>
{
    public TaktTrainingCourseCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CourseCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.coursecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.coursecode", 1, 50));

        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.coursename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.coursename", 1, 200));

        RuleFor(x => x.CourseType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.coursetype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.coursetype", 1, 50));

        RuleFor(x => x.CourseLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.courselevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.courselevel", 1, 50));

        RuleFor(x => x.CourseDescription)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.coursedescription"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.coursedescription", 1, 1000));

        RuleFor(x => x.CourseObjectives)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.courseobjectives"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.courseobjectives", 1, 1000));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.applicabledepartment"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.applicableposition"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.applicableposition", 1, 100));

        RuleFor(x => x.MainInstructor)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.maininstructor"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.maininstructor", 1, 50));

        RuleFor(x => x.TrainingMethod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.trainingmethod"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.trainingmethod", 1, 50));

        RuleFor(x => x.AssessmentMethod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.assessmentmethod"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.assessmentmethod", 1, 50));

        RuleFor(x => x.IsCertification)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.trainingcourse.iscertification"));

        RuleFor(x => x.CourseOutline)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.courseoutline"))
            .Length(1, 2000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.courseoutline", 1, 2000));

        RuleFor(x => x.MaterialList)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.materiallist"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.trainingcourse.materiallist", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.trainingcourse.status"));
    }
}

/// <summary>
/// TrainingCourse更新 DTO 验证器。
/// </summary>
public class TaktTrainingCourseUpdateDtoValidator : AbstractValidator<TaktTrainingCourseUpdateDto>
{
    public TaktTrainingCourseUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTrainingCourseCreateDtoValidator(localizer));

        RuleFor(x => x.TrainingCourseId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.trainingcourse.trainingcourseid"));

        RuleFor(x => x.CourseCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.coursecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseCode));

        RuleFor(x => x.CourseName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.coursename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseName));

        RuleFor(x => x.CourseType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.coursetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseType));

        RuleFor(x => x.CourseLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.courselevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseLevel));

        RuleFor(x => x.CourseDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.coursedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseDescription));

        RuleFor(x => x.CourseObjectives)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.courseobjectives", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseObjectives));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.MainInstructor)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.maininstructor", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MainInstructor));

        RuleFor(x => x.TrainingMethod)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.trainingmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingMethod));

        RuleFor(x => x.AssessmentMethod)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.assessmentmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssessmentMethod));

        RuleFor(x => x.CourseOutline)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.courseoutline", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseOutline));

        RuleFor(x => x.MaterialList)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.trainingcourse.materiallist", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialList));
    }
}
