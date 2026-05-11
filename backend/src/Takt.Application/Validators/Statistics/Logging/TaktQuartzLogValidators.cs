// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Statistics.Logging
// 文件名称：TaktQuartzLogValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QuartzLog DTO 验证器（根据实体 TaktQuartzLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Logging;

namespace Takt.Application.Validators.Statistics.Logging;

/// <summary>
/// QuartzLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Logging.TaktQuartzLog"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktQuartzLogCreateDtoValidator : AbstractValidator<TaktQuartzLogCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQuartzLogCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.JobName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.quartzlog.jobname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.quartzlog.jobname", 1, 200));

        RuleFor(x => x.JobGroup)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.quartzlog.jobgroup"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.quartzlog.jobgroup", 1, 200));

        RuleFor(x => x.TriggerName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.quartzlog.triggername"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.quartzlog.triggername", 1, 200));

        RuleFor(x => x.TriggerGroup)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.quartzlog.triggergroup"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.quartzlog.triggergroup", 1, 200));

        RuleFor(x => x.ExecuteStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.quartzlog.executestatus"));

        RuleFor(x => x.ErrorMsg)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.quartzlog.errormsg", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMsg));
    }
}

/// <summary>
/// QuartzLog更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktQuartzLogCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>QuartzLogId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktQuartzLogUpdateDtoValidator : AbstractValidator<TaktQuartzLogUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQuartzLogUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktQuartzLogCreateDtoValidator(validationMessages));

        RuleFor(x => x.QuartzLogId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.quartzlog.quartzlogid"));

        RuleFor(x => x.JobName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.quartzlog.jobname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.JobName));

        RuleFor(x => x.JobGroup)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.quartzlog.jobgroup", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.JobGroup));

        RuleFor(x => x.TriggerName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.quartzlog.triggername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TriggerName));

        RuleFor(x => x.TriggerGroup)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.quartzlog.triggergroup", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TriggerGroup));

        RuleFor(x => x.ErrorMsg)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.quartzlog.errormsg", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMsg));
    }
}
