// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Report
// 文件名称：TaktReportExecutionLogDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：报表执行日志表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Statistics.Report;

/// <summary>
/// 报表执行日志表Dto
/// </summary>
public partial class TaktReportExecutionLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportExecutionLogDto()
    {
        VariantName = string.Empty;
        SelectionParameters = string.Empty;
        LayoutVariant = string.Empty;
        ExecutionType = string.Empty;
        BackgroundJobName = string.Empty;
        BackgroundJobCount = string.Empty;
        ErrorMessage = string.Empty;
        MessageType = string.Empty;
        MessageNumber = string.Empty;
        PlantCode = string.Empty;
        CompanyCode = string.Empty;
        ClientIp = string.Empty;
        TerminalName = string.Empty;
        OutputType = string.Empty;
        SpoolRequestNo = string.Empty;
        ExportFormat = string.Empty;
        ExportFilePath = string.Empty;
    }

    /// <summary>
    /// 报表执行日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReportExecutionLogId { get; set; } = 0;

    /// <summary>
    /// 报表定义ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReportId { get; set; }
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }
    /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime ExecutionTime { get; set; }
    /// <summary>
    /// 变式名称
    /// </summary>
    public string VariantName { get; set; }
    /// <summary>
    /// 选择屏幕参数
    /// </summary>
    public string SelectionParameters { get; set; }
    /// <summary>
    /// 布局变式
    /// </summary>
    public string LayoutVariant { get; set; }
    /// <summary>
    /// 执行类型
    /// </summary>
    public string ExecutionType { get; set; }
    /// <summary>
    /// 后台作业名称
    /// </summary>
    public string BackgroundJobName { get; set; }
    /// <summary>
    /// 后台作业编号
    /// </summary>
    public string BackgroundJobCount { get; set; }
    /// <summary>
    /// 执行耗时
    /// </summary>
    public int ExecutionDurationMs { get; set; }
    /// <summary>
    /// 返回数据行数
    /// </summary>
    public int RowCount { get; set; }
    /// <summary>
    /// 是否成功
    /// </summary>
    public int IsSuccess { get; set; }
    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; }
    /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }
    /// <summary>
    /// 消息编号
    /// </summary>
    public string MessageNumber { get; set; }
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }
    /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }
    /// <summary>
    /// 终端名称
    /// </summary>
    public string TerminalName { get; set; }
    /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }
    /// <summary>
    /// Spool请求号
    /// </summary>
    public string SpoolRequestNo { get; set; }
    /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }
    /// <summary>
    /// 导出格式
    /// </summary>
    public string ExportFormat { get; set; }
    /// <summary>
    /// 导出文件路径
    /// </summary>
    public string ExportFilePath { get; set; }
    /// <summary>
    /// 是否下载
    /// </summary>
    public int IsDownloaded { get; set; }
    /// <summary>
    /// 下载时间
    /// </summary>
    public DateTime DownloadTime { get; set; }
}

