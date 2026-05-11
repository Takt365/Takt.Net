// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Accounting.Controlling
// 文件名称：TaktStandardWageRateValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：StandardWageRate DTO 验证器（根据实体 TaktStandardWageRate 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Controlling;

namespace Takt.Application.Validators.Accounting.Controlling;

/// <summary>
/// StandardWageRate创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktStandardWageRate"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktStandardWageRateCreateDtoValidator : AbstractValidator<TaktStandardWageRateCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktStandardWageRateCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.standardwagerate.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.standardwagerate.companycode"));

        RuleFor(x => x.YearMonth)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.standardwagerate.yearmonth"))
            .Length(1, 6).WithMessage(_validationMessages.LengthBetween("entity.standardwagerate.yearmonth", 1, 6));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.standardwagerate.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}

/// <summary>
/// StandardWageRate更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktStandardWageRateCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>StandardWageRateId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktStandardWageRateUpdateDtoValidator : AbstractValidator<TaktStandardWageRateUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktStandardWageRateUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktStandardWageRateCreateDtoValidator(validationMessages));

        RuleFor(x => x.StandardWageRateId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.standardwagerate.standardwagerateid"));

        RuleFor(x => x.YearMonth)
            .MaximumLength(6).WithMessage(_validationMessages.LengthMax("entity.standardwagerate.yearmonth", 6))
            .When(x => !string.IsNullOrWhiteSpace(x.YearMonth));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.standardwagerate.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
