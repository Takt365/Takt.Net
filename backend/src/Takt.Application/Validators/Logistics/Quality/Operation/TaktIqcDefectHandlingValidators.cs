// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Quality.Operation
// 文件名称：TaktIqcDefectHandlingValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：IqcDefectHandling DTO 验证器（根据实体 TaktIqcDefectHandling 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Quality.Operation;

namespace Takt.Application.Validators.Logistics.Quality.Operation;

/// <summary>
/// IqcDefectHandling创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Quality.Operation.TaktIqcDefectHandling"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktIqcDefectHandlingCreateDtoValidator : AbstractValidator<TaktIqcDefectHandlingCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIqcDefectHandlingCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.IqcDefectHandlingCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcdefecthandling.iqcdefecthandlingcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcdefecthandling.iqcdefecthandlingcode", 1, 50));

        RuleFor(x => x.IqcOrderCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcdefecthandling.iqcordercode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcdefecthandling.iqcordercode", 1, 50));

        RuleFor(x => x.DefectType)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.iqcdefecthandling.defecttype"));

        RuleFor(x => x.DefectCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcdefecthandling.defectcode"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.iqcdefecthandling.defectcode", 1, 50));

        RuleFor(x => x.DefectDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.iqcdefecthandling.defectdescription"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.iqcdefecthandling.defectdescription", 1, 500));

        RuleFor(x => x.HandlingMethod)
            .InclusiveBetween(0, 5)
            .WithMessage(_validationMessages.FormatInvalid("entity.iqcdefecthandling.handlingmethod"));

        RuleFor(x => x.HandlingDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.handlingdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingDescription));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.HandlingStatus)
            .InclusiveBetween(0, 3)
            .WithMessage(_validationMessages.FormatInvalid("entity.iqcdefecthandling.handlingstatus"));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.correctiveaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.DefectImages)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.defectimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectImages));
    }
}

/// <summary>
/// IqcDefectHandling更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktIqcDefectHandlingCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>IqcDefectHandlingId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktIqcDefectHandlingUpdateDtoValidator : AbstractValidator<TaktIqcDefectHandlingUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktIqcDefectHandlingUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktIqcDefectHandlingCreateDtoValidator(validationMessages));

        RuleFor(x => x.IqcDefectHandlingId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.iqcdefecthandling.iqcdefecthandlingid"));

        RuleFor(x => x.IqcDefectHandlingCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.iqcdefecthandlingcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IqcDefectHandlingCode));

        RuleFor(x => x.IqcOrderCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.iqcordercode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.IqcOrderCode));

        RuleFor(x => x.DefectCode)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.defectcode", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectCode));

        RuleFor(x => x.DefectDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.defectdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectDescription));

        RuleFor(x => x.HandlingDescription)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.handlingdescription", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlingDescription));

        RuleFor(x => x.ResponsibleDept)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.responsibledept", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleDept));

        RuleFor(x => x.ResponsibleBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.responsibleby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ResponsibleBy));

        RuleFor(x => x.HandlerBy)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.handlerby", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.HandlerBy));

        RuleFor(x => x.CorrectiveAction)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.correctiveaction", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.CorrectiveAction));

        RuleFor(x => x.DefectImages)
            .MaximumLength(2000).WithMessage(_validationMessages.LengthMax("entity.iqcdefecthandling.defectimages", 2000))
            .When(x => !string.IsNullOrWhiteSpace(x.DefectImages));
    }
}
