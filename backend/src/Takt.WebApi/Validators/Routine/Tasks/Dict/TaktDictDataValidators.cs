// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.Dict
// 文件名称：TaktDictDataValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：DictData DTO 验证器（根据实体 TaktDictData 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Dict;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Tasks.Dict;

/// <summary>
/// DictData创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Dict.TaktDictData"/> 字段对齐）。
/// </summary>
public class TaktDictDataCreateDtoValidator : AbstractValidator<TaktDictDataCreateDto>
{
    public TaktDictDataCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DictTypeCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dictdata.dicttypecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.dictdata.dicttypecode", 1, 50));

        RuleFor(x => x.DictLabel)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dictdata.dictlabel"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.dictdata.dictlabel", 1, 100));

        RuleFor(x => x.DictL10nKey)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dictdata.dictl10nkey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DictL10nKey));

        RuleFor(x => x.DictValue)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dictdata.dictvalue"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.dictdata.dictvalue", 1, 200));

        RuleFor(x => x.ExtLabel)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dictdata.extlabel", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ExtLabel));

        RuleFor(x => x.ExtValue)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dictdata.extvalue", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ExtValue));
    }
}

/// <summary>
/// DictData更新 DTO 验证器。
/// </summary>
public class TaktDictDataUpdateDtoValidator : AbstractValidator<TaktDictDataUpdateDto>
{
    public TaktDictDataUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktDictDataCreateDtoValidator(localizer));

        RuleFor(x => x.DictDataId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.dictdata.dictdataid"));

        RuleFor(x => x.DictTypeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dictdata.dicttypecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DictTypeCode));

        RuleFor(x => x.DictLabel)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dictdata.dictlabel", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DictLabel));

        RuleFor(x => x.DictL10nKey)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dictdata.dictl10nkey", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DictL10nKey));

        RuleFor(x => x.DictValue)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dictdata.dictvalue", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DictValue));

        RuleFor(x => x.ExtLabel)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dictdata.extlabel", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ExtLabel));

        RuleFor(x => x.ExtValue)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dictdata.extvalue", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ExtValue));
    }
}
