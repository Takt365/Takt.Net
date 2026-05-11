// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluationItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：SupplierEvaluationItem DTO 验证器（根据实体 TaktSupplierEvaluationItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Complaint;

namespace Takt.Application.Validators.Logistics.Quality.Complaint;

/// <summary>
/// SupplierEvaluationItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Complaint.TaktSupplierEvaluationItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktSupplierEvaluationItemCreateDtoValidator : AbstractValidator<TaktSupplierEvaluationItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSupplierEvaluationItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.SupplierEvaluationCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.supplierevaluationitem.supplierevaluationcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.supplierevaluationitem.supplierevaluationcode", 1, 50));

        RuleFor(x => x.CategoryType)
            .InclusiveBetween(0, 6)
            .WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluationitem.categorytype"));

        RuleFor(x => x.ItemName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.supplierevaluationitem.itemname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.supplierevaluationitem.itemname", 1, 200));

        RuleFor(x => x.ItemDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.itemdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemDescription));

        RuleFor(x => x.ScoringStandard)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.scoringstandard", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ScoringStandard));

        RuleFor(x => x.EvaluationComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.evaluationcomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationComment));

        RuleFor(x => x.ExistingIssues)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.existingissues", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ExistingIssues));

        RuleFor(x => x.ImprovementRequirement)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.improvementrequirement", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementRequirement));

        RuleFor(x => x.RectificationRequired)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluationitem.rectificationrequired"));

        RuleFor(x => x.RectificationStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.supplierevaluationitem.rectificationstatus"));
    }
}

/// <summary>
/// SupplierEvaluationItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktSupplierEvaluationItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>SupplierEvaluationItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktSupplierEvaluationItemUpdateDtoValidator : AbstractValidator<TaktSupplierEvaluationItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktSupplierEvaluationItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktSupplierEvaluationItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.SupplierEvaluationItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.supplierevaluationitem.supplierevaluationitemid"));

        RuleFor(x => x.SupplierEvaluationCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.supplierevaluationcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SupplierEvaluationCode));

        RuleFor(x => x.ItemName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.itemname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemName));

        RuleFor(x => x.ItemDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.itemdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemDescription));

        RuleFor(x => x.ScoringStandard)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.scoringstandard", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ScoringStandard));

        RuleFor(x => x.EvaluationComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.evaluationcomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.EvaluationComment));

        RuleFor(x => x.ExistingIssues)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.existingissues", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ExistingIssues));

        RuleFor(x => x.ImprovementRequirement)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.supplierevaluationitem.improvementrequirement", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementRequirement));
    }
}
