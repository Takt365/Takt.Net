// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.Setting
// 文件名称：TaktSettingsValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Settings DTO 验证器（根据实体 TaktSettings 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Setting;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Tasks.Setting;

/// <summary>
/// Settings创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Setting.TaktSettings"/> 字段对齐）。
/// </summary>
public class TaktSettingsCreateDtoValidator : AbstractValidator<TaktSettingsCreateDto>
{
    public TaktSettingsCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.SettingKey)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.settings.settingkey"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.settings.settingkey", 1, 100));

        RuleFor(x => x.SettingValue)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.settings.settingvalue", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingValue));

        RuleFor(x => x.SettingName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.settings.settingname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingName));

        RuleFor(x => x.SettingGroup)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.settings.settinggroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingGroup));

        RuleFor(x => x.IsBuiltIn)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.settings.isbuiltin"));

        RuleFor(x => x.IsEncrypted)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.settings.isencrypted"));

        RuleFor(x => x.SettingStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.settings.settingstatus"));
    }
}

/// <summary>
/// Settings更新 DTO 验证器。
/// </summary>
public class TaktSettingsUpdateDtoValidator : AbstractValidator<TaktSettingsUpdateDto>
{
    public TaktSettingsUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktSettingsCreateDtoValidator(localizer));

        RuleFor(x => x.SettingsId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.settings.settingsid"));

        RuleFor(x => x.SettingKey)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.settings.settingkey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingKey));

        RuleFor(x => x.SettingValue)
            .MaximumLength(4000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.settings.settingvalue", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingValue));

        RuleFor(x => x.SettingName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.settings.settingname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingName));

        RuleFor(x => x.SettingGroup)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.settings.settinggroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingGroup));
    }
}
