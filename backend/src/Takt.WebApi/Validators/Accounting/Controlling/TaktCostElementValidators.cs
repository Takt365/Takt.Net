// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Controlling
// 文件名称：TaktCostElementValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CostElement DTO 验证器（根据实体 TaktCostElement 自动生成）
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
/// CostElement创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktCostElement"/> 字段对齐）。
/// </summary>
public class TaktCostElementCreateDtoValidator : AbstractValidator<TaktCostElementCreateDto>
{
    public TaktCostElementCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelement.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.CostElementCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.costelement.costelementcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.costelement.costelementcode", 1, 50));

        RuleFor(x => x.CostElementName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.costelement.costelementname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.costelement.costelementname", 1, 100));

        RuleFor(x => x.CostElementType)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.costelement.costelementtype"));

        RuleFor(x => x.CostElementCategory)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.costelement.costelementcategory"));

        RuleFor(x => x.CostElementStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.costelement.costelementstatus"));
    }
}

/// <summary>
/// CostElement更新 DTO 验证器。
/// </summary>
public class TaktCostElementUpdateDtoValidator : AbstractValidator<TaktCostElementUpdateDto>
{
    public TaktCostElementUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktCostElementCreateDtoValidator(localizer));

        RuleFor(x => x.CostElementId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.costelement.costelementid"));

        RuleFor(x => x.CompanyCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelement.companycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyCode));

        RuleFor(x => x.CostElementCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelement.costelementcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CostElementCode));

        RuleFor(x => x.CostElementName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.costelement.costelementname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostElementName));
    }
}
