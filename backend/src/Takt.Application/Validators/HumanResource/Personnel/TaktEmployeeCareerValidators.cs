// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeCareerValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeCareer DTO 验证器（根据实体 TaktEmployeeCareer 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;

namespace Takt.Application.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeCareer创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeCareer"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEmployeeCareerCreateDtoValidator : AbstractValidator<TaktEmployeeCareerCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeCareerCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.DeptName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeecareer.deptname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.employeecareer.deptname", 1, 100));

        RuleFor(x => x.PostName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeecareer.postname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PostName));

        RuleFor(x => x.JobLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeecareer.joblevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JobLevel));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeecareer.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.WorkLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeecareer.worklocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkLocation));

        RuleFor(x => x.WorkNature)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeecareer.worknature"));

        RuleFor(x => x.EmploymentType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeecareer.employmenttype"));

        RuleFor(x => x.IsPrimary)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeecareer.isprimary"));

        RuleFor(x => x.DirectManagerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeecareer.directmanagername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DirectManagerName));
    }
}

/// <summary>
/// EmployeeCareer更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEmployeeCareerCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EmployeeCareerId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEmployeeCareerUpdateDtoValidator : AbstractValidator<TaktEmployeeCareerUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeCareerUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEmployeeCareerCreateDtoValidator(validationMessages));

        RuleFor(x => x.EmployeeCareerId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.employeecareer.employeecareerid"));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeecareer.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PostName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeecareer.postname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PostName));

        RuleFor(x => x.JobLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeecareer.joblevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JobLevel));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeecareer.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.WorkLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeecareer.worklocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkLocation));

        RuleFor(x => x.DirectManagerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeecareer.directmanagername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DirectManagerName));
    }
}
