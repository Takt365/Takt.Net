// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueAssyReworkValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityIssueAssyRework DTO 验证器（根据实体 TaktQualityIssueAssyRework 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Quality.Cost;

/// <summary>
/// QualityIssueAssyRework创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityIssueAssyRework"/> 字段对齐）。
/// </summary>
public class TaktQualityIssueAssyReworkCreateDtoValidator : AbstractValidator<TaktQualityIssueAssyReworkCreateDto>
{
    public TaktQualityIssueAssyReworkCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.AssyCustomerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissueassyrework.assycustomername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyCustomerName));

        RuleFor(x => x.AssyDebitNoteNo)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissueassyrework.assydebitnoteno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyDebitNoteNo));

        RuleFor(x => x.AssyRecorder)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissueassyrework.assyrecorder", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyRecorder));
    }
}

/// <summary>
/// QualityIssueAssyRework更新 DTO 验证器。
/// </summary>
public class TaktQualityIssueAssyReworkUpdateDtoValidator : AbstractValidator<TaktQualityIssueAssyReworkUpdateDto>
{
    public TaktQualityIssueAssyReworkUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktQualityIssueAssyReworkCreateDtoValidator(localizer));

        RuleFor(x => x.QualityIssueAssyReworkId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityissueassyrework.qualityissueassyreworkid"));

        RuleFor(x => x.AssyCustomerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissueassyrework.assycustomername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyCustomerName));

        RuleFor(x => x.AssyDebitNoteNo)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissueassyrework.assydebitnoteno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyDebitNoteNo));

        RuleFor(x => x.AssyRecorder)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissueassyrework.assyrecorder", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AssyRecorder));
    }
}
