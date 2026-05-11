// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.HumanResource.Personnel
// 文件名称：TaktEmployeeTransferValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EmployeeTransfer DTO 验证器（根据实体 TaktEmployeeTransfer 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.HumanResource.Personnel;

namespace Takt.Application.Validators.HumanResource.Personnel;

/// <summary>
/// EmployeeTransfer创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployeeTransfer"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEmployeeTransferCreateDtoValidator : AbstractValidator<TaktEmployeeTransferCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeTransferCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.TransferType)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeetransfer.transfertype"));

        RuleFor(x => x.FromDeptName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeetransfer.fromdeptname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.employeetransfer.fromdeptname", 1, 100));

        RuleFor(x => x.FromPostName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.frompostname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromPostName));

        RuleFor(x => x.ToDeptName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.employeetransfer.todeptname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.employeetransfer.todeptname", 1, 100));

        RuleFor(x => x.ToPostName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.topostname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ToPostName));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.HandlingBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.handlingby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingBy));

        RuleFor(x => x.HandlingComment)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.handlingcomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingComment));

        RuleFor(x => x.TransferStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.employeetransfer.transferstatus"));
    }
}

/// <summary>
/// EmployeeTransfer更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEmployeeTransferCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EmployeeTransferId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEmployeeTransferUpdateDtoValidator : AbstractValidator<TaktEmployeeTransferUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEmployeeTransferUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEmployeeTransferCreateDtoValidator(validationMessages));

        RuleFor(x => x.EmployeeTransferId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.employeetransfer.employeetransferid"));

        RuleFor(x => x.ApplicantBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.applicantby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicantBy));

        RuleFor(x => x.FromDeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.fromdeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromDeptName));

        RuleFor(x => x.FromPostName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.frompostname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.FromPostName));

        RuleFor(x => x.ToDeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.todeptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ToDeptName));

        RuleFor(x => x.ToPostName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.topostname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ToPostName));

        RuleFor(x => x.Reason)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.reason", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.Reason));

        RuleFor(x => x.ApproverBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.approverby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproverBy));

        RuleFor(x => x.ApproveComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.approvecomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ApproveComment));

        RuleFor(x => x.HandlingBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.handlingby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingBy));

        RuleFor(x => x.HandlingComment)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.employeetransfer.handlingcomment", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingComment));
    }
}
