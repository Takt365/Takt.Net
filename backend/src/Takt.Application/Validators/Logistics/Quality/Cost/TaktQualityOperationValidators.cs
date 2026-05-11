// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityOperation DTO 验证器（根据实体 TaktQualityOperation 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Cost;

namespace Takt.Application.Validators.Logistics.Quality.Cost;

/// <summary>
/// QualityOperation创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityOperation"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktQualityOperationCreateDtoValidator : AbstractValidator<TaktQualityOperationCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQualityOperationCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityoperation.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.qualityoperation.plantcode"));

        RuleFor(x => x.QualityOperationCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityoperation.qualityoperationcode"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.qualityoperation.qualityoperationcode", 1, 30));

        RuleFor(x => x.OperationMonth)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityoperation.operationmonth"))
            .Length(1, 7).WithMessage(_validationMessages.LengthBetween("entity.qualityoperation.operationmonth", 1, 7));

        RuleFor(x => x.CustomerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.qualityoperation.customername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerName));

        RuleFor(x => x.DebitNoteNo)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityoperation.debitnoteno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.DebitNoteNo));

        RuleFor(x => x.Recorder)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityoperation.recorder", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.Recorder));

        RuleFor(x => x.CostCurrency)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.qualityoperation.costcurrency"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.qualityoperation.costcurrency", 1, 10));
    }
}

/// <summary>
/// QualityOperation更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktQualityOperationCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>QualityOperationId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktQualityOperationUpdateDtoValidator : AbstractValidator<TaktQualityOperationUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktQualityOperationUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktQualityOperationCreateDtoValidator(validationMessages));

        RuleFor(x => x.QualityOperationId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.qualityoperation.qualityoperationid"));

        RuleFor(x => x.QualityOperationCode)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityoperation.qualityoperationcode", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.QualityOperationCode));

        RuleFor(x => x.OperationMonth)
            .MaximumLength(7).WithMessage(_validationMessages.LengthMax("entity.qualityoperation.operationmonth", 7))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationMonth));

        RuleFor(x => x.CustomerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.qualityoperation.customername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerName));

        RuleFor(x => x.DebitNoteNo)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityoperation.debitnoteno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.DebitNoteNo));

        RuleFor(x => x.Recorder)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.qualityoperation.recorder", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.Recorder));

        RuleFor(x => x.CostCurrency)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.qualityoperation.costcurrency", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCurrency));
    }
}
