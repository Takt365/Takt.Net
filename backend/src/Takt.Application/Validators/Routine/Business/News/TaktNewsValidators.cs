// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.News
// 文件名称：TaktNewsValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：News DTO 验证器（根据实体 TaktNews 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.News;

namespace Takt.Application.Validators.Routine.Business.News;

/// <summary>
/// News创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNews"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktNewsCreateDtoValidator : AbstractValidator<TaktNewsCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNewsCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.NewsCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.news.newscode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.news.newscode", 1, 50));

        RuleFor(x => x.NewsCategory)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.news.newscategory"));

        RuleFor(x => x.NewsTitle)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.news.newstitle"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.news.newstitle", 1, 200));

        RuleFor(x => x.NewsSummary)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.news.newssummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsSummary));

        RuleFor(x => x.NewsContent)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.news.newscontent"));

        RuleFor(x => x.NewsCoverImage)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.news.newscoverimage", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsCoverImage));

        RuleFor(x => x.IsTop)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.news.istop"));

        RuleFor(x => x.IsRecommended)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.news.isrecommended"));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.news.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PublisherName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.news.publishername"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.news.publishername", 1, 50));

        RuleFor(x => x.NewsStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.news.newsstatus"));
    }
}

/// <summary>
/// News更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktNewsCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>NewsId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktNewsUpdateDtoValidator : AbstractValidator<TaktNewsUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNewsUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktNewsCreateDtoValidator(validationMessages));

        RuleFor(x => x.NewsId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.news.newsid"));

        RuleFor(x => x.NewsCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.news.newscode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsCode));

        RuleFor(x => x.NewsTitle)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.news.newstitle", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsTitle));

        RuleFor(x => x.NewsSummary)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.news.newssummary", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsSummary));

        RuleFor(x => x.NewsCoverImage)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.news.newscoverimage", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NewsCoverImage));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.news.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.PublisherName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.news.publishername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.PublisherName));
    }
}
