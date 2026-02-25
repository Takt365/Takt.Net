// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Workflow
// 文件名称：TaktFlowExecution.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流转历史实体，记录实例从开始到结束经历的所有节点、步骤和状态变化的完整记录
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Workflow;

/// <summary>
/// 工作流流转历史实体（BPM 序列流/活动实例执行记录）
/// </summary>
/// <remarks>
/// 工作流流转历史：指工作流实例从开始到结束经历的所有节点、步骤和状态变化的完整记录。
/// 对应 BPM 中的流转记录 (Sequence Flow Execution) / 活动实例历史 (Activity Instance History)。
/// 记录每次从 From 节点到 To 节点的迁移、流转人、意见、是否结束等，用于审计与轨迹回溯。
/// </remarks>
[SugarTable("takt_workflow_execution", "工作流流转历史表")]
[SugarIndex("ix_takt_workflow_execution_instance_id", nameof(InstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_execution_from_node_id", nameof(FromNodeId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_execution_to_node_id", nameof(ToNodeId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_execution_transition_time", nameof(TransitionTime), OrderByType.Desc)]
[SugarIndex("ix_takt_workflow_execution_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_execution_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFlowExecution : TaktEntityBase
{
    /// <summary>
    /// 流程实例ID（BPM Process Instance Id；序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 流程实例编码（BPM 实例业务标识）
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
    /// 源活动节点ID（BPM From Activity Id / Sequence Flow 起点）
    /// </summary>
    [SugarColumn(ColumnName = "from_node_id", ColumnDescription = "源节点ID", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? FromNodeId { get; set; }

    /// <summary>
    /// 源活动节点名称
    /// </summary>
    [SugarColumn(ColumnName = "from_node_name", ColumnDescription = "源节点名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? FromNodeName { get; set; }

    /// <summary>
    /// 源活动节点类型（BPM Activity Type，如 UserTask、StartEvent）
    /// </summary>
    [SugarColumn(ColumnName = "from_node_type", ColumnDescription = "源节点类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? FromNodeType { get; set; }

    /// <summary>
    /// 目标活动节点ID（BPM To Activity Id / Sequence Flow 终点）
    /// </summary>
    [SugarColumn(ColumnName = "to_node_id", ColumnDescription = "目标节点ID", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ToNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 目标活动节点名称
    /// </summary>
    [SugarColumn(ColumnName = "to_node_name", ColumnDescription = "目标节点名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ToNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 目标活动节点类型（BPM Activity Type）
    /// </summary>
    [SugarColumn(ColumnName = "to_node_type", ColumnDescription = "目标节点类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ToNodeType { get; set; }

    /// <summary>
    /// 是否已结束（BPM 该次迁移后实例是否到达终态；0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_finish", ColumnDescription = "是否已结束", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsFinish { get; set; } = 0;

    /// <summary>
    /// 流转类型（BPM Transition Type：0=正常流转，1=退回，2=转办，3=加签，4=减签，5=撤回）
    /// </summary>
    [SugarColumn(ColumnName = "transition_type", ColumnDescription = "流转类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TransitionType { get; set; } = 0;

    /// <summary>
    /// 流转时间（BPM Transition Time）
    /// </summary>
    [SugarColumn(ColumnName = "transition_time", ColumnDescription = "流转时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime TransitionTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 流转人ID（BPM Actor / User Id；序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "transition_user_id", ColumnDescription = "流转人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TransitionUserId { get; set; }

    /// <summary>
    /// 流转人姓名（BPM Actor Name）
    /// </summary>
    [SugarColumn(ColumnName = "transition_user_name", ColumnDescription = "流转人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string TransitionUserName { get; set; } = string.Empty;

    /// <summary>
    /// 流转人部门ID（BPM Actor Org；序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "transition_dept_id", ColumnDescription = "流转部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? TransitionDeptId { get; set; }

    /// <summary>
    /// 流转人部门名称
    /// </summary>
    [SugarColumn(ColumnName = "transition_dept_name", ColumnDescription = "流转部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? TransitionDeptName { get; set; }

    /// <summary>
    /// 流转意见（BPM Comment / Outcome）
    /// </summary>
    [SugarColumn(ColumnName = "transition_comment", ColumnDescription = "流转意见", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? TransitionComment { get; set; }

    /// <summary>
    /// 流转附件（BPM Attachments，JSON 存储附件 ID 列表）
    /// </summary>
    [SugarColumn(ColumnName = "transition_attachments", ColumnDescription = "流转附件", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? TransitionAttachments { get; set; }

    /// <summary>
    /// 活动耗时（BPM Activity Duration，毫秒）
    /// </summary>
    [SugarColumn(ColumnName = "elapsed_milliseconds", ColumnDescription = "流转耗时（毫秒）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ElapsedMilliseconds { get; set; } = 0;
}
