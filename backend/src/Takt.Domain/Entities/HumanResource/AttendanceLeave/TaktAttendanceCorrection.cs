// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceCorrection.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：补卡申请/补签管理。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 补卡管理（员工申请在指定日补录上/下班打卡）。
/// </summary>
[SugarTable("takt_humanresource_attendance_correction", "补卡记录表")]
[SugarIndex("ix_takt_humanresource_attendance_correction_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_dept_id", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_date", nameof(TargetDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_status", nameof(ApprovalStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_applicant_by", nameof(ApplicantBy), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_approver_by", nameof(ApproverBy), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_handling_by", nameof(HandlingBy), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_flow_instance_id", nameof(FlowInstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAttendanceCorrection : TaktEntityBase
{
    /// <summary>
    /// 员工 ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

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
    /// 补卡归属日期
    /// </summary>
    [SugarColumn(ColumnName = "target_date", ColumnDescription = "归属日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime TargetDate { get; set; }

    /// <summary>
    /// 补卡类型：1=上班 2=下班
    /// </summary>
    [SugarColumn(ColumnName = "correction_kind", ColumnDescription = "补卡类型", ColumnDataType = "int", IsNullable = false)]
    public int CorrectionKind { get; set; }

    /// <summary>
    /// 申请补录的打卡时间
    /// </summary>
    [SugarColumn(ColumnName = "request_punch_time", ColumnDescription = "申请打卡时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime RequestPunchTime { get; set; }

    /// <summary>
    /// 申请原因
    /// </summary>
    [SugarColumn(ColumnName = "reason", ColumnDescription = "原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = false, DefaultValue = "")]
    public string Reason { get; set; } = string.Empty;

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
    /// 审批状态：0=草稿 1=待审 2=通过 3=驳回
    /// </summary>
    [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审批状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ApprovalStatus { get; set; } = 0;
}
