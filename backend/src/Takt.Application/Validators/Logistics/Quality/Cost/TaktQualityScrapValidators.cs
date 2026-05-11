// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityScrapValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityScrap DTO 验证器（根据实体 TaktQualityScrap 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Cost;

namespace Takt.Application.Validators.Logistics.Quality.Cost;

/// <summary>
/// QualityScrap创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityScrap"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktQualityScrapCreateDtoValidator : AbstractValidator<TaktQualityScrapCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQualityScrapCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityscrap.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.qualityscrap.plantcode"));

        RuleFor(x => x.QualityScrapCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityscrap.qualityscrapcode"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.qualityscrap.qualityscrapcode", 1, 30));

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityscrap.model"))
            .Length(1, 255).WithMessage(_validationMessages.LengthBetween("entity.qualityscrap.model", 1, 255));

        RuleFor(x => x.CostCurrency)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityscrap.costcurrency"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.qualityscrap.costcurrency", 1, 10));
    }
}

/// <summary>
/// QualityScrap更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktQualityScrapCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>QualityScrapId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktQualityScrapUpdateDtoValidator : AbstractValidator<TaktQualityScrapUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQualityScrapUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktQualityScrapCreateDtoValidator(validationMessages));

        RuleFor(x => x.QualityScrapId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.qualityscrap.qualityscrapid"));

        RuleFor(x => x.QualityScrapCode)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityscrap.qualityscrapcode", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.QualityScrapCode));

        RuleFor(x => x.Model)
            .MaximumLength(255).WithMessage(_validationMessages.LengthMax("entity.qualityscrap.model", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.Model));

        RuleFor(x => x.CostCurrency)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.qualityscrap.costcurrency", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCurrency));
    }
}
