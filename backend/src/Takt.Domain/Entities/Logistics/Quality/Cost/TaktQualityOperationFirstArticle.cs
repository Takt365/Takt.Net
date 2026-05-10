// ========================================
// 项目名称:节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间:Takt.Domain.Entities.Logistics.Quality.Cost
// 文件名称:TaktQualityOperationFirstArticle.cs
// 创建时间:2026-05-08
// 创建人:Takt365(Qoder AI)
// 功能描述:品质业务明细 - 初期检定・定期检定费用
//
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Cost;

/// <summary>
/// 品质业务明细 - 初期检定・定期检定费用
/// </summary>
[SugarTable("takt_logistics_quality_operation_first_article", "品质业务初期定期检定费用明细表")]
[SugarIndex("ix_takt_logistics_quality_operation_first_article_quality_operation_id", nameof(QualityOperationId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_first_article_line_number", nameof(LineNumber), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_first_article_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_first_article_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktQualityOperationFirstArticle : TaktEntityBase
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
    /// 初期检定・定期检定业务费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "qualification_cost", ColumnDescription = "初期检定定期检定业务费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal QualificationCost { get; set; } = 0;

    /// <summary>
    /// 检定作业时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "work_time_minutes", ColumnDescription = "检定作业时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int WorkTimeMinutes { get; set; } = 0;

    /// <summary>
    /// 检定其他费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "other_expenses", ColumnDescription = "检定其他费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal OtherExpenses { get; set; } = 0;

    /// <summary>
    /// 检定备注
    /// </summary>
    [SugarColumn(ColumnName = "qualification_note", ColumnDescription = "检定备注", ColumnDataType = "ntext", IsNullable = true)]
    public string? QualificationNote { get; set; }

    /// <summary>
    /// 品质业务主表(导航属性)
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(QualityOperationId))]
    public TaktQualityOperation? Operation { get; set; }
}
