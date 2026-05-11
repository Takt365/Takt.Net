// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintHandlingValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CustomerComplaintHandling DTO 验证器（根据实体 TaktCustomerComplaintHandling 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Complaint;

namespace Takt.Application.Validators.Logistics.Quality.Complaint;

/// <summary>
/// CustomerComplaintHandling创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Complaint.TaktCustomerComplaintHandling"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktCustomerComplaintHandlingCreateDtoValidator : AbstractValidator<TaktCustomerComplaintHandlingCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCustomerComplaintHandlingCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ComplaintHandlingCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customercomplainthandling.complainthandlingcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.customercomplainthandling.complainthandlingcode", 1, 50));

        RuleFor(x => x.ComplaintNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customercomplainthandling.complaintno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.customercomplainthandling.complaintno", 1, 50));

        RuleFor(x => x.HandlingStage)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.customercomplainthandling.handlingstage"));

        RuleFor(x => x.HandlingMethod)
            .InclusiveBetween(0, 6)
            .WithMessage(_validationMessages.FormatInvalid("entity.customercomplainthandling.handlingmethod"));

        RuleFor(x => x.HandlingDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customercomplainthandling.handlingdescription"))
            .Length(1, 2000).WithMessage(_validationMessages.LengthBetween("entity.customercomplainthandling.handlingdescription", 1, 2000));

        RuleFor(x => x.CauseAnalysis)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.causeanalysis", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CauseAnalysis));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.correctiveaction", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.PreventiveAction)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.preventiveaction", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.PreventiveAction));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.HandlingStatus)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.customercomplainthandling.handlingstatus"));

        RuleFor(x => x.CustomerFeedback)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.customerfeedback", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerFeedback));

        RuleFor(x => x.AttachmentPaths)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.attachmentpaths", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentPaths));
    }
}

/// <summary>
/// CustomerComplaintHandling更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktCustomerComplaintHandlingCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>CustomerComplaintHandlingId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktCustomerComplaintHandlingUpdateDtoValidator : AbstractValidator<TaktCustomerComplaintHandlingUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCustomerComplaintHandlingUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktCustomerComplaintHandlingCreateDtoValidator(validationMessages));

        RuleFor(x => x.CustomerComplaintHandlingId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.customercomplainthandling.customercomplainthandlingid"));

        RuleFor(x => x.ComplaintHandlingCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.complainthandlingcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ComplaintHandlingCode));

        RuleFor(x => x.ComplaintNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.complaintno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ComplaintNo));

        RuleFor(x => x.HandlingDescription)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.handlingdescription", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingDescription));

        RuleFor(x => x.CauseAnalysis)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.causeanalysis", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CauseAnalysis));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.correctiveaction", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.PreventiveAction)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.preventiveaction", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.PreventiveAction));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.CustomerFeedback)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.customerfeedback", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerFeedback));

        RuleFor(x => x.AttachmentPaths)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplainthandling.attachmentpaths", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentPaths));
    }
}
