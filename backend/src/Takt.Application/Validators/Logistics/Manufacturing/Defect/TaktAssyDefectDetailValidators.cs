// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Defect
// 文件名称：TaktAssyDefectDetailValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AssyDefectDetail DTO 验证器（根据实体 TaktAssyDefectDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Defect;

namespace Takt.Application.Validators.Logistics.Manufacturing.Defect;

/// <summary>
/// AssyDefectDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Defect.TaktAssyDefectDetail"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAssyDefectDetailCreateDtoValidator : AbstractValidator<TaktAssyDefectDetailCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAssyDefectDetailCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.assydefectdetail.prodordercode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.assydefectdetail.prodordercode", 1, 20));

        RuleFor(x => x.DefectCategory)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.defectcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectCategory));

        RuleFor(x => x.RandomCardNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.randomcardno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RandomCardNo));

        RuleFor(x => x.OccurrenceEngineering)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.occurrenceengineering", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OccurrenceEngineering));

        RuleFor(x => x.TestStep)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.teststep", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TestStep));

        RuleFor(x => x.DefectSymptom)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.defectsymptom", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectSymptom));

        RuleFor(x => x.DefectLocation)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.defectlocation", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLocation));

        RuleFor(x => x.DefectReason)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.defectreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectReason));

        RuleFor(x => x.RepairOperator)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.repairoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RepairOperator));
    }
}

/// <summary>
/// AssyDefectDetail更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAssyDefectDetailCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AssyDefectDetailId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAssyDefectDetailUpdateDtoValidator : AbstractValidator<TaktAssyDefectDetailUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAssyDefectDetailUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAssyDefectDetailCreateDtoValidator(validationMessages));

        RuleFor(x => x.AssyDefectDetailId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.assydefectdetail.assydefectdetailid"));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.DefectCategory)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.defectcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectCategory));

        RuleFor(x => x.RandomCardNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.randomcardno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RandomCardNo));

        RuleFor(x => x.OccurrenceEngineering)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.occurrenceengineering", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OccurrenceEngineering));

        RuleFor(x => x.TestStep)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.teststep", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TestStep));

        RuleFor(x => x.DefectSymptom)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.defectsymptom", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectSymptom));

        RuleFor(x => x.DefectLocation)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.defectlocation", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLocation));

        RuleFor(x => x.DefectReason)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.defectreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectReason));

        RuleFor(x => x.RepairOperator)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.assydefectdetail.repairoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RepairOperator));
    }
}
