// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Statistics.Report
// 文件名称：TaktReportExecutionLog.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：报表执行日志实体（SAP风格：支持后台作业、变式、Spool请求等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Statistics.Report;

/// <summary>
/// 报表执行日志实体（SAP风格：支持后台作业、变式、Spool请求等）
/// </summary>
[SugarTable("takt_statistics_report_execution_log", "报表执行日志表")]
[SugarIndex("ix_takt_statistics_report_execution_log_report_id", nameof(ReportId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_report_execution_log_execution_time", nameof(ExecutionTime), OrderByType.Desc)]
[SugarIndex("ix_takt_statistics_report_execution_log_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_report_execution_log_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_report_execution_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktReportExecutionLog : TaktEntityBase
{
    /// <summary>
    /// 报表定义ID
    /// </summary>
    [SugarColumn(ColumnName = "report_id", ColumnDescription = "报表定义ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ReportId { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    [SugarColumn(ColumnName = "execution_time", ColumnDescription = "执行时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ExecutionTime { get; set; }

    /// <summary>
    /// 变式名称（用户保存的选择条件）
    /// </summary>
    [SugarColumn(ColumnName = "variant_name", ColumnDescription = "变式名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string VariantName { get; set; } = string.Empty;

    /// <summary>
    /// 选择屏幕参数(JSON格式)
    /// </summary>
    [SugarColumn(ColumnName = "selection_parameters", ColumnDescription = "选择屏幕参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SelectionParameters { get; set; } = string.Empty;

    /// <summary>
    /// 布局变式名称
    /// </summary>
    [SugarColumn(ColumnName = "layout_variant", ColumnDescription = "布局变式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string LayoutVariant { get; set; } = string.Empty;

    /// <summary>
    /// 执行类型(在线/后台作业/定时任务)
    /// </summary>
    [SugarColumn(ColumnName = "execution_type", ColumnDescription = "执行类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ExecutionType { get; set; } = string.Empty;

    /// <summary>
    /// 后台作业名称
    /// </summary>
    [SugarColumn(ColumnName = "background_job_name", ColumnDescription = "后台作业名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string BackgroundJobName { get; set; } = string.Empty;

    /// <summary>
    /// 后台作业编号
    /// </summary>
    [SugarColumn(ColumnName = "background_job_count", ColumnDescription = "后台作业编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string BackgroundJobCount { get; set; } = string.Empty;

    /// <summary>
    /// 执行耗时(毫秒)
    /// </summary>
    [SugarColumn(ColumnName = "execution_duration_ms", ColumnDescription = "执行耗时", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ExecutionDurationMs { get; set; } = 0;

    /// <summary>
    /// 返回数据行数
    /// </summary>
    [SugarColumn(ColumnName = "row_count", ColumnDescription = "返回数据行数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RowCount { get; set; } = 0;

    /// <summary>
    /// 是否成功(0=失败 1=成功)
    /// </summary>
    [SugarColumn(ColumnName = "is_success", ColumnDescription = "是否成功", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsSuccess { get; set; } = 1;

    /// <summary>
    /// 错误消息
    /// </summary>
    [SugarColumn(ColumnName = "error_message", ColumnDescription = "错误消息", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// 消息类型(S=成功/E=错误/W=警告/I=信息)
    /// </summary>
    [SugarColumn(ColumnName = "message_type", ColumnDescription = "消息类型", Length = 1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string MessageType { get; set; } = string.Empty;

    /// <summary>
    /// 消息编号
    /// </summary>
    [SugarColumn(ColumnName = "message_number", ColumnDescription = "消息编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string MessageNumber { get; set; } = string.Empty;

    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 客户端IP
    /// </summary>
    [SugarColumn(ColumnName = "client_ip", ColumnDescription = "客户端IP", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ClientIp { get; set; } = string.Empty;

    /// <summary>
    /// 终端名称
    /// </summary>
    [SugarColumn(ColumnName = "terminal_name", ColumnDescription = "终端名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TerminalName { get; set; } = string.Empty;

    /// <summary>
    /// 输出类型(PRINT打印/SCREEN屏幕/FILE文件/SPOOL假脱机)
    /// </summary>
    [SugarColumn(ColumnName = "output_type", ColumnDescription = "输出类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string OutputType { get; set; } = string.Empty;

    /// <summary>
    /// Spool请求号（SAP打印队列号）
    /// </summary>
    [SugarColumn(ColumnName = "spool_request_no", ColumnDescription = "Spool请求号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SpoolRequestNo { get; set; } = string.Empty;

    /// <summary>
    /// 是否导出(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "is_export", ColumnDescription = "是否导出", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsExport { get; set; } = 0;

    /// <summary>
    /// 导出格式(Excel/PDF/CSV/TXT/HTML)
    /// </summary>
    [SugarColumn(ColumnName = "export_format", ColumnDescription = "导出格式", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ExportFormat { get; set; } = string.Empty;

    /// <summary>
    /// 导出文件路径
    /// </summary>
    [SugarColumn(ColumnName = "export_file_path", ColumnDescription = "导出文件路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ExportFilePath { get; set; } = string.Empty;

    /// <summary>
    /// 是否下载(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "is_downloaded", ColumnDescription = "是否下载", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsDownloaded { get; set; } = 0;

    /// <summary>
    /// 下载时间
    /// </summary>
    [SugarColumn(ColumnName = "download_time", ColumnDescription = "下载时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime DownloadTime { get; set; }
}