/// <summary>
/// 报表执行日志表查询DTO
/// </summary>
public partial class TaktReportExecutionLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportExecutionLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 报表定义ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ReportId { get; set; }
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }
    /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime? ExecutionTime { get; set; }

    /// <summary>
    /// 执行时间开始时间
    /// </summary>
    public DateTime? ExecutionTimeStart { get; set; }
    /// <summary>
    /// 执行时间结束时间
    /// </summary>
    public DateTime? ExecutionTimeEnd { get; set; }
    /// <summary>
    /// 变式名称
    /// </summary>
    public string? VariantName { get; set; }
    /// <summary>
    /// 选择屏幕参数
    /// </summary>
    public string? SelectionParameters { get; set; }
    /// <summary>
    /// 布局变式
    /// </summary>
    public string? LayoutVariant { get; set; }
    /// <summary>
    /// 执行类型
    /// </summary>
    public string? ExecutionType { get; set; }
    /// <summary>
    /// 后台作业名称
    /// </summary>
    public string? BackgroundJobName { get; set; }
    /// <summary>
    /// 后台作业编号
    /// </summary>
    public string? BackgroundJobCount { get; set; }
    /// <summary>
    /// 执行耗时
    /// </summary>
    public int? ExecutionDurationMs { get; set; }
    /// <summary>
    /// 返回数据行数
    /// </summary>
    public int? RowCount { get; set; }
    /// <summary>
    /// 是否成功
    /// </summary>
    public int? IsSuccess { get; set; }
    /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMessage { get; set; }
    /// <summary>
    /// 消息类型
    /// </summary>
    public string? MessageType { get; set; }
    /// <summary>
    /// 消息编号
    /// </summary>
    public string? MessageNumber { get; set; }
    /// <summary>
    /// 工厂代码
    /// </summary>
    public string? PlantCode { get; set; }
    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 客户端IP
    /// </summary>
    public string? ClientIp { get; set; }
    /// <summary>
    /// 终端名称
    /// </summary>
    public string? TerminalName { get; set; }
    /// <summary>
    /// 输出类型
    /// </summary>
    public string? OutputType { get; set; }
    /// <summary>
    /// Spool请求号
    /// </summary>
    public string? SpoolRequestNo { get; set; }
    /// <summary>
    /// 是否导出
    /// </summary>
    public int? IsExport { get; set; }
    /// <summary>
    /// 导出格式
    /// </summary>
    public string? ExportFormat { get; set; }
    /// <summary>
    /// 导出文件路径
    /// </summary>
    public string? ExportFilePath { get; set; }
    /// <summary>
    /// 是否下载
    /// </summary>
    public int? IsDownloaded { get; set; }
    /// <summary>
    /// 下载时间
    /// </summary>
    public DateTime? DownloadTime { get; set; }

    /// <summary>
    /// 下载时间开始时间
    /// </summary>
    public DateTime? DownloadTimeStart { get; set; }
    /// <summary>
    /// 下载时间结束时间
    /// </summary>
    public DateTime? DownloadTimeEnd { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建报表执行日志表DTO
/// </summary>
public partial class TaktReportExecutionLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportExecutionLogCreateDto()
    {
        VariantName = string.Empty;
        SelectionParameters = string.Empty;
        LayoutVariant = string.Empty;
        ExecutionType = string.Empty;
        BackgroundJobName = string.Empty;
        BackgroundJobCount = string.Empty;
        ErrorMessage = string.Empty;
        MessageType = string.Empty;
        MessageNumber = string.Empty;
        PlantCode = string.Empty;
        CompanyCode = string.Empty;
        ClientIp = string.Empty;
        TerminalName = string.Empty;
        OutputType = string.Empty;
        SpoolRequestNo = string.Empty;
        ExportFormat = string.Empty;
        ExportFilePath = string.Empty;
    }

        /// <summary>
    /// 报表定义ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReportId { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long UserId { get; set; }

        /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime ExecutionTime { get; set; }

        /// <summary>
    /// 变式名称
    /// </summary>
    public string VariantName { get; set; }

        /// <summary>
    /// 选择屏幕参数
    /// </summary>
    public string SelectionParameters { get; set; }

        /// <summary>
    /// 布局变式
    /// </summary>
    public string LayoutVariant { get; set; }

        /// <summary>
    /// 执行类型
    /// </summary>
    public string ExecutionType { get; set; }

        /// <summary>
    /// 后台作业名称
    /// </summary>
    public string BackgroundJobName { get; set; }

        /// <summary>
    /// 后台作业编号
    /// </summary>
    public string BackgroundJobCount { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int ExecutionDurationMs { get; set; }

        /// <summary>
    /// 返回数据行数
    /// </summary>
    public int RowCount { get; set; }

        /// <summary>
    /// 是否成功
    /// </summary>
    public int IsSuccess { get; set; }

        /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; }

        /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }

        /// <summary>
    /// 消息编号
    /// </summary>
    public string MessageNumber { get; set; }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }

        /// <summary>
    /// 终端名称
    /// </summary>
    public string TerminalName { get; set; }

        /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }

        /// <summary>
    /// Spool请求号
    /// </summary>
    public string SpoolRequestNo { get; set; }

        /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }

        /// <summary>
    /// 导出格式
    /// </summary>
    public string ExportFormat { get; set; }

        /// <summary>
    /// 导出文件路径
    /// </summary>
    public string ExportFilePath { get; set; }

        /// <summary>
    /// 是否下载
    /// </summary>
    public int IsDownloaded { get; set; }

        /// <summary>
    /// 下载时间
    /// </summary>
    public DateTime DownloadTime { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

}

