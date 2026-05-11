// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluationValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SupplierEvaluation DTO 验证器（根据实体 TaktSupplierEvaluation 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Complaint;

namespace Takt.Application.Validators.Logistics.Quality.Complaint;

/// <summary>
/// SupplierEvaluation创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Complaint.TaktSupplierEvaluation"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSupplierEvaluationCreateDtoValidator : AbstractValidator<TaktSupplierEvaluationCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSupplierEvaluationCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.supplierevaluation.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluation.companycode"));

        RuleFor(x => x.SupplierEvaluationCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.supplierevaluation.supplierevaluationcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.supplierevaluation.supplierevaluationcode", 1, 50));

        RuleFor(x => x.SupplierName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.supplierevaluation.suppliername"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.supplierevaluation.suppliername", 1, 200));

        RuleFor(x => x.SupplierCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.suppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierCode));

        RuleFor(x => x.EvaluationPeriod)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluation.evaluationperiod"));

        RuleFor(x => x.EvaluationType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluation.evaluationtype"));

        RuleFor(x => x.EvaluatorBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.evaluatorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluatorBy));

        RuleFor(x => x.EvaluationDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.evaluationdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationDept));

        RuleFor(x => x.OverallRating)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluation.overallrating"));

        RuleFor(x => x.MainStrengths)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.mainstrengths", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MainStrengths));

        RuleFor(x => x.MainIssues)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.mainissues", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MainIssues));

        RuleFor(x => x.ImprovementRequirements)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.improvementrequirements", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementRequirements));

        RuleFor(x => x.EvaluationConclusion)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluation.evaluationconclusion"));

        RuleFor(x => x.EvaluationStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluation.evaluationstatus"));

        RuleFor(x => x.RectificationStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluation.rectificationstatus"));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}

/// <summary>
/// SupplierEvaluation更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSupplierEvaluationCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SupplierEvaluationId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSupplierEvaluationUpdateDtoValidator : AbstractValidator<TaktSupplierEvaluationUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSupplierEvaluationUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSupplierEvaluationCreateDtoValidator(validationMessages));

        RuleFor(x => x.SupplierEvaluationId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.supplierevaluation.supplierevaluationid"));

        RuleFor(x => x.SupplierEvaluationCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.supplierevaluationcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierEvaluationCode));

        RuleFor(x => x.SupplierName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.suppliername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierName));

        RuleFor(x => x.SupplierCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.suppliercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierCode));

        RuleFor(x => x.EvaluatorBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.evaluatorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluatorBy));

        RuleFor(x => x.EvaluationDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.evaluationdept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationDept));

        RuleFor(x => x.MainStrengths)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.mainstrengths", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MainStrengths));

        RuleFor(x => x.MainIssues)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.mainissues", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.MainIssues));

        RuleFor(x => x.ImprovementRequirements)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.improvementrequirements", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementRequirements));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.supplierevaluation.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
