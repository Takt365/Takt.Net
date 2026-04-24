// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktProductionTeamValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProductionTeam DTO 验证器（根据实体 TaktProductionTeam 自动生成）
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
/// ProductionTeam创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktProductionTeam"/> 字段对齐）。
/// </summary>
public class TaktProductionTeamCreateDtoValidator : AbstractValidator<TaktProductionTeamCreateDto>
{
    public TaktProductionTeamCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.productionteam.plantcode"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.productionteam.plantcode", 1, 8));

        RuleFor(x => x.TeamCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.productionteam.teamcode"))
            .Length(1, 32).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.productionteam.teamcode", 1, 32));

        RuleFor(x => x.TeamName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.productionteam.teamname"))
            .Length(1, 64).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.productionteam.teamname", 1, 64));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionteam.productionline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));

        RuleFor(x => x.ProductionLineName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionteam.productionlinename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineName));

        RuleFor(x => x.TeamLeaderName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionteam.teamleadername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeaderName));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.productionteam.status"));
    }
}

/// <summary>
/// ProductionTeam更新 DTO 验证器。
/// </summary>
public class TaktProductionTeamUpdateDtoValidator : AbstractValidator<TaktProductionTeamUpdateDto>
{
    public TaktProductionTeamUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktProductionTeamCreateDtoValidator(localizer));

        RuleFor(x => x.ProductionTeamId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.productionteam.productionteamid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionteam.plantcode", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.TeamCode)
            .MaximumLength(32).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionteam.teamcode", 32))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamCode));

        RuleFor(x => x.TeamName)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionteam.teamname", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamName));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionteam.productionline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));

        RuleFor(x => x.ProductionLineName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionteam.productionlinename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLineName));

        RuleFor(x => x.TeamLeaderName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.productionteam.teamleadername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeaderName));
    }
}
