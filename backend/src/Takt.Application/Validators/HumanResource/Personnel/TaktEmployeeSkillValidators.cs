// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeSkillValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeSkill DTO 验证器（根据实体 TaktEmployeeSkill 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;

namespace Takt.Application.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeSkill创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeSkill"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEmployeeSkillCreateDtoValidator : AbstractValidator<TaktEmployeeSkillCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeSkillCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.SkillName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeeskill.skillname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.employeeskill.skillname", 1, 100));

        RuleFor(x => x.SkillLevel)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeeskill.skilllevel"));

        RuleFor(x => x.CertificateName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeeskill.certificatename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateName));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeeskill.certificateno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));
    }
}

/// <summary>
/// EmployeeSkill更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEmployeeSkillCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EmployeeSkillId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEmployeeSkillUpdateDtoValidator : AbstractValidator<TaktEmployeeSkillUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeSkillUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEmployeeSkillCreateDtoValidator(validationMessages));

        RuleFor(x => x.EmployeeSkillId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.employeeskill.employeeskillid"));

        RuleFor(x => x.SkillName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeeskill.skillname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SkillName));

        RuleFor(x => x.CertificateName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeeskill.certificatename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateName));

        RuleFor(x => x.CertificateNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeeskill.certificateno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CertificateNo));
    }
}
