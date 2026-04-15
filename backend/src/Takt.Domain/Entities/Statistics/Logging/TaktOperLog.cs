// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logging
// 文件名称：TaktOperLog.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt操作日志实体，记录用户操作信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Statistics.Logging;

/// <summary>
/// Takt操作日志实体
/// </summary>
[SugarTable("takt_statistics_logging_oper_log", "操作日志表")]
[SugarIndex("ix_takt_statistics_logging_oper_log_user_name", nameof(UserName), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_oper_log_oper_module", nameof(OperModule), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_oper_log_oper_type", nameof(OperType), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_oper_log_oper_status", nameof(OperStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_oper_log_oper_time", nameof(OperTime), OrderByType.Desc)]
[SugarIndex("ix_takt_statistics_logging_oper_log_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_oper_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktOperLog : TaktEntityBase
{
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 操作模块
    /// </summary>
    [SugarColumn(ColumnName = "oper_module", ColumnDescription = "操作模块", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OperModule { get; set; }

    /// <summary>
    /// 操作类型（如：新增、删除、修改、查询）
    /// </summary>
    [SugarColumn(ColumnName = "oper_type", ColumnDescription = "操作类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? OperType { get; set; }

    /// <summary>
    /// 操作方法
    /// </summary>
    [SugarColumn(ColumnName = "oper_method", ColumnDescription = "操作方法", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? OperMethod { get; set; }

    /// <summary>
    /// 请求方式（如：GET、POST、PUT、DELETE）
    /// </summary>
    [SugarColumn(ColumnName = "request_method", ColumnDescription = "请求方式", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? RequestMethod { get; set; }

    /// <summary>
    /// 操作URL
    /// </summary>
    [SugarColumn(ColumnName = "oper_url", ColumnDescription = "操作URL", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? OperUrl { get; set; }

    /// <summary>
    /// 请求参数（JSON格式）
    /// </summary>
    [SugarColumn(ColumnName = "request_param", ColumnDescription = "请求参数", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? RequestParam { get; set; }

    /// <summary>
    /// 返回结果（JSON格式）
    /// </summary>
    [SugarColumn(ColumnName = "json_result", ColumnDescription = "返回结果", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? JsonResult { get; set; }

    /// <summary>
    /// 操作状态（0=成功，1=失败）
    /// </summary>
    [SugarColumn(ColumnName = "oper_status", ColumnDescription = "操作状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OperStatus { get; set; } = 0;

    /// <summary>
    /// 错误消息
    /// </summary>
    [SugarColumn(ColumnName = "error_msg", ColumnDescription = "错误消息", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? ErrorMsg { get; set; }

    /// <summary>
    /// 操作IP
    /// </summary>
    [SugarColumn(ColumnName = "oper_ip", ColumnDescription = "操作IP", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? OperIp { get; set; }

    /// <summary>
    /// 操作地点
    /// </summary>
    [SugarColumn(ColumnName = "oper_location", ColumnDescription = "操作地点", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? OperLocation { get; set; }

    /// <summary>
    /// 操作国家
    /// </summary>
    [SugarColumn(ColumnName = "oper_country", ColumnDescription = "操作国家", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OperCountry { get; set; }

    /// <summary>
    /// 操作省份
    /// </summary>
    [SugarColumn(ColumnName = "oper_province", ColumnDescription = "操作省份", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OperProvince { get; set; }

    /// <summary>
    /// 操作城市
    /// </summary>
    [SugarColumn(ColumnName = "oper_city", ColumnDescription = "操作城市", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OperCity { get; set; }

    /// <summary>
    /// 操作ISP（互联网服务提供商）
    /// </summary>
    [SugarColumn(ColumnName = "oper_isp", ColumnDescription = "操作ISP", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OperIsp { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    [SugarColumn(ColumnName = "oper_time", ColumnDescription = "操作时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime OperTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    [SugarColumn(ColumnName = "cost_time", ColumnDescription = "执行耗时", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CostTime { get; set; } = 0;
}
