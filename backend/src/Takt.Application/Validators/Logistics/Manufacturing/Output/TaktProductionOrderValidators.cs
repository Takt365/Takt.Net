// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktProductionOrderValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProductionOrder DTO 验证器（根据实体 TaktProductionOrder 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;

namespace Takt.Application.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// ProductionOrder创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktProductionOrder"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktProductionOrderCreateDtoValidator : AbstractValidator<TaktProductionOrderCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductionOrderCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productionorder.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.productionorder.plantcode"));

        RuleFor(x => x.ProdOrderType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productionorder.prodordertype"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.productionorder.prodordertype", 1, 10));

        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productionorder.prodordercode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.productionorder.prodordercode", 1, 20));

        RuleFor(x => x.MaterialCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productionorder.materialcode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.productionorder.materialcode", 1, 20));

        RuleFor(x => x.UnitOfMeasure)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.productionorder.unitofmeasure"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.productionorder.unitofmeasure", 1, 10));

        RuleFor(x => x.Priority)
            .InclusiveBetween(1, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.productionorder.priority"));

        RuleFor(x => x.WorkCenter)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.workcenter", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenter));

        RuleFor(x => x.ProdLine)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.prodline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.ProdBatch)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.prodbatch", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdBatch));

        RuleFor(x => x.SerialNo)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.serialno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNo));

        RuleFor(x => x.RoutingCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.routingcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.RoutingCode));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.productionorder.status"));
    }
}

/// <summary>
/// ProductionOrder更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktProductionOrderCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ProductionOrderId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktProductionOrderUpdateDtoValidator : AbstractValidator<TaktProductionOrderUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProductionOrderUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktProductionOrderCreateDtoValidator(validationMessages));

        RuleFor(x => x.ProductionOrderId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.productionorder.productionorderid"));

        RuleFor(x => x.ProdOrderType)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.productionorder.prodordertype", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderType));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.MaterialCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.materialcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MaterialCode));

        RuleFor(x => x.UnitOfMeasure)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.productionorder.unitofmeasure", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.UnitOfMeasure));

        RuleFor(x => x.WorkCenter)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.workcenter", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.WorkCenter));

        RuleFor(x => x.ProdLine)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.prodline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdLine));

        RuleFor(x => x.ProdBatch)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.prodbatch", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdBatch));

        RuleFor(x => x.SerialNo)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.serialno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNo));

        RuleFor(x => x.RoutingCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.productionorder.routingcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.RoutingCode));
    }
}
