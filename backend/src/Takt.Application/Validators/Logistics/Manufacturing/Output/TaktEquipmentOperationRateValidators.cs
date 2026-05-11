// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktEquipmentOperationRateValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EquipmentOperationRate DTO 验证器（根据实体 TaktEquipmentOperationRate 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;

namespace Takt.Application.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// EquipmentOperationRate创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktEquipmentOperationRate"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEquipmentOperationRateCreateDtoValidator : AbstractValidator<TaktEquipmentOperationRateCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEquipmentOperationRateCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.equipmentoperationrate.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.equipmentoperationrate.plantcode"));

        RuleFor(x => x.TimeCategory)
            .InclusiveBetween(1, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.equipmentoperationrate.timecategory"));

        RuleFor(x => x.EquipmentCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.equipmentoperationrate.equipmentcode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.equipmentoperationrate.equipmentcode", 1, 20));

        RuleFor(x => x.EquipmentName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.equipmentoperationrate.equipmentname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.equipmentoperationrate.equipmentname", 1, 100));

        RuleFor(x => x.EquipmentType)
            .InclusiveBetween(1, 4)
            .WithMessage(_validationMessages.FormatInvalid("entity.equipmentoperationrate.equipmenttype"));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.productionline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));

        RuleFor(x => x.ShiftNo)
            .InclusiveBetween(1, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.equipmentoperationrate.shiftno"));

        RuleFor(x => x.DowntimeReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.downtimereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeReason));

        RuleFor(x => x.EquipmentStatus)
            .InclusiveBetween(1, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.equipmentoperationrate.equipmentstatus"));

        RuleFor(x => x.EquipmentOperator)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.equipmentoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentOperator));

        RuleFor(x => x.EquipmentMaintainer)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.equipmentmaintainer", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentMaintainer));

        RuleFor(x => x.TeamLeader)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.teamleader", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeader));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.equipmentoperationrate.status"));
    }
}

/// <summary>
/// EquipmentOperationRate更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEquipmentOperationRateCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EquipmentOperationRateId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEquipmentOperationRateUpdateDtoValidator : AbstractValidator<TaktEquipmentOperationRateUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEquipmentOperationRateUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEquipmentOperationRateCreateDtoValidator(validationMessages));

        RuleFor(x => x.EquipmentOperationRateId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.equipmentoperationrate.equipmentoperationrateid"));

        RuleFor(x => x.EquipmentCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.equipmentcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentCode));

        RuleFor(x => x.EquipmentName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.equipmentname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentName));

        RuleFor(x => x.ProductionLine)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.productionline", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProductionLine));

        RuleFor(x => x.DowntimeReason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.downtimereason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeReason));

        RuleFor(x => x.EquipmentOperator)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.equipmentoperator", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentOperator));

        RuleFor(x => x.EquipmentMaintainer)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.equipmentmaintainer", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EquipmentMaintainer));

        RuleFor(x => x.TeamLeader)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.equipmentoperationrate.teamleader", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TeamLeader));
    }
}
