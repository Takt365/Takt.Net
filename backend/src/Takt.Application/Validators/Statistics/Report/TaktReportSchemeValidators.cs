// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Validators.Statistics.Report
// 文件名称：TaktReportSchemeValidators.cs
// 创建时间：2026-05-11
// 创建人：Takt365(AI Auto-Generated)
// 功能描述：ReportScheme DTO 验证器（根据实体 TaktReportScheme 自动生成）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentValidation;
using Takt.Application.Dtos.Statistics.Report;

namespace Takt.Application.Validators.Statistics.Report;

/// <summary>
/// ReportScheme创建 DTO 验证器（与 <see cref="Takt.Domain.Entities.Statistics.Report.TaktReportScheme"/> 字段对齐）。
/// </summary>
/// <remarks>
/// 验证规则：
/// <list type="bullet">
///   <item>必填验证：根据实体字段的 IsNullable 特性自动生成</item>
///   <item>长度验证：根据实体字段的 Length 特性自动生成</item>
///   <item>格式验证：Email、Phone、IdCard 等特殊字段自动添加正则验证</item>
/// </list>
/// </remarks>
public class TaktReportSchemeCreateDtoValidator : AbstractValidator<TaktReportSchemeCreateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktReportSchemeCreateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        RuleFor(x => x.ReportCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.reportcode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.reportcode", 1, 20));

        RuleFor(x => x.ReportName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.reportname"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.reportname", 1, 100));

        RuleFor(x => x.ReportCategory)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.reportcategory"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.reportcategory", 1, 50));

        RuleFor(x => x.ApplicationModule)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.applicationmodule"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.applicationmodule", 1, 50));

        RuleFor(x => x.ReportDescription)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.reportdescription"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.reportdescription", 1, 500));

        RuleFor(x => x.SelectionScreenConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.selectionscreenconfig"));

        RuleFor(x => x.DataSourceType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.datasourcetype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.datasourcetype", 1, 50));

        RuleFor(x => x.DataSourceName)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.datasourcename"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.datasourcename", 1, 100));

        RuleFor(x => x.SqlQuery)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.sqlquery"));

        RuleFor(x => x.OutputType)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.outputtype"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.outputtype", 1, 50));

        RuleFor(x => x.AlvColumnConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.alvcolumnconfig"));

        RuleFor(x => x.DefaultLayoutVariant)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.defaultlayoutvariant"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.defaultlayoutvariant", 1, 50));

        RuleFor(x => x.SupportLayoutVariant)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.supportlayoutvariant"));

        RuleFor(x => x.SubtotalFields)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.subtotalfields"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.subtotalfields", 1, 500));

        RuleFor(x => x.SortFields)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.sortfields"))
            .Length(1, 500).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.sortfields", 1, 500));

        RuleFor(x => x.FilterConfig)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.filterconfig"));

        RuleFor(x => x.SupportTotal)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.supporttotal"));

        RuleFor(x => x.SupportSubtotal)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.supportsubtotal"));

        RuleFor(x => x.SupportAggregation)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.supportaggregation"));

        RuleFor(x => x.SupportDrillDown)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.supportdrilldown"));

        RuleFor(x => x.DrillDownReportCode)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.drilldownreportcode"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.drilldownreportcode", 1, 20));

        RuleFor(x => x.SupportBackground)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.supportbackground"));

        RuleFor(x => x.SupportVariantSave)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.supportvariantsave"));

        RuleFor(x => x.MaxRowCount)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.maxrowcount"));

        RuleFor(x => x.IsExportable)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.isexportable"));

        RuleFor(x => x.ExportFormats)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.exportformats"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.exportformats", 1, 200));

        RuleFor(x => x.IsPrintable)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.isprintable"));

        RuleFor(x => x.PrintTemplate)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.printtemplate"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.printtemplate", 1, 100));

        RuleFor(x => x.ApplicablePlantCodes)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.applicableplantcodes"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.applicableplantcodes", 1, 200));

        RuleFor(x => x.ApplicableCompanyCodes)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.applicablecompanycodes"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.applicablecompanycodes", 1, 200));

        RuleFor(x => x.ApplicableDepartment)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.applicabledepartment"))
            .Length(1, 100).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.applicabledepartment", 1, 100));

        RuleFor(x => x.ApplicableRoles)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.applicableroles"))
            .Length(1, 200).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.applicableroles", 1, 200));

        RuleFor(x => x.DevelopmentClass)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.developmentclass"))
            .Length(1, 30).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.developmentclass", 1, 30));

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.author"))
            .Length(1, 50).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.author", 1, 50));

        RuleFor(x => x.Version)
            .NotEmpty().WithMessage(_validationMessages.Required("entity.reportscheme.version"))
            .Length(1, 20).WithMessage(_validationMessages.LengthBetween("entity.reportscheme.version", 1, 20));

        RuleFor(x => x.Status)
            .InclusiveBetween(0, 0)
            .WithMessage(_validationMessages.FormatInvalid("entity.reportscheme.status"));
    }
}

