// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Routine.Business.HelpDesk
// 文件名称：TaktTicketCategoryAssignValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：TicketCategoryAssign DTO 验证器（根据实体 TaktTicketCategoryAssign 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Routine.Business.HelpDesk;

namespace Takt.Application.Validators.Routine.Business.HelpDesk;

/// <summary>
/// TicketCategoryAssign创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Routine.Business.HelpDesk.TaktTicketCategoryAssign"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktTicketCategoryAssignCreateDtoValidator : AbstractValidator<TaktTicketCategoryAssignCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTicketCategoryAssignCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CategoryCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ticketcategoryassign.categorycode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.ticketcategoryassign.categorycode", 1, 50));

        RuleFor(x => x.AssigneeName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticketcategoryassign.assigneename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssigneeName));
    }
}

/// <summary>
/// TicketCategoryAssign更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktTicketCategoryAssignCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>TicketCategoryAssignId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktTicketCategoryAssignUpdateDtoValidator : AbstractValidator<TaktTicketCategoryAssignUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktTicketCategoryAssignUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktTicketCategoryAssignCreateDtoValidator(validationMessages));

        RuleFor(x => x.TicketCategoryAssignId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ticketcategoryassign.ticketcategoryassignid"));

        RuleFor(x => x.CategoryCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticketcategoryassign.categorycode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.CategoryCode));

        RuleFor(x => x.AssigneeName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ticketcategoryassign.assigneename", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.AssigneeName));
    }
}
