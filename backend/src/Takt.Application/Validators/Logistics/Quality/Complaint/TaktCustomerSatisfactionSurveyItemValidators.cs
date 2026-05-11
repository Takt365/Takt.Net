// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurveyItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CustomerSatisfactionSurveyItem DTO 验证器（根据实体 TaktCustomerSatisfactionSurveyItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Complaint;

namespace Takt.Application.Validators.Logistics.Quality.Complaint;

/// <summary>
/// CustomerSatisfactionSurveyItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurveyItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktCustomerSatisfactionSurveyItemCreateDtoValidator : AbstractValidator<TaktCustomerSatisfactionSurveyItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCustomerSatisfactionSurveyItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CustomerSatisfactionSurveyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customersatisfactionsurveyitem.customersatisfactionsurveycode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.customersatisfactionsurveyitem.customersatisfactionsurveycode", 1, 50));

        RuleFor(x => x.CategoryType)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.customersatisfactionsurveyitem.categorytype"));

        RuleFor(x => x.ItemName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customersatisfactionsurveyitem.itemname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.customersatisfactionsurveyitem.itemname", 1, 200));

        RuleFor(x => x.ItemDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.itemdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemDescription));

        RuleFor(x => x.CustomerFeedback)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.customerfeedback", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerFeedback));

        RuleFor(x => x.ImprovementSuggestion)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.improvementsuggestion", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestion));

        RuleFor(x => x.FollowUpAction)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.followupaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.FollowUpAction));

        RuleFor(x => x.FollowUpStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.customersatisfactionsurveyitem.followupstatus"));
    }
}

/// <summary>
/// CustomerSatisfactionSurveyItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktCustomerSatisfactionSurveyItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>CustomerSatisfactionSurveyItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktCustomerSatisfactionSurveyItemUpdateDtoValidator : AbstractValidator<TaktCustomerSatisfactionSurveyItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCustomerSatisfactionSurveyItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktCustomerSatisfactionSurveyItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.CustomerSatisfactionSurveyItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.customersatisfactionsurveyitem.customersatisfactionsurveyitemid"));

        RuleFor(x => x.CustomerSatisfactionSurveyCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.customersatisfactionsurveycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerSatisfactionSurveyCode));

        RuleFor(x => x.ItemName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.itemname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemName));

        RuleFor(x => x.ItemDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.itemdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ItemDescription));

        RuleFor(x => x.CustomerFeedback)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.customerfeedback", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerFeedback));

        RuleFor(x => x.ImprovementSuggestion)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.improvementsuggestion", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementSuggestion));

        RuleFor(x => x.FollowUpAction)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurveyitem.followupaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.FollowUpAction));
    }
}
