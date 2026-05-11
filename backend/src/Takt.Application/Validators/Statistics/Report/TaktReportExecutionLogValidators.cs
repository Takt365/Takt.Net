// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Statistics.Report
// 文件名称：TaktReportExecutionLogValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ReportExecutionLog DTO 验证器（根据实体 TaktReportExecutionLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Report;

namespace Takt.Application.Validators.Statistics.Report;

/// <summary>
/// ReportExecutionLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Report.TaktReportExecutionLog"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktReportExecutionLogCreateDtoValidator : AbstractValidator<TaktReportExecutionLogCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktReportExecutionLogCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.VariantName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.variantname"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.variantname", 1, 50));

        RuleFor(x => x.SelectionParameters)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.selectionparameters"));

        RuleFor(x => x.LayoutVariant)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.layoutvariant"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.layoutvariant", 1, 50));

        RuleFor(x => x.ExecutionType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.executiontype"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.executiontype", 1, 20));

        RuleFor(x => x.BackgroundJobName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.backgroundjobname"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.backgroundjobname", 1, 50));

        RuleFor(x => x.BackgroundJobCount)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.backgroundjobcount"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.backgroundjobcount", 1, 20));

        RuleFor(x => x.IsSuccess)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportexecutionlog.issuccess"));

        RuleFor(x => x.ErrorMessage)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.errormessage"))
            .Length(1, 1000).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.errormessage", 1, 1000));

        RuleFor(x => x.MessageType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.messagetype"))
            .Length(1, 1).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.messagetype", 1, 1));

        RuleFor(x => x.MessageNumber)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.messagenumber"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.messagenumber", 1, 20));

        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(_validationMessages.FormatInvalid("entity.reportexecutionlog.plantcode"));

        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(_validationMessages.FormatInvalid("entity.reportexecutionlog.companycode"));

        RuleFor(x => x.ClientIp)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.clientip"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.clientip", 1, 50));

        RuleFor(x => x.TerminalName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.terminalname"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.terminalname", 1, 50));

        RuleFor(x => x.OutputType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.outputtype"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.outputtype", 1, 20));

        RuleFor(x => x.SpoolRequestNo)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.spoolrequestno"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.spoolrequestno", 1, 20));

        RuleFor(x => x.IsExport)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportexecutionlog.isexport"));

        RuleFor(x => x.ExportFormat)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.exportformat"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.exportformat", 1, 20));

        RuleFor(x => x.ExportFilePath)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportexecutionlog.exportfilepath"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.reportexecutionlog.exportfilepath", 1, 500));

        RuleFor(x => x.IsDownloaded)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportexecutionlog.isdownloaded"));
    }
}

/// <summary>
/// ReportExecutionLog更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktReportExecutionLogCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ReportExecutionLogId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktReportExecutionLogUpdateDtoValidator : AbstractValidator<TaktReportExecutionLogUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktReportExecutionLogUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktReportExecutionLogCreateDtoValidator(validationMessages));

        RuleFor(x => x.ReportExecutionLogId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.reportexecutionlog.reportexecutionlogid"));

        RuleFor(x => x.VariantName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.variantname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.VariantName));

        RuleFor(x => x.LayoutVariant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.layoutvariant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LayoutVariant));

        RuleFor(x => x.ExecutionType)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.executiontype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ExecutionType));

        RuleFor(x => x.BackgroundJobName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.backgroundjobname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BackgroundJobName));

        RuleFor(x => x.BackgroundJobCount)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.backgroundjobcount", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BackgroundJobCount));

        RuleFor(x => x.ErrorMessage)
            .MaximumLength(1000).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.errormessage", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMessage));

        RuleFor(x => x.MessageType)
            .MaximumLength(1).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.messagetype", 1))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageType));

        RuleFor(x => x.MessageNumber)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.messagenumber", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageNumber));

        RuleFor(x => x.ClientIp)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.clientip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ClientIp));

        RuleFor(x => x.TerminalName)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.terminalname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TerminalName));

        RuleFor(x => x.OutputType)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.outputtype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.OutputType));

        RuleFor(x => x.SpoolRequestNo)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.spoolrequestno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SpoolRequestNo));

        RuleFor(x => x.ExportFormat)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.exportformat", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ExportFormat));

        RuleFor(x => x.ExportFilePath)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.reportexecutionlog.exportfilepath", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ExportFilePath));
    }
}
