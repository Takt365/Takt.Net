// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluationDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：供应商评价考核表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.Logistics.Quality.Complaint;

/// <summary>
/// 供应商评价考核表Dto
/// </summary>
public partial class TaktSupplierEvaluationDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationDto()
    {
        CompanyCode = string.Empty;
        SupplierEvaluationCode = string.Empty;
        SupplierName = string.Empty;
    }

    /// <summary>
    /// 供应商评价考核表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierEvaluationId { get; set; } = 0;

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }
    /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }
    /// <summary>
    /// 供应商ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierId { get; set; }
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }
    /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }
    /// <summary>
    /// 评价日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }
    /// <summary>
    /// 评价周期
    /// </summary>
    public int EvaluationPeriod { get; set; }
    /// <summary>
    /// 评价类型
    /// </summary>
    public int EvaluationType { get; set; }
    /// <summary>
    /// 评价人
    /// </summary>
    public string? EvaluatorBy { get; set; }
    /// <summary>
    /// 评价部门
    /// </summary>
    public string? EvaluationDept { get; set; }
    /// <summary>
    /// 总体评级
    /// </summary>
    public int OverallRating { get; set; }
    /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }
    /// <summary>
    /// 质量评分
    /// </summary>
    public int? QualityScore { get; set; }
    /// <summary>
    /// 交付评分
    /// </summary>
    public int? DeliveryScore { get; set; }
    /// <summary>
    /// 价格评分
    /// </summary>
    public int? PriceScore { get; set; }
    /// <summary>
    /// 服务评分
    /// </summary>
    public int? ServiceScore { get; set; }
    /// <summary>
    /// 技术能力评分
    /// </summary>
    public int? TechnicalScore { get; set; }
    /// <summary>
    /// 主要优点
    /// </summary>
    public string? MainStrengths { get; set; }
    /// <summary>
    /// 主要问题
    /// </summary>
    public string? MainIssues { get; set; }
    /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirements { get; set; }
    /// <summary>
    /// 考核结论
    /// </summary>
    public int EvaluationConclusion { get; set; }
    /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }
    /// <summary>
    /// 评价状态
    /// </summary>
    public int EvaluationStatus { get; set; }
    /// <summary>
    /// 整改跟进状态
    /// </summary>
    public int RectificationStatus { get; set; }
    /// <summary>
    /// 关联工厂
    /// </summary>
    public string? RelatedPlant { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 评价项目明细列表（主子表关系）（外键在子表 TaktSupplierEvaluationItemDto.EvaluationId）
    /// </summary>
    public List<TaktSupplierEvaluationItemDto>? Items { get; set; }
}

/// <summary>
/// 供应商评价考核表查询DTO
/// </summary>
public partial class TaktSupplierEvaluationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 公司代码
    /// </summary>
    public string? CompanyCode { get; set; }
    /// <summary>
    /// 评价表编号
    /// </summary>
    public string? SupplierEvaluationCode { get; set; }
    /// <summary>
    /// 供应商ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SupplierId { get; set; }
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string? SupplierName { get; set; }
    /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }
    /// <summary>
    /// 评价日期
    /// </summary>
    public DateTime? EvaluationDate { get; set; }

    /// <summary>
    /// 评价日期开始时间
    /// </summary>
    public DateTime? EvaluationDateStart { get; set; }
    /// <summary>
    /// 评价日期结束时间
    /// </summary>
    public DateTime? EvaluationDateEnd { get; set; }
    /// <summary>
    /// 评价周期
    /// </summary>
    public int? EvaluationPeriod { get; set; }
    /// <summary>
    /// 评价类型
    /// </summary>
    public int? EvaluationType { get; set; }
    /// <summary>
    /// 评价人
    /// </summary>
    public string? EvaluatorBy { get; set; }
    /// <summary>
    /// 评价部门
    /// </summary>
    public string? EvaluationDept { get; set; }
    /// <summary>
    /// 总体评级
    /// </summary>
    public int? OverallRating { get; set; }
    /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }
    /// <summary>
    /// 质量评分
    /// </summary>
    public int? QualityScore { get; set; }
    /// <summary>
    /// 交付评分
    /// </summary>
    public int? DeliveryScore { get; set; }
    /// <summary>
    /// 价格评分
    /// </summary>
    public int? PriceScore { get; set; }
    /// <summary>
    /// 服务评分
    /// </summary>
    public int? ServiceScore { get; set; }
    /// <summary>
    /// 技术能力评分
    /// </summary>
    public int? TechnicalScore { get; set; }
    /// <summary>
    /// 主要优点
    /// </summary>
    public string? MainStrengths { get; set; }
    /// <summary>
    /// 主要问题
    /// </summary>
    public string? MainIssues { get; set; }
    /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirements { get; set; }
    /// <summary>
    /// 考核结论
    /// </summary>
    public int? EvaluationConclusion { get; set; }
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
    /// 评价状态
    /// </summary>
    public int? EvaluationStatus { get; set; }
    /// <summary>
    /// 整改跟进状态
    /// </summary>
    public int? RectificationStatus { get; set; }
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
/// Takt创建供应商评价考核表DTO
/// </summary>
public partial class TaktSupplierEvaluationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationCreateDto()
    {
        CompanyCode = string.Empty;
        SupplierEvaluationCode = string.Empty;
        SupplierName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }

        /// <summary>
    /// 供应商ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierId { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }

        /// <summary>
    /// 评价日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 评价周期
    /// </summary>
    public int EvaluationPeriod { get; set; }

        /// <summary>
    /// 评价类型
    /// </summary>
    public int EvaluationType { get; set; }

        /// <summary>
    /// 评价人
    /// </summary>
    public string? EvaluatorBy { get; set; }

        /// <summary>
    /// 评价部门
    /// </summary>
    public string? EvaluationDept { get; set; }

        /// <summary>
    /// 总体评级
    /// </summary>
    public int OverallRating { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }

        /// <summary>
    /// 质量评分
    /// </summary>
    public int? QualityScore { get; set; }

        /// <summary>
    /// 交付评分
    /// </summary>
    public int? DeliveryScore { get; set; }

        /// <summary>
    /// 价格评分
    /// </summary>
    public int? PriceScore { get; set; }

        /// <summary>
    /// 服务评分
    /// </summary>
    public int? ServiceScore { get; set; }

        /// <summary>
    /// 技术能力评分
    /// </summary>
    public int? TechnicalScore { get; set; }

        /// <summary>
    /// 主要优点
    /// </summary>
    public string? MainStrengths { get; set; }

        /// <summary>
    /// 主要问题
    /// </summary>
    public string? MainIssues { get; set; }

        /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirements { get; set; }

        /// <summary>
    /// 考核结论
    /// </summary>
    public int EvaluationConclusion { get; set; }

        /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }

        /// <summary>
    /// 评价状态
    /// </summary>
    public int EvaluationStatus { get; set; }

        /// <summary>
    /// 整改跟进状态
    /// </summary>
    public int RectificationStatus { get; set; }

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
    /// 评价项目明细列表（主子表关系）（外键在子表 TaktSupplierEvaluationItemCreateDto.EvaluationId）
    /// </summary>
    public List<TaktSupplierEvaluationItemCreateDto>? Items { get; set; }

}

