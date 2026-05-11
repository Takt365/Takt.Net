// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Organization
// 文件名称：TaktPostValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Post DTO 验证器（根据实体 TaktPost 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Organization;

namespace Takt.Application.Validators.HumanResource.Organization;

/// <summary>
/// Post创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Organization.TaktPost"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPostCreateDtoValidator : AbstractValidator<TaktPostCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPostCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.post.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.post.companycode"));

        RuleFor(x => x.PostName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.post.postname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.post.postname", 1, 100));

        RuleFor(x => x.PostCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.post.postcode"))
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.post.postcode", 50));

        RuleFor(x => x.PostCategory)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.post.postcategory"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.post.postcategory", 1, 50));

        RuleFor(x => x.PostLevel)
            .InclusiveBetween(1, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.post.postlevel"));

        RuleFor(x => x.PostDuty)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.post.postduty", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PostDuty));

        RuleFor(x => x.DataScope)
            .InclusiveBetween(0, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.post.datascope"));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.post.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));

        RuleFor(x => x.PostStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.post.poststatus"));
    }
}

/// <summary>
/// Post更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPostCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PostId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPostUpdateDtoValidator : AbstractValidator<TaktPostUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPostUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPostCreateDtoValidator(validationMessages));

        RuleFor(x => x.PostId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.post.postid"));

        RuleFor(x => x.PostName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.post.postname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PostName));

        RuleFor(x => x.PostCategory)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.post.postcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PostCategory));

        RuleFor(x => x.PostDuty)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.post.postduty", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.PostDuty));

        RuleFor(x => x.CustomScope)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.post.customscope", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.CustomScope));
    }
}
