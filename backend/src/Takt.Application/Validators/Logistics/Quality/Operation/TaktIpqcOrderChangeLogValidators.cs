// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderChangeLogValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IpqcOrderChangeLog DTO 验证器（根据实体 TaktIpqcOrderChangeLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// IpqcOrderChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIpqcOrderChangeLog"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktIpqcOrderChangeLogCreateDtoValidator : AbstractValidator<TaktIpqcOrderChangeLogCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIpqcOrderChangeLogCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.ipqcorderchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.ipqcorderchangelog.changetype"));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.ipqcorderchangelog.changereason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorderchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));
    }
}

/// <summary>
/// IpqcOrderChangeLog更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktIpqcOrderChangeLogCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>IpqcOrderChangeLogId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktIpqcOrderChangeLogUpdateDtoValidator : AbstractValidator<TaktIpqcOrderChangeLogUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIpqcOrderChangeLogUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktIpqcOrderChangeLogCreateDtoValidator(validationMessages));

        RuleFor(x => x.IpqcOrderChangeLogId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ipqcorderchangelog.ipqcorderchangelogid"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.ipqcorderchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.ipqcorderchangelog.changereason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ipqcorderchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));
    }
}
