// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Operation
// 文件名称：TaktIqcDefectHandlingValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IqcDefectHandling DTO 验证器（根据实体 TaktIqcDefectHandling 自动生成）
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
/// IqcDefectHandling创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIqcDefectHandling"/> 字段对齐）。
/// </summary>
public class TaktIqcDefectHandlingCreateDtoValidator : AbstractValidator<TaktIqcDefectHandlingCreateDto>
{
    public TaktIqcDefectHandlingCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.HandlingCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcdefecthandling.handlingcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcdefecthandling.handlingcode", 1, 50));

        RuleFor(x => x.OrderCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcdefecthandling.ordercode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcdefecthandling.ordercode", 1, 50));

        RuleFor(x => x.DefectType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.iqcdefecthandling.defecttype"));

        RuleFor(x => x.DefectCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcdefecthandling.defectcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcdefecthandling.defectcode", 1, 50));

        RuleFor(x => x.DefectDescription)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcdefecthandling.defectdescription"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.iqcdefecthandling.defectdescription", 1, 500));

        RuleFor(x => x.HandlingMethod)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.iqcdefecthandling.handlingmethod"));

        RuleFor(x => x.HandlingDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.handlingdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingDescription));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.HandlingStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.iqcdefecthandling.handlingstatus"));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.correctiveaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.DefectImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.defectimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectImages));
    }
}

/// <summary>
/// IqcDefectHandling更新 DTO 验证器。
/// </summary>
public class TaktIqcDefectHandlingUpdateDtoValidator : AbstractValidator<TaktIqcDefectHandlingUpdateDto>
{
    public TaktIqcDefectHandlingUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktIqcDefectHandlingCreateDtoValidator(localizer));

        RuleFor(x => x.IqcDefectHandlingId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.iqcdefecthandling.iqcdefecthandlingid"));

        RuleFor(x => x.HandlingCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.handlingcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingCode));

        RuleFor(x => x.OrderCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.ordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OrderCode));

        RuleFor(x => x.DefectCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.defectcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectCode));

        RuleFor(x => x.DefectDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.defectdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectDescription));

        RuleFor(x => x.HandlingDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.handlingdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingDescription));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.correctiveaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.DefectImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.iqcdefecthandling.defectimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectImages));
    }
}
