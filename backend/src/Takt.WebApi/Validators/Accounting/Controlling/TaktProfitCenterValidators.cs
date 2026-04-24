// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Controlling
// 文件名称：TaktProfitCenterValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProfitCenter DTO 验证器（根据实体 TaktProfitCenter 自动生成）
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
/// ProfitCenter创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktProfitCenter"/> 字段对齐）。
/// </summary>
public class TaktProfitCenterCreateDtoValidator : AbstractValidator<TaktProfitCenterCreateDto>
{
    public TaktProfitCenterCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.ProfitCenterCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.profitcenter.profitcentercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.profitcenter.profitcentercode", 1, 50));

        RuleFor(x => x.ProfitCenterName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.profitcenter.profitcentername"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.profitcenter.profitcentername", 1, 100));

        RuleFor(x => x.ManagerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.managername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ManagerName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.ProfitCenterStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.profitcenter.profitcenterstatus"));
    }
}

/// <summary>
/// ProfitCenter更新 DTO 验证器。
/// </summary>
public class TaktProfitCenterUpdateDtoValidator : AbstractValidator<TaktProfitCenterUpdateDto>
{
    public TaktProfitCenterUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktProfitCenterCreateDtoValidator(localizer));

        RuleFor(x => x.ProfitCenterId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.profitcenter.profitcenterid"));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.ProfitCenterCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.profitcentercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProfitCenterCode));

        RuleFor(x => x.ProfitCenterName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.profitcentername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProfitCenterName));

        RuleFor(x => x.ManagerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.managername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ManagerName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.profitcenter.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
