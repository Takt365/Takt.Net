// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Accounting.Controlling
// 文件名称：TaktProfitCenterValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ProfitCenter DTO 验证器（根据实体 TaktProfitCenter 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Controlling;

namespace Takt.Application.Validators.Accounting.Controlling;

/// <summary>
/// ProfitCenter创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktProfitCenter"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktProfitCenterCreateDtoValidator : AbstractValidator<TaktProfitCenterCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProfitCenterCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.profitcenter.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.profitcenter.companycode"));

        RuleFor(x => x.ProfitCenterCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.profitcenter.profitcentercode"))
            .Must(TaktRegexHelper.IsValidProfitCenterCode).WithMessage(_validationMessages.FormatInvalid("entity.profitcenter.profitcentercode"));

        RuleFor(x => x.ProfitCenterName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.profitcenter.profitcentername"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.profitcenter.profitcentername", 1, 100));

        RuleFor(x => x.ManagerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.profitcenter.managername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ManagerName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.profitcenter.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.profitcenter.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.ProfitCenterStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.profitcenter.profitcenterstatus"));
    }
}

/// <summary>
/// ProfitCenter更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktProfitCenterCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ProfitCenterId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktProfitCenterUpdateDtoValidator : AbstractValidator<TaktProfitCenterUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktProfitCenterUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktProfitCenterCreateDtoValidator(validationMessages));

        RuleFor(x => x.ProfitCenterId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.profitcenter.profitcenterid"));

        RuleFor(x => x.ProfitCenterName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.profitcenter.profitcentername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ProfitCenterName));

        RuleFor(x => x.ManagerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.profitcenter.managername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ManagerName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.profitcenter.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.profitcenter.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
