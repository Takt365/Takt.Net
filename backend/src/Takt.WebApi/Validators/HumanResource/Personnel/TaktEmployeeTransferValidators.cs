// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeTransferValidators.cs
// 创建时间：2026-04-24
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeTransfer DTO 验证器（根据实体 TaktEmployeeTransfer 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeTransfer创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeTransfer"/> 字段对齐）。
/// </summary>
public class TaktEmployeeTransferCreateDtoValidator : AbstractValidator<TaktEmployeeTransferCreateDto>
{
    public TaktEmployeeTransferCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.TransferType)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeetransfer.transfertype"));

        RuleFor(x => x.FromDeptName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeetransfer.fromdeptname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeetransfer.fromdeptname", 1, 100));

        RuleFor(x => x.FromPostName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeetransfer.frompostname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromPostName));

        RuleFor(x => x.ToDeptName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.employeetransfer.todeptname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.employeetransfer.todeptname", 1, 100));

        RuleFor(x => x.ToPostName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeetransfer.topostname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ToPostName));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeetransfer.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.TransferStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.employeetransfer.transferstatus"));
    }
}

/// <summary>
/// EmployeeTransfer更新 DTO 验证器。
/// </summary>
public class TaktEmployeeTransferUpdateDtoValidator : AbstractValidator<TaktEmployeeTransferUpdateDto>
{
    public TaktEmployeeTransferUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktEmployeeTransferCreateDtoValidator(localizer));

        RuleFor(x => x.EmployeeTransferId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.employeetransfer.employeetransferid"));

        RuleFor(x => x.FromDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeetransfer.fromdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromDeptName));

        RuleFor(x => x.FromPostName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeetransfer.frompostname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromPostName));

        RuleFor(x => x.ToDeptName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeetransfer.todeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ToDeptName));

        RuleFor(x => x.ToPostName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeetransfer.topostname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ToPostName));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.employeetransfer.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));
    }
}
