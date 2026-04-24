// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.Mail
// 文件名称：TaktMail.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt邮件实体，定义邮件领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.Mail;

/// <summary>
/// Takt邮件实体
/// </summary>
[SugarTable("takt_routine_mail", "邮件表")]
[SugarIndex("ix_takt_routine_mail_mail_code", nameof(MailCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_mail_sender_id", nameof(SenderId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_mail_mail_type", nameof(MailType), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_mail_send_time", nameof(SendTime), OrderByType.Desc)]
[SugarIndex("ix_takt_routine_mail_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_mail_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_mail_mail_status", nameof(MailStatus), OrderByType.Asc)]
public class TaktMail : TaktEntityBase
{
    /// <summary>
    /// 邮件编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "mail_code", ColumnDescription = "邮件编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string MailCode { get; set; } = string.Empty;

    /// <summary>
    /// 邮件主题
    /// </summary>
    [SugarColumn(ColumnName = "mail_subject", ColumnDescription = "邮件主题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string MailSubject { get; set; } = string.Empty;

    /// <summary>
    /// 邮件内容
    /// </summary>
    [SugarColumn(ColumnName = "mail_content", ColumnDescription = "邮件内容", ColumnDataType = "nvarchar", Length = -1, IsNullable = false)]
    public string MailContent { get; set; } = string.Empty;

    /// <summary>
    /// 邮件类型（0=普通邮件，1=系统邮件，2=通知邮件，3=提醒邮件）
    /// </summary>
    [SugarColumn(ColumnName = "mail_type", ColumnDescription = "邮件类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MailType { get; set; } = 0;

    /// <summary>
    /// 发送人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "sender_id", ColumnDescription = "发送人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SenderId { get; set; }

    /// <summary>
    /// 发送人姓名
    /// </summary>
    [SugarColumn(ColumnName = "sender_name", ColumnDescription = "发送人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string SenderName { get; set; } = string.Empty;

    /// <summary>
    /// 发送人邮箱
    /// </summary>
    [SugarColumn(ColumnName = "sender_email", ColumnDescription = "发送人邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? SenderEmail { get; set; }

    /// <summary>
    /// 收件人列表（JSON格式，存储收件人ID、姓名、邮箱等信息）
    /// </summary>
    [SugarColumn(ColumnName = "recipient_list", ColumnDescription = "收件人列表", ColumnDataType = "nvarchar", Length = 2000, IsNullable = false)]
    public string RecipientList { get; set; } = string.Empty;

    /// <summary>
    /// 抄送人列表（JSON格式，存储抄送人ID、姓名、邮箱等信息）
    /// </summary>
    [SugarColumn(ColumnName = "cc_list", ColumnDescription = "抄送人列表", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? CcList { get; set; }

    /// <summary>
    /// 密送人列表（JSON格式，存储密送人ID、姓名、邮箱等信息）
    /// </summary>
    [SugarColumn(ColumnName = "bcc_list", ColumnDescription = "密送人列表", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? BccList { get; set; }

    /// <summary>
    /// 是否重要（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_important", ColumnDescription = "是否重要", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsImportant { get; set; } = 0;

    /// <summary>
    /// 是否紧急（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsUrgent { get; set; } = 0;

    /// <summary>
    /// 是否需要回执（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_read_receipt_required", ColumnDescription = "是否需要回执", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsReadReceiptRequired { get; set; } = 0;

    /// <summary>
    /// 是否已读回执（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_read_receipt_sent", ColumnDescription = "是否已读回执", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsReadReceiptSent { get; set; } = 0;

    /// <summary>
    /// 发送时间
    /// </summary>
    [SugarColumn(ColumnName = "send_time", ColumnDescription = "发送时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? SendTime { get; set; }

    /// <summary>
    /// 定时发送时间（如果设置了定时发送）
    /// </summary>
    [SugarColumn(ColumnName = "scheduled_send_time", ColumnDescription = "定时发送时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ScheduledSendTime { get; set; }

    /// <summary>
    /// 邮件状态（0=草稿，1=已发送，2=发送失败，3=已撤回，4=定时发送中）
    /// </summary>
    [SugarColumn(ColumnName = "mail_status", ColumnDescription = "邮件状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MailStatus { get; set; } = 0;

    /// <summary>
    /// 发送失败原因
    /// </summary>
    [SugarColumn(ColumnName = "send_failure_reason", ColumnDescription = "发送失败原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? SendFailureReason { get; set; }

    /// <summary>
    /// 附件数量
    /// </summary>
    [SugarColumn(ColumnName = "attachment_count", ColumnDescription = "附件数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AttachmentCount { get; set; } = 0;
}
