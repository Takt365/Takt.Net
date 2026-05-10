// ========================================
// 项目名称:节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间:Takt.Domain.Entities.Logistics.Quality.Cost
// 文件名称:TaktQualityScrap.cs
// 创建时间:2026-05-07
// 创建人:Takt365(Qoder AI)
// 功能描述:品质废弃主表,用于记录因品质问题导致的物料/产品报废基础信息及汇总数据
//
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Cost;

/// <summary>
/// 品质废弃主表,用于记录废弃单的基础信息(年月日、机种)及汇总数据
/// </summary>
[SugarTable("takt_logistics_quality_scrap", "品质废弃主表")]
[SugarIndex("ix_takt_logistics_quality_scrap_scrap_no", nameof(ScrapNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_scrap_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_scrap_scrap_date", nameof(ScrapDate), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_quality_scrap_model", nameof(Model), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_scrap_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_scrap_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktQualityScrap : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 废弃单号(唯一,如:QS-2026-0001)
    /// </summary>
    [SugarColumn(ColumnName = "scrap_no", ColumnDescription = "废弃单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ScrapNo { get; set; } = string.Empty;

    // ==================== 基础日期与产品信息 ====================

    /// <summary>
    /// 废弃日期
    /// </summary>
    [SugarColumn(ColumnName = "scrap_date", ColumnDescription = "废弃日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ScrapDate { get; set; }

    /// <summary>
    /// 间接人员费率(元/分钟)
    /// </summary>
    [SugarColumn(ColumnName = "indirect_manpower_cost_per_minute", ColumnDescription = "间接人员费率", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal IndirectManpowerCostPerMinute { get; set; } = 0;

    /// <summary>
    /// 机种/产品型号
    /// </summary>
    [SugarColumn(ColumnName = "model", ColumnDescription = "机种", Length = 255, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// 事故内容(废弃原因)
    /// </summary>
    [SugarColumn(ColumnName = "scrap_reason", ColumnDescription = "事故内容", ColumnDataType = "ntext", IsNullable = true)]
    public string? ScrapReason { get; set; }

    // ==================== 汇总信息 ====================

    /// <summary>
    /// 废弃总数(自动计算 = 各子表废弃数量合计)
    /// </summary>
    [SugarColumn(ColumnName = "total_scrap_quantity", ColumnDescription = "废弃总数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TotalScrapQuantity { get; set; } = 0;

    /// <summary>
    /// 总废弃费用(元,自动计算 = 各子表费用合计)
    /// </summary>
    [SugarColumn(ColumnName = "total_scrap_cost", ColumnDescription = "总废弃费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TotalScrapCost { get; set; } = 0;

    /// <summary>
    /// 成本币种(CNY/USD/JPY等)
    /// </summary>
    [SugarColumn(ColumnName = "cost_currency", ColumnDescription = "成本币种", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
    public string CostCurrency { get; set; } = "CNY";

    // ==================== 导航关系 ====================

    /// <summary>
    /// 废弃明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktQualityScrapItem.QualityScrapId))]
    public List<TaktQualityScrapItem>? ScrapItems { get; set; }
}
