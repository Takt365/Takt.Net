// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktInspectionStandardValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：InspectionStandard DTO 验证器（根据实体 TaktInspectionStandard 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// InspectionStandard创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktInspectionStandard"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktInspectionStandardCreateDtoValidator : AbstractValidator<TaktInspectionStandardCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktInspectionStandardCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandard.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.inspectionstandard.plantcode"));

        RuleFor(x => x.StandardCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandard.standardcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandard.standardcode", 1, 50));

        RuleFor(x => x.StandardName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandard.standardname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandard.standardname", 1, 200));

        RuleFor(x => x.InspectionType)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.inspectionstandard.inspectiontype"));

        RuleFor(x => x.MaterialCategoryCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandard.materialcategorycode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandard.materialcategorycode", 1, 50));

        RuleFor(x => x.MaterialCategoryName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.inspectionstandard.materialcategoryname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.inspectionstandard.materialcategoryname", 1, 200));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.SamplingSchemeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.samplingschemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeName));

        RuleFor(x => x.IsEnabled)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.inspectionstandard.isenabled"));

        RuleFor(x => x.StandardStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.inspectionstandard.standardstatus"));

        RuleFor(x => x.StandardDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.standarddescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardDescription));
    }
}

/// <summary>
/// InspectionStandard更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktInspectionStandardCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>InspectionStandardId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktInspectionStandardUpdateDtoValidator : AbstractValidator<TaktInspectionStandardUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktInspectionStandardUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktInspectionStandardCreateDtoValidator(validationMessages));

        RuleFor(x => x.InspectionStandardId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.inspectionstandard.inspectionstandardid"));

        RuleFor(x => x.StandardCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.standardcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardCode));

        RuleFor(x => x.StandardName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.standardname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardName));

        RuleFor(x => x.MaterialCategoryCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.materialcategorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCategoryCode));

        RuleFor(x => x.MaterialCategoryName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.materialcategoryname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCategoryName));

        RuleFor(x => x.SamplingSchemeCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.samplingschemecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeCode));

        RuleFor(x => x.SamplingSchemeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.samplingschemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingSchemeName));

        RuleFor(x => x.StandardDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.inspectionstandard.standarddescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.StandardDescription));
    }
}
