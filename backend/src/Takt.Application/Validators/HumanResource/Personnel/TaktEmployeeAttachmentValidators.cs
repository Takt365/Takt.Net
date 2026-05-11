// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeAttachmentValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeAttachment DTO 验证器（根据实体 TaktEmployeeAttachment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;

namespace Takt.Application.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeAttachment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeAttachment"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEmployeeAttachmentCreateDtoValidator : AbstractValidator<TaktEmployeeAttachmentCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeAttachmentCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.FileCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeeattachment.filecode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.employeeattachment.filecode", 1, 50));

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeeattachment.filename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.employeeattachment.filename", 1, 200));

        RuleFor(x => x.FilePath)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeeattachment.filepath"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.employeeattachment.filepath", 1, 500));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeeattachment.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.AttachmentType)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeeattachment.attachmenttype"));

        RuleFor(x => x.AttachmentDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employeeattachment.attachmentdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentDescription));
    }
}

/// <summary>
/// EmployeeAttachment更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEmployeeAttachmentCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EmployeeAttachmentId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEmployeeAttachmentUpdateDtoValidator : AbstractValidator<TaktEmployeeAttachmentUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeAttachmentUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEmployeeAttachmentCreateDtoValidator(validationMessages));

        RuleFor(x => x.EmployeeAttachmentId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.employeeattachment.employeeattachmentid"));

        RuleFor(x => x.FileCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeeattachment.filecode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FileCode));

        RuleFor(x => x.FileName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.employeeattachment.filename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileName));

        RuleFor(x => x.FilePath)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employeeattachment.filepath", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.FilePath));

        RuleFor(x => x.FileType)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeeattachment.filetype", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FileType));

        RuleFor(x => x.AttachmentDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employeeattachment.attachmentdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentDescription));
    }
}
