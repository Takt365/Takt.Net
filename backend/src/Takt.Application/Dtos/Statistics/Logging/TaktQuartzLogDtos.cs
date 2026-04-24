// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Logging
// 文件名称：TaktQuartzLogDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：任务日志表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Logging;

/// <summary>
/// 任务日志表Dto
/// </summary>
public partial class TaktQuartzLogDto : TaktDtosEntityBase
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
    }

    /// <summary>
    /// 任务日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QuartzLogId { get; set; }

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
    public int ExecuteStatus { get; set; }
    /// <summary>
    /// 执行结果
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
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }
    /// <summary>
    /// 任务参数
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
/// 任务日志表查询DTO
/// </summary>
public partial class TaktQuartzLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 任务日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QuartzLogId { get; set; }

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
    /// 执行状态
    /// </summary>
    public int? ExecuteStatus { get; set; }
    /// <summary>
    /// 执行结果
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
    /// 执行时间开始时间
    /// </summary>
    public DateTime? ExecuteTimeStart { get; set; }
    /// <summary>
    /// 执行时间结束时间
    /// </summary>
    public DateTime? ExecuteTimeEnd { get; set; }
    /// <summary>
    /// 执行耗时
    /// </summary>
    public int? CostTime { get; set; }
    /// <summary>
    /// 任务参数
    /// </summary>
    public string? JobData { get; set; }
    /// <summary>
    /// 下一次执行时间
    /// </summary>
    public DateTime? NextFireTime { get; set; }

    /// <summary>
    /// 下一次执行时间开始时间
    /// </summary>
    public DateTime? NextFireTimeStart { get; set; }
    /// <summary>
    /// 下一次执行时间结束时间
    /// </summary>
    public DateTime? NextFireTimeEnd { get; set; }
    /// <summary>
    /// 上一次执行时间
    /// </summary>
    public DateTime? PreviousFireTime { get; set; }

    /// <summary>
    /// 上一次执行时间开始时间
    /// </summary>
    public DateTime? PreviousFireTimeStart { get; set; }
    /// <summary>
    /// 上一次执行时间结束时间
    /// </summary>
    public DateTime? PreviousFireTimeEnd { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
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
/// Takt创建任务日志表DTO
/// </summary>
public partial class TaktQuartzLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogCreateDto()
    {
        UserName = string.Empty;
        JobName = string.Empty;
        JobGroup = string.Empty;
        TriggerName = string.Empty;
        TriggerGroup = string.Empty;
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
    public int ExecuteStatus { get; set; }

        /// <summary>
    /// 执行结果
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
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

        /// <summary>
    /// 任务参数
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
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新任务日志表DTO
/// </summary>
public partial class TaktQuartzLogUpdateDto : TaktQuartzLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogUpdateDto()
    {
    }

        /// <summary>
    /// 任务日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QuartzLogId { get; set; }
}

/// <summary>
/// 任务日志表执行状态DTO
/// </summary>
public partial class TaktQuartzLogExecuteStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogExecuteStatusDto()
    {
    }

        /// <summary>
    /// 任务日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long QuartzLogId { get; set; }

    /// <summary>
    /// 执行状态（0=禁用，1=启用）
    /// </summary>
    public int ExecuteStatus { get; set; }
}

/// <summary>
/// 任务日志表导入模板DTO
/// </summary>
public partial class TaktQuartzLogTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogTemplateDto()
    {
        UserName = string.Empty;
        JobName = string.Empty;
        JobGroup = string.Empty;
        TriggerName = string.Empty;
        TriggerGroup = string.Empty;
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
    public int ExecuteStatus { get; set; }

        /// <summary>
    /// 执行结果
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
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

        /// <summary>
    /// 任务参数
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
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 任务日志表导入DTO
/// </summary>
public partial class TaktQuartzLogImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogImportDto()
    {
        UserName = string.Empty;
        JobName = string.Empty;
        JobGroup = string.Empty;
        TriggerName = string.Empty;
        TriggerGroup = string.Empty;
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
    public int ExecuteStatus { get; set; }

        /// <summary>
    /// 执行结果
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
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

        /// <summary>
    /// 任务参数
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
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 任务日志表导出DTO
/// </summary>
public partial class TaktQuartzLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQuartzLogExportDto()
    {
        CreatedAt = DateTime.Now;
        UserName = string.Empty;
        JobName = string.Empty;
        JobGroup = string.Empty;
        TriggerName = string.Empty;
        TriggerGroup = string.Empty;
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
    public int ExecuteStatus { get; set; }

        /// <summary>
    /// 执行结果
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
    /// 执行耗时
    /// </summary>
    public int CostTime { get; set; }

        /// <summary>
    /// 任务参数
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
    public DateTime CreatedAt { get; set; }
}