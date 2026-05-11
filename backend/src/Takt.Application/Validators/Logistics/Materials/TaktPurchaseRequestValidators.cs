// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Materials
// 文件名称：TaktPurchaseRequestValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchaseRequest DTO 验证器（根据实体 TaktPurchaseRequest 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;

namespace Takt.Application.Validators.Logistics.Materials;

/// <summary>
/// PurchaseRequest创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchaseRequest"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPurchaseRequestCreateDtoValidator : AbstractValidator<TaktPurchaseRequestCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchaseRequestCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaserequest.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.purchaserequest.plantcode"));

        RuleFor(x => x.PurchaseRequestCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaserequest.purchaserequestcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchaserequest.purchaserequestcode", 1, 50));

        RuleFor(x => x.RequestBy)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.purchaserequest.requestby"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.purchaserequest.requestby", 1, 50));

        RuleFor(x => x.RequestStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.purchaserequest.requeststatus"));

        RuleFor(x => x.ConvertedStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.purchaserequest.convertedstatus"));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaserequest.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.purchaserequest.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.RequestReason)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.purchaserequest.requestreason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestReason));
    }
}

/// <summary>
/// PurchaseRequest更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPurchaseRequestCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PurchaseRequestId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPurchaseRequestUpdateDtoValidator : AbstractValidator<TaktPurchaseRequestUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPurchaseRequestUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPurchaseRequestCreateDtoValidator(validationMessages));

        RuleFor(x => x.PurchaseRequestId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.purchaserequest.purchaserequestid"));

        RuleFor(x => x.PurchaseRequestCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaserequest.purchaserequestcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseRequestCode));

        RuleFor(x => x.RequestBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaserequest.requestby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestBy));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.purchaserequest.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.purchaserequest.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.RequestReason)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.purchaserequest.requestreason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestReason));
    }
}
