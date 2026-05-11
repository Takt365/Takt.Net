// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.HelpDesk
// 文件名称：TaktSelfServiceValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SelfService DTO 验证器（根据实体 TaktSelfService 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.HelpDesk;

namespace Takt.Application.Validators.Routine.Business.HelpDesk;

/// <summary>
/// SelfService创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktSelfService"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSelfServiceCreateDtoValidator : AbstractValidator<TaktSelfServiceCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSelfServiceCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ServiceName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.selfservice.servicename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.selfservice.servicename", 1, 100));

        RuleFor(x => x.ServiceType)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.selfservice.servicetype"));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.selfservice.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.LinkOrCode)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.selfservice.linkorcode", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkOrCode));

        RuleFor(x => x.IconUrl)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.selfservice.iconurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.IconUrl));

        RuleFor(x => x.SelfServiceStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.selfservice.selfservicestatus"));
    }
}

/// <summary>
/// SelfService更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSelfServiceCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SelfServiceId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSelfServiceUpdateDtoValidator : AbstractValidator<TaktSelfServiceUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSelfServiceUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSelfServiceCreateDtoValidator(validationMessages));

        RuleFor(x => x.SelfServiceId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.selfservice.selfserviceid"));

        RuleFor(x => x.ServiceName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.selfservice.servicename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ServiceName));

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.selfservice.description", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.LinkOrCode)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.selfservice.linkorcode", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.LinkOrCode));

        RuleFor(x => x.IconUrl)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.selfservice.iconurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.IconUrl));
    }
}