/// <summary>
/// Takt更新供应商评价考核表DTO
/// </summary>
public partial class TaktSupplierEvaluationUpdateDto : TaktSupplierEvaluationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationUpdateDto()
    {
    }

        /// <summary>
    /// 供应商评价考核表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierEvaluationId { get; set; } = 0;
}

/// <summary>
/// 供应商评价考核表评价状态DTO
/// </summary>
public partial class TaktSupplierEvaluationEvaluationStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationEvaluationStatusDto()
    {
    }

        /// <summary>
    /// 供应商评价考核表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierEvaluationId { get; set; } = 0;

    /// <summary>
    /// 评价状态（0=禁用，1=启用）
    /// </summary>
    public int EvaluationStatus { get; set; }
}

/// <summary>
/// 供应商评价考核表整改跟进状态DTO
/// </summary>
public partial class TaktSupplierEvaluationRectificationStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationRectificationStatusDto()
    {
    }

        /// <summary>
    /// 供应商评价考核表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierEvaluationId { get; set; } = 0;

    /// <summary>
    /// 整改跟进状态（0=禁用，1=启用）
    /// </summary>
    public int RectificationStatus { get; set; }
}

/// <summary>
/// 供应商评价考核表排序DTO
/// </summary>
public partial class TaktSupplierEvaluationSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationSortDto()
    {
    }

        /// <summary>
    /// 供应商评价考核表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SupplierEvaluationId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 供应商评价考核表导入模板DTO
