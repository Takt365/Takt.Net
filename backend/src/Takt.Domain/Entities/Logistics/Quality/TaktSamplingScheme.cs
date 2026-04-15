// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality
// 文件名称：TaktSamplingScheme.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt抽样方案实体，定义抽样方案领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality;

/// <summary>
/// Takt抽样方案实体
/// </summary>
[SugarTable("takt_logistics_quality_sampling_scheme", "抽样方案表")]
[SugarIndex("ix_takt_logistics_quality_sampling_scheme_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_sampling_scheme_scheme_code", nameof(SchemeCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_sampling_scheme_scheme_type", nameof(SchemeType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_sampling_scheme_inspection_level", nameof(InspectionLevel), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_sampling_scheme_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_sampling_scheme_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_sampling_scheme_scheme_status", nameof(SchemeStatus), OrderByType.Asc)]
public class TaktSamplingScheme : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 抽样方案编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_code", ColumnDescription = "抽样方案编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string SchemeCode { get; set; } = string.Empty;

    /// <summary>
    /// 抽样方案名称
    /// </summary>
    [SugarColumn(ColumnName = "scheme_name", ColumnDescription = "抽样方案名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string SchemeName { get; set; } = string.Empty;

    /// <summary>
    /// 抽样方案类型（0=计数型，1=计量型，2=计数调整型，3=计量调整型）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_type", ColumnDescription = "抽样方案类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SchemeType { get; set; } = 0;

    /// <summary>
    /// 抽样标准（0=GB/T 2828.1，1=GB/T 6378，2=MIL-STD-105E，3=ANSI/ASQ Z1.4，4=ISO 2859-1，5=自定义）
    /// </summary>
    [SugarColumn(ColumnName = "sampling_standard", ColumnDescription = "抽样标准", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SamplingStandard { get; set; } = 0;

    /// <summary>
    /// 检验水平（0=I，1=II，2=III，3=S-1，4=S-2，5=S-3，6=S-4）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_level", ColumnDescription = "检验水平", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int InspectionLevel { get; set; } = 1;

    /// <summary>
    /// AQL值（可接受质量水平，0.010-1000，存储为小数）
    /// </summary>
    [SugarColumn(ColumnName = "aql_value", ColumnDescription = "AQL值", ColumnDataType = "decimal", Length = 10, DecimalDigits = 3, IsNullable = false, DefaultValue = "2.5")]
    public decimal AqlValue { get; set; } = 2.5m;

    /// <summary>
    /// 批量范围最小值
    /// </summary>
    [SugarColumn(ColumnName = "lot_size_min", ColumnDescription = "批量范围最小值", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LotSizeMin { get; set; } = 0;

    /// <summary>
    /// 批量范围最大值（0表示无上限）
    /// </summary>
    [SugarColumn(ColumnName = "lot_size_max", ColumnDescription = "批量范围最大值", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LotSizeMax { get; set; } = 0;

    /// <summary>
    /// 样本量（抽样数量）
    /// </summary>
    [SugarColumn(ColumnName = "sample_size", ColumnDescription = "样本量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SampleSize { get; set; } = 0;

    /// <summary>
    /// 接收数（Ac，Acceptance Number）
    /// </summary>
    [SugarColumn(ColumnName = "acceptance_number", ColumnDescription = "接收数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AcceptanceNumber { get; set; } = 0;

    /// <summary>
    /// 拒收数（Re，Rejection Number）
    /// </summary>
    [SugarColumn(ColumnName = "rejection_number", ColumnDescription = "拒收数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RejectionNumber { get; set; } = 0;

    /// <summary>
    /// 检验严格度（0=正常检验，1=加严检验，2=放宽检验）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_strictness", ColumnDescription = "检验严格度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InspectionStrictness { get; set; } = 0;

    /// <summary>
    /// 是否支持转移规则（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_transfer_rule_enabled", ColumnDescription = "是否支持转移规则", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsTransferRuleEnabled { get; set; } = 0;

    /// <summary>
    /// 转移规则配置（JSON格式，存储正常/加严/放宽检验的转移条件）
    /// </summary>
    [SugarColumn(ColumnName = "transfer_rule_config", ColumnDescription = "转移规则配置", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? TransferRuleConfig { get; set; }

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_enabled", ColumnDescription = "是否启用", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsEnabled { get; set; } = 1;

    /// <summary>
    /// 抽样方案状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_status", ColumnDescription = "抽样方案状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SchemeStatus { get; set; } = 0;

    /// <summary>
    /// 抽样方案描述
    /// </summary>
    [SugarColumn(ColumnName = "scheme_description", ColumnDescription = "抽样方案描述", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? SchemeDescription { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
