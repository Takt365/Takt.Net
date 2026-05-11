// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktFqcOrderValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FqcOrder DTO 验证器（根据实体 TaktFqcOrder 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// FqcOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktFqcOrder"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFqcOrderCreateDtoValidator : AbstractValidator<TaktFqcOrderCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFqcOrderCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcorder.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.fqcorder.plantcode"));

        RuleFor(x => x.SourceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcorder.sourcecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcorder.sourcecode", 1, 50));

        RuleFor(x => x.FqcOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcorder.fqcordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcorder.fqcordercode", 1, 50));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorder.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));

        RuleFor(x => x.JudgeStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.fqcorder.judgestatus"));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.JudgeDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.fqcorder.judgedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeDescription));
    }
}

/// <summary>
/// FqcOrder更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFqcOrderCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FqcOrderId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFqcOrderUpdateDtoValidator : AbstractValidator<TaktFqcOrderUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFqcOrderUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFqcOrderCreateDtoValidator(validationMessages));

        RuleFor(x => x.FqcOrderId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.fqcorder.fqcorderid"));

        RuleFor(x => x.SourceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorder.sourcecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SourceCode));

        RuleFor(x => x.FqcOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorder.fqcordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FqcOrderCode));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorder.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.JudgeDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.fqcorder.judgedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeDescription));
    }
}
