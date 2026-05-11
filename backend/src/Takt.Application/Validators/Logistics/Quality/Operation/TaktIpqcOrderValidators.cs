// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IpqcOrder DTO 验证器（根据实体 TaktIpqcOrder 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// IpqcOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIpqcOrder"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktIpqcOrderCreateDtoValidator : AbstractValidator<TaktIpqcOrderCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIpqcOrderCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorder.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.ipqcorder.plantcode"));

        RuleFor(x => x.SourceCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorder.sourcecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ipqcorder.sourcecode", 1, 50));

        RuleFor(x => x.IpqcOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorder.ipqcordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ipqcorder.ipqcordercode", 1, 50));

        RuleFor(x => x.ProcessCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorder.processcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ipqcorder.processcode", 1, 50));

        RuleFor(x => x.ProcessName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ipqcorder.processname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.ipqcorder.processname", 1, 200));

        RuleFor(x => x.JudgeStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.ipqcorder.judgestatus"));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.JudgeDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.ipqcorder.judgedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeDescription));
    }
}

/// <summary>
/// IpqcOrder更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktIpqcOrderCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>IpqcOrderId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktIpqcOrderUpdateDtoValidator : AbstractValidator<TaktIpqcOrderUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIpqcOrderUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktIpqcOrderCreateDtoValidator(validationMessages));

        RuleFor(x => x.IpqcOrderId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ipqcorder.ipqcorderid"));

        RuleFor(x => x.SourceCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorder.sourcecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SourceCode));

        RuleFor(x => x.IpqcOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorder.ipqcordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IpqcOrderCode));

        RuleFor(x => x.ProcessCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorder.processcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessCode));

        RuleFor(x => x.ProcessName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ipqcorder.processname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProcessName));

        RuleFor(x => x.JudgeBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorder.judgeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeBy));

        RuleFor(x => x.JudgeDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.ipqcorder.judgedescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.JudgeDescription));
    }
}
