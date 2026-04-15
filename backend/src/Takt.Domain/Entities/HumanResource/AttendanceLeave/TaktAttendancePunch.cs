// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktAttendancePunch.cs
// 功能描述：打卡签到原始记录。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 打卡记录（上班/下班/外勤等）。
/// </summary>
[SugarTable("takt_humanresource_attendance_punch", "打卡记录表")]
[SugarIndex("ix_takt_humanresource_attendance_punch_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_punch_time", nameof(PunchTime), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_punch_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_punch_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAttendancePunch : TaktEntityBase
{
    /// <summary>
    /// 员工 ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 打卡时间
    /// </summary>
    [SugarColumn(ColumnName = "punch_time", ColumnDescription = "打卡时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime PunchTime { get; set; }

    /// <summary>
    /// 打卡类型：1=上班 2=下班 3=外勤
    /// </summary>
    [SugarColumn(ColumnName = "punch_type", ColumnDescription = "打卡类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int PunchType { get; set; } = 1;

    /// <summary>
    /// 来源：0=后台录入 1=移动端 2=导入
    /// </summary>
    [SugarColumn(ColumnName = "punch_source", ColumnDescription = "打卡来源", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PunchSource { get; set; }

    /// <summary>
    /// 打卡地点或说明
    /// </summary>
    [SugarColumn(ColumnName = "punch_address", ColumnDescription = "打卡地点", ColumnDataType = "nvarchar", Length = 256, IsNullable = true)]
    public string? PunchAddress { get; set; }
}
