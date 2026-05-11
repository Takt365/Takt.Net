// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.CompensationBenefits
// 文件名称：TaktBenefitPlanDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：福利方案表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.CompensationBenefits;

/// <summary>
/// 福利方案表Dto
/// </summary>
public partial class TaktBenefitPlanDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBenefitPlanDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        BenefitType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        DistributionMethod = string.Empty;
        BenefitConditions = string.Empty;
        Description = string.Empty;
    }

    /// <summary>
    /// 福利方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BenefitPlanId { get; set; } = 0;

    /// <summary>
    /// 方案编码
    /// </summary>
    public string PlanCode { get; set; }
    /// <summary>
    /// 方案名称
    /// </summary>
    public string PlanName { get; set; }
    /// <summary>
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }
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
    /// 福利标准金额
    /// </summary>
    public decimal BenefitStandardAmount { get; set; }
    /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }
    /// <summary>
    /// 福利条件说明
    /// </summary>
    public string BenefitConditions { get; set; }
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
/// 福利方案表查询DTO
/// </summary>
public partial class TaktBenefitPlanQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBenefitPlanQueryDto()
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
    /// 福利类型
    /// </summary>
    public string? BenefitType { get; set; }
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
    /// 福利标准金额
    /// </summary>
    public decimal? BenefitStandardAmount { get; set; }
    /// <summary>
    /// 发放方式
    /// </summary>
    public string? DistributionMethod { get; set; }
    /// <summary>
    /// 福利条件说明
    /// </summary>
    public string? BenefitConditions { get; set; }
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
/// Takt创建福利方案表DTO
/// </summary>
public partial class TaktBenefitPlanCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBenefitPlanCreateDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        BenefitType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        DistributionMethod = string.Empty;
        BenefitConditions = string.Empty;
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
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }

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
    /// 福利标准金额
    /// </summary>
    public decimal BenefitStandardAmount { get; set; }

        /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }

        /// <summary>
    /// 福利条件说明
    /// </summary>
    public string BenefitConditions { get; set; }

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
/// Takt更新福利方案表DTO
/// </summary>
public partial class TaktBenefitPlanUpdateDto : TaktBenefitPlanCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBenefitPlanUpdateDto()
    {
    }

        /// <summary>
    /// 福利方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BenefitPlanId { get; set; } = 0;
}

/// <summary>
/// 福利方案表状态DTO
/// </summary>
public partial class TaktBenefitPlanStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBenefitPlanStatusDto()
    {
    }

        /// <summary>
    /// 福利方案表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long BenefitPlanId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 福利方案表导入模板DTO
/// </summary>
public partial class TaktBenefitPlanTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBenefitPlanTemplateDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        BenefitType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        DistributionMethod = string.Empty;
        BenefitConditions = string.Empty;
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
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }

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
    /// 福利标准金额
    /// </summary>
    public decimal BenefitStandardAmount { get; set; }

        /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }

        /// <summary>
    /// 福利条件说明
    /// </summary>
    public string BenefitConditions { get; set; }

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
/// 福利方案表导入DTO
/// </summary>
public partial class TaktBenefitPlanImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBenefitPlanImportDto()
    {
        PlanCode = string.Empty;
        PlanName = string.Empty;
        BenefitType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        DistributionMethod = string.Empty;
        BenefitConditions = string.Empty;
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
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }

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
    /// 福利标准金额
    /// </summary>
    public decimal BenefitStandardAmount { get; set; }

        /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }

        /// <summary>
    /// 福利条件说明
    /// </summary>
    public string BenefitConditions { get; set; }

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
/// 福利方案表导出DTO
/// </summary>
public partial class TaktBenefitPlanExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBenefitPlanExportDto()
    {
        CreatedAt = DateTime.Now;
        PlanCode = string.Empty;
        PlanName = string.Empty;
        BenefitType = string.Empty;
        ApplicableDepartment = string.Empty;
        ApplicablePosition = string.Empty;
        ApplicableLevel = string.Empty;
        DistributionMethod = string.Empty;
        BenefitConditions = string.Empty;
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
    /// 福利类型
    /// </summary>
    public string BenefitType { get; set; }

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
    /// 福利标准金额
    /// </summary>
    public decimal BenefitStandardAmount { get; set; }

        /// <summary>
    /// 发放方式
    /// </summary>
    public string DistributionMethod { get; set; }

        /// <summary>
    /// 福利条件说明
    /// </summary>
    public string BenefitConditions { get; set; }

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