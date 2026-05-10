// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingActivity.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：培训活动实体，记录具体培训活动执行、参与人员、签到、反馈等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训活动实体
/// </summary>
[SugarTable("takt_human_resource_training_activity", "培训活动表")]
[SugarIndex("ix_takt_human_resource_training_activity_activity_code", nameof(ActivityCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_training_activity_training_date", nameof(TrainingDate), OrderByType.Desc)]
[SugarIndex("ix_takt_human_resource_training_activity_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_training_activity_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktTrainingActivity : TaktEntityBase
{
    /// <summary>
    /// 活动编码
    /// </summary>
    [SugarColumn(ColumnName = "activity_code", ColumnDescription = "活动编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ActivityCode { get; set; } = string.Empty;

    /// <summary>
    /// 活动名称
    /// </summary>
    [SugarColumn(ColumnName = "activity_name", ColumnDescription = "活动名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ActivityName { get; set; } = string.Empty;

    /// <summary>
    /// 培训课程ID
    /// </summary>
    [SugarColumn(ColumnName = "training_course_id", ColumnDescription = "培训课程ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TrainingCourseId { get; set; }

    /// <summary>
    /// 培训计划ID
    /// </summary>
    [SugarColumn(ColumnName = "training_plan_id", ColumnDescription = "培训计划ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TrainingPlanId { get; set; }

    /// <summary>
    /// 培训日期
    /// </summary>
    [SugarColumn(ColumnName = "training_date", ColumnDescription = "培训日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime TrainingDate { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", ColumnDataType = "nvarchar", Length = 10, IsNullable = false)]
    public string StartTime { get; set; } = string.Empty;

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", ColumnDataType = "nvarchar", Length = 10, IsNullable = false)]
    public string EndTime { get; set; } = string.Empty;

    /// <summary>
    /// 培训地点
    /// </summary>
    [SugarColumn(ColumnName = "training_location", ColumnDescription = "培训地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TrainingLocation { get; set; } = string.Empty;

    /// <summary>
    /// 培训讲师
    /// </summary>
    [SugarColumn(ColumnName = "instructor", ColumnDescription = "培训讲师", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Instructor { get; set; } = string.Empty;

    /// <summary>
    /// 计划人数
    /// </summary>
    [SugarColumn(ColumnName = "planned_attendees", ColumnDescription = "计划人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PlannedAttendees { get; set; } = 0;

    /// <summary>
    /// 实际签到人数
    /// </summary>
    [SugarColumn(ColumnName = "actual_attendees", ColumnDescription = "实际签到人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ActualAttendees { get; set; } = 0;

    /// <summary>
    /// 培训时长（小时）
    /// </summary>
    [SugarColumn(ColumnName = "training_hours", ColumnDescription = "培训时长", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TrainingHours { get; set; } = 0;

    /// <summary>
    /// 培训费用（元）
    /// </summary>
    [SugarColumn(ColumnName = "training_cost", ColumnDescription = "培训费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TrainingCost { get; set; } = 0;

    /// <summary>
    /// 培训内容摘要
    /// </summary>
    [SugarColumn(ColumnName = "content_summary", ColumnDescription = "培训内容摘要", Length = 2000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ContentSummary { get; set; } = string.Empty;

    /// <summary>
    /// 培训材料
    /// </summary>
    [SugarColumn(ColumnName = "training_materials", ColumnDescription = "培训材料", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string TrainingMaterials { get; set; } = string.Empty;

    /// <summary>
    /// 培训效果评估
    /// </summary>
    [SugarColumn(ColumnName = "effectiveness_evaluation", ColumnDescription = "培训效果评估", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EffectivenessEvaluation { get; set; } = string.Empty;

    /// <summary>
    /// 学员反馈意见
    /// </summary>
    [SugarColumn(ColumnName = "participant_feedback", ColumnDescription = "学员反馈意见", Length = 1000, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ParticipantFeedback { get; set; } = string.Empty;

    /// <summary>
    /// 改进建议
    /// </summary>
    [SugarColumn(ColumnName = "improvement_suggestions", ColumnDescription = "改进建议", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ImprovementSuggestions { get; set; } = string.Empty;

    /// <summary>
    /// 组织者ID
    /// </summary>
    [SugarColumn(ColumnName = "organizer_id", ColumnDescription = "组织者ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OrganizerId { get; set; }

    /// <summary>
    /// 状态(0=待开展 1=进行中 2=已完成 3=已取消 4=延期)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
