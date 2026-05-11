// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.SignalR
// 文件名称：TaktMessageValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Message DTO 验证器（根据实体 TaktMessage 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.SignalR;

namespace Takt.Application.Validators.Routine.Tasks.SignalR;

/// <summary>
/// Message创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.SignalR.TaktMessage"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktMessageCreateDtoValidator : AbstractValidator<TaktMessageCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMessageCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.FromUserName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.message.fromusername"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.message.fromusername", 1, 50));

        RuleFor(x => x.ToUserName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.message.tousername"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.message.tousername", 1, 50));

        RuleFor(x => x.MessageTitle)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.message.messagetitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageTitle));

        RuleFor(x => x.MessageContent)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.message.messagecontent"));

        RuleFor(x => x.MessageType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.message.messagetype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.message.messagetype", 1, 50));

        RuleFor(x => x.MessageGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.message.messagegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageGroup));

        RuleFor(x => x.ReadStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.message.readstatus"));

        RuleFor(x => x.MessageExtData)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.message.messageextdata", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageExtData));
    }
}

/// <summary>
/// Message更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktMessageCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>MessageId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktMessageUpdateDtoValidator : AbstractValidator<TaktMessageUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktMessageUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktMessageCreateDtoValidator(validationMessages));

        RuleFor(x => x.MessageId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.message.messageid"));

        RuleFor(x => x.FromUserName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.message.fromusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FromUserName));

        RuleFor(x => x.ToUserName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.message.tousername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ToUserName));

        RuleFor(x => x.MessageTitle)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.message.messagetitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageTitle));

        RuleFor(x => x.MessageType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.message.messagetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageType));

        RuleFor(x => x.MessageGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.message.messagegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageGroup));

        RuleFor(x => x.MessageExtData)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.message.messageextdata", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageExtData));
    }
}
