// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Defect
// 文件名称：TaktAssyDefectDetailValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AssyDefectDetail DTO 验证器（根据实体 TaktAssyDefectDetail 自动生成）
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
/// AssyDefectDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Defect.TaktAssyDefectDetail"/> 字段对齐）。
/// </summary>
public class TaktAssyDefectDetailCreateDtoValidator : AbstractValidator<TaktAssyDefectDetailCreateDto>
{
    public TaktAssyDefectDetailCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.DefectCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.defectcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectCategory));

        RuleFor(x => x.RandomCardNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.randomcardno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RandomCardNo));

        RuleFor(x => x.OccurrenceEngineering)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.occurrenceengineering", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OccurrenceEngineering));

        RuleFor(x => x.TestStep)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.teststep", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TestStep));

        RuleFor(x => x.DefectSymptom)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.defectsymptom", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectSymptom));

        RuleFor(x => x.DefectLocation)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.defectlocation", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLocation));

        RuleFor(x => x.DefectReason)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.defectreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectReason));

        RuleFor(x => x.RepairOperator)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.repairoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RepairOperator));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.assydefectdetail.status"));
    }
}

/// <summary>
/// AssyDefectDetail更新 DTO 验证器。
/// </summary>
public class TaktAssyDefectDetailUpdateDtoValidator : AbstractValidator<TaktAssyDefectDetailUpdateDto>
{
    public TaktAssyDefectDetailUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAssyDefectDetailCreateDtoValidator(localizer));

        RuleFor(x => x.AssyDefectDetailId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.assydefectdetail.assydefectdetailid"));

        RuleFor(x => x.DefectCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.defectcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectCategory));

        RuleFor(x => x.RandomCardNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.randomcardno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RandomCardNo));

        RuleFor(x => x.OccurrenceEngineering)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.occurrenceengineering", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OccurrenceEngineering));

        RuleFor(x => x.TestStep)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.teststep", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TestStep));

        RuleFor(x => x.DefectSymptom)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.defectsymptom", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectSymptom));

        RuleFor(x => x.DefectLocation)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.defectlocation", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLocation));

        RuleFor(x => x.DefectReason)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.defectreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectReason));

        RuleFor(x => x.RepairOperator)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.assydefectdetail.repairoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RepairOperator));
    }
}
