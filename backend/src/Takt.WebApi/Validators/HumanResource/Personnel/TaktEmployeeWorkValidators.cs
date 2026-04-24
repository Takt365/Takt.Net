// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeWorkValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeWork DTO 验证器（根据实体 TaktEmployeeWork 自动生成）
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
/// EmployeeWork创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeWork"/> 字段对齐）。
/// </summary>
public class TaktEmployeeWorkCreateDtoValidator : AbstractValidator<TaktEmployeeWorkCreateDto>
{
    public TaktEmployeeWorkCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeework.companyname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeework.companyname", 1, 200));

        RuleFor(x => x.PositionName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeework.positionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PositionName));

        RuleFor(x => x.JobContent)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeework.jobcontent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.JobContent));

        RuleFor(x => x.WitnessName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeework.witnessname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WitnessName));

        RuleFor(x => x.WitnessPhone)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeework.witnessphone", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.WitnessPhone));
    }
}

/// <summary>
/// EmployeeWork更新 DTO 验证器。
/// </summary>
public class TaktEmployeeWorkUpdateDtoValidator : AbstractValidator<TaktEmployeeWorkUpdateDto>
{
    public TaktEmployeeWorkUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeWorkCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeWorkId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeework.employeeworkid"));

        RuleFor(x => x.CompanyName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeework.companyname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyName));

        RuleFor(x => x.PositionName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeework.positionname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PositionName));

        RuleFor(x => x.JobContent)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeework.jobcontent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.JobContent));

        RuleFor(x => x.WitnessName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeework.witnessname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.WitnessName));

        RuleFor(x => x.WitnessPhone)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeework.witnessphone", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.WitnessPhone));
    }
}
