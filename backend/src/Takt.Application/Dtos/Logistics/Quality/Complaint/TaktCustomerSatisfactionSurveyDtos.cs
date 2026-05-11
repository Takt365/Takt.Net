// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Complaint
// 文件名称：TaktCustomerSatisfactionSurveyDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：客户满意度调查表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Complaint;

/// <summary>
/// 客户满意度调查表Dto
/// </summary>
public partial class TaktCustomerSatisfactionSurveyDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyDto()
    {
        CompanyCode = string.Empty;
        CustomerSatisfactionSurveyCode = string.Empty;
        CustomerName = string.Empty;
    }

    /// <summary>
    /// 客户满意度调查表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerSatisfactionSurveyId { get; set; } = 0;

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }
    /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }
    /// <summary>
    /// 客户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerId { get; set; }
    /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }
    /// <summary>
    /// 调查日期
    /// </summary>
    public DateTime SurveyDate { get; set; }
    /// <summary>
    /// 调查方式
    /// </summary>
    public int SurveyMethod { get; set; }
    /// <summary>
    /// 调查类型
    /// </summary>
    public int SurveyType { get; set; }
    /// <summary>
    /// 调查周期
    /// </summary>
    public int SurveyPeriod { get; set; }
    /// <summary>
    /// 调查人
    /// </summary>
    public string? SurveyorBy { get; set; }
    /// <summary>
    /// 客户联系人
    /// </summary>
    public string? CustomerContact { get; set; }
    /// <summary>
    /// 客户联系电话
    /// </summary>
    public string? CustomerPhone { get; set; }
    /// <summary>
    /// 整体满意度
    /// </summary>
    public int OverallSatisfaction { get; set; }
    /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }
    /// <summary>
    /// 产品质量评分
    /// </summary>
    public int? QualityScore { get; set; }
    /// <summary>
    /// 交付准时率评分
    /// </summary>
    public int? DeliveryScore { get; set; }
    /// <summary>
    /// 服务质量评分
    /// </summary>
    public int? ServiceScore { get; set; }
    /// <summary>
    /// 价格竞争力评分
    /// </summary>
    public int? PriceScore { get; set; }
    /// <summary>
    /// 技术支持评分
    /// </summary>
    public int? TechnicalScore { get; set; }
    /// <summary>
    /// 客户主要表扬
    /// </summary>
    public string? CustomerPraise { get; set; }
    /// <summary>
    /// 客户意见
    /// </summary>
    public string? CustomerFeedback { get; set; }
    /// <summary>
    /// 改进计划
    /// </summary>
    public string? ImprovementPlan { get; set; }
    /// <summary>
    /// 调查状态
    /// </summary>
    public int SurveyStatus { get; set; }
    /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }
    /// <summary>
    /// 关联客诉ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RelatedComplaintId { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 调查项目明细列表（主子表关系）（外键在子表 TaktCustomerSatisfactionSurveyItemDto.SurveyId）
    /// </summary>
    public List<TaktCustomerSatisfactionSurveyItemDto>? Items { get; set; }
}

