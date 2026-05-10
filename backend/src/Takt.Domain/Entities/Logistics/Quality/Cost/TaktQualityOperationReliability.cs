// ========================================
// 项目名称:节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间:Takt.Domain.Entities.Logistics.Quality.Cost
// 文件名称:TaktQualityOperationReliability.cs
// 创建时间:2026-05-08
// 创建人:Takt365(Qoder AI)
// 功能描述:品质业务明细 - 信赖性评价/ORT费用
//
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Cost;

/// <summary>
/// 品质业务明细 - 信赖性评价/ORT费用
/// </summary>
[SugarTable("takt_logistics_quality_operation_reliability", "品质业务信赖性评价ORT费用明细表")]
[SugarIndex("ix_takt_logistics_quality_operation_reliability_quality_operation_id", nameof(QualityOperationId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_reliability_line_number", nameof(LineNumber), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_reliability_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_reliability_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktQualityOperationReliability : TaktEntityBase
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
    /// 信赖性评价・ORT业务费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "test_cost", ColumnDescription = "信赖性评价ORT业务费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TestCost { get; set; } = 0;

    /// <summary>
    /// 评价作业时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "work_time_minutes", ColumnDescription = "评价作业时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int WorkTimeMinutes { get; set; } = 0;

    /// <summary>
    /// 评价其他费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "other_expenses", ColumnDescription = "评价其他费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal OtherExpenses { get; set; } = 0;

    /// <summary>
    /// 信赖性评价备注
    /// </summary>
    [SugarColumn(ColumnName = "reliability_note", ColumnDescription = "信赖性评价备注", ColumnDataType = "ntext", IsNullable = true)]
    public string? ReliabilityNote { get; set; }

    /// <summary>
    /// 品质业务主表(导航属性)
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(QualityOperationId))]
    public TaktQualityOperation? Operation { get; set; }
}
