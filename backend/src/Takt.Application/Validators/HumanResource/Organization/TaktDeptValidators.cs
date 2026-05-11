// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Organization
// 文件名称：TaktDeptValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Dept DTO 验证器（根据实体 TaktDept 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Organization;

namespace Takt.Application.Validators.HumanResource.Organization;

/// <summary>
/// Dept创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Organization.TaktDept"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktDeptCreateDtoValidator : AbstractValidator<TaktDeptCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktDeptCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dept.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.dept.companycode"));

        RuleFor(x => x.DeptName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dept.deptname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.dept.deptname", 1, 100));

        RuleFor(x => x.DeptCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dept.deptcode"))
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.dept.deptcode", 50));

        RuleFor(x => x.CostCenterCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dept.costcentercode"))
            .Must(TaktRegexHelper.IsValidCostCenterCode).WithMessage(_validationMessages.FormatInvalid("entity.dept.costcentercode"));

        RuleFor(x => x.DeptType)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.dept.depttype"));

        RuleFor(x => x.DeptPhone)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.dept.deptphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptPhone));

        RuleFor(x => x.DeptMail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.dept.deptmail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptMail));

        RuleFor(x => x.DeptAddr)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.dept.deptaddr", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptAddr));

        RuleFor(x => x.DataScope)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.dept.datascope"));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.dept.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));

        RuleFor(x => x.DeptStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.dept.deptstatus"));
    }
}

/// <summary>
/// Dept更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktDeptCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>DeptId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktDeptUpdateDtoValidator : AbstractValidator<TaktDeptUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktDeptUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktDeptCreateDtoValidator(validationMessages));

        RuleFor(x => x.DeptId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.dept.deptid"));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.dept.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.DeptPhone)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.dept.deptphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptPhone));

        RuleFor(x => x.DeptMail)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.dept.deptmail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptMail));

        RuleFor(x => x.DeptAddr)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.dept.deptaddr", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptAddr));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.dept.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));
    }
}
