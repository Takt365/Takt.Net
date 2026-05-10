// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationFirstArticleValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityOperationFirstArticle DTO 验证器（根据实体 TaktQualityOperationFirstArticle 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Quality.Cost;

/// <summary>
/// QualityOperationFirstArticle创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityOperationFirstArticle"/> 字段对齐）。
/// </summary>
public class TaktQualityOperationFirstArticleCreateDtoValidator : AbstractValidator<TaktQualityOperationFirstArticleCreateDto>
{
    public TaktQualityOperationFirstArticleCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
    }
}

/// <summary>
/// QualityOperationFirstArticle更新 DTO 验证器。
/// </summary>
public class TaktQualityOperationFirstArticleUpdateDtoValidator : AbstractValidator<TaktQualityOperationFirstArticleUpdateDto>
{
    public TaktQualityOperationFirstArticleUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktQualityOperationFirstArticleCreateDtoValidator(localizer));

        RuleFor(x => x.QualityOperationFirstArticleId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityoperationfirstarticle.qualityoperationfirstarticleid"));

    }
}
