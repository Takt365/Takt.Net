// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Performance
// 文件名称：TaktPerformanceDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：绩效考核表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Performance;

/// <summary>
/// 绩效考核表Dto
/// </summary>
public partial class TaktPerformanceDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceDto()
    {
        EvaluationPeriod = string.Empty;
        EvaluationCriteria = string.Empty;
        Grade = string.Empty;
        Comments = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

    /// <summary>
    /// 绩效考核表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationPeriod { get; set; }
    /// <summary>
    /// 考核日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }
    /// <summary>
    /// 考核指标
    /// </summary>
    public string EvaluationCriteria { get; set; }
    /// <summary>
    /// 考核得分
    /// </summary>
    public decimal Score { get; set; }
    /// <summary>
    /// 绩效等级
    /// </summary>
    public string Grade { get; set; }
    /// <summary>
    /// 自评得分
    /// </summary>
    public decimal SelfScore { get; set; }
    /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }
    /// <summary>
    /// 考核评语
    /// </summary>
    public string Comments { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }
    /// <summary>
    /// 考核人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EvaluatorId { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 绩效考核表查询DTO
/// </summary>
public partial class TaktPerformanceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 考核周期
    /// </summary>
    public string? EvaluationPeriod { get; set; }
    /// <summary>
    /// 考核日期
    /// </summary>
    public DateTime? EvaluationDate { get; set; }

    /// <summary>
    /// 考核日期开始时间
    /// </summary>
    public DateTime? EvaluationDateStart { get; set; }
    /// <summary>
    /// 考核日期结束时间
    /// </summary>
    public DateTime? EvaluationDateEnd { get; set; }
    /// <summary>
    /// 考核指标
    /// </summary>
    public string? EvaluationCriteria { get; set; }
    /// <summary>
    /// 考核得分
    /// </summary>
    public decimal? Score { get; set; }
    /// <summary>
    /// 绩效等级
    /// </summary>
    public string? Grade { get; set; }
    /// <summary>
    /// 自评得分
    /// </summary>
    public decimal? SelfScore { get; set; }
    /// <summary>
    /// 主管评分
    /// </summary>
    public decimal? SupervisorScore { get; set; }
    /// <summary>
    /// 考核评语
    /// </summary>
    public string? Comments { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestions { get; set; }
    /// <summary>
    /// 考核人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EvaluatorId { get; set; }
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
/// Takt创建绩效考核表DTO
/// </summary>
public partial class TaktPerformanceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceCreateDto()
    {
        EvaluationPeriod = string.Empty;
        EvaluationCriteria = string.Empty;
        Grade = string.Empty;
        Comments = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationPeriod { get; set; }

        /// <summary>
    /// 考核日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 考核指标
    /// </summary>
    public string EvaluationCriteria { get; set; }

        /// <summary>
    /// 考核得分
    /// </summary>
    public decimal Score { get; set; }

        /// <summary>
    /// 绩效等级
    /// </summary>
    public string Grade { get; set; }

        /// <summary>
    /// 自评得分
    /// </summary>
    public decimal SelfScore { get; set; }

        /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }

        /// <summary>
    /// 考核评语
    /// </summary>
    public string Comments { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 考核人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EvaluatorId { get; set; }

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
/// Takt更新绩效考核表DTO
/// </summary>
public partial class TaktPerformanceUpdateDto : TaktPerformanceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceUpdateDto()
    {
    }

        /// <summary>
    /// 绩效考核表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceId { get; set; } = 0;
}

/// <summary>
/// 绩效考核表状态DTO
/// </summary>
public partial class TaktPerformanceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceStatusDto()
    {
    }

        /// <summary>
    /// 绩效考核表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 绩效考核表导入模板DTO
/// </summary>
public partial class TaktPerformanceTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceTemplateDto()
    {
        EvaluationPeriod = string.Empty;
        EvaluationCriteria = string.Empty;
        Grade = string.Empty;
        Comments = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationPeriod { get; set; }

        /// <summary>
    /// 考核日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 考核指标
    /// </summary>
    public string EvaluationCriteria { get; set; }

        /// <summary>
    /// 考核得分
    /// </summary>
    public decimal Score { get; set; }

        /// <summary>
    /// 绩效等级
    /// </summary>
    public string Grade { get; set; }

        /// <summary>
    /// 自评得分
    /// </summary>
    public decimal SelfScore { get; set; }

        /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }

        /// <summary>
    /// 考核评语
    /// </summary>
    public string Comments { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 考核人ID
    /// </summary>
    public long EvaluatorId { get; set; }

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
/// 绩效考核表导入DTO
/// </summary>
public partial class TaktPerformanceImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceImportDto()
    {
        EvaluationPeriod = string.Empty;
        EvaluationCriteria = string.Empty;
        Grade = string.Empty;
        Comments = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationPeriod { get; set; }

        /// <summary>
    /// 考核日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 考核指标
    /// </summary>
    public string EvaluationCriteria { get; set; }

        /// <summary>
    /// 考核得分
    /// </summary>
    public decimal Score { get; set; }

        /// <summary>
    /// 绩效等级
    /// </summary>
    public string Grade { get; set; }

        /// <summary>
    /// 自评得分
    /// </summary>
    public decimal SelfScore { get; set; }

        /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }

        /// <summary>
    /// 考核评语
    /// </summary>
    public string Comments { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 考核人ID
    /// </summary>
    public long EvaluatorId { get; set; }

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
/// 绩效考核表导出DTO
/// </summary>
public partial class TaktPerformanceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceExportDto()
    {
        CreatedAt = DateTime.Now;
        EvaluationPeriod = string.Empty;
        EvaluationCriteria = string.Empty;
        Grade = string.Empty;
        Comments = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 考核周期
    /// </summary>
    public string EvaluationPeriod { get; set; }

        /// <summary>
    /// 考核日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 考核指标
    /// </summary>
    public string EvaluationCriteria { get; set; }

        /// <summary>
    /// 考核得分
    /// </summary>
    public decimal Score { get; set; }

        /// <summary>
    /// 绩效等级
    /// </summary>
    public string Grade { get; set; }

        /// <summary>
    /// 自评得分
    /// </summary>
    public decimal SelfScore { get; set; }

        /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }

        /// <summary>
    /// 考核评语
    /// </summary>
    public string Comments { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 考核人ID
    /// </summary>
    public long EvaluatorId { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}