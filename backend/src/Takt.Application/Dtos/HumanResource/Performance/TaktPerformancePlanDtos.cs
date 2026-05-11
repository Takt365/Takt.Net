// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Performance
// 文件名称：TaktPerformancePlanDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：绩效方案表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Performance;

/// <summary>
/// 绩效方案表Dto
/// </summary>
public partial class TaktPerformancePlanDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancePlanDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        CycleType = string.Empty;
        ScoringStandard = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// 绩效方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformancePlanId { get; set; } = 0;

    /// <summary>
    /// 方案编码
    /// </summary>
    public string PlanCode { get; set; }
    /// <summary>
    /// 方案名称
    /// </summary>
    public string PlanName { get; set; }
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
    /// 考核周期类型
    /// </summary>
    public string CycleType { get; set; }
    /// <summary>
    /// 评分标准
    /// </summary>
    public string ScoringStandard { get; set; }
    /// <summary>
    /// 自评权重
    /// </summary>
    public decimal SelfEvaluationWeight { get; set; }
    /// <summary>
    /// 主管评分权重
    /// </summary>
    public decimal SupervisorWeight { get; set; }
    /// <summary>
    /// 同事评分权重
    /// </summary>
    public decimal PeerWeight { get; set; }
    /// <summary>
    /// 方案说明
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 绩效方案表查询DTO
/// </summary>
public partial class TaktPerformancePlanQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancePlanQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 方案编码
    /// </summary>
    public string? PlanCode { get; set; }
    /// <summary>
    /// 方案名称
    /// </summary>
    public string? PlanName { get; set; }
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
    /// 考核周期类型
    /// </summary>
    public string? CycleType { get; set; }
    /// <summary>
    /// 评分标准
    /// </summary>
    public string? ScoringStandard { get; set; }
    /// <summary>
    /// 自评权重
    /// </summary>
    public decimal? SelfEvaluationWeight { get; set; }
    /// <summary>
    /// 主管评分权重
    /// </summary>
    public decimal? SupervisorWeight { get; set; }
    /// <summary>
    /// 同事评分权重
    /// </summary>
    public decimal? PeerWeight { get; set; }
    /// <summary>
    /// 方案说明
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 生效日期开始时间
    /// </summary>
    public DateTime? EffectiveDateStart { get; set; }
    /// <summary>
    /// 生效日期结束时间
    /// </summary>
    public DateTime? EffectiveDateEnd { get; set; }
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
/// Takt创建绩效方案表DTO
/// </summary>
public partial class TaktPerformancePlanCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancePlanCreateDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        CycleType = string.Empty;
        ScoringStandard = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 方案编码
    /// </summary>
    public string PlanCode { get; set; }

        /// <summary>
    /// 方案名称
    /// </summary>
    public string PlanName { get; set; }

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
    /// 考核周期类型
    /// </summary>
    public string CycleType { get; set; }

        /// <summary>
    /// 评分标准
    /// </summary>
    public string ScoringStandard { get; set; }

        /// <summary>
    /// 自评权重
    /// </summary>
    public decimal SelfEvaluationWeight { get; set; }

        /// <summary>
    /// 主管评分权重
    /// </summary>
    public decimal SupervisorWeight { get; set; }

        /// <summary>
    /// 同事评分权重
    /// </summary>
    public decimal PeerWeight { get; set; }

        /// <summary>
    /// 方案说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

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
/// Takt更新绩效方案表DTO
/// </summary>
public partial class TaktPerformancePlanUpdateDto : TaktPerformancePlanCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancePlanUpdateDto()
    {
    }

        /// <summary>
    /// 绩效方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformancePlanId { get; set; } = 0;
}

/// <summary>
/// 绩效方案表状态DTO
/// </summary>
public partial class TaktPerformancePlanStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancePlanStatusDto()
    {
    }

        /// <summary>
    /// 绩效方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long PerformancePlanId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 绩效方案表导入模板DTO
/// </summary>
public partial class TaktPerformancePlanTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancePlanTemplateDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        CycleType = string.Empty;
        ScoringStandard = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 方案编码
    /// </summary>
    public string PlanCode { get; set; }

        /// <summary>
    /// 方案名称
    /// </summary>
    public string PlanName { get; set; }

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
    /// 考核周期类型
    /// </summary>
    public string CycleType { get; set; }

        /// <summary>
    /// 评分标准
    /// </summary>
    public string ScoringStandard { get; set; }

        /// <summary>
    /// 自评权重
    /// </summary>
    public decimal SelfEvaluationWeight { get; set; }

        /// <summary>
    /// 主管评分权重
    /// </summary>
    public decimal SupervisorWeight { get; set; }

        /// <summary>
    /// 同事评分权重
    /// </summary>
    public decimal PeerWeight { get; set; }

        /// <summary>
    /// 方案说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

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
/// 绩效方案表导入DTO
/// </summary>
public partial class TaktPerformancePlanImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancePlanImportDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        CycleType = string.Empty;
        ScoringStandard = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 方案编码
    /// </summary>
    public string PlanCode { get; set; }

        /// <summary>
    /// 方案名称
    /// </summary>
    public string PlanName { get; set; }

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
    /// 考核周期类型
    /// </summary>
    public string CycleType { get; set; }

        /// <summary>
    /// 评分标准
    /// </summary>
    public string ScoringStandard { get; set; }

        /// <summary>
    /// 自评权重
    /// </summary>
    public decimal SelfEvaluationWeight { get; set; }

        /// <summary>
    /// 主管评分权重
    /// </summary>
    public decimal SupervisorWeight { get; set; }

        /// <summary>
    /// 同事评分权重
    /// </summary>
    public decimal PeerWeight { get; set; }

        /// <summary>
    /// 方案说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

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
/// 绩效方案表导出DTO
/// </summary>
public partial class TaktPerformancePlanExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancePlanExportDto()
    {
        CreatedAt = DateTime.Now;
        PlanCode = string.Empty;
        PlanName = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        CycleType = string.Empty;
        ScoringStandard = string.Empty;
        Description = string.Empty;
    }

        /// <summary>
    /// 方案编码
    /// </summary>
    public string PlanCode { get; set; }

        /// <summary>
    /// 方案名称
    /// </summary>
    public string PlanName { get; set; }

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
    /// 考核周期类型
    /// </summary>
    public string CycleType { get; set; }

        /// <summary>
    /// 评分标准
    /// </summary>
    public string ScoringStandard { get; set; }

        /// <summary>
    /// 自评权重
    /// </summary>
    public decimal SelfEvaluationWeight { get; set; }

        /// <summary>
    /// 主管评分权重
    /// </summary>
    public decimal SupervisorWeight { get; set; }

        /// <summary>
    /// 同事评分权重
    /// </summary>
    public decimal PeerWeight { get; set; }

        /// <summary>
    /// 方案说明
    /// </summary>
    public string Description { get; set; }

        /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime EffectiveDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}