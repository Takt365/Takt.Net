// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.Talent
// 文件名称：TaktTalentDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：人才管理表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.Talent;

/// <summary>
/// 人才管理表Dto
/// </summary>
public partial class TaktTalentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTalentDto()
    {
        TalentLevel = string.Empty;
        ProfessionalSkills = string.Empty;
        CoreCompetency = string.Empty;
        DevelopmentPotential = string.Empty;
        CareerPlan = string.Empty;
        TalentTags = string.Empty;
    }

    /// <summary>
    /// 人才管理表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TalentId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 人才等级
    /// </summary>
    public string TalentLevel { get; set; }
    /// <summary>
    /// 专业技能
    /// </summary>
    public string ProfessionalSkills { get; set; }
    /// <summary>
    /// 核心能力评估
    /// </summary>
    public string CoreCompetency { get; set; }
    /// <summary>
    /// 领导力评分
    /// </summary>
    public decimal LeadershipScore { get; set; }
    /// <summary>
    /// 创新能力评分
    /// </summary>
    public decimal InnovationScore { get; set; }
    /// <summary>
    /// 协作能力评分
    /// </summary>
    public decimal CollaborationScore { get; set; }
    /// <summary>
    /// 发展潜力
    /// </summary>
    public string DevelopmentPotential { get; set; }
    /// <summary>
    /// 职业发展规划
    /// </summary>
    public string CareerPlan { get; set; }
    /// <summary>
    /// 人才标签
    /// </summary>
    public string TalentTags { get; set; }
    /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }
    /// <summary>
    /// 评估人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EvaluatorId { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 人才管理表查询DTO
/// </summary>
public partial class TaktTalentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTalentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 人才等级
    /// </summary>
    public string? TalentLevel { get; set; }
    /// <summary>
    /// 专业技能
    /// </summary>
    public string? ProfessionalSkills { get; set; }
    /// <summary>
    /// 核心能力评估
    /// </summary>
    public string? CoreCompetency { get; set; }
    /// <summary>
    /// 领导力评分
    /// </summary>
    public decimal? LeadershipScore { get; set; }
    /// <summary>
    /// 创新能力评分
    /// </summary>
    public decimal? InnovationScore { get; set; }
    /// <summary>
    /// 协作能力评分
    /// </summary>
    public decimal? CollaborationScore { get; set; }
    /// <summary>
    /// 发展潜力
    /// </summary>
    public string? DevelopmentPotential { get; set; }
    /// <summary>
    /// 职业发展规划
    /// </summary>
    public string? CareerPlan { get; set; }
    /// <summary>
    /// 人才标签
    /// </summary>
    public string? TalentTags { get; set; }
    /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime? EvaluationDate { get; set; }

    /// <summary>
    /// 评估日期开始时间
    /// </summary>
    public DateTime? EvaluationDateStart { get; set; }
    /// <summary>
    /// 评估日期结束时间
    /// </summary>
    public DateTime? EvaluationDateEnd { get; set; }
    /// <summary>
    /// 评估人ID
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
/// Takt创建人才管理表DTO
/// </summary>
public partial class TaktTalentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTalentCreateDto()
    {
        TalentLevel = string.Empty;
        ProfessionalSkills = string.Empty;
        CoreCompetency = string.Empty;
        DevelopmentPotential = string.Empty;
        CareerPlan = string.Empty;
        TalentTags = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 人才等级
    /// </summary>
    public string TalentLevel { get; set; }

        /// <summary>
    /// 专业技能
    /// </summary>
    public string ProfessionalSkills { get; set; }

        /// <summary>
    /// 核心能力评估
    /// </summary>
    public string CoreCompetency { get; set; }

        /// <summary>
    /// 领导力评分
    /// </summary>
    public decimal LeadershipScore { get; set; }

        /// <summary>
    /// 创新能力评分
    /// </summary>
    public decimal InnovationScore { get; set; }

        /// <summary>
    /// 协作能力评分
    /// </summary>
    public decimal CollaborationScore { get; set; }

        /// <summary>
    /// 发展潜力
    /// </summary>
    public string DevelopmentPotential { get; set; }

        /// <summary>
    /// 职业发展规划
    /// </summary>
    public string CareerPlan { get; set; }

        /// <summary>
    /// 人才标签
    /// </summary>
    public string TalentTags { get; set; }

        /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 评估人ID
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
/// Takt更新人才管理表DTO
/// </summary>
public partial class TaktTalentUpdateDto : TaktTalentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTalentUpdateDto()
    {
    }

        /// <summary>
    /// 人才管理表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TalentId { get; set; } = 0;
}

