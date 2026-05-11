// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Statistics.Logging
// 文件名称：TaktOperLogValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：OperLog DTO 验证器（根据实体 TaktOperLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Logging;

namespace Takt.Application.Validators.Statistics.Logging;

/// <summary>
/// OperLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Logging.TaktOperLog"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktOperLogCreateDtoValidator : AbstractValidator<TaktOperLogCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktOperLogCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.OperModule)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.opermodule", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperModule));

        RuleFor(x => x.OperType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.operlog.opertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperType));

        RuleFor(x => x.OperMethod)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.operlog.opermethod", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.OperMethod));

        RuleFor(x => x.RequestMethod)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.operlog.requestmethod", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestMethod));

        RuleFor(x => x.OperUrl)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.operlog.operurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.OperUrl));

        RuleFor(x => x.OperStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.operlog.operstatus"));

        RuleFor(x => x.ErrorMsg)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.operlog.errormsg", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMsg));

        RuleFor(x => x.OperIp)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.operlog.operip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperIp));

        RuleFor(x => x.OperLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.operlog.operlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.OperLocation));

        RuleFor(x => x.OperCountry)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.opercountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperCountry));

        RuleFor(x => x.OperProvince)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.operprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperProvince));

        RuleFor(x => x.OperCity)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.opercity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperCity));

        RuleFor(x => x.OperIsp)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.operisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperIsp));
    }
}

/// <summary>
/// OperLog更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktOperLogCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>OperLogId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktOperLogUpdateDtoValidator : AbstractValidator<TaktOperLogUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktOperLogUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktOperLogCreateDtoValidator(validationMessages));

        RuleFor(x => x.OperLogId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.operlog.operlogid"));

        RuleFor(x => x.OperModule)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.opermodule", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperModule));

        RuleFor(x => x.OperType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.operlog.opertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperType));

        RuleFor(x => x.OperMethod)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.operlog.opermethod", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.OperMethod));

        RuleFor(x => x.RequestMethod)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.operlog.requestmethod", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestMethod));

        RuleFor(x => x.OperUrl)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.operlog.operurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.OperUrl));

        RuleFor(x => x.ErrorMsg)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.operlog.errormsg", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMsg));

        RuleFor(x => x.OperIp)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.operlog.operip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperIp));

        RuleFor(x => x.OperLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.operlog.operlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.OperLocation));

        RuleFor(x => x.OperCountry)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.opercountry", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperCountry));

        RuleFor(x => x.OperProvince)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.operprovince", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperProvince));

        RuleFor(x => x.OperCity)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.opercity", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperCity));

        RuleFor(x => x.OperIsp)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.operlog.operisp", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperIsp));
    }
}
