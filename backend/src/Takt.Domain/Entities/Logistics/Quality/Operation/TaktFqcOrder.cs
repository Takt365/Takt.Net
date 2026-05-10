// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality
// 文件名称：TaktFqcOrder.cs
// 功能描述：FQC出货检验单实体
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Operation;

/// <summary>
/// FQC出货检验单实体
/// </summary>
[SugarTable("takt_logistics_quality_fqc_order", "出货检验单表")]
[SugarIndex("ix_takt_logistics_quality_fqc_order_order_code", nameof(OrderCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_source_code", nameof(SourceCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_plan_code", nameof(PlanCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_customer_code", nameof(CustomerCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_order_status", nameof(OrderStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_fqc_order_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFqcOrder : TaktEntityBase
{
    /// <summary>
    /// 检验单编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "order_code", ColumnDescription = "检验单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 来源单号（销售订单编码或发货单编码）
    /// </summary>
    [SugarColumn(ColumnName = "source_code", ColumnDescription = "来源单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string SourceCode { get; set; } = string.Empty;

    /// <summary>
    /// 检验计划编码（可选，从计划生成）
    /// </summary>
    [SugarColumn(ColumnName = "plan_code", ColumnDescription = "检验计划编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlanCode { get; set; }

    /// <summary>
    /// 检验标准编码
    /// </summary>
    [SugarColumn(ColumnName = "standard_code", ColumnDescription = "检验标准编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string StandardCode { get; set; } = string.Empty;

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
    /// 客户编码
    /// </summary>
    [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CustomerCode { get; set; } = string.Empty;

    /// <summary>
    /// 客户名称
    /// </summary>
    [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// 出货数量
    /// </summary>
    [SugarColumn(ColumnName = "outgoing_quantity", ColumnDescription = "出货数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal OutgoingQuantity { get; set; } = 0;

    /// <summary>
    /// 出货单号
    /// </summary>
    [SugarColumn(ColumnName = "delivery_order_code", ColumnDescription = "出货单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DeliveryOrderCode { get; set; }

    /// <summary>
    /// 抽样方案编码
    /// </summary>
    [SugarColumn(ColumnName = "sampling_scheme_code", ColumnDescription = "抽样方案编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? SamplingSchemeCode { get; set; }

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
    /// 检验结论（0=待判定，1=合格，2=不合格，3=让步接收，4=退货）
    /// </summary>
    [SugarColumn(ColumnName = "inspection_conclusion", ColumnDescription = "检验结论", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InspectionConclusion { get; set; } = 0;

    /// <summary>
    /// 判定人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "judge_by", ColumnDescription = "判定人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? JudgeBy { get; set; }

    /// <summary>
    /// 判定时间
    /// </summary>
    [SugarColumn(ColumnName = "judge_time", ColumnDescription = "判定时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? JudgeTime { get; set; }

    /// <summary>
    /// 检验备注
    /// </summary>
    [SugarColumn(ColumnName = "inspection_remark", ColumnDescription = "检验备注", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? InspectionRemark { get; set; }

    /// <summary>
    /// 检验单状态（0=草稿，1=待检验，2=检验中，3=待判定，4=已完成，5=已关闭）
    /// </summary>
    [SugarColumn(ColumnName = "order_status", ColumnDescription = "检验单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderStatus { get; set; } = 0;

    /// <summary>
    /// FQC检验单明细列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktFqcOrderItem.FqcOrderId))]
    public List<TaktFqcOrderItem>? Items { get; set; }

    /// <summary>
    /// 变更日志列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktFqcOrderChangeLog.FqcOrderId))]
    public List<TaktFqcOrderChangeLog>? ChangeLogs { get; set; }
}
