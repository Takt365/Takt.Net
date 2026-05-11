// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.Dict
// 文件名称：TaktDictTypeValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：DictType DTO 验证器（根据实体 TaktDictType 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Dict;

namespace Takt.Application.Validators.Routine.Tasks.Dict;

/// <summary>
/// DictType创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Dict.TaktDictType"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktDictTypeCreateDtoValidator : AbstractValidator<TaktDictTypeCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktDictTypeCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.DictTypeCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dicttype.dicttypecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.dicttype.dicttypecode", 1, 50));

        RuleFor(x => x.DictTypeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dicttype.dicttypename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.dicttype.dicttypename", 1, 100));

        RuleFor(x => x.DataSource)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.dicttype.datasource"));

        RuleFor(x => x.DataConfigId)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dicttype.dataconfigid"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.dicttype.dataconfigid", 1, 50));

        RuleFor(x => x.IsBuiltIn)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.dicttype.isbuiltin"));

        RuleFor(x => x.DictTypeStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.dicttype.dicttypestatus"));
    }
}

/// <summary>
/// DictType更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktDictTypeCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>DictTypeId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktDictTypeUpdateDtoValidator : AbstractValidator<TaktDictTypeUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktDictTypeUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktDictTypeCreateDtoValidator(validationMessages));

        RuleFor(x => x.DictTypeId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.dicttype.dicttypeid"));

        RuleFor(x => x.DictTypeCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.dicttype.dicttypecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DictTypeCode));

        RuleFor(x => x.DictTypeName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.dicttype.dicttypename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DictTypeName));

        RuleFor(x => x.DataConfigId)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.dicttype.dataconfigid", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DataConfigId));
    }
}
