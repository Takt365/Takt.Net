// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Controlling
// 文件名称：TaktCostCenterValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CostCenter DTO 验证器（根据实体 TaktCostCenter 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Accounting.Controlling;

/// <summary>
/// CostCenter创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktCostCenter"/> 字段对齐）。
/// </summary>
public class TaktCostCenterCreateDtoValidator : AbstractValidator<TaktCostCenterCreateDto>
{
    public TaktCostCenterCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.CostCenterCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.costcenter.costcentercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.costcenter.costcentercode", 1, 50));

        RuleFor(x => x.CostCenterName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.costcenter.costcentername"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.costcenter.costcentername", 1, 100));

        RuleFor(x => x.CostCenterType)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.costcenter.costcentertype"));

        RuleFor(x => x.ManagerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.managername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ManagerName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.CostCenterStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.costcenter.costcenterstatus"));
    }
}

/// <summary>
/// CostCenter更新 DTO 验证器。
/// </summary>
public class TaktCostCenterUpdateDtoValidator : AbstractValidator<TaktCostCenterUpdateDto>
{
    public TaktCostCenterUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktCostCenterCreateDtoValidator(localizer));

        RuleFor(x => x.CostCenterId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.costcenter.costcenterid"));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.CostCenterCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.costcentercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterCode));

        RuleFor(x => x.CostCenterName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.costcentername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterName));

        RuleFor(x => x.ManagerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.managername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ManagerName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costcenter.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
