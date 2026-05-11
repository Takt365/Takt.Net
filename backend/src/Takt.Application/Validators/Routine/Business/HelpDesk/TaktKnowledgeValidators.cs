// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.HelpDesk
// 文件名称：TaktKnowledgeValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Knowledge DTO 验证器（根据实体 TaktKnowledge 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.HelpDesk;

namespace Takt.Application.Validators.Routine.Business.HelpDesk;

/// <summary>
/// Knowledge创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktKnowledge"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktKnowledgeCreateDtoValidator : AbstractValidator<TaktKnowledgeCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktKnowledgeCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.knowledge.title"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.knowledge.title", 1, 200));

        RuleFor(x => x.Summary)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.knowledge.summary", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Summary));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.knowledge.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.Tags)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.knowledge.tags", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Tags));

        RuleFor(x => x.KnowledgeStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.knowledge.knowledgestatus"));

        RuleFor(x => x.IsPublished)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.knowledge.ispublished"));
    }
}

/// <summary>
/// Knowledge更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktKnowledgeCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>KnowledgeId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktKnowledgeUpdateDtoValidator : AbstractValidator<TaktKnowledgeUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktKnowledgeUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktKnowledgeCreateDtoValidator(validationMessages));

        RuleFor(x => x.KnowledgeId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.knowledge.knowledgeid"));

        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.knowledge.title", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Title));

        RuleFor(x => x.Summary)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.knowledge.summary", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.Summary));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.knowledge.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.Tags)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.knowledge.tags", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Tags));
    }
}
