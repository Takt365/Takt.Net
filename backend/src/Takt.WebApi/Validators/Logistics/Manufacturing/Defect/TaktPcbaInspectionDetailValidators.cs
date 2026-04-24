// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaInspectionDetailValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PcbaInspectionDetail DTO 验证器（根据实体 TaktPcbaInspectionDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Defect;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Defect;

/// <summary>
/// PcbaInspectionDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Defect.TaktPcbaInspectionDetail"/> 字段对齐）。
/// </summary>
public class TaktPcbaInspectionDetailCreateDtoValidator : AbstractValidator<TaktPcbaInspectionDetailCreateDto>
{
    public TaktPcbaInspectionDetailCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PcbaBoardType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.pcbaboardtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaBoardType));

        RuleFor(x => x.VisualInspectionLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.visualinspectionline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.VisualInspectionLine));

        RuleFor(x => x.AoiLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.aoiline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AoiLine));

        RuleFor(x => x.ShiftNo)
            .InclusiveBetween(1, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.pcbainspectiondetail.shiftno"));

        RuleFor(x => x.InspectorName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.inspectorname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectorName));

        RuleFor(x => x.InspectionStatus)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.inspectionstatus", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionStatus));

        RuleFor(x => x.ProdLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.prodline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.HandPlacement)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.handplacement", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.HandPlacement));

        RuleFor(x => x.SerialNumber)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.serialnumber", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNumber));

        RuleFor(x => x.Content)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.content", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Content));

        RuleFor(x => x.DefectLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.defectlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLocation));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.pcbainspectiondetail.status"));
    }
}

/// <summary>
/// PcbaInspectionDetail更新 DTO 验证器。
/// </summary>
public class TaktPcbaInspectionDetailUpdateDtoValidator : AbstractValidator<TaktPcbaInspectionDetailUpdateDto>
{
    public TaktPcbaInspectionDetailUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPcbaInspectionDetailCreateDtoValidator(localizer));

        RuleFor(x => x.PcbaInspectionDetailId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbainspectiondetail.pcbainspectiondetailid"));

        RuleFor(x => x.PcbaBoardType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.pcbaboardtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaBoardType));

        RuleFor(x => x.VisualInspectionLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.visualinspectionline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.VisualInspectionLine));

        RuleFor(x => x.AoiLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.aoiline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AoiLine));

        RuleFor(x => x.InspectorName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.inspectorname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectorName));

        RuleFor(x => x.InspectionStatus)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.inspectionstatus", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionStatus));

        RuleFor(x => x.ProdLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.prodline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.HandPlacement)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.handplacement", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.HandPlacement));

        RuleFor(x => x.SerialNumber)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.serialnumber", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNumber));

        RuleFor(x => x.Content)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.content", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Content));

        RuleFor(x => x.DefectLocation)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbainspectiondetail.defectlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLocation));
    }
}
