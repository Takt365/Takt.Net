// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Statistics.Report
// 文件名称：TaktReportExecutionLogValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ReportExecutionLog DTO 验证器（根据实体 TaktReportExecutionLog 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Report;
using Takt.Domain.Interfaces;
using Takt.Shared.Helpers;
using Takt.WebApi.Validation;

namespace Takt.WebApi.Validators.Statistics.Report;

/// <summary>
/// ReportExecutionLog创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Report.TaktReportExecutionLog"/> 字段对齐）。
/// </summary>
public class TaktReportExecutionLogCreateDtoValidator : AbstractValidator<TaktReportExecutionLogCreateDto>
{
    public TaktReportExecutionLogCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.VariantName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.variantname"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.variantname", 1, 50));

        RuleFor(x => x.SelectionParameters)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.selectionparameters"));

        RuleFor(x => x.LayoutVariant)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.layoutvariant"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.layoutvariant", 1, 50));

        RuleFor(x => x.ExecutionType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.executiontype"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.executiontype", 1, 20));

        RuleFor(x => x.BackgroundJobName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.backgroundjobname"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.backgroundjobname", 1, 50));

        RuleFor(x => x.BackgroundJobCount)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.backgroundjobcount"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.backgroundjobcount", 1, 20));

        RuleFor(x => x.IsSuccess)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportexecutionlog.issuccess"));

        RuleFor(x => x.ErrorMessage)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.errormessage"))
            .Length(1, 1000).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.errormessage", 1, 1000));

        RuleFor(x => x.MessageType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.messagetype"))
            .Length(1, 1).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.messagetype", 1, 1));

        RuleFor(x => x.MessageNumber)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.messagenumber"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.messagenumber", 1, 20));

        RuleFor(x => x.PlantCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.plantcode"))
            .Must(TaktRegexHelper.IsValidPlantCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportexecutionlog.plantcode"));

        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.companycode"))
            .Must(TaktRegexHelper.IsValidCompanyCode).WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportexecutionlog.companycode"));

        RuleFor(x => x.ClientIp)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.clientip"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.clientip", 1, 50));

        RuleFor(x => x.TerminalName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.terminalname"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.terminalname", 1, 50));

        RuleFor(x => x.OutputType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.outputtype"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.outputtype", 1, 20));

        RuleFor(x => x.SpoolRequestNo)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.spoolrequestno"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.spoolrequestno", 1, 20));

        RuleFor(x => x.IsExport)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportexecutionlog.isexport"));

        RuleFor(x => x.ExportFormat)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.exportformat"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.exportformat", 1, 20));

        RuleFor(x => x.ExportFilePath)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.exportfilepath"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportexecutionlog.exportfilepath", 1, 500));

        RuleFor(x => x.IsDownloaded)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportexecutionlog.isdownloaded"));
    }
}

/// <summary>
/// ReportExecutionLog更新 DTO 验证器。
/// </summary>
public class TaktReportExecutionLogUpdateDtoValidator : AbstractValidator<TaktReportExecutionLogUpdateDto>
{
    public TaktReportExecutionLogUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktReportExecutionLogCreateDtoValidator(localizer));

        RuleFor(x => x.ReportExecutionLogId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.reportexecutionlog.reportexecutionlogid"));

        RuleFor(x => x.VariantName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.variantname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.VariantName));

        RuleFor(x => x.LayoutVariant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.layoutvariant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.LayoutVariant));

        RuleFor(x => x.ExecutionType)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.executiontype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ExecutionType));

        RuleFor(x => x.BackgroundJobName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.backgroundjobname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.BackgroundJobName));

        RuleFor(x => x.BackgroundJobCount)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.backgroundjobcount", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.BackgroundJobCount));

        RuleFor(x => x.ErrorMessage)
            .MaximumLength(1000).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.errormessage", 1000))
            .When(x => !string.IsNullOrWhiteSpace(x.ErrorMessage));

        RuleFor(x => x.MessageType)
            .MaximumLength(1).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.messagetype", 1))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageType));

        RuleFor(x => x.MessageNumber)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.messagenumber", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.MessageNumber));

        RuleFor(x => x.ClientIp)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.clientip", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ClientIp));

        RuleFor(x => x.TerminalName)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.terminalname", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.TerminalName));

        RuleFor(x => x.OutputType)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.outputtype", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.OutputType));

        RuleFor(x => x.SpoolRequestNo)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.spoolrequestno", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.SpoolRequestNo));

        RuleFor(x => x.ExportFormat)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.exportformat", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ExportFormat));

        RuleFor(x => x.ExportFilePath)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportexecutionlog.exportfilepath", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ExportFilePath));
    }
}