/// <summary>
/// 客户满意度调查表查询DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 调查表编号
    /// </summary>
    public string? CustomerSatisfactionSurveyCode { get; set; }
    /// <summary>
    /// 客户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CustomerId { get; set; }
    /// <summary>
    /// 客户名称
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }
    /// <summary>
    /// 调查日期
    /// </summary>
    public DateTime? SurveyDate { get; set; }

    /// <summary>
    /// 调查日期开始时间
    /// </summary>
    public DateTime? SurveyDateStart { get; set; }
    /// <summary>
    /// 调查日期结束时间
    /// </summary>
    public DateTime? SurveyDateEnd { get; set; }
    /// <summary>
    /// 调查方式
    /// </summary>
    public int? SurveyMethod { get; set; }
    /// <summary>
    /// 调查类型
    /// </summary>
    public int? SurveyType { get; set; }
    /// <summary>
    /// 调查周期
    /// </summary>
    public int? SurveyPeriod { get; set; }
    /// <summary>
    /// 调查人
    /// </summary>
    public string? SurveyorBy { get; set; }
    /// <summary>
    /// 客户联系人
    /// </summary>
    public string? CustomerContact { get; set; }
    /// <summary>
    /// 客户联系电话
    /// </summary>
    public string? CustomerPhone { get; set; }
    /// <summary>
    /// 整体满意度
    /// </summary>
    public int? OverallSatisfaction { get; set; }
    /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }
    /// <summary>
    /// 产品质量评分
    /// </summary>
    public int? QualityScore { get; set; }
    /// <summary>
    /// 交付准时率评分
    /// </summary>
    public int? DeliveryScore { get; set; }
    /// <summary>
    /// 服务质量评分
    /// </summary>
    public int? ServiceScore { get; set; }
    /// <summary>
    /// 价格竞争力评分
    /// </summary>
    public int? PriceScore { get; set; }
    /// <summary>
    /// 技术支持评分
    /// </summary>
    public int? TechnicalScore { get; set; }
    /// <summary>
    /// 客户主要表扬
    /// </summary>
    public string? CustomerPraise { get; set; }
    /// <summary>
    /// 客户意见
    /// </summary>
    public string? CustomerFeedback { get; set; }
    /// <summary>
    /// 改进计划
    /// </summary>
    public string? ImprovementPlan { get; set; }
    /// <summary>
    /// 调查状态
    /// </summary>
    public int? SurveyStatus { get; set; }
    /// <summary>
    /// 跟进状态
    /// </summary>
    public int? FollowUpStatus { get; set; }
    /// <summary>
    /// 关联客诉ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RelatedComplaintId { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

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
/// Takt创建客户满意度调查表DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyCreateDto()
    {
        CompanyCode = string.Empty;
        CustomerSatisfactionSurveyCode = string.Empty;
        CustomerName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }

        /// <summary>
    /// 客户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerId { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 调查日期
    /// </summary>
    public DateTime SurveyDate { get; set; }

        /// <summary>
    /// 调查方式
    /// </summary>
    public int SurveyMethod { get; set; }

        /// <summary>
    /// 调查类型
    /// </summary>
    public int SurveyType { get; set; }

        /// <summary>
    /// 调查周期
    /// </summary>
    public int SurveyPeriod { get; set; }

        /// <summary>
    /// 调查人
    /// </summary>
    public string? SurveyorBy { get; set; }

        /// <summary>
    /// 客户联系人
    /// </summary>
    public string? CustomerContact { get; set; }

        /// <summary>
    /// 客户联系电话
    /// </summary>
    public string? CustomerPhone { get; set; }

        /// <summary>
    /// 整体满意度
    /// </summary>
    public int OverallSatisfaction { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }

        /// <summary>
    /// 产品质量评分
    /// </summary>
    public int? QualityScore { get; set; }

        /// <summary>
    /// 交付准时率评分
    /// </summary>
    public int? DeliveryScore { get; set; }

        /// <summary>
    /// 服务质量评分
    /// </summary>
    public int? ServiceScore { get; set; }

        /// <summary>
    /// 价格竞争力评分
    /// </summary>
    public int? PriceScore { get; set; }

        /// <summary>
    /// 技术支持评分
    /// </summary>
    public int? TechnicalScore { get; set; }

        /// <summary>
    /// 客户主要表扬
    /// </summary>
    public string? CustomerPraise { get; set; }

        /// <summary>
    /// 客户意见
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 改进计划
    /// </summary>
    public string? ImprovementPlan { get; set; }

        /// <summary>
    /// 调查状态
    /// </summary>
    public int SurveyStatus { get; set; }

        /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }

        /// <summary>
    /// 关联客诉ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RelatedComplaintId { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

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


    /// <summary>
    /// 调查项目明细列表（主子表关系）（外键在子表 TaktCustomerSatisfactionSurveyItemCreateDto.SurveyId）
    /// </summary>
    public List<TaktCustomerSatisfactionSurveyItemCreateDto>? Items { get; set; }

}

