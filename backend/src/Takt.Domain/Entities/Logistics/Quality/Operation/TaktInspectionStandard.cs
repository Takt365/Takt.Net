// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Quality
// 文件名称：TaktInspectionStandard.cs
// 功能描述：检验标准实体，定义IQC/IPQC/FQC的检验项目和标准
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Operation;

/// <summary>
/// 检验标准实体（IQC/IPQC/FQC通用）
/// </summary>
[SugarTable("takt_logistics_quality_inspection_standard", "检验标准表")]
[SugarIndex("ix_takt_logistics_quality_inspection_standard_is_unique", nameof(PlantCode), OrderByType.Asc, nameof(StandardCode), OrderByType.Asc, nameof(StandardName), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_inspection_standard_inspection_type", nameof(InspectionType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_inspection_standard_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_inspection_standard_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_inspection_standard_standard_status", nameof(StandardStatus), OrderByType.Asc)]
public class TaktInspectionStandard : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 检验标准编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "standard_code", ColumnDescription = "检验标准编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string StandardCode { get; set; } = string.Empty;

    /// <summary>
    /// 检验标准名称
    /// </summary>
    [SugarColumn(ColumnName = "standard_name", ColumnDescription = "检验标准名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string StandardName { get; set; } = string.Empty;

    /// <summary>
    /// 检验类型（0=IQC来料检验，1=IPQC过程检验，2=FQC最终检验）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_type", ColumnDescription = "检验类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InspectionType { get; set; } = 0;

    /// <summary>
    /// 物料类别编码
    /// </summary>
    [SugarColumn(ColumnName = "material_category_code", ColumnDescription = "物料类别编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string MaterialCategoryCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料类别名称
    /// </summary>
    [SugarColumn(ColumnName = "material_category_name", ColumnDescription = "物料类别名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string MaterialCategoryName { get; set; } = string.Empty;

    /// <summary>
    /// 抽样方案编码
    /// </summary>
    [SugarColumn(ColumnName = "sampling_scheme_code", ColumnDescription = "抽样方案编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? SamplingSchemeCode { get; set; }

    /// <summary>
    /// 抽样方案名称
    /// </summary>
    [SugarColumn(ColumnName = "sampling_scheme_name", ColumnDescription = "抽样方案名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? SamplingSchemeName { get; set; }

    /// <summary>
    /// 是否启用（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_enabled", ColumnDescription = "是否启用", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsEnabled { get; set; } = 1;

    /// <summary>
    /// 检验标准状态（0=草稿，1=已发布，2=已停用）
    /// </summary>
    [SugarColumn(ColumnName = "standard_status", ColumnDescription = "检验标准状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int StandardStatus { get; set; } = 0;

    /// <summary>
    /// 检验标准描述
    /// </summary>
    [SugarColumn(ColumnName = "standard_description", ColumnDescription = "检验标准描述", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? StandardDescription { get; set; }

    /// <summary>
    /// 检验标准明细列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktInspectionStandardItem.InspectionStandardId))]
    public List<TaktInspectionStandardItem>? Items { get; set; }
}
