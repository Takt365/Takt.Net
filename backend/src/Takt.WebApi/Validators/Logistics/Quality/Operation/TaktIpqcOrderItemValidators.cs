// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderItemValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IpqcOrderItem DTO 验证器（根据实体 TaktIpqcOrderItem 自动生成）
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
/// IpqcOrderItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIpqcOrderItem"/> 字段对齐）。
/// </summary>
public class TaktIpqcOrderItemCreateDtoValidator : AbstractValidator<TaktIpqcOrderItemCreateDto>
{
    public TaktIpqcOrderItemCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ItemCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorderitem.itemcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ipqcorderitem.itemcode", 1, 50));

        RuleFor(x => x.ItemName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorderitem.itemname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ipqcorderitem.itemname", 1, 200));

        RuleFor(x => x.ItemType)
            .InclusiveBetween(0, 4)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ipqcorderitem.itemtype"));

        RuleFor(x => x.StandardValue)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.standardvalue", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardValue));

        RuleFor(x => x.UpperLimit)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.upperlimit", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.UpperLimit));

        RuleFor(x => x.LowerLimit)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.lowerlimit", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LowerLimit));

        RuleFor(x => x.InspectionTool)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.inspectiontool", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionTool));

        RuleFor(x => x.InspectionMethod)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.inspectionmethod", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionMethod));

        RuleFor(x => x.ActualValue)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.actualvalue", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ActualValue));

        RuleFor(x => x.InspectionResult)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ipqcorderitem.inspectionresult"));

        RuleFor(x => x.DefectDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.defectdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectDescription));

        RuleFor(x => x.InspectorBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.inspectorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectorBy));

        RuleFor(x => x.InspectionImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.inspectionimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionImages));
    }
}

/// <summary>
/// IpqcOrderItem更新 DTO 验证器。
/// </summary>
public class TaktIpqcOrderItemUpdateDtoValidator : AbstractValidator<TaktIpqcOrderItemUpdateDto>
{
    public TaktIpqcOrderItemUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktIpqcOrderItemCreateDtoValidator(localizer));

        RuleFor(x => x.IpqcOrderItemId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ipqcorderitem.ipqcorderitemid"));

        RuleFor(x => x.ItemCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.itemcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemCode));

        RuleFor(x => x.ItemName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.itemname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemName));

        RuleFor(x => x.StandardValue)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.standardvalue", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardValue));

        RuleFor(x => x.UpperLimit)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.upperlimit", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.UpperLimit));

        RuleFor(x => x.LowerLimit)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.lowerlimit", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LowerLimit));

        RuleFor(x => x.InspectionTool)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.inspectiontool", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionTool));

        RuleFor(x => x.InspectionMethod)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.inspectionmethod", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionMethod));

        RuleFor(x => x.ActualValue)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.actualvalue", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ActualValue));

        RuleFor(x => x.DefectDescription)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.defectdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectDescription));

        RuleFor(x => x.InspectorBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.inspectorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectorBy));

        RuleFor(x => x.InspectionImages)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ipqcorderitem.inspectionimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionImages));
    }
}
