// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange
// 文件名称：TaktEcNoticeValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：EcNotice DTO 验证器（根据实体 TaktEcNotice 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange;

namespace Takt.Application.Validators.Logistics.Manufacturing.EngineeringChange;

/// <summary>
/// EcNotice创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Logistics.Manufacturing.EngineeringChange.TaktEcNotice"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktEcNoticeCreateDtoValidator : AbstractValidator<TaktEcNoticeCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcNoticeCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecnotice.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.ecnotice.plantcode"));

        RuleFor(x => x.NoticeNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecnotice.noticeno"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.ecnotice.noticeno", 1, 30));

        RuleFor(x => x.EcnNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.ecnotice.ecnno"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.ecnotice.ecnno", 1, 30));

        RuleFor(x => x.EcnTitle)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecnotice.ecntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnTitle));

        RuleFor(x => x.NoticeDeptCodes)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecnotice.noticedeptcodes", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeDeptCodes));

        RuleFor(x => x.NoticeDeptNames)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecnotice.noticedeptnames", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeDeptNames));

        RuleFor(x => x.NotifierName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecnotice.notifiername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NotifierName));

        RuleFor(x => x.NoticeMethod)
            .InclusiveBetween(1, 1)
            .WithMessage(_validationMessages.FormatInvalid("entity.ecnotice.noticemethod"));

        RuleFor(x => x.NoticeStatus)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.ecnotice.noticestatus"));

        RuleFor(x => x.ConfirmerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecnotice.confirmername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ConfirmerName));

        RuleFor(x => x.ConfirmComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.ecnotice.confirmcomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ConfirmComment));
    }
}

/// <summary>
/// EcNotice更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktEcNoticeCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>EcNoticeId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktEcNoticeUpdateDtoValidator : AbstractValidator<TaktEcNoticeUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktEcNoticeUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktEcNoticeCreateDtoValidator(validationMessages));

        RuleFor(x => x.EcNoticeId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.ecnotice.ecnoticeid"));

        RuleFor(x => x.NoticeNo)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.ecnotice.noticeno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeNo));

        RuleFor(x => x.EcnNo)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.ecnotice.ecnno", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnNo));

        RuleFor(x => x.EcnTitle)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecnotice.ecntitle", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.EcnTitle));

        RuleFor(x => x.NoticeDeptCodes)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.ecnotice.noticedeptcodes", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeDeptCodes));

        RuleFor(x => x.NoticeDeptNames)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.ecnotice.noticedeptnames", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.NoticeDeptNames));

        RuleFor(x => x.NotifierName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecnotice.notifiername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.NotifierName));

        RuleFor(x => x.ConfirmerName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.ecnotice.confirmername", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ConfirmerName));

        RuleFor(x => x.ConfirmComment)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.ecnotice.confirmcomment", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ConfirmComment));
    }
}
