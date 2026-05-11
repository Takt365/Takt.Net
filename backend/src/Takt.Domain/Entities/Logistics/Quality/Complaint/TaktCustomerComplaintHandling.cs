// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality.Complaint
// 文件名称：TaktCustomerComplaintHandling.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客诉处理记录实体，记录客诉的处理过程和结果
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Complaint;

/// <summary>
/// 客诉处理记录实体
/// </summary>
[SugarTable("takt_logistics_quality_customer_complaint_handling", "客诉处理记录表")]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_handling_complaint_id", nameof(ComplaintId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_handling_handling_status", nameof(HandlingStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_handling_handler_by", nameof(HandlerBy), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_complaint_handling_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktCustomerComplaintHandling : TaktEntityBase
{
    /// <summary>
    /// 客诉处理记录编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "complaint_handling_code", ColumnDescription = "客诉处理记录编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ComplaintHandlingCode { get; set; } = string.Empty;

    /// <summary>
    /// 客诉ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "complaint_id", ColumnDescription = "客诉ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ComplaintId { get; set; }

    /// <summary>
    /// 客诉单号（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "complaint_no", ColumnDescription = "客诉单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ComplaintNo { get; set; } = string.Empty;

    /// <summary>
    /// 客诉明细ID（可选，关联到具体不良项目，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "complaint_item_id", ColumnDescription = "客诉明细ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ComplaintItemId { get; set; }

    /// <summary>
    /// 处理阶段（0=初步响应，1=原因分析，2=改善对策，3=效果验证，4=结案）
    /// </summary>
    [SugarColumn(ColumnName = "handling_stage", ColumnDescription = "处理阶段", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int HandlingStage { get; set; } = 0;

    /// <summary>
    /// 处理方式（0=返工，1=返修，2=补货，3=退货，4=退款，5=折扣，6=其他）
    /// </summary>
    [SugarColumn(ColumnName = "handling_method", ColumnDescription = "处理方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int HandlingMethod { get; set; } = 0;

    /// <summary>
    /// 处理说明
    /// </summary>
    [SugarColumn(ColumnName = "handling_description", ColumnDescription = "处理说明", ColumnDataType = "nvarchar", Length = 2000, IsNullable = false)]
    public string HandlingDescription { get; set; } = string.Empty;

    /// <summary>
    /// 原因分析
    /// </summary>
    [SugarColumn(ColumnName = "cause_analysis", ColumnDescription = "原因分析", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? CauseAnalysis { get; set; }

    /// <summary>
    /// 改善对策/纠正措施
    /// </summary>
    [SugarColumn(ColumnName = "corrective_action", ColumnDescription = "纠正措施", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? CorrectiveAction { get; set; }

    /// <summary>
    /// 预防措施
    /// </summary>
    [SugarColumn(ColumnName = "preventive_action", ColumnDescription = "预防措施", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? PreventiveAction { get; set; }

    /// <summary>
    /// 责任部门
    /// </summary>
    [SugarColumn(ColumnName = "responsible_dept", ColumnDescription = "责任部门", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ResponsibleDept { get; set; }

    /// <summary>
    /// 责任人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "responsible_by", ColumnDescription = "责任人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ResponsibleBy { get; set; }

    /// <summary>
    /// 处理人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "handler_by", ColumnDescription = "处理人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? HandlerBy { get; set; }

    /// <summary>
    /// 处理时间
    /// </summary>
    [SugarColumn(ColumnName = "handling_time", ColumnDescription = "处理时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? HandlingTime { get; set; }

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
    /// 处理状态（0=待处理，1=处理中，2=已完成，3=已关闭，4=已驳回）
    /// </summary>
    [SugarColumn(ColumnName = "handling_status", ColumnDescription = "处理状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int HandlingStatus { get; set; } = 0;

    /// <summary>
    /// 处理成本/损失金额
    /// </summary>
    [SugarColumn(ColumnName = "handling_cost", ColumnDescription = "处理成本", ColumnDataType = "decimal", Length = 18, DecimalDigits =  2, IsNullable = true)]
    public decimal? HandlingCost { get; set; }

    /// <summary>
    /// 客户反馈
    /// </summary>
    [SugarColumn(ColumnName = "customer_feedback", ColumnDescription = "客户反馈", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? CustomerFeedback { get; set; }

    /// <summary>
    /// 客户满意度（0=不满意，1=一般，2=满意，3=非常满意）
    /// </summary>
    [SugarColumn(ColumnName = "customer_satisfaction", ColumnDescription = "客户满意度", ColumnDataType = "int", IsNullable = true)]
    public int? CustomerSatisfaction { get; set; }

    /// <summary>
    /// 附件路径（JSON格式，存储相关文件URL列表）
    /// </summary>
    [SugarColumn(ColumnName = "attachment_paths", ColumnDescription = "附件路径", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? AttachmentPaths { get; set; }


    /// <summary>
    /// 客诉主表
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(ComplaintId))]
    public TaktCustomerComplaint? Complaint { get; set; }
}
