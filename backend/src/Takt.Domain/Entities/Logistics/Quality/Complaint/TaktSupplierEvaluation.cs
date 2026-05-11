// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluation.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：供应商评价考核主表实体，记录供应商评价考核的基本信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Complaint;

/// <summary>
/// 供应商评价考核主表实体
/// </summary>
[SugarTable("takt_logistics_quality_supplier_evaluation", "供应商评价考核表")]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_evaluation_unique", nameof(CompanyCode), OrderByType.Asc, nameof(SupplierEvaluationCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_evaluation_date", nameof(EvaluationDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_supplier_id", nameof(SupplierId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_overall_rating", nameof(OverallRating), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSupplierEvaluation : TaktEntityBase
{
    /// <summary>
    /// 公司代码（4位字母数字组合）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 评价表编号（组合唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "supplier_evaluation_code", ColumnDescription = "评价表编号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string SupplierEvaluationCode { get; set; } = string.Empty;

    /// <summary>
    /// 供应商ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "supplier_id", ColumnDescription = "供应商ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SupplierId { get; set; }

    /// <summary>
    /// 供应商名称
    /// </summary>
    [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string SupplierName { get; set; } = string.Empty;

    /// <summary>
    /// 供应商编码
    /// </summary>
    [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? SupplierCode { get; set; }

    /// <summary>
    /// 评价日期
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_date", ColumnDescription = "评价日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EvaluationDate { get; set; } = DateTime.Today;

    /// <summary>
    /// 评价周期（0=月度，1=季度，2=半年度，3=年度）
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_period", ColumnDescription = "评价周期", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int EvaluationPeriod { get; set; } = 1;

    /// <summary>
    /// 评价类型（0=常规评价，1=准入评价，2=年度评审，3=专项评价）
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_type", ColumnDescription = "评价类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EvaluationType { get; set; } = 0;

    /// <summary>
    /// 评价人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "evaluator_by", ColumnDescription = "评价人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? EvaluatorBy { get; set; }

    /// <summary>
    /// 评价部门
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_dept", ColumnDescription = "评价部门", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? EvaluationDept { get; set; }

    /// <summary>
    /// 总体评级（0=D级-不合格，1=C级-合格，2=B级-良好，3=A级-优秀）
    /// </summary>
    [SugarColumn(ColumnName = "overall_rating", ColumnDescription = "总体评级", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OverallRating { get; set; } = 0;

    /// <summary>
    /// 综合评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "total_score", ColumnDescription = "综合评分", ColumnDataType = "int", IsNullable = true)]
    public int? TotalScore { get; set; }

    /// <summary>
    /// 质量评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "quality_score", ColumnDescription = "质量评分", ColumnDataType = "int", IsNullable = true)]
    public int? QualityScore { get; set; }

    /// <summary>
    /// 交付评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "delivery_score", ColumnDescription = "交付评分", ColumnDataType = "int", IsNullable = true)]
    public int? DeliveryScore { get; set; }

    /// <summary>
    /// 价格评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "price_score", ColumnDescription = "价格评分", ColumnDataType = "int", IsNullable = true)]
    public int? PriceScore { get; set; }

    /// <summary>
    /// 服务评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "service_score", ColumnDescription = "服务评分", ColumnDataType = "int", IsNullable = true)]
    public int? ServiceScore { get; set; }

    /// <summary>
    /// 技术能力评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "technical_score", ColumnDescription = "技术能力评分", ColumnDataType = "int", IsNullable = true)]
    public int? TechnicalScore { get; set; }

    /// <summary>
    /// 主要优点
    /// </summary>
    [SugarColumn(ColumnName = "main_strengths", ColumnDescription = "主要优点", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? MainStrengths { get; set; }

    /// <summary>
    /// 主要问题/不足
    /// </summary>
    [SugarColumn(ColumnName = "main_issues", ColumnDescription = "主要问题", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? MainIssues { get; set; }

    /// <summary>
    /// 改进要求/建议
    /// </summary>
    [SugarColumn(ColumnName = "improvement_requirements", ColumnDescription = "改进要求", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? ImprovementRequirements { get; set; }

    /// <summary>
    /// 考核结论（0=继续合作，1=限期整改，2=减少订单，3=暂停合作，4=取消资格）
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_conclusion", ColumnDescription = "考核结论", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EvaluationConclusion { get; set; } = 0;

    /// <summary>
    /// 整改期限（要求完成日期）
    /// </summary>
    [SugarColumn(ColumnName = "rectification_deadline", ColumnDescription = "整改期限", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? RectificationDeadline { get; set; }

    /// <summary>
    /// 评价状态（0=草稿，1=评价中，2=已完成，3=已归档）
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_status", ColumnDescription = "评价状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EvaluationStatus { get; set; } = 0;

    /// <summary>
    /// 整改跟进状态（0=无需整改，1=待整改，2=整改中，3=已完成，4=未通过）
    /// </summary>
    [SugarColumn(ColumnName = "rectification_status", ColumnDescription = "整改跟进状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RectificationStatus { get; set; } = 0;

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
    /// 评价项目明细列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktSupplierEvaluationItem.EvaluationId))]
    public List<TaktSupplierEvaluationItem>? Items { get; set; }
}
