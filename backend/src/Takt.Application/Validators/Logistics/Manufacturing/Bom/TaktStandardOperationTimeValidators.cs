// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Bom
// 文件名称：TaktStandardOperationTimeValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：StandardOperationTime DTO 验证器（根据实体 TaktStandardOperationTime 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;

namespace Takt.Application.Validators.Logistics.Manufacturing.Bom;

/// <summary>
/// StandardOperationTime创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Bom.TaktStandardOperationTime"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktStandardOperationTimeCreateDtoValidator : AbstractValidator<TaktStandardOperationTimeCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktStandardOperationTimeCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.standardoperationtime.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.standardoperationtime.plantcode"));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.standardoperationtime.materialcode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.standardoperationtime.materialcode", 1, 20));

        RuleFor(x => x.WorkCenter)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.standardoperationtime.workcenter"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.standardoperationtime.workcenter", 1, 20));

        RuleFor(x => x.OperationDesc)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.standardoperationtime.operationdesc", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationDesc));

        RuleFor(x => x.TimeUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.standardoperationtime.timeunit"))
            .Length(1, 3).WithMessage(_validationMessages.LengthBetween("entity.standardoperationtime.timeunit", 1, 3));

        RuleFor(x => x.PointsUnit)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.standardoperationtime.pointsunit"))
            .Length(1, 5).WithMessage(_validationMessages.LengthBetween("entity.standardoperationtime.pointsunit", 1, 5));

        RuleFor(x => x.ApprovalStatus)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.standardoperationtime.approvalstatus"));

        RuleFor(x => x.Approver)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.standardoperationtime.approver", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Approver));
    }
}

/// <summary>
/// StandardOperationTime更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktStandardOperationTimeCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>StandardOperationTimeId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktStandardOperationTimeUpdateDtoValidator : AbstractValidator<TaktStandardOperationTimeUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktStandardOperationTimeUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktStandardOperationTimeCreateDtoValidator(validationMessages));

        RuleFor(x => x.StandardOperationTimeId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.standardoperationtime.standardoperationtimeid"));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.standardoperationtime.materialcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.WorkCenter)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.standardoperationtime.workcenter", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenter));

        RuleFor(x => x.OperationDesc)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.standardoperationtime.operationdesc", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OperationDesc));

        RuleFor(x => x.TimeUnit)
            .MaximumLength(3).WithMessage(_validationMessages.LengthMax("entity.standardoperationtime.timeunit", 3))
            .When(x => !string.IsNullOrWhiteSpace(x.TimeUnit));

        RuleFor(x => x.PointsUnit)
            .MaximumLength(5).WithMessage(_validationMessages.LengthMax("entity.standardoperationtime.pointsunit", 5))
            .When(x => !string.IsNullOrWhiteSpace(x.PointsUnit));

        RuleFor(x => x.Approver)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.standardoperationtime.approver", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Approver));
    }
}
