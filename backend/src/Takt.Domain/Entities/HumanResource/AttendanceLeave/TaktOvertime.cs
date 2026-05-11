// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktOvertime.cs
// 功能描述：加班申请/登记。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 加班（时长与状态由业务维护，可与审批流扩展对接）。
/// </summary>
[SugarTable("takt_humanresource_overtime", "加班信息表")]
[SugarIndex("ix_takt_humanresource_overtime_dept_date_unique", nameof(DeptId), OrderByType.Asc, nameof(OvertimeDate), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_overtime_dept_id", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_date", nameof(OvertimeDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_type", nameof(OvertimeType), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_status", nameof(OvertimeStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_applicant_by", nameof(ApplicantBy), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_approver_by", nameof(ApproverBy), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_handling_by", nameof(HandlingBy), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_flow_instance_id", nameof(FlowInstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktOvertime : TaktEntityBase
{
    /// <summary>
    /// 申请人员工ID（关联 TaktEmployee，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "申请人员工ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ApplicantId { get; set; }

    /// <summary>
    /// 申请人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_by", ColumnDescription = "申请人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ApplicantBy { get; set; }

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "application_date", ColumnDescription = "申请日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ApplicationDate { get; set; } = DateTime.Now;

    /// <summary>
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DeptName { get; set; }

    /// <summary>
    /// 加班归属日期
    /// </summary>
    [SugarColumn(ColumnName = "overtime_date", ColumnDescription = "加班日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime OvertimeDate { get; set; }

    /// <summary>
    /// 计划加班开始时间
    /// </summary>
    [SugarColumn(ColumnName = "planned_start_time", ColumnDescription = "计划开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime PlannedStartTime { get; set; }

    /// <summary>
    /// 计划加班结束时间
    /// </summary>
    [SugarColumn(ColumnName = "planned_end_time", ColumnDescription = "计划结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime PlannedEndTime { get; set; }

    /// <summary>
    /// 加班总人数
    /// </summary>
    [SugarColumn(ColumnName = "total_employees", ColumnDescription = "加班总人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TotalEmployees { get; set; } = 0;

    /// <summary>
    /// 计划加班总小时数
    /// </summary>
    [SugarColumn(ColumnName = "total_planned_hours", ColumnDescription = "计划总小时数", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TotalPlannedHours { get; set; } = 0;

    /// <summary>
    /// 实际加班总小时数
    /// </summary>
    [SugarColumn(ColumnName = "total_actual_hours", ColumnDescription = "实际总小时数", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TotalActualHours { get; set; } = 0;

    /// <summary>
    /// 加班类型（数据字典：hr_overtime_type）
    /// </summary>
    [SugarColumn(ColumnName = "overtime_type", ColumnDescription = "加班类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OvertimeType { get; set; } = 0;

    /// <summary>
    /// 加班原因
    /// </summary>
    [SugarColumn(ColumnName = "reason", ColumnDescription = "加班原因", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? Reason { get; set; }

    /// <summary>
    /// 审批人员工ID（关联 TaktEmployee，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人员工ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ApproverId { get; set; }

    /// <summary>
    /// 审批人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "approver_by", ColumnDescription = "审批人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ApproverBy { get; set; }

    /// <summary>
    /// 审批时间
    /// </summary>
    [SugarColumn(ColumnName = "approve_time", ColumnDescription = "审批时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ApproveTime { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    [SugarColumn(ColumnName = "approve_comment", ColumnDescription = "审批意见", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ApproveComment { get; set; }

    /// <summary>
    /// 经办人员工ID（关联 TaktEmployee，序列化为 string 以避免 Javascript 精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "handling_id", ColumnDescription = "经办人员工ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? HandlingId { get; set; }

    /// <summary>
    /// 经办人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "handling_by", ColumnDescription = "经办人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? HandlingBy { get; set; }

    /// <summary>
    /// 经办时间
    /// </summary>
    [SugarColumn(ColumnName = "handling_time", ColumnDescription = "经办时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? HandlingTime { get; set; }

    /// <summary>
    /// 经办备注
    /// </summary>
    [SugarColumn(ColumnName = "handling_comment", ColumnDescription = "经办备注", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? HandlingComment { get; set; }

    /// <summary>
    /// 流程实例ID（关联 TaktFlowInstance，发起审批后由业务写入，用于审批流程）
    /// </summary>
    [SugarColumn(ColumnName = "flow_instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回
    /// </summary>
    [SugarColumn(ColumnName = "overtime_status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OvertimeStatus { get; set; } = 0;

    /// <summary>
    /// 加班明细列表（主子表关系，一个申请可以有多个人员的加班记录）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktOvertimeItem.OvertimeId))]
    public List<TaktOvertimeItem>? Items { get; set; }
}
