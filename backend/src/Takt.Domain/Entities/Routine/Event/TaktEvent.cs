// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.Event
// 文件名称：TaktEvent.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：活动组织（Event）实体，定义活动/事件领域模型（活动名称、类型、时间、地点、组织人等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Event;

/// <summary>
/// 活动组织实体（Event = 有计划的活动/事件，与 Activity 行为记录区分）
/// </summary>
/// <remarks>用于日常事务中的活动组织管理，活动编码可由单据编码规则生成。</remarks>
[SugarTable("takt_routine_event", "活动组织表")]
[SugarIndex("ix_takt_routine_event_event_code", nameof(EventCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_event_event_type", nameof(EventType), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_event_start_time", nameof(StartTime), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_event_event_status", nameof(EventStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_event_organizer_id", nameof(OrganizerId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_event_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_event_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_event_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEvent : TaktEntityBase
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
    /// 活动编码（唯一索引，可由单据编码规则生成）
    /// </summary>
    [SugarColumn(ColumnName = "event_code", ColumnDescription = "活动编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string EventCode { get; set; } = string.Empty;

    /// <summary>
    /// 活动名称
    /// </summary>
    [SugarColumn(ColumnName = "event_name", ColumnDescription = "活动名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string EventName { get; set; } = string.Empty;

    /// <summary>
    /// 活动类型（0=培训，1=团建，2=会议活动，3=庆典，4=其他；可关联字典）
    /// </summary>
    [SugarColumn(ColumnName = "event_type", ColumnDescription = "活动类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EventType { get; set; } = 0;

    /// <summary>
    /// 活动开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 活动结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 活动地点
    /// </summary>
    [SugarColumn(ColumnName = "location", ColumnDescription = "活动地点", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
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
    /// 组织部门ID（序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "组织部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DeptId { get; set; }

    /// <summary>
    /// 组织部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "组织部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DeptName { get; set; }

    /// <summary>
    /// 活动状态（0=草稿，1=已发布，2=进行中，3=已结束，4=已取消）
    /// </summary>
    [SugarColumn(ColumnName = "event_status", ColumnDescription = "活动状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EventStatus { get; set; } = 0;

    /// <summary>
    /// 活动内容/描述
    /// </summary>
    [SugarColumn(ColumnName = "event_content", ColumnDescription = "活动内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? EventContent { get; set; }

    /// <summary>
    /// 参与人摘要（如：张三、李四；详细参与可由关联表维护）
    /// </summary>
    [SugarColumn(ColumnName = "participant_summary", ColumnDescription = "参与人摘要", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ParticipantSummary { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
