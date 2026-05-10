// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingCourse.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：培训课程实体，定义培训课程信息、课程内容、讲师、时长等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训课程实体
/// </summary>
[SugarTable("takt_human_resource_training_course", "培训课程表")]
[SugarIndex("ix_takt_human_resource_training_course_course_code", nameof(CourseCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_training_course_course_type", nameof(CourseType), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_training_course_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_training_course_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktTrainingCourse : TaktEntityBase
{
    /// <summary>
    /// 课程编码
    /// </summary>
    [SugarColumn(ColumnName = "course_code", ColumnDescription = "课程编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CourseCode { get; set; } = string.Empty;

    /// <summary>
    /// 课程名称
    /// </summary>
    [SugarColumn(ColumnName = "course_name", ColumnDescription = "课程名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CourseName { get; set; } = string.Empty;

    /// <summary>
    /// 课程类型(入职培训/技能培训/管理培训/安全培训/专业培训/软技能培训/其他)
    /// </summary>
    [SugarColumn(ColumnName = "course_type", ColumnDescription = "课程类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CourseType { get; set; } = string.Empty;

    /// <summary>
    /// 课程级别(初级/中级/高级/专家)
    /// </summary>
    [SugarColumn(ColumnName = "course_level", ColumnDescription = "课程级别", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CourseLevel { get; set; } = string.Empty;

    /// <summary>
    /// 课程描述
    /// </summary>
    [SugarColumn(ColumnName = "course_description", ColumnDescription = "课程描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CourseDescription { get; set; } = string.Empty;

    /// <summary>
    /// 课程目标
    /// </summary>
    [SugarColumn(ColumnName = "course_objectives", ColumnDescription = "课程目标", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CourseObjectives { get; set; } = string.Empty;

    /// <summary>
    /// 适用部门
    /// </summary>
    [SugarColumn(ColumnName = "applicable_department", ColumnDescription = "适用部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicableDepartment { get; set; } = string.Empty;

    /// <summary>
    /// 适用岗位
    /// </summary>
    [SugarColumn(ColumnName = "applicable_position", ColumnDescription = "适用岗位", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicablePosition { get; set; } = string.Empty;

    /// <summary>
    /// 培训时长（小时）
    /// </summary>
    [SugarColumn(ColumnName = "training_hours", ColumnDescription = "培训时长", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TrainingHours { get; set; } = 0;

    /// <summary>
    /// 培训天数
    /// </summary>
    [SugarColumn(ColumnName = "training_days", ColumnDescription = "培训天数", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TrainingDays { get; set; } = 0;

    /// <summary>
    /// 主讲讲师
    /// </summary>
    [SugarColumn(ColumnName = "main_instructor", ColumnDescription = "主讲讲师", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string MainInstructor { get; set; } = string.Empty;

    /// <summary>
    /// 培训方式(线下/线上/混合)
    /// </summary>
    [SugarColumn(ColumnName = "training_method", ColumnDescription = "培训方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TrainingMethod { get; set; } = string.Empty;

    /// <summary>
    /// 考核方式(考试/实操/作业/无)
    /// </summary>
    [SugarColumn(ColumnName = "assessment_method", ColumnDescription = "考核方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string AssessmentMethod { get; set; } = string.Empty;

    /// <summary>
    /// 及格分数线
    /// </summary>
    [SugarColumn(ColumnName = "passing_score", ColumnDescription = "及格分数线", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PassingScore { get; set; } = 0;

    /// <summary>
    /// 是否颁发证书(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "is_certification", ColumnDescription = "是否颁发证书", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsCertification { get; set; } = 0;

    /// <summary>
    /// 课程大纲
    /// </summary>
    [SugarColumn(ColumnName = "course_outline", ColumnDescription = "课程大纲", Length = 2000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CourseOutline { get; set; } = string.Empty;

    /// <summary>
    /// 培训材料清单
    /// </summary>
    [SugarColumn(ColumnName = "material_list", ColumnDescription = "培训材料清单", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string MaterialList { get; set; } = string.Empty;

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 状态(0=启用 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
