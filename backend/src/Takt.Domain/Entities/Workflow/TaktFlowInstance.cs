// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Workflow
// 文件名称：TaktFlowInstance.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt流程实例实体，定义工作流流程实例领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Workflow;

/// <summary>
/// Takt流程实例实体（一次流程运行）
/// </summary>
/// <remarks>
/// 关联：N 实例 → 1 方案（SchemeId）；实例 → 0..1 表单快照（FormId/FormCode，启动时取自方案）；1 实例 → N 操作记录（TaktFlowOperation）、N 执行记录（TaktFlowExecution）。待办由 MakerList 含当前用户或 TaktFlowAddApprover 查得。
/// 当前节点：CurrentNodeName 存流程定义中的节点ID；MakerList 存当前节点执行人 ID 列表（逗号分隔）。
/// 实例状态：0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回，5=草稿。
/// </remarks>
[SugarTable("takt_workflow_instance", "流程实例表")]
[SugarIndex("ix_takt_workflow_instance_instance_code", nameof(InstanceCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_workflow_instance_process_key", nameof(ProcessKey), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_scheme_id", nameof(SchemeId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_start_user_id", nameof(StartUserId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_instance_status", nameof(InstanceStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_start_time", nameof(StartTime), OrderByType.Desc)]
[SugarIndex("ix_takt_workflow_instance_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_instance_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFlowInstance : TaktEntityBase
{
    /// <summary>
    /// 实例编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "instance_code", ColumnDescription = "实例编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    [SugarColumn(ColumnName = "process_key", ColumnDescription = "流程Key", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程方案ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_id", ColumnDescription = "流程方案ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    [SugarColumn(ColumnName = "process_name", ColumnDescription = "流程名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 业务主键（调用方传入的字符串，由发起流程的业务模块约定含义；引擎不解析，不据此关联任何业务表）
    /// </summary>
    [SugarColumn(ColumnName = "business_key", ColumnDescription = "业务主键", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 业务类型（调用方传入的字符串，由发起流程的业务模块约定含义；引擎不解析，不据此关联任何业务表；新增流程仅配置方案，无需改代码）
    /// </summary>
    [SugarColumn(ColumnName = "business_type", ColumnDescription = "业务类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? BusinessType { get; set; }

    /// <summary>
    /// 启动人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "start_user_id", ColumnDescription = "启动人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long StartUserId { get; set; }

    /// <summary>
    /// 启动人姓名
    /// </summary>
    [SugarColumn(ColumnName = "start_user_name", ColumnDescription = "启动人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string StartUserName { get; set; } = string.Empty;

    /// <summary>
    /// 启动部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "start_dept_id", ColumnDescription = "启动部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? StartDeptId { get; set; }

    /// <summary>
    /// 启动部门名称
    /// </summary>
    [SugarColumn(ColumnName = "start_dept_name", ColumnDescription = "启动部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? StartDeptName { get; set; }

    /// <summary>
    /// 启动时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "启动时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 当前节点ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "current_node_id", ColumnDescription = "当前节点ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? CurrentNodeId { get; set; }

    /// <summary>
    /// 当前节点ID（流程定义中的节点 id）
    /// </summary>
    [SugarColumn(ColumnName = "current_node_name", ColumnDescription = "当前节点ID", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 当前节点名称（用于展示）
    /// </summary>
    [SugarColumn(ColumnName = "activity_name", ColumnDescription = "当前节点名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ActivityName { get; set; }

    /// <summary>
    /// 上一节点ID（流程定义中的节点 id，用于驳回等）
    /// </summary>
    [SugarColumn(ColumnName = "previous_node_id", ColumnDescription = "上一节点ID", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? PreviousNodeId { get; set; }

    /// <summary>
    /// 当前节点执行人 ID 列表（逗号分隔），待办 = MakerList 包含当前用户或加签表有待办
    /// </summary>
    [SugarColumn(ColumnName = "maker_list", ColumnDescription = "当前节点执行人ID列表", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? MakerList { get; set; }

    /// <summary>
    /// 表单数据（JSON，用于审批表单及条件分支）
    /// </summary>
    [SugarColumn(ColumnName = "frm_data", ColumnDescription = "表单数据", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? FrmData { get; set; }

    /// <summary>
    /// 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回，5=草稿）。
    /// </summary>
    [SugarColumn(ColumnName = "instance_status", ColumnDescription = "实例状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InstanceStatus { get; set; } = 0;

    /// <summary>
    /// 是否挂起（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_suspended", ColumnDescription = "是否挂起", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsSuspended { get; set; } = 0;

    /// <summary>
    /// 挂起时间
    /// </summary>
    [SugarColumn(ColumnName = "suspend_time", ColumnDescription = "挂起时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? SuspendTime { get; set; }

    /// <summary>
    /// 挂起原因
    /// </summary>
    [SugarColumn(ColumnName = "suspend_reason", ColumnDescription = "挂起原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? SuspendReason { get; set; }

    /// <summary>
    /// 优先级（0=低，1=中，2=高，3=紧急）
    /// </summary>
    [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Priority { get; set; } = 1;

    /// <summary>
    /// 流程标题
    /// </summary>
    [SugarColumn(ColumnName = "process_title", ColumnDescription = "流程标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ProcessTitle { get; set; }

    /// <summary>
    /// 流程表单ID（启动时从方案快照，用于渲染发起/审批表单）
    /// </summary>
    [SugarColumn(ColumnName = "form_id", ColumnDescription = "流程表单ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程表单编码（启动时从方案快照）
    /// </summary>
    [SugarColumn(ColumnName = "form_code", ColumnDescription = "流程表单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? FormCode { get; set; }
}
