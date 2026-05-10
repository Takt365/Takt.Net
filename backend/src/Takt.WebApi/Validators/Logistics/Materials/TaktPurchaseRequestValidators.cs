// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Materials
// 文件名称：TaktPurchaseRequestValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PurchaseRequest DTO 验证器（根据实体 TaktPurchaseRequest 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Materials;

/// <summary>
/// PurchaseRequest创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Materials.TaktPurchaseRequest"/> 字段对齐）。
/// </summary>
public class TaktPurchaseRequestCreateDtoValidator : AbstractValidator<TaktPurchaseRequestCreateDto>
{
    public TaktPurchaseRequestCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaserequest.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaserequest.plantcode"));

        RuleFor(x => x.RequestCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaserequest.requestcode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaserequest.requestcode", 1, 50));

        RuleFor(x => x.RequestBy)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaserequest.requestby"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.purchaserequest.requestby", 1, 50));

        RuleFor(x => x.RequestStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaserequest.requeststatus"));

        RuleFor(x => x.ConvertedStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.purchaserequest.convertedstatus"));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequest.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequest.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.RequestReason)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequest.requestreason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestReason));
    }
}

/// <summary>
/// PurchaseRequest更新 DTO 验证器。
/// </summary>
public class TaktPurchaseRequestUpdateDtoValidator : AbstractValidator<TaktPurchaseRequestUpdateDto>
{
    public TaktPurchaseRequestUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPurchaseRequestCreateDtoValidator(localizer));

        RuleFor(x => x.PurchaseRequestId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.purchaserequest.purchaserequestid"));

        RuleFor(x => x.RequestCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequest.requestcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestCode));

        RuleFor(x => x.RequestBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequest.requestby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestBy));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequest.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequest.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.RequestReason)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.purchaserequest.requestreason", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.RequestReason));
    }
}
