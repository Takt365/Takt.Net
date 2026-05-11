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
[SugarIndex("ix_takt_logistics_quality_fqc_order_item_material_code", nameof(MaterialCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_item_standard_code", nameof(StandardCode), OrderByType.Asc)]
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
    /// FQC检验单编码（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "fqc_order_code", ColumnDescription = "FQC检验单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string FqcOrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 行号/项号（检验单明细行号，与FqcOrderId组成联合唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料名称
    /// </summary>
    [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string MaterialName { get; set; } = string.Empty;

    /// <summary>
    /// 批次号
    /// </summary>
    [SugarColumn(ColumnName = "batch_no", ColumnDescription = "批次号", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? BatchNo { get; set; }

    /// <summary>
    /// 入库数量
    /// </summary>
    [SugarColumn(ColumnName = "warehouse_quantity", ColumnDescription = "入库数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal WarehouseQuantity { get; set; } = 0;

    /// <summary>
    /// 检验标准编码
    /// </summary>
    [SugarColumn(ColumnName = "standard_code", ColumnDescription = "检验标准编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string StandardCode { get; set; } = string.Empty;

    /// <summary>
    /// 抽样方案编码
    /// </summary>
    [SugarColumn(ColumnName = "sampling_scheme_code", ColumnDescription = "抽样方案编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string SamplingSchemeCode { get; set; } = string.Empty;

    /// <summary>
    /// 检验方式（0=免检，1=减量，2=正常，3=加严，4=全检）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_method", ColumnDescription = "检验方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
    public int InspectionMethod { get; set; } = 2;

    /// <summary>
    /// 抽样数量
    /// </summary>
    [SugarColumn(ColumnName = "sample_quantity", ColumnDescription = "抽样数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SampleQuantity { get; set; } = 0;

    /// <summary>
    /// 合格数量
    /// </summary>
    [SugarColumn(ColumnName = "qualified_quantity", ColumnDescription = "合格数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int QualifiedQuantity { get; set; } = 0;

    /// <summary>
    /// 不合格数量
    /// </summary>
    [SugarColumn(ColumnName = "unqualified_quantity", ColumnDescription = "不合格数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int UnqualifiedQuantity { get; set; } = 0;

    /// <summary>
    /// 验退数量
    /// </summary>
    [SugarColumn(ColumnName = "inspection_return_quantity", ColumnDescription = "验退数量", ColumnDataType = "decimal", Length = 16, DecimalDigits = 6, IsNullable = false, DefaultValue = "0")]
    public decimal InspectionReturnQuantity { get; set; } = 0;

    /// <summary>
    /// 判定状态（0=待判定，1=合格，2=不合格，3=让步接收，4=退货）
    /// </summary>
    [SugarColumn(ColumnName = "judge_status", ColumnDescription = "判定状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int JudgeStatus { get; set; } = 0;

    /// <summary>
    /// 抽检序列号
    /// </summary>
    [SugarColumn(ColumnName = "sample_serial_no", ColumnDescription = "抽检序列号", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? SampleSerialNo { get; set; }

    /// <summary>
    /// 检验说明
    /// </summary>
    [SugarColumn(ColumnName = "inspection_description", ColumnDescription = "检验说明", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? InspectionDescription { get; set; }

    /// <summary>
    /// 检验员（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "inspector_by", ColumnDescription = "检验员", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string InspectorBy { get; set; } = string.Empty;

    /// <summary>
    /// 检验日期
    /// </summary>
    [SugarColumn(ColumnName = "inspection_date", ColumnDescription = "检验日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime InspectionDate { get; set; }

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
