// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Defect
// 文件名称：TaktPcbaInspectionDetailValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PcbaInspectionDetail DTO 验证器（根据实体 TaktPcbaInspectionDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Defect;

namespace Takt.Application.Validators.Logistics.Manufacturing.Defect;

/// <summary>
/// PcbaInspectionDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Defect.TaktPcbaInspectionDetail"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPcbaInspectionDetailCreateDtoValidator : AbstractValidator<TaktPcbaInspectionDetailCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPcbaInspectionDetailCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbainspectiondetail.prodordercode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbainspectiondetail.prodordercode", 1, 20));

        RuleFor(x => x.PcbaBoardType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.pcbaboardtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaBoardType));

        RuleFor(x => x.VisualInspectionLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.visualinspectionline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.VisualInspectionLine));

        RuleFor(x => x.AoiLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.aoiline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AoiLine));

        RuleFor(x => x.ShiftNo)
            .InclusiveBetween(1, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.pcbainspectiondetail.shiftno"));

        RuleFor(x => x.InspectorName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.inspectorname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectorName));

        RuleFor(x => x.InspectionStatus)
            .InclusiveBetween(1, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.pcbainspectiondetail.inspectionstatus"));

        RuleFor(x => x.ProdLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.prodline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.HandPlacement)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.handplacement", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.HandPlacement));

        RuleFor(x => x.SerialNumber)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.serialnumber", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNumber));

        RuleFor(x => x.Content)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.content", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Content));

        RuleFor(x => x.DefectLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.defectlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLocation));
    }
}

/// <summary>
/// PcbaInspectionDetail更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPcbaInspectionDetailCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PcbaInspectionDetailId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPcbaInspectionDetailUpdateDtoValidator : AbstractValidator<TaktPcbaInspectionDetailUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPcbaInspectionDetailUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPcbaInspectionDetailCreateDtoValidator(validationMessages));

        RuleFor(x => x.PcbaInspectionDetailId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.pcbainspectiondetail.pcbainspectiondetailid"));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.PcbaBoardType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.pcbaboardtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbaBoardType));

        RuleFor(x => x.VisualInspectionLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.visualinspectionline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.VisualInspectionLine));

        RuleFor(x => x.AoiLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.aoiline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AoiLine));

        RuleFor(x => x.InspectorName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.inspectorname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectorName));

        RuleFor(x => x.ProdLine)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.prodline", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.HandPlacement)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.handplacement", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.HandPlacement));

        RuleFor(x => x.SerialNumber)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.serialnumber", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNumber));

        RuleFor(x => x.Content)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.content", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Content));

        RuleFor(x => x.DefectLocation)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.pcbainspectiondetail.defectlocation", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLocation));
    }
}
