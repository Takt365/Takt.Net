// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Meeting
// 文件名称：TaktMeeting.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：会议实体，定义会议领域模型（主题、时间、地点、组织人、议程等）；会议申请见 TaktMeetingApply，会议纪要见 TaktMeetingMinutes
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Meeting;

/// <summary>
/// 会议实体
/// </summary>
/// <remarks>
/// 表示一次会议的核心信息，不含申请单与工作流；可由会议申请 TaktMeetingApply 审批通过后生成并回写 MeetingId，也可直接创建。会议纪要 TaktMeetingMinutes 按 MeetingId 关联本表。
/// </remarks>
[SugarTable("takt_routine_meeting", "会议表")]
[SugarIndex("ix_takt_routine_meeting_start_time", nameof(StartTime), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_meeting_status", nameof(MeetingStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_meeting_type", nameof(MeetingType), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_organizer_id", nameof(OrganizerId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktMeeting : TaktEntityBase
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
    /// 会议主题
    /// </summary>
    [SugarColumn(ColumnName = "meeting_title", ColumnDescription = "会议主题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string MeetingTitle { get; set; } = string.Empty;

    /// <summary>
    /// 会议类型（0=普通会议，1=例会，2=项目会，3=评审会等；可关联字典 sys_meeting_type）
    /// </summary>
    [SugarColumn(ColumnName = "meeting_type", ColumnDescription = "会议类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MeetingType { get; set; } = 0;

    /// <summary>
    /// 会议开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 会议结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 是否全天会议（全天时展示与筛选用）
    /// </summary>
    [SugarColumn(ColumnName = "is_all_day", ColumnDescription = "是否全天", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsAllDay { get; set; } = 0;

    /// <summary>
    /// 会议地点（会议室名称或地址）
    /// </summary>
    [SugarColumn(ColumnName = "location", ColumnDescription = "会议地点", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Location { get; set; }

    /// <summary>
    /// 组织人ID（序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "organizer_id", ColumnDescription = "组织人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OrganizerId { get; set; }

    /// <summary>
    /// 组织人姓名
    /// </summary>
    [SugarColumn(ColumnName = "organizer_name", ColumnDescription = "组织人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OrganizerName { get; set; } = string.Empty;

    /// <summary>
    /// 参与人姓名摘要（如：张三、李四、王五；详细参与人可由关联表维护）
    /// </summary>
    [SugarColumn(ColumnName = "participant_summary", ColumnDescription = "参与人摘要", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ParticipantSummary { get; set; }

    /// <summary>
    /// 会前提醒（分钟数，0=不提醒，5/15/30/60 等）
    /// </summary>
    [SugarColumn(ColumnName = "remind_minutes", ColumnDescription = "提前提醒分钟数", ColumnDataType = "int", IsNullable = false, DefaultValue = "15")]
    public int RemindMinutes { get; set; } = 15;

    /// <summary>
    /// 会议状态（0=待开始，1=进行中，2=已结束，3=已取消；表示会议本身状态，非审批状态）
    /// </summary>
    [SugarColumn(ColumnName = "meeting_status", ColumnDescription = "会议状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MeetingStatus { get; set; } = 0;

    /// <summary>
    /// 会议议程摘要（议题说明；纪要见 TaktMeetingMinutes）
    /// </summary>
    [SugarColumn(ColumnName = "meeting_agenda", ColumnDescription = "会议议程摘要", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? MeetingAgenda { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
