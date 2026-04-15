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
[SugarTable("takt_humanresource_attendance_correction", "补卡表")]
[SugarIndex("ix_takt_humanresource_attendance_correction_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_date", nameof(TargetDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_correction_status", nameof(ApprovalStatus), OrderByType.Asc)]
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
    /// 审批状态：0=草稿 1=待审 2=通过 3=驳回
    /// </summary>
    [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审批状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ApprovalStatus { get; set; }
}
