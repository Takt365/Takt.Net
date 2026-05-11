// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Identity
// 文件名称：TaktRoleValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Role DTO 验证器（根据实体 TaktRole 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Identity;

namespace Takt.Application.Validators.Identity;

/// <summary>
/// Role创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktRole"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktRoleCreateDtoValidator : AbstractValidator<TaktRoleCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktRoleCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.role.rolename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.role.rolename", 1, 100));

        RuleFor(x => x.RoleCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.role.rolecode"))
            .Must(v => TaktRegexHelper.IsMatch(TaktRegexHelper.RoleCode, v)).WithMessage(_validationMessages.FormatInvalid("entity.role.rolecode"));

        RuleFor(x => x.DataScope)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.role.datascope"));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.role.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));

        RuleFor(x => x.RoleStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.role.rolestatus"));
    }
}

/// <summary>
/// Role更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktRoleCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>RoleId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktRoleUpdateDtoValidator : AbstractValidator<TaktRoleUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktRoleUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktRoleCreateDtoValidator(validationMessages));

        RuleFor(x => x.RoleId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.role.roleid"));

        RuleFor(x => x.RoleName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.role.rolename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.RoleName));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.role.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));
    }
}
