// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.HelpDesk
// 文件名称：TaktTicket.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt工单实体，服务台工单领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.HelpDesk;

/// <summary>
/// Takt工单实体
/// </summary>
[SugarTable("takt_routine_help_desk_ticket", "工单表")]
[SugarIndex("ix_takt_routine_help_desk_ticket_ticket_no", nameof(TicketNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_help_desk_ticket_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_ticket_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_ticket_ticket_status", nameof(TicketStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_ticket_parent_ticket_id", nameof(ParentTicketId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_ticket_ticket_source", nameof(TicketSource), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_ticket_flow_instance_id", nameof(FlowInstanceId), OrderByType.Asc)]
public class TaktTicket : TaktEntityBase
{
    /// <summary>
    /// 工单编号（唯一）
    /// </summary>
    [SugarColumn(ColumnName = "ticket_no", ColumnDescription = "工单编号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string TicketNo { get; set; } = string.Empty;

    /// <summary>
    /// 工单标题
    /// </summary>
    [SugarColumn(ColumnName = "title", ColumnDescription = "工单标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 工单内容描述
    /// </summary>
    [SugarColumn(ColumnName = "content", ColumnDescription = "工单内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? Content { get; set; }

    /// <summary>
    /// 附件列表 JSON。格式：[{ "FileId": 0, "FileName": "", "FilePath": "", "FileSize": 0, "FileType": "", "FileExtension": "", "SortOrder": 0 }]
    /// </summary>
    [SugarColumn(ColumnName = "attachments_json", ColumnDescription = "附件列表JSON", ColumnDataType = "nvarchar", Length = -1, IsNullable = true)]
    public string? AttachmentsJson { get; set; }

    /// <summary>
    /// 工单状态（0=待处理，1=处理中，2=已解决，3=已关闭）
    /// </summary>
    [SugarColumn(ColumnName = "ticket_status", ColumnDescription = "工单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TicketStatus { get; set; } = 0;

    /// <summary>
    /// 优先级（0=低，1=中，2=高，3=紧急）
    /// </summary>
    [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Priority { get; set; } = 1;

    /// <summary>
    /// 分类编码（如 incident/request 等）
    /// </summary>
    [SugarColumn(ColumnName = "category_code", ColumnDescription = "分类编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CategoryCode { get; set; }

    /// <summary>
    /// 工单来源（0=门户网站，1=邮件，2=电话，3=API接入）
    /// </summary>
    [SugarColumn(ColumnName = "ticket_source", ColumnDescription = "工单来源", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TicketSource { get; set; } = 0;

    /// <summary>
    /// 提交人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "submitter_id", ColumnDescription = "提交人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SubmitterId { get; set; }

    /// <summary>
    /// 提交人姓名
    /// </summary>
    [SugarColumn(ColumnName = "submitter_name", ColumnDescription = "提交人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? SubmitterName { get; set; }

    /// <summary>
    /// 处理人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "assignee_id", ColumnDescription = "处理人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? AssigneeId { get; set; }

    /// <summary>
    /// 处理人姓名
    /// </summary>
    [SugarColumn(ColumnName = "assignee_name", ColumnDescription = "处理人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? AssigneeName { get; set; }

    /// <summary>
    /// 关联知识ID（可选，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "knowledge_id", ColumnDescription = "关联知识ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? KnowledgeId { get; set; }

    /// <summary>
    /// 父工单ID（为空表示顶级工单；非空表示该工单为子工单，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_ticket_id", ColumnDescription = "父工单ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ParentTicketId { get; set; }

    /// <summary>
    /// 首次响应时间（支持人员首次回复用户的时间，SLA/OLA 时间追踪）
    /// </summary>
    [SugarColumn(ColumnName = "first_response_at", ColumnDescription = "首次响应时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? FirstResponseAt { get; set; }

    /// <summary>
    /// 首次响应期限（根据 SLA 计算出的首次响应截止时间）
    /// </summary>
    [SugarColumn(ColumnName = "first_response_due_by", ColumnDescription = "首次响应期限", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? FirstResponseDueBy { get; set; }

    /// <summary>
    /// 解决时间（问题被标记为已解决的时间）
    /// </summary>
    [SugarColumn(ColumnName = "resolved_at", ColumnDescription = "解决时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ResolvedAt { get; set; }

    /// <summary>
    /// 解决期限（根据 SLA 计算出的解决截止时间）
    /// </summary>
    [SugarColumn(ColumnName = "resolution_due_by", ColumnDescription = "解决期限", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ResolutionDueBy { get; set; }

    /// <summary>
    /// 关闭时间（工单最终关闭的时间）
    /// </summary>
    [SugarColumn(ColumnName = "closed_at", ColumnDescription = "关闭时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ClosedAt { get; set; }

    /// <summary>
    /// 流程实例ID（关联工作流；流程侧 BusinessType=Ticket、BusinessKey=本表 Id）
    /// </summary>
    [SugarColumn(ColumnName = "flow_instance_id", ColumnDescription = "流程实例ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 申请部门ID
    /// </summary>
    [SugarColumn(ColumnName = "applicant_dept_id", ColumnDescription = "申请部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ApplicantDeptId { get; set; }

    /// <summary>
    /// 申请部门名称
    /// </summary>
    [SugarColumn(ColumnName = "applicant_dept_name", ColumnDescription = "申请部门名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? ApplicantDeptName { get; set; }

    /// <summary>
    /// 申请人ID（实际申请人；代理人代提时填被代理人ID）
    /// </summary>
    [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "申请人ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ApplicantId { get; set; }

    /// <summary>
    /// 申请人（实际申请人姓名）
    /// </summary>
    [SugarColumn(ColumnName = "applicant", ColumnDescription = "申请人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Applicant { get; set; }

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "application_date", ColumnDescription = "申请日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ApplicationDate { get; set; }

    /// <summary>
    /// 子工单列表（父工单时有效；外键：本表 Id = 子工单 ParentTicketId）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(ParentTicketId))]
    public List<TaktTicket>? ChildTickets { get; set; }

    /// <summary>
    /// 服务评价（工单关闭后的评价，一对一）
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(TaktTicketEvaluation.TicketId))]
    public TaktTicketEvaluation? Evaluation { get; set; }
}
