// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluationItemDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：供应商评价考核项目明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Complaint;

/// <summary>
/// 供应商评价考核项目明细表Dto
/// </summary>
public partial class TaktSupplierEvaluationItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemDto()
    {
        SupplierEvaluationCode = string.Empty;
        ItemName = string.Empty;
    }

    /// <summary>
    /// 供应商评价考核项目明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierEvaluationItemId { get; set; } = 0;

    /// <summary>
    /// 评价表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EvaluationId { get; set; }
    /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 评价类别
    /// </summary>
    public int CategoryType { get; set; }
    /// <summary>
    /// 评价项目
    /// </summary>
    public string ItemName { get; set; }
    /// <summary>
    /// 项目说明
    /// </summary>
    public string? ItemDescription { get; set; }
    /// <summary>
    /// 权重
    /// </summary>
    public int Weight { get; set; }
    /// <summary>
    /// 评分标准
    /// </summary>
    public string? ScoringStandard { get; set; }
    /// <summary>
    /// 评分
    /// </summary>
    public int? Score { get; set; }
    /// <summary>
    /// 评级
    /// </summary>
    public int? RatingLevel { get; set; }
    /// <summary>
    /// 评价说明
    /// </summary>
    public string? EvaluationComment { get; set; }
    /// <summary>
    /// 存在问题
    /// </summary>
    public string? ExistingIssues { get; set; }
    /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirement { get; set; }
    /// <summary>
    /// 整改要求
    /// </summary>
    public int RectificationRequired { get; set; }
    /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }
    /// <summary>
    /// 整改状态
    /// </summary>
    public int RectificationStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 评价表主表
    /// </summary>
    public TaktSupplierEvaluationDto? Evaluation { get; set; }
}

/// <summary>
/// 供应商评价考核项目明细表查询DTO
/// </summary>
public partial class TaktSupplierEvaluationItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 评价表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EvaluationId { get; set; }
    /// <summary>
    /// 评价表编号
    /// </summary>
    public string? SupplierEvaluationCode { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 评价类别
    /// </summary>
    public int? CategoryType { get; set; }
    /// <summary>
    /// 评价项目
    /// </summary>
    public string? ItemName { get; set; }
    /// <summary>
    /// 项目说明
    /// </summary>
    public string? ItemDescription { get; set; }
    /// <summary>
    /// 权重
    /// </summary>
    public int? Weight { get; set; }
    /// <summary>
    /// 评分标准
    /// </summary>
    public string? ScoringStandard { get; set; }
    /// <summary>
    /// 评分
    /// </summary>
    public int? Score { get; set; }
    /// <summary>
    /// 评级
    /// </summary>
    public int? RatingLevel { get; set; }
    /// <summary>
    /// 评价说明
    /// </summary>
    public string? EvaluationComment { get; set; }
    /// <summary>
    /// 存在问题
    /// </summary>
    public string? ExistingIssues { get; set; }
    /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirement { get; set; }
    /// <summary>
    /// 整改要求
    /// </summary>
    public int? RectificationRequired { get; set; }
    /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }

    /// <summary>
    /// 整改期限开始时间
    /// </summary>
    public DateTime? RectificationDeadlineStart { get; set; }
    /// <summary>
    /// 整改期限结束时间
    /// </summary>
    public DateTime? RectificationDeadlineEnd { get; set; }
    /// <summary>
    /// 整改状态
    /// </summary>
    public int? RectificationStatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }
    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建供应商评价考核项目明细表DTO
/// </summary>
public partial class TaktSupplierEvaluationItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemCreateDto()
    {
        SupplierEvaluationCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// 评价表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EvaluationId { get; set; }

        /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 评价类别
    /// </summary>
    public int CategoryType { get; set; }

        /// <summary>
    /// 评价项目
    /// </summary>
    public string ItemName { get; set; }

        /// <summary>
    /// 项目说明
    /// </summary>
    public string? ItemDescription { get; set; }

        /// <summary>
    /// 权重
    /// </summary>
    public int Weight { get; set; }

        /// <summary>
    /// 评分标准
    /// </summary>
    public string? ScoringStandard { get; set; }

        /// <summary>
    /// 评分
    /// </summary>
    public int? Score { get; set; }

        /// <summary>
    /// 评级
    /// </summary>
    public int? RatingLevel { get; set; }

        /// <summary>
    /// 评价说明
    /// </summary>
    public string? EvaluationComment { get; set; }

        /// <summary>
    /// 存在问题
    /// </summary>
    public string? ExistingIssues { get; set; }

        /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirement { get; set; }

        /// <summary>
    /// 整改要求
    /// </summary>
    public int RectificationRequired { get; set; }

        /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }

        /// <summary>
    /// 整改状态
    /// </summary>
    public int RectificationStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

}

/// <summary>
/// Takt更新供应商评价考核项目明细表DTO
/// </summary>
public partial class TaktSupplierEvaluationItemUpdateDto : TaktSupplierEvaluationItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemUpdateDto()
    {
    }

        /// <summary>
    /// 供应商评价考核项目明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierEvaluationItemId { get; set; } = 0;
}