/// <summary>
/// Takt更新客户满意度调查表DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyUpdateDto : TaktCustomerSatisfactionSurveyCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyUpdateDto()
    {
    }

        /// <summary>
    /// 客户满意度调查表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerSatisfactionSurveyId { get; set; } = 0;
}

/// <summary>
/// 客户满意度调查表调查状态DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveySurveyStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveySurveyStatusDto()
    {
    }

        /// <summary>
    /// 客户满意度调查表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerSatisfactionSurveyId { get; set; } = 0;

    /// <summary>
    /// 调查状态（0=禁用，1=启用）
    /// </summary>
    public int SurveyStatus { get; set; }
}

/// <summary>
/// 客户满意度调查表跟进状态DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyFollowUpStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyFollowUpStatusDto()
    {
    }

        /// <summary>
    /// 客户满意度调查表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerSatisfactionSurveyId { get; set; } = 0;

    /// <summary>
    /// 跟进状态（0=禁用，1=启用）
    /// </summary>
    public int FollowUpStatus { get; set; }
}

/// <summary>
/// 客户满意度调查表排序DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveySortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveySortDto()
    {
    }

        /// <summary>
    /// 客户满意度调查表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CustomerSatisfactionSurveyId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 客户满意度调查表导入模板DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyTemplateDto()
    {
        CompanyCode = string.Empty;
        CustomerSatisfactionSurveyCode = string.Empty;
        CustomerName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }

        /// <summary>
    /// 客户ID
    /// </summary>
    public long CustomerId { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 调查日期
    /// </summary>
    public DateTime SurveyDate { get; set; }

        /// <summary>
    /// 调查方式
    /// </summary>
    public int SurveyMethod { get; set; }

        /// <summary>
    /// 调查类型
    /// </summary>
    public int SurveyType { get; set; }

        /// <summary>
    /// 调查周期
    /// </summary>
    public int SurveyPeriod { get; set; }

        /// <summary>
    /// 调查人
    /// </summary>
    public string? SurveyorBy { get; set; }

        /// <summary>
    /// 客户联系人
    /// </summary>
    public string? CustomerContact { get; set; }

        /// <summary>
    /// 客户联系电话
    /// </summary>
    public string? CustomerPhone { get; set; }

        /// <summary>
    /// 整体满意度
    /// </summary>
    public int OverallSatisfaction { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }

        /// <summary>
    /// 产品质量评分
    /// </summary>
    public int? QualityScore { get; set; }

        /// <summary>
    /// 交付准时率评分
    /// </summary>
    public int? DeliveryScore { get; set; }

        /// <summary>
    /// 服务质量评分
    /// </summary>
    public int? ServiceScore { get; set; }

        /// <summary>
    /// 价格竞争力评分
    /// </summary>
    public int? PriceScore { get; set; }

        /// <summary>
    /// 技术支持评分
    /// </summary>
    public int? TechnicalScore { get; set; }

        /// <summary>
    /// 客户主要表扬
    /// </summary>
    public string? CustomerPraise { get; set; }

        /// <summary>
    /// 客户意见
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 改进计划
    /// </summary>
    public string? ImprovementPlan { get; set; }

        /// <summary>
    /// 调查状态
    /// </summary>
    public int SurveyStatus { get; set; }

        /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }

        /// <summary>
    /// 关联客诉ID
    /// </summary>
    public long? RelatedComplaintId { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

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
/// 客户满意度调查表导入DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyImportDto()
    {
        CompanyCode = string.Empty;
        CustomerSatisfactionSurveyCode = string.Empty;
        CustomerName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }

        /// <summary>
    /// 客户ID
    /// </summary>
    public long CustomerId { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 调查日期
    /// </summary>
    public DateTime SurveyDate { get; set; }

        /// <summary>
    /// 调查方式
    /// </summary>
    public int SurveyMethod { get; set; }

        /// <summary>
    /// 调查类型
    /// </summary>
    public int SurveyType { get; set; }

        /// <summary>
    /// 调查周期
    /// </summary>
    public int SurveyPeriod { get; set; }

        /// <summary>
    /// 调查人
    /// </summary>
    public string? SurveyorBy { get; set; }

        /// <summary>
    /// 客户联系人
    /// </summary>
    public string? CustomerContact { get; set; }

        /// <summary>
    /// 客户联系电话
    /// </summary>
    public string? CustomerPhone { get; set; }

        /// <summary>
    /// 整体满意度
    /// </summary>
    public int OverallSatisfaction { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }

        /// <summary>
    /// 产品质量评分
    /// </summary>
    public int? QualityScore { get; set; }

        /// <summary>
    /// 交付准时率评分
    /// </summary>
    public int? DeliveryScore { get; set; }

        /// <summary>
    /// 服务质量评分
    /// </summary>
    public int? ServiceScore { get; set; }

        /// <summary>
    /// 价格竞争力评分
    /// </summary>
    public int? PriceScore { get; set; }

        /// <summary>
    /// 技术支持评分
    /// </summary>
    public int? TechnicalScore { get; set; }

        /// <summary>
    /// 客户主要表扬
    /// </summary>
    public string? CustomerPraise { get; set; }

        /// <summary>
    /// 客户意见
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 改进计划
    /// </summary>
    public string? ImprovementPlan { get; set; }

        /// <summary>
    /// 调查状态
    /// </summary>
    public int SurveyStatus { get; set; }

        /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }

        /// <summary>
    /// 关联客诉ID
    /// </summary>
    public long? RelatedComplaintId { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

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
/// 客户满意度调查表导出DTO
/// </summary>
public partial class TaktCustomerSatisfactionSurveyExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCustomerSatisfactionSurveyExportDto()
    {
        CreatedAt = DateTime.Now;
        CompanyCode = string.Empty;
        CustomerSatisfactionSurveyCode = string.Empty;
        CustomerName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 调查表编号
    /// </summary>
    public string CustomerSatisfactionSurveyCode { get; set; }

        /// <summary>
    /// 客户ID
    /// </summary>
    public long CustomerId { get; set; }

        /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; }

        /// <summary>
    /// 客户编码
    /// </summary>
    public string? CustomerCode { get; set; }

        /// <summary>
    /// 调查日期
    /// </summary>
    public DateTime SurveyDate { get; set; }

        /// <summary>
    /// 调查方式
    /// </summary>
    public int SurveyMethod { get; set; }

        /// <summary>
    /// 调查类型
    /// </summary>
    public int SurveyType { get; set; }

        /// <summary>
    /// 调查周期
    /// </summary>
    public int SurveyPeriod { get; set; }

        /// <summary>
    /// 调查人
    /// </summary>
    public string? SurveyorBy { get; set; }

        /// <summary>
    /// 客户联系人
    /// </summary>
    public string? CustomerContact { get; set; }

        /// <summary>
    /// 客户联系电话
    /// </summary>
    public string? CustomerPhone { get; set; }

        /// <summary>
    /// 整体满意度
    /// </summary>
    public int OverallSatisfaction { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }

        /// <summary>
    /// 产品质量评分
    /// </summary>
    public int? QualityScore { get; set; }

        /// <summary>
    /// 交付准时率评分
    /// </summary>
    public int? DeliveryScore { get; set; }

        /// <summary>
    /// 服务质量评分
    /// </summary>
    public int? ServiceScore { get; set; }

        /// <summary>
    /// 价格竞争力评分
    /// </summary>
    public int? PriceScore { get; set; }

        /// <summary>
    /// 技术支持评分
    /// </summary>
    public int? TechnicalScore { get; set; }

        /// <summary>
    /// 客户主要表扬
    /// </summary>
    public string? CustomerPraise { get; set; }

        /// <summary>
    /// 客户意见
    /// </summary>
    public string? CustomerFeedback { get; set; }

        /// <summary>
    /// 改进计划
    /// </summary>
    public string? ImprovementPlan { get; set; }

        /// <summary>
    /// 调查状态
    /// </summary>
    public int SurveyStatus { get; set; }

        /// <summary>
    /// 跟进状态
    /// </summary>
    public int FollowUpStatus { get; set; }

        /// <summary>
    /// 关联客诉ID
    /// </summary>
    public long? RelatedComplaintId { get; set; }

        /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}