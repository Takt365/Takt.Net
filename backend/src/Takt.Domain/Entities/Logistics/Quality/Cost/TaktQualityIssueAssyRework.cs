// ========================================
// 项目名称:节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间:Takt.Domain.Entities.Logistics.Quality.Cost
// 文件名称:TaktQualityIssueAssyRework.cs
// 创建时间:2026-05-07
// 创建人:Takt365(Qoder AI)
// 功能描述:品质问题应对明细 - 组装不良改修应对(组装选别・改修费用)
//
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Cost;

/// <summary>
/// 品质问题应对明细 - 组装不良改修应对(组装选别・改修费用)
/// </summary>
[SugarTable("takt_logistics_quality_issue_assy_rework", "质量问题组装不良改修费用明细表")]
[SugarIndex("ix_takt_logistics_quality_issue_assy_rework_quality_issue_id", nameof(QualityIssueId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_issue_assy_rework_line_number", nameof(LineNumber), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_issue_assy_rework_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_issue_assy_rework_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktQualityIssueAssyRework : TaktEntityBase
{
    /// <summary>
    /// 品质问题主表ID(主子表关系,序列化为string以避免Javascript精度问题)
    /// </summary>
    [SugarColumn(ColumnName = "quality_issue_id", ColumnDescription = "品质问题主表ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long QualityIssueId { get; set; }

    /// <summary>
    /// 项号(行号)
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "项号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 组装不良内容(Parts/Components)
    /// </summary>
    [SugarColumn(ColumnName = "assy_defect_parts", ColumnDescription = "组装不良内容", ColumnDataType = "ntext", IsNullable = true)]
    public string? AssyDefectParts { get; set; }

    /// <summary>
    /// 组装选别・改修费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "assy_rework_cost", ColumnDescription = "组装选别改修费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AssyReworkCost { get; set; } = 0;

    /// <summary>
    /// 组装选别・改修时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "assy_rework_time_minutes", ColumnDescription = "组装选别改修时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AssyReworkTimeMinutes { get; set; } = 0;

    /// <summary>
    /// 组装再检查时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "assy_reinspection_time_minutes", ColumnDescription = "组装再检查时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AssyReinspectionTimeMinutes { get; set; } = 0;

    /// <summary>
    /// 组装交通费、旅费(元)
    /// </summary>
    [SugarColumn(ColumnName = "assy_travel_cost", ColumnDescription = "组装交通费旅费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AssyTravelCost { get; set; } = 0;

    /// <summary>
    /// 组装仓库管理费(元)
    /// </summary>
    [SugarColumn(ColumnName = "assy_warehouse_cost", ColumnDescription = "组装仓库管理费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AssyWarehouseCost { get; set; } = 0;

    /// <summary>
    /// 组装选别・改修其他费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "assy_other_expenses", ColumnDescription = "组装选别改修其他费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AssyOtherExpenses { get; set; } = 0;

    /// <summary>
    /// 组装选别・改修备注
    /// </summary>
    [SugarColumn(ColumnName = "assy_rework_note", ColumnDescription = "组装选别改修备注", ColumnDataType = "ntext", IsNullable = true)]
    public string? AssyReworkNote { get; set; }

    /// <summary>
    /// 组装向顾客的费用请求(元)
    /// </summary>
    [SugarColumn(ColumnName = "assy_scrap_cost", ColumnDescription = "组装向顾客费用请求", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AssyScrapCost { get; set; } = 0;

    /// <summary>
    /// 组装顾客名
    /// </summary>
    [SugarColumn(ColumnName = "assy_customer_name", ColumnDescription = "组装顾客名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? AssyCustomerName { get; set; }

    /// <summary>
    /// 组装 Debit Note No
    /// </summary>
    [SugarColumn(ColumnName = "assy_debit_note_no", ColumnDescription = "组装 Debit Note No", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? AssyDebitNoteNo { get; set; }

    /// <summary>
    /// 组装其他费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "assy_other_expenses2", ColumnDescription = "组装其他费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AssyOtherExpenses2 { get; set; } = 0;

    /// <summary>
    /// 组装备注
    /// </summary>
    [SugarColumn(ColumnName = "assy_note", ColumnDescription = "组装备注", ColumnDataType = "ntext", IsNullable = true)]
    public string? AssyNote { get; set; }

    /// <summary>
    /// 组装不良改修应对记录者
    /// </summary>
    [SugarColumn(ColumnName = "assy_recorder", ColumnDescription = "组装不良改修对应记录者", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? AssyRecorder { get; set; }

    /// <summary>
    /// 品质问题主表(导航属性)
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(QualityIssueId))]
    public TaktQualityIssue? Issue { get; set; }
}
