// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.SignalR
// 文件名称：TaktOnlineValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Online DTO 验证器（根据实体 TaktOnline 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.SignalR;

namespace Takt.Application.Validators.Routine.Tasks.SignalR;

/// <summary>
/// Online创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.SignalR.TaktOnline"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktOnlineCreateDtoValidator : AbstractValidator<TaktOnlineCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktOnlineCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ConnectionId)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.online.connectionid"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.online.connectionid", 1, 200));

        RuleFor(x => x.OnlineStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.online.onlinestatus"));

        RuleFor(x => x.ConnectIp)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.online.connectip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectIp));

        RuleFor(x => x.ConnectLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.online.connectlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectLocation));

        RuleFor(x => x.ConnectCountry)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.online.connectcountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectCountry));

        RuleFor(x => x.ConnectProvince)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.online.connectprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectProvince));

        RuleFor(x => x.ConnectCity)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.online.connectcity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectCity));

        RuleFor(x => x.ConnectIsp)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.online.connectisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectIsp));

        RuleFor(x => x.UserAgent)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.online.useragent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAgent));

        RuleFor(x => x.DeviceType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.online.devicetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceType));

        RuleFor(x => x.BrowserType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.online.browsertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BrowserType));

        RuleFor(x => x.OperatingSystem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.online.operatingsystem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperatingSystem));
    }
}

/// <summary>
/// Online更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktOnlineCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>OnlineId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktOnlineUpdateDtoValidator : AbstractValidator<TaktOnlineUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktOnlineUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktOnlineCreateDtoValidator(validationMessages));

        RuleFor(x => x.OnlineId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.online.onlineid"));

        RuleFor(x => x.ConnectionId)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.online.connectionid", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectionId));

        RuleFor(x => x.ConnectIp)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.online.connectip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectIp));

        RuleFor(x => x.ConnectLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.online.connectlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectLocation));

        RuleFor(x => x.ConnectCountry)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.online.connectcountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectCountry));

        RuleFor(x => x.ConnectProvince)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.online.connectprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectProvince));

        RuleFor(x => x.ConnectCity)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.online.connectcity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectCity));

        RuleFor(x => x.ConnectIsp)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.online.connectisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ConnectIsp));

        RuleFor(x => x.UserAgent)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.online.useragent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAgent));

        RuleFor(x => x.DeviceType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.online.devicetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DeviceType));

        RuleFor(x => x.BrowserType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.online.browsertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BrowserType));

        RuleFor(x => x.OperatingSystem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.online.operatingsystem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperatingSystem));
    }
}
