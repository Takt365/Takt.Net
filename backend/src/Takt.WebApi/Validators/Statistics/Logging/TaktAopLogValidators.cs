// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Statistics.Logging
// 文件名称：TaktAopLogValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AopLog DTO 验证器（根据实体 TaktAopLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Logging;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Statistics.Logging;

/// <summary>
/// AopLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Logging.TaktAopLog"/> 字段对齐）。
/// </summary>
public class TaktAopLogCreateDtoValidator : AbstractValidator<TaktAopLogCreateDto>
{
    public TaktAopLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.OperType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.aoplog.opertype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.aoplog.opertype", 1, 50));

        RuleFor(x => x.TableName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.aoplog.tablename"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.aoplog.tablename", 1, 200));
    }
}

/// <summary>
/// AopLog更新 DTO 验证器。
/// </summary>
public class TaktAopLogUpdateDtoValidator : AbstractValidator<TaktAopLogUpdateDto>
{
    public TaktAopLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktAopLogCreateDtoValidator(localizer));

        RuleFor(x => x.AopLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.aoplog.aoplogid"));

        RuleFor(x => x.OperType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.aoplog.opertype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OperType));

        RuleFor(x => x.TableName)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.aoplog.tablename", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.TableName));
    }
}
