// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurveyValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CustomerSatisfactionSurvey DTO 验证器（根据实体 TaktCustomerSatisfactionSurvey 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Complaint;

namespace Takt.Application.Validators.Logistics.Quality.Complaint;

/// <summary>
/// CustomerSatisfactionSurvey创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Complaint.TaktCustomerSatisfactionSurvey"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktCustomerSatisfactionSurveyCreateDtoValidator : AbstractValidator<TaktCustomerSatisfactionSurveyCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCustomerSatisfactionSurveyCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customersatisfactionsurvey.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.customersatisfactionsurvey.companycode"));

        RuleFor(x => x.CustomerSatisfactionSurveyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customersatisfactionsurvey.customersatisfactionsurveycode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.customersatisfactionsurvey.customersatisfactionsurveycode", 1, 50));

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customersatisfactionsurvey.customername"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.customersatisfactionsurvey.customername", 1, 200));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));

        RuleFor(x => x.SurveyMethod)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.customersatisfactionsurvey.surveymethod"));

        RuleFor(x => x.SurveyType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.customersatisfactionsurvey.surveytype"));

        RuleFor(x => x.SurveyPeriod)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.customersatisfactionsurvey.surveyperiod"));

        RuleFor(x => x.SurveyorBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.surveyorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SurveyorBy));

        RuleFor(x => x.CustomerContact)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customercontact", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerContact));

        RuleFor(x => x.CustomerPhone)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customerphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerPhone));

        RuleFor(x => x.OverallSatisfaction)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.customersatisfactionsurvey.overallsatisfaction"));

        RuleFor(x => x.CustomerPraise)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customerpraise", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerPraise));

        RuleFor(x => x.CustomerFeedback)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customerfeedback", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerFeedback));

        RuleFor(x => x.ImprovementPlan)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.improvementplan", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementPlan));

        RuleFor(x => x.SurveyStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.customersatisfactionsurvey.surveystatus"));

        RuleFor(x => x.FollowUpStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.customersatisfactionsurvey.followupstatus"));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}

/// <summary>
/// CustomerSatisfactionSurvey更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktCustomerSatisfactionSurveyCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>CustomerSatisfactionSurveyId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktCustomerSatisfactionSurveyUpdateDtoValidator : AbstractValidator<TaktCustomerSatisfactionSurveyUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCustomerSatisfactionSurveyUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktCustomerSatisfactionSurveyCreateDtoValidator(validationMessages));

        RuleFor(x => x.CustomerSatisfactionSurveyId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.customersatisfactionsurvey.customersatisfactionsurveyid"));

        RuleFor(x => x.CustomerSatisfactionSurveyCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customersatisfactionsurveycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerSatisfactionSurveyCode));

        RuleFor(x => x.CustomerName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customername", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerName));

        RuleFor(x => x.CustomerCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerCode));

        RuleFor(x => x.SurveyorBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.surveyorby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.SurveyorBy));

        RuleFor(x => x.CustomerContact)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customercontact", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerContact));

        RuleFor(x => x.CustomerPhone)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customerphone", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerPhone));

        RuleFor(x => x.CustomerPraise)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customerpraise", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerPraise));

        RuleFor(x => x.CustomerFeedback)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.customerfeedback", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerFeedback));

        RuleFor(x => x.ImprovementPlan)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.improvementplan", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementPlan));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customersatisfactionsurvey.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
