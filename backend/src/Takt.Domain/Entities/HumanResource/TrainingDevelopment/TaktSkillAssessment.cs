// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.TrainingDevelopment
// 文件名称：TaktSkillAssessment.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：技能评估实体，记录员工技能评估、技能等级认证、评估结果等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.TrainingDevelopment;

/// <summary>
/// 技能评估实体
/// </summary>
[SugarTable("takt_human_resource_skill_assessment", "技能评估表")]
[SugarIndex("ix_takt_human_resource_skill_assessment_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_skill_assessment_assessment_date", nameof(AssessmentDate), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_skill_assessment_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_skill_assessment_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSkillAssessment : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 技能类别(技术技能/管理技能/沟通技能/安全技能/质量技能/其他)
    /// </summary>
    [SugarColumn(ColumnName = "skill_category", ColumnDescription = "技能类别", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SkillCategory { get; set; } = string.Empty;

    /// <summary>
    /// 技能名称
    /// </summary>
    [SugarColumn(ColumnName = "skill_name", ColumnDescription = "技能名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SkillName { get; set; } = string.Empty;

    /// <summary>
    /// 技能描述
    /// </summary>
    [SugarColumn(ColumnName = "skill_description", ColumnDescription = "技能描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SkillDescription { get; set; } = string.Empty;

    /// <summary>
    /// 评估日期
    /// </summary>
    [SugarColumn(ColumnName = "assessment_date", ColumnDescription = "评估日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime AssessmentDate { get; set; }

    /// <summary>
    /// 评估方式(理论考试/实操考核/项目评审/综合评估)
    /// </summary>
    [SugarColumn(ColumnName = "assessment_method", ColumnDescription = "评估方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string AssessmentMethod { get; set; } = string.Empty;

    /// <summary>
    /// 评估得分
    /// </summary>
    [SugarColumn(ColumnName = "assessment_score", ColumnDescription = "评估得分", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal AssessmentScore { get; set; } = 0;

    /// <summary>
    /// 技能等级(初级/中级/高级/专家/资深专家)
    /// </summary>
    [SugarColumn(ColumnName = "skill_level", ColumnDescription = "技能等级", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SkillLevel { get; set; } = string.Empty;

    /// <summary>
    /// 评估前等级
    /// </summary>
    [SugarColumn(ColumnName = "previous_level", ColumnDescription = "评估前等级", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PreviousLevel { get; set; } = string.Empty;

    /// <summary>
    /// 评估后等级
    /// </summary>
    [SugarColumn(ColumnName = "new_level", ColumnDescription = "评估后等级", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string NewLevel { get; set; } = string.Empty;

    /// <summary>
    /// 是否通过(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "is_passed", ColumnDescription = "是否通过", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsPassed { get; set; } = 0;

    /// <summary>
    /// 证书编号
    /// </summary>
    [SugarColumn(ColumnName = "certificate_no", ColumnDescription = "证书编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CertificateNo { get; set; } = string.Empty;

    /// <summary>
    /// 证书有效期
    /// </summary>
    [SugarColumn(ColumnName = "certificate_expiry_date", ColumnDescription = "证书有效期", ColumnDataType = "date", IsNullable = false)]
    public DateTime CertificateExpiryDate { get; set; }

    /// <summary>
    /// 评估人ID
    /// </summary>
    [SugarColumn(ColumnName = "assessor_id", ColumnDescription = "评估人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long AssessorId { get; set; }

    /// <summary>
    /// 评估评语
    /// </summary>
    [SugarColumn(ColumnName = "assessment_comments", ColumnDescription = "评估评语", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string AssessmentComments { get; set; } = string.Empty;

    /// <summary>
    /// 优势分析
    /// </summary>
    [SugarColumn(ColumnName = "strengths_analysis", ColumnDescription = "优势分析", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string StrengthsAnalysis { get; set; } = string.Empty;

    /// <summary>
    /// 改进建议
    /// </summary>
    [SugarColumn(ColumnName = "improvement_suggestions", ColumnDescription = "改进建议", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ImprovementSuggestions { get; set; } = string.Empty;

    /// <summary>
    /// 下次评估日期
    /// </summary>
    [SugarColumn(ColumnName = "next_assessment_date", ColumnDescription = "下次评估日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime NextAssessmentDate { get; set; }

    /// <summary>
    /// 状态(0=待评估 1=评估中 2=已通过 3=未通过 4=已认证 5=已过期)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
