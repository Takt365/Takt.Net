// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktOvertimeItem.cs
// 功能描述：加班申请明细（记录每个人员的加班信息）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 加班申请明细（一次申请可包含多个人员的加班记录）。
/// </summary>
[SugarTable("takt_humanresource_overtime_item", "加班明细表")]
[SugarIndex("ix_takt_humanresource_overtime_item_employee_actual_start_unique", nameof(EmployeeId), OrderByType.Asc, nameof(ActualStartTime), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_overtime_item_overtime_id", nameof(OvertimeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_item_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_item_line_number", nameof(LineNumber), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_item_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktOvertimeItem : TaktEntityBase
{
    /// <summary>
    /// 加班申请单ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "overtime_id", ColumnDescription = "加班申请单ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OvertimeId { get; set; }

    /// <summary>
    /// 项号（行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "项号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 员工姓名
    /// </summary>
    [SugarColumn(ColumnName = "employee_name", ColumnDescription = "员工姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string EmployeeName { get; set; } = string.Empty;

    /// <summary>
    /// 计划加班小时数
    /// </summary>
    [SugarColumn(ColumnName = "planned_hours", ColumnDescription = "计划小时数", ColumnDataType = "decimal", Length = 8, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PlannedHours { get; set; } = 0;

    /// <summary>
    /// 实际加班开始时间（默认等于主表计划开始时间）
    /// </summary>
    [SugarColumn(ColumnName = "actual_start_time", ColumnDescription = "实际开始时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ActualStartTime { get; set; }

    /// <summary>
    /// 实际加班结束时间（默认等于主表计划结束时间）
    /// </summary>
    [SugarColumn(ColumnName = "actual_end_time", ColumnDescription = "实际结束时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ActualEndTime { get; set; }

    /// <summary>
    /// 实际加班小时数（可后填，根据实际起止时间计算）
    /// </summary>
    [SugarColumn(ColumnName = "actual_hours", ColumnDescription = "实际小时数", ColumnDataType = "decimal", Length = 8, DecimalDigits = 2, IsNullable = true)]
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 加班主表（导航属性）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(OvertimeId))]
    public TaktOvertime? Overtime { get; set; }
}
