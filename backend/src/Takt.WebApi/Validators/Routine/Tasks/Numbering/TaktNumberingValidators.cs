// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.Numbering
// 文件名称：TaktNumberingValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Numbering DTO 验证器（根据实体 TaktNumbering 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Numbering;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Tasks.Numbering;

/// <summary>
/// Numbering创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Numbering.TaktNumbering"/> 字段对齐）。
/// </summary>
public class TaktNumberingCreateDtoValidator : AbstractValidator<TaktNumberingCreateDto>
{
    public TaktNumberingCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.RuleCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.numbering.rulecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.numbering.rulecode", 1, 50));

        RuleFor(x => x.RuleName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.numbering.rulename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.numbering.rulename", 1, 100));

        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.numbering.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.numbering.companycode"));

        RuleFor(x => x.DeptCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.numbering.deptcode"))
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numbering.deptcode", 50));

        RuleFor(x => x.Prefix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numbering.prefix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Prefix));

        RuleFor(x => x.DateFormat)
            .MaximumLength(32).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numbering.dateformat", 32))
            .When(x => !string.IsNullOrWhiteSpace(x.DateFormat));

        RuleFor(x => x.Suffix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numbering.suffix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Suffix));

        RuleFor(x => x.RuleStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.numbering.rulestatus"));
    }
}

/// <summary>
/// Numbering更新 DTO 验证器。
/// </summary>
public class TaktNumberingUpdateDtoValidator : AbstractValidator<TaktNumberingUpdateDto>
{
    public TaktNumberingUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktNumberingCreateDtoValidator(localizer));

        RuleFor(x => x.NumberingId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.numbering.numberingid"));

        RuleFor(x => x.RuleCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numbering.rulecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleCode));

        RuleFor(x => x.RuleName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numbering.rulename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleName));

        RuleFor(x => x.Prefix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numbering.prefix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Prefix));

        RuleFor(x => x.DateFormat)
            .MaximumLength(32).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numbering.dateformat", 32))
            .When(x => !string.IsNullOrWhiteSpace(x.DateFormat));

        RuleFor(x => x.Suffix)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.numbering.suffix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Suffix));
    }
}
