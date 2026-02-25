// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Workflow
// 文件名称：TaktFlowInstance.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt流程实例实体（BPM 流程实例）；创建人/时间用基类 CreateId/CreateBy/CreateTime 表示流程发起人，本类仅增 DeptId、CurrentNodeId 等业务字段。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Workflow;

/// <summary>
/// Takt流程实例实体（BPM 流程实例）
/// </summary>
/// <remarks>
/// 对应 BPM 中的流程实例 (Process Instance) / BPMN 中的 Process Instance。
/// SchemeId 对应 processDefinitionId，BusinessKey 对应 businessKey，CurrentNodeId 对应 currentActivityId，InstanceStatus 对应 instance state。
/// 实例状态：0=Active 运行中，1=Completed 已完成，2=Terminated 已终止，3=Suspended 已挂起，4=Recalled 已撤回。
/// </remarks>
[SugarTable("takt_workflow_instance", "流程实例表")]
[SugarIndex("ix_takt_workflow_instance_instance_code", nameof(InstanceCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_workflow_instance_process_key", nameof(ProcessKey), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_scheme_id", nameof(SchemeId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_create_id", nameof(CreateId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_instance_status", nameof(InstanceStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_create_time", nameof(CreateTime), OrderByType.Desc)]
[SugarIndex("ix_takt_workflow_instance_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFlowInstance : TaktEntityBase
{
    /// <summary>
    /// 实例编码（BPM Process Instance Id 业务侧唯一标识）
    /// </summary>
    [SugarColumn(ColumnName = "instance_code", ColumnDescription = "实例编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义Key（BPM Process Definition Key，冗余便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "process_key", ColumnDescription = "流程Key", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程定义ID（BPM Process Definition Id，对应 TaktFlowScheme.Id）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_id", ColumnDescription = "流程方案ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程实例标题（BPM 实例名称/主题）
    /// </summary>
    [SugarColumn(ColumnName = "process_title", ColumnDescription = "流程标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ProcessTitle { get; set; }

    /// <summary>
    /// 业务主键（BPM Business Key，关联业务单据）
    /// </summary>
    [SugarColumn(ColumnName = "business_key", ColumnDescription = "业务主键", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 发起部门ID（BPM 组织维度；0 表示未指定）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 当前活动节点ID（BPM Current Activity Id / Token 位置）
    /// </summary>
    [SugarColumn(ColumnName = "current_node_id", ColumnDescription = "当前节点ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? CurrentNodeId { get; set; }

    /// <summary>
    /// 当前活动节点名称（BPM Current Activity Name）
    /// </summary>
    [SugarColumn(ColumnName = "current_node_name", ColumnDescription = "当前节点名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 上一活动节点ID（BPM Previous Activity Id）
    /// </summary>
    [SugarColumn(ColumnName = "previous_node_id", ColumnDescription = "上一节点ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? PreviousNodeId { get; set; }

    /// <summary>
    /// 优先级（BPM Priority，用于调度/展示）
    /// </summary>
    [SugarColumn(ColumnName = "priority", ColumnDescription = "等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Priority { get; set; } = 1;

    /// <summary>
    /// 实例状态（BPM Process Instance State：0=Active 运行中，1=Completed 已完成，2=Terminated 已终止，3=Suspended 已挂起，4=Recalled 已撤回）
    /// </summary>
    [SugarColumn(ColumnName = "instance_status", ColumnDescription = "实例状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InstanceStatus { get; set; } = 0;

    /// <summary>
    /// 当前节点处理人ID（认领后写入；空表示未认领，任何人可认领）
    /// </summary>
    [SugarColumn(ColumnName = "assignee_id", ColumnDescription = "当前处理人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? AssigneeId { get; set; }
}
