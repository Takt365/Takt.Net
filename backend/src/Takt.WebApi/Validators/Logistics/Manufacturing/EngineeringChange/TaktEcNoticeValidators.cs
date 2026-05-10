// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcNoticeValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EcNotice DTO 验证器（根据实体 TaktEcNotice 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// EcNotice创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEcNotice"/> 字段对齐）。
/// </summary>
public class TaktEcNoticeCreateDtoValidator : AbstractValidator<TaktEcNoticeCreateDto>
{
    public TaktEcNoticeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ecnotice.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ecnotice.plantcode"));

        RuleFor(x => x.NoticeNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ecnotice.noticeno"))
            .Length(1, 30).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ecnotice.noticeno", 1, 30));

        RuleFor(x => x.EcnNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ecnotice.ecnno"))
            .Length(1, 30).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ecnotice.ecnno", 1, 30));

        RuleFor(x => x.EcnTitle)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.ecntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnTitle));

        RuleFor(x => x.NoticeDeptCodes)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.noticedeptcodes", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeDeptCodes));

        RuleFor(x => x.NoticeDeptNames)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.noticedeptnames", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeDeptNames));

        RuleFor(x => x.NotifierName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.notifiername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NotifierName));

        RuleFor(x => x.NoticeMethod)
            .InclusiveBetween(1, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ecnotice.noticemethod"));

        RuleFor(x => x.NoticeStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.ecnotice.noticestatus"));

        RuleFor(x => x.ConfirmerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.confirmername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ConfirmerName));

        RuleFor(x => x.ConfirmComment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.confirmcomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ConfirmComment));
    }
}

/// <summary>
/// EcNotice更新 DTO 验证器。
/// </summary>
public class TaktEcNoticeUpdateDtoValidator : AbstractValidator<TaktEcNoticeUpdateDto>
{
    public TaktEcNoticeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEcNoticeCreateDtoValidator(localizer));

        RuleFor(x => x.EcNoticeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ecnotice.ecnoticeid"));

        RuleFor(x => x.NoticeNo)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.noticeno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeNo));

        RuleFor(x => x.EcnNo)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.ecnno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNo));

        RuleFor(x => x.EcnTitle)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.ecntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnTitle));

        RuleFor(x => x.NoticeDeptCodes)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.noticedeptcodes", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeDeptCodes));

        RuleFor(x => x.NoticeDeptNames)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.noticedeptnames", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeDeptNames));

        RuleFor(x => x.NotifierName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.notifiername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NotifierName));

        RuleFor(x => x.ConfirmerName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.confirmername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ConfirmerName));

        RuleFor(x => x.ConfirmComment)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecnotice.confirmcomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ConfirmComment));
    }
}
