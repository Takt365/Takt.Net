// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logging
// 文件名称：TaktQuartzLog.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt任务日志实体，记录Quartz任务调度日志
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Statistics.Logging;

/// <summary>
/// Takt任务日志实体
/// </summary>
[SugarTable("takt_statistics_logging_quartz_log", "任务日志表")]
[SugarIndex("ix_takt_statistics_logging_quartz_log_user_name", nameof(UserName), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_quartz_log_job_name", nameof(JobName), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_quartz_log_job_group", nameof(JobGroup), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_quartz_log_trigger_name", nameof(TriggerName), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_quartz_log_trigger_group", nameof(TriggerGroup), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_quartz_log_execute_status", nameof(ExecuteStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_quartz_log_execute_time", nameof(ExecuteTime), OrderByType.Desc)]
[SugarIndex("ix_takt_statistics_logging_quartz_log_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_logging_quartz_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktQuartzLog : TaktEntityBase
{
    /// <summary>
    /// 用户名（系统任务可为"system"）
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 任务名称
    /// </summary>
    [SugarColumn(ColumnName = "job_name", ColumnDescription = "任务名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string JobName { get; set; } = string.Empty;

    /// <summary>
    /// 任务组
    /// </summary>
    [SugarColumn(ColumnName = "job_group", ColumnDescription = "任务组", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string JobGroup { get; set; } = string.Empty;

    /// <summary>
    /// 触发器名称
    /// </summary>
    [SugarColumn(ColumnName = "trigger_name", ColumnDescription = "触发器名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string TriggerName { get; set; } = string.Empty;

    /// <summary>
    /// 触发器组
    /// </summary>
    [SugarColumn(ColumnName = "trigger_group", ColumnDescription = "触发器组", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string TriggerGroup { get; set; } = string.Empty;

    /// <summary>
    /// 执行状态（0=成功，1=失败）
    /// </summary>
    [SugarColumn(ColumnName = "execute_status", ColumnDescription = "执行状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ExecuteStatus { get; set; } = 0;

    /// <summary>
    /// 执行结果（JSON格式）
    /// </summary>
    [SugarColumn(ColumnName = "execute_result", ColumnDescription = "执行结果", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? ExecuteResult { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    [SugarColumn(ColumnName = "error_msg", ColumnDescription = "错误消息", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? ErrorMsg { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    [SugarColumn(ColumnName = "execute_time", ColumnDescription = "执行时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ExecuteTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    [SugarColumn(ColumnName = "cost_time", ColumnDescription = "执行耗时", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CostTime { get; set; } = 0;

    /// <summary>
    /// 任务参数（JSON格式）
    /// </summary>
    [SugarColumn(ColumnName = "job_data", ColumnDescription = "任务参数", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? JobData { get; set; }

    /// <summary>
    /// 下一次执行时间
    /// </summary>
    [SugarColumn(ColumnName = "next_fire_time", ColumnDescription = "下一次执行时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? NextFireTime { get; set; }

    /// <summary>
    /// 上一次执行时间
    /// </summary>
    [SugarColumn(ColumnName = "previous_fire_time", ColumnDescription = "上一次执行时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PreviousFireTime { get; set; }
}
