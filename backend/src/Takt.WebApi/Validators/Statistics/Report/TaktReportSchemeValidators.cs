// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.WebApi.Validators.Statistics.Report
// 文件名称：TaktReportSchemeValidators.cs
// 创建时间：2026-05-10
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ReportScheme DTO 验证器（根据实体 TaktReportScheme 自动生成）
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
/// ReportScheme创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Report.TaktReportScheme"/> 字段对齐）。
/// </summary>
public class TaktReportSchemeCreateDtoValidator : AbstractValidator<TaktReportSchemeCreateDto>
{
    public TaktReportSchemeCreateDtoValidator(ITaktLocalizer? localizer = null)
    {
        RuleFor(x => x.ReportCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.reportcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.reportcode", 1, 20));

        RuleFor(x => x.ReportName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.reportname"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.reportname", 1, 100));

        RuleFor(x => x.ReportCategory)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.reportcategory"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.reportcategory", 1, 50));

        RuleFor(x => x.ApplicationModule)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.applicationmodule"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.applicationmodule", 1, 50));

        RuleFor(x => x.ReportDescription)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.reportdescription"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.reportdescription", 1, 500));

        RuleFor(x => x.SelectionScreenConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.selectionscreenconfig"));

        RuleFor(x => x.DataSourceType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.datasourcetype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.datasourcetype", 1, 50));

        RuleFor(x => x.DataSourceName)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.datasourcename"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.datasourcename", 1, 100));

        RuleFor(x => x.SqlQuery)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.sqlquery"));

        RuleFor(x => x.OutputType)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.outputtype"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.outputtype", 1, 50));

        RuleFor(x => x.AlvColumnConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.alvcolumnconfig"));

        RuleFor(x => x.DefaultLayoutVariant)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.defaultlayoutvariant"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.defaultlayoutvariant", 1, 50));

        RuleFor(x => x.SupportLayoutVariant)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.supportlayoutvariant"));

        RuleFor(x => x.SubtotalFields)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.subtotalfields"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.subtotalfields", 1, 500));

        RuleFor(x => x.SortFields)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.sortfields"))
            .Length(1, 500).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.sortfields", 1, 500));

        RuleFor(x => x.FilterConfig)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.filterconfig"));

        RuleFor(x => x.SupportTotal)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.supporttotal"));

        RuleFor(x => x.SupportSubtotal)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.supportsubtotal"));

        RuleFor(x => x.SupportAggregation)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.supportaggregation"));

        RuleFor(x => x.SupportDrillDown)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.supportdrilldown"));

        RuleFor(x => x.DrillDownReportCode)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.drilldownreportcode"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.drilldownreportcode", 1, 20));

        RuleFor(x => x.SupportBackground)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.supportbackground"));

        RuleFor(x => x.SupportVariantSave)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.supportvariantsave"));

        RuleFor(x => x.MaxRowCount)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.maxrowcount"));

        RuleFor(x => x.IsExportable)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.isexportable"));

        RuleFor(x => x.ExportFormats)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.exportformats"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.exportformats", 1, 200));

        RuleFor(x => x.IsPrintable)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.isprintable"));

        RuleFor(x => x.PrintTemplate)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.printtemplate"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.printtemplate", 1, 100));

        RuleFor(x => x.ApplicablePlantCodes)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.applicableplantcodes"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.applicableplantcodes", 1, 200));

        RuleFor(x => x.ApplicableCompanyCodes)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.applicablecompanycodes"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.applicablecompanycodes", 1, 200));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.applicabledepartment"))
            .Length(1, 100).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicableRoles)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.applicableroles"))
            .Length(1, 200).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.applicableroles", 1, 200));

        RuleFor(x => x.DevelopmentClass)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.developmentclass"))
            .Length(1, 30).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.developmentclass", 1, 30));

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.author"))
            .Length(1, 50).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.author", 1, 50));

        RuleFor(x => x.Version)
            .NotEmpty().WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.version"))
            .Length(1, 20).WithMessage(TaktValidationMessages.LengthBetween(localizer, "entity.reportscheme.version", 1, 20));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(TaktValidationMessages.FormatInvalid(localizer, "entity.reportscheme.status"));
    }
}

/// <summary>
/// ReportScheme更新 DTO 验证器。
/// </summary>
public class TaktReportSchemeUpdateDtoValidator : AbstractValidator<TaktReportSchemeUpdateDto>
{
    public TaktReportSchemeUpdateDtoValidator(ITaktLocalizer? localizer = null)
    {
        Include(new TaktReportSchemeCreateDtoValidator(localizer));

        RuleFor(x => x.ReportSchemeId)
            .GreaterThan(0)
            .WithMessage(TaktValidationMessages.Required(localizer, "entity.reportscheme.reportschemeid"));

        RuleFor(x => x.ReportCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.reportcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ReportCode));

        RuleFor(x => x.ReportName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.reportname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ReportName));

        RuleFor(x => x.ReportCategory)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.reportcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ReportCategory));

        RuleFor(x => x.ApplicationModule)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.applicationmodule", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicationModule));

        RuleFor(x => x.ReportDescription)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.reportdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ReportDescription));

        RuleFor(x => x.DataSourceType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.datasourcetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DataSourceType));

        RuleFor(x => x.DataSourceName)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.datasourcename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DataSourceName));

        RuleFor(x => x.OutputType)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.outputtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OutputType));

        RuleFor(x => x.DefaultLayoutVariant)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.defaultlayoutvariant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefaultLayoutVariant));

        RuleFor(x => x.SubtotalFields)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.subtotalfields", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SubtotalFields));

        RuleFor(x => x.SortFields)
            .MaximumLength(500).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.sortfields", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SortFields));

        RuleFor(x => x.DrillDownReportCode)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.drilldownreportcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.DrillDownReportCode));

        RuleFor(x => x.ExportFormats)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.exportformats", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ExportFormats));

        RuleFor(x => x.PrintTemplate)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.printtemplate", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PrintTemplate));

        RuleFor(x => x.ApplicablePlantCodes)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.applicableplantcodes", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePlantCodes));

        RuleFor(x => x.ApplicableCompanyCodes)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.applicablecompanycodes", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableCompanyCodes));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicableRoles)
            .MaximumLength(200).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.applicableroles", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableRoles));

        RuleFor(x => x.DevelopmentClass)
            .MaximumLength(30).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.developmentclass", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.DevelopmentClass));

        RuleFor(x => x.Author)
            .MaximumLength(50).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.author", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Author));

        RuleFor(x => x.Version)
            .MaximumLength(20).WithMessage(TaktValidationMessages.LengthMax(localizer, "entity.reportscheme.version", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.Version));
    }
}
