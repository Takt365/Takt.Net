// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryStructureValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalaryStructure DTO 验证器（根据实体 TaktSalaryStructure 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.CompensationBenefits;

/// <summary>
/// SalaryStructure创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktSalaryStructure"/> 字段对齐）。
/// </summary>
public class TaktSalaryStructureCreateDtoValidator : AbstractValidator<TaktSalaryStructureCreateDto>
{
    public TaktSalaryStructureCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.StructureCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarystructure.structurecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarystructure.structurecode", 1, 50));

        RuleFor(x => x.StructureName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarystructure.structurename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarystructure.structurename", 1, 100));

        RuleFor(x => x.SalaryLevel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarystructure.salarylevel"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarystructure.salarylevel", 1, 50));

        RuleFor(x => x.SalaryGrade)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarystructure.salarygrade"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarystructure.salarygrade", 1, 50));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarystructure.applicabledepartment"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarystructure.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarystructure.applicableposition"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarystructure.applicableposition", 1, 100));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.salarystructure.description"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.salarystructure.description", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.salarystructure.status"));
    }
}

/// <summary>
/// SalaryStructure更新 DTO 验证器。
/// </summary>
public class TaktSalaryStructureUpdateDtoValidator : AbstractValidator<TaktSalaryStructureUpdateDto>
{
    public TaktSalaryStructureUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSalaryStructureCreateDtoValidator(localizer));

        RuleFor(x => x.SalaryStructureId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.salarystructure.salarystructureid"));

        RuleFor(x => x.StructureCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarystructure.structurecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StructureCode));

        RuleFor(x => x.StructureName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarystructure.structurename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.StructureName));

        RuleFor(x => x.SalaryLevel)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarystructure.salarylevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SalaryLevel));

        RuleFor(x => x.SalaryGrade)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarystructure.salarygrade", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SalaryGrade));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarystructure.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarystructure.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.salarystructure.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
