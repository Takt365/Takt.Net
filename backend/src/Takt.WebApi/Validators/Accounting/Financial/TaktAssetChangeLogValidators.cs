// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Accounting.Financial
// 文件名称：TaktAssetChangeLogValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AssetChangeLog DTO 验证器（根据实体 TaktAssetChangeLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Financial;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Accounting.Financial;

/// <summary>
/// AssetChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Financial.TaktAssetChangeLog"/> 字段对齐）。
/// </summary>
public class TaktAssetChangeLogCreateDtoValidator : AbstractValidator<TaktAssetChangeLogCreateDto>
{
    public TaktAssetChangeLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.AssetCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.assetchangelog.assetcode"))
            .Must(TaktRegexHelper.IsValidAssetCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.assetchangelog.assetcode"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assetchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assetchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assetchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// AssetChangeLog更新 DTO 验证器。
/// </summary>
public class TaktAssetChangeLogUpdateDtoValidator : AbstractValidator<TaktAssetChangeLogUpdateDto>
{
    public TaktAssetChangeLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAssetChangeLogCreateDtoValidator(localizer));

        RuleFor(x => x.AssetChangeLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.assetchangelog.assetchangelogid"));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assetchangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assetchangelog.changeby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeBy));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assetchangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
