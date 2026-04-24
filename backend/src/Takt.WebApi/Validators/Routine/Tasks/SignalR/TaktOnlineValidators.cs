// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.SignalR
// 文件名称：TaktOnlineValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Online DTO 验证器（根据实体 TaktOnline 自动生成）
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
/// Online创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.SignalR.TaktOnline"/> 字段对齐）。
/// </summary>
public class TaktOnlineCreateDtoValidator : AbstractValidator<TaktOnlineCreateDto>
{
    public TaktOnlineCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ConnectionId)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.online.connectionid"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.online.connectionid", 1, 200));

        RuleFor(x => x.OnlineStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.online.onlinestatus"));

        RuleFor(x => x.ConnectIp)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectIp));

        RuleFor(x => x.ConnectLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectLocation));

        RuleFor(x => x.ConnectCountry)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectcountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectCountry));

        RuleFor(x => x.ConnectProvince)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectProvince));

        RuleFor(x => x.ConnectCity)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectcity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectCity));

        RuleFor(x => x.ConnectIsp)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectIsp));

        RuleFor(x => x.UserAgent)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.useragent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAgent));

        RuleFor(x => x.DeviceType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.devicetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceType));

        RuleFor(x => x.BrowserType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.browsertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BrowserType));

        RuleFor(x => x.OperatingSystem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.operatingsystem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperatingSystem));
    }
}

/// <summary>
/// Online更新 DTO 验证器。
/// </summary>
public class TaktOnlineUpdateDtoValidator : AbstractValidator<TaktOnlineUpdateDto>
{
    public TaktOnlineUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktOnlineCreateDtoValidator(localizer));

        RuleFor(x => x.OnlineId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.online.onlineid"));

        RuleFor(x => x.ConnectionId)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectionid", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectionId));

        RuleFor(x => x.ConnectIp)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectIp));

        RuleFor(x => x.ConnectLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectLocation));

        RuleFor(x => x.ConnectCountry)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectcountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectCountry));

        RuleFor(x => x.ConnectProvince)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectProvince));

        RuleFor(x => x.ConnectCity)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectcity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectCity));

        RuleFor(x => x.ConnectIsp)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.connectisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectIsp));

        RuleFor(x => x.UserAgent)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.useragent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAgent));

        RuleFor(x => x.DeviceType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.devicetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceType));

        RuleFor(x => x.BrowserType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.browsertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BrowserType));

        RuleFor(x => x.OperatingSystem)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.online.operatingsystem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperatingSystem));
    }
}
