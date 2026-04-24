// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeAttachmentValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeAttachment DTO 验证器（根据实体 TaktEmployeeAttachment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeAttachment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeAttachment"/> 字段对齐）。
/// </summary>
public class TaktEmployeeAttachmentCreateDtoValidator : AbstractValidator<TaktEmployeeAttachmentCreateDto>
{
    public TaktEmployeeAttachmentCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.FileCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeeattachment.filecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeeattachment.filecode", 1, 50));

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeeattachment.filename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeeattachment.filename", 1, 200));

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeeattachment.filepath"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeeattachment.filepath", 1, 500));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeattachment.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.AttachmentType)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeeattachment.attachmenttype"));

        RuleFor(x => x.AttachmentDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeattachment.attachmentdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentDescription));
    }
}

/// <summary>
/// EmployeeAttachment更新 DTO 验证器。
/// </summary>
public class TaktEmployeeAttachmentUpdateDtoValidator : AbstractValidator<TaktEmployeeAttachmentUpdateDto>
{
    public TaktEmployeeAttachmentUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeAttachmentCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeAttachmentId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeeattachment.employeeattachmentid"));

        RuleFor(x => x.FileCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeattachment.filecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FileCode));

        RuleFor(x => x.FileName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeattachment.filename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileName));

        RuleFor(x => x.FilePath)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeattachment.filepath", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FilePath));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeattachment.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.AttachmentDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeeattachment.attachmentdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentDescription));
    }
}
