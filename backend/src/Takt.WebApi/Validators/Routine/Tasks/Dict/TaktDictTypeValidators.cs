// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.Dict
// 文件名称：TaktDictTypeValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：DictType DTO 验证器（根据实体 TaktDictType 自动生成）
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
/// DictType创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Dict.TaktDictType"/> 字段对齐）。
/// </summary>
public class TaktDictTypeCreateDtoValidator : AbstractValidator<TaktDictTypeCreateDto>
{
    public TaktDictTypeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DictTypeCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dicttype.dicttypecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.dicttype.dicttypecode", 1, 50));

        RuleFor(x => x.DictTypeName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dicttype.dicttypename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.dicttype.dicttypename", 1, 100));

        RuleFor(x => x.DataSource)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dicttype.datasource"));

        RuleFor(x => x.DataConfigId)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.dicttype.dataconfigid"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.dicttype.dataconfigid", 1, 50));

        RuleFor(x => x.IsBuiltIn)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dicttype.isbuiltin"));

        RuleFor(x => x.DictTypeStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.dicttype.dicttypestatus"));
    }
}

/// <summary>
/// DictType更新 DTO 验证器。
/// </summary>
public class TaktDictTypeUpdateDtoValidator : AbstractValidator<TaktDictTypeUpdateDto>
{
    public TaktDictTypeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktDictTypeCreateDtoValidator(localizer));

        RuleFor(x => x.DictTypeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.dicttype.dicttypeid"));

        RuleFor(x => x.DictTypeCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dicttype.dicttypecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DictTypeCode));

        RuleFor(x => x.DictTypeName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dicttype.dicttypename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DictTypeName));

        RuleFor(x => x.DataConfigId)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.dicttype.dataconfigid", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DataConfigId));
    }
}
