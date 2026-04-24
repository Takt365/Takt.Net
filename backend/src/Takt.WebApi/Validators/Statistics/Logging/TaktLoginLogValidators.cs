// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Statistics.Logging
// 文件名称：TaktLoginLogValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：LoginLog DTO 验证器（根据实体 TaktLoginLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Statistics.Logging;

/// <summary>
/// LoginLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Logging.TaktLoginLog"/> 字段对齐）。
/// </summary>
public class TaktLoginLogCreateDtoValidator : AbstractValidator<TaktLoginLogCreateDto>
{
    public TaktLoginLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.LoginIp)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginIp));

        RuleFor(x => x.LoginLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginLocation));

        RuleFor(x => x.LoginCountry)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.logincountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginCountry));

        RuleFor(x => x.LoginProvince)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginProvince));

        RuleFor(x => x.LoginCity)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.logincity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginCity));

        RuleFor(x => x.LoginIsp)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginIsp));

        RuleFor(x => x.LoginType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.logintype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginType));

        RuleFor(x => x.UserAgent)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.useragent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAgent));

        RuleFor(x => x.LoginStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.loginlog.loginstatus"));

        RuleFor(x => x.LoginMsg)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginmsg", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginMsg));
    }
}

/// <summary>
/// LoginLog更新 DTO 验证器。
/// </summary>
public class TaktLoginLogUpdateDtoValidator : AbstractValidator<TaktLoginLogUpdateDto>
{
    public TaktLoginLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktLoginLogCreateDtoValidator(localizer));

        RuleFor(x => x.LoginLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.loginlog.loginlogid"));

        RuleFor(x => x.LoginIp)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginIp));

        RuleFor(x => x.LoginLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginLocation));

        RuleFor(x => x.LoginCountry)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.logincountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginCountry));

        RuleFor(x => x.LoginProvince)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginProvince));

        RuleFor(x => x.LoginCity)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.logincity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginCity));

        RuleFor(x => x.LoginIsp)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginIsp));

        RuleFor(x => x.LoginType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.logintype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginType));

        RuleFor(x => x.UserAgent)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.useragent", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UserAgent));

        RuleFor(x => x.LoginMsg)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.loginlog.loginmsg", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LoginMsg));
    }
}
