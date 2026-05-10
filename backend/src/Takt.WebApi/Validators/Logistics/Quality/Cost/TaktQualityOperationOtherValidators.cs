// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationOtherValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：QualityOperationOther DTO 验证器（根据实体 TaktQualityOperationOther 自动生成）
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
/// QualityOperationOther创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Cost.TaktQualityOperationOther"/> 字段对齐）。
/// </summary>
public class TaktQualityOperationOtherCreateDtoValidator : AbstractValidator<TaktQualityOperationOtherCreateDto>
{
    public TaktQualityOperationOtherCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
    }
}

/// <summary>
/// QualityOperationOther更新 DTO 验证器。
/// </summary>
public class TaktQualityOperationOtherUpdateDtoValidator : AbstractValidator<TaktQualityOperationOtherUpdateDto>
{
    public TaktQualityOperationOtherUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktQualityOperationOtherCreateDtoValidator(localizer));

        RuleFor(x => x.QualityOperationOtherId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.qualityoperationother.qualityoperationotherid"));

    }
}
