// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Statistics.Logging
// 文件名称：TaktLoginLogValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：LoginLog DTO 验证器（根据实体 TaktLoginLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Logging;

namespace Takt.Application.Validators.Statistics.Logging;

/// <summary>
/// LoginLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Logging.TaktLoginLog"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktLoginLogCreateDtoValidator : AbstractValidator<TaktLoginLogCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktLoginLogCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.LoginIp)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginIp));

        RuleFor(x => x.LoginLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginLocation));

        RuleFor(x => x.LoginCountry)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.loginlog.logincountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginCountry));

        RuleFor(x => x.LoginProvince)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginProvince));

        RuleFor(x => x.LoginCity)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.loginlog.logincity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginCity));

        RuleFor(x => x.LoginIsp)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginIsp));

        RuleFor(x => x.LoginType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.loginlog.logintype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginType));

        RuleFor(x => x.UserAgent)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.loginlog.useragent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAgent));

        RuleFor(x => x.LoginStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.loginlog.loginstatus"));

        RuleFor(x => x.LoginMsg)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginmsg", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginMsg));
    }
}

/// <summary>
/// LoginLog更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktLoginLogCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>LoginLogId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktLoginLogUpdateDtoValidator : AbstractValidator<TaktLoginLogUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktLoginLogUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktLoginLogCreateDtoValidator(validationMessages));

        RuleFor(x => x.LoginLogId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.loginlog.loginlogid"));

        RuleFor(x => x.LoginIp)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginIp));

        RuleFor(x => x.LoginLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginLocation));

        RuleFor(x => x.LoginCountry)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.loginlog.logincountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginCountry));

        RuleFor(x => x.LoginProvince)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginProvince));

        RuleFor(x => x.LoginCity)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.loginlog.logincity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginCity));

        RuleFor(x => x.LoginIsp)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginIsp));

        RuleFor(x => x.LoginType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.loginlog.logintype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginType));

        RuleFor(x => x.UserAgent)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.loginlog.useragent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAgent));

        RuleFor(x => x.LoginMsg)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.loginlog.loginmsg", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginMsg));
    }
}
