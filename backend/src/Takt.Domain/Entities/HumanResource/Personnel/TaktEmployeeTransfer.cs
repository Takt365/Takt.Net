// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Personnel
// 文件名称：TaktEmployeeTransfer.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工调动实体（转岗/调岗），与工作流审批关联；流程 BusinessType=EmployeeTransfer、BusinessKey=本表 Id。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Personnel;

/// <summary>
/// 员工调动实体（转岗/调岗）。FlowInstanceId 存流程实例 Id，由业务方在发起流程后写入；审批通过后可同步更新 TaktEmployeeCareer 或员工部门/岗位关联。
/// </summary>
[SugarTable("takt_humanresource_employee_transfer", "员工调动表（转岗调岗）")]
[SugarIndex("ix_takt_humanresource_employee_transfer_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_transfer_flow_instance_id", nameof(FlowInstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_transfer_transfer_status", nameof(TransferStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_transfer_transfer_type", nameof(TransferType), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_transfer_effective_date", nameof(EffectiveDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_transfer_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_employee_transfer_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeeTransfer : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 调动类型（0=转岗 1=调岗）
    /// </summary>
    [SugarColumn(ColumnName = "transfer_type", ColumnDescription = "调动类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TransferType { get; set; } = 0;

    /// <summary>
    /// 原部门ID
    /// </summary>
    [SugarColumn(ColumnName = "from_dept_id", ColumnDescription = "原部门ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FromDeptId { get; set; }

    /// <summary>
    /// 原部门名称
    /// </summary>
    [SugarColumn(ColumnName = "from_dept_name", ColumnDescription = "原部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string FromDeptName { get; set; } = string.Empty;

    /// <summary>
    /// 原岗位ID
    /// </summary>
    [SugarColumn(ColumnName = "from_post_id", ColumnDescription = "原岗位ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FromPostId { get; set; }

    /// <summary>
    /// 原岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "from_post_name", ColumnDescription = "原岗位名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? FromPostName { get; set; }

    /// <summary>
    /// 目标部门ID
    /// </summary>
    [SugarColumn(ColumnName = "to_dept_id", ColumnDescription = "目标部门ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ToDeptId { get; set; }

    /// <summary>
    /// 目标部门名称
    /// </summary>
    [SugarColumn(ColumnName = "to_dept_name", ColumnDescription = "目标部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ToDeptName { get; set; } = string.Empty;

    /// <summary>
    /// 目标岗位ID
    /// </summary>
    [SugarColumn(ColumnName = "to_post_id", ColumnDescription = "目标岗位ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ToPostId { get; set; }

    /// <summary>
    /// 目标岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "to_post_name", ColumnDescription = "目标岗位名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ToPostName { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 申请事由
    /// </summary>
    [SugarColumn(ColumnName = "reason", ColumnDescription = "申请事由", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Reason { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [SugarColumn(ColumnName = "flow_instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 调动状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回
    /// </summary>
    [SugarColumn(ColumnName = "transfer_status", ColumnDescription = "调动状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TransferStatus { get; set; } = 0;
}
