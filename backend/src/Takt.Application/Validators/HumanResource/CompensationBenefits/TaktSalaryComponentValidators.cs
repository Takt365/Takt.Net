// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryComponentValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SalaryComponent DTO 验证器（根据实体 TaktSalaryComponent 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.CompensationBenefits;

namespace Takt.Application.Validators.HumanResource.CompensationBenefits;

/// <summary>
/// SalaryComponent创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.CompensationBenefits.TaktSalaryComponent"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSalaryComponentCreateDtoValidator : AbstractValidator<TaktSalaryComponentCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSalaryComponentCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ComponentCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarycomponent.componentcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salarycomponent.componentcode", 1, 50));

        RuleFor(x => x.ComponentName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarycomponent.componentname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.salarycomponent.componentname", 1, 100));

        RuleFor(x => x.ComponentType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarycomponent.componenttype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salarycomponent.componenttype", 1, 50));

        RuleFor(x => x.CalculationMethod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarycomponent.calculationmethod"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.salarycomponent.calculationmethod", 1, 50));

        RuleFor(x => x.CalculationFormula)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarycomponent.calculationformula"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.salarycomponent.calculationformula", 1, 500));

        RuleFor(x => x.IsTaxable)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.salarycomponent.istaxable"));

        RuleFor(x => x.IsSocialSecurityBase)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.salarycomponent.issocialsecuritybase"));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.salarycomponent.description"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.salarycomponent.description", 1, 500));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.salarycomponent.status"));
    }
}

/// <summary>
/// SalaryComponent更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSalaryComponentCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SalaryComponentId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSalaryComponentUpdateDtoValidator : AbstractValidator<TaktSalaryComponentUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSalaryComponentUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSalaryComponentCreateDtoValidator(validationMessages));

        RuleFor(x => x.SalaryComponentId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.salarycomponent.salarycomponentid"));

        RuleFor(x => x.ComponentCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salarycomponent.componentcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ComponentCode));

        RuleFor(x => x.ComponentName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.salarycomponent.componentname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ComponentName));

        RuleFor(x => x.ComponentType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salarycomponent.componenttype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ComponentType));

        RuleFor(x => x.CalculationMethod)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.salarycomponent.calculationmethod", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CalculationMethod));

        RuleFor(x => x.CalculationFormula)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.salarycomponent.calculationformula", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.CalculationFormula));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.salarycomponent.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
