// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.News
// 文件名称：TaktNewsAttachmentValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：NewsAttachment DTO 验证器（根据实体 TaktNewsAttachment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.News;

namespace Takt.Application.Validators.Routine.Business.News;

/// <summary>
/// NewsAttachment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNewsAttachment"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktNewsAttachmentCreateDtoValidator : AbstractValidator<TaktNewsAttachmentCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNewsAttachmentCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.newsattachment.filename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.newsattachment.filename", 1, 200));

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.newsattachment.filepath"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.newsattachment.filepath", 1, 500));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.newsattachment.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.FileExtension)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.newsattachment.fileextension", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FileExtension));
    }
}

/// <summary>
/// NewsAttachment更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktNewsAttachmentCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>NewsAttachmentId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktNewsAttachmentUpdateDtoValidator : AbstractValidator<TaktNewsAttachmentUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNewsAttachmentUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktNewsAttachmentCreateDtoValidator(validationMessages));

        RuleFor(x => x.NewsAttachmentId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.newsattachment.newsattachmentid"));

        RuleFor(x => x.FileName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.newsattachment.filename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileName));

        RuleFor(x => x.FilePath)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.newsattachment.filepath", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FilePath));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.newsattachment.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.FileExtension)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.newsattachment.fileextension", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FileExtension));
    }
}
