// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaRepairDetailValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PcbaRepairDetail DTO 验证器（根据实体 TaktPcbaRepairDetail 自动生成）
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
/// PcbaRepairDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Defect.TaktPcbaRepairDetail"/> 字段对齐）。
/// </summary>
public class TaktPcbaRepairDetailCreateDtoValidator : AbstractValidator<TaktPcbaRepairDetailCreateDto>
{
    public TaktPcbaRepairDetailCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PcbaBoardType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.pcbaboardtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaBoardType));

        RuleFor(x => x.ProdLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.prodline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.CardNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.cardno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CardNo));

        RuleFor(x => x.DefectSymptom)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectsymptom", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectSymptom));

        RuleFor(x => x.DefectEngineering)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectengineering", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectEngineering));

        RuleFor(x => x.DefectReason)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectreason", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectReason));

        RuleFor(x => x.DefectResponsibility)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectresponsibility", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectResponsibility));

        RuleFor(x => x.DefectNature)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectnature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectNature));

        RuleFor(x => x.RepairOperator)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.repairoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RepairOperator));
    }
}

/// <summary>
/// PcbaRepairDetail更新 DTO 验证器。
/// </summary>
public class TaktPcbaRepairDetailUpdateDtoValidator : AbstractValidator<TaktPcbaRepairDetailUpdateDto>
{
    public TaktPcbaRepairDetailUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPcbaRepairDetailCreateDtoValidator(localizer));

        RuleFor(x => x.PcbaRepairDetailId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbarepairdetail.pcbarepairdetailid"));

        RuleFor(x => x.PcbaBoardType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.pcbaboardtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaBoardType));

        RuleFor(x => x.ProdLine)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.prodline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.CardNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.cardno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CardNo));

        RuleFor(x => x.DefectSymptom)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectsymptom", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectSymptom));

        RuleFor(x => x.DefectEngineering)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectengineering", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectEngineering));

        RuleFor(x => x.DefectReason)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectreason", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectReason));

        RuleFor(x => x.DefectResponsibility)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectresponsibility", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectResponsibility));

        RuleFor(x => x.DefectNature)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.defectnature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectNature));

        RuleFor(x => x.RepairOperator)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbarepairdetail.repairoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RepairOperator));
    }
}
