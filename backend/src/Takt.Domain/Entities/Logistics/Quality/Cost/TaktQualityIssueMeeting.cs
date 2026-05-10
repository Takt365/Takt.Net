// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Quality.Cost
// 文件名称：TaktQualityIssueMeeting.cs
// 创建时间：2026-05-07
// 创建人：Takt365(Qoder AI)
// 功能描述:品质问题应对明细 - 会议/调查/试验费用
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Cost;

/// <summary>
/// 品质问题应对明细 - 会议/调查/试验费用
/// </summary>
[SugarTable("takt_logistics_quality_issue_meeting", "质量问题会议调查试验费用明细表")]
[SugarIndex("ix_takt_logistics_quality_issue_meeting_quality_issue_id", nameof(QualityIssueId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_issue_meeting_line_number", nameof(LineNumber), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_issue_meeting_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_issue_meeting_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktQualityIssueMeeting : TaktEntityBase
{
    /// <summary>
    /// 品质问题主表ID(主子表关系,序列化为string以避免Javascript精度问题)
    /// </summary>
    [SugarColumn(ColumnName = "quality_issue_id", ColumnDescription = "品质问题主表ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long QualityIssueId { get; set; }

    /// <summary>
    /// 项号（行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "项号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 直接人员费率（元/分钟）
    /// </summary>
    [SugarColumn(ColumnName = "direct_manpower_cost_per_minute", ColumnDescription = "直接人员费率", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal DirectManpowerCostPerMinute { get; set; } = 0;

    /// <summary>
    /// 间接人员费率（元/分钟）
    /// </summary>
    [SugarColumn(ColumnName = "indirect_manpower_cost_per_minute", ColumnDescription = "间接人员费率", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal IndirectManpowerCostPerMinute { get; set; } = 0;

    /// <summary>
    /// 讨论调查试验内容(会议记录)
    /// </summary>
    [SugarColumn(ColumnName = "meeting_investigation_content", ColumnDescription = "讨论调查试验内容", ColumnDataType = "ntext", IsNullable = true)]
    public string? MeetingInvestigationContent { get; set; }

    /// <summary>
    /// 讨论调查试验费用(元)
    /// </summary>
    [SugarColumn(ColumnName = "meeting_investigation_cost", ColumnDescription = "讨论调查试验费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal MeetingInvestigationCost { get; set; } = 0;

    /// <summary>
    /// 讨论会使用时间(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "meeting_time_minutes", ColumnDescription = "检讨会使用时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MeetingTimeMinutes { get; set; } = 0;

    /// <summary>
    /// 直接人员参加人数
    /// </summary>
    [SugarColumn(ColumnName = "direct_participant_count", ColumnDescription = "直接人员参加人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DirectParticipantCount { get; set; } = 0;

    /// <summary>
    /// 间接人员参加人数
    /// </summary>
    [SugarColumn(ColumnName = "indirect_participant_count", ColumnDescription = "间接人员参加人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IndirectParticipantCount { get; set; } = 0;

    /// <summary>
    /// 调查评价试验工作时间（分钟）
    /// </summary>
    [SugarColumn(ColumnName = "investigation_work_time_minutes", ColumnDescription = "调查评价试验工作时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int InvestigationWorkTimeMinutes { get; set; } = 0;

    /// <summary>
    /// 交通费、旅费（元）
    /// </summary>
    [SugarColumn(ColumnName = "travel_cost", ColumnDescription = "交通费旅费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TravelCost { get; set; } = 0;

    /// <summary>
    /// 其他费用（元）
    /// </summary>
    [SugarColumn(ColumnName = "other_expenses", ColumnDescription = "其他费用", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal OtherExpenses { get; set; } = 0;

    /// <summary>
    /// 其他作业時間（分钟）
    /// </summary>
    [SugarColumn(ColumnName = "other_work_time_minutes", ColumnDescription = "其他作业时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OtherWorkTimeMinutes { get; set; } = 0;

    /// <summary>
    /// 其他设备购入费、工程费、搬运费等（元）
    /// </summary>
    [SugarColumn(ColumnName = "other_apparatus_cost", ColumnDescription = "其他设备工程搬运费", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal OtherApparatusCost { get; set; } = 0;

    /// <summary>
    /// 品质问题対応记录者（会议调查试验记录者）
    /// </summary>
    [SugarColumn(ColumnName = "meeting_recorder", ColumnDescription = "品质问题对应记录者", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? MeetingRecorder { get; set; }


    /// <summary>
    /// 质量问题主表（导航属性）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(QualityIssueId))]
    public TaktQualityIssue? Issue { get; set; }
}
