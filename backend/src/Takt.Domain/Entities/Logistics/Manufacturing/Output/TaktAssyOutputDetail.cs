// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Output
// 文件名称：TaktAssyOutputDetail.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：组立日报明细（产出子表）实体，按生产时段记录实际产量、停线、达成率等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Output;

/// <summary>
/// 组立日报明细（产出子表）实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_output_assy_output_detail", "组立日报明细表")]
[SugarIndex("ix_takt_logistics_manufacturing_output_assy_output_detail_assy_output_id", nameof(AssyOutputId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_assy_output_detail_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_assy_output_detail_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktAssyOutputDetail : TaktEntityBase
{
    /// <summary>
    /// 组立日报ID（主表主键，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "assy_output_id", ColumnDescription = "组立日报ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long AssyOutputId { get; set; }

    /// <summary>
    /// 生产时段
    /// </summary>
    [SugarColumn(ColumnName = "time_period", ColumnDescription = "生产时段", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TimePeriod { get; set; } = string.Empty;

    /// <summary>
    /// 实际生产数量
    /// </summary>
    [SugarColumn(ColumnName = "prod_actual_qty", ColumnDescription = "实际生产数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
    public decimal ProdActualQty { get; set; } = 0;

    /// <summary>
    /// 停线时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "downtime_minutes", ColumnDescription = "停线时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DowntimeMinutes { get; set; } = 0;

    /// <summary>
    /// 停线原因
    /// </summary>
    [SugarColumn(ColumnName = "downtime_reason", ColumnDescription = "停线原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DowntimeReason { get; set; }

    /// <summary>
    /// 停线说明
    /// </summary>
    [SugarColumn(ColumnName = "downtime_description", ColumnDescription = "停线说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DowntimeDescription { get; set; }

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
    /// 投入工时(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "input_minutes", ColumnDescription = "投入工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal InputMinutes { get; set; } = 0;

    /// <summary>
    /// 生产工时(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "prod_minutes", ColumnDescription = "生产工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ProdMinutes { get; set; } = 0;

    /// <summary>
    /// 实际工时(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "actual_minutes", ColumnDescription = "实际工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ActualMinutes { get; set; } = 0;

    /// <summary>
    /// 达成率(%)
    /// </summary>
    [SugarColumn(ColumnName = "achievement_rate", ColumnDescription = "达成率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AchievementRate { get; set; } = 0;

    /// <summary>
    /// 组立日报（主表）
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(AssyOutputId))]
    public TaktAssyOutput? AssyOutput { get; set; }
}
