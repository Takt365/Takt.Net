// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Identity
// 文件名称：TaktTenantValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Tenant DTO 验证器（根据实体 TaktTenant 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Identity;

/// <summary>
/// Tenant创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Identity.TaktTenant"/> 字段对齐）。
/// </summary>
public class TaktTenantCreateDtoValidator : AbstractValidator<TaktTenantCreateDto>
{
    public TaktTenantCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TenantName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.tenant.tenantname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.tenant.tenantname", 1, 100));

        RuleFor(x => x.TenantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.tenant.tenantcode"))
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.tenant.tenantcode", 50));

        RuleFor(x => x.AllowedConfigIds)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.tenant.allowedconfigids"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.tenant.allowedconfigids", 1, 500));

        RuleFor(x => x.TenantStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.tenant.tenantstatus"));
    }
}

/// <summary>
/// Tenant更新 DTO 验证器。
/// </summary>
public class TaktTenantUpdateDtoValidator : AbstractValidator<TaktTenantUpdateDto>
{
    public TaktTenantUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktTenantCreateDtoValidator(localizer));

        RuleFor(x => x.TenantId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.tenant.tenantid"));

        RuleFor(x => x.TenantName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.tenant.tenantname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.TenantName));

        RuleFor(x => x.AllowedConfigIds)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.tenant.allowedconfigids", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AllowedConfigIds));
    }
}
