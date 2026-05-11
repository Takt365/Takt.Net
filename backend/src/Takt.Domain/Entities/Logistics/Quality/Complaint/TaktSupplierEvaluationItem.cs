// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluationItem.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：供应商评价考核项目明细实体，记录具体的评价项目和评分
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Complaint;

/// <summary>
/// 供应商评价考核项目明细实体
/// </summary>
[SugarTable("takt_logistics_quality_supplier_evaluation_item", "供应商评价考核项目明细表")]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_item_evaluation_id", nameof(EvaluationId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_item_supplier_evaluation_code", nameof(SupplierEvaluationCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_item_category_type", nameof(CategoryType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_supplier_evaluation_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSupplierEvaluationItem : TaktEntityBase
{
    /// <summary>
    /// 评价表ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_id", ColumnDescription = "评价表ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EvaluationId { get; set; }

    /// <summary>
    /// 评价表编号（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "supplier_evaluation_code", ColumnDescription = "评价表编号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string SupplierEvaluationCode { get; set; } = string.Empty;

    /// <summary>
    /// 行号（评价项目行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 评价类别类型（0=质量管理，1=交付能力，2=价格水平，3=服务水平，4=技术能力，5=管理体系，6=其他）
    /// </summary>
    [SugarColumn(ColumnName = "category_type", ColumnDescription = "评价类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CategoryType { get; set; } = 0;

    /// <summary>
    /// 评价项目名称
    /// </summary>
    [SugarColumn(ColumnName = "item_name", ColumnDescription = "评价项目", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 评价项目说明
    /// </summary>
    [SugarColumn(ColumnName = "item_description", ColumnDescription = "项目说明", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ItemDescription { get; set; }

    /// <summary>
    /// 权重（%）
    /// </summary>
    [SugarColumn(ColumnName = "weight", ColumnDescription = "权重", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Weight { get; set; } = 0;

    /// <summary>
    /// 评分标准
    /// </summary>
    [SugarColumn(ColumnName = "scoring_standard", ColumnDescription = "评分标准", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ScoringStandard { get; set; }

    /// <summary>
    /// 评分（0-100分）
    /// </summary>
    [SugarColumn(ColumnName = "score", ColumnDescription = "评分", ColumnDataType = "int", IsNullable = true)]
    public int? Score { get; set; }

    /// <summary>
    /// 评级（0=D级-不合格，1=C级-合格，2=B级-良好，3=A级-优秀）
    /// </summary>
    [SugarColumn(ColumnName = "rating_level", ColumnDescription = "评级", ColumnDataType = "int", IsNullable = true)]
    public int? RatingLevel { get; set; }

    /// <summary>
    /// 评价说明/事实依据
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_comment", ColumnDescription = "评价说明", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? EvaluationComment { get; set; }

    /// <summary>
    /// 存在问题
    /// </summary>
    [SugarColumn(ColumnName = "existing_issues", ColumnDescription = "存在问题", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ExistingIssues { get; set; }

    /// <summary>
    /// 改进要求
    /// </summary>
    [SugarColumn(ColumnName = "improvement_requirement", ColumnDescription = "改进要求", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ImprovementRequirement { get; set; }

    /// <summary>
    /// 整改要求（0=无需整改，1=限期整改，2=重点整改）
    /// </summary>
    [SugarColumn(ColumnName = "rectification_required", ColumnDescription = "整改要求", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RectificationRequired { get; set; } = 0;

    /// <summary>
    /// 整改期限
    /// </summary>
    [SugarColumn(ColumnName = "rectification_deadline", ColumnDescription = "整改期限", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? RectificationDeadline { get; set; }

    /// <summary>
    /// 整改状态（0=无需整改，1=待整改，2=整改中，3=已完成，4=未通过）
    /// </summary>
    [SugarColumn(ColumnName = "rectification_status", ColumnDescription = "整改状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RectificationStatus { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 评价表主表
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(EvaluationId))]
    public TaktSupplierEvaluation? Evaluation { get; set; }
}
