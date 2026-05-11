// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Accounting.Controlling
// 文件名称：TaktCostCenterValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：CostCenter DTO 验证器（根据实体 TaktCostCenter 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Accounting.Controlling;

namespace Takt.Application.Validators.Accounting.Controlling;

/// <summary>
/// CostCenter创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Accounting.Controlling.TaktCostCenter"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktCostCenterCreateDtoValidator : AbstractValidator<TaktCostCenterCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCostCenterCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.costcenter.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.costcenter.companycode"));

        RuleFor(x => x.CostCenterCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.costcenter.costcentercode"))
            .Must(TaktRegexHelper.IsValidCostCenterCode).WithMessage(_validationMessages.FormatInvalid("entity.costcenter.costcentercode"));

        RuleFor(x => x.CostCenterName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.costcenter.costcentername"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.costcenter.costcentername", 1, 100));

        RuleFor(x => x.CostCenterType)
            .InclusiveBetween(0, 2)
            .WithMessage(_validationMessages.FormatInvalid("entity.costcenter.costcentertype"));

        RuleFor(x => x.ManagerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.costcenter.managername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ManagerName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.costcenter.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.costcenter.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));

        RuleFor(x => x.CostCenterStatus)
            .InclusiveBetween(0, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.costcenter.costcenterstatus"));
    }
}

/// <summary>
/// CostCenter更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktCostCenterCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>CostCenterId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktCostCenterUpdateDtoValidator : AbstractValidator<TaktCostCenterUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktCostCenterUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktCostCenterCreateDtoValidator(validationMessages));

        RuleFor(x => x.CostCenterId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.costcenter.costcenterid"));

        RuleFor(x => x.CostCenterName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.costcenter.costcentername", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.CostCenterName));

        RuleFor(x => x.ManagerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.costcenter.managername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ManagerName));

        RuleFor(x => x.DeptName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.costcenter.deptname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DeptName));

        RuleFor(x => x.RelatedPlant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.costcenter.relatedplant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.RelatedPlant));
    }
}
