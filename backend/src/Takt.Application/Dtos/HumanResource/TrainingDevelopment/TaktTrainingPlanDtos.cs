// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingPlanDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：培训计划表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训计划表Dto
/// </summary>
public partial class TaktTrainingPlanDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingPlanDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        PlanType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        TrainingObjectives = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// 培训计划表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingPlanId { get; set; } = 0;

    /// <summary>
    /// 计划编码
    /// </summary>
    public string PlanCode { get; set; }
    /// <summary>
    /// 计划名称
    /// </summary>
    public string PlanName { get; set; }
    /// <summary>
    /// 计划年度
    /// </summary>
    public int PlanYear { get; set; }
    /// <summary>
    /// 计划类型
    /// </summary>
    public string PlanType { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }
    /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }
    /// <summary>
    /// 适用职级
    /// </summary>
    public string ApplicableLevel { get; set; }
    /// <summary>
    /// 计划开始日期
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// 计划结束日期
    /// </summary>
    public DateTime EndDate { get; set; }
    /// <summary>
    /// 培训目标
    /// </summary>
    public string TrainingObjectives { get; set; }
    /// <summary>
    /// 计划培训人数
    /// </summary>
    public int PlannedHeadcount { get; set; }
    /// <summary>
    /// 实际培训人数
    /// </summary>
    public int ActualHeadcount { get; set; }
    /// <summary>
    /// 计划总课时
    /// </summary>
    public decimal PlannedTotalHours { get; set; }
    /// <summary>
    /// 实际总课时
    /// </summary>
    public decimal ActualTotalHours { get; set; }
    /// <summary>
    /// 培训预算
    /// </summary>
    public decimal TrainingBudget { get; set; }
    /// <summary>
    /// 实际花费
    /// </summary>
    public decimal ActualCost { get; set; }
    /// <summary>
    /// 计划说明
    /// </summary>
    public string Description { get; set; }
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
/// 培训计划表查询DTO
/// </summary>
public partial class TaktTrainingPlanQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingPlanQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 计划编码
    /// </summary>
    public string? PlanCode { get; set; }
    /// <summary>
    /// 计划名称
    /// </summary>
    public string? PlanName { get; set; }
    /// <summary>
    /// 计划年度
    /// </summary>
    public int? PlanYear { get; set; }
    /// <summary>
    /// 计划类型
    /// </summary>
    public string? PlanType { get; set; }
    /// <summary>
    /// 适用部门
    /// </summary>
    public string? ApplicableDepartment { get; set; }
    /// <summary>
    /// 适用岗位
    /// </summary>
    public string? ApplicablePosition { get; set; }
    /// <summary>
    /// 适用职级
    /// </summary>
    public string? ApplicableLevel { get; set; }
    /// <summary>
    /// 计划开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 计划开始日期开始时间
    /// </summary>
    public DateTime? StartDateStart { get; set; }
    /// <summary>
    /// 计划开始日期结束时间
    /// </summary>
    public DateTime? StartDateEnd { get; set; }
    /// <summary>
    /// 计划结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 计划结束日期开始时间
    /// </summary>
    public DateTime? EndDateStart { get; set; }
    /// <summary>
    /// 计划结束日期结束时间
    /// </summary>
    public DateTime? EndDateEnd { get; set; }
    /// <summary>
    /// 培训目标
    /// </summary>
    public string? TrainingObjectives { get; set; }
    /// <summary>
    /// 计划培训人数
    /// </summary>
    public int? PlannedHeadcount { get; set; }
    /// <summary>
    /// 实际培训人数
    /// </summary>
    public int? ActualHeadcount { get; set; }
    /// <summary>
    /// 计划总课时
    /// </summary>
    public decimal? PlannedTotalHours { get; set; }
    /// <summary>
    /// 实际总课时
    /// </summary>
    public decimal? ActualTotalHours { get; set; }
    /// <summary>
    /// 培训预算
    /// </summary>
    public decimal? TrainingBudget { get; set; }
    /// <summary>
    /// 实际花费
    /// </summary>
    public decimal? ActualCost { get; set; }
    /// <summary>
    /// 计划说明
    /// </summary>
    public string? Description { get; set; }
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
/// Takt创建培训计划表DTO
/// </summary>
public partial class TaktTrainingPlanCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingPlanCreateDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        PlanType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        TrainingObjectives = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 计划编码
    /// </summary>
    public string PlanCode { get; set; }

        /// <summary>
    /// 计划名称
    /// </summary>
    public string PlanName { get; set; }

        /// <summary>
    /// 计划年度
    /// </summary>
    public int PlanYear { get; set; }

        /// <summary>
    /// 计划类型
    /// </summary>
    public string PlanType { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 适用职级
    /// </summary>
    public string ApplicableLevel { get; set; }

        /// <summary>
    /// 计划开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 计划结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 培训目标
    /// </summary>
    public string TrainingObjectives { get; set; }

        /// <summary>
    /// 计划培训人数
    /// </summary>
    public int PlannedHeadcount { get; set; }

        /// <summary>
    /// 实际培训人数
    /// </summary>
    public int ActualHeadcount { get; set; }

        /// <summary>
    /// 计划总课时
    /// </summary>
    public decimal PlannedTotalHours { get; set; }

        /// <summary>
    /// 实际总课时
    /// </summary>
    public decimal ActualTotalHours { get; set; }

        /// <summary>
    /// 培训预算
    /// </summary>
    public decimal TrainingBudget { get; set; }

        /// <summary>
    /// 实际花费
    /// </summary>
    public decimal ActualCost { get; set; }

        /// <summary>
    /// 计划说明
    /// </summary>
    public string Description { get; set; }

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
/// Takt更新培训计划表DTO
/// </summary>
public partial class TaktTrainingPlanUpdateDto : TaktTrainingPlanCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingPlanUpdateDto()
    {
    }

        /// <summary>
    /// 培训计划表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingPlanId { get; set; } = 0;
}

