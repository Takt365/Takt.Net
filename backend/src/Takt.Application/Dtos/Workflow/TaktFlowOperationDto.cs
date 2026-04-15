// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowOperationDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：流程操作历史 DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程操作历史 DTO
/// </summary>
public class TaktFlowOperationDto : TaktDtoBase
{
    /// <summary>
    /// 操作ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OperationId { get; set; }

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
    /// 关联的待办任务ID（序列化为string以避免Javascript精度问题）
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
    /// 操作类型（0=启动，1=提交，2=退回，3=转办，4=加签，5=减签，6=撤回，7=挂起，8=恢复，9=终止，10=完成）
    /// </summary>
    public int OperationType { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperationTime { get; set; }

    /// <summary>
    /// 操作人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long OperatorId { get; set; }

    /// <summary>
    /// 操作人姓名
    /// </summary>
    public string OperatorName { get; set; } = string.Empty;

    /// <summary>
    /// 操作部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? OperatorDeptId { get; set; }

    /// <summary>
    /// 操作部门名称
    /// </summary>
    public string? OperatorDeptName { get; set; }

    /// <summary>
    /// 操作节点ID
    /// </summary>
    public string? NodeId { get; set; }

    /// <summary>
    /// 操作节点名称
    /// </summary>
    public string? NodeName { get; set; }

    /// <summary>
    /// 操作内容
    /// </summary>
    public string? OperationContent { get; set; }

    /// <summary>
    /// 操作意见
    /// </summary>
    public string? OperationComment { get; set; }

    /// <summary>
    /// 操作前状态（JSON）
    /// </summary>
    public string? BeforeStatus { get; set; }

    /// <summary>
    /// 操作后状态（JSON）
    /// </summary>
    public string? AfterStatus { get; set; }

    /// <summary>
    /// 操作IP地址
    /// </summary>
    public string? OperationIp { get; set; }

    /// <summary>
    /// 操作设备信息
    /// </summary>
    public string? OperationDevice { get; set; }

    /// <summary>
    /// 操作结果（0=成功，1=失败）
    /// </summary>
    public int OperationResult { get; set; }

    /// <summary>
    /// 错误信息（操作失败时）
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// 流程操作历史查询 DTO
/// </summary>
public class TaktFlowOperationQueryDto : TaktPagedQuery
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

    /// <summary>
    /// 操作类型（0=启动，1=提交，2=退回，3=转办，4=加签，5=减签，6=撤回，7=挂起，8=恢复，9=终止，10=完成）
    /// </summary>
    public int? OperationType { get; set; }
}

/// <summary>
/// 流程操作历史导出 DTO
/// </summary>
public class TaktFlowOperationExportDto
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
    /// 操作类型（0=启动，1=提交，2=退回，3=转办，4=加签，5=减签，6=撤回，7=挂起，8=恢复，9=终止，10=完成）
    /// </summary>
    public int OperationType { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperationTime { get; set; }

    /// <summary>
    /// 操作人姓名
    /// </summary>
    public string OperatorName { get; set; } = string.Empty;

    /// <summary>
    /// 操作节点名称
    /// </summary>
    public string? NodeName { get; set; }

    /// <summary>
    /// 操作内容
    /// </summary>
    public string? OperationContent { get; set; }

    /// <summary>
    /// 操作意见
    /// </summary>
    public string? OperationComment { get; set; }

    /// <summary>
    /// 操作结果（0=成功，1=失败）
    /// </summary>
    public int OperationResult { get; set; }
}
