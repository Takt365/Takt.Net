// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Performance
// 文件名称：TaktImprovementPlanDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：改进计划表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Performance;

/// <summary>
/// 改进计划表Dto
/// </summary>
public partial class TaktImprovementPlanDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktImprovementPlanDto()
    {
        PlanTitle = string.Empty;
        ImprovementArea = string.Empty;
        CurrentSituation = string.Empty;
        ImprovementGoal = string.Empty;
        ImprovementActions = string.Empty;
        RequiredResources = string.Empty;
        MidtermCheckResult = string.Empty;
        ResultDescription = string.Empty;
    }

    /// <summary>
    /// 改进计划表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ImprovementPlanId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 绩效评审ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceReviewId { get; set; }
    /// <summary>
    /// 改进计划标题
    /// </summary>
    public string PlanTitle { get; set; }
    /// <summary>
    /// 改进领域
    /// </summary>
    public string ImprovementArea { get; set; }
    /// <summary>
    /// 当前状况描述
    /// </summary>
    public string CurrentSituation { get; set; }
    /// <summary>
    /// 改进目标
    /// </summary>
    public string ImprovementGoal { get; set; }
    /// <summary>
    /// 改进措施
    /// </summary>
    public string ImprovementActions { get; set; }
    /// <summary>
    /// 所需资源支持
    /// </summary>
    public string RequiredResources { get; set; }
    /// <summary>
    /// 计划制定日期
    /// </summary>
    public DateTime PlanDate { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// 目标完成日期
    /// </summary>
    public DateTime TargetCompletionDate { get; set; }
    /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime ActualCompletionDate { get; set; }
    /// <summary>
    /// 进度百分比
    /// </summary>
    public decimal ProgressPercentage { get; set; }
    /// <summary>
    /// 中期检查日期
    /// </summary>
    public DateTime MidtermCheckDate { get; set; }
    /// <summary>
    /// 中期检查结果
    /// </summary>
    public string MidtermCheckResult { get; set; }
    /// <summary>
    /// 改进结果说明
    /// </summary>
    public string ResultDescription { get; set; }
    /// <summary>
    /// 指导老师ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MentorId { get; set; }
    /// <summary>
    /// 审批人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApproverId { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 改进计划表查询DTO
/// </summary>
public partial class TaktImprovementPlanQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktImprovementPlanQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 绩效评审ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PerformanceReviewId { get; set; }
    /// <summary>
    /// 改进计划标题
    /// </summary>
    public string? PlanTitle { get; set; }
    /// <summary>
    /// 改进领域
    /// </summary>
    public string? ImprovementArea { get; set; }
    /// <summary>
    /// 当前状况描述
    /// </summary>
    public string? CurrentSituation { get; set; }
    /// <summary>
    /// 改进目标
    /// </summary>
    public string? ImprovementGoal { get; set; }
    /// <summary>
    /// 改进措施
    /// </summary>
    public string? ImprovementActions { get; set; }
    /// <summary>
    /// 所需资源支持
    /// </summary>
    public string? RequiredResources { get; set; }
    /// <summary>
    /// 计划制定日期
    /// </summary>
    public DateTime? PlanDate { get; set; }

    /// <summary>
    /// 计划制定日期开始时间
    /// </summary>
    public DateTime? PlanDateStart { get; set; }
    /// <summary>
    /// 计划制定日期结束时间
    /// </summary>
    public DateTime? PlanDateEnd { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 开始日期开始时间
    /// </summary>
    public DateTime? StartDateStart { get; set; }
    /// <summary>
    /// 开始日期结束时间
    /// </summary>
    public DateTime? StartDateEnd { get; set; }
    /// <summary>
    /// 目标完成日期
    /// </summary>
    public DateTime? TargetCompletionDate { get; set; }

    /// <summary>
    /// 目标完成日期开始时间
    /// </summary>
    public DateTime? TargetCompletionDateStart { get; set; }
    /// <summary>
    /// 目标完成日期结束时间
    /// </summary>
    public DateTime? TargetCompletionDateEnd { get; set; }
    /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }

    /// <summary>
    /// 实际完成日期开始时间
    /// </summary>
    public DateTime? ActualCompletionDateStart { get; set; }
    /// <summary>
    /// 实际完成日期结束时间
    /// </summary>
    public DateTime? ActualCompletionDateEnd { get; set; }
    /// <summary>
    /// 进度百分比
    /// </summary>
    public decimal? ProgressPercentage { get; set; }
    /// <summary>
    /// 中期检查日期
    /// </summary>
    public DateTime? MidtermCheckDate { get; set; }

    /// <summary>
    /// 中期检查日期开始时间
    /// </summary>
    public DateTime? MidtermCheckDateStart { get; set; }
    /// <summary>
    /// 中期检查日期结束时间
    /// </summary>
    public DateTime? MidtermCheckDateEnd { get; set; }
    /// <summary>
    /// 中期检查结果
    /// </summary>
    public string? MidtermCheckResult { get; set; }
    /// <summary>
    /// 改进结果说明
    /// </summary>
    public string? ResultDescription { get; set; }
    /// <summary>
    /// 指导老师ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? MentorId { get; set; }
    /// <summary>
    /// 审批人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApproverId { get; set; }
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
/// Takt创建改进计划表DTO
/// </summary>
public partial class TaktImprovementPlanCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktImprovementPlanCreateDto()
    {
        PlanTitle = string.Empty;
        ImprovementArea = string.Empty;
        CurrentSituation = string.Empty;
        ImprovementGoal = string.Empty;
        ImprovementActions = string.Empty;
        RequiredResources = string.Empty;
        MidtermCheckResult = string.Empty;
        ResultDescription = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 绩效评审ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceReviewId { get; set; }

        /// <summary>
    /// 改进计划标题
    /// </summary>
    public string PlanTitle { get; set; }

        /// <summary>
    /// 改进领域
    /// </summary>
    public string ImprovementArea { get; set; }

        /// <summary>
    /// 当前状况描述
    /// </summary>
    public string CurrentSituation { get; set; }

        /// <summary>
    /// 改进目标
    /// </summary>
    public string ImprovementGoal { get; set; }

        /// <summary>
    /// 改进措施
    /// </summary>
    public string ImprovementActions { get; set; }

        /// <summary>
    /// 所需资源支持
    /// </summary>
    public string RequiredResources { get; set; }

        /// <summary>
    /// 计划制定日期
    /// </summary>
    public DateTime PlanDate { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 目标完成日期
    /// </summary>
    public DateTime TargetCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime ActualCompletionDate { get; set; }

        /// <summary>
    /// 进度百分比
    /// </summary>
    public decimal ProgressPercentage { get; set; }

        /// <summary>
    /// 中期检查日期
    /// </summary>
    public DateTime MidtermCheckDate { get; set; }

        /// <summary>
    /// 中期检查结果
    /// </summary>
    public string MidtermCheckResult { get; set; }

        /// <summary>
    /// 改进结果说明
    /// </summary>
    public string ResultDescription { get; set; }

        /// <summary>
    /// 指导老师ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MentorId { get; set; }

        /// <summary>
    /// 审批人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApproverId { get; set; }

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
/// Takt更新改进计划表DTO
/// </summary>
public partial class TaktImprovementPlanUpdateDto : TaktImprovementPlanCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktImprovementPlanUpdateDto()
    {
    }

        /// <summary>
    /// 改进计划表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ImprovementPlanId { get; set; } = 0;
}

/// <summary>
/// 改进计划表状态DTO
/// </summary>
public partial class TaktImprovementPlanStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktImprovementPlanStatusDto()
    {
    }

        /// <summary>
    /// 改进计划表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ImprovementPlanId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 改进计划表导入模板DTO
/// </summary>
public partial class TaktImprovementPlanTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktImprovementPlanTemplateDto()
    {
        PlanTitle = string.Empty;
        ImprovementArea = string.Empty;
        CurrentSituation = string.Empty;
        ImprovementGoal = string.Empty;
        ImprovementActions = string.Empty;
        RequiredResources = string.Empty;
        MidtermCheckResult = string.Empty;
        ResultDescription = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 绩效评审ID
    /// </summary>
    public long PerformanceReviewId { get; set; }

        /// <summary>
    /// 改进计划标题
    /// </summary>
    public string PlanTitle { get; set; }

        /// <summary>
    /// 改进领域
    /// </summary>
    public string ImprovementArea { get; set; }

        /// <summary>
    /// 当前状况描述
    /// </summary>
    public string CurrentSituation { get; set; }

        /// <summary>
    /// 改进目标
    /// </summary>
    public string ImprovementGoal { get; set; }

        /// <summary>
    /// 改进措施
    /// </summary>
    public string ImprovementActions { get; set; }

        /// <summary>
    /// 所需资源支持
    /// </summary>
    public string RequiredResources { get; set; }

        /// <summary>
    /// 计划制定日期
    /// </summary>
    public DateTime PlanDate { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 目标完成日期
    /// </summary>
    public DateTime TargetCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime ActualCompletionDate { get; set; }

        /// <summary>
    /// 进度百分比
    /// </summary>
    public decimal ProgressPercentage { get; set; }

        /// <summary>
    /// 中期检查日期
    /// </summary>
    public DateTime MidtermCheckDate { get; set; }

        /// <summary>
    /// 中期检查结果
    /// </summary>
    public string MidtermCheckResult { get; set; }

        /// <summary>
    /// 改进结果说明
    /// </summary>
    public string ResultDescription { get; set; }

        /// <summary>
    /// 指导老师ID
    /// </summary>
    public long MentorId { get; set; }

        /// <summary>
    /// 审批人ID
    /// </summary>
    public long ApproverId { get; set; }

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
/// 改进计划表导入DTO
/// </summary>
public partial class TaktImprovementPlanImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktImprovementPlanImportDto()
    {
        PlanTitle = string.Empty;
        ImprovementArea = string.Empty;
        CurrentSituation = string.Empty;
        ImprovementGoal = string.Empty;
        ImprovementActions = string.Empty;
        RequiredResources = string.Empty;
        MidtermCheckResult = string.Empty;
        ResultDescription = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 绩效评审ID
    /// </summary>
    public long PerformanceReviewId { get; set; }

        /// <summary>
    /// 改进计划标题
    /// </summary>
    public string PlanTitle { get; set; }

        /// <summary>
    /// 改进领域
    /// </summary>
    public string ImprovementArea { get; set; }

        /// <summary>
    /// 当前状况描述
    /// </summary>
    public string CurrentSituation { get; set; }

        /// <summary>
    /// 改进目标
    /// </summary>
    public string ImprovementGoal { get; set; }

        /// <summary>
    /// 改进措施
    /// </summary>
    public string ImprovementActions { get; set; }

        /// <summary>
    /// 所需资源支持
    /// </summary>
    public string RequiredResources { get; set; }

        /// <summary>
    /// 计划制定日期
    /// </summary>
    public DateTime PlanDate { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 目标完成日期
    /// </summary>
    public DateTime TargetCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime ActualCompletionDate { get; set; }

        /// <summary>
    /// 进度百分比
    /// </summary>
    public decimal ProgressPercentage { get; set; }

        /// <summary>
    /// 中期检查日期
    /// </summary>
    public DateTime MidtermCheckDate { get; set; }

        /// <summary>
    /// 中期检查结果
    /// </summary>
    public string MidtermCheckResult { get; set; }

        /// <summary>
    /// 改进结果说明
    /// </summary>
    public string ResultDescription { get; set; }

        /// <summary>
    /// 指导老师ID
    /// </summary>
    public long MentorId { get; set; }

        /// <summary>
    /// 审批人ID
    /// </summary>
    public long ApproverId { get; set; }

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
/// 改进计划表导出DTO
/// </summary>
public partial class TaktImprovementPlanExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktImprovementPlanExportDto()
    {
        CreatedAt = DateTime.Now;
        PlanTitle = string.Empty;
        ImprovementArea = string.Empty;
        CurrentSituation = string.Empty;
        ImprovementGoal = string.Empty;
        ImprovementActions = string.Empty;
        RequiredResources = string.Empty;
        MidtermCheckResult = string.Empty;
        ResultDescription = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 绩效评审ID
    /// </summary>
    public long PerformanceReviewId { get; set; }

        /// <summary>
    /// 改进计划标题
    /// </summary>
    public string PlanTitle { get; set; }

        /// <summary>
    /// 改进领域
    /// </summary>
    public string ImprovementArea { get; set; }

        /// <summary>
    /// 当前状况描述
    /// </summary>
    public string CurrentSituation { get; set; }

        /// <summary>
    /// 改进目标
    /// </summary>
    public string ImprovementGoal { get; set; }

        /// <summary>
    /// 改进措施
    /// </summary>
    public string ImprovementActions { get; set; }

        /// <summary>
    /// 所需资源支持
    /// </summary>
    public string RequiredResources { get; set; }

        /// <summary>
    /// 计划制定日期
    /// </summary>
    public DateTime PlanDate { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 目标完成日期
    /// </summary>
    public DateTime TargetCompletionDate { get; set; }

        /// <summary>
    /// 实际完成日期
    /// </summary>
    public DateTime ActualCompletionDate { get; set; }

        /// <summary>
    /// 进度百分比
    /// </summary>
    public decimal ProgressPercentage { get; set; }

        /// <summary>
    /// 中期检查日期
    /// </summary>
    public DateTime MidtermCheckDate { get; set; }

        /// <summary>
    /// 中期检查结果
    /// </summary>
    public string MidtermCheckResult { get; set; }

        /// <summary>
    /// 改进结果说明
    /// </summary>
    public string ResultDescription { get; set; }

        /// <summary>
    /// 指导老师ID
    /// </summary>
    public long MentorId { get; set; }

        /// <summary>
    /// 审批人ID
    /// </summary>
    public long ApproverId { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}