/// </summary>
public partial class TaktSupplierEvaluationTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationTemplateDto()
    {
        CompanyCode = string.Empty;
        SupplierEvaluationCode = string.Empty;
        SupplierName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }

        /// <summary>
    /// 供应商ID
    /// </summary>
    public long SupplierId { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }

        /// <summary>
    /// 评价日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 评价周期
    /// </summary>
    public int EvaluationPeriod { get; set; }

        /// <summary>
    /// 评价类型
    /// </summary>
    public int EvaluationType { get; set; }

        /// <summary>
    /// 评价人
    /// </summary>
    public string? EvaluatorBy { get; set; }

        /// <summary>
    /// 评价部门
    /// </summary>
    public string? EvaluationDept { get; set; }

        /// <summary>
    /// 总体评级
    /// </summary>
    public int OverallRating { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }

        /// <summary>
    /// 质量评分
    /// </summary>
    public int? QualityScore { get; set; }

        /// <summary>
    /// 交付评分
    /// </summary>
    public int? DeliveryScore { get; set; }

        /// <summary>
    /// 价格评分
    /// </summary>
    public int? PriceScore { get; set; }

        /// <summary>
    /// 服务评分
    /// </summary>
    public int? ServiceScore { get; set; }

        /// <summary>
    /// 技术能力评分
    /// </summary>
    public int? TechnicalScore { get; set; }

        /// <summary>
    /// 主要优点
    /// </summary>
    public string? MainStrengths { get; set; }

        /// <summary>
    /// 主要问题
    /// </summary>
    public string? MainIssues { get; set; }

        /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirements { get; set; }

        /// <summary>
    /// 考核结论
    /// </summary>
    public int EvaluationConclusion { get; set; }

        /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }

        /// <summary>
    /// 评价状态
    /// </summary>
    public int EvaluationStatus { get; set; }

        /// <summary>
    /// 整改跟进状态
    /// </summary>
    public int RectificationStatus { get; set; }

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
/// 供应商评价考核表导入DTO
/// </summary>
public partial class TaktSupplierEvaluationImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationImportDto()
    {
        CompanyCode = string.Empty;
        SupplierEvaluationCode = string.Empty;
        SupplierName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }

        /// <summary>
    /// 供应商ID
    /// </summary>
    public long SupplierId { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }

        /// <summary>
    /// 评价日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 评价周期
    /// </summary>
    public int EvaluationPeriod { get; set; }

        /// <summary>
    /// 评价类型
    /// </summary>
    public int EvaluationType { get; set; }

        /// <summary>
    /// 评价人
    /// </summary>
    public string? EvaluatorBy { get; set; }

        /// <summary>
    /// 评价部门
    /// </summary>
    public string? EvaluationDept { get; set; }

        /// <summary>
    /// 总体评级
    /// </summary>
    public int OverallRating { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }

        /// <summary>
    /// 质量评分
    /// </summary>
    public int? QualityScore { get; set; }

        /// <summary>
    /// 交付评分
    /// </summary>
    public int? DeliveryScore { get; set; }

        /// <summary>
    /// 价格评分
    /// </summary>
    public int? PriceScore { get; set; }

        /// <summary>
    /// 服务评分
    /// </summary>
    public int? ServiceScore { get; set; }

        /// <summary>
    /// 技术能力评分
    /// </summary>
    public int? TechnicalScore { get; set; }

        /// <summary>
    /// 主要优点
    /// </summary>
    public string? MainStrengths { get; set; }

        /// <summary>
    /// 主要问题
    /// </summary>
    public string? MainIssues { get; set; }

        /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirements { get; set; }

        /// <summary>
    /// 考核结论
    /// </summary>
    public int EvaluationConclusion { get; set; }

        /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }

        /// <summary>
    /// 评价状态
    /// </summary>
    public int EvaluationStatus { get; set; }

        /// <summary>
    /// 整改跟进状态
    /// </summary>
    public int RectificationStatus { get; set; }

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
/// 供应商评价考核表导出DTO
/// </summary>
public partial class TaktSupplierEvaluationExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationExportDto()
    {
        CreatedAt = DateTime.Now;
        CompanyCode = string.Empty;
        SupplierEvaluationCode = string.Empty;
        SupplierName = string.Empty;
    }

        /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; }

        /// <summary>
    /// 评价表编号
    /// </summary>
    public string SupplierEvaluationCode { get; set; }

        /// <summary>
    /// 供应商ID
    /// </summary>
    public long SupplierId { get; set; }

        /// <summary>
    /// 供应商名称
    /// </summary>
    public string SupplierName { get; set; }

        /// <summary>
    /// 供应商编码
    /// </summary>
    public string? SupplierCode { get; set; }

        /// <summary>
    /// 评价日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 评价周期
    /// </summary>
    public int EvaluationPeriod { get; set; }

        /// <summary>
    /// 评价类型
    /// </summary>
    public int EvaluationType { get; set; }

        /// <summary>
    /// 评价人
    /// </summary>
    public string? EvaluatorBy { get; set; }

        /// <summary>
    /// 评价部门
    /// </summary>
    public string? EvaluationDept { get; set; }

        /// <summary>
    /// 总体评级
    /// </summary>
    public int OverallRating { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int? TotalScore { get; set; }

        /// <summary>
    /// 质量评分
    /// </summary>
    public int? QualityScore { get; set; }

        /// <summary>
    /// 交付评分
    /// </summary>
    public int? DeliveryScore { get; set; }

        /// <summary>
    /// 价格评分
    /// </summary>
    public int? PriceScore { get; set; }

        /// <summary>
    /// 服务评分
    /// </summary>
    public int? ServiceScore { get; set; }

        /// <summary>
    /// 技术能力评分
    /// </summary>
    public int? TechnicalScore { get; set; }

        /// <summary>
    /// 主要优点
    /// </summary>
    public string? MainStrengths { get; set; }

        /// <summary>
    /// 主要问题
    /// </summary>
    public string? MainIssues { get; set; }

        /// <summary>
    /// 改进要求
    /// </summary>
    public string? ImprovementRequirements { get; set; }

        /// <summary>
    /// 考核结论
    /// </summary>
    public int EvaluationConclusion { get; set; }

        /// <summary>
    /// 整改期限
    /// </summary>
    public DateTime? RectificationDeadline { get; set; }

        /// <summary>
    /// 评价状态
    /// </summary>
    public int EvaluationStatus { get; set; }

        /// <summary>
    /// 整改跟进状态
    /// </summary>
    public int RectificationStatus { get; set; }

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