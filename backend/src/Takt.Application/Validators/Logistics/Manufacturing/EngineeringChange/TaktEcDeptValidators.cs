// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcDeptValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EcDept DTO 验证器（根据实体 TaktEcDept 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

namespace Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// EcDept创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEcDept"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEcDeptCreateDtoValidator : AbstractValidator<TaktEcDeptCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcDeptCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.EcnNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecdept.ecnno"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.ecdept.ecnno", 1, 10));

        RuleFor(x => x.DeptCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecdept.deptcode"))
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdept.deptcode", 50));

        RuleFor(x => x.IsImplemented)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.ecdept.isimplemented"));

        RuleFor(x => x.Content)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.ecdept.content", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.Content));

        RuleFor(x => x.ScheduledBatch)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.scheduledbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduledBatch));

        RuleFor(x => x.PoRemainder)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdept.poremainder", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PoRemainder));

        RuleFor(x => x.Balance)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdept.balance", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Balance));

        RuleFor(x => x.OldProductHandling)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecdept.oldproducthandling", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.OldProductHandling));

        RuleFor(x => x.Supplier)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdept.supplier", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Supplier));

        RuleFor(x => x.PurchaseOrderNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.purchaseorderno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseOrderNo));

        RuleFor(x => x.IqcOrderNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdept.iqcorderno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IqcOrderNo));

        RuleFor(x => x.OutboundBatch)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.outboundbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundBatch));

        RuleFor(x => x.ProductionBatch)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.productionbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionBatch));

        RuleFor(x => x.OutboundOrderNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.outboundorderno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundOrderNo));

        RuleFor(x => x.ProductionTeam)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.productionteam", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionTeam));

        RuleFor(x => x.InspectionBatch)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.inspectionbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionBatch));

        RuleFor(x => x.SamplingNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.samplingno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingNo));

        RuleFor(x => x.IsSopUpdated)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.ecdept.issopupdated"));
    }
}

/// <summary>
/// EcDept更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEcDeptCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EcDeptId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEcDeptUpdateDtoValidator : AbstractValidator<TaktEcDeptUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcDeptUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEcDeptCreateDtoValidator(validationMessages));

        RuleFor(x => x.EcDeptId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ecdept.ecdeptid"));

        RuleFor(x => x.EcnNo)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.ecdept.ecnno", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNo));

        RuleFor(x => x.Content)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.ecdept.content", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.Content));

        RuleFor(x => x.ScheduledBatch)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.scheduledbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ScheduledBatch));

        RuleFor(x => x.PoRemainder)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdept.poremainder", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.PoRemainder));

        RuleFor(x => x.Balance)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdept.balance", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Balance));

        RuleFor(x => x.OldProductHandling)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecdept.oldproducthandling", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.OldProductHandling));

        RuleFor(x => x.Supplier)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecdept.supplier", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.Supplier));

        RuleFor(x => x.PurchaseOrderNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.purchaseorderno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PurchaseOrderNo));

        RuleFor(x => x.IqcOrderNo)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecdept.iqcorderno", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IqcOrderNo));

        RuleFor(x => x.OutboundBatch)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.outboundbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundBatch));

        RuleFor(x => x.ProductionBatch)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.productionbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionBatch));

        RuleFor(x => x.OutboundOrderNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.outboundorderno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.OutboundOrderNo));

        RuleFor(x => x.ProductionTeam)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.productionteam", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionTeam));

        RuleFor(x => x.InspectionBatch)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.inspectionbatch", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.InspectionBatch));

        RuleFor(x => x.SamplingNo)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.ecdept.samplingno", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.SamplingNo));
    }
}
