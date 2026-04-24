// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktAttendanceSetting.cs
// 功能描述：考勤规则设置实体（标准上下班时间、宽限等），人事库 ConfigId。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 考勤设置（工作日规则条目，可多条并存，由 IsDefault 标识默认方案）。
/// </summary>
[SugarTable("takt_humanresource_attendance_setting", "考勤设置表")]
[SugarIndex("ix_takt_humanresource_attendance_setting_code", nameof(SettingCode), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_setting_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_attendance_setting_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAttendanceSetting : TaktEntityBase
{
    /// <summary>
    /// 方案编码（唯一）
    /// </summary>
    [SugarColumn(ColumnName = "setting_code", ColumnDescription = "方案编码", ColumnDataType = "nvarchar", Length = 64, IsNullable = false)]
    public string SettingCode { get; set; } = string.Empty;

    /// <summary>
    /// 方案名称
    /// </summary>
    [SugarColumn(ColumnName = "setting_name", ColumnDescription = "方案名称", ColumnDataType = "nvarchar", Length = 128, IsNullable = false)]
    public string SettingName { get; set; } = string.Empty;

    /// <summary>
    /// 标准上班时间点（HH:mm，24 小时制）
    /// </summary>
    [SugarColumn(ColumnName = "work_start_time", ColumnDescription = "标准上班时间", ColumnDataType = "nvarchar", Length = 8, IsNullable = false)]
    public string WorkStartTime { get; set; } = "09:00";

    /// <summary>
    /// 标准下班时间点（HH:mm）
    /// </summary>
    [SugarColumn(ColumnName = "work_end_time", ColumnDescription = "标准下班时间", ColumnDataType = "nvarchar", Length = 8, IsNullable = false)]
    public string WorkEndTime { get; set; } = "18:00";

    /// <summary>
    /// 迟到宽限（分钟）
    /// </summary>
    [SugarColumn(ColumnName = "late_grace_minutes", ColumnDescription = "迟到宽限分钟", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LateGraceMinutes { get; set; }

    /// <summary>
    /// 早退宽限（分钟）
    /// </summary>
    [SugarColumn(ColumnName = "early_leave_grace_minutes", ColumnDescription = "早退宽限分钟", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EarlyLeaveGraceMinutes { get; set; }

    /// <summary>
    /// 是否默认方案（0=否 1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_default", ColumnDescription = "是否默认", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsDefault { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; }
}
