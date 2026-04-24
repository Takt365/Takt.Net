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
[SugarIndex("ix_takt_humanresource_overtime_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_date", nameof(OvertimeDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_type", nameof(OvertimeType), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_status", nameof(OvertimeStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_overtime_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktOvertime : TaktEntityBase
{
    /// <summary>
    /// 员工 ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 加班归属日期
    /// </summary>
    [SugarColumn(ColumnName = "overtime_date", ColumnDescription = "加班日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime OvertimeDate { get; set; }

    /// <summary>
    /// 计划加班小时数
    /// </summary>
    [SugarColumn(ColumnName = "planned_hours", ColumnDescription = "计划小时数", ColumnDataType = "decimal", Length = 8, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PlannedHours { get; set; }

    /// <summary>
    /// 实际加班小时数（可后填）
    /// </summary>
    [SugarColumn(ColumnName = "actual_hours", ColumnDescription = "实际小时数", ColumnDataType = "decimal", Length = 8, DecimalDigits = 2, IsNullable = true)]
    public decimal? ActualHours { get; set; }

    /// <summary>
    /// 加班类型（数据字典：hr_overtime_type）
    /// </summary>
    [SugarColumn(ColumnName = "overtime_type", ColumnDescription = "加班类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OvertimeType { get; set; }

    /// <summary>
    /// 加班原因
    /// </summary>
    [SugarColumn(ColumnName = "reason", ColumnDescription = "加班原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = false, DefaultValue = "")]
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// 状态：0=草稿 1=已提交 2=已通过 3=已驳回
    /// </summary>
    [SugarColumn(ColumnName = "overtime_status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OvertimeStatus { get; set; }
}
