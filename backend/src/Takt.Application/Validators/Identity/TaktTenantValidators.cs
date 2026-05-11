// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Identity
// 文件名称：TaktTenantValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Tenant DTO 验证器（根据实体 TaktTenant 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Identity;

namespace Takt.Application.Validators.Identity;

/// <summary>
/// Tenant创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktTenant"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktTenantCreateDtoValidator : AbstractValidator<TaktTenantCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTenantCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.TenantName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.tenant.tenantname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.tenant.tenantname", 1, 100));

        RuleFor(x => x.TenantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.tenant.tenantcode"))
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.tenant.tenantcode", 50));

        RuleFor(x => x.AllowedConfigIds)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.tenant.allowedconfigids"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.tenant.allowedconfigids", 1, 500));

        RuleFor(x => x.TenantStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.tenant.tenantstatus"));
    }
}

/// <summary>
/// Tenant更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktTenantCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>TenantId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktTenantUpdateDtoValidator : AbstractValidator<TaktTenantUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTenantUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktTenantCreateDtoValidator(validationMessages));

        RuleFor(x => x.TenantId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.tenant.tenantid"));

        RuleFor(x => x.TenantName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.tenant.tenantname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.TenantName));

        RuleFor(x => x.AllowedConfigIds)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.tenant.allowedconfigids", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AllowedConfigIds));
    }
}
