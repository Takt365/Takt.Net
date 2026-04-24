// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeContractValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeContract DTO 验证器（根据实体 TaktEmployeeContract 自动生成）
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
/// EmployeeContract创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeContract"/> 字段对齐）。
/// </summary>
public class TaktEmployeeContractCreateDtoValidator : AbstractValidator<TaktEmployeeContractCreateDto>
{
    public TaktEmployeeContractCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ContractNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeecontract.contractno"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeecontract.contractno", 1, 100));

        RuleFor(x => x.ContractType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeecontract.contracttype"));

        RuleFor(x => x.ContractStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeecontract.contractstatus"));

        RuleFor(x => x.SignCompany)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecontract.signcompany", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SignCompany));
    }
}

/// <summary>
/// EmployeeContract更新 DTO 验证器。
/// </summary>
public class TaktEmployeeContractUpdateDtoValidator : AbstractValidator<TaktEmployeeContractUpdateDto>
{
    public TaktEmployeeContractUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeContractCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeContractId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeecontract.employeecontractid"));

        RuleFor(x => x.ContractNo)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecontract.contractno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ContractNo));

        RuleFor(x => x.SignCompany)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeecontract.signcompany", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SignCompany));
    }
}
