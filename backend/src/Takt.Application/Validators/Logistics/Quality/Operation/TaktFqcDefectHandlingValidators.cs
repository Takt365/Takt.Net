// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktFqcDefectHandlingValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：FqcDefectHandling DTO 验证器（根据实体 TaktFqcDefectHandling 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// FqcDefectHandling创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktFqcDefectHandling"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktFqcDefectHandlingCreateDtoValidator : AbstractValidator<TaktFqcDefectHandlingCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFqcDefectHandlingCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.FqcDefectHandlingCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcdefecthandling.fqcdefecthandlingcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcdefecthandling.fqcdefecthandlingcode", 1, 50));

        RuleFor(x => x.FqcOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcdefecthandling.fqcordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcdefecthandling.fqcordercode", 1, 50));

        RuleFor(x => x.DefectType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.fqcdefecthandling.defecttype"));

        RuleFor(x => x.DefectCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcdefecthandling.defectcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.fqcdefecthandling.defectcode", 1, 50));

        RuleFor(x => x.DefectDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.fqcdefecthandling.defectdescription"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.fqcdefecthandling.defectdescription", 1, 500));

        RuleFor(x => x.HandlingMethod)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.fqcdefecthandling.handlingmethod"));

        RuleFor(x => x.HandlingDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.handlingdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingDescription));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.HandlingStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.fqcdefecthandling.handlingstatus"));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.correctiveaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.DefectImages)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.defectimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectImages));
    }
}

/// <summary>
/// FqcDefectHandling更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktFqcDefectHandlingCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>FqcDefectHandlingId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktFqcDefectHandlingUpdateDtoValidator : AbstractValidator<TaktFqcDefectHandlingUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktFqcDefectHandlingUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktFqcDefectHandlingCreateDtoValidator(validationMessages));

        RuleFor(x => x.FqcDefectHandlingId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.fqcdefecthandling.fqcdefecthandlingid"));

        RuleFor(x => x.FqcDefectHandlingCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.fqcdefecthandlingcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FqcDefectHandlingCode));

        RuleFor(x => x.FqcOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.fqcordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.FqcOrderCode));

        RuleFor(x => x.DefectCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.defectcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectCode));

        RuleFor(x => x.DefectDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.defectdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectDescription));

        RuleFor(x => x.HandlingDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.handlingdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingDescription));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.correctiveaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.DefectImages)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.fqcdefecthandling.defectimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectImages));
    }
}
