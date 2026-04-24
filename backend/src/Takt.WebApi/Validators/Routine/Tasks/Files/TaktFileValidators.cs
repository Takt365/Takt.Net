// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Routine.Tasks.Files
// 文件名称：TaktFileValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：File DTO 验证器（根据实体 TaktFile 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.Files;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Routine.Tasks.Files;

/// <summary>
/// File创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.Files.TaktFile"/> 字段对齐）。
/// </summary>
public class TaktFileCreateDtoValidator : AbstractValidator<TaktFileCreateDto>
{
    public TaktFileCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.FileCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.file.filecode"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.file.filecode", 1, 50));

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.file.filename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.file.filename", 1, 200));

        RuleFor(x => x.FileOriginalName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.file.fileoriginalname"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.file.fileoriginalname", 1, 200));

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.file.filepath"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.file.filepath", 1, 500));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.FileExtension)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.fileextension", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FileExtension));

        RuleFor(x => x.FileHash)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filehash", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.FileHash));

        RuleFor(x => x.FileCategory)
            .InclusiveBetween(0, 5)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.file.filecategory"));

        RuleFor(x => x.StorageType)
            .InclusiveBetween(0, 3)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.file.storagetype"));

        RuleFor(x => x.StorageConfig)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.storageconfig", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.StorageConfig));

        RuleFor(x => x.AccessUrl)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.accessurl", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessUrl));

        RuleFor(x => x.FileStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.file.filestatus"));

        RuleFor(x => x.IsPublic)
            .InclusiveBetween(0, 1)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.file.ispublic"));

        RuleFor(x => x.AccessPermissionConfig)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.accesspermissionconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessPermissionConfig));

        RuleFor(x => x.FileDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FileDescription));

        RuleFor(x => x.FileTags)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filetags", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileTags));

        RuleFor(x => x.IpAddress)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.ipaddress", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IpAddress));

        RuleFor(x => x.Location)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.location", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Location));
    }
}

/// <summary>
/// File更新 DTO 验证器。
/// </summary>
public class TaktFileUpdateDtoValidator : AbstractValidator<TaktFileUpdateDto>
{
    public TaktFileUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktFileCreateDtoValidator(localizer));

        RuleFor(x => x.FileId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.file.fileid"));

        RuleFor(x => x.FileCode)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FileCode));

        RuleFor(x => x.FileName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileName));

        RuleFor(x => x.FileOriginalName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.fileoriginalname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileOriginalName));

        RuleFor(x => x.FilePath)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filepath", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FilePath));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.FileExtension)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.fileextension", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FileExtension));

        RuleFor(x => x.FileHash)
            .MaximumLength(64).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filehash", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.FileHash));

        RuleFor(x => x.StorageConfig)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.storageconfig", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.StorageConfig));

        RuleFor(x => x.AccessUrl)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.accessurl", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessUrl));

        RuleFor(x => x.AccessPermissionConfig)
            .MaximumLength(2000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.accesspermissionconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessPermissionConfig));

        RuleFor(x => x.FileDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FileDescription));

        RuleFor(x => x.FileTags)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.filetags", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileTags));

        RuleFor(x => x.IpAddress)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.ipaddress", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IpAddress));

        RuleFor(x => x.Location)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.file.location", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Location));
    }
}
