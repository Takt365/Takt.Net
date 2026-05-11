// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.News
// 文件名称：TaktNewsShareValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：NewsShare DTO 验证器（根据实体 TaktNewsShare 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.News;

namespace Takt.Application.Validators.Routine.Business.News;

/// <summary>
/// NewsShare创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.News.TaktNewsShare"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktNewsShareCreateDtoValidator : AbstractValidator<TaktNewsShareCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNewsShareCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.SharePlatform)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.newsshare.shareplatform"));

        RuleFor(x => x.ShareRemark)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.newsshare.shareremark", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ShareRemark));
    }
}

/// <summary>
/// NewsShare更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktNewsShareCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>NewsShareId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktNewsShareUpdateDtoValidator : AbstractValidator<TaktNewsShareUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktNewsShareUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktNewsShareCreateDtoValidator(validationMessages));

        RuleFor(x => x.NewsShareId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.newsshare.newsshareid"));

        RuleFor(x => x.ShareRemark)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.newsshare.shareremark", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ShareRemark));
    }
}
