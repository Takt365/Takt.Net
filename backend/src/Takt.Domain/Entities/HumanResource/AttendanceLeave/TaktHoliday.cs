// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.AttendanceLeave
// 文件名称：TaktHoliday.cs
// 功能描述：假日实体，支撑考勤、薪资、排产等业务（符合 ERP/SAP/OA 假日条目模型）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.AttendanceLeave;

/// <summary>
/// Takt假日实体（假日条目，支撑考勤、薪资计算）
/// </summary>
[SugarTable("takt_humanresource_holiday", "假日表")]
[SugarIndex("ix_takt_humanresource_holiday_start_date", nameof(StartDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_holiday_end_date", nameof(EndDate), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_holiday_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_holiday_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_holiday_holiday_type", nameof(HolidayType), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_holiday_is_working_day", nameof(IsWorkingDay), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_holiday_region", nameof(Region), OrderByType.Asc)]
public class TaktHoliday : TaktEntityBase
{
    /// <summary>
    /// 地区（Region，如 CN、US、TW、HK，用于区分不同地区假日；非所有地区均为被承认的国家）
    /// </summary>
    [SugarColumn(ColumnName = "region", ColumnDescription = "地区", ColumnDataType = "nvarchar", Length = 10, IsNullable = false, DefaultValue = "CN")]
    public string Region { get; set; } = "CN";

    /// <summary>
    /// 假日名称
    /// </summary>
    [SugarColumn(ColumnName = "holiday_name", ColumnDescription = "假日名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string HolidayName { get; set; } = string.Empty;

    /// <summary>
    /// 假日类型（0=法定 1=调休 2=公司，影响薪资计算，保持多种可能性）
    /// </summary>
    [SugarColumn(ColumnName = "holiday_type", ColumnDescription = "假日类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int HolidayType { get; set; }

    /// <summary>
    /// 假日开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "假日开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 假日结束日期
    /// </summary>
    [SugarColumn(ColumnName = "end_date", ColumnDescription = "假日结束日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 是否工作日（0=非工作日 1=工作日 2=半天等，保持多种可能性）
    /// </summary>
    [SugarColumn(ColumnName = "is_working_day", ColumnDescription = "是否工作日", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsWorkingDay { get; set; }

    /// <summary>
    /// 假日问候语（简短，用于问候语行）
    /// </summary>
    [SugarColumn(ColumnName = "holiday_greeting", ColumnDescription = "假日问候语", ColumnDataType = "nvarchar", Length = 200, IsNullable = false, DefaultValue = "")]
    public string HolidayGreeting { get; set; } = string.Empty;

    /// <summary>
    /// 假日引用/诗句（用于引用区展示）
    /// </summary>
    [SugarColumn(ColumnName = "holiday_quote", ColumnDescription = "假日引用", ColumnDataType = "nvarchar", Length = 500, IsNullable = false, DefaultValue = "")]
    public string HolidayQuote { get; set; } = string.Empty;

    /// <summary>
    /// 假日主题（对应前端 themeColorMap 的 key，用于非工作日的主题展示；命名 HolidayTheme 避免与前端的 ThemeColorKey 冲突）
    /// </summary>
    [SugarColumn(ColumnName = "holiday_theme", ColumnDescription = "假日主题", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string HolidayTheme { get; set; } = string.Empty;
}