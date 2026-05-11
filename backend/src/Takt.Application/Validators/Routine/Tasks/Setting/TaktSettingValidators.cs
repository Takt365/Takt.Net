// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.Setting
// 文件名称：TaktSettingValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Setting DTO 验证器（根据实体 TaktSetting 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Setting;

namespace Takt.Application.Validators.Routine.Tasks.Setting;

/// <summary>
/// Setting创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Setting.TaktSetting"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSettingCreateDtoValidator : AbstractValidator<TaktSettingCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSettingCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.SettingKey)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.setting.settingkey"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.setting.settingkey", 1, 100));

        RuleFor(x => x.SettingValue)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.setting.settingvalue", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingValue));

        RuleFor(x => x.SettingName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.setting.settingname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingName));

        RuleFor(x => x.SettingGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.setting.settinggroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingGroup));

        RuleFor(x => x.IsBuiltIn)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.setting.isbuiltin"));

        RuleFor(x => x.IsEncrypted)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.setting.isencrypted"));

        RuleFor(x => x.SettingStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.setting.settingstatus"));
    }
}

/// <summary>
/// Setting更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSettingCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SettingId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSettingUpdateDtoValidator : AbstractValidator<TaktSettingUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSettingUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSettingCreateDtoValidator(validationMessages));

        RuleFor(x => x.SettingId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.setting.settingid"));

        RuleFor(x => x.SettingKey)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.setting.settingkey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingKey));

        RuleFor(x => x.SettingValue)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.setting.settingvalue", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingValue));

        RuleFor(x => x.SettingName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.setting.settingname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingName));

        RuleFor(x => x.SettingGroup)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.setting.settinggroup", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SettingGroup));
    }
}
