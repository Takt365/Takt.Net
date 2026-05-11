// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgeChangeLogValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：KnowledgeChangeLog DTO 验证器（根据实体 TaktKnowledgeChangeLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.HelpDesk;

namespace Takt.Application.Validators.Routine.Business.HelpDesk;

/// <summary>
/// KnowledgeChangeLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktKnowledgeChangeLog"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktKnowledgeChangeLogCreateDtoValidator : AbstractValidator<TaktKnowledgeChangeLogCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktKnowledgeChangeLogCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.KnowledgeTitle)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.knowledgechangelog.knowledgetitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.KnowledgeTitle));

        RuleFor(x => x.ChangeType)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.knowledgechangelog.changetype"));

        RuleFor(x => x.ChangeSummary)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.knowledgechangelog.changesummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeSummary));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.knowledgechangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.knowledgechangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}

/// <summary>
/// KnowledgeChangeLog更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktKnowledgeChangeLogCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>KnowledgeChangeLogId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktKnowledgeChangeLogUpdateDtoValidator : AbstractValidator<TaktKnowledgeChangeLogUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktKnowledgeChangeLogUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktKnowledgeChangeLogCreateDtoValidator(validationMessages));

        RuleFor(x => x.KnowledgeChangeLogId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.knowledgechangelog.knowledgechangelogid"));

        RuleFor(x => x.KnowledgeTitle)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.knowledgechangelog.knowledgetitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.KnowledgeTitle));

        RuleFor(x => x.ChangeSummary)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.knowledgechangelog.changesummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeSummary));

        RuleFor(x => x.ChangeFields)
            .MaximumLength(4000).WithMessage(_validationMessages.LengthMax("entity.knowledgechangelog.changefields", 4000))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeFields));

        RuleFor(x => x.ChangeReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.knowledgechangelog.changereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ChangeReason));
    }
}
