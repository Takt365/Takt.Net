// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Output
// 文件名称：TaktPcbaOutputDetail.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：PCBA明细实体，按生产时段、板别等记录完成数、工数、未达成等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Output;

/// <summary>
/// PCBA明细实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_output_pcba_output_detail", "PCBA日报明细表")]
[SugarIndex("ix_takt_logistics_manufacturing_output_pcba_output_detail_pcba_output_id", nameof(PcbaOutputId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_pcba_output_detail_time_period", nameof(TimePeriod), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_pcba_output_detail_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_pcba_output_detail_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPcbaOutputDetail : TaktEntityBase
{
    /// <summary>
    /// PCBA日报ID（主表主键，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "pcba_output_id", ColumnDescription = "PCBA日报ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PcbaOutputId { get; set; }

    /// <summary>
    /// 生产时段
    /// </summary>
    [SugarColumn(ColumnName = "time_period", ColumnDescription = "生产时段", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TimePeriod { get; set; } = string.Empty;

    /// <summary>
    /// 班组
    /// </summary>
    [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班组", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int ShiftNo { get; set; } = 1;

    /// <summary>
    /// 板别（PCB板别）
    /// </summary>
    [SugarColumn(ColumnName = "pcb_board_type", ColumnDescription = "PCB板别", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PcbBoardType { get; set; } = string.Empty;

    /// <summary>
    /// 面板别
    /// </summary>
    [SugarColumn(ColumnName = "panel_side", ColumnDescription = "面板别", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PanelSide { get; set; } = string.Empty;

    /// <summary>
    /// 批次数量
    /// </summary>
    [SugarColumn(ColumnName = "batch_qty", ColumnDescription = "批次数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
    public decimal BatchQty { get; set; } = 0;

    /// <summary>
    /// 当日完成数
    /// </summary>
    [SugarColumn(ColumnName = "daily_completed_qty", ColumnDescription = "当日完成数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
    public decimal DailyCompletedQty { get; set; } = 0;

    /// <summary>
    /// 累计完成数
    /// </summary>
    [SugarColumn(ColumnName = "total_completed_qty", ColumnDescription = "累计完成数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
    public decimal TotalCompletedQty { get; set; } = 0;

    /// <summary>
    /// 完成状态
    /// </summary>
    [SugarColumn(ColumnName = "completed_status", ColumnDescription = "完成状态", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CompletedStatus { get; set; } = string.Empty;

    /// <summary>
    /// 序列号
    /// </summary>
    [SugarColumn(ColumnName = "serial_no", ColumnDescription = "序列号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SerialNo { get; set; } = string.Empty;

    /// <summary>
    /// 不良台数
    /// </summary>
    [SugarColumn(ColumnName = "defect_count", ColumnDescription = "不良台数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DefectCount { get; set; } = 0;

    /// <summary>
    /// 投入工数(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "input_minutes", ColumnDescription = "投入工数", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal InputMinutes { get; set; } = 0;

    /// <summary>
    /// 修工数(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "repair_minutes", ColumnDescription = "修工数", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal RepairMinutes { get; set; } = 0;

    /// <summary>
    /// 切换次数
    /// </summary>
    [SugarColumn(ColumnName = "switch_count", ColumnDescription = "切换次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SwitchCount { get; set; } = 0;

    /// <summary>
    /// 切换时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "switch_time", ColumnDescription = "切换时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal SwitchTime { get; set; } = 0;

    /// <summary>
    /// 切停机时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "stop_time", ColumnDescription = "切停机时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal StopTime { get; set; } = 0;

    /// <summary>
    /// 总工数(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "total_minutes", ColumnDescription = "总工数", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TotalMinutes { get; set; } = 0;

    /// <summary>
    /// 未达成原因
    /// </summary>
    [SugarColumn(ColumnName = "unachieved_reason", ColumnDescription = "未达成原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? UnachievedReason { get; set; }

    /// <summary>
    /// 未达成说明
    /// </summary>
    [SugarColumn(ColumnName = "unachieved_description", ColumnDescription = "未达成说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? UnachievedDescription { get; set; }

    /// <summary>
    /// PCBA日报（主表）
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(PcbaOutputId))]
    public TaktPcbaOutput? PcbaOutput { get; set; }
}
