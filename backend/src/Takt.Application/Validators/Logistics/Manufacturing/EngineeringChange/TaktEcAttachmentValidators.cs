// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcAttachmentValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EcAttachment DTO 验证器（根据实体 TaktEcAttachment 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

namespace Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// EcAttachment创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEcAttachment"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEcAttachmentCreateDtoValidator : AbstractValidator<TaktEcAttachmentCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcAttachmentCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.EcnNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecattachment.ecnno"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.ecattachment.ecnno", 1, 10));

        RuleFor(x => x.AttachmentType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecattachment.attachmenttype"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.ecattachment.attachmenttype", 1, 30));

        RuleFor(x => x.DocNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecattachment.docno"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ecattachment.docno", 1, 50));

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecattachment.filename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.ecattachment.filename", 1, 200));

        RuleFor(x => x.AccessUrl)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecattachment.accessurl"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.ecattachment.accessurl", 1, 500));
    }
}

/// <summary>
/// EcAttachment更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEcAttachmentCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EcAttachmentId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEcAttachmentUpdateDtoValidator : AbstractValidator<TaktEcAttachmentUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcAttachmentUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEcAttachmentCreateDtoValidator(validationMessages));

        RuleFor(x => x.EcAttachmentId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ecattachment.ecattachmentid"));

        RuleFor(x => x.EcnNo)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.ecattachment.ecnno", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNo));

        RuleFor(x => x.AttachmentType)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.ecattachment.attachmenttype", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.AttachmentType));

        RuleFor(x => x.DocNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecattachment.docno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DocNo));

        RuleFor(x => x.FileName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecattachment.filename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FileName));

        RuleFor(x => x.AccessUrl)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecattachment.accessurl", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.AccessUrl));
    }
}
