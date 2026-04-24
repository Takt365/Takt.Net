// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceResult.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤日结结果（计算后的出勤状态）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤结果（按员工、归属日的汇总结果）。
/// </summary>
[SugarTable("takt_humanresource_attendance_result", "考勤结果表")]
[SugarIndex("ix_takt_humanresource_attendance_result_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_result_date", nameof(AttendanceDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_result_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_result_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAttendanceResult : TaktEntityBase
{
    /// <summary>
    /// 员工 ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 归属考勤日期
    /// </summary>
    [SugarColumn(ColumnName = "attendance_date", ColumnDescription = "考勤日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime AttendanceDate { get; set; }

    /// <summary>
    /// 排班 ID（可选）
    /// </summary>
    [SugarColumn(ColumnName = "shift_schedule_id", ColumnDescription = "排班ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ShiftScheduleId { get; set; }

    /// <summary>
    /// 出勤状态：0=正常 1=迟到 2=早退 3=缺卡 4=旷工 5=加班
    /// </summary>
    [SugarColumn(ColumnName = "attendance_status", ColumnDescription = "出勤状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AttendanceStatus { get; set; }

    /// <summary>
    /// 首次上班时间
    /// </summary>
    [SugarColumn(ColumnName = "first_in_time", ColumnDescription = "上班打卡", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? FirstInTime { get; set; }

    /// <summary>
    /// 末次下班时间
    /// </summary>
    [SugarColumn(ColumnName = "last_out_time", ColumnDescription = "下班打卡", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LastOutTime { get; set; }

    /// <summary>
    /// 计薪/计出勤分钟数（业务写入）
    /// </summary>
    [SugarColumn(ColumnName = "work_minutes", ColumnDescription = "出勤分钟", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int WorkMinutes { get; set; }

    /// <summary>
    /// 结果计算完成时间
    /// </summary>
    [SugarColumn(ColumnName = "calculated_at", ColumnDescription = "计算时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? CalculatedAt { get; set; }
}
