// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaRepairDetailValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PcbaRepairDetail DTO 验证器（根据实体 TaktPcbaRepairDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Defect;

namespace Takt.Application.Validators.Logistics.Manufacturing.Defect;

/// <summary>
/// PcbaRepairDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Defect.TaktPcbaRepairDetail"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPcbaRepairDetailCreateDtoValidator : AbstractValidator<TaktPcbaRepairDetailCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPcbaRepairDetailCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbarepairdetail.prodordercode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbarepairdetail.prodordercode", 1, 20));

        RuleFor(x => x.PcbaBoardType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.pcbaboardtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaBoardType));

        RuleFor(x => x.ProdLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.prodline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.CardNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.cardno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CardNo));

        RuleFor(x => x.DefectSymptom)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectsymptom", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectSymptom));

        RuleFor(x => x.DefectEngineering)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectengineering", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectEngineering));

        RuleFor(x => x.DefectReason)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectreason", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectReason));

        RuleFor(x => x.DefectResponsibility)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectresponsibility", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectResponsibility));

        RuleFor(x => x.DefectNature)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectnature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectNature));

        RuleFor(x => x.RepairOperator)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.repairoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RepairOperator));
    }
}

/// <summary>
/// PcbaRepairDetail更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPcbaRepairDetailCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PcbaRepairDetailId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPcbaRepairDetailUpdateDtoValidator : AbstractValidator<TaktPcbaRepairDetailUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPcbaRepairDetailUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPcbaRepairDetailCreateDtoValidator(validationMessages));

        RuleFor(x => x.PcbaRepairDetailId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.pcbarepairdetail.pcbarepairdetailid"));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.PcbaBoardType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.pcbaboardtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaBoardType));

        RuleFor(x => x.ProdLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.prodline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.CardNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.cardno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CardNo));

        RuleFor(x => x.DefectSymptom)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectsymptom", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectSymptom));

        RuleFor(x => x.DefectEngineering)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectengineering", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectEngineering));

        RuleFor(x => x.DefectReason)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectreason", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectReason));

        RuleFor(x => x.DefectResponsibility)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectresponsibility", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectResponsibility));

        RuleFor(x => x.DefectNature)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.defectnature", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectNature));

        RuleFor(x => x.RepairOperator)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbarepairdetail.repairoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RepairOperator));
    }
}
