// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Workflow
// 文件名称：TaktFlowFormValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FlowForm DTO 验证器（根据实体 TaktFlowForm 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Workflow;

namespace Takt.Application.Validators.Workflow;

/// <summary>
/// FlowForm创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Workflow.TaktFlowForm"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFlowFormCreateDtoValidator : AbstractValidator<TaktFlowFormCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowFormCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.FormCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowform.formcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.flowform.formcode", 1, 50));

        RuleFor(x => x.FormName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowform.formname"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.flowform.formname", 1, 200));

        RuleFor(x => x.FormCategory)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowform.formcategory"));

        RuleFor(x => x.FormType)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowform.formtype"));

        RuleFor(x => x.FormVersion)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.flowform.formversion"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.flowform.formversion", 1, 20));

        RuleFor(x => x.IsDatasource)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowform.isdatasource"));

        RuleFor(x => x.RelatedDataBaseName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowform.relateddatabasename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedDataBaseName));

        RuleFor(x => x.RelatedTableName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowform.relatedtablename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedTableName));

        RuleFor(x => x.RelatedFormField)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowform.relatedformfield", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedFormField));

        RuleFor(x => x.FormStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.flowform.formstatus"));
    }
}

/// <summary>
/// FlowForm更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFlowFormCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FlowFormId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFlowFormUpdateDtoValidator : AbstractValidator<TaktFlowFormUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFlowFormUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFlowFormCreateDtoValidator(validationMessages));

        RuleFor(x => x.FlowFormId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.flowform.flowformid"));

        RuleFor(x => x.FormCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.flowform.formcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FormCode));

        RuleFor(x => x.FormName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowform.formname", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FormName));

        RuleFor(x => x.FormVersion)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.flowform.formversion", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.FormVersion));

        RuleFor(x => x.RelatedDataBaseName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowform.relateddatabasename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedDataBaseName));

        RuleFor(x => x.RelatedTableName)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.flowform.relatedtablename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedTableName));

        RuleFor(x => x.RelatedFormField)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.flowform.relatedformfield", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedFormField));
    }
}
