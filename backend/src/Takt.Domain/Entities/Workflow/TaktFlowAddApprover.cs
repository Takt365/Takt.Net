// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Workflow
// 文件名称：TaktFlowAddApprover.cs
// 功能描述：流程加签审批人（Add approval，特殊流程需本实体）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Workflow;

/// <summary>
/// 流程加签审批人（Add approval）
/// </summary>
/// <remarks>
/// 加签：在既定的审批流程中，临时增加一个审批环节或审批人，需本实体记录。会签为通用流程不另述。
/// ApproveType：sequential=顺序，all=并行且，one=并行或。关联：N → 1 实例（InstanceId）、按节点（ActivityId）分组。
/// </remarks>
[SugarTable("takt_workflow_add_approver", "流程加签审批人表")]
[SugarIndex("ix_takt_workflow_add_approver_instance_id", nameof(InstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_add_approver_activity_id", nameof(ActivityId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_add_approver_approver_id", nameof(ApproverUserId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_add_approver_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_workflow_add_approver_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFlowAddApprover : TaktEntityBase
{
    /// <summary>流程实例ID</summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>节点ID（流程定义中的节点 id）</summary>
    [SugarColumn(ColumnName = "activity_id", ColumnDescription = "节点ID", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ActivityId { get; set; } = string.Empty;

    /// <summary>审批人用户ID</summary>
    [SugarColumn(ColumnName = "approver_user_id", ColumnDescription = "审批人用户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ApproverUserId { get; set; }

    /// <summary>审批人姓名</summary>
    [SugarColumn(ColumnName = "approver_user_name", ColumnDescription = "审批人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ApproverUserName { get; set; } = string.Empty;

    /// <summary>审批类型：sequential=顺序，all=并行且，one=并行或</summary>
    [SugarColumn(ColumnName = "approve_type", ColumnDescription = "审批类型", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? ApproveType { get; set; }

    /// <summary>顺序号</summary>
    [SugarColumn(ColumnName = "order_no", ColumnDescription = "顺序号", ColumnDataType = "int", IsNullable = true)]
    public int? OrderNo { get; set; }

    /// <summary>状态：0未处理，1通过，2未通过，3驳回</summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; }

    /// <summary>审批意见</summary>
    [SugarColumn(ColumnName = "verify_comment", ColumnDescription = "审批意见", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? VerifyComment { get; set; }

    /// <summary>审批时间</summary>
    [SugarColumn(ColumnName = "verify_time", ColumnDescription = "审批时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? VerifyTime { get; set; }

    /// <summary>加签原因</summary>
    [SugarColumn(ColumnName = "reason", ColumnDescription = "加签原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Reason { get; set; }

    /// <summary>加签发起人用户ID</summary>
    [SugarColumn(ColumnName = "create_user_id", ColumnDescription = "加签发起人用户ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? CreateUserId { get; set; }

    /// <summary>加签发起人姓名</summary>
    [SugarColumn(ColumnName = "create_user_name", ColumnDescription = "加签发起人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CreateUserName { get; set; }

    /// <summary>加签完成后是否回到原节点再审批</summary>
    [SugarColumn(ColumnName = "return_to_sign_node", ColumnDescription = "加签完成后是否回原节点", ColumnDataType = "bit", IsNullable = true)]
    public bool? ReturnToSignNode { get; set; }
}
