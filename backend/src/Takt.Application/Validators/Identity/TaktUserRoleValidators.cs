// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Identity
// 文件名称：TaktUserRoleValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：UserRole DTO 验证器（根据实体 TaktUserRole 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Identity;

namespace Takt.Application.Validators.Identity;

/// <summary>
/// UserRole创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktUserRole"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktUserRoleCreateDtoValidator : AbstractValidator<TaktUserRoleCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktUserRoleCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
    }
}

/// <summary>
/// UserRole更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktUserRoleCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>UserRoleId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktUserRoleUpdateDtoValidator : AbstractValidator<TaktUserRoleUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktUserRoleUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktUserRoleCreateDtoValidator(validationMessages));

        RuleFor(x => x.UserRoleId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.userrole.userroleid"));

    }
}
