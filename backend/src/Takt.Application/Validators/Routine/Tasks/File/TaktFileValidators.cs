// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Tasks.File
// 文件名称：TaktFileValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：File DTO 验证器（根据实体 TaktFile 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Tasks.File;

namespace Takt.Application.Validators.Routine.Tasks.File;

/// <summary>
/// File创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Tasks.File.TaktFile"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFileCreateDtoValidator : AbstractValidator<TaktFileCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFileCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.FileCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.file.filecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.file.filecode", 1, 50));

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.file.filename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.file.filename", 1, 200));

        RuleFor(x => x.FileOriginalName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.file.fileoriginalname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.file.fileoriginalname", 1, 200));

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.file.filepath"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.file.filepath", 1, 500));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.file.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.FileExtension)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.file.fileextension", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FileExtension));

        RuleFor(x => x.FileHash)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.file.filehash", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.FileHash));

        RuleFor(x => x.FileCategory)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.file.filecategory"));

        RuleFor(x => x.StorageType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.file.storagetype"));

        RuleFor(x => x.StorageConfig)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.file.storageconfig", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.StorageConfig));

        RuleFor(x => x.AccessUrl)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.file.accessurl", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessUrl));

        RuleFor(x => x.FileStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.file.filestatus"));

        RuleFor(x => x.IsPublic)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.file.ispublic"));

        RuleFor(x => x.AccessPermissionConfig)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.file.accesspermissionconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessPermissionConfig));

        RuleFor(x => x.FileDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.file.filedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FileDescription));

        RuleFor(x => x.FileTags)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.file.filetags", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileTags));

        RuleFor(x => x.IpAddress)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.file.ipaddress", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IpAddress));

        RuleFor(x => x.Location)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.file.location", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Location));
    }
}

/// <summary>
/// File更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFileCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FileId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFileUpdateDtoValidator : AbstractValidator<TaktFileUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFileUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFileCreateDtoValidator(validationMessages));

        RuleFor(x => x.FileId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.file.fileid"));

        RuleFor(x => x.FileCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.file.filecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FileCode));

        RuleFor(x => x.FileName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.file.filename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileName));

        RuleFor(x => x.FileOriginalName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.file.fileoriginalname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileOriginalName));

        RuleFor(x => x.FilePath)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.file.filepath", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FilePath));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.file.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.FileExtension)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.file.fileextension", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FileExtension));

        RuleFor(x => x.FileHash)
            .MaximumLength(64).WithMessage(_validationMessages.LengthMax("entity.file.filehash", 64))
            .When(x => !string.IsNullOrWhiteSpace(x.FileHash));

        RuleFor(x => x.StorageConfig)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.file.storageconfig", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.StorageConfig));

        RuleFor(x => x.AccessUrl)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.file.accessurl", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessUrl));

        RuleFor(x => x.AccessPermissionConfig)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.file.accesspermissionconfig", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessPermissionConfig));

        RuleFor(x => x.FileDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.file.filedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FileDescription));

        RuleFor(x => x.FileTags)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.file.filetags", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileTags));

        RuleFor(x => x.IpAddress)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.file.ipaddress", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IpAddress));

        RuleFor(x => x.Location)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.file.location", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Location));
    }
}
