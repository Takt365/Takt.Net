// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Statistics.Report
// 文件名称：TaktReportScheme.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：报表方案实体，定义报表的基本信息、数据源、查询条件、展示配置等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Statistics.Report;

/// <summary>
/// 报表方案实体（SAP风格：支持选择屏幕、布局变式、ALV输出、后台作业等）
/// </summary>
[SugarTable("takt_statistics_report_scheme", "报表方案表")]
[SugarIndex("ix_takt_statistics_report_scheme_report_code", nameof(ReportCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_statistics_report_scheme_report_category", nameof(ReportCategory), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_report_scheme_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_report_scheme_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktReportScheme : TaktEntityBase
{
    /// <summary>
    /// 报表编码（SAP风格：如 ZMMR001、ZPPR002）
    /// </summary>
    [SugarColumn(ColumnName = "report_code", ColumnDescription = "报表编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ReportCode { get; set; } = string.Empty;

    /// <summary>
    /// 报表名称
    /// </summary>
    [SugarColumn(ColumnName = "report_name", ColumnDescription = "报表名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ReportName { get; set; } = string.Empty;

    /// <summary>
    /// 报表类别(单表报表/多表报表/汇总报表/对比报表/趋势报表)
    /// </summary>
    [SugarColumn(ColumnName = "report_category", ColumnDescription = "报表类别", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ReportCategory { get; set; } = string.Empty;

    /// <summary>
    /// 应用模块(MM物料/PP生产/SD销售/FI财务/CO成本/HR人事/PM设备/QM质量/其他)
    /// </summary>
    [SugarColumn(ColumnName = "application_module", ColumnDescription = "应用模块", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicationModule { get; set; } = string.Empty;

    /// <summary>
    /// 报表描述
    /// </summary>
    [SugarColumn(ColumnName = "report_description", ColumnDescription = "报表描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ReportDescription { get; set; } = string.Empty;

    /// <summary>
    /// 选择屏幕配置(JSON格式，定义查询条件字段)
    /// </summary>
    [SugarColumn(ColumnName = "selection_screen_config", ColumnDescription = "选择屏幕配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SelectionScreenConfig { get; set; } = string.Empty;

    /// <summary>
    /// 数据源类型(CDS视图/数据库表/存储过程/BAPI函数/API/其他)
    /// </summary>
    [SugarColumn(ColumnName = "data_source_type", ColumnDescription = "数据源类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DataSourceType { get; set; } = string.Empty;

    /// <summary>
    /// 数据源名称（表名/视图名/过程名）
    /// </summary>
    [SugarColumn(ColumnName = "data_source_name", ColumnDescription = "数据源名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DataSourceName { get; set; } = string.Empty;

    /// <summary>
    /// SQL查询语句
    /// </summary>
    [SugarColumn(ColumnName = "sql_query", ColumnDescription = "SQL查询语句", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SqlQuery { get; set; } = string.Empty;

    /// <summary>
    /// 输出类型(ALV列表/ALV网格/SmartForms/SAPscript/普通列表/图表)
    /// </summary>
    [SugarColumn(ColumnName = "output_type", ColumnDescription = "输出类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string OutputType { get; set; } = string.Empty;

    /// <summary>
    /// ALV列配置(JSON格式)
    /// </summary>
    [SugarColumn(ColumnName = "alv_column_config", ColumnDescription = "ALV列配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string AlvColumnConfig { get; set; } = string.Empty;

    /// <summary>
    /// 默认布局变式（用户可保存个性化布局）
    /// </summary>
    [SugarColumn(ColumnName = "default_layout_variant", ColumnDescription = "默认布局变式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DefaultLayoutVariant { get; set; } = string.Empty;

    /// <summary>
    /// 是否支持布局变式(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "support_layout_variant", ColumnDescription = "是否支持布局变式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int SupportLayoutVariant { get; set; } = 1;

    /// <summary>
    /// 小计字段配置(JSON数组)
    /// </summary>
    [SugarColumn(ColumnName = "subtotal_fields", ColumnDescription = "小计字段配置", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SubtotalFields { get; set; } = string.Empty;

    /// <summary>
    /// 排序字段配置(JSON数组)
    /// </summary>
    [SugarColumn(ColumnName = "sort_fields", ColumnDescription = "排序字段配置", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SortFields { get; set; } = string.Empty;

    /// <summary>
    /// 过滤条件配置(JSON格式)
    /// </summary>
    [SugarColumn(ColumnName = "filter_config", ColumnDescription = "过滤条件配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string FilterConfig { get; set; } = string.Empty;

    /// <summary>
    /// 是否支持总计(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "support_total", ColumnDescription = "是否支持总计", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int SupportTotal { get; set; } = 1;

    /// <summary>
    /// 是否支持小计(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "support_subtotal", ColumnDescription = "是否支持小计", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int SupportSubtotal { get; set; } = 1;

    /// <summary>
    /// 是否支持汇总(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "support_aggregation", ColumnDescription = "是否支持汇总", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int SupportAggregation { get; set; } = 1;

    /// <summary>
    /// 是否支持钻取(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "support_drill_down", ColumnDescription = "是否支持钻取", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SupportDrillDown { get; set; } = 0;

    /// <summary>
    /// 钻取目标报表编码
    /// </summary>
    [SugarColumn(ColumnName = "drill_down_report_code", ColumnDescription = "钻取目标报表编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DrillDownReportCode { get; set; } = string.Empty;

    /// <summary>
    /// 是否支持后台执行(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "support_background", ColumnDescription = "是否支持后台执行", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SupportBackground { get; set; } = 0;

    /// <summary>
    /// 是否支持变式保存(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "support_variant_save", ColumnDescription = "是否支持变式保存", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int SupportVariantSave { get; set; } = 1;

    /// <summary>
    /// 默认页大小
    /// </summary>
    [SugarColumn(ColumnName = "default_page_size", ColumnDescription = "默认页大小", ColumnDataType = "int", IsNullable = false, DefaultValue = "100")]
    public int DefaultPageSize { get; set; } = 100;

    /// <summary>
    /// 最大数据行数(0=不限制)
    /// </summary>
    [SugarColumn(ColumnName = "max_row_count", ColumnDescription = "最大数据行数", ColumnDataType = "int", IsNullable = false, DefaultValue = "10000")]
    public int MaxRowCount { get; set; } = 10000;

    /// <summary>
    /// 是否支持导出(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "is_exportable", ColumnDescription = "是否支持导出", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsExportable { get; set; } = 1;

    /// <summary>
    /// 导出格式配置(JSON数组：Excel/PDF/CSV/TXT)
    /// </summary>
    [SugarColumn(ColumnName = "export_formats", ColumnDescription = "导出格式配置", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ExportFormats { get; set; } = string.Empty;

    /// <summary>
    /// 是否支持打印(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "is_printable", ColumnDescription = "是否支持打印", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsPrintable { get; set; } = 1;

    /// <summary>
    /// 打印模板名称
    /// </summary>
    [SugarColumn(ColumnName = "print_template", ColumnDescription = "打印模板", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PrintTemplate { get; set; } = string.Empty;

    /// <summary>
    /// 适用工厂代码(逗号分隔)
    /// </summary>
    [SugarColumn(ColumnName = "applicable_plant_codes", ColumnDescription = "适用工厂代码", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicablePlantCodes { get; set; } = string.Empty;

    /// <summary>
    /// 适用公司代码(逗号分隔)
    /// </summary>
    [SugarColumn(ColumnName = "applicable_company_codes", ColumnDescription = "适用公司代码", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicableCompanyCodes { get; set; } = string.Empty;

    /// <summary>
    /// 适用部门
    /// </summary>
    [SugarColumn(ColumnName = "applicable_department", ColumnDescription = "适用部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicableDepartment { get; set; } = string.Empty;

    /// <summary>
    /// 适用角色(逗号分隔)
    /// </summary>
    [SugarColumn(ColumnName = "applicable_roles", ColumnDescription = "适用角色", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicableRoles { get; set; } = string.Empty;

    /// <summary>
    /// 开发类（SAP风格：如 ZMM、ZPP）
    /// </summary>
    [SugarColumn(ColumnName = "development_class", ColumnDescription = "开发类", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DevelopmentClass { get; set; } = string.Empty;

    /// <summary>
    /// 作者
    /// </summary>
    [SugarColumn(ColumnName = "author", ColumnDescription = "作者", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// 版本
    /// </summary>
    [SugarColumn(ColumnName = "version", ColumnDescription = "版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 状态(0=激活 1=未激活 2=已废弃)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
