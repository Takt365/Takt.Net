// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logging
// 文件名称：TaktAopLog.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt差异日志实体，记录SqlSugar AOP审计日志（数据变更差异）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Statistics.Logging;

/// <summary>
/// Takt差异日志实体（AOP审计日志）
/// </summary>
[SugarTable("takt_statistics_logging_aop_log", "差异日志表")]
[SugarIndex("ix_takt_statistics_logging_aop_log_user_name", nameof(UserName), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_aop_log_oper_type", nameof(OperType), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_aop_log_table_name", nameof(TableName), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_aop_log_primary_key_id", nameof(PrimaryKeyId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_aop_log_oper_time", nameof(OperTime), OrderByType.Desc)]
[SugarIndex("ix_takt_statistics_logging_aop_log_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_aop_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAopLog : TaktEntityBase
{
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 操作类型（如：INSERT、UPDATE、DELETE）
    /// </summary>
    [SugarColumn(ColumnName = "oper_type", ColumnDescription = "操作类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OperType { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 主键ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "primary_key_id", ColumnDescription = "主键ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PrimaryKeyId { get; set; }

    /// <summary>
    /// 修改前的数据（JSON格式）
    /// </summary>
    [SugarColumn(ColumnName = "before_data", ColumnDescription = "修改前数据", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? BeforeData { get; set; }

    /// <summary>
    /// 修改后的数据（JSON格式）
    /// </summary>
    [SugarColumn(ColumnName = "after_data", ColumnDescription = "修改后数据", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? AfterData { get; set; }

    /// <summary>
    /// 差异内容（JSON格式，记录具体修改的字段和值）
    /// </summary>
    [SugarColumn(ColumnName = "diff_data", ColumnDescription = "差异内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? DiffData { get; set; }

    /// <summary>
    /// SQL语句
    /// </summary>
    [SugarColumn(ColumnName = "sql_statement", ColumnDescription = "SQL语句", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? SqlStatement { get; set; }

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
