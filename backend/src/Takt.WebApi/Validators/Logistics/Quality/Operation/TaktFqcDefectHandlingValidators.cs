// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Operation
// 文件名称：TaktFqcDefectHandlingValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FqcDefectHandling DTO 验证器（根据实体 TaktFqcDefectHandling 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Quality.Operation;

/// <summary>
/// FqcDefectHandling创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktFqcDefectHandling"/> 字段对齐）。
/// </summary>
public class TaktFqcDefectHandlingCreateDtoValidator : AbstractValidator<TaktFqcDefectHandlingCreateDto>
{
    public TaktFqcDefectHandlingCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.HandlingCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcdefecthandling.handlingcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcdefecthandling.handlingcode", 1, 50));

        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcdefecthandling.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcdefecthandling.ordercode", 1, 50));

        RuleFor(x => x.DefectType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.fqcdefecthandling.defecttype"));

        RuleFor(x => x.DefectCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcdefecthandling.defectcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcdefecthandling.defectcode", 1, 50));

        RuleFor(x => x.DefectDescription)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcdefecthandling.defectdescription"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.fqcdefecthandling.defectdescription", 1, 500));

        RuleFor(x => x.HandlingMethod)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.fqcdefecthandling.handlingmethod"));

        RuleFor(x => x.HandlingDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.handlingdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingDescription));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.HandlingStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.fqcdefecthandling.handlingstatus"));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.correctiveaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.DefectImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.defectimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectImages));
    }
}

/// <summary>
/// FqcDefectHandling更新 DTO 验证器。
/// </summary>
public class TaktFqcDefectHandlingUpdateDtoValidator : AbstractValidator<TaktFqcDefectHandlingUpdateDto>
{
    public TaktFqcDefectHandlingUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktFqcDefectHandlingCreateDtoValidator(localizer));

        RuleFor(x => x.FqcDefectHandlingId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.fqcdefecthandling.fqcdefecthandlingid"));

        RuleFor(x => x.HandlingCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.handlingcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingCode));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.DefectCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.defectcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectCode));

        RuleFor(x => x.DefectDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.defectdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectDescription));

        RuleFor(x => x.HandlingDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.handlingdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingDescription));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.correctiveaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.DefectImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.fqcdefecthandling.defectimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectImages));
    }
}
