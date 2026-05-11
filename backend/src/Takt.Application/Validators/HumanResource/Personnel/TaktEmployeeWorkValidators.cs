// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeWorkValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeWork DTO 验证器（根据实体 TaktEmployeeWork 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;

namespace Takt.Application.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeWork创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeWork"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEmployeeWorkCreateDtoValidator : AbstractValidator<TaktEmployeeWorkCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeWorkCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeework.companyname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.employeework.companyname", 1, 200));

        RuleFor(x => x.PositionName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeework.positionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PositionName));

        RuleFor(x => x.JobContent)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employeework.jobcontent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.JobContent));

        RuleFor(x => x.WitnessName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeework.witnessname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WitnessName));

        RuleFor(x => x.WitnessPhone)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.employeework.witnessphone", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.WitnessPhone));
    }
}

/// <summary>
/// EmployeeWork更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEmployeeWorkCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EmployeeWorkId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEmployeeWorkUpdateDtoValidator : AbstractValidator<TaktEmployeeWorkUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeWorkUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEmployeeWorkCreateDtoValidator(validationMessages));

        RuleFor(x => x.EmployeeWorkId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.employeework.employeeworkid"));

        RuleFor(x => x.CompanyName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeework.companyname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyName));

        RuleFor(x => x.PositionName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeework.positionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PositionName));

        RuleFor(x => x.JobContent)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employeework.jobcontent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.JobContent));

        RuleFor(x => x.WitnessName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeework.witnessname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WitnessName));

        RuleFor(x => x.WitnessPhone)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.employeework.witnessphone", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.WitnessPhone));
    }
}
