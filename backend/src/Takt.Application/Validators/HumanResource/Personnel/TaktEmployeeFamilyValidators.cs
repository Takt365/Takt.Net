// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeFamilyValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeFamily DTO 验证器（根据实体 TaktEmployeeFamily 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;

namespace Takt.Application.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeFamily创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeFamily"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEmployeeFamilyCreateDtoValidator : AbstractValidator<TaktEmployeeFamilyCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeFamilyCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.MemberName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeefamily.membername"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.employeefamily.membername", 1, 50));

        RuleFor(x => x.RelationType)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeefamily.relationtype"));

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.employeefamily.phonenumber", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.WorkUnit)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeefamily.workunit", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkUnit));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeefamily.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.IsEmergencyContact)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeefamily.isemergencycontact"));
    }
}

/// <summary>
/// EmployeeFamily更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEmployeeFamilyCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EmployeeFamilyId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEmployeeFamilyUpdateDtoValidator : AbstractValidator<TaktEmployeeFamilyUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeFamilyUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEmployeeFamilyCreateDtoValidator(validationMessages));

        RuleFor(x => x.EmployeeFamilyId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.employeefamily.employeefamilyid"));

        RuleFor(x => x.MemberName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeefamily.membername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MemberName));

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.employeefamily.phonenumber", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.WorkUnit)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeefamily.workunit", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkUnit));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeefamily.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));
    }
}
