// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowExecutionDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：流程执行记录 DTO（节点流转记录）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程执行记录 DTO（节点流转记录）
/// </summary>
public class TaktFlowExecutionDto : TaktDtoBase
{
    /// <summary>
    /// 执行记录ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ExecutionId { get; set; }

    /// <summary>
    /// 流程实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 流程方案ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 触发本流转的待办任务ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TaskId { get; set; }

    /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 源节点ID
    /// </summary>
    public string? FromNodeId { get; set; }

    /// <summary>
    /// 源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }

    /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 流转类型（0=正常流转，1=退回，2=转办，3=加签，4=减签，5=撤回）
    /// </summary>
    public int TransitionType { get; set; }

    /// <summary>
    /// 流转时间
    /// </summary>
    public DateTime TransitionTime { get; set; }

    /// <summary>
    /// 流转人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TransitionUserId { get; set; }

    /// <summary>
    /// 流转人姓名
    /// </summary>
    public string TransitionUserName { get; set; } = string.Empty;

    /// <summary>
    /// 流转部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TransitionDeptId { get; set; }

    /// <summary>
    /// 流转部门名称
    /// </summary>
    public string? TransitionDeptName { get; set; }

    /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }

    /// <summary>
    /// 流转附件（JSON）
    /// </summary>
    public string? TransitionAttachments { get; set; }

    /// <summary>
    /// 流转耗时（毫秒）
    /// </summary>
    public int ElapsedMilliseconds { get; set; }
}

/// <summary>
/// 流程执行记录查询 DTO
/// </summary>
public class TaktFlowExecutionQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 流程实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? InstanceId { get; set; }

    /// <summary>
    /// 流程Key
    /// </summary>
    public string? ProcessKey { get; set; }

    /// <summary>
    /// 实例编码
    /// </summary>
    public string? InstanceCode { get; set; }
}

/// <summary>
/// 流程执行记录导出 DTO
/// </summary>
public class TaktFlowExecutionExportDto
{
    /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }

    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 流转类型（0=正常流转，1=退回，2=转办，3=加签，4=减签，5=撤回）
    /// </summary>
    public int TransitionType { get; set; }

    /// <summary>
    /// 流转时间
    /// </summary>
    public DateTime TransitionTime { get; set; }

    /// <summary>
    /// 流转人姓名
    /// </summary>
    public string TransitionUserName { get; set; } = string.Empty;

    /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }

    /// <summary>
    /// 流转耗时（毫秒）
    /// </summary>
    public int ElapsedMilliseconds { get; set; }
}
