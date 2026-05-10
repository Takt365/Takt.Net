// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktShiftSchedule.cs
// 功能描述：排班计划（按排班类别区分部门/人员）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 排班计划（按排班类别区分部门/人员 + 日期 + 班次）。
/// 规则：
/// - ScheduleType = 0（部门排班）时，DeptId 必填，EmployeeId 为空；
/// - ScheduleType = 1（人员排班）时，EmployeeId 必填，DeptId 可空。
/// </summary>
[SugarTable("takt_humanresource_shift_schedule", "排班信息表")]
[SugarIndex("ix_takt_humanresource_shift_schedule_schedule_type", nameof(ScheduleType), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_shift_schedule_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_shift_schedule_dept_id", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_shift_schedule_schedule_date", nameof(ScheduleDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_shift_schedule_shift_id", nameof(ShiftId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_shift_schedule_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_shift_schedule_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktShiftSchedule : TaktEntityBase
{
    /// <summary>
    /// 排班类别（数据字典：0=部门，1=人员）
    /// </summary>
    [SugarColumn(ColumnName = "schedule_type", ColumnDescription = "排班类别", ColumnDataType = "tinyint", IsNullable = false)]
    public int ScheduleType { get; set; }

    /// <summary>
    /// 部门 ID（ScheduleType=0 时必填）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 员工 ID（ScheduleType=1 时必填）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 排班日期（日期部分有效）
    /// </summary>
    [SugarColumn(ColumnName = "schedule_date", ColumnDescription = "排班日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ScheduleDate { get; set; }

    /// <summary>
    /// 班次 ID（<see cref="TaktWorkShift"/>）
    /// </summary>
    [SugarColumn(ColumnName = "shift_id", ColumnDescription = "班次ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ShiftId { get; set; }
}
