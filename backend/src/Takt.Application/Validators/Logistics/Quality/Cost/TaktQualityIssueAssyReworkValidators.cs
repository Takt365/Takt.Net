// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueAssyReworkValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityIssueAssyRework DTO 验证器（根据实体 TaktQualityIssueAssyRework 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Cost;

namespace Takt.Application.Validators.Logistics.Quality.Cost;

/// <summary>
/// QualityIssueAssyRework创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityIssueAssyRework"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktQualityIssueAssyReworkCreateDtoValidator : AbstractValidator<TaktQualityIssueAssyReworkCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQualityIssueAssyReworkCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.QualityIssueCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityissueassyrework.qualityissuecode"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.qualityissueassyrework.qualityissuecode", 1, 30));

        RuleFor(x => x.AssyCustomerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.qualityissueassyrework.assycustomername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyCustomerName));

        RuleFor(x => x.AssyDebitNoteNo)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityissueassyrework.assydebitnoteno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyDebitNoteNo));

        RuleFor(x => x.AssyRecorder)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityissueassyrework.assyrecorder", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyRecorder));
    }
}

/// <summary>
/// QualityIssueAssyRework更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktQualityIssueAssyReworkCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>QualityIssueAssyReworkId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktQualityIssueAssyReworkUpdateDtoValidator : AbstractValidator<TaktQualityIssueAssyReworkUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQualityIssueAssyReworkUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktQualityIssueAssyReworkCreateDtoValidator(validationMessages));

        RuleFor(x => x.QualityIssueAssyReworkId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.qualityissueassyrework.qualityissueassyreworkid"));

        RuleFor(x => x.QualityIssueCode)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityissueassyrework.qualityissuecode", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.QualityIssueCode));

        RuleFor(x => x.AssyCustomerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.qualityissueassyrework.assycustomername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyCustomerName));

        RuleFor(x => x.AssyDebitNoteNo)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityissueassyrework.assydebitnoteno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyDebitNoteNo));

        RuleFor(x => x.AssyRecorder)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityissueassyrework.assyrecorder", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyRecorder));
    }
}
