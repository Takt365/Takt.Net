// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Performance
// 文件名称：TaktPerformanceReviewDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：绩效评审表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Performance;

/// <summary>
/// 绩效评审表Dto
/// </summary>
public partial class TaktPerformanceReviewDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceReviewDto()
    {
        ReviewPeriod = string.Empty;
        SelfEvaluationNotes = string.Empty;
        SupervisorComments = string.Empty;
        PerformanceGrade = string.Empty;
        InterviewNotes = string.Empty;
        EmployeeFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

    /// <summary>
    /// 绩效评审表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceReviewId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 评审周期
    /// </summary>
    public string ReviewPeriod { get; set; }
    /// <summary>
    /// 评审日期
    /// </summary>
    public DateTime ReviewDate { get; set; }
    /// <summary>
    /// 绩效方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformancePlanId { get; set; }
    /// <summary>
    /// 自评分数
    /// </summary>
    public decimal SelfScore { get; set; }
    /// <summary>
    /// 自评说明
    /// </summary>
    public string SelfEvaluationNotes { get; set; }
    /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }
    /// <summary>
    /// 主管评语
    /// </summary>
    public string SupervisorComments { get; set; }
    /// <summary>
    /// 综合得分
    /// </summary>
    public decimal FinalScore { get; set; }
    /// <summary>
    /// 绩效等级
    /// </summary>
    public string PerformanceGrade { get; set; }
    /// <summary>
    /// 评审人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReviewerId { get; set; }
    /// <summary>
    /// 面谈日期
    /// </summary>
    public DateTime InterviewDate { get; set; }
    /// <summary>
    /// 面谈记录
    /// </summary>
    public string InterviewNotes { get; set; }
    /// <summary>
    /// 员工反馈
    /// </summary>
    public string EmployeeFeedback { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }
    /// <summary>
    /// 下次评审日期
    /// </summary>
    public DateTime NextReviewDate { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 绩效评审表查询DTO
/// </summary>
public partial class TaktPerformanceReviewQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceReviewQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 评审周期
    /// </summary>
    public string? ReviewPeriod { get; set; }
    /// <summary>
    /// 评审日期
    /// </summary>
    public DateTime? ReviewDate { get; set; }

    /// <summary>
    /// 评审日期开始时间
    /// </summary>
    public DateTime? ReviewDateStart { get; set; }
    /// <summary>
    /// 评审日期结束时间
    /// </summary>
    public DateTime? ReviewDateEnd { get; set; }
    /// <summary>
    /// 绩效方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PerformancePlanId { get; set; }
    /// <summary>
    /// 自评分数
    /// </summary>
    public decimal? SelfScore { get; set; }
    /// <summary>
    /// 自评说明
    /// </summary>
    public string? SelfEvaluationNotes { get; set; }
    /// <summary>
    /// 主管评分
    /// </summary>
    public decimal? SupervisorScore { get; set; }
    /// <summary>
    /// 主管评语
    /// </summary>
    public string? SupervisorComments { get; set; }
    /// <summary>
    /// 综合得分
    /// </summary>
    public decimal? FinalScore { get; set; }
    /// <summary>
    /// 绩效等级
    /// </summary>
    public string? PerformanceGrade { get; set; }
    /// <summary>
    /// 评审人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ReviewerId { get; set; }
    /// <summary>
    /// 面谈日期
    /// </summary>
    public DateTime? InterviewDate { get; set; }

    /// <summary>
    /// 面谈日期开始时间
    /// </summary>
    public DateTime? InterviewDateStart { get; set; }
    /// <summary>
    /// 面谈日期结束时间
    /// </summary>
    public DateTime? InterviewDateEnd { get; set; }
    /// <summary>
    /// 面谈记录
    /// </summary>
    public string? InterviewNotes { get; set; }
    /// <summary>
    /// 员工反馈
    /// </summary>
    public string? EmployeeFeedback { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestions { get; set; }
    /// <summary>
    /// 下次评审日期
    /// </summary>
    public DateTime? NextReviewDate { get; set; }

    /// <summary>
    /// 下次评审日期开始时间
    /// </summary>
    public DateTime? NextReviewDateStart { get; set; }
    /// <summary>
    /// 下次评审日期结束时间
    /// </summary>
    public DateTime? NextReviewDateEnd { get; set; }
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
/// Takt创建绩效评审表DTO
/// </summary>
public partial class TaktPerformanceReviewCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceReviewCreateDto()
    {
        ReviewPeriod = string.Empty;
        SelfEvaluationNotes = string.Empty;
        SupervisorComments = string.Empty;
        PerformanceGrade = string.Empty;
        InterviewNotes = string.Empty;
        EmployeeFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 评审周期
    /// </summary>
    public string ReviewPeriod { get; set; }

        /// <summary>
    /// 评审日期
    /// </summary>
    public DateTime ReviewDate { get; set; }

        /// <summary>
    /// 绩效方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformancePlanId { get; set; }

        /// <summary>
    /// 自评分数
    /// </summary>
    public decimal SelfScore { get; set; }

        /// <summary>
    /// 自评说明
    /// </summary>
    public string SelfEvaluationNotes { get; set; }

        /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }

        /// <summary>
    /// 主管评语
    /// </summary>
    public string SupervisorComments { get; set; }

        /// <summary>
    /// 综合得分
    /// </summary>
    public decimal FinalScore { get; set; }

        /// <summary>
    /// 绩效等级
    /// </summary>
    public string PerformanceGrade { get; set; }

        /// <summary>
    /// 评审人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReviewerId { get; set; }

        /// <summary>
    /// 面谈日期
    /// </summary>
    public DateTime InterviewDate { get; set; }

        /// <summary>
    /// 面谈记录
    /// </summary>
    public string InterviewNotes { get; set; }

        /// <summary>
    /// 员工反馈
    /// </summary>
    public string EmployeeFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 下次评审日期
    /// </summary>
    public DateTime NextReviewDate { get; set; }

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
/// Takt更新绩效评审表DTO
/// </summary>
public partial class TaktPerformanceReviewUpdateDto : TaktPerformanceReviewCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceReviewUpdateDto()
    {
    }

        /// <summary>
    /// 绩效评审表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceReviewId { get; set; } = 0;
}

/// <summary>
/// 绩效评审表状态DTO
/// </summary>
public partial class TaktPerformanceReviewStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceReviewStatusDto()
    {
    }

        /// <summary>
    /// 绩效评审表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceReviewId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 绩效评审表导入模板DTO
/// </summary>
public partial class TaktPerformanceReviewTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceReviewTemplateDto()
    {
        ReviewPeriod = string.Empty;
        SelfEvaluationNotes = string.Empty;
        SupervisorComments = string.Empty;
        PerformanceGrade = string.Empty;
        InterviewNotes = string.Empty;
        EmployeeFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 评审周期
    /// </summary>
    public string ReviewPeriod { get; set; }

        /// <summary>
    /// 评审日期
    /// </summary>
    public DateTime ReviewDate { get; set; }

        /// <summary>
    /// 绩效方案ID
    /// </summary>
    public long PerformancePlanId { get; set; }

        /// <summary>
    /// 自评分数
    /// </summary>
    public decimal SelfScore { get; set; }

        /// <summary>
    /// 自评说明
    /// </summary>
    public string SelfEvaluationNotes { get; set; }

        /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }

        /// <summary>
    /// 主管评语
    /// </summary>
    public string SupervisorComments { get; set; }

        /// <summary>
    /// 综合得分
    /// </summary>
    public decimal FinalScore { get; set; }

        /// <summary>
    /// 绩效等级
    /// </summary>
    public string PerformanceGrade { get; set; }

        /// <summary>
    /// 评审人ID
    /// </summary>
    public long ReviewerId { get; set; }

        /// <summary>
    /// 面谈日期
    /// </summary>
    public DateTime InterviewDate { get; set; }

        /// <summary>
    /// 面谈记录
    /// </summary>
    public string InterviewNotes { get; set; }

        /// <summary>
    /// 员工反馈
    /// </summary>
    public string EmployeeFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 下次评审日期
    /// </summary>
    public DateTime NextReviewDate { get; set; }

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
/// 绩效评审表导入DTO
/// </summary>
public partial class TaktPerformanceReviewImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceReviewImportDto()
    {
        ReviewPeriod = string.Empty;
        SelfEvaluationNotes = string.Empty;
        SupervisorComments = string.Empty;
        PerformanceGrade = string.Empty;
        InterviewNotes = string.Empty;
        EmployeeFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 评审周期
    /// </summary>
    public string ReviewPeriod { get; set; }

        /// <summary>
    /// 评审日期
    /// </summary>
    public DateTime ReviewDate { get; set; }

        /// <summary>
    /// 绩效方案ID
    /// </summary>
    public long PerformancePlanId { get; set; }

        /// <summary>
    /// 自评分数
    /// </summary>
    public decimal SelfScore { get; set; }

        /// <summary>
    /// 自评说明
    /// </summary>
    public string SelfEvaluationNotes { get; set; }

        /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }

        /// <summary>
    /// 主管评语
    /// </summary>
    public string SupervisorComments { get; set; }

        /// <summary>
    /// 综合得分
    /// </summary>
    public decimal FinalScore { get; set; }

        /// <summary>
    /// 绩效等级
    /// </summary>
    public string PerformanceGrade { get; set; }

        /// <summary>
    /// 评审人ID
    /// </summary>
    public long ReviewerId { get; set; }

        /// <summary>
    /// 面谈日期
    /// </summary>
    public DateTime InterviewDate { get; set; }

        /// <summary>
    /// 面谈记录
    /// </summary>
    public string InterviewNotes { get; set; }

        /// <summary>
    /// 员工反馈
    /// </summary>
    public string EmployeeFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 下次评审日期
    /// </summary>
    public DateTime NextReviewDate { get; set; }

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
/// 绩效评审表导出DTO
/// </summary>
public partial class TaktPerformanceReviewExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceReviewExportDto()
    {
        CreatedAt = DateTime.Now;
        ReviewPeriod = string.Empty;
        SelfEvaluationNotes = string.Empty;
        SupervisorComments = string.Empty;
        PerformanceGrade = string.Empty;
        InterviewNotes = string.Empty;
        EmployeeFeedback = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 评审周期
    /// </summary>
    public string ReviewPeriod { get; set; }

        /// <summary>
    /// 评审日期
    /// </summary>
    public DateTime ReviewDate { get; set; }

        /// <summary>
    /// 绩效方案ID
    /// </summary>
    public long PerformancePlanId { get; set; }

        /// <summary>
    /// 自评分数
    /// </summary>
    public decimal SelfScore { get; set; }

        /// <summary>
    /// 自评说明
    /// </summary>
    public string SelfEvaluationNotes { get; set; }

        /// <summary>
    /// 主管评分
    /// </summary>
    public decimal SupervisorScore { get; set; }

        /// <summary>
    /// 主管评语
    /// </summary>
    public string SupervisorComments { get; set; }

        /// <summary>
    /// 综合得分
    /// </summary>
    public decimal FinalScore { get; set; }

        /// <summary>
    /// 绩效等级
    /// </summary>
    public string PerformanceGrade { get; set; }

        /// <summary>
    /// 评审人ID
    /// </summary>
    public long ReviewerId { get; set; }

        /// <summary>
    /// 面谈日期
    /// </summary>
    public DateTime InterviewDate { get; set; }

        /// <summary>
    /// 面谈记录
    /// </summary>
    public string InterviewNotes { get; set; }

        /// <summary>
    /// 员工反馈
    /// </summary>
    public string EmployeeFeedback { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 下次评审日期
    /// </summary>
    public DateTime NextReviewDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}