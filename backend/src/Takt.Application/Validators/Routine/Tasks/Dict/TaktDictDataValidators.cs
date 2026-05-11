// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.Dict
// 文件名称：TaktDictDataValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：DictData DTO 验证器（根据实体 TaktDictData 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Dict;

namespace Takt.Application.Validators.Routine.Tasks.Dict;

/// <summary>
/// DictData创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Dict.TaktDictData"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktDictDataCreateDtoValidator : AbstractValidator<TaktDictDataCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktDictDataCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.DictTypeCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dictdata.dicttypecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.dictdata.dicttypecode", 1, 50));

        RuleFor(x => x.DictLabel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dictdata.dictlabel"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.dictdata.dictlabel", 1, 100));

        RuleFor(x => x.DictL10nKey)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.dictdata.dictl10nkey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DictL10nKey));

        RuleFor(x => x.DictValue)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.dictdata.dictvalue"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.dictdata.dictvalue", 1, 200));

        RuleFor(x => x.ExtLabel)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.dictdata.extlabel", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ExtLabel));

        RuleFor(x => x.ExtValue)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.dictdata.extvalue", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ExtValue));
    }
}

/// <summary>
/// DictData更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktDictDataCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>DictDataId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktDictDataUpdateDtoValidator : AbstractValidator<TaktDictDataUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktDictDataUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktDictDataCreateDtoValidator(validationMessages));

        RuleFor(x => x.DictDataId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.dictdata.dictdataid"));

        RuleFor(x => x.DictTypeCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.dictdata.dicttypecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DictTypeCode));

        RuleFor(x => x.DictLabel)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.dictdata.dictlabel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DictLabel));

        RuleFor(x => x.DictL10nKey)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.dictdata.dictl10nkey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DictL10nKey));

        RuleFor(x => x.DictValue)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.dictdata.dictvalue", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DictValue));

        RuleFor(x => x.ExtLabel)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.dictdata.extlabel", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ExtLabel));

        RuleFor(x => x.ExtValue)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.dictdata.extvalue", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ExtValue));
    }
}
