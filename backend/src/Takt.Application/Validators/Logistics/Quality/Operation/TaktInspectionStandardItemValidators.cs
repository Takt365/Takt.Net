// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktInspectionStandardItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：InspectionStandardItem DTO 验证器（根据实体 TaktInspectionStandardItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// InspectionStandardItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktInspectionStandardItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktInspectionStandardItemCreateDtoValidator : AbstractValidator<TaktInspectionStandardItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktInspectionStandardItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ItemCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.itemcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.itemcode", 1, 50));

        RuleFor(x => x.ItemName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.itemname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.itemname", 1, 200));

        RuleFor(x => x.ItemType)
            .InclusiveBetween(0, 6)
            .WithMessage(_validationMessages.FormatInvalid("entity.inspectionstandarditem.itemtype"));

        RuleFor(x => x.DefectLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.defectlevel"))
            .Length(1, 2).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.defectlevel", 1, 2));

        RuleFor(x => x.InspectionMode)
            .InclusiveBetween(1, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.inspectionstandarditem.inspectionmode"));

        RuleFor(x => x.StandardValue)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.standardvalue"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.standardvalue", 1, 500));

        RuleFor(x => x.UpperLimit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.upperlimit"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.upperlimit", 1, 100));

        RuleFor(x => x.LowerLimit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.lowerlimit"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.lowerlimit", 1, 100));

        RuleFor(x => x.InspectionTool)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.inspectiontool"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.inspectiontool", 1, 200));

        RuleFor(x => x.InspectionMethodDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.inspectionmethoddescription"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.inspectionmethoddescription", 1, 500));

        RuleFor(x => x.AcceptanceCriteria)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.acceptancecriteria"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.acceptancecriteria", 1, 50));

        RuleFor(x => x.RejectionCriteria)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandarditem.rejectioncriteria"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandarditem.rejectioncriteria", 1, 50));

        RuleFor(x => x.IsQualifiedBasis)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.inspectionstandarditem.isqualifiedbasis"));
    }
}

/// <summary>
/// InspectionStandardItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktInspectionStandardItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>InspectionStandardItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktInspectionStandardItemUpdateDtoValidator : AbstractValidator<TaktInspectionStandardItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktInspectionStandardItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktInspectionStandardItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.InspectionStandardItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.inspectionstandarditem.inspectionstandarditemid"));

        RuleFor(x => x.ItemCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.itemcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemCode));

        RuleFor(x => x.ItemName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.itemname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemName));

        RuleFor(x => x.DefectLevel)
            .MaximumLength(2).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.defectlevel", 2))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLevel));

        RuleFor(x => x.StandardValue)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.standardvalue", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardValue));

        RuleFor(x => x.UpperLimit)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.upperlimit", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.UpperLimit));

        RuleFor(x => x.LowerLimit)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.lowerlimit", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.LowerLimit));

        RuleFor(x => x.InspectionTool)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.inspectiontool", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionTool));

        RuleFor(x => x.InspectionMethodDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.inspectionmethoddescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionMethodDescription));

        RuleFor(x => x.AcceptanceCriteria)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.acceptancecriteria", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AcceptanceCriteria));

        RuleFor(x => x.RejectionCriteria)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.inspectionstandarditem.rejectioncriteria", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RejectionCriteria));
    }
}
