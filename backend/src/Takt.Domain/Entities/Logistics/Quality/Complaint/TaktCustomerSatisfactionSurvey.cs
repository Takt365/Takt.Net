// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurvey.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：客户满意度调查表主表实体，记录客户满意度调查的基本信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Complaint;

/// <summary>
/// 客户满意度调查表主表实体
/// </summary>
[SugarTable("takt_logistics_quality_customer_satisfaction_survey", "客户满意度调查表")]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_survey_unique", nameof(CompanyCode), OrderByType.Asc, nameof(CustomerSatisfactionSurveyCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_survey_date", nameof(SurveyDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_customer_id", nameof(CustomerId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_overall_satisfaction", nameof(OverallSatisfaction), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_customer_satisfaction_survey_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktCustomerSatisfactionSurvey : TaktEntityBase
{
    /// <summary>
    /// 公司代码（4位字母数字组合）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 调查表编号（组合唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "customer_satisfaction_survey_code", ColumnDescription = "调查表编号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CustomerSatisfactionSurveyCode { get; set; } = string.Empty;

    /// <summary>
    /// 客户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "customer_id", ColumnDescription = "客户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long CustomerId { get; set; }

    /// <summary>
    /// 客户名称
    /// </summary>
    [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// 客户编码
    /// </summary>
    [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CustomerCode { get; set; }

    /// <summary>
    /// 调查日期
    /// </summary>
    [SugarColumn(ColumnName = "survey_date", ColumnDescription = "调查日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime SurveyDate { get; set; } = DateTime.Today;

    /// <summary>
    /// 调查方式（0=问卷，1=电话，2=邮件，3=现场，4=在线）
    /// </summary>
    [SugarColumn(ColumnName = "survey_method", ColumnDescription = "调查方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SurveyMethod { get; set; } = 0;

    /// <summary>
    /// 调查类型（0=定期调查，1=专项调查，2=投诉后调查，3=其他）
    /// </summary>
    [SugarColumn(ColumnName = "survey_type", ColumnDescription = "调查类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SurveyType { get; set; } = 0;

    /// <summary>
    /// 调查周期（0=月度，1=季度，2=半年度，3=年度）
    /// </summary>
    [SugarColumn(ColumnName = "survey_period", ColumnDescription = "调查周期", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int SurveyPeriod { get; set; } = 1;

    /// <summary>
    /// 调查人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "surveyor_by", ColumnDescription = "调查人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? SurveyorBy { get; set; }

    /// <summary>
    /// 客户联系人
    /// </summary>
    [SugarColumn(ColumnName = "customer_contact", ColumnDescription = "客户联系人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CustomerContact { get; set; }

    /// <summary>
    /// 客户联系电话
    /// </summary>
    [SugarColumn(ColumnName = "customer_phone", ColumnDescription = "客户联系电话", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CustomerPhone { get; set; }

    /// <summary>
    /// 整体满意度（0=非常不满意，1=不满意，2=一般，3=满意，4=非常满意）
    /// </summary>
    [SugarColumn(ColumnName = "overall_satisfaction", ColumnDescription = "整体满意度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OverallSatisfaction { get; set; } = 0;

    /// <summary>
    /// 综合评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "total_score", ColumnDescription = "综合评分", ColumnDataType = "int", IsNullable = true)]
    public int? TotalScore { get; set; }

    /// <summary>
    /// 产品质量评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "quality_score", ColumnDescription = "产品质量评分", ColumnDataType = "int", IsNullable = true)]
    public int? QualityScore { get; set; }

    /// <summary>
    /// 交付准时率评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "delivery_score", ColumnDescription = "交付准时率评分", ColumnDataType = "int", IsNullable = true)]
    public int? DeliveryScore { get; set; }

    /// <summary>
    /// 服务质量评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "service_score", ColumnDescription = "服务质量评分", ColumnDataType = "int", IsNullable = true)]
    public int? ServiceScore { get; set; }

    /// <summary>
    /// 价格竞争力评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "price_score", ColumnDescription = "价格竞争力评分", ColumnDataType = "int", IsNullable = true)]
    public int? PriceScore { get; set; }

    /// <summary>
    /// 技术支持评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "technical_score", ColumnDescription = "技术支持评分", ColumnDataType = "int", IsNullable = true)]
    public int? TechnicalScore { get; set; }

    /// <summary>
    /// 客户主要表扬
    /// </summary>
    [SugarColumn(ColumnName = "customer_praise", ColumnDescription = "客户主要表扬", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? CustomerPraise { get; set; }

    /// <summary>
    /// 客户主要意见/建议
    /// </summary>
    [SugarColumn(ColumnName = "customer_feedback", ColumnDescription = "客户意见", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? CustomerFeedback { get; set; }

    /// <summary>
    /// 改进计划/措施
    /// </summary>
    [SugarColumn(ColumnName = "improvement_plan", ColumnDescription = "改进计划", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? ImprovementPlan { get; set; }

    /// <summary>
    /// 调查状态（0=草稿，1=进行中，2=已完成，3=已归档）
    /// </summary>
    [SugarColumn(ColumnName = "survey_status", ColumnDescription = "调查状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SurveyStatus { get; set; } = 0;

    /// <summary>
    /// 跟进状态（0=无需跟进，1=待跟进，2=跟进中，3=已完成）
    /// </summary>
    [SugarColumn(ColumnName = "follow_up_status", ColumnDescription = "跟进状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FollowUpStatus { get; set; } = 0;

    /// <summary>
    /// 关联客诉ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "related_complaint_id", ColumnDescription = "关联客诉ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? RelatedComplaintId { get; set; }

    /// <summary>
    /// 关联工厂
    /// </summary>
    [SugarColumn(ColumnName = "related_plant", ColumnDescription = "关联工厂", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RelatedPlant { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 调查项目明细列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktCustomerSatisfactionSurveyItem.SurveyId))]
    public List<TaktCustomerSatisfactionSurveyItem>? Items { get; set; }
}
