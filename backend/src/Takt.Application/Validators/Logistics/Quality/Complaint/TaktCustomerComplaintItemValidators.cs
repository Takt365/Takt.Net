// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintItemValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CustomerComplaintItem DTO 验证器（根据实体 TaktCustomerComplaintItem 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Complaint;

namespace Takt.Application.Validators.Logistics.Quality.Complaint;

/// <summary>
/// CustomerComplaintItem创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Complaint.TaktCustomerComplaintItem"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktCustomerComplaintItemCreateDtoValidator : AbstractValidator<TaktCustomerComplaintItemCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCustomerComplaintItemCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CustomerComplaintCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customercomplaintitem.customercomplaintcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.customercomplaintitem.customercomplaintcode", 1, 50));

        RuleFor(x => x.ProductCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.productcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductCode));

        RuleFor(x => x.ProductName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.productname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductName));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.ItemType)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.customercomplaintitem.itemtype"));

        RuleFor(x => x.DefectDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customercomplaintitem.defectdescription"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.customercomplaintitem.defectdescription", 1, 1000));

        RuleFor(x => x.DefectLevel)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.customercomplaintitem.defectlevel"))
            .Length(1, 2).WithMessage(_validationMessages.LengthBetween("entity.customercomplaintitem.defectlevel", 1, 2));

        RuleFor(x => x.CauseAnalysis)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.causeanalysis", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CauseAnalysis));

        RuleFor(x => x.ImprovementAction)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.improvementaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementAction));

        RuleFor(x => x.ImprovementResponsible)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.improvementresponsible", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementResponsible));

        RuleFor(x => x.ImprovementStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.customercomplaintitem.improvementstatus"));

        RuleFor(x => x.AttachmentPaths)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.attachmentpaths", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentPaths));
    }
}

/// <summary>
/// CustomerComplaintItem更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktCustomerComplaintItemCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>CustomerComplaintItemId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktCustomerComplaintItemUpdateDtoValidator : AbstractValidator<TaktCustomerComplaintItemUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCustomerComplaintItemUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktCustomerComplaintItemCreateDtoValidator(validationMessages));

        RuleFor(x => x.CustomerComplaintItemId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.customercomplaintitem.customercomplaintitemid"));

        RuleFor(x => x.CustomerComplaintCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.customercomplaintcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomerComplaintCode));

        RuleFor(x => x.ProductCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.productcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductCode));

        RuleFor(x => x.ProductName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.productname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductName));

        RuleFor(x => x.BatchNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.batchno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BatchNo));

        RuleFor(x => x.DefectDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.defectdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectDescription));

        RuleFor(x => x.DefectLevel)
            .MaximumLength(2).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.defectlevel", 2))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectLevel));

        RuleFor(x => x.CauseAnalysis)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.causeanalysis", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CauseAnalysis));

        RuleFor(x => x.ImprovementAction)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.improvementaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementAction));

        RuleFor(x => x.ImprovementResponsible)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.improvementresponsible", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ImprovementResponsible));

        RuleFor(x => x.AttachmentPaths)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.customercomplaintitem.attachmentpaths", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentPaths));
    }
}
