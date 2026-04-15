// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.Mail
// 文件名称：TaktMailRecipient.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt邮件收件人实体，记录邮件的收件人、抄送人、密送人信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Business.Mail;

/// <summary>
/// Takt邮件收件人实体
/// </summary>
[SugarTable("takt_routine_mail_recipient", "邮件收件人表")]
[SugarIndex("ix_takt_routine_mail_recipient_mail_id_recipient_id_recipient_type", nameof(MailId), OrderByType.Asc, nameof(RecipientId), OrderByType.Asc, nameof(RecipientType), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_mail_recipient_mail_id", nameof(MailId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_mail_recipient_recipient_id", nameof(RecipientId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_mail_recipient_recipient_type", nameof(RecipientType), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_mail_recipient_read_status", nameof(ReadStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_mail_recipient_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_mail_recipient_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktMailRecipient : TaktEntityBase
{
    /// <summary>
    /// 邮件ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "mail_id", ColumnDescription = "邮件ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long MailId { get; set; }

    /// <summary>
    /// 收件人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "recipient_id", ColumnDescription = "收件人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RecipientId { get; set; }

    /// <summary>
    /// 收件人姓名
    /// </summary>
    [SugarColumn(ColumnName = "recipient_name", ColumnDescription = "收件人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RecipientName { get; set; } = string.Empty;

    /// <summary>
    /// 收件人邮箱
    /// </summary>
    [SugarColumn(ColumnName = "recipient_email", ColumnDescription = "收件人邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? RecipientEmail { get; set; }

    /// <summary>
    /// 收件人类型（0=收件人，1=抄送人，2=密送人）
    /// </summary>
    [SugarColumn(ColumnName = "recipient_type", ColumnDescription = "收件人类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RecipientType { get; set; } = 0;

    /// <summary>
    /// 读取状态（0=未读，1=已读）
    /// </summary>
    [SugarColumn(ColumnName = "read_status", ColumnDescription = "读取状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ReadStatus { get; set; } = 0;

    /// <summary>
    /// 读取时间
    /// </summary>
    [SugarColumn(ColumnName = "read_time", ColumnDescription = "读取时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ReadTime { get; set; }

    /// <summary>
    /// 是否已删除（0=否，1=是，用于收件箱的删除标记）
    /// </summary>
    [SugarColumn(ColumnName = "is_recipient_deleted", ColumnDescription = "是否已删除", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsRecipientDeleted { get; set; } = 0;

    /// <summary>
    /// 是否已收藏（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_starred", ColumnDescription = "是否已收藏", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsStarred { get; set; } = 0;

    /// <summary>
    /// 是否已标记（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_flagged", ColumnDescription = "是否已标记", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsFlagged { get; set; } = 0;
}
