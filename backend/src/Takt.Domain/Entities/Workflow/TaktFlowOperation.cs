// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Workflow
// 文件名称：TaktFlowOperation.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt流程操作历史实体，记录流程操作历史
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Workflow;

/// <summary>
/// Takt流程操作历史实体（操作审计）
/// </summary>
/// <remarks>
/// 关联：N 操作 → 1 实例（InstanceId）、N 操作 → 1 方案（SchemeId）；提交/退回等操作可关联到具体任务（TaskId）。OperationType：0=启动，1=提交，2=退回，3=转办，4=加签，5=减签，6=撤回，7=挂起，8=恢复，9=终止，10=完成。
/// </remarks>
[SugarTable("takt_workflow_operation", "流程操作历史表")]
[SugarIndex("ix_takt_workflow_operation_instance_id", nameof(InstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_operation_scheme_id", nameof(SchemeId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_operation_operation_type", nameof(OperationType), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_operation_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_operation_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFlowOperation : TaktEntityBase
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 流程方案ID（与实例一致，便于按方案查操作）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_id", ColumnDescription = "流程方案ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 关联的待办任务ID（提交/退回等操作时对应被办结的任务，可为空如启动/撤回）
    /// </summary>
    [SugarColumn(ColumnName = "task_id", ColumnDescription = "关联任务ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? TaskId { get; set; }

    /// <summary>
    /// 流程实例编码
    /// </summary>
    [SugarColumn(ColumnName = "instance_code", ColumnDescription = "流程实例编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    [SugarColumn(ColumnName = "process_key", ColumnDescription = "流程Key", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string SchemeKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    [SugarColumn(ColumnName = "process_name", ColumnDescription = "流程名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string SchemeName { get; set; } = string.Empty;

    /// <summary>
    /// 操作类型（0=启动，1=提交，2=退回，3=转办，4=加签，5=减签，6=撤回，7=挂起，8=恢复，9=终止，10=完成）
    /// </summary>
    [SugarColumn(ColumnName = "operation_type", ColumnDescription = "操作类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OperationType { get; set; } = 0;

    /// <summary>
    /// 操作节点ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "node_id", ColumnDescription = "操作节点ID", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? NodeId { get; set; }

    /// <summary>
    /// 操作节点名称
    /// </summary>
    [SugarColumn(ColumnName = "node_name", ColumnDescription = "操作节点名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? NodeName { get; set; }

    /// <summary>
    /// 操作内容（业务数据摘要，JSON格式）
    /// </summary>
    [SugarColumn(ColumnName = "operation_content", ColumnDescription = "操作内容", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? OperationContent { get; set; }

    /// <summary>
    /// 操作意见（人工填写的审批意见等）
    /// </summary>
    [SugarColumn(ColumnName = "operation_comment", ColumnDescription = "操作意见", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? OperationComment { get; set; }

    /// <summary>
    /// 操作结果（0=成功，1=失败）
    /// </summary>
    [SugarColumn(ColumnName = "operation_result", ColumnDescription = "操作结果", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OperationResult { get; set; } = 0;

    /// <summary>
    /// 错误信息（如果操作失败）
    /// </summary>
    [SugarColumn(ColumnName = "error_message", ColumnDescription = "错误信息", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? ErrorMessage { get; set; }
}
