// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktWorkShift.cs
// 功能描述：班次定义实体（排班管理-班次库）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// 班次定义（如早班、中班、夜班）。
/// </summary>
[SugarTable("takt_humanresource_work_shift", "班次表")]
[SugarIndex("ix_takt_humanresource_work_shift_code", nameof(ShiftCode), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_work_shift_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_work_shift_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktWorkShift : TaktEntityBase
{
    /// <summary>
    /// 班次编码（唯一）
    /// </summary>
    [SugarColumn(ColumnName = "shift_code", ColumnDescription = "班次编码", ColumnDataType = "nvarchar", Length = 64, IsNullable = false)]
    public string ShiftCode { get; set; } = string.Empty;

    /// <summary>
    /// 班次名称
    /// </summary>
    [SugarColumn(ColumnName = "shift_name", ColumnDescription = "班次名称", ColumnDataType = "nvarchar", Length = 128, IsNullable = false)]
    public string ShiftName { get; set; } = string.Empty;

    /// <summary>
    /// 当班开始时间（HH:mm）
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", ColumnDataType = "nvarchar", Length = 8, IsNullable = false)]
    public string StartTime { get; set; } = string.Empty;

    /// <summary>
    /// 当班结束时间（HH:mm）
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", ColumnDataType = "nvarchar", Length = 8, IsNullable = false)]
    public string EndTime { get; set; } = string.Empty;

    /// <summary>
    /// 是否跨自然日（0=否 1=是）
    /// </summary>
    [SugarColumn(ColumnName = "cross_midnight", ColumnDescription = "是否跨日", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CrossMidnight { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; }
}
