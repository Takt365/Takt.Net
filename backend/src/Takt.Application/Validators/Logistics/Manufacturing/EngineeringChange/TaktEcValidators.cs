// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：Ec DTO 验证器（根据实体 TaktEc 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

namespace Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// Ec创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEc"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEcCreateDtoValidator : AbstractValidator<TaktEcCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ec.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.ec.plantcode"));

        RuleFor(x => x.EcnNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ec.ecnno"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.ec.ecnno", 1, 10));

        RuleFor(x => x.ChangeStatus)
            .InclusiveBetween(1, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.ec.changestatus"));

        RuleFor(x => x.EcnTitle)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ec.ecntitle"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.ec.ecntitle", 1, 500));

        RuleFor(x => x.EcnDetailText)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ec.ecndetailtext"));

        RuleFor(x => x.EcnLeader)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ec.ecnleader"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ec.ecnleader", 1, 50));

        RuleFor(x => x.EcnDistinction)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ec.ecndistinction"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ec.ecndistinction", 1, 50));

        RuleFor(x => x.EcStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.ec.ecstatus"));
    }
}

/// <summary>
/// Ec更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEcCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EcId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEcUpdateDtoValidator : AbstractValidator<TaktEcUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEcCreateDtoValidator(validationMessages));

        RuleFor(x => x.EcId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ec.ecid"));

        RuleFor(x => x.EcnNo)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.ec.ecnno", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNo));

        RuleFor(x => x.EcnTitle)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ec.ecntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnTitle));

        RuleFor(x => x.EcnLeader)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ec.ecnleader", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnLeader));

        RuleFor(x => x.EcnDistinction)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ec.ecndistinction", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnDistinction));
    }
}
