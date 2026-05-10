// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Talent
// 文件名称：TaktTalent.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：人才管理实体，记录员工人才档案、能力评估、发展规划等信息
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.Talent;

/// <summary>
/// 人才管理实体
/// </summary>
[SugarTable("takt_human_resource_talent", "人才管理表")]
[SugarIndex("ix_takt_human_resource_talent_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_talent_talent_level", nameof(TalentLevel), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_talent_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_talent_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktTalent : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 人才等级(初级/中级/高级/专家/资深专家)
    /// </summary>
    [SugarColumn(ColumnName = "talent_level", ColumnDescription = "人才等级", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TalentLevel { get; set; } = string.Empty;

    /// <summary>
    /// 专业技能
    /// </summary>
    [SugarColumn(ColumnName = "professional_skills", ColumnDescription = "专业技能", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ProfessionalSkills { get; set; } = string.Empty;

    /// <summary>
    /// 核心能力评估
    /// </summary>
    [SugarColumn(ColumnName = "core_competency", ColumnDescription = "核心能力评估", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CoreCompetency { get; set; } = string.Empty;

    /// <summary>
    /// 领导力评分
    /// </summary>
    [SugarColumn(ColumnName = "leadership_score", ColumnDescription = "领导力评分", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal LeadershipScore { get; set; } = 0;

    /// <summary>
    /// 创新能力评分
    /// </summary>
    [SugarColumn(ColumnName = "innovation_score", ColumnDescription = "创新能力评分", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal InnovationScore { get; set; } = 0;

    /// <summary>
    /// 协作能力评分
    /// </summary>
    [SugarColumn(ColumnName = "collaboration_score", ColumnDescription = "协作能力评分", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal CollaborationScore { get; set; } = 0;

    /// <summary>
    /// 发展潜力(高/中/低)
    /// </summary>
    [SugarColumn(ColumnName = "development_potential", ColumnDescription = "发展潜力", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DevelopmentPotential { get; set; } = string.Empty;

    /// <summary>
    /// 职业发展规划
    /// </summary>
    [SugarColumn(ColumnName = "career_plan", ColumnDescription = "职业发展规划", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CareerPlan { get; set; } = string.Empty;

    /// <summary>
    /// 人才标签(多个标签用逗号分隔)
    /// </summary>
    [SugarColumn(ColumnName = "talent_tags", ColumnDescription = "人才标签", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TalentTags { get; set; } = string.Empty;

    /// <summary>
    /// 评估日期
    /// </summary>
    [SugarColumn(ColumnName = "evaluation_date", ColumnDescription = "评估日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EvaluationDate { get; set; }

    /// <summary>
    /// 评估人ID
    /// </summary>
    [SugarColumn(ColumnName = "evaluator_id", ColumnDescription = "评估人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EvaluatorId { get; set; }

    /// <summary>
    /// 状态(0=待评估 1=评估中 2=已入库 3=已归档)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
