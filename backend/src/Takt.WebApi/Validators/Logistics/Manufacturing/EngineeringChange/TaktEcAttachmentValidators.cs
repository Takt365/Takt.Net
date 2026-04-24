// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcAttachmentValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EcAttachment DTO 验证器（根据实体 TaktEcAttachment 自动生成）
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
/// EcAttachment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEcAttachment"/> 字段对齐）。
/// </summary>
public class TaktEcAttachmentCreateDtoValidator : AbstractValidator<TaktEcAttachmentCreateDto>
{
    public TaktEcAttachmentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.AttachmentType)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecattachment.attachmenttype", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentType));

        RuleFor(x => x.DocNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecattachment.docno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DocNo));

        RuleFor(x => x.FileName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecattachment.filename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileName));

        RuleFor(x => x.AccessUrl)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.ecattachment.accessurl"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.ecattachment.accessurl", 1, 500));
    }
}

/// <summary>
/// EcAttachment更新 DTO 验证器。
/// </summary>
public class TaktEcAttachmentUpdateDtoValidator : AbstractValidator<TaktEcAttachmentUpdateDto>
{
    public TaktEcAttachmentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEcAttachmentCreateDtoValidator(localizer));

        RuleFor(x => x.EcAttachmentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.ecattachment.ecattachmentid"));

        RuleFor(x => x.AttachmentType)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecattachment.attachmenttype", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentType));

        RuleFor(x => x.DocNo)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecattachment.docno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DocNo));

        RuleFor(x => x.FileName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecattachment.filename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileName));

        RuleFor(x => x.AccessUrl)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.ecattachment.accessurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessUrl));
    }
}
