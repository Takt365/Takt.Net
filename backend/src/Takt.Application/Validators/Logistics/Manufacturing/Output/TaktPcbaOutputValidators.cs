// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktPcbaOutputValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PcbaOutput DTO 验证器（根据实体 TaktPcbaOutput 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;

namespace Takt.Application.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// PcbaOutput创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktPcbaOutput"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPcbaOutputCreateDtoValidator : AbstractValidator<TaktPcbaOutputCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPcbaOutputCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutput.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.pcbaoutput.plantcode"));

        RuleFor(x => x.ProdCategory)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutput.prodcategory"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutput.prodcategory", 1, 20));

        RuleFor(x => x.ProdLine)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutput.prodline"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutput.prodline", 1, 20));

        RuleFor(x => x.ShiftNo)
            .InclusiveBetween(1, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.pcbaoutput.shiftno"));

        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutput.prodordercode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutput.prodordercode", 1, 20));

        RuleFor(x => x.ModelCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutput.modelcode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutput.modelcode", 1, 20));

        RuleFor(x => x.BatchNo)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutput.batchno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutput.materialcode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutput.materialcode", 1, 20));
    }
}

/// <summary>
/// PcbaOutput更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPcbaOutputCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PcbaOutputId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPcbaOutputUpdateDtoValidator : AbstractValidator<TaktPcbaOutputUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPcbaOutputUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPcbaOutputCreateDtoValidator(validationMessages));

        RuleFor(x => x.PcbaOutputId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.pcbaoutput.pcbaoutputid"));

        RuleFor(x => x.ProdCategory)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutput.prodcategory", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdCategory));

        RuleFor(x => x.ProdLine)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutput.prodline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutput.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.ModelCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutput.modelcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ModelCode));

        RuleFor(x => x.BatchNo)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutput.batchno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutput.materialcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));
    }
}