/// <summary>
/// 人才管理表状态DTO
/// </summary>
public partial class TaktTalentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTalentStatusDto()
    {
    }

        /// <summary>
    /// 人才管理表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TalentId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 人才管理表导入模板DTO
/// </summary>
public partial class TaktTalentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTalentTemplateDto()
    {
        TalentLevel = string.Empty;
        ProfessionalSkills = string.Empty;
        CoreCompetency = string.Empty;
        DevelopmentPotential = string.Empty;
        CareerPlan = string.Empty;
        TalentTags = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 人才等级
    /// </summary>
    public string TalentLevel { get; set; }

        /// <summary>
    /// 专业技能
    /// </summary>
    public string ProfessionalSkills { get; set; }

        /// <summary>
    /// 核心能力评估
    /// </summary>
    public string CoreCompetency { get; set; }

        /// <summary>
    /// 领导力评分
    /// </summary>
    public decimal LeadershipScore { get; set; }

        /// <summary>
    /// 创新能力评分
    /// </summary>
    public decimal InnovationScore { get; set; }

        /// <summary>
    /// 协作能力评分
    /// </summary>
    public decimal CollaborationScore { get; set; }

        /// <summary>
    /// 发展潜力
    /// </summary>
    public string DevelopmentPotential { get; set; }

        /// <summary>
    /// 职业发展规划
    /// </summary>
    public string CareerPlan { get; set; }

        /// <summary>
    /// 人才标签
    /// </summary>
    public string TalentTags { get; set; }

        /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 评估人ID
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
/// 人才管理表导入DTO
/// </summary>
public partial class TaktTalentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTalentImportDto()
    {
        TalentLevel = string.Empty;
        ProfessionalSkills = string.Empty;
        CoreCompetency = string.Empty;
        DevelopmentPotential = string.Empty;
        CareerPlan = string.Empty;
        TalentTags = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 人才等级
    /// </summary>
    public string TalentLevel { get; set; }

        /// <summary>
    /// 专业技能
    /// </summary>
    public string ProfessionalSkills { get; set; }

        /// <summary>
    /// 核心能力评估
    /// </summary>
    public string CoreCompetency { get; set; }

        /// <summary>
    /// 领导力评分
    /// </summary>
    public decimal LeadershipScore { get; set; }

        /// <summary>
    /// 创新能力评分
    /// </summary>
    public decimal InnovationScore { get; set; }

        /// <summary>
    /// 协作能力评分
    /// </summary>
    public decimal CollaborationScore { get; set; }

        /// <summary>
    /// 发展潜力
    /// </summary>
    public string DevelopmentPotential { get; set; }

        /// <summary>
    /// 职业发展规划
    /// </summary>
    public string CareerPlan { get; set; }

        /// <summary>
    /// 人才标签
    /// </summary>
    public string TalentTags { get; set; }

        /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 评估人ID
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
/// 人才管理表导出DTO
/// </summary>
public partial class TaktTalentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTalentExportDto()
    {
        CreatedAt = DateTime.Now;
        TalentLevel = string.Empty;
        ProfessionalSkills = string.Empty;
        CoreCompetency = string.Empty;
        DevelopmentPotential = string.Empty;
        CareerPlan = string.Empty;
        TalentTags = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 人才等级
    /// </summary>
    public string TalentLevel { get; set; }

        /// <summary>
    /// 专业技能
    /// </summary>
    public string ProfessionalSkills { get; set; }

        /// <summary>
    /// 核心能力评估
    /// </summary>
    public string CoreCompetency { get; set; }

        /// <summary>
    /// 领导力评分
    /// </summary>
    public decimal LeadershipScore { get; set; }

        /// <summary>
    /// 创新能力评分
    /// </summary>
    public decimal InnovationScore { get; set; }

        /// <summary>
    /// 协作能力评分
    /// </summary>
    public decimal CollaborationScore { get; set; }

        /// <summary>
    /// 发展潜力
    /// </summary>
    public string DevelopmentPotential { get; set; }

        /// <summary>
    /// 职业发展规划
    /// </summary>
    public string CareerPlan { get; set; }

        /// <summary>
    /// 人才标签
    /// </summary>
    public string TalentTags { get; set; }

        /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime EvaluationDate { get; set; }

        /// <summary>
    /// 评估人ID
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