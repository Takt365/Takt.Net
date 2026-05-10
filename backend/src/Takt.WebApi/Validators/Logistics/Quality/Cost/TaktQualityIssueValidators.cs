// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityIssue DTO 验证器（根据实体 TaktQualityIssue 自动生成）
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
/// QualityIssue创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityIssue"/> 字段对齐）。
/// </summary>
public class TaktQualityIssueCreateDtoValidator : AbstractValidator<TaktQualityIssueCreateDto>
{
    public TaktQualityIssueCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityissue.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.qualityissue.plantcode"));

        RuleFor(x => x.IssueNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityissue.issueno"))
            .Length(1, 30).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.qualityissue.issueno", 1, 30));

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityissue.model"))
            .Length(1, 255).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.qualityissue.model", 1, 255));

        RuleFor(x => x.Lot)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityissue.lot"))
            .Length(1, 30).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.qualityissue.lot", 1, 30));

        RuleFor(x => x.QualityProblemsResponse)
            .MaximumLength(255).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.qualityproblemsresponse", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.QualityProblemsResponse));

        RuleFor(x => x.ReworkDueToDefects)
            .MaximumLength(255).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.reworkduetodefects", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.ReworkDueToDefects));

        RuleFor(x => x.NeedRework)
            .MaximumLength(1).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.needrework", 1))
            .When(x => !string.IsNullOrWhiteSpace(x.NeedRework));

        RuleFor(x => x.CostCurrency)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityissue.costcurrency"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.qualityissue.costcurrency", 1, 10));
    }
}

/// <summary>
/// QualityIssue更新 DTO 验证器。
/// </summary>
public class TaktQualityIssueUpdateDtoValidator : AbstractValidator<TaktQualityIssueUpdateDto>
{
    public TaktQualityIssueUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktQualityIssueCreateDtoValidator(localizer));

        RuleFor(x => x.QualityIssueId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityissue.qualityissueid"));

        RuleFor(x => x.IssueNo)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.issueno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.IssueNo));

        RuleFor(x => x.Model)
            .MaximumLength(255).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.model", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.Model));

        RuleFor(x => x.Lot)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.lot", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.Lot));

        RuleFor(x => x.QualityProblemsResponse)
            .MaximumLength(255).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.qualityproblemsresponse", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.QualityProblemsResponse));

        RuleFor(x => x.ReworkDueToDefects)
            .MaximumLength(255).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.reworkduetodefects", 255))
            .When(x => !string.IsNullOrWhiteSpace(x.ReworkDueToDefects));

        RuleFor(x => x.NeedRework)
            .MaximumLength(1).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.needrework", 1))
            .When(x => !string.IsNullOrWhiteSpace(x.NeedRework));

        RuleFor(x => x.CostCurrency)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.qualityissue.costcurrency", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCurrency));
    }
}
