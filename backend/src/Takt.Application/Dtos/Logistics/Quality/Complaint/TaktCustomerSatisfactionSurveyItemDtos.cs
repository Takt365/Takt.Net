// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurveyItemDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：客户满意度调查项目明细表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Complaint;

/// <summary>
/// 客户满意度调查项目明细表Dto
/// </summary>
public partial class TaktCustomerSatisfactionSurveyItemDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemDto()
    {
        CustomerSatisfactionSurveyCode = string.Empty;
        ItemName = string.Empty;
    }

    /// <summary>
    /// 客户满意度调查项目明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerSatisfactionSurveyItemId { get; set; } = 0;

    /// <summary>
    /// 调查表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SurveyId { get; set; }
    /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }
    /// <summary>
    /// 调查类别
    /// </summary>
    public int CategoryType { get; set; }
    /// <summary>
    /// 调查项目
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
    /// 评分
    /// </summary>
    public int? Score { get; set; }
    /// <summary>
    /// 满意度等级
    /// </summary>
    public int? SatisfactionLevel { get; set; }
    /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestion { get; set; }
    /// <summary>
    /// 跟进措施
    /// </summary>
    public string? FollowUpAction { get; set; }
    /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 调查表主表
    /// </summary>
    public TaktCustomerSatisfactionSurveyDto? Survey { get; set; }
}

/// <summary>
/// 客户满意度调查项目明细表查询DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 调查表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SurveyId { get; set; }
    /// <summary>
    /// 调查表编号
    /// </summary>
    public string? CustomerSatisfactionSurveyCode { get; set; }
    /// <summary>
    /// 行号
    /// </summary>
    public int? LineNumber { get; set; }
    /// <summary>
    /// 调查类别
    /// </summary>
    public int? CategoryType { get; set; }
    /// <summary>
    /// 调查项目
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
    /// 评分
    /// </summary>
    public int? Score { get; set; }
    /// <summary>
    /// 满意度等级
    /// </summary>
    public int? SatisfactionLevel { get; set; }
    /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestion { get; set; }
    /// <summary>
    /// 跟进措施
    /// </summary>
    public string? FollowUpAction { get; set; }
    /// <summary>
    /// 跟进状态
    /// </summary>
    public int? FollowUpStatus { get; set; }

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
/// Takt创建客户满意度调查项目明细表DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemCreateDto()
    {
        CustomerSatisfactionSurveyCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// 调查表ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SurveyId { get; set; }

        /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 调查类别
    /// </summary>
    public int CategoryType { get; set; }

        /// <summary>
    /// 调查项目
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
    /// 评分
    /// </summary>
    public int? Score { get; set; }

        /// <summary>
    /// 满意度等级
    /// </summary>
    public int? SatisfactionLevel { get; set; }

        /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestion { get; set; }

        /// <summary>
    /// 跟进措施
    /// </summary>
    public string? FollowUpAction { get; set; }

        /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }

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
/// Takt更新客户满意度调查项目明细表DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyItemUpdateDto : TaktCustomerSatisfactionSurveyItemCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemUpdateDto()
    {
    }

        /// <summary>
    /// 客户满意度调查项目明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerSatisfactionSurveyItemId { get; set; } = 0;
}

/// <summary>
/// 客户满意度调查项目明细表跟进状态DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyItemFollowUpStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemFollowUpStatusDto()
    {
    }

        /// <summary>
    /// 客户满意度调查项目明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerSatisfactionSurveyItemId { get; set; } = 0;

    /// <summary>
    /// 跟进状态（0=禁用，1=启用）
    /// </summary>
    public int FollowUpStatus { get; set; }
}

/// <summary>
/// 客户满意度调查项目明细表排序DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyItemSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemSortDto()
    {
    }

        /// <summary>
    /// 客户满意度调查项目明细表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerSatisfactionSurveyItemId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 客户满意度调查项目明细表导入模板DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyItemTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemTemplateDto()
    {
        CustomerSatisfactionSurveyCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// 调查表ID
    /// </summary>
    public long SurveyId { get; set; }

        /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 调查类别
    /// </summary>
    public int CategoryType { get; set; }

        /// <summary>
    /// 调查项目
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
    /// 评分
    /// </summary>
    public int? Score { get; set; }

        /// <summary>
    /// 满意度等级
    /// </summary>
    public int? SatisfactionLevel { get; set; }

        /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestion { get; set; }

        /// <summary>
    /// 跟进措施
    /// </summary>
    public string? FollowUpAction { get; set; }

        /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }

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
/// 客户满意度调查项目明细表导入DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyItemImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemImportDto()
    {
        CustomerSatisfactionSurveyCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// 调查表ID
    /// </summary>
    public long SurveyId { get; set; }

        /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 调查类别
    /// </summary>
    public int CategoryType { get; set; }

        /// <summary>
    /// 调查项目
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
    /// 评分
    /// </summary>
    public int? Score { get; set; }

        /// <summary>
    /// 满意度等级
    /// </summary>
    public int? SatisfactionLevel { get; set; }

        /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestion { get; set; }

        /// <summary>
    /// 跟进措施
    /// </summary>
    public string? FollowUpAction { get; set; }

        /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }

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
/// 客户满意度调查项目明细表导出DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyItemExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyItemExportDto()
    {
        CreatedAt = DateTime.Now;
        CustomerSatisfactionSurveyCode = string.Empty;
        ItemName = string.Empty;
    }

        /// <summary>
    /// 调查表ID
    /// </summary>
    public long SurveyId { get; set; }

        /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }

        /// <summary>
    /// 行号
    /// </summary>
    public int LineNumber { get; set; }

        /// <summary>
    /// 调查类别
    /// </summary>
    public int CategoryType { get; set; }

        /// <summary>
    /// 调查项目
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
    /// 评分
    /// </summary>
    public int? Score { get; set; }

        /// <summary>
    /// 满意度等级
    /// </summary>
    public int? SatisfactionLevel { get; set; }

        /// <summary>
    /// 客户反馈
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestion { get; set; }

        /// <summary>
    /// 跟进措施
    /// </summary>
    public string? FollowUpAction { get; set; }

        /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}