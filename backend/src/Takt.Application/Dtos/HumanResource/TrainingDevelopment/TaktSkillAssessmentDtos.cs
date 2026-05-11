// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.HumanResource.TrainingDevelopment
// 文件名称：TaktSkillAssessmentDtos.cs
// 创建时间：2026-05-11
// 创建人：Takt365
// 功能描述：技能评估表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Dtos.HumanResource.TrainingDevelopment;

/// <summary>
/// 技能评估表Dto
/// </summary>
public partial class TaktSkillAssessmentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSkillAssessmentDto()
    {
        SkillCategory = string.Empty;
        SkillName = string.Empty;
        SkillDescription = string.Empty;
        AssessmentMethod = string.Empty;
        SkillLevel = string.Empty;
        PreviousLevel = string.Empty;
        NewLevel = string.Empty;
        CertificateNo = string.Empty;
        AssessmentComments = string.Empty;
        StrengthsAnalysis = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

    /// <summary>
    /// 技能评估表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SkillAssessmentId { get; set; } = 0;

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }
    /// <summary>
    /// 技能类别
    /// </summary>
    public string SkillCategory { get; set; }
    /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }
    /// <summary>
    /// 技能描述
    /// </summary>
    public string SkillDescription { get; set; }
    /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime AssessmentDate { get; set; }
    /// <summary>
    /// 评估方式
    /// </summary>
    public string AssessmentMethod { get; set; }
    /// <summary>
    /// 评估得分
    /// </summary>
    public decimal AssessmentScore { get; set; }
    /// <summary>
    /// 技能等级
    /// </summary>
    public string SkillLevel { get; set; }
    /// <summary>
    /// 评估前等级
    /// </summary>
    public string PreviousLevel { get; set; }
    /// <summary>
    /// 评估后等级
    /// </summary>
    public string NewLevel { get; set; }
    /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }
    /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }
    /// <summary>
    /// 证书有效期
    /// </summary>
    public DateTime CertificateExpiryDate { get; set; }
    /// <summary>
    /// 评估人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssessorId { get; set; }
    /// <summary>
    /// 评估评语
    /// </summary>
    public string AssessmentComments { get; set; }
    /// <summary>
    /// 优势分析
    /// </summary>
    public string StrengthsAnalysis { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }
    /// <summary>
    /// 下次评估日期
    /// </summary>
    public DateTime NextAssessmentDate { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 技能评估表查询DTO
/// </summary>
public partial class TaktSkillAssessmentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSkillAssessmentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EmployeeId { get; set; }
    /// <summary>
    /// 技能类别
    /// </summary>
    public string? SkillCategory { get; set; }
    /// <summary>
    /// 技能名称
    /// </summary>
    public string? SkillName { get; set; }
    /// <summary>
    /// 技能描述
    /// </summary>
    public string? SkillDescription { get; set; }
    /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime? AssessmentDate { get; set; }

    /// <summary>
    /// 评估日期开始时间
    /// </summary>
    public DateTime? AssessmentDateStart { get; set; }
    /// <summary>
    /// 评估日期结束时间
    /// </summary>
    public DateTime? AssessmentDateEnd { get; set; }
    /// <summary>
    /// 评估方式
    /// </summary>
    public string? AssessmentMethod { get; set; }
    /// <summary>
    /// 评估得分
    /// </summary>
    public decimal? AssessmentScore { get; set; }
    /// <summary>
    /// 技能等级
    /// </summary>
    public string? SkillLevel { get; set; }
    /// <summary>
    /// 评估前等级
    /// </summary>
    public string? PreviousLevel { get; set; }
    /// <summary>
    /// 评估后等级
    /// </summary>
    public string? NewLevel { get; set; }
    /// <summary>
    /// 是否通过
    /// </summary>
    public int? IsPassed { get; set; }
    /// <summary>
    /// 证书编号
    /// </summary>
    public string? CertificateNo { get; set; }
    /// <summary>
    /// 证书有效期
    /// </summary>
    public DateTime? CertificateExpiryDate { get; set; }

    /// <summary>
    /// 证书有效期开始时间
    /// </summary>
    public DateTime? CertificateExpiryDateStart { get; set; }
    /// <summary>
    /// 证书有效期结束时间
    /// </summary>
    public DateTime? CertificateExpiryDateEnd { get; set; }
    /// <summary>
    /// 评估人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssessorId { get; set; }
    /// <summary>
    /// 评估评语
    /// </summary>
    public string? AssessmentComments { get; set; }
    /// <summary>
    /// 优势分析
    /// </summary>
    public string? StrengthsAnalysis { get; set; }
    /// <summary>
    /// 改进建议
    /// </summary>
    public string? ImprovementSuggestions { get; set; }
    /// <summary>
    /// 下次评估日期
    /// </summary>
    public DateTime? NextAssessmentDate { get; set; }

    /// <summary>
    /// 下次评估日期开始时间
    /// </summary>
    public DateTime? NextAssessmentDateStart { get; set; }
    /// <summary>
    /// 下次评估日期结束时间
    /// </summary>
    public DateTime? NextAssessmentDateEnd { get; set; }
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
/// Takt创建技能评估表DTO
/// </summary>
public partial class TaktSkillAssessmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSkillAssessmentCreateDto()
    {
        SkillCategory = string.Empty;
        SkillName = string.Empty;
        SkillDescription = string.Empty;
        AssessmentMethod = string.Empty;
        SkillLevel = string.Empty;
        PreviousLevel = string.Empty;
        NewLevel = string.Empty;
        CertificateNo = string.Empty;
        AssessmentComments = string.Empty;
        StrengthsAnalysis = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EmployeeId { get; set; }

        /// <summary>
    /// 技能类别
    /// </summary>
    public string SkillCategory { get; set; }

        /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

        /// <summary>
    /// 技能描述
    /// </summary>
    public string SkillDescription { get; set; }

        /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime AssessmentDate { get; set; }

        /// <summary>
    /// 评估方式
    /// </summary>
    public string AssessmentMethod { get; set; }

        /// <summary>
    /// 评估得分
    /// </summary>
    public decimal AssessmentScore { get; set; }

        /// <summary>
    /// 技能等级
    /// </summary>
    public string SkillLevel { get; set; }

        /// <summary>
    /// 评估前等级
    /// </summary>
    public string PreviousLevel { get; set; }

        /// <summary>
    /// 评估后等级
    /// </summary>
    public string NewLevel { get; set; }

        /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }

        /// <summary>
    /// 证书有效期
    /// </summary>
    public DateTime CertificateExpiryDate { get; set; }

        /// <summary>
    /// 评估人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AssessorId { get; set; }

        /// <summary>
    /// 评估评语
    /// </summary>
    public string AssessmentComments { get; set; }

        /// <summary>
    /// 优势分析
    /// </summary>
    public string StrengthsAnalysis { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 下次评估日期
    /// </summary>
    public DateTime NextAssessmentDate { get; set; }

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
/// Takt更新技能评估表DTO
/// </summary>
public partial class TaktSkillAssessmentUpdateDto : TaktSkillAssessmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSkillAssessmentUpdateDto()
    {
    }

        /// <summary>
    /// 技能评估表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SkillAssessmentId { get; set; } = 0;
}

/// <summary>
/// 技能评估表状态DTO
/// </summary>
public partial class TaktSkillAssessmentStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSkillAssessmentStatusDto()
    {
    }

        /// <summary>
    /// 技能评估表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SkillAssessmentId { get; set; } = 0;

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 技能评估表导入模板DTO
/// </summary>
public partial class TaktSkillAssessmentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSkillAssessmentTemplateDto()
    {
        SkillCategory = string.Empty;
        SkillName = string.Empty;
        SkillDescription = string.Empty;
        AssessmentMethod = string.Empty;
        SkillLevel = string.Empty;
        PreviousLevel = string.Empty;
        NewLevel = string.Empty;
        CertificateNo = string.Empty;
        AssessmentComments = string.Empty;
        StrengthsAnalysis = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 技能类别
    /// </summary>
    public string SkillCategory { get; set; }

        /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

        /// <summary>
    /// 技能描述
    /// </summary>
    public string SkillDescription { get; set; }

        /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime AssessmentDate { get; set; }

        /// <summary>
    /// 评估方式
    /// </summary>
    public string AssessmentMethod { get; set; }

        /// <summary>
    /// 评估得分
    /// </summary>
    public decimal AssessmentScore { get; set; }

        /// <summary>
    /// 技能等级
    /// </summary>
    public string SkillLevel { get; set; }

        /// <summary>
    /// 评估前等级
    /// </summary>
    public string PreviousLevel { get; set; }

        /// <summary>
    /// 评估后等级
    /// </summary>
    public string NewLevel { get; set; }

        /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }

        /// <summary>
    /// 证书有效期
    /// </summary>
    public DateTime CertificateExpiryDate { get; set; }

        /// <summary>
    /// 评估人ID
    /// </summary>
    public long AssessorId { get; set; }

        /// <summary>
    /// 评估评语
    /// </summary>
    public string AssessmentComments { get; set; }

        /// <summary>
    /// 优势分析
    /// </summary>
    public string StrengthsAnalysis { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 下次评估日期
    /// </summary>
    public DateTime NextAssessmentDate { get; set; }

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
/// 技能评估表导入DTO
/// </summary>
public partial class TaktSkillAssessmentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSkillAssessmentImportDto()
    {
        SkillCategory = string.Empty;
        SkillName = string.Empty;
        SkillDescription = string.Empty;
        AssessmentMethod = string.Empty;
        SkillLevel = string.Empty;
        PreviousLevel = string.Empty;
        NewLevel = string.Empty;
        CertificateNo = string.Empty;
        AssessmentComments = string.Empty;
        StrengthsAnalysis = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 技能类别
    /// </summary>
    public string SkillCategory { get; set; }

        /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

        /// <summary>
    /// 技能描述
    /// </summary>
    public string SkillDescription { get; set; }

        /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime AssessmentDate { get; set; }

        /// <summary>
    /// 评估方式
    /// </summary>
    public string AssessmentMethod { get; set; }

        /// <summary>
    /// 评估得分
    /// </summary>
    public decimal AssessmentScore { get; set; }

        /// <summary>
    /// 技能等级
    /// </summary>
    public string SkillLevel { get; set; }

        /// <summary>
    /// 评估前等级
    /// </summary>
    public string PreviousLevel { get; set; }

        /// <summary>
    /// 评估后等级
    /// </summary>
    public string NewLevel { get; set; }

        /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }

        /// <summary>
    /// 证书有效期
    /// </summary>
    public DateTime CertificateExpiryDate { get; set; }

        /// <summary>
    /// 评估人ID
    /// </summary>
    public long AssessorId { get; set; }

        /// <summary>
    /// 评估评语
    /// </summary>
    public string AssessmentComments { get; set; }

        /// <summary>
    /// 优势分析
    /// </summary>
    public string StrengthsAnalysis { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 下次评估日期
    /// </summary>
    public DateTime NextAssessmentDate { get; set; }

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
/// 技能评估表导出DTO
/// </summary>
public partial class TaktSkillAssessmentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSkillAssessmentExportDto()
    {
        CreatedAt = DateTime.Now;
        SkillCategory = string.Empty;
        SkillName = string.Empty;
        SkillDescription = string.Empty;
        AssessmentMethod = string.Empty;
        SkillLevel = string.Empty;
        PreviousLevel = string.Empty;
        NewLevel = string.Empty;
        CertificateNo = string.Empty;
        AssessmentComments = string.Empty;
        StrengthsAnalysis = string.Empty;
        ImprovementSuggestions = string.Empty;
    }

        /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

        /// <summary>
    /// 技能类别
    /// </summary>
    public string SkillCategory { get; set; }

        /// <summary>
    /// 技能名称
    /// </summary>
    public string SkillName { get; set; }

        /// <summary>
    /// 技能描述
    /// </summary>
    public string SkillDescription { get; set; }

        /// <summary>
    /// 评估日期
    /// </summary>
    public DateTime AssessmentDate { get; set; }

        /// <summary>
    /// 评估方式
    /// </summary>
    public string AssessmentMethod { get; set; }

        /// <summary>
    /// 评估得分
    /// </summary>
    public decimal AssessmentScore { get; set; }

        /// <summary>
    /// 技能等级
    /// </summary>
    public string SkillLevel { get; set; }

        /// <summary>
    /// 评估前等级
    /// </summary>
    public string PreviousLevel { get; set; }

        /// <summary>
    /// 评估后等级
    /// </summary>
    public string NewLevel { get; set; }

        /// <summary>
    /// 是否通过
    /// </summary>
    public int IsPassed { get; set; }

        /// <summary>
    /// 证书编号
    /// </summary>
    public string CertificateNo { get; set; }

        /// <summary>
    /// 证书有效期
    /// </summary>
    public DateTime CertificateExpiryDate { get; set; }

        /// <summary>
    /// 评估人ID
    /// </summary>
    public long AssessorId { get; set; }

        /// <summary>
    /// 评估评语
    /// </summary>
    public string AssessmentComments { get; set; }

        /// <summary>
    /// 优势分析
    /// </summary>
    public string StrengthsAnalysis { get; set; }

        /// <summary>
    /// 改进建议
    /// </summary>
    public string ImprovementSuggestions { get; set; }

        /// <summary>
    /// 下次评估日期
    /// </summary>
    public DateTime NextAssessmentDate { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}