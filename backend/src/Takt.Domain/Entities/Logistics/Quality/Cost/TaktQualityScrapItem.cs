// ========================================
// 项目名称:节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间:Takt.Domain.Entities.Logistics.Quality.Cost
// 文件名称:TaktQualityScrapItem.cs
// 创建时间:2026-05-08
// 创建人:Takt365(Qoder AI)
// 功能描述:品质废弃明细 - 废弃零件明细行
//
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Cost;

/// <summary>
/// 品质废弃明细 - 废弃零件明细行
/// </summary>
[SugarTable("takt_logistics_quality_scrap_item", "品质废弃明细表")]
[SugarIndex("ix_takt_logistics_quality_scrap_item_quality_scrap_id", nameof(QualityScrapId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_scrap_item_line_number", nameof(LineNumber), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_scrap_item_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_scrap_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktQualityScrapItem : TaktEntityBase
{
    /// <summary>
    /// 品质废弃主表ID(主子表关系,序列化为string以避免Javascript精度问题)
    /// </summary>
    [SugarColumn(ColumnName = "quality_scrap_id", ColumnDescription = "品质废弃主表ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long QualityScrapId { get; set; }

    /// <summary>
    /// 项号(行号)
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "项号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料名称
    /// </summary>
    [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", Length = 60, ColumnDataType = "nvarchar", IsNullable = false)]
    public string MaterialName { get; set; } = string.Empty;

    /// <summary>
    /// 废弃费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "scrap_cost", ColumnDescription = "废弃费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ScrapCost { get; set; } = 0;

    /// <summary>
    /// 废弃数量
    /// </summary>
    [SugarColumn(ColumnName = "scrap_size", ColumnDescription = "废弃数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ScrapSize { get; set; } = 0;

    /// <summary>
    /// 零件单价(元)
    /// </summary>
    [SugarColumn(ColumnName = "part_price", ColumnDescription = "零件单价", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PartPrice { get; set; } = 0;

    /// <summary>
    /// 废弃处理费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "scrap_reason_cost", ColumnDescription = "废弃处理费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ScrapReasonCost { get; set; } = 0;

    /// <summary>
    /// 运费(元)
    /// </summary>
    [SugarColumn(ColumnName = "freight_charges", ColumnDescription = "运费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal FreightCharges { get; set; } = 0;

    /// <summary>
    /// 其他费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "other_expenses", ColumnDescription = "其他费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal OtherExpenses { get; set; } = 0;

    /// <summary>
    /// 处理作业时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "reason_work_time_minutes", ColumnDescription = "处理作业时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ReasonWorkTimeMinutes { get; set; } = 0;

    /// <summary>
    /// 关税(元)
    /// </summary>
    [SugarColumn(ColumnName = "tax", ColumnDescription = "关税", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal Tax { get; set; } = 0;

    /// <summary>
    /// 处理发生其他费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "reason_other_expenses", ColumnDescription = "处理发生其他费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ReasonOtherExpenses { get; set; } = 0;

    /// <summary>
    /// 废弃备注
    /// </summary>
    [SugarColumn(ColumnName = "scrap_note", ColumnDescription = "废弃备注", ColumnDataType = "ntext", IsNullable = true)]
    public string? ScrapNote { get; set; }

    /// <summary>
    /// 品质废弃主表(导航属性)
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(QualityScrapId))]
    public TaktQualityScrap? Scrap { get; set; }
}
