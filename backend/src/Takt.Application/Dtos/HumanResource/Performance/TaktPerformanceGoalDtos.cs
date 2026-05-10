// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Performance
// 文件名称：TaktPerformanceGoalDtos.cs
// 创建时间：2026-05-10
// 创建人：Takt365
// 功能描述：绩效目标表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Performance;

/// <summary>
/// 绩效目标表Dto
/// </summary>
public partial class TaktPerformanceGoalDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceGoalDto()
    {
        GoalPeriod = string.Empty;
        GoalDescription = string.Empty;
        AchievementNotes = string.Empty;
        FailureReason = string.Empty;
    }

    /// <summary>
    /// 绩效目标表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceGoalId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 绩效指标ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceIndicatorId { get; set; }
    /// <summary>
    /// 目标周期
    /// </summary>
    public string GoalPeriod { get; set; }
    /// <summary>
    /// 目标描述
    /// </summary>
    public string GoalDescription { get; set; }
    /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }
    /// <summary>
    /// 实际完成值
    /// </summary>
    public decimal ActualValue { get; set; }
    /// <summary>
    /// 完成百分比
    /// </summary>
    public decimal CompletionPercentage { get; set; }
    /// <summary>
    /// 目标权重
    /// </summary>
    public decimal GoalWeight { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// 截止日期
    /// </summary>
    public DateTime DueDate { get; set; }
    /// <summary>
    /// 完成日期
    /// </summary>
    public DateTime CompletionDate { get; set; }
    /// <summary>
    /// 目标达成说明
    /// </summary>
    public string AchievementNotes { get; set; }
    /// <summary>
    /// 未达成原因
    /// </summary>
    public string FailureReason { get; set; }
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
/// 绩效目标表查询DTO
/// </summary>
public partial class TaktPerformanceGoalQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceGoalQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 绩效指标ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PerformanceIndicatorId { get; set; }
    /// <summary>
    /// 目标周期
    /// </summary>
    public string? GoalPeriod { get; set; }
    /// <summary>
    /// 目标描述
    /// </summary>
    public string? GoalDescription { get; set; }
    /// <summary>
    /// 目标值
    /// </summary>
    public decimal? TargetValue { get; set; }
    /// <summary>
    /// 实际完成值
    /// </summary>
    public decimal? ActualValue { get; set; }
    /// <summary>
    /// 完成百分比
    /// </summary>
    public decimal? CompletionPercentage { get; set; }
    /// <summary>
    /// 目标权重
    /// </summary>
    public decimal? GoalWeight { get; set; }
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
    /// 截止日期
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// 截止日期开始时间
    /// </summary>
    public DateTime? DueDateStart { get; set; }
    /// <summary>
    /// 截止日期结束时间
    /// </summary>
    public DateTime? DueDateEnd { get; set; }
    /// <summary>
    /// 完成日期
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// 完成日期开始时间
    /// </summary>
    public DateTime? CompletionDateStart { get; set; }
    /// <summary>
    /// 完成日期结束时间
    /// </summary>
    public DateTime? CompletionDateEnd { get; set; }
    /// <summary>
    /// 目标达成说明
    /// </summary>
    public string? AchievementNotes { get; set; }
    /// <summary>
    /// 未达成原因
    /// </summary>
    public string? FailureReason { get; set; }
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
/// Takt创建绩效目标表DTO
/// </summary>
public partial class TaktPerformanceGoalCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceGoalCreateDto()
    {
        GoalPeriod = string.Empty;
        GoalDescription = string.Empty;
        AchievementNotes = string.Empty;
        FailureReason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 绩效指标ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceIndicatorId { get; set; }

        /// <summary>
    /// 目标周期
    /// </summary>
    public string GoalPeriod { get; set; }

        /// <summary>
    /// 目标描述
    /// </summary>
    public string GoalDescription { get; set; }

        /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }

        /// <summary>
    /// 实际完成值
    /// </summary>
    public decimal ActualValue { get; set; }

        /// <summary>
    /// 完成百分比
    /// </summary>
    public decimal CompletionPercentage { get; set; }

        /// <summary>
    /// 目标权重
    /// </summary>
    public decimal GoalWeight { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 截止日期
    /// </summary>
    public DateTime DueDate { get; set; }

        /// <summary>
    /// 完成日期
    /// </summary>
    public DateTime CompletionDate { get; set; }

        /// <summary>
    /// 目标达成说明
    /// </summary>
    public string AchievementNotes { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string FailureReason { get; set; }

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
/// Takt更新绩效目标表DTO
/// </summary>
public partial class TaktPerformanceGoalUpdateDto : TaktPerformanceGoalCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceGoalUpdateDto()
    {
    }

        /// <summary>
    /// 绩效目标表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceGoalId { get; set; } = 0;
}

/// <summary>
/// 绩效目标表状态DTO
/// </summary>
public partial class TaktPerformanceGoalStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceGoalStatusDto()
    {
    }

        /// <summary>
    /// 绩效目标表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformanceGoalId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 绩效目标表导入模板DTO
/// </summary>
public partial class TaktPerformanceGoalTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceGoalTemplateDto()
    {
        GoalPeriod = string.Empty;
        GoalDescription = string.Empty;
        AchievementNotes = string.Empty;
        FailureReason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 绩效指标ID
    /// </summary>
    public long PerformanceIndicatorId { get; set; }

        /// <summary>
    /// 目标周期
    /// </summary>
    public string GoalPeriod { get; set; }

        /// <summary>
    /// 目标描述
    /// </summary>
    public string GoalDescription { get; set; }

        /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }

        /// <summary>
    /// 实际完成值
    /// </summary>
    public decimal ActualValue { get; set; }

        /// <summary>
    /// 完成百分比
    /// </summary>
    public decimal CompletionPercentage { get; set; }

        /// <summary>
    /// 目标权重
    /// </summary>
    public decimal GoalWeight { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 截止日期
    /// </summary>
    public DateTime DueDate { get; set; }

        /// <summary>
    /// 完成日期
    /// </summary>
    public DateTime CompletionDate { get; set; }

        /// <summary>
    /// 目标达成说明
    /// </summary>
    public string AchievementNotes { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string FailureReason { get; set; }

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
/// 绩效目标表导入DTO
/// </summary>
public partial class TaktPerformanceGoalImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceGoalImportDto()
    {
        GoalPeriod = string.Empty;
        GoalDescription = string.Empty;
        AchievementNotes = string.Empty;
        FailureReason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 绩效指标ID
    /// </summary>
    public long PerformanceIndicatorId { get; set; }

        /// <summary>
    /// 目标周期
    /// </summary>
    public string GoalPeriod { get; set; }

        /// <summary>
    /// 目标描述
    /// </summary>
    public string GoalDescription { get; set; }

        /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }

        /// <summary>
    /// 实际完成值
    /// </summary>
    public decimal ActualValue { get; set; }

        /// <summary>
    /// 完成百分比
    /// </summary>
    public decimal CompletionPercentage { get; set; }

        /// <summary>
    /// 目标权重
    /// </summary>
    public decimal GoalWeight { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 截止日期
    /// </summary>
    public DateTime DueDate { get; set; }

        /// <summary>
    /// 完成日期
    /// </summary>
    public DateTime CompletionDate { get; set; }

        /// <summary>
    /// 目标达成说明
    /// </summary>
    public string AchievementNotes { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string FailureReason { get; set; }

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
/// 绩效目标表导出DTO
/// </summary>
public partial class TaktPerformanceGoalExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceGoalExportDto()
    {
        CreatedAt = DateTime.Now;
        GoalPeriod = string.Empty;
        GoalDescription = string.Empty;
        AchievementNotes = string.Empty;
        FailureReason = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 绩效指标ID
    /// </summary>
    public long PerformanceIndicatorId { get; set; }

        /// <summary>
    /// 目标周期
    /// </summary>
    public string GoalPeriod { get; set; }

        /// <summary>
    /// 目标描述
    /// </summary>
    public string GoalDescription { get; set; }

        /// <summary>
    /// 目标值
    /// </summary>
    public decimal TargetValue { get; set; }

        /// <summary>
    /// 实际完成值
    /// </summary>
    public decimal ActualValue { get; set; }

        /// <summary>
    /// 完成百分比
    /// </summary>
    public decimal CompletionPercentage { get; set; }

        /// <summary>
    /// 目标权重
    /// </summary>
    public decimal GoalWeight { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 截止日期
    /// </summary>
    public DateTime DueDate { get; set; }

        /// <summary>
    /// 完成日期
    /// </summary>
    public DateTime CompletionDate { get; set; }

        /// <summary>
    /// 目标达成说明
    /// </summary>
    public string AchievementNotes { get; set; }

        /// <summary>
    /// 未达成原因
    /// </summary>
    public string FailureReason { get; set; }

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