/// <summary>
/// ReportScheme更新 DTO 验证器。
/// </summary>
/// <remarks>
/// 继承 <see cref="TaktReportSchemeCreateDtoValidator"/> 的所有验证规则，并额外验证：
/// <list type="bullet">
///   <item>ReportSchemeId 必须大于 0</item>
/// </list>
/// </remarks>
public class TaktReportSchemeUpdateDtoValidator : AbstractValidator<TaktReportSchemeUpdateDto>
{
    /// <summary>
    /// 验证消息格式化器
    /// </summary>
    private readonly ITaktValidationMessages _validationMessages;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="validationMessages">验证消息格式化器</param>
    public TaktReportSchemeUpdateDtoValidator(ITaktValidationMessages validationMessages)
    {
        _validationMessages = validationMessages;
        Include(new TaktReportSchemeCreateDtoValidator(validationMessages));

        RuleFor(x => x.ReportSchemeId)
            .GreaterThan(0)
            .WithMessage(_validationMessages.Required("entity.reportscheme.reportschemeid"));

        RuleFor(x => x.ReportCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.reportscheme.reportcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.ReportCode));

        RuleFor(x => x.ReportName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.reportscheme.reportname", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ReportName));

        RuleFor(x => x.ReportCategory)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportscheme.reportcategory", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ReportCategory));

        RuleFor(x => x.ApplicationModule)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportscheme.applicationmodule", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicationModule));

        RuleFor(x => x.ReportDescription)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.reportscheme.reportdescription", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.ReportDescription));

        RuleFor(x => x.DataSourceType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportscheme.datasourcetype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DataSourceType));

        RuleFor(x => x.DataSourceName)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.reportscheme.datasourcename", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.DataSourceName));

        RuleFor(x => x.OutputType)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportscheme.outputtype", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.OutputType));

        RuleFor(x => x.DefaultLayoutVariant)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportscheme.defaultlayoutvariant", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.DefaultLayoutVariant));

        RuleFor(x => x.SubtotalFields)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.reportscheme.subtotalfields", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SubtotalFields));

        RuleFor(x => x.SortFields)
            .MaximumLength(500).WithMessage(_validationMessages.LengthMax("entity.reportscheme.sortfields", 500))
            .When(x => !string.IsNullOrWhiteSpace(x.SortFields));

        RuleFor(x => x.DrillDownReportCode)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.reportscheme.drilldownreportcode", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.DrillDownReportCode));

        RuleFor(x => x.ExportFormats)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.reportscheme.exportformats", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ExportFormats));

        RuleFor(x => x.PrintTemplate)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.reportscheme.printtemplate", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.PrintTemplate));

        RuleFor(x => x.ApplicablePlantCodes)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.reportscheme.applicableplantcodes", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicablePlantCodes));

        RuleFor(x => x.ApplicableCompanyCodes)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.reportscheme.applicablecompanycodes", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableCompanyCodes));

        RuleFor(x => x.ApplicableDepartment)
            .MaximumLength(100).WithMessage(_validationMessages.LengthMax("entity.reportscheme.applicabledepartment", 100))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableDepartment));

        RuleFor(x => x.ApplicableRoles)
            .MaximumLength(200).WithMessage(_validationMessages.LengthMax("entity.reportscheme.applicableroles", 200))
            .When(x => !string.IsNullOrWhiteSpace(x.ApplicableRoles));

        RuleFor(x => x.DevelopmentClass)
            .MaximumLength(30).WithMessage(_validationMessages.LengthMax("entity.reportscheme.developmentclass", 30))
            .When(x => !string.IsNullOrWhiteSpace(x.DevelopmentClass));

        RuleFor(x => x.Author)
            .MaximumLength(50).WithMessage(_validationMessages.LengthMax("entity.reportscheme.author", 50))
            .When(x => !string.IsNullOrWhiteSpace(x.Author));

        RuleFor(x => x.Version)
            .MaximumLength(20).WithMessage(_validationMessages.LengthMax("entity.reportscheme.version", 20))
            .When(x => !string.IsNullOrWhiteSpace(x.Version));
    }
}
