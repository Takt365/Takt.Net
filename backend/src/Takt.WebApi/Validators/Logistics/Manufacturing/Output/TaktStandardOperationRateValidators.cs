// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktStandardOperationRateValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：StandardOperationRate DTO 验证器（根据实体 TaktStandardOperationRate 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// StandardOperationRate创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktStandardOperationRate"/> 字段对齐）。
/// </summary>
public class TaktStandardOperationRateCreateDtoValidator : AbstractValidator<TaktStandardOperationRateCreateDto>
{
    public TaktStandardOperationRateCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.standardoperationrate.plantcode"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.standardoperationrate.plantcode", 1, 8));

        RuleFor(x => x.FinancialYear)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.standardoperationrate.financialyear"))
            .Length(1, 4).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.standardoperationrate.financialyear", 1, 4));

        RuleFor(x => x.OperationType)
            .InclusiveBetween(1, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.standardoperationrate.operationtype"));
    }
}

/// <summary>
/// StandardOperationRate更新 DTO 验证器。
/// </summary>
public class TaktStandardOperationRateUpdateDtoValidator : AbstractValidator<TaktStandardOperationRateUpdateDto>
{
    public TaktStandardOperationRateUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktStandardOperationRateCreateDtoValidator(localizer));

        RuleFor(x => x.StandardOperationRateId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.standardoperationrate.standardoperationrateid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationrate.plantcode", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.FinancialYear)
            .MaximumLength(4).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationrate.financialyear", 4))
            .When(x => !string.IsNullOrWhiteSpace(x.FinancialYear));
    }
}
