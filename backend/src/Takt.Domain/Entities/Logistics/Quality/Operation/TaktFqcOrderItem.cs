// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality
// 文件名称：TaktFqcOrderItem.cs
// 功能描述：FQC出货检验单明细实体
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Operation;

/// <summary>
/// FQC出货检验单明细实体
/// </summary>
[SugarTable("takt_logistics_quality_fqc_order_item", "出货检验单明细表")]
[SugarIndex("ix_takt_logistics_quality_fqc_order_item_fqc_order_id", nameof(FqcOrderId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_item_order_line", nameof(FqcOrderId), OrderByType.Asc, nameof(LineNumber), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_item_item_code", nameof(ItemCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFqcOrderItem : TaktEntityBase
{
    /// <summary>
    /// FQC检验单ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "fqc_order_id", ColumnDescription = "FQC检验单ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FqcOrderId { get; set; }

    /// <summary>
    /// 行号/项号（检验单明细行号，与OrderId组成联合唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 检验项目编码
    /// </summary>
    [SugarColumn(ColumnName = "item_code", ColumnDescription = "检验项目编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 检验项目名称
    /// </summary>
    [SugarColumn(ColumnName = "item_name", ColumnDescription = "检验项目名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 检验项目类型（0=外观，1=尺寸，2=性能，3=材质，4=功能）
    /// </summary>
    [SugarColumn(ColumnName = "item_type", ColumnDescription = "检验项目类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ItemType { get; set; } = 0;

    /// <summary>
    /// 检验标准值
    /// </summary>
    [SugarColumn(ColumnName = "standard_value", ColumnDescription = "检验标准值", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? StandardValue { get; set; }

    /// <summary>
    /// 检验上限值
    /// </summary>
    [SugarColumn(ColumnName = "upper_limit", ColumnDescription = "检验上限值", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? UpperLimit { get; set; }

    /// <summary>
    /// 检验下限值
    /// </summary>
    [SugarColumn(ColumnName = "lower_limit", ColumnDescription = "检验下限值", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? LowerLimit { get; set; }

    /// <summary>
    /// 检验工具
    /// </summary>
    [SugarColumn(ColumnName = "inspection_tool", ColumnDescription = "检验工具", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? InspectionTool { get; set; }

    /// <summary>
    /// 检验方法
    /// </summary>
    [SugarColumn(ColumnName = "inspection_method", ColumnDescription = "检验方法", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? InspectionMethod { get; set; }

    /// <summary>
    /// 实际检验值
    /// </summary>
    [SugarColumn(ColumnName = "actual_value", ColumnDescription = "实际检验值", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ActualValue { get; set; }

    /// <summary>
    /// 检验结果（0=待检验，1=合格，2=不合格，3=不适用）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_result", ColumnDescription = "检验结果", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InspectionResult { get; set; } = 0;

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "defect_quantity", ColumnDescription = "不良数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DefectQuantity { get; set; } = 0;

    /// <summary>
    /// 不良描述
    /// </summary>
    [SugarColumn(ColumnName = "defect_description", ColumnDescription = "不良描述", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? DefectDescription { get; set; }

    /// <summary>
    /// 检验员（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "inspector_by", ColumnDescription = "检验员", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? InspectorBy { get; set; }

    /// <summary>
    /// 检验时间
    /// </summary>
    [SugarColumn(ColumnName = "inspection_time", ColumnDescription = "检验时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? InspectionTime { get; set; }

    /// <summary>
    /// 检验图片（JSON格式，存储图片URL列表）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_images", ColumnDescription = "检验图片", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? InspectionImages { get; set; }

    /// <summary>
    /// FQC检验单（主表）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(FqcOrderId))]
    public TaktFqcOrder? Order { get; set; }

    /// <summary>
    /// 不良处理记录列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktFqcDefectHandling.FqcOrderItemId))]
    public List<TaktFqcDefectHandling>? DefectHandlings { get; set; }
}
