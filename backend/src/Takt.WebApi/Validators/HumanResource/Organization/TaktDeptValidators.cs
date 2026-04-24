// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Organization
// 文件名称：TaktDeptValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Dept DTO 验证器（根据实体 TaktDept 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Organization;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Organization;

/// <summary>
/// Dept创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Organization.TaktDept"/> 字段对齐）。
/// </summary>
public class TaktDeptCreateDtoValidator : AbstractValidator<TaktDeptCreateDto>
{
    public TaktDeptCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DeptName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.deptname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.dept.deptname", 1, 100));

        RuleFor(x => x.DeptCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.deptcode"))
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.deptcode", 50));

        RuleFor(x => x.CostCenterCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.costcentercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterCode));

        RuleFor(x => x.DeptType)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dept.depttype"));

        RuleFor(x => x.DeptPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.deptphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptPhone));

        RuleFor(x => x.DeptMail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.deptmail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptMail));

        RuleFor(x => x.DeptAddr)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.deptaddr", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptAddr));

        RuleFor(x => x.DataScope)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dept.datascope"));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));

        RuleFor(x => x.DeptStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dept.deptstatus"));
    }
}

/// <summary>
/// Dept更新 DTO 验证器。
/// </summary>
public class TaktDeptUpdateDtoValidator : AbstractValidator<TaktDeptUpdateDto>
{
    public TaktDeptUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktDeptCreateDtoValidator(localizer));

        RuleFor(x => x.DeptId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.dept.deptid"));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.CostCenterCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.costcentercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterCode));

        RuleFor(x => x.DeptPhone)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.deptphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptPhone));

        RuleFor(x => x.DeptMail)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.deptmail", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptMail));

        RuleFor(x => x.DeptAddr)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.deptaddr", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptAddr));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dept.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));
    }
}
