// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDetailValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EcDetail DTO 验证器（根据实体 TaktEcDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

namespace Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// EcDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEcDetail"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEcDetailCreateDtoValidator : AbstractValidator<TaktEcDetailCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcDetailCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.EcnNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecdetail.ecnno"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.ecdetail.ecnno", 1, 10));

        RuleFor(x => x.EcnModel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecdetail.ecnmodel"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ecdetail.ecnmodel", 1, 50));

        RuleFor(x => x.EcnBomItem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnbomitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomItem));

        RuleFor(x => x.EcnBomSubItem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnbomsubitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomSubItem));

        RuleFor(x => x.EcnBomNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnbomno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomNo));

        RuleFor(x => x.EcnChange)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnchange", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnChange));

        RuleFor(x => x.EcnLocal)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnlocal", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnLocal));

        RuleFor(x => x.EcnNote)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnnote", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNote));

        RuleFor(x => x.EcnProcess)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnprocess", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnProcess));

        RuleFor(x => x.EcnOldItem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnolditem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldItem));

        RuleFor(x => x.EcnOldText)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnoldtext", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldText));

        RuleFor(x => x.EcnOldSet)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnoldset", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldSet));

        RuleFor(x => x.EcnNewItem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnnewitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewItem));

        RuleFor(x => x.EcnNewText)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnnewtext", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewText));

        RuleFor(x => x.EcnNewSet)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnnewset", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewSet));

        RuleFor(x => x.IsProcurement)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.ecdetail.isprocurement"));

        RuleFor(x => x.IsCheck)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.ecdetail.ischeck"));

        RuleFor(x => x.EcnWarehouse)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnwarehouse", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnWarehouse));

        RuleFor(x => x.IsEndOfLine)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.ecdetail.isendofline"));
    }
}

/// <summary>
/// EcDetail更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEcDetailCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EcDetailId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEcDetailUpdateDtoValidator : AbstractValidator<TaktEcDetailUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcDetailUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEcDetailCreateDtoValidator(validationMessages));

        RuleFor(x => x.EcDetailId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ecdetail.ecdetailid"));

        RuleFor(x => x.EcnNo)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnno", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNo));

        RuleFor(x => x.EcnModel)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnmodel", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnModel));

        RuleFor(x => x.EcnBomItem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnbomitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomItem));

        RuleFor(x => x.EcnBomSubItem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnbomsubitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomSubItem));

        RuleFor(x => x.EcnBomNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnbomno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnBomNo));

        RuleFor(x => x.EcnChange)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnchange", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnChange));

        RuleFor(x => x.EcnLocal)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnlocal", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnLocal));

        RuleFor(x => x.EcnNote)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnnote", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNote));

        RuleFor(x => x.EcnProcess)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnprocess", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnProcess));

        RuleFor(x => x.EcnOldItem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnolditem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldItem));

        RuleFor(x => x.EcnOldText)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnoldtext", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldText));

        RuleFor(x => x.EcnOldSet)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnoldset", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnOldSet));

        RuleFor(x => x.EcnNewItem)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnnewitem", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewItem));

        RuleFor(x => x.EcnNewText)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnnewtext", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewText));

        RuleFor(x => x.EcnNewSet)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnnewset", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNewSet));

        RuleFor(x => x.EcnWarehouse)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdetail.ecnwarehouse", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnWarehouse));
    }
}
