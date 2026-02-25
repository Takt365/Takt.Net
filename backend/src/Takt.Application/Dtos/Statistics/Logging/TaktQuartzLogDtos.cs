// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Logging
// 文件名称：TaktQuartzLogDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt任务日志DTO，包含任务日志相关的数据传输对象（查询、导出）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// Takt任务日志DTO
/// </summary>
public class TaktQuartzLogDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogDto()
    {
        UserName = string.Empty;
        JobName = string.Empty;
        JobGroup = string.Empty;
        TriggerName = string.Empty;
        TriggerGroup = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 任务日志ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long QuartzLogId { get; set; }

    /// <summary>
    /// 租户配置ID
    /// </summary>
    public string ConfigId { get; set; }

    /// <summary>
    /// 扩展字段JSON（与实体基类一致）
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 用户名（系统任务可为"system"）
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    public string JobName { get; set; }

    /// <summary>
    /// 任务组
    /// </summary>
    public string JobGroup { get; set; }

    /// <summary>
    /// 触发器名称
    /// </summary>
    public string TriggerName { get; set; }

    /// <summary>
    /// 触发器组
    /// </summary>
    public string TriggerGroup { get; set; }

    /// <summary>
    /// 执行状态（0=成功，1=失败）
    /// </summary>
    public int ExecuteStatus { get; set; }

    /// <summary>
    /// 执行结果（JSON格式）
    /// </summary>
    public string? ExecuteResult { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMsg { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime ExecuteTime { get; set; }

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    public int CostTime { get; set; }

    /// <summary>
    /// 任务参数（JSON格式）
    /// </summary>
    public string? JobData { get; set; }

    /// <summary>
    /// 下一次执行时间
    /// </summary>
    public DateTime? NextFireTime { get; set; }

    /// <summary>
    /// 上一次执行时间
    /// </summary>
    public DateTime? PreviousFireTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// Takt任务日志查询DTO
/// </summary>
public class TaktQuartzLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在用户名、任务名称、触发器名称中模糊查询

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    public string? JobName { get; set; }

    /// <summary>
    /// 任务组
    /// </summary>
    public string? JobGroup { get; set; }

    /// <summary>
    /// 触发器名称
    /// </summary>
    public string? TriggerName { get; set; }

    /// <summary>
    /// 触发器组
    /// </summary>
    public string? TriggerGroup { get; set; }

    /// <summary>
    /// 执行状态（0=成功，1=失败）
    /// </summary>
    public int? ExecuteStatus { get; set; }

    /// <summary>
    /// 执行时间开始
    /// </summary>
    public DateTime? ExecuteTimeStart { get; set; }

    /// <summary>
    /// 执行时间结束
    /// </summary>
    public DateTime? ExecuteTimeEnd { get; set; }
}

/// <summary>
/// Takt创建任务日志DTO
/// </summary>
public class TaktCreateQuartzLogDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCreateQuartzLogDto()
    {
        UserName = string.Empty;
        JobName = string.Empty;
        JobGroup = string.Empty;
        TriggerName = string.Empty;
        TriggerGroup = string.Empty;
    }

    /// <summary>
    /// 用户名（系统任务可为"system"）
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    public string JobName { get; set; }

    /// <summary>
    /// 任务组
    /// </summary>
    public string JobGroup { get; set; }

    /// <summary>
    /// 触发器名称
    /// </summary>
    public string TriggerName { get; set; }

    /// <summary>
    /// 触发器组
    /// </summary>
    public string TriggerGroup { get; set; }

    /// <summary>
    /// 执行状态（0=成功，1=失败）
    /// </summary>
    public int ExecuteStatus { get; set; }

    /// <summary>
    /// 执行结果（JSON格式）
    /// </summary>
    public string? ExecuteResult { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMsg { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime? ExecuteTime { get; set; }

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    public int CostTime { get; set; }

    /// <summary>
    /// 任务参数（JSON格式）
    /// </summary>
    public string? JobData { get; set; }

    /// <summary>
    /// 下一次执行时间
    /// </summary>
    public DateTime? NextFireTime { get; set; }

    /// <summary>
    /// 上一次执行时间
    /// </summary>
    public DateTime? PreviousFireTime { get; set; }
}

/// <summary>
/// Takt任务日志导出DTO
/// </summary>
public class TaktQuartzLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogExportDto()
    {
        UserName = string.Empty;
        JobName = string.Empty;
        JobGroup = string.Empty;
        TriggerName = string.Empty;
        TriggerGroup = string.Empty;
        ExecuteStatus = string.Empty;
        ErrorMsg = string.Empty;
        ExecuteTime = DateTime.Now;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    public string JobName { get; set; }

    /// <summary>
    /// 任务组
    /// </summary>
    public string JobGroup { get; set; }

    /// <summary>
    /// 触发器名称
    /// </summary>
    public string TriggerName { get; set; }

    /// <summary>
    /// 触发器组
    /// </summary>
    public string TriggerGroup { get; set; }

    /// <summary>
    /// 执行状态
    /// </summary>
    public string ExecuteStatus { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMsg { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    public DateTime ExecuteTime { get; set; }

    /// <summary>
    /// 执行耗时（毫秒）
    /// </summary>
    public int CostTime { get; set; }

    /// <summary>
    /// 下一次执行时间
    /// </summary>
    public DateTime? NextFireTime { get; set; }

    /// <summary>
    /// 上一次执行时间
    /// </summary>
    public DateTime? PreviousFireTime { get; set; }
}
