// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktStandardOperationTimeValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：StandardOperationTime DTO 验证器（根据实体 TaktStandardOperationTime 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Bom;

/// <summary>
/// StandardOperationTime创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktStandardOperationTime"/> 字段对齐）。
/// </summary>
public class TaktStandardOperationTimeCreateDtoValidator : AbstractValidator<TaktStandardOperationTimeCreateDto>
{
    public TaktStandardOperationTimeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.standardoperationtime.plantcode"))
            .Length(1, 8).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.standardoperationtime.plantcode", 1, 8));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.standardoperationtime.materialcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.standardoperationtime.materialcode", 1, 20));

        RuleFor(x => x.WorkCenter)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.standardoperationtime.workcenter"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.standardoperationtime.workcenter", 1, 20));

        RuleFor(x => x.OperationDesc)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationtime.operationdesc", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationDesc));

        RuleFor(x => x.TimeUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.standardoperationtime.timeunit"))
            .Length(1, 3).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.standardoperationtime.timeunit", 1, 3));

        RuleFor(x => x.PointsUnit)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.standardoperationtime.pointsunit"))
            .Length(1, 5).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.standardoperationtime.pointsunit", 1, 5));

        RuleFor(x => x.ApprovalStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.standardoperationtime.approvalstatus"));

        RuleFor(x => x.Approver)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationtime.approver", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Approver));
    }
}

/// <summary>
/// StandardOperationTime更新 DTO 验证器。
/// </summary>
public class TaktStandardOperationTimeUpdateDtoValidator : AbstractValidator<TaktStandardOperationTimeUpdateDto>
{
    public TaktStandardOperationTimeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktStandardOperationTimeCreateDtoValidator(localizer));

        RuleFor(x => x.StandardOperationTimeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.standardoperationtime.standardoperationtimeid"));

        RuleFor(x => x.PlantCode)
            .MaximumLength(8).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationtime.plantcode", 8))
            .When(x => !string.IsNullOrWhiteSpace(x.PlantCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationtime.materialcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.WorkCenter)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationtime.workcenter", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenter));

        RuleFor(x => x.OperationDesc)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationtime.operationdesc", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationDesc));

        RuleFor(x => x.TimeUnit)
            .MaximumLength(3).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationtime.timeunit", 3))
            .When(x => !string.IsNullOrWhiteSpace(x.TimeUnit));

        RuleFor(x => x.PointsUnit)
            .MaximumLength(5).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationtime.pointsunit", 5))
            .When(x => !string.IsNullOrWhiteSpace(x.PointsUnit));

        RuleFor(x => x.Approver)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.standardoperationtime.approver", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Approver));
    }
}
