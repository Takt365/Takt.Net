// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityIssuePcbaReworkValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityIssuePcbaRework DTO 验证器（根据实体 TaktQualityIssuePcbaRework 自动生成）
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
/// QualityIssuePcbaRework创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityIssuePcbaRework"/> 字段对齐）。
/// </summary>
public class TaktQualityIssuePcbaReworkCreateDtoValidator : AbstractValidator<TaktQualityIssuePcbaReworkCreateDto>
{
    public TaktQualityIssuePcbaReworkCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PcbaCustomerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissuepcbarework.pcbacustomername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaCustomerName));

        RuleFor(x => x.PcbaDebitNoteNo)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissuepcbarework.pcbadebitnoteno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaDebitNoteNo));

        RuleFor(x => x.PcbaRecorder)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissuepcbarework.pcbarecorder", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaRecorder));
    }
}

/// <summary>
/// QualityIssuePcbaRework更新 DTO 验证器。
/// </summary>
public class TaktQualityIssuePcbaReworkUpdateDtoValidator : AbstractValidator<TaktQualityIssuePcbaReworkUpdateDto>
{
    public TaktQualityIssuePcbaReworkUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktQualityIssuePcbaReworkCreateDtoValidator(localizer));

        RuleFor(x => x.QualityIssuePcbaReworkId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityissuepcbarework.qualityissuepcbareworkid"));

        RuleFor(x => x.PcbaCustomerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissuepcbarework.pcbacustomername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaCustomerName));

        RuleFor(x => x.PcbaDebitNoteNo)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissuepcbarework.pcbadebitnoteno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaDebitNoteNo));

        RuleFor(x => x.PcbaRecorder)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissuepcbarework.pcbarecorder", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaRecorder));
    }
}
