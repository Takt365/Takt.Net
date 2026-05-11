// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Workflow
// 文件名称：TaktFlowSchemeValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowScheme DTO 验证器（根据实体 TaktFlowScheme 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Workflow;

namespace Takt.Application.Validators.Workflow;

/// <summary>
/// FlowScheme创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowScheme"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFlowSchemeCreateDtoValidator : AbstractValidator<TaktFlowSchemeCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowSchemeCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.SchemeKey)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowscheme.schemekey"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.flowscheme.schemekey", 1, 100));

        RuleFor(x => x.SchemeName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowscheme.schemename"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.flowscheme.schemename", 1, 200));

        RuleFor(x => x.SchemeCategory)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowscheme.schemecategory"));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.flowscheme.schemedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowscheme.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));

        RuleFor(x => x.SchemeStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowscheme.schemestatus"));
    }
}

/// <summary>
/// FlowScheme更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFlowSchemeCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FlowSchemeId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFlowSchemeUpdateDtoValidator : AbstractValidator<TaktFlowSchemeUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowSchemeUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFlowSchemeCreateDtoValidator(validationMessages));

        RuleFor(x => x.FlowSchemeId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.flowscheme.flowschemeid"));

        RuleFor(x => x.SchemeKey)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.flowscheme.schemekey", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeKey));

        RuleFor(x => x.SchemeName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowscheme.schemename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeName));

        RuleFor(x => x.SchemeDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.flowscheme.schemedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SchemeDescription));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowscheme.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));
    }
}