/// <summary>
/// Takt更新报表执行日志表DTO
/// </summary>
public partial class TaktReportExecutionLogUpdateDto : TaktReportExecutionLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportExecutionLogUpdateDto()
    {
    }

        /// <summary>
    /// 报表执行日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReportExecutionLogId { get; set; } = 0;
}

/// <summary>
/// 报表执行日志表导入模板DTO
/// </summary>
public partial class TaktReportExecutionLogTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportExecutionLogTemplateDto()
    {
        VariantName = string.Empty;
        SelectionParameters = string.Empty;
        LayoutVariant = string.Empty;
        ExecutionType = string.Empty;
        BackgroundJobName = string.Empty;
        BackgroundJobCount = string.Empty;
        ErrorMessage = string.Empty;
        MessageType = string.Empty;
        MessageNumber = string.Empty;
        PlantCode = string.Empty;
        CompanyCode = string.Empty;
        ClientIp = string.Empty;
        TerminalName = string.Empty;
        OutputType = string.Empty;
        SpoolRequestNo = string.Empty;
        ExportFormat = string.Empty;
        ExportFilePath = string.Empty;
    }

        /// <summary>
    /// 报表定义ID
    /// </summary>
    public long ReportId { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

        /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime ExecutionTime { get; set; }

        /// <summary>
    /// 变式名称
    /// </summary>
    public string VariantName { get; set; }

        /// <summary>
    /// 选择屏幕参数
    /// </summary>
    public string SelectionParameters { get; set; }

        /// <summary>
    /// 布局变式
    /// </summary>
    public string LayoutVariant { get; set; }

        /// <summary>
    /// 执行类型
    /// </summary>
    public string ExecutionType { get; set; }

        /// <summary>
    /// 后台作业名称
    /// </summary>
    public string BackgroundJobName { get; set; }

        /// <summary>
    /// 后台作业编号
    /// </summary>
    public string BackgroundJobCount { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int ExecutionDurationMs { get; set; }

        /// <summary>
    /// 返回数据行数
    /// </summary>
    public int RowCount { get; set; }

        /// <summary>
    /// 是否成功
    /// </summary>
    public int IsSuccess { get; set; }

        /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; }

        /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }

        /// <summary>
    /// 消息编号
    /// </summary>
    public string MessageNumber { get; set; }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }

        /// <summary>
    /// 终端名称
    /// </summary>
    public string TerminalName { get; set; }

        /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }

        /// <summary>
    /// Spool请求号
    /// </summary>
    public string SpoolRequestNo { get; set; }

        /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }

        /// <summary>
    /// 导出格式
    /// </summary>
    public string ExportFormat { get; set; }

        /// <summary>
    /// 导出文件路径
    /// </summary>
    public string ExportFilePath { get; set; }

        /// <summary>
    /// 是否下载
    /// </summary>
    public int IsDownloaded { get; set; }

        /// <summary>
    /// 下载时间
    /// </summary>
    public DateTime DownloadTime { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 报表执行日志表导入DTO
/// </summary>
public partial class TaktReportExecutionLogImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportExecutionLogImportDto()
    {
        VariantName = string.Empty;
        SelectionParameters = string.Empty;
        LayoutVariant = string.Empty;
        ExecutionType = string.Empty;
        BackgroundJobName = string.Empty;
        BackgroundJobCount = string.Empty;
        ErrorMessage = string.Empty;
        MessageType = string.Empty;
        MessageNumber = string.Empty;
        PlantCode = string.Empty;
        CompanyCode = string.Empty;
        ClientIp = string.Empty;
        TerminalName = string.Empty;
        OutputType = string.Empty;
        SpoolRequestNo = string.Empty;
        ExportFormat = string.Empty;
        ExportFilePath = string.Empty;
    }

        /// <summary>
    /// 报表定义ID
    /// </summary>
    public long ReportId { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

        /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime ExecutionTime { get; set; }

        /// <summary>
    /// 变式名称
    /// </summary>
    public string VariantName { get; set; }

        /// <summary>
    /// 选择屏幕参数
    /// </summary>
    public string SelectionParameters { get; set; }

        /// <summary>
    /// 布局变式
    /// </summary>
    public string LayoutVariant { get; set; }

        /// <summary>
    /// 执行类型
    /// </summary>
    public string ExecutionType { get; set; }

        /// <summary>
    /// 后台作业名称
    /// </summary>
    public string BackgroundJobName { get; set; }

        /// <summary>
    /// 后台作业编号
    /// </summary>
    public string BackgroundJobCount { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int ExecutionDurationMs { get; set; }

        /// <summary>
    /// 返回数据行数
    /// </summary>
    public int RowCount { get; set; }

        /// <summary>
    /// 是否成功
    /// </summary>
    public int IsSuccess { get; set; }

        /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; }

        /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }

        /// <summary>
    /// 消息编号
    /// </summary>
    public string MessageNumber { get; set; }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }

        /// <summary>
    /// 终端名称
    /// </summary>
    public string TerminalName { get; set; }

        /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }

        /// <summary>
    /// Spool请求号
    /// </summary>
    public string SpoolRequestNo { get; set; }

        /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }

        /// <summary>
    /// 导出格式
    /// </summary>
    public string ExportFormat { get; set; }

        /// <summary>
    /// 导出文件路径
    /// </summary>
    public string ExportFilePath { get; set; }

        /// <summary>
    /// 是否下载
    /// </summary>
    public int IsDownloaded { get; set; }

        /// <summary>
    /// 下载时间
    /// </summary>
    public DateTime DownloadTime { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 报表执行日志表导出DTO
/// </summary>
public partial class TaktReportExecutionLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReportExecutionLogExportDto()
    {
        CreatedAt = DateTime.Now;
        VariantName = string.Empty;
        SelectionParameters = string.Empty;
        LayoutVariant = string.Empty;
        ExecutionType = string.Empty;
        BackgroundJobName = string.Empty;
        BackgroundJobCount = string.Empty;
        ErrorMessage = string.Empty;
        MessageType = string.Empty;
        MessageNumber = string.Empty;
        PlantCode = string.Empty;
        CompanyCode = string.Empty;
        ClientIp = string.Empty;
        TerminalName = string.Empty;
        OutputType = string.Empty;
        SpoolRequestNo = string.Empty;
        ExportFormat = string.Empty;
        ExportFilePath = string.Empty;
    }

        /// <summary>
    /// 报表定义ID
    /// </summary>
    public long ReportId { get; set; }

        /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

        /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime ExecutionTime { get; set; }

        /// <summary>
    /// 变式名称
    /// </summary>
    public string VariantName { get; set; }

        /// <summary>
    /// 选择屏幕参数
    /// </summary>
    public string SelectionParameters { get; set; }

        /// <summary>
    /// 布局变式
    /// </summary>
    public string LayoutVariant { get; set; }

        /// <summary>
    /// 执行类型
    /// </summary>
    public string ExecutionType { get; set; }

        /// <summary>
    /// 后台作业名称
    /// </summary>
    public string BackgroundJobName { get; set; }

        /// <summary>
    /// 后台作业编号
    /// </summary>
    public string BackgroundJobCount { get; set; }

        /// <summary>
    /// 执行耗时
    /// </summary>
    public int ExecutionDurationMs { get; set; }

        /// <summary>
    /// 返回数据行数
    /// </summary>
    public int RowCount { get; set; }

        /// <summary>
    /// 是否成功
    /// </summary>
    public int IsSuccess { get; set; }

        /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; }

        /// <summary>
    /// 消息类型
    /// </summary>
    public string MessageType { get; set; }

        /// <summary>
    /// 消息编号
    /// </summary>
    public string MessageNumber { get; set; }

        /// <summary>
    /// 工厂代码
    /// </summary>
    public string PlantCode { get; set; }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }

        /// <summary>
    /// 终端名称
    /// </summary>
    public string TerminalName { get; set; }

        /// <summary>
    /// 输出类型
    /// </summary>
    public string OutputType { get; set; }

        /// <summary>
    /// Spool请求号
    /// </summary>
    public string SpoolRequestNo { get; set; }

        /// <summary>
    /// 是否导出
    /// </summary>
    public int IsExport { get; set; }

        /// <summary>
    /// 导出格式
    /// </summary>
    public string ExportFormat { get; set; }

        /// <summary>
    /// 导出文件路径
    /// </summary>
    public string ExportFilePath { get; set; }

        /// <summary>
    /// 是否下载
    /// </summary>
    public int IsDownloaded { get; set; }

        /// <summary>
    /// 下载时间
    /// </summary>
    public DateTime DownloadTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}