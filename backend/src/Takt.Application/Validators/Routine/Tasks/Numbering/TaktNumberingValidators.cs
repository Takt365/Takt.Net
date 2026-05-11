// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.Numbering
// 文件名称：TaktNumberingValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Numbering DTO 验证器（根据实体 TaktNumbering 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Numbering;

namespace Takt.Application.Validators.Routine.Tasks.Numbering;

/// <summary>
/// Numbering创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Numbering.TaktNumbering"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktNumberingCreateDtoValidator : AbstractValidator<TaktNumberingCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNumberingCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.RuleCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.numbering.rulecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.numbering.rulecode", 1, 50));

        RuleFor(x => x.RuleName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.numbering.rulename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.numbering.rulename", 1, 100));

        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.numbering.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.numbering.companycode"));

        RuleFor(x => x.DeptCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.numbering.deptcode"))
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.numbering.deptcode", 50));

        RuleFor(x => x.Prefix)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.numbering.prefix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Prefix));

        RuleFor(x => x.DateFormat)
            .MaximumLength(32).WithMessage(_validationMessages.LengthMax("entity.numbering.dateformat", 32))
            .When(x => !string.IsNullOrWhiteSpace(x.DateFormat));

        RuleFor(x => x.Suffix)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.numbering.suffix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Suffix));

        RuleFor(x => x.RuleStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.numbering.rulestatus"));
    }
}

/// <summary>
/// Numbering更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktNumberingCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>NumberingId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktNumberingUpdateDtoValidator : AbstractValidator<TaktNumberingUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNumberingUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktNumberingCreateDtoValidator(validationMessages));

        RuleFor(x => x.NumberingId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.numbering.numberingid"));

        RuleFor(x => x.RuleCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.numbering.rulecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleCode));

        RuleFor(x => x.RuleName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.numbering.rulename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RuleName));

        RuleFor(x => x.Prefix)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.numbering.prefix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Prefix));

        RuleFor(x => x.DateFormat)
            .MaximumLength(32).WithMessage(_validationMessages.LengthMax("entity.numbering.dateformat", 32))
            .When(x => !string.IsNullOrWhiteSpace(x.DateFormat));

        RuleFor(x => x.Suffix)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.numbering.suffix", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Suffix));
    }
}
