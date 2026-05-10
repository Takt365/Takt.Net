// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingDevelopment.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：培训发展实体，记录员工培训课程、学习进度、发展计划等信息
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训发展实体
/// </summary>
[SugarTable("takt_human_resource_training_development", "培训发展表")]
[SugarIndex("ix_takt_human_resource_training_development_employee_id", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_training_development_training_date", nameof(TrainingDate), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_training_development_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_training_development_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktTrainingDevelopment : TaktEntityBase
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 培训课程名称
    /// </summary>
    [SugarColumn(ColumnName = "course_name", ColumnDescription = "培训课程名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CourseName { get; set; } = string.Empty;

    /// <summary>
    /// 培训类型(入职培训/技能培训/管理培训/安全培训/专业培训)
    /// </summary>
    [SugarColumn(ColumnName = "training_type", ColumnDescription = "培训类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TrainingType { get; set; } = string.Empty;

    /// <summary>
    /// 培训讲师
    /// </summary>
    [SugarColumn(ColumnName = "instructor", ColumnDescription = "培训讲师", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Instructor { get; set; } = string.Empty;

    /// <summary>
    /// 培训开始日期
    /// </summary>
    [SugarColumn(ColumnName = "training_start_date", ColumnDescription = "培训开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime TrainingStartDate { get; set; }

    /// <summary>
    /// 培训结束日期
    /// </summary>
    [SugarColumn(ColumnName = "training_end_date", ColumnDescription = "培训结束日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime TrainingEndDate { get; set; }

    /// <summary>
    /// 培训日期（单次培训使用）
    /// </summary>
    [SugarColumn(ColumnName = "training_date", ColumnDescription = "培训日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime TrainingDate { get; set; }

    /// <summary>
    /// 培训时长（小时）
    /// </summary>
    [SugarColumn(ColumnName = "training_hours", ColumnDescription = "培训时长", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TrainingHours { get; set; } = 0;

    /// <summary>
    /// 培训地点
    /// </summary>
    [SugarColumn(ColumnName = "training_location", ColumnDescription = "培训地点", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TrainingLocation { get; set; } = string.Empty;

    /// <summary>
    /// 培训成绩
    /// </summary>
    [SugarColumn(ColumnName = "training_score", ColumnDescription = "培训成绩", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TrainingScore { get; set; } = 0;

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
    /// 培训评价
    /// </summary>
    [SugarColumn(ColumnName = "training_evaluation", ColumnDescription = "培训评价", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TrainingEvaluation { get; set; } = string.Empty;

    /// <summary>
    /// 改进建议
    /// </summary>
    [SugarColumn(ColumnName = "improvement_suggestions", ColumnDescription = "改进建议", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ImprovementSuggestions { get; set; } = string.Empty;

    /// <summary>
    /// 发展计划
    /// </summary>
    [SugarColumn(ColumnName = "development_plan", ColumnDescription = "发展计划", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DevelopmentPlan { get; set; } = string.Empty;

    /// <summary>
    /// 状态(0=未开始 1=进行中 2=已完成 3=已认证)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
