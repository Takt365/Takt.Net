// ========================================
// 项目名称:节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间:Takt.Domain.Entities.Logistics.Quality.Cost
// 文件名称:TaktQualityOperationIncoming.cs
// 创建时间:2026-05-08
// 创建人:Takt365(Qoder AI)
// 功能描述:品质业务明细 - 来料检验费用
//
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Cost;

/// <summary>
/// 品质业务明细 - 来料检验费用
/// </summary>
[SugarTable("takt_logistics_quality_operation_incoming", "品质业务来料检验费用明细表")]
[SugarIndex("ix_takt_logistics_quality_operation_incoming_quality_operation_id", nameof(QualityOperationId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_incoming_line_number", nameof(LineNumber), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_incoming_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_incoming_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktQualityOperationIncoming : TaktEntityBase
{
    /// <summary>
    /// 品质业务主表ID(主子表关系,序列化为string以避免Javascript精度问题)
    /// </summary>
    [SugarColumn(ColumnName = "quality_operation_id", ColumnDescription = "品质业务主表ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long QualityOperationId { get; set; }

    /// <summary>
    /// 项号(行号)
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "项号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 直接人员费率(元/分钟)
    /// </summary>
    [SugarColumn(ColumnName = "direct_manpower_cost_per_minute", ColumnDescription = "直接人员费率", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal DirectManpowerCostPerMinute { get; set; } = 0;

    /// <summary>
    /// 来料检验业务费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "incoming_inspection_cost", ColumnDescription = "来料检验业务费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal IncomingInspectionCost { get; set; } = 0;

    /// <summary>
    /// 检查时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "inspection_time_minutes", ColumnDescription = "检查时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InspectionTimeMinutes { get; set; } = 0;

    /// <summary>
    /// 交通费、旅费(元)
    /// </summary>
    [SugarColumn(ColumnName = "travel_cost", ColumnDescription = "交通费旅费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TravelCost { get; set; } = 0;

    /// <summary>
    /// 检查其他费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "other_expenses", ColumnDescription = "检查其他费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal OtherExpenses { get; set; } = 0;

    /// <summary>
    /// 来料检验备注
    /// </summary>
    [SugarColumn(ColumnName = "incoming_note", ColumnDescription = "来料检验备注", ColumnDataType = "ntext", IsNullable = true)]
    public string? IncomingNote { get; set; }

    /// <summary>
    /// 品质业务主表(导航属性)
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(QualityOperationId))]
    public TaktQualityOperation? Operation { get; set; }
}
