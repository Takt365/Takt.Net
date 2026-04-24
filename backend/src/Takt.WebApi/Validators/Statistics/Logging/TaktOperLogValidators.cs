// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Statistics.Logging
// 文件名称：TaktOperLogValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：OperLog DTO 验证器（根据实体 TaktOperLog 自动生成）
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
/// OperLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Logging.TaktOperLog"/> 字段对齐）。
/// </summary>
public class TaktOperLogCreateDtoValidator : AbstractValidator<TaktOperLogCreateDto>
{
    public TaktOperLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.OperModule)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opermodule", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperModule));

        RuleFor(x => x.OperType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperType));

        RuleFor(x => x.OperMethod)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opermethod", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.OperMethod));

        RuleFor(x => x.RequestMethod)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.requestmethod", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestMethod));

        RuleFor(x => x.OperUrl)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.OperUrl));

        RuleFor(x => x.OperStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.operlog.operstatus"));

        RuleFor(x => x.ErrorMsg)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.errormsg", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMsg));

        RuleFor(x => x.OperIp)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperIp));

        RuleFor(x => x.OperLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.OperLocation));

        RuleFor(x => x.OperCountry)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opercountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperCountry));

        RuleFor(x => x.OperProvince)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperProvince));

        RuleFor(x => x.OperCity)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opercity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperCity));

        RuleFor(x => x.OperIsp)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperIsp));
    }
}

/// <summary>
/// OperLog更新 DTO 验证器。
/// </summary>
public class TaktOperLogUpdateDtoValidator : AbstractValidator<TaktOperLogUpdateDto>
{
    public TaktOperLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktOperLogCreateDtoValidator(localizer));

        RuleFor(x => x.OperLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.operlog.operlogid"));

        RuleFor(x => x.OperModule)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opermodule", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperModule));

        RuleFor(x => x.OperType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperType));

        RuleFor(x => x.OperMethod)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opermethod", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.OperMethod));

        RuleFor(x => x.RequestMethod)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.requestmethod", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestMethod));

        RuleFor(x => x.OperUrl)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.OperUrl));

        RuleFor(x => x.ErrorMsg)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.errormsg", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMsg));

        RuleFor(x => x.OperIp)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperIp));

        RuleFor(x => x.OperLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.OperLocation));

        RuleFor(x => x.OperCountry)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opercountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperCountry));

        RuleFor(x => x.OperProvince)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperProvince));

        RuleFor(x => x.OperCity)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.opercity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperCity));

        RuleFor(x => x.OperIsp)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.operlog.operisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperIsp));
    }
}
