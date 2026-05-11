// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingCourseValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TrainingCourse DTO 验证器（根据实体 TaktTrainingCourse 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.TrainingDevelopment;

namespace Takt.Application.Validators.HumanResource.TrainingDevelopment;

/// <summary>
/// TrainingCourse创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.TrainingDevelopment.TaktTrainingCourse"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktTrainingCourseCreateDtoValidator : AbstractValidator<TaktTrainingCourseCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTrainingCourseCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CourseCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.coursecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.coursecode", 1, 50));

        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.coursename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.coursename", 1, 200));

        RuleFor(x => x.CourseType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.coursetype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.coursetype", 1, 50));

        RuleFor(x => x.CourseLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.courselevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.courselevel", 1, 50));

        RuleFor(x => x.CourseDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.coursedescription"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.coursedescription", 1, 1000));

        RuleFor(x => x.CourseObjectives)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.courseobjectives"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.courseobjectives", 1, 1000));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.applicabledepartment"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.applicableposition"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.applicableposition", 1, 100));

        RuleFor(x => x.MainInstructor)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.maininstructor"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.maininstructor", 1, 50));

        RuleFor(x => x.TrainingMethod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.trainingmethod"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.trainingmethod", 1, 50));

        RuleFor(x => x.AssessmentMethod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.assessmentmethod"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.assessmentmethod", 1, 50));

        RuleFor(x => x.IsCertification)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.trainingcourse.iscertification"));

        RuleFor(x => x.CourseOutline)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.courseoutline"))
            .Length(1, 2000).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.courseoutline", 1, 2000));

        RuleFor(x => x.MaterialList)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.trainingcourse.materiallist"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.trainingcourse.materiallist", 1, 1000));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.trainingcourse.status"));
    }
}

/// <summary>
/// TrainingCourse更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktTrainingCourseCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>TrainingCourseId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktTrainingCourseUpdateDtoValidator : AbstractValidator<TaktTrainingCourseUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTrainingCourseUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktTrainingCourseCreateDtoValidator(validationMessages));

        RuleFor(x => x.TrainingCourseId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.trainingcourse.trainingcourseid"));

        RuleFor(x => x.CourseCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.coursecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseCode));

        RuleFor(x => x.CourseName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.coursename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseName));

        RuleFor(x => x.CourseType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.coursetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseType));

        RuleFor(x => x.CourseLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.courselevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseLevel));

        RuleFor(x => x.CourseDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.coursedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseDescription));

        RuleFor(x => x.CourseObjectives)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.courseobjectives", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseObjectives));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.MainInstructor)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.maininstructor", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MainInstructor));

        RuleFor(x => x.TrainingMethod)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.trainingmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TrainingMethod));

        RuleFor(x => x.AssessmentMethod)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.assessmentmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssessmentMethod));

        RuleFor(x => x.CourseOutline)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.courseoutline", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CourseOutline));

        RuleFor(x => x.MaterialList)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.trainingcourse.materiallist", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialList));
    }
}
