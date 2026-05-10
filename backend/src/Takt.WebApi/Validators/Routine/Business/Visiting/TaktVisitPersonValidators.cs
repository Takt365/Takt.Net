// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Business.Visiting
// 文件名称：TaktVisitPersonValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：VisitPerson DTO 验证器（根据实体 TaktVisitPerson 自动生成）
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
/// VisitPerson创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.Visiting.TaktVisitPerson"/> 字段对齐）。
/// </summary>
public class TaktVisitPersonCreateDtoValidator : AbstractValidator<TaktVisitPersonCreateDto>
{
    public TaktVisitPersonCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.Department)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.visitperson.department"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.visitperson.department", 1, 100));

        RuleFor(x => x.JobTitle)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.visitperson.jobtitle"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.visitperson.jobtitle", 1, 100));

        RuleFor(x => x.PersonName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.visitperson.personname"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.visitperson.personname", 1, 50));
    }
}

/// <summary>
/// VisitPerson更新 DTO 验证器。
/// </summary>
public class TaktVisitPersonUpdateDtoValidator : AbstractValidator<TaktVisitPersonUpdateDto>
{
    public TaktVisitPersonUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktVisitPersonCreateDtoValidator(localizer));

        RuleFor(x => x.VisitPersonId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.visitperson.visitpersonid"));

        RuleFor(x => x.Department)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.visitperson.department", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.Department));

        RuleFor(x => x.JobTitle)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.visitperson.jobtitle", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.JobTitle));

        RuleFor(x => x.PersonName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.visitperson.personname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PersonName));
    }
}
