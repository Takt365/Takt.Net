// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Logistics.Manufacturing.Output
// 文件名称：TaktPcbaOutputDetailValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：PcbaOutputDetail DTO 验证器（根据实体 TaktPcbaOutputDetail 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Logistics.Manufacturing.Output;

/// <summary>
/// PcbaOutputDetail创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.Output.TaktPcbaOutputDetail"/> 字段对齐）。
/// </summary>
public class TaktPcbaOutputDetailCreateDtoValidator : AbstractValidator<TaktPcbaOutputDetailCreateDto>
{
    public TaktPcbaOutputDetailCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TimePeriod)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbaoutputdetail.timeperiod"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbaoutputdetail.timeperiod", 1, 20));

        RuleFor(x => x.PcbBoardType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbaoutputdetail.pcbboardtype"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbaoutputdetail.pcbboardtype", 1, 20));

        RuleFor(x => x.PanelSide)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbaoutputdetail.panelside"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbaoutputdetail.panelside", 1, 10));

        RuleFor(x => x.CompletedStatus)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbaoutputdetail.completedstatus"))
            .Length(1, 10).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbaoutputdetail.completedstatus", 1, 10));

        RuleFor(x => x.SerialNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbaoutputdetail.serialno"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.pcbaoutputdetail.serialno", 1, 20));

        RuleFor(x => x.UnachievedReason)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbaoutputdetail.unachievedreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedReason));

        RuleFor(x => x.UnachievedDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbaoutputdetail.unachieveddescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedDescription));
    }
}

/// <summary>
/// PcbaOutputDetail更新 DTO 验证器。
/// </summary>
public class TaktPcbaOutputDetailUpdateDtoValidator : AbstractValidator<TaktPcbaOutputDetailUpdateDto>
{
    public TaktPcbaOutputDetailUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktPcbaOutputDetailCreateDtoValidator(localizer));

        RuleFor(x => x.PcbaOutputDetailId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.pcbaoutputdetail.pcbaoutputdetailid"));

        RuleFor(x => x.TimePeriod)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbaoutputdetail.timeperiod", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.TimePeriod));

        RuleFor(x => x.PcbBoardType)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbaoutputdetail.pcbboardtype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.PcbBoardType));

        RuleFor(x => x.PanelSide)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbaoutputdetail.panelside", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.PanelSide));

        RuleFor(x => x.CompletedStatus)
            .MaximumLength(10).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbaoutputdetail.completedstatus", 10))
            .When(x => !string.IsNullOrWhiteSpace(x.CompletedStatus));

        RuleFor(x => x.SerialNo)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbaoutputdetail.serialno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SerialNo));

        RuleFor(x => x.UnachievedReason)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbaoutputdetail.unachievedreason", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedReason));

        RuleFor(x => x.UnachievedDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.pcbaoutputdetail.unachieveddescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.UnachievedDescription));
    }
}
