// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryStructureValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalaryStructure DTO 验证器（根据实体 TaktSalaryStructure 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;

namespace Takt.Application.Validators.HumanResource.CompensationBenefits;

/// <summary>
/// SalaryStructure创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktSalaryStructure"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSalaryStructureCreateDtoValidator : AbstractValidator<TaktSalaryStructureCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSalaryStructureCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.StructureCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarystructure.structurecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salarystructure.structurecode", 1, 50));

        RuleFor(x => x.StructureName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarystructure.structurename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.salarystructure.structurename", 1, 100));

        RuleFor(x => x.SalaryLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarystructure.salarylevel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salarystructure.salarylevel", 1, 50));

        RuleFor(x => x.SalaryGrade)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarystructure.salarygrade"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salarystructure.salarygrade", 1, 50));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarystructure.applicabledepartment"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.salarystructure.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicablePosition)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarystructure.applicableposition"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.salarystructure.applicableposition", 1, 100));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarystructure.description"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.salarystructure.description", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.salarystructure.status"));
    }
}

/// <summary>
/// SalaryStructure更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSalaryStructureCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SalaryStructureId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSalaryStructureUpdateDtoValidator : AbstractValidator<TaktSalaryStructureUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSalaryStructureUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSalaryStructureCreateDtoValidator(validationMessages));

        RuleFor(x => x.SalaryStructureId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.salarystructure.salarystructureid"));

        RuleFor(x => x.StructureCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salarystructure.structurecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StructureCode));

        RuleFor(x => x.StructureName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.salarystructure.structurename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.StructureName));

        RuleFor(x => x.SalaryLevel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salarystructure.salarylevel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SalaryLevel));

        RuleFor(x => x.SalaryGrade)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salarystructure.salarygrade", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SalaryGrade));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.salarystructure.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicablePosition)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.salarystructure.applicableposition", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePosition));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.salarystructure.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
