// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputDetailValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：AssyOutputDetail DTO 验证器（根据实体 TaktAssyOutputDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;

namespace Takt.Application.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// AssyOutputDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktAssyOutputDetail"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktAssyOutputDetailCreateDtoValidator : AbstractValidator<TaktAssyOutputDetailCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAssyOutputDetailCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.assyoutputdetail.prodordercode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.assyoutputdetail.prodordercode", 1, 20));

        RuleFor(x => x.TimePeriod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.assyoutputdetail.timeperiod"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.assyoutputdetail.timeperiod", 1, 20));

        RuleFor(x => x.DowntimeReason)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.downtimereason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeReason));

        RuleFor(x => x.DowntimeDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.downtimedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeDescription));

        RuleFor(x => x.UnachievedReason)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.unachievedreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedReason));

        RuleFor(x => x.UnachievedDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.unachieveddescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedDescription));
    }
}

/// <summary>
/// AssyOutputDetail更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktAssyOutputDetailCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>AssyOutputDetailId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktAssyOutputDetailUpdateDtoValidator : AbstractValidator<TaktAssyOutputDetailUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktAssyOutputDetailUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktAssyOutputDetailCreateDtoValidator(validationMessages));

        RuleFor(x => x.AssyOutputDetailId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.assyoutputdetail.assyoutputdetailid"));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.TimePeriod)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.timeperiod", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.TimePeriod));

        RuleFor(x => x.DowntimeReason)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.downtimereason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeReason));

        RuleFor(x => x.DowntimeDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.downtimedescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DowntimeDescription));

        RuleFor(x => x.UnachievedReason)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.unachievedreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedReason));

        RuleFor(x => x.UnachievedDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.assyoutputdetail.unachieveddescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedDescription));
    }
}