/// <summary>
/// 培训计划表状态DTO
/// </summary>
public partial class TaktTrainingPlanStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingPlanStatusDto()
    {
    }

        /// <summary>
    /// 培训计划表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TrainingPlanId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 培训计划表导入模板DTO
/// </summary>
public partial class TaktTrainingPlanTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingPlanTemplateDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        PlanType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        TrainingObjectives = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 计划编码
    /// </summary>
    public string PlanCode { get; set; }

        /// <summary>
    /// 计划名称
    /// </summary>
    public string PlanName { get; set; }

        /// <summary>
    /// 计划年度
    /// </summary>
    public int PlanYear { get; set; }

        /// <summary>
    /// 计划类型
    /// </summary>
    public string PlanType { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 适用职级
    /// </summary>
    public string ApplicableLevel { get; set; }

        /// <summary>
    /// 计划开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 计划结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 培训目标
    /// </summary>
    public string TrainingObjectives { get; set; }

        /// <summary>
    /// 计划培训人数
    /// </summary>
    public int PlannedHeadcount { get; set; }

        /// <summary>
    /// 实际培训人数
    /// </summary>
    public int ActualHeadcount { get; set; }

        /// <summary>
    /// 计划总课时
    /// </summary>
    public decimal PlannedTotalHours { get; set; }

        /// <summary>
    /// 实际总课时
    /// </summary>
    public decimal ActualTotalHours { get; set; }

        /// <summary>
    /// 培训预算
    /// </summary>
    public decimal TrainingBudget { get; set; }

        /// <summary>
    /// 实际花费
    /// </summary>
    public decimal ActualCost { get; set; }

        /// <summary>
    /// 计划说明
    /// </summary>
    public string Description { get; set; }

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
/// 培训计划表导入DTO
/// </summary>
public partial class TaktTrainingPlanImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingPlanImportDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        PlanType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        TrainingObjectives = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 计划编码
    /// </summary>
    public string PlanCode { get; set; }

        /// <summary>
    /// 计划名称
    /// </summary>
    public string PlanName { get; set; }

        /// <summary>
    /// 计划年度
    /// </summary>
    public int PlanYear { get; set; }

        /// <summary>
    /// 计划类型
    /// </summary>
    public string PlanType { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 适用职级
    /// </summary>
    public string ApplicableLevel { get; set; }

        /// <summary>
    /// 计划开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 计划结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 培训目标
    /// </summary>
    public string TrainingObjectives { get; set; }

        /// <summary>
    /// 计划培训人数
    /// </summary>
    public int PlannedHeadcount { get; set; }

        /// <summary>
    /// 实际培训人数
    /// </summary>
    public int ActualHeadcount { get; set; }

        /// <summary>
    /// 计划总课时
    /// </summary>
    public decimal PlannedTotalHours { get; set; }

        /// <summary>
    /// 实际总课时
    /// </summary>
    public decimal ActualTotalHours { get; set; }

        /// <summary>
    /// 培训预算
    /// </summary>
    public decimal TrainingBudget { get; set; }

        /// <summary>
    /// 实际花费
    /// </summary>
    public decimal ActualCost { get; set; }

        /// <summary>
    /// 计划说明
    /// </summary>
    public string Description { get; set; }

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
/// 培训计划表导出DTO
/// </summary>
public partial class TaktTrainingPlanExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingPlanExportDto()
    {
        CreatedAt = DateTime.Now;
        PlanCode = string.Empty;
        PlanName = string.Empty;
        PlanType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        TrainingObjectives = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 计划编码
    /// </summary>
    public string PlanCode { get; set; }

        /// <summary>
    /// 计划名称
    /// </summary>
    public string PlanName { get; set; }

        /// <summary>
    /// 计划年度
    /// </summary>
    public int PlanYear { get; set; }

        /// <summary>
    /// 计划类型
    /// </summary>
    public string PlanType { get; set; }

        /// <summary>
    /// 适用部门
    /// </summary>
    public string ApplicableDepartment { get; set; }

        /// <summary>
    /// 适用岗位
    /// </summary>
    public string ApplicablePosition { get; set; }

        /// <summary>
    /// 适用职级
    /// </summary>
    public string ApplicableLevel { get; set; }

        /// <summary>
    /// 计划开始日期
    /// </summary>
    public DateTime StartDate { get; set; }

        /// <summary>
    /// 计划结束日期
    /// </summary>
    public DateTime EndDate { get; set; }

        /// <summary>
    /// 培训目标
    /// </summary>
    public string TrainingObjectives { get; set; }

        /// <summary>
    /// 计划培训人数
    /// </summary>
    public int PlannedHeadcount { get; set; }

        /// <summary>
    /// 实际培训人数
    /// </summary>
    public int ActualHeadcount { get; set; }

        /// <summary>
    /// 计划总课时
    /// </summary>
    public decimal PlannedTotalHours { get; set; }

        /// <summary>
    /// 实际总课时
    /// </summary>
    public decimal ActualTotalHours { get; set; }

        /// <summary>
    /// 培训预算
    /// </summary>
    public decimal TrainingBudget { get; set; }

        /// <summary>
    /// 实际花费
    /// </summary>
    public decimal ActualCost { get; set; }

        /// <summary>
    /// 计划说明
    /// </summary>
    public string Description { get; set; }

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