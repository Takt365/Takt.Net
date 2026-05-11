// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Accounting.Controlling
// 文件名称：TaktCostElementValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CostElement DTO 验证器（根据实体 TaktCostElement 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Controlling;

namespace Takt.Application.Validators.Accounting.Controlling;

/// <summary>
/// CostElement创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktCostElement"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktCostElementCreateDtoValidator : AbstractValidator<TaktCostElementCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCostElementCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.costelement.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.costelement.companycode"));

        RuleFor(x => x.CostElementCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.costelement.costelementcode"))
            .Must(TaktRegexHelper.IsValidCostElementCode).WithMessage(_validationMessages.FormatInvalid("entity.costelement.costelementcode"));

        RuleFor(x => x.CostElementName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.costelement.costelementname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.costelement.costelementname", 1, 100));

        RuleFor(x => x.CostElementType)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.costelement.costelementtype"));

        RuleFor(x => x.CostElementCategory)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.costelement.costelementcategory"));

        RuleFor(x => x.CostElementStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.costelement.costelementstatus"));
    }
}

/// <summary>
/// CostElement更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktCostElementCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>CostElementId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktCostElementUpdateDtoValidator : AbstractValidator<TaktCostElementUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCostElementUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktCostElementCreateDtoValidator(validationMessages));

        RuleFor(x => x.CostElementId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.costelement.costelementid"));

        RuleFor(x => x.CostElementName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.costelement.costelementname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostElementName));
    }
}
