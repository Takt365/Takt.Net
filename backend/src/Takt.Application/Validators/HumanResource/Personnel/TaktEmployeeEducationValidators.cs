// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeEducationValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeEducation DTO 验证器（根据实体 TaktEmployeeEducation 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;

namespace Takt.Application.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeEducation创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeEducation"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEmployeeEducationCreateDtoValidator : AbstractValidator<TaktEmployeeEducationCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeEducationCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.EducationLevel)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeeeducation.educationlevel"));

        RuleFor(x => x.SchoolName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeeeducation.schoolname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.employeeeducation.schoolname", 1, 200));

        RuleFor(x => x.MajorName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeeeducation.majorname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MajorName));

        RuleFor(x => x.DegreeLevel)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeeeducation.degreelevel"));

        RuleFor(x => x.IsHighest)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeeeducation.ishighest"));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeeeducation.certificateno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));
    }
}

/// <summary>
/// EmployeeEducation更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEmployeeEducationCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EmployeeEducationId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEmployeeEducationUpdateDtoValidator : AbstractValidator<TaktEmployeeEducationUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeEducationUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEmployeeEducationCreateDtoValidator(validationMessages));

        RuleFor(x => x.EmployeeEducationId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.employeeeducation.employeeeducationid"));

        RuleFor(x => x.SchoolName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeeeducation.schoolname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchoolName));

        RuleFor(x => x.MajorName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeeeducation.majorname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.MajorName));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeeeducation.certificateno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));
    }
}