/// <summary>
/// 供应商评价考核项目明细表整改状态DTO
/// </summary>
public partial class TaktSupplierEvaluationItemRectificationStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemRectificationStatusDto()
    {
    }

        /// <summary>
    /// 供应商评价考核项目明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierEvaluationItemId { get; set; } = 0;

    /// <summary>
    /// 整改状态（0=禁用，1=启用）
    /// </summary>
    public int RectificationStatus { get; set; }
}

/// <summary>
/// 供应商评价考核项目明细表排序DTO
/// </summary>
public partial class TaktSupplierEvaluationItemSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemSortDto()
    {
    }

        /// <summary>
    /// 供应商评价考核项目明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierEvaluationItemId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 供应商评价考核项目明细表导入模板DTO
/// </summary>
public partial class TaktSupplierEvaluationItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemTemplateDto()
    {
        SupplierEvaluationCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// 评价表ID
    /// </summary>
    public long EvaluationId { get; set; }

        /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 评价类别
    /// </summary>
    public int CategoryType { get; set; }

        /// <summary>
    /// 评价项目
    /// </summary>
    public string ItemName { get; set; }

        /// <summary>
    /// 项目说明
    /// </summary>
    public string? ItemDescription { get; set; }

        /// <summary>
    /// 权重
    /// </summary>
    public int Weight { get; set; }

        /// <summary>
    /// 评分标准
    /// </summary>
    public string? ScoringStandard { get; set; }

        /// <summary>
    /// 评分
    /// </summary>
    public int? Score { get; set; }

        /// <summary>
    /// 评级
    /// </summary>
    public int? RatingLevel { get; set; }

        /// <summary>
    /// 评价说明
    /// </summary>
    public string? EvaluationComment { get; set; }

        /// <summary>
    /// 存在问题
    /// </summary>
    public string? ExistingIssues { get; set; }

        /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirement { get; set; }

        /// <summary>
    /// 整改要求
    /// </summary>
    public int RectificationRequired { get; set; }

        /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }

        /// <summary>
    /// 整改状态
    /// </summary>
    public int RectificationStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 供应商评价考核项目明细表导入DTO
/// </summary>
public partial class TaktSupplierEvaluationItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemImportDto()
    {
        SupplierEvaluationCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// 评价表ID
    /// </summary>
    public long EvaluationId { get; set; }

        /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 评价类别
    /// </summary>
    public int CategoryType { get; set; }

        /// <summary>
    /// 评价项目
    /// </summary>
    public string ItemName { get; set; }

        /// <summary>
    /// 项目说明
    /// </summary>
    public string? ItemDescription { get; set; }

        /// <summary>
    /// 权重
    /// </summary>
    public int Weight { get; set; }

        /// <summary>
    /// 评分标准
    /// </summary>
    public string? ScoringStandard { get; set; }

        /// <summary>
    /// 评分
    /// </summary>
    public int? Score { get; set; }

        /// <summary>
    /// 评级
    /// </summary>
    public int? RatingLevel { get; set; }

        /// <summary>
    /// 评价说明
    /// </summary>
    public string? EvaluationComment { get; set; }

        /// <summary>
    /// 存在问题
    /// </summary>
    public string? ExistingIssues { get; set; }

        /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirement { get; set; }

        /// <summary>
    /// 整改要求
    /// </summary>
    public int RectificationRequired { get; set; }

        /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }

        /// <summary>
    /// 整改状态
    /// </summary>
    public int RectificationStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 供应商评价考核项目明细表导出DTO
/// </summary>
public partial class TaktSupplierEvaluationItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemExportDto()
    {
        CreatedAt = DateTime.Now;
        SupplierEvaluationCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// 评价表ID
    /// </summary>
    public long EvaluationId { get; set; }

        /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 评价类别
    /// </summary>
    public int CategoryType { get; set; }

        /// <summary>
    /// 评价项目
    /// </summary>
    public string ItemName { get; set; }

        /// <summary>
    /// 项目说明
    /// </summary>
    public string? ItemDescription { get; set; }

        /// <summary>
    /// 权重
    /// </summary>
    public int Weight { get; set; }

        /// <summary>
    /// 评分标准
    /// </summary>
    public string? ScoringStandard { get; set; }

        /// <summary>
    /// 评分
    /// </summary>
    public int? Score { get; set; }

        /// <summary>
    /// 评级
    /// </summary>
    public int? RatingLevel { get; set; }

        /// <summary>
    /// 评价说明
    /// </summary>
    public string? EvaluationComment { get; set; }

        /// <summary>
    /// 存在问题
    /// </summary>
    public string? ExistingIssues { get; set; }

        /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirement { get; set; }

        /// <summary>
    /// 整改要求
    /// </summary>
    public int RectificationRequired { get; set; }

        /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }

        /// <summary>
    /// 整改状态
    /// </summary>
    public int RectificationStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}