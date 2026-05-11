// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.News
// 文件名称：TaktNewsFavoriteValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：NewsFavorite DTO 验证器（根据实体 TaktNewsFavorite 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.News;

namespace Takt.Application.Validators.Routine.Business.News;

/// <summary>
/// NewsFavorite创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNewsFavorite"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktNewsFavoriteCreateDtoValidator : AbstractValidator<TaktNewsFavoriteCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNewsFavoriteCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.FavoriteTags)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.newsfavorite.favoritetags", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FavoriteTags));
    }
}

/// <summary>
/// NewsFavorite更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktNewsFavoriteCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>NewsFavoriteId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktNewsFavoriteUpdateDtoValidator : AbstractValidator<TaktNewsFavoriteUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNewsFavoriteUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktNewsFavoriteCreateDtoValidator(validationMessages));

        RuleFor(x => x.NewsFavoriteId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.newsfavorite.newsfavoriteid"));

        RuleFor(x => x.FavoriteTags)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.newsfavorite.favoritetags", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.FavoriteTags));
    }
}
