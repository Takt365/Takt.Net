// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Meeting
// 文件名称：TaktMeetingMinutes.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：会议纪要实体，定义会议记录领域模型（关联会议申请、纪要内容、决议、待办等），与会议申请 TaktMeeting 分离
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Meeting;

/// <summary>
/// 会议纪要实体
/// </summary>
/// <remarks>
/// 用于记录会议内容、决议与待办；可关联会议申请（MeetingId 指向 TaktMeeting），也可独立创建（如临时会议后补记）。与会议申请 TaktMeeting 分离，申请管预订与审批，纪要管记录与落实。
/// </remarks>
[SugarTable("takt_routine_meeting_minutes", "会议纪要表")]
[SugarIndex("ix_takt_routine_meeting_minutes_meeting_id", nameof(MeetingId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_minutes_meeting_time", nameof(MeetingTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_meeting_minutes_recorder_id", nameof(RecorderId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_minutes_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_minutes_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_minutes_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktMeetingMinutes : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 工厂代码（关联工厂主数据 TaktPlant.PlantCode；冗余便于列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 关联的会议申请ID（0=未关联，表示无申请或临时会议后补记；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "meeting_id", ColumnDescription = "会议申请ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long MeetingId { get; set; }

    /// <summary>
    /// 纪要标题
    /// </summary>
    [SugarColumn(ColumnName = "minutes_title", ColumnDescription = "纪要标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string MinutesTitle { get; set; } = string.Empty;

    /// <summary>
    /// 会议时间（实际开会日期时间，用于排序与筛选；可与申请的开始时间一致或由记录人填写）
    /// </summary>
    [SugarColumn(ColumnName = "meeting_time", ColumnDescription = "会议时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime MeetingTime { get; set; }

    /// <summary>
    /// 参与人摘要（如：张三、李四、王五；用于列表展示）
    /// </summary>
    [SugarColumn(ColumnName = "attendee_summary", ColumnDescription = "参与人摘要", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? AttendeeSummary { get; set; }

    /// <summary>
    /// 记录人ID（序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "recorder_id", ColumnDescription = "记录人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RecorderId { get; set; }

    /// <summary>
    /// 记录人姓名
    /// </summary>
    [SugarColumn(ColumnName = "recorder_name", ColumnDescription = "记录人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RecorderName { get; set; } = string.Empty;

    /// <summary>
    /// 纪要内容（会议记录正文）
    /// </summary>
    [SugarColumn(ColumnName = "minutes_content", ColumnDescription = "纪要内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? MinutesContent { get; set; }

    /// <summary>
    /// 决议摘要（会议形成的决议、结论；可结构化存 JSON 或纯文本）
    /// </summary>
    [SugarColumn(ColumnName = "conclusions", ColumnDescription = "决议摘要", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? Conclusions { get; set; }

    /// <summary>
    /// 待办事项摘要（后续待办、责任人等；可结构化存 JSON 或纯文本）
    /// </summary>
    [SugarColumn(ColumnName = "action_items", ColumnDescription = "待办事项摘要", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? ActionItems { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
