// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Performance
// 文件名称：TaktReviewCycleDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：评审周期表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Performance;

/// <summary>
/// 评审周期表Dto
/// </summary>
public partial class TaktReviewCycleDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReviewCycleDto()
    {
        CycleCode = string.Empty;
        CycleName = string.Empty;
        CycleType = string.Empty;
        ApplicableDepartment = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// 评审周期表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReviewCycleId { get; set; } = 0;

    /// <summary>
    /// 周期编码
    /// </summary>
    public string CycleCode { get; set; }
    /// <summary>
    /// 周期名称
    /// </summary>
    public string CycleName { get; set; }
    /// <summary>
    /// 周期类型
    /// </summary>
    public string CycleType { get; set; }
    /// <summary>
    /// 周期年度
    /// </summary>
    public int CycleYear { get; set; }
    /// <summary>
    /// 周期序号
    /// </summary>
    public int CycleSequence { get; set; }
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// 目标设定开始日期
    /// </summary>
    public DateTime GoalSettingStartDate { get; set; }
    /// <summary>
    /// 目标设定截止日期
    /// </summary>
    public DateTime GoalSettingDueDate { get; set; }
    /// <summary>
    /// 自评开始日期
    /// </summary>
    public DateTime SelfEvaluationStartDate { get; set; }
    /// <summary>
    /// 自评截止日期
    /// </summary>
    public DateTime SelfEvaluationDueDate { get; set; }
    /// <summary>
    /// 主管评审开始日期
    /// </summary>
    public DateTime SupervisorReviewStartDate { get; set; }
    /// <summary>
    /// 主管评审截止日期
    /// </summary>
    public DateTime SupervisorReviewDueDate { get; set; }
    /// <summary>
    /// 面谈截止日期
    /// </summary>
    public DateTime InterviewDueDate { get; set; }
    /// <summary>
    /// 结果确认截止日期
    /// </summary>
    public DateTime ResultConfirmationDueDate { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }
    /// <summary>
    /// 周期说明
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 评审周期表查询DTO
/// </summary>
public partial class TaktReviewCycleQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReviewCycleQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 周期编码
    /// </summary>
    public string? CycleCode { get; set; }
    /// <summary>
    /// 周期名称
    /// </summary>
    public string? CycleName { get; set; }
    /// <summary>
    /// 周期类型
    /// </summary>
    public string? CycleType { get; set; }
    /// <summary>
    /// 周期年度
    /// </summary>
    public int? CycleYear { get; set; }
    /// <summary>
    /// 周期序号
    /// </summary>
    public int? CycleSequence { get; set; }
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
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 结束日期开始时间
    /// </summary>
    public DateTime? EndDateStart { get; set; }
    /// <summary>
    /// 结束日期结束时间
    /// </summary>
    public DateTime? EndDateEnd { get; set; }
    /// <summary>
    /// 目标设定开始日期
    /// </summary>
    public DateTime? GoalSettingStartDate { get; set; }

    /// <summary>
    /// 目标设定开始日期开始时间
    /// </summary>
    public DateTime? GoalSettingStartDateStart { get; set; }
    /// <summary>
    /// 目标设定开始日期结束时间
    /// </summary>
    public DateTime? GoalSettingStartDateEnd { get; set; }
    /// <summary>
    /// 目标设定截止日期
    /// </summary>
    public DateTime? GoalSettingDueDate { get; set; }

    /// <summary>
    /// 目标设定截止日期开始时间
    /// </summary>
    public DateTime? GoalSettingDueDateStart { get; set; }
    /// <summary>
    /// 目标设定截止日期结束时间
    /// </summary>
    public DateTime? GoalSettingDueDateEnd { get; set; }
    /// <summary>
    /// 自评开始日期
    /// </summary>
    public DateTime? SelfEvaluationStartDate { get; set; }

    /// <summary>
    /// 自评开始日期开始时间
    /// </summary>
    public DateTime? SelfEvaluationStartDateStart { get; set; }
    /// <summary>
    /// 自评开始日期结束时间
    /// </summary>
    public DateTime? SelfEvaluationStartDateEnd { get; set; }
    /// <summary>
    /// 自评截止日期
    /// </summary>
    public DateTime? SelfEvaluationDueDate { get; set; }

    /// <summary>
    /// 自评截止日期开始时间
    /// </summary>
    public DateTime? SelfEvaluationDueDateStart { get; set; }
    /// <summary>
    /// 自评截止日期结束时间
    /// </summary>
    public DateTime? SelfEvaluationDueDateEnd { get; set; }
    /// <summary>
    /// 主管评审开始日期
    /// </summary>
    public DateTime? SupervisorReviewStartDate { get; set; }

    /// <summary>
    /// 主管评审开始日期开始时间
    /// </summary>
    public DateTime? SupervisorReviewStartDateStart { get; set; }
    /// <summary>
    /// 主管评审开始日期结束时间
    /// </summary>
    public DateTime? SupervisorReviewStartDateEnd { get; set; }
    /// <summary>
    /// 主管评审截止日期
    /// </summary>
    public DateTime? SupervisorReviewDueDate { get; set; }

    /// <summary>
    /// 主管评审截止日期开始时间
    /// </summary>
    public DateTime? SupervisorReviewDueDateStart { get; set; }
    /// <summary>
    /// 主管评审截止日期结束时间
    /// </summary>
    public DateTime? SupervisorReviewDueDateEnd { get; set; }
    /// <summary>
    /// 面谈截止日期
    /// </summary>
    public DateTime? InterviewDueDate { get; set; }

    /// <summary>
    /// 面谈截止日期开始时间
    /// </summary>
    public DateTime? InterviewDueDateStart { get; set; }
    /// <summary>
    /// 面谈截止日期结束时间
    /// </summary>
    public DateTime? InterviewDueDateEnd { get; set; }
    /// <summary>
    /// 结果确认截止日期
    /// </summary>
    public DateTime? ResultConfirmationDueDate { get; set; }

    /// <summary>
    /// 结果确认截止日期开始时间
    /// </summary>
    public DateTime? ResultConfirmationDueDateStart { get; set; }
    /// <summary>
    /// 结果确认截止日期结束时间
    /// </summary>
    public DateTime? ResultConfirmationDueDateEnd { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string? ApplicableDepartment { get; set; }
    /// <summary>
    /// 周期说明
    /// </summary>
    public string? Description { get; set; }
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
/// Takt创建评审周期表DTO
/// </summary>
public partial class TaktReviewCycleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReviewCycleCreateDto()
    {
        CycleCode = string.Empty;
        CycleName = string.Empty;
        CycleType = string.Empty;
        ApplicableDepartment = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 周期编码
    /// </summary>
    public string CycleCode { get; set; }

        /// <summary>
    /// 周期名称
    /// </summary>
    public string CycleName { get; set; }

        /// <summary>
    /// 周期类型
    /// </summary>
    public string CycleType { get; set; }

        /// <summary>
    /// 周期年度
    /// </summary>
    public int CycleYear { get; set; }

        /// <summary>
    /// 周期序号
    /// </summary>
    public int CycleSequence { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 目标设定开始日期
    /// </summary>
    public DateTime GoalSettingStartDate { get; set; }

        /// <summary>
    /// 目标设定截止日期
    /// </summary>
    public DateTime GoalSettingDueDate { get; set; }

        /// <summary>
    /// 自评开始日期
    /// </summary>
    public DateTime SelfEvaluationStartDate { get; set; }

        /// <summary>
    /// 自评截止日期
    /// </summary>
    public DateTime SelfEvaluationDueDate { get; set; }

        /// <summary>
    /// 主管评审开始日期
    /// </summary>
    public DateTime SupervisorReviewStartDate { get; set; }

        /// <summary>
    /// 主管评审截止日期
    /// </summary>
    public DateTime SupervisorReviewDueDate { get; set; }

        /// <summary>
    /// 面谈截止日期
    /// </summary>
    public DateTime InterviewDueDate { get; set; }

        /// <summary>
    /// 结果确认截止日期
    /// </summary>
    public DateTime ResultConfirmationDueDate { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 周期说明
    /// </summary>
    public string Description { get; set; }

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
/// Takt更新评审周期表DTO
/// </summary>
public partial class TaktReviewCycleUpdateDto : TaktReviewCycleCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReviewCycleUpdateDto()
    {
    }

        /// <summary>
    /// 评审周期表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReviewCycleId { get; set; } = 0;
}

/// <summary>
/// 评审周期表状态DTO
/// </summary>
public partial class TaktReviewCycleStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReviewCycleStatusDto()
    {
    }

        /// <summary>
    /// 评审周期表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ReviewCycleId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 评审周期表导入模板DTO
/// </summary>
public partial class TaktReviewCycleTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReviewCycleTemplateDto()
    {
        CycleCode = string.Empty;
        CycleName = string.Empty;
        CycleType = string.Empty;
        ApplicableDepartment = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 周期编码
    /// </summary>
    public string CycleCode { get; set; }

        /// <summary>
    /// 周期名称
    /// </summary>
    public string CycleName { get; set; }

        /// <summary>
    /// 周期类型
    /// </summary>
    public string CycleType { get; set; }

        /// <summary>
    /// 周期年度
    /// </summary>
    public int CycleYear { get; set; }

        /// <summary>
    /// 周期序号
    /// </summary>
    public int CycleSequence { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 目标设定开始日期
    /// </summary>
    public DateTime GoalSettingStartDate { get; set; }

        /// <summary>
    /// 目标设定截止日期
    /// </summary>
    public DateTime GoalSettingDueDate { get; set; }

        /// <summary>
    /// 自评开始日期
    /// </summary>
    public DateTime SelfEvaluationStartDate { get; set; }

        /// <summary>
    /// 自评截止日期
    /// </summary>
    public DateTime SelfEvaluationDueDate { get; set; }

        /// <summary>
    /// 主管评审开始日期
    /// </summary>
    public DateTime SupervisorReviewStartDate { get; set; }

        /// <summary>
    /// 主管评审截止日期
    /// </summary>
    public DateTime SupervisorReviewDueDate { get; set; }

        /// <summary>
    /// 面谈截止日期
    /// </summary>
    public DateTime InterviewDueDate { get; set; }

        /// <summary>
    /// 结果确认截止日期
    /// </summary>
    public DateTime ResultConfirmationDueDate { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 周期说明
    /// </summary>
    public string Description { get; set; }

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
/// 评审周期表导入DTO
/// </summary>
public partial class TaktReviewCycleImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReviewCycleImportDto()
    {
        CycleCode = string.Empty;
        CycleName = string.Empty;
        CycleType = string.Empty;
        ApplicableDepartment = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 周期编码
    /// </summary>
    public string CycleCode { get; set; }

        /// <summary>
    /// 周期名称
    /// </summary>
    public string CycleName { get; set; }

        /// <summary>
    /// 周期类型
    /// </summary>
    public string CycleType { get; set; }

        /// <summary>
    /// 周期年度
    /// </summary>
    public int CycleYear { get; set; }

        /// <summary>
    /// 周期序号
    /// </summary>
    public int CycleSequence { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 目标设定开始日期
    /// </summary>
    public DateTime GoalSettingStartDate { get; set; }

        /// <summary>
    /// 目标设定截止日期
    /// </summary>
    public DateTime GoalSettingDueDate { get; set; }

        /// <summary>
    /// 自评开始日期
    /// </summary>
    public DateTime SelfEvaluationStartDate { get; set; }

        /// <summary>
    /// 自评截止日期
    /// </summary>
    public DateTime SelfEvaluationDueDate { get; set; }

        /// <summary>
    /// 主管评审开始日期
    /// </summary>
    public DateTime SupervisorReviewStartDate { get; set; }

        /// <summary>
    /// 主管评审截止日期
    /// </summary>
    public DateTime SupervisorReviewDueDate { get; set; }

        /// <summary>
    /// 面谈截止日期
    /// </summary>
    public DateTime InterviewDueDate { get; set; }

        /// <summary>
    /// 结果确认截止日期
    /// </summary>
    public DateTime ResultConfirmationDueDate { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 周期说明
    /// </summary>
    public string Description { get; set; }

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
/// 评审周期表导出DTO
/// </summary>
public partial class TaktReviewCycleExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktReviewCycleExportDto()
    {
        CreatedAt = DateTime.Now;
        CycleCode = string.Empty;
        CycleName = string.Empty;
        CycleType = string.Empty;
        ApplicableDepartment = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 周期编码
    /// </summary>
    public string CycleCode { get; set; }

        /// <summary>
    /// 周期名称
    /// </summary>
    public string CycleName { get; set; }

        /// <summary>
    /// 周期类型
    /// </summary>
    public string CycleType { get; set; }

        /// <summary>
    /// 周期年度
    /// </summary>
    public int CycleYear { get; set; }

        /// <summary>
    /// 周期序号
    /// </summary>
    public int CycleSequence { get; set; }

        /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 目标设定开始日期
    /// </summary>
    public DateTime GoalSettingStartDate { get; set; }

        /// <summary>
    /// 目标设定截止日期
    /// </summary>
    public DateTime GoalSettingDueDate { get; set; }

        /// <summary>
    /// 自评开始日期
    /// </summary>
    public DateTime SelfEvaluationStartDate { get; set; }

        /// <summary>
    /// 自评截止日期
    /// </summary>
    public DateTime SelfEvaluationDueDate { get; set; }

        /// <summary>
    /// 主管评审开始日期
    /// </summary>
    public DateTime SupervisorReviewStartDate { get; set; }

        /// <summary>
    /// 主管评审截止日期
    /// </summary>
    public DateTime SupervisorReviewDueDate { get; set; }

        /// <summary>
    /// 面谈截止日期
    /// </summary>
    public DateTime InterviewDueDate { get; set; }

        /// <summary>
    /// 结果确认截止日期
    /// </summary>
    public DateTime ResultConfirmationDueDate { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 周期说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}