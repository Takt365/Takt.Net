// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Workflow
// 文件名称：TaktFlowOperation.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：用户对工作流实例执行的操作记录（如提交、驳回、转交、挂起等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Workflow;

/// <summary>
/// 工作流操作记录实体（用户对工作流实例执行的操作）
/// </summary>
/// <remarks>
/// 用户对工作流实例执行的操作（如提交、驳回、转交、挂起等）。记录每次操作的类型、操作人、节点、内容与结果，用于审计与追溯。
/// 操作类型：0=启动，1=提交，2=退回，3=转办，4=加签，5=减签，6=撤回，7=挂起，8=恢复，9=终止，10=完成，11=撤销审批，12=挂起，13=恢复，14=退回。
/// </remarks>
[SugarTable("takt_workflow_operation", "工作流操作记录表")]
[SugarIndex("ix_takt_workflow_operation_instance_id", nameof(InstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_operation_operation_type", nameof(OperationType), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_operation_operation_time", nameof(OperationTime), OrderByType.Desc)]
[SugarIndex("ix_takt_workflow_operation_operator_id", nameof(OperatorId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_operation_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_operation_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFlowOperation : TaktEntityBase
{
    /// <summary>
    /// 流程实例ID（BPM Process Instance Id；序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 流程实例编码（冗余便于展示）
    /// </summary>
    [SugarColumn(ColumnName = "instance_code", ColumnDescription = "流程实例编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义Key（冗余便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "process_key", ColumnDescription = "流程Key", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义名称（冗余便于展示）
    /// </summary>
    [SugarColumn(ColumnName = "process_name", ColumnDescription = "流程名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 操作类型（BPM Operation Type：0=启动，1=提交，2=退回，3=转办，4=加签，5=减签，6=撤回，7=挂起，8=恢复，9=终止，10=完成）
    /// </summary>
    [SugarColumn(ColumnName = "operation_type", ColumnDescription = "操作类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OperationType { get; set; } = 0;

    /// <summary>
    /// 操作时间（BPM Operation Timestamp）
    /// </summary>
    [SugarColumn(ColumnName = "operation_time", ColumnDescription = "操作时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime OperationTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 操作人ID（BPM Actor / User Id；序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "operator_id", ColumnDescription = "操作人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OperatorId { get; set; }

    /// <summary>
    /// 操作人姓名（BPM Actor Name）
    /// </summary>
    [SugarColumn(ColumnName = "operator_name", ColumnDescription = "操作人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OperatorName { get; set; } = string.Empty;

    /// <summary>
    /// 操作人部门ID（BPM Actor Org；序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "operator_dept_id", ColumnDescription = "操作部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? OperatorDeptId { get; set; }

    /// <summary>
    /// 操作人部门名称
    /// </summary>
    [SugarColumn(ColumnName = "operator_dept_name", ColumnDescription = "操作部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OperatorDeptName { get; set; }

    /// <summary>
    /// 操作所在活动节点ID（BPM Activity Id）
    /// </summary>
    [SugarColumn(ColumnName = "node_id", ColumnDescription = "操作节点ID", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? NodeId { get; set; }

    /// <summary>
    /// 操作所在活动节点名称（BPM Activity Name）
    /// </summary>
    [SugarColumn(ColumnName = "node_name", ColumnDescription = "操作节点名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? NodeName { get; set; }

    /// <summary>
    /// 操作内容（BPM Operation Description）
    /// </summary>
    [SugarColumn(ColumnName = "operation_content", ColumnDescription = "操作内容", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? OperationContent { get; set; }

    /// <summary>
    /// 操作意见（BPM Comment）
    /// </summary>
    [SugarColumn(ColumnName = "operation_comment", ColumnDescription = "操作意见", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? OperationComment { get; set; }

    /// <summary>
    /// 操作前状态（BPM 审计用，JSON 存储操作前流程状态快照）
    /// </summary>
    [SugarColumn(ColumnName = "before_status", ColumnDescription = "操作前状态", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? BeforeStatus { get; set; }

    /// <summary>
    /// 操作后状态（BPM 审计用，JSON 存储操作后流程状态快照）
    /// </summary>
    [SugarColumn(ColumnName = "after_status", ColumnDescription = "操作后状态", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? AfterStatus { get; set; }

    /// <summary>
    /// 操作IP（BPM 审计用）
    /// </summary>
    [SugarColumn(ColumnName = "operation_ip", ColumnDescription = "操作IP地址", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? OperationIp { get; set; }

    /// <summary>
    /// 操作设备（BPM 审计用）
    /// </summary>
    [SugarColumn(ColumnName = "operation_device", ColumnDescription = "操作设备信息", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? OperationDevice { get; set; }

    /// <summary>
    /// 操作结果（BPM 审计用：0=成功，1=失败）
    /// </summary>
    [SugarColumn(ColumnName = "operation_result", ColumnDescription = "操作结果", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OperationResult { get; set; } = 0;

    /// <summary>
    /// 错误信息（BPM 审计用，操作失败时记录）
    /// </summary>
    [SugarColumn(ColumnName = "error_message", ColumnDescription = "错误信息", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? ErrorMessage { get; set; }
}
