// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityIssue DTO 验证器（根据实体 TaktQualityIssue 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Cost;

namespace Takt.Application.Validators.Logistics.Quality.Cost;

/// <summary>
/// QualityIssue创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityIssue"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktQualityIssueCreateDtoValidator : AbstractValidator<TaktQualityIssueCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQualityIssueCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityissue.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.qualityissue.plantcode"));

        RuleFor(x => x.QualityIssueCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityissue.qualityissuecode"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.qualityissue.qualityissuecode", 1, 30));

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityissue.model"))
            .Length(1, 255).WithMessage(_validationMessages.LengthBetween("entity.qualityissue.model", 1, 255));

        RuleFor(x => x.Lot)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityissue.lot"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.qualityissue.lot", 1, 30));

        RuleFor(x => x.QualityProblemsResponse)
            .MaximumLength(255).WithMessage(_validationMessages.LengthMax("entity.qualityissue.qualityproblemsresponse", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.QualityProblemsResponse));

        RuleFor(x => x.ReworkDueToDefects)
            .MaximumLength(255).WithMessage(_validationMessages.LengthMax("entity.qualityissue.reworkduetodefects", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.ReworkDueToDefects));

        RuleFor(x => x.NeedRework)
            .MaximumLength(1).WithMessage(_validationMessages.LengthMax("entity.qualityissue.needrework", 1))
            .When(x => !string.IsNullOrWhiteSpace(x.NeedRework));

        RuleFor(x => x.CostCurrency)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityissue.costcurrency"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.qualityissue.costcurrency", 1, 10));
    }
}

/// <summary>
/// QualityIssue更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktQualityIssueCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>QualityIssueId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktQualityIssueUpdateDtoValidator : AbstractValidator<TaktQualityIssueUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQualityIssueUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktQualityIssueCreateDtoValidator(validationMessages));

        RuleFor(x => x.QualityIssueId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.qualityissue.qualityissueid"));

        RuleFor(x => x.QualityIssueCode)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityissue.qualityissuecode", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.QualityIssueCode));

        RuleFor(x => x.Model)
            .MaximumLength(255).WithMessage(_validationMessages.LengthMax("entity.qualityissue.model", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.Model));

        RuleFor(x => x.Lot)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityissue.lot", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.Lot));

        RuleFor(x => x.QualityProblemsResponse)
            .MaximumLength(255).WithMessage(_validationMessages.LengthMax("entity.qualityissue.qualityproblemsresponse", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.QualityProblemsResponse));

        RuleFor(x => x.ReworkDueToDefects)
            .MaximumLength(255).WithMessage(_validationMessages.LengthMax("entity.qualityissue.reworkduetodefects", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.ReworkDueToDefects));

        RuleFor(x => x.NeedRework)
            .MaximumLength(1).WithMessage(_validationMessages.LengthMax("entity.qualityissue.needrework", 1))
            .When(x => !string.IsNullOrWhiteSpace(x.NeedRework));

        RuleFor(x => x.CostCurrency)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.qualityissue.costcurrency", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCurrency));
    }
}
