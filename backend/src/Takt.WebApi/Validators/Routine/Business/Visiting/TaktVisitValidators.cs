// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.Visiting
// 文件名称：TaktVisitValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Visit DTO 验证器（根据实体 TaktVisit 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.Visiting;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Business.Visiting;

/// <summary>
/// Visit创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.Visiting.TaktVisit"/> 字段对齐）。
/// </summary>
public class TaktVisitCreateDtoValidator : AbstractValidator<TaktVisitCreateDto>
{
    public TaktVisitCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.visit.companyname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.visit.companyname", 1, 200));
    }
}

/// <summary>
/// Visit更新 DTO 验证器。
/// </summary>
public class TaktVisitUpdateDtoValidator : AbstractValidator<TaktVisitUpdateDto>
{
    public TaktVisitUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktVisitCreateDtoValidator(localizer));

        RuleFor(x => x.VisitId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.visit.visitid"));

        RuleFor(x => x.CompanyName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.visit.companyname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyName));
    }
}
