// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Performance
// 文件名称：TaktPerformanceIndicatorDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：绩效指标表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Performance;

/// <summary>
/// 绩效指标表Dto
/// </summary>
public partial class TaktPerformanceIndicatorDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorDto()
    {
        IndicatorCode = string.Empty;
        IndicatorName = string.Empty;
        Category = string.Empty;
        IndicatorType = string.Empty;
        IndicatorDescription = string.Empty;
        ScoringCriteria = string.Empty;
        DataSource = string.Empty;
        EvaluationCycle = string.Empty;
    }

    /// <summary>
    /// 绩效指标表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceIndicatorId { get; set; } = 0;

    /// <summary>
    /// 指标编码
    /// </summary>
    public string IndicatorCode { get; set; }
    /// <summary>
    /// 指标名称
    /// </summary>
    public string IndicatorName { get; set; }
    /// <summary>
    /// 指标类别
    /// </summary>
    public string Category { get; set; }
    /// <summary>
    /// 指标类型
    /// </summary>
    public string IndicatorType { get; set; }
    /// <summary>
    /// 指标说明
    /// </summary>
    public string IndicatorDescription { get; set; }
    /// <summary>
    /// 评分标准说明
    /// </summary>
    public string ScoringCriteria { get; set; }
    /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }
    /// <summary>
    /// 最低要求值
    /// </summary>
    public decimal MinimumValue { get; set; }
    /// <summary>
    /// 卓越值
    /// </summary>
    public decimal ExcellentValue { get; set; }
    /// <summary>
    /// 标准权重
    /// </summary>
    public decimal StandardWeight { get; set; }
    /// <summary>
    /// 数据来源
    /// </summary>
    public string DataSource { get; set; }
    /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationCycle { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 绩效指标表查询DTO
/// </summary>
public partial class TaktPerformanceIndicatorQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 指标编码
    /// </summary>
    public string? IndicatorCode { get; set; }
    /// <summary>
    /// 指标名称
    /// </summary>
    public string? IndicatorName { get; set; }
    /// <summary>
    /// 指标类别
    /// </summary>
    public string? Category { get; set; }
    /// <summary>
    /// 指标类型
    /// </summary>
    public string? IndicatorType { get; set; }
    /// <summary>
    /// 指标说明
    /// </summary>
    public string? IndicatorDescription { get; set; }
    /// <summary>
    /// 评分标准说明
    /// </summary>
    public string? ScoringCriteria { get; set; }
    /// <summary>
    /// 目标值
    /// </summary>
    public decimal? TargetValue { get; set; }
    /// <summary>
    /// 最低要求值
    /// </summary>
    public decimal? MinimumValue { get; set; }
    /// <summary>
    /// 卓越值
    /// </summary>
    public decimal? ExcellentValue { get; set; }
    /// <summary>
    /// 标准权重
    /// </summary>
    public decimal? StandardWeight { get; set; }
    /// <summary>
    /// 数据来源
    /// </summary>
    public string? DataSource { get; set; }
    /// <summary>
    /// 考核周期
    /// </summary>
    public string? EvaluationCycle { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

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
/// Takt创建绩效指标表DTO
/// </summary>
public partial class TaktPerformanceIndicatorCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorCreateDto()
    {
        IndicatorCode = string.Empty;
        IndicatorName = string.Empty;
        Category = string.Empty;
        IndicatorType = string.Empty;
        IndicatorDescription = string.Empty;
        ScoringCriteria = string.Empty;
        DataSource = string.Empty;
        EvaluationCycle = string.Empty;
    }

        /// <summary>
    /// 指标编码
    /// </summary>
    public string IndicatorCode { get; set; }

        /// <summary>
    /// 指标名称
    /// </summary>
    public string IndicatorName { get; set; }

        /// <summary>
    /// 指标类别
    /// </summary>
    public string Category { get; set; }

        /// <summary>
    /// 指标类型
    /// </summary>
    public string IndicatorType { get; set; }

        /// <summary>
    /// 指标说明
    /// </summary>
    public string IndicatorDescription { get; set; }

        /// <summary>
    /// 评分标准说明
    /// </summary>
    public string ScoringCriteria { get; set; }

        /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }

        /// <summary>
    /// 最低要求值
    /// </summary>
    public decimal MinimumValue { get; set; }

        /// <summary>
    /// 卓越值
    /// </summary>
    public decimal ExcellentValue { get; set; }

        /// <summary>
    /// 标准权重
    /// </summary>
    public decimal StandardWeight { get; set; }

        /// <summary>
    /// 数据来源
    /// </summary>
    public string DataSource { get; set; }

        /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationCycle { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// Takt更新绩效指标表DTO
/// </summary>
public partial class TaktPerformanceIndicatorUpdateDto : TaktPerformanceIndicatorCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorUpdateDto()
    {
    }

        /// <summary>
    /// 绩效指标表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceIndicatorId { get; set; } = 0;
}

/// <summary>
/// 绩效指标表状态DTO
/// </summary>
public partial class TaktPerformanceIndicatorStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorStatusDto()
    {
    }

        /// <summary>
    /// 绩效指标表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceIndicatorId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 绩效指标表排序DTO
/// </summary>
public partial class TaktPerformanceIndicatorSortDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorSortDto()
    {
    }

        /// <summary>
    /// 绩效指标表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceIndicatorId { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 绩效指标表导入模板DTO
/// </summary>
public partial class TaktPerformanceIndicatorTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorTemplateDto()
    {
        IndicatorCode = string.Empty;
        IndicatorName = string.Empty;
        Category = string.Empty;
        IndicatorType = string.Empty;
        IndicatorDescription = string.Empty;
        ScoringCriteria = string.Empty;
        DataSource = string.Empty;
        EvaluationCycle = string.Empty;
    }

        /// <summary>
    /// 指标编码
    /// </summary>
    public string IndicatorCode { get; set; }

        /// <summary>
    /// 指标名称
    /// </summary>
    public string IndicatorName { get; set; }

        /// <summary>
    /// 指标类别
    /// </summary>
    public string Category { get; set; }

        /// <summary>
    /// 指标类型
    /// </summary>
    public string IndicatorType { get; set; }

        /// <summary>
    /// 指标说明
    /// </summary>
    public string IndicatorDescription { get; set; }

        /// <summary>
    /// 评分标准说明
    /// </summary>
    public string ScoringCriteria { get; set; }

        /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }

        /// <summary>
    /// 最低要求值
    /// </summary>
    public decimal MinimumValue { get; set; }

        /// <summary>
    /// 卓越值
    /// </summary>
    public decimal ExcellentValue { get; set; }

        /// <summary>
    /// 标准权重
    /// </summary>
    public decimal StandardWeight { get; set; }

        /// <summary>
    /// 数据来源
    /// </summary>
    public string DataSource { get; set; }

        /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationCycle { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// 绩效指标表导入DTO
/// </summary>
public partial class TaktPerformanceIndicatorImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorImportDto()
    {
        IndicatorCode = string.Empty;
        IndicatorName = string.Empty;
        Category = string.Empty;
        IndicatorType = string.Empty;
        IndicatorDescription = string.Empty;
        ScoringCriteria = string.Empty;
        DataSource = string.Empty;
        EvaluationCycle = string.Empty;
    }

        /// <summary>
    /// 指标编码
    /// </summary>
    public string IndicatorCode { get; set; }

        /// <summary>
    /// 指标名称
    /// </summary>
    public string IndicatorName { get; set; }

        /// <summary>
    /// 指标类别
    /// </summary>
    public string Category { get; set; }

        /// <summary>
    /// 指标类型
    /// </summary>
    public string IndicatorType { get; set; }

        /// <summary>
    /// 指标说明
    /// </summary>
    public string IndicatorDescription { get; set; }

        /// <summary>
    /// 评分标准说明
    /// </summary>
    public string ScoringCriteria { get; set; }

        /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }

        /// <summary>
    /// 最低要求值
    /// </summary>
    public decimal MinimumValue { get; set; }

        /// <summary>
    /// 卓越值
    /// </summary>
    public decimal ExcellentValue { get; set; }

        /// <summary>
    /// 标准权重
    /// </summary>
    public decimal StandardWeight { get; set; }

        /// <summary>
    /// 数据来源
    /// </summary>
    public string DataSource { get; set; }

        /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationCycle { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

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
/// 绩效指标表导出DTO
/// </summary>
public partial class TaktPerformanceIndicatorExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorExportDto()
    {
        CreatedAt = DateTime.Now;
        IndicatorCode = string.Empty;
        IndicatorName = string.Empty;
        Category = string.Empty;
        IndicatorType = string.Empty;
        IndicatorDescription = string.Empty;
        ScoringCriteria = string.Empty;
        DataSource = string.Empty;
        EvaluationCycle = string.Empty;
    }

        /// <summary>
    /// 指标编码
    /// </summary>
    public string IndicatorCode { get; set; }

        /// <summary>
    /// 指标名称
    /// </summary>
    public string IndicatorName { get; set; }

        /// <summary>
    /// 指标类别
    /// </summary>
    public string Category { get; set; }

        /// <summary>
    /// 指标类型
    /// </summary>
    public string IndicatorType { get; set; }

        /// <summary>
    /// 指标说明
    /// </summary>
    public string IndicatorDescription { get; set; }

        /// <summary>
    /// 评分标准说明
    /// </summary>
    public string ScoringCriteria { get; set; }

        /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }

        /// <summary>
    /// 最低要求值
    /// </summary>
    public decimal MinimumValue { get; set; }

        /// <summary>
    /// 卓越值
    /// </summary>
    public decimal ExcellentValue { get; set; }

        /// <summary>
    /// 标准权重
    /// </summary>
    public decimal StandardWeight { get; set; }

        /// <summary>
    /// 数据来源
    /// </summary>
    public string DataSource { get; set; }

        /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationCycle { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}