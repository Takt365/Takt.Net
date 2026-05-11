// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Statistics.Logging
// 文件名称：TaktAopLogValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AopLog DTO 验证器（根据实体 TaktAopLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Logging;

namespace Takt.Application.Validators.Statistics.Logging;

/// <summary>
/// AopLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Logging.TaktAopLog"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAopLogCreateDtoValidator : AbstractValidator<TaktAopLogCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAopLogCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.OperType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.aoplog.opertype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.aoplog.opertype", 1, 50));

        RuleFor(x => x.TableName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.aoplog.tablename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.aoplog.tablename", 1, 200));
    }
}

/// <summary>
/// AopLog更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAopLogCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AopLogId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAopLogUpdateDtoValidator : AbstractValidator<TaktAopLogUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAopLogUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAopLogCreateDtoValidator(validationMessages));

        RuleFor(x => x.AopLogId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.aoplog.aoplogid"));

        RuleFor(x => x.OperType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.aoplog.opertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperType));

        RuleFor(x => x.TableName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.aoplog.tablename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TableName));
    }
}
