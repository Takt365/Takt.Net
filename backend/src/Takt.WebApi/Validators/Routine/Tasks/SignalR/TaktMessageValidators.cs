// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.SignalR
// 文件名称：TaktMessageValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Message DTO 验证器（根据实体 TaktMessage 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.SignalR;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Tasks.SignalR;

/// <summary>
/// Message创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.SignalR.TaktMessage"/> 字段对齐）。
/// </summary>
public class TaktMessageCreateDtoValidator : AbstractValidator<TaktMessageCreateDto>
{
    public TaktMessageCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.FromUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.message.fromusername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.message.fromusername", 1, 50));

        RuleFor(x => x.ToUserName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.message.tousername"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.message.tousername", 1, 50));

        RuleFor(x => x.MessageTitle)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.message.messagetitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageTitle));

        RuleFor(x => x.MessageContent)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.message.messagecontent"));

        RuleFor(x => x.MessageType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.message.messagetype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.message.messagetype", 1, 50));

        RuleFor(x => x.MessageGroup)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.message.messagegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageGroup));

        RuleFor(x => x.ReadStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.message.readstatus"));

        RuleFor(x => x.MessageExtData)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.message.messageextdata", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageExtData));
    }
}

/// <summary>
/// Message更新 DTO 验证器。
/// </summary>
public class TaktMessageUpdateDtoValidator : AbstractValidator<TaktMessageUpdateDto>
{
    public TaktMessageUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktMessageCreateDtoValidator(localizer));

        RuleFor(x => x.MessageId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.message.messageid"));

        RuleFor(x => x.FromUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.message.fromusername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FromUserName));

        RuleFor(x => x.ToUserName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.message.tousername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ToUserName));

        RuleFor(x => x.MessageTitle)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.message.messagetitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageTitle));

        RuleFor(x => x.MessageType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.message.messagetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageType));

        RuleFor(x => x.MessageGroup)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.message.messagegroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageGroup));

        RuleFor(x => x.MessageExtData)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.message.messageextdata", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageExtData));
    }
}
