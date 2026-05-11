// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktPcbaOutputDetailValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PcbaOutputDetail DTO 验证器（根据实体 TaktPcbaOutputDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;

namespace Takt.Application.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// PcbaOutputDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktPcbaOutputDetail"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktPcbaOutputDetailCreateDtoValidator : AbstractValidator<TaktPcbaOutputDetailCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPcbaOutputDetailCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ProdOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutputdetail.prodordercode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutputdetail.prodordercode", 1, 20));

        RuleFor(x => x.TimePeriod)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutputdetail.timeperiod"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutputdetail.timeperiod", 1, 20));

        RuleFor(x => x.PcbBoardType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutputdetail.pcbboardtype"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutputdetail.pcbboardtype", 1, 20));

        RuleFor(x => x.PanelSide)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutputdetail.panelside"))
            .Length(1, 10).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutputdetail.panelside", 1, 10));

        RuleFor(x => x.CompletedStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.pcbaoutputdetail.completedstatus"));

        RuleFor(x => x.SerialNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.pcbaoutputdetail.serialno"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.pcbaoutputdetail.serialno", 1, 20));

        RuleFor(x => x.UnachievedReason)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.pcbaoutputdetail.unachievedreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedReason));

        RuleFor(x => x.UnachievedDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.pcbaoutputdetail.unachieveddescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedDescription));
    }
}

/// <summary>
/// PcbaOutputDetail更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktPcbaOutputDetailCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>PcbaOutputDetailId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktPcbaOutputDetailUpdateDtoValidator : AbstractValidator<TaktPcbaOutputDetailUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktPcbaOutputDetailUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktPcbaOutputDetailCreateDtoValidator(validationMessages));

        RuleFor(x => x.PcbaOutputDetailId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.pcbaoutputdetail.pcbaoutputdetailid"));

        RuleFor(x => x.ProdOrderCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutputdetail.prodordercode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ProdOrderCode));

        RuleFor(x => x.TimePeriod)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutputdetail.timeperiod", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.TimePeriod));

        RuleFor(x => x.PcbBoardType)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutputdetail.pcbboardtype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbBoardType));

        RuleFor(x => x.PanelSide)
            .MaximumLength(10).WithMessage(_validationMessages.LengthMax("entity.pcbaoutputdetail.panelside", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.PanelSide));

        RuleFor(x => x.SerialNo)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.pcbaoutputdetail.serialno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNo));

        RuleFor(x => x.UnachievedReason)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.pcbaoutputdetail.unachievedreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedReason));

        RuleFor(x => x.UnachievedDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.pcbaoutputdetail.unachieveddescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedDescription));
    }
}
