// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceException.cs
// 创建时间：2026-04-13
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤异常记录。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤异常（缺卡、迟到等需处理条目）。
/// </summary>
[SugarTable("takt_humanresource_attendance_exception", "考勤异常表")]
[SugarIndex("ix_takt_humanresource_attendance_exception_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_exception_date", nameof(ExceptionDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_exception_status", nameof(HandleStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_exception_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_exception_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAttendanceException : TaktEntityBase
{
    /// <summary>
    /// 员工 ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 异常归属日期
    /// </summary>
    [SugarColumn(ColumnName = "exception_date", ColumnDescription = "异常日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ExceptionDate { get; set; }

    /// <summary>
    /// 异常类型：1=上班缺卡 2=下班缺卡 3=迟到 4=早退 5=旷工 9=其他
    /// </summary>
    [SugarColumn(ColumnName = "exception_type", ColumnDescription = "异常类型", ColumnDataType = "int", IsNullable = false)]
    public int ExceptionType { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    [SugarColumn(ColumnName = "summary", ColumnDescription = "说明", ColumnDataType = "nvarchar", Length = 500, IsNullable = false, DefaultValue = "")]
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// 处理状态：0=待处理 1=已处理 2=已忽略
    /// </summary>
    [SugarColumn(ColumnName = "handle_status", ColumnDescription = "处理状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int HandleStatus { get; set; }
}
