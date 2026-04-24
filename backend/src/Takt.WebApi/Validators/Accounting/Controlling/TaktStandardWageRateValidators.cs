// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Controlling
// 文件名称：TaktStandardWageRateValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：StandardWageRate DTO 验证器（根据实体 TaktStandardWageRate 自动生成）
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
/// StandardWageRate创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktStandardWageRate"/> 字段对齐）。
/// </summary>
public class TaktStandardWageRateCreateDtoValidator : AbstractValidator<TaktStandardWageRateCreateDto>
{
    public TaktStandardWageRateCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardwagerate.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.YearMonth)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.standardwagerate.yearmonth"))
            .Length(1, 6).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.standardwagerate.yearmonth", 1, 6));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardwagerate.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}

/// <summary>
/// StandardWageRate更新 DTO 验证器。
/// </summary>
public class TaktStandardWageRateUpdateDtoValidator : AbstractValidator<TaktStandardWageRateUpdateDto>
{
    public TaktStandardWageRateUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktStandardWageRateCreateDtoValidator(localizer));

        RuleFor(x => x.StandardWageRateId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.standardwagerate.standardwagerateid"));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardwagerate.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.YearMonth)
            .MaximumLength(6).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardwagerate.yearmonth", 6))
            .When(x => !string.IsNullOrWhiteSpace(x.YearMonth));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardwagerate.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
