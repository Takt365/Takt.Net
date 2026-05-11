// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintItem.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客诉明细实体，记录客诉的不良项目详情
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Complaint;

/// <summary>
/// 客诉明细实体
/// </summary>
[SugarTable("takt_logistics_quality_customer_complaint_item", "客诉明细表")]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_item_complaint_id", nameof(ComplaintId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_item_customer_complaint_code", nameof(CustomerComplaintCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_item_item_type", nameof(ItemType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktCustomerComplaintItem : TaktEntityBase
{
    /// <summary>
    /// 客诉ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "complaint_id", ColumnDescription = "客诉ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ComplaintId { get; set; }

    /// <summary>
    /// 客诉单号（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "customer_complaint_code", ColumnDescription = "客诉单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CustomerComplaintCode { get; set; } = string.Empty;

    /// <summary>
    /// 行号（客诉明细行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 产品编码
    /// </summary>
    [SugarColumn(ColumnName = "product_code", ColumnDescription = "产品编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ProductCode { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    [SugarColumn(ColumnName = "product_name", ColumnDescription = "产品名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? ProductName { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    [SugarColumn(ColumnName = "batch_no", ColumnDescription = "批次号", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? BatchNo { get; set; }

    /// <summary>
    /// 不良项目类型（0=外观，1=尺寸，2=性能，3=功能，4=包装，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "item_type", ColumnDescription = "不良项目类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ItemType { get; set; } = 0;

    /// <summary>
    /// 不良现象描述
    /// </summary>
    [SugarColumn(ColumnName = "defect_description", ColumnDescription = "不良现象描述", ColumnDataType = "nvarchar", Length = 1000, IsNullable = false)]
    public string DefectDescription { get; set; } = string.Empty;

    /// <summary>
    /// 缺点等级（CR=严重，MA=主要，MI=次要）
    /// </summary>
    [SugarColumn(ColumnName = "defect_level", ColumnDescription = "缺点等级", ColumnDataType = "nvarchar", Length = 2, IsNullable = false)]
    public string DefectLevel { get; set; } = string.Empty;

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "defect_quantity", ColumnDescription = "不良数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DefectQuantity { get; set; } = 0;

    /// <summary>
    /// 不良率（%）
    /// </summary>
    [SugarColumn(ColumnName = "defect_rate", ColumnDescription = "不良率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = true)]
    public decimal? DefectRate { get; set; }

    /// <summary>
    /// 原因分析
    /// </summary>
    [SugarColumn(ColumnName = "cause_analysis", ColumnDescription = "原因分析", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? CauseAnalysis { get; set; }

    /// <summary>
    /// 改善对策
    /// </summary>
    [SugarColumn(ColumnName = "improvement_action", ColumnDescription = "改善对策", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ImprovementAction { get; set; }

    /// <summary>
    /// 改善责任人
    /// </summary>
    [SugarColumn(ColumnName = "improvement_responsible", ColumnDescription = "改善责任人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ImprovementResponsible { get; set; }

    /// <summary>
    /// 计划完成日期
    /// </summary>
    [SugarColumn(ColumnName = "planned_completion_date", ColumnDescription = "计划完成日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PlannedCompletionDate { get; set; }

    /// <summary>
    /// 实际完成日期
    /// </summary>
    [SugarColumn(ColumnName = "actual_completion_date", ColumnDescription = "实际完成日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ActualCompletionDate { get; set; }

    /// <summary>
    /// 改善状态（0=待改善，1=改善中，2=已完成，3=已验证）
    /// </summary>
    [SugarColumn(ColumnName = "improvement_status", ColumnDescription = "改善状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ImprovementStatus { get; set; } = 0;

    /// <summary>
    /// 附件路径（多个附件用逗号分隔）
    /// </summary>
    [SugarColumn(ColumnName = "attachment_paths", ColumnDescription = "附件路径", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? AttachmentPaths { get; set; }



    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 客诉主表
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(ComplaintId))]
    public TaktCustomerComplaint? Complaint { get; set; }
}
