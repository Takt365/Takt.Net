// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurveyItem.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客户满意度调查项目明细实体，记录具体的调查项目和评分
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Complaint;

/// <summary>
/// 客户满意度调查项目明细实体
/// </summary>
[SugarTable("takt_logistics_quality_customer_satisfaction_survey_item", "客户满意度调查项目明细表")]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_item_survey_id", nameof(SurveyId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_item_customer_satisfaction_survey_code", nameof(CustomerSatisfactionSurveyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_item_category_type", nameof(CategoryType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktCustomerSatisfactionSurveyItem : TaktEntityBase
{
    /// <summary>
    /// 调查表ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "survey_id", ColumnDescription = "调查表ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SurveyId { get; set; }

    /// <summary>
    /// 调查表编号（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "customer_satisfaction_survey_code", ColumnDescription = "调查表编号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CustomerSatisfactionSurveyCode { get; set; } = string.Empty;

    /// <summary>
    /// 行号（调查项目行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 调查类别类型（0=产品质量，1=交付服务，2=售后服务，3=技术支持，4=价格，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "category_type", ColumnDescription = "调查类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CategoryType { get; set; } = 0;

    /// <summary>
    /// 调查项目名称
    /// </summary>
    [SugarColumn(ColumnName = "item_name", ColumnDescription = "调查项目", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 调查项目说明
    /// </summary>
    [SugarColumn(ColumnName = "item_description", ColumnDescription = "项目说明", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ItemDescription { get; set; }

    /// <summary>
    /// 权重（%）
    /// </summary>
    [SugarColumn(ColumnName = "weight", ColumnDescription = "权重", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Weight { get; set; } = 0;

    /// <summary>
    /// 评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "score", ColumnDescription = "评分", ColumnDataType = "int", IsNullable = true)]
    public int? Score { get; set; }

    /// <summary>
    /// 满意度等级（0=非常不满意，1=不满意，2=一般，3=满意，4=非常满意）
    /// </summary>
    [SugarColumn(ColumnName = "satisfaction_level", ColumnDescription = "满意度等级", ColumnDataType = "int", IsNullable = true)]
    public int? SatisfactionLevel { get; set; }

    /// <summary>
    /// 客户反馈/意见
    /// </summary>
    [SugarColumn(ColumnName = "customer_feedback", ColumnDescription = "客户反馈", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? CustomerFeedback { get; set; }

    /// <summary>
    /// 改进建议
    /// </summary>
    [SugarColumn(ColumnName = "improvement_suggestion", ColumnDescription = "改进建议", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ImprovementSuggestion { get; set; }

    /// <summary>
    /// 跟进措施
    /// </summary>
    [SugarColumn(ColumnName = "follow_up_action", ColumnDescription = "跟进措施", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? FollowUpAction { get; set; }

    /// <summary>
    /// 跟进状态（0=无需跟进，1=待跟进，2=跟进中，3=已完成）
    /// </summary>
    [SugarColumn(ColumnName = "follow_up_status", ColumnDescription = "跟进状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FollowUpStatus { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 调查表主表
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(SurveyId))]
    public TaktCustomerSatisfactionSurvey? Survey { get; set; }
}
