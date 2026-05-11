// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeContractValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeContract DTO 验证器（根据实体 TaktEmployeeContract 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;

namespace Takt.Application.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeContract创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeContract"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEmployeeContractCreateDtoValidator : AbstractValidator<TaktEmployeeContractCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeContractCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ContractNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeecontract.contractno"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.employeecontract.contractno", 1, 100));

        RuleFor(x => x.ContractType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeecontract.contracttype"));

        RuleFor(x => x.ContractStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeecontract.contractstatus"));

        RuleFor(x => x.SignCompany)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeecontract.signcompany", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SignCompany));
    }
}

/// <summary>
/// EmployeeContract更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEmployeeContractCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EmployeeContractId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEmployeeContractUpdateDtoValidator : AbstractValidator<TaktEmployeeContractUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeContractUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEmployeeContractCreateDtoValidator(validationMessages));

        RuleFor(x => x.EmployeeContractId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.employeecontract.employeecontractid"));

        RuleFor(x => x.ContractNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeecontract.contractno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ContractNo));

        RuleFor(x => x.SignCompany)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeecontract.signcompany", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SignCompany));
    }
}
