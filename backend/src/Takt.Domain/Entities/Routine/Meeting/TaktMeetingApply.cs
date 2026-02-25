// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Meeting
// 文件名称：TaktMeetingApply.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：会议申请实体，定义会议申请领域模型；申请经工作流审批（关联 TaktFlowInstance），审批通过后可生成或关联会议实体 TaktMeeting
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Meeting;

/// <summary>
/// 会议申请实体
/// </summary>
/// <remarks>
/// 与用车申请 TaktVehicleApply 同风格：申请单编码、工作流实例、申请状态、申请人、申请时间及会议信息。流程实例 ProcessKey = "MeetingApply"，BusinessKey = 本实体 Id；发起时回写 InstanceId，结束时按 InstanceId 更新 ApplyStatus；审批通过后可生成 TaktMeeting 并回写 MeetingId。
/// </remarks>
[SugarTable("takt_routine_meeting_apply", "会议申请表")]
[SugarIndex("ix_takt_routine_meeting_apply_apply_code", nameof(ApplyCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_meeting_apply_apply_status", nameof(ApplyStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_apply_instance_id", nameof(InstanceId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_apply_apply_time", nameof(ApplyTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_meeting_apply_applicant_id", nameof(ApplicantId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_apply_meeting_id", nameof(MeetingId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_apply_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_apply_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_meeting_apply_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktMeetingApply : TaktEntityBase
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
    /// 申请单编码（唯一索引，可由单据编码规则生成）
    /// </summary>
    [SugarColumn(ColumnName = "apply_code", ColumnDescription = "申请单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ApplyCode { get; set; } = string.Empty;

    /// <summary>
    /// 关联工作流实例ID（对应 TaktFlowInstance.Id，0=未关联；流程处理见 TaktFlowInstanceService；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "工作流实例ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 申请状态（0=草稿，1=审批中，2=已批准，3=已驳回；与工作流审批衔接）
    /// </summary>
    [SugarColumn(ColumnName = "apply_status", ColumnDescription = "申请状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ApplyStatus { get; set; } = 0;

    /// <summary>
    /// 申请人ID（序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "申请人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ApplicantId { get; set; }

    /// <summary>
    /// 申请人姓名
    /// </summary>
    [SugarColumn(ColumnName = "applicant_name", ColumnDescription = "申请人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ApplicantName { get; set; } = string.Empty;

    /// <summary>
    /// 申请部门ID（序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_dept_id", ColumnDescription = "申请部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ApplicantDeptId { get; set; }

    /// <summary>
    /// 申请部门名称
    /// </summary>
    [SugarColumn(ColumnName = "applicant_dept_name", ColumnDescription = "申请部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? ApplicantDeptName { get; set; }

    /// <summary>
    /// 申请时间（提交审批时间）
    /// </summary>
    [SugarColumn(ColumnName = "apply_time", ColumnDescription = "申请时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ApplyTime { get; set; }

    /// <summary>
    /// 会议主题
    /// </summary>
    [SugarColumn(ColumnName = "meeting_title", ColumnDescription = "会议主题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string MeetingTitle { get; set; } = string.Empty;

    /// <summary>
    /// 会议类型（0=普通会议，1=例会，2=项目会，3=评审会等；可关联字典）
    /// </summary>
    [SugarColumn(ColumnName = "meeting_type", ColumnDescription = "会议类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MeetingType { get; set; } = 0;

    /// <summary>
    /// 会议开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "会议开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 会议结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "会议结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 是否全天会议
    /// </summary>
    [SugarColumn(ColumnName = "is_all_day", ColumnDescription = "是否全天", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsAllDay { get; set; } = 0;

    /// <summary>
    /// 会议地点（会议室名称或地址）
    /// </summary>
    [SugarColumn(ColumnName = "location", ColumnDescription = "会议地点", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Location { get; set; }

    /// <summary>
    /// 参与人姓名摘要（如：张三、李四、王五）
    /// </summary>
    [SugarColumn(ColumnName = "participant_summary", ColumnDescription = "参与人摘要", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ParticipantSummary { get; set; }

    /// <summary>
    /// 会前提醒（分钟数，0=不提醒，5/15/30/60 等）
    /// </summary>
    [SugarColumn(ColumnName = "remind_minutes", ColumnDescription = "提前提醒分钟数", ColumnDataType = "int", IsNullable = false, DefaultValue = "15")]
    public int RemindMinutes { get; set; } = 15;

    /// <summary>
    /// 会议议程摘要（申请时填写的议题说明）
    /// </summary>
    [SugarColumn(ColumnName = "meeting_agenda", ColumnDescription = "会议议程摘要", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? MeetingAgenda { get; set; }

    /// <summary>
    /// 关联的会议实体ID（审批通过后生成或关联 TaktMeeting 时回写，0=未关联；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "meeting_id", ColumnDescription = "会议实体ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long MeetingId { get; set; }
}
