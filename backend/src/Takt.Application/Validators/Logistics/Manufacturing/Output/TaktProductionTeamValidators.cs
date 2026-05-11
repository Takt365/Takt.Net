// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktProductionTeamValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProductionTeam DTO 验证器（根据实体 TaktProductionTeam 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;

namespace Takt.Application.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// ProductionTeam创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktProductionTeam"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktProductionTeamCreateDtoValidator : AbstractValidator<TaktProductionTeamCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductionTeamCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productionteam.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.productionteam.plantcode"));

        RuleFor(x => x.TeamCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productionteam.teamcode"))
            .Length(1, 32).WithMessage(_validationMessages.LengthBetween("entity.productionteam.teamcode", 1, 32));

        RuleFor(x => x.TeamName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productionteam.teamname"))
            .Length(1, 64).WithMessage(_validationMessages.LengthBetween("entity.productionteam.teamname", 1, 64));

        RuleFor(x => x.TeamCategory)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.productionteam.teamcategory", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamCategory));

        RuleFor(x => x.TeamCategoryName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productionteam.teamcategoryname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamCategoryName));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionteam.productionline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));

        RuleFor(x => x.TeamLeaderName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productionteam.teamleadername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeaderName));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.productionteam.status"));
    }
}

/// <summary>
/// ProductionTeam更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktProductionTeamCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ProductionTeamId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktProductionTeamUpdateDtoValidator : AbstractValidator<TaktProductionTeamUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductionTeamUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktProductionTeamCreateDtoValidator(validationMessages));

        RuleFor(x => x.ProductionTeamId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.productionteam.productionteamid"));

        RuleFor(x => x.TeamCode)
            .MaximumLength(32).WithMessage(_validationMessages.LengthMax("entity.productionteam.teamcode", 32))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamCode));

        RuleFor(x => x.TeamName)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.productionteam.teamname", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamName));

        RuleFor(x => x.TeamCategory)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.productionteam.teamcategory", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamCategory));

        RuleFor(x => x.TeamCategoryName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productionteam.teamcategoryname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamCategoryName));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionteam.productionline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));

        RuleFor(x => x.TeamLeaderName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.productionteam.teamleadername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeaderName));
    }
}
