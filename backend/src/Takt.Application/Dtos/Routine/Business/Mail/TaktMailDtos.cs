// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.Mail
// 文件名称：TaktMailDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：邮件表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.Mail;

/// <summary>
/// 邮件表Dto
/// </summary>
public partial class TaktMailDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailDto()
    {
        MailCode = string.Empty;
        MailSubject = string.Empty;
        MailContent = string.Empty;
        SenderName = string.Empty;
        RecipientList = string.Empty;
    }

    /// <summary>
    /// 邮件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailId { get; set; }

    /// <summary>
    /// 邮件编码
    /// </summary>
    public string MailCode { get; set; }
    /// <summary>
    /// 邮件主题
    /// </summary>
    public string MailSubject { get; set; }
    /// <summary>
    /// 邮件内容
    /// </summary>
    public string MailContent { get; set; }
    /// <summary>
    /// 邮件类型
    /// </summary>
    public int MailType { get; set; }
    /// <summary>
    /// 发送人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SenderId { get; set; }
    /// <summary>
    /// 发送人姓名
    /// </summary>
    public string SenderName { get; set; }
    /// <summary>
    /// 发送人邮箱
    /// </summary>
    public string? SenderEmail { get; set; }
    /// <summary>
    /// 收件人列表
    /// </summary>
    public string RecipientList { get; set; }
    /// <summary>
    /// 抄送人列表
    /// </summary>
    public string? CcList { get; set; }
    /// <summary>
    /// 密送人列表
    /// </summary>
    public string? BccList { get; set; }
    /// <summary>
    /// 是否重要
    /// </summary>
    public int IsImportant { get; set; }
    /// <summary>
    /// 是否紧急
    /// </summary>
    public int IsUrgent { get; set; }
    /// <summary>
    /// 是否需要回执
    /// </summary>
    public int IsReadReceiptRequired { get; set; }
    /// <summary>
    /// 是否已读回执
    /// </summary>
    public int IsReadReceiptSent { get; set; }
    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime? SendTime { get; set; }
    /// <summary>
    /// 定时发送时间
    /// </summary>
    public DateTime? ScheduledSendTime { get; set; }
    /// <summary>
    /// 邮件状态
    /// </summary>
    public int MailStatus { get; set; }
    /// <summary>
    /// 发送失败原因
    /// </summary>
    public string? SendFailureReason { get; set; }
    /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }
}

/// <summary>
/// 邮件表查询DTO
/// </summary>
public partial class TaktMailQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 邮件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailId { get; set; }

    /// <summary>
    /// 邮件编码
    /// </summary>
    public string? MailCode { get; set; }
    /// <summary>
    /// 邮件主题
    /// </summary>
    public string? MailSubject { get; set; }
    /// <summary>
    /// 邮件内容
    /// </summary>
    public string? MailContent { get; set; }
    /// <summary>
    /// 邮件类型
    /// </summary>
    public int? MailType { get; set; }
    /// <summary>
    /// 发送人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SenderId { get; set; }
    /// <summary>
    /// 发送人姓名
    /// </summary>
    public string? SenderName { get; set; }
    /// <summary>
    /// 发送人邮箱
    /// </summary>
    public string? SenderEmail { get; set; }
    /// <summary>
    /// 收件人列表
    /// </summary>
    public string? RecipientList { get; set; }
    /// <summary>
    /// 抄送人列表
    /// </summary>
    public string? CcList { get; set; }
    /// <summary>
    /// 密送人列表
    /// </summary>
    public string? BccList { get; set; }
    /// <summary>
    /// 是否重要
    /// </summary>
    public int? IsImportant { get; set; }
    /// <summary>
    /// 是否紧急
    /// </summary>
    public int? IsUrgent { get; set; }
    /// <summary>
    /// 是否需要回执
    /// </summary>
    public int? IsReadReceiptRequired { get; set; }
    /// <summary>
    /// 是否已读回执
    /// </summary>
    public int? IsReadReceiptSent { get; set; }
    /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime? SendTime { get; set; }

    /// <summary>
    /// 发送时间开始时间
    /// </summary>
    public DateTime? SendTimeStart { get; set; }
    /// <summary>
    /// 发送时间结束时间
    /// </summary>
    public DateTime? SendTimeEnd { get; set; }
    /// <summary>
    /// 定时发送时间
    /// </summary>
    public DateTime? ScheduledSendTime { get; set; }

    /// <summary>
    /// 定时发送时间开始时间
    /// </summary>
    public DateTime? ScheduledSendTimeStart { get; set; }
    /// <summary>
    /// 定时发送时间结束时间
    /// </summary>
    public DateTime? ScheduledSendTimeEnd { get; set; }
    /// <summary>
    /// 邮件状态
    /// </summary>
    public int? MailStatus { get; set; }
    /// <summary>
    /// 发送失败原因
    /// </summary>
    public string? SendFailureReason { get; set; }
    /// <summary>
    /// 附件数量
    /// </summary>
    public int? AttachmentCount { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建邮件表DTO
/// </summary>
public partial class TaktMailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailCreateDto()
    {
        MailCode = string.Empty;
        MailSubject = string.Empty;
        MailContent = string.Empty;
        SenderName = string.Empty;
        RecipientList = string.Empty;
    }

        /// <summary>
    /// 邮件编码
    /// </summary>
    public string MailCode { get; set; }

        /// <summary>
    /// 邮件主题
    /// </summary>
    public string MailSubject { get; set; }

        /// <summary>
    /// 邮件内容
    /// </summary>
    public string MailContent { get; set; }

        /// <summary>
    /// 邮件类型
    /// </summary>
    public int MailType { get; set; }

        /// <summary>
    /// 发送人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SenderId { get; set; }

        /// <summary>
    /// 发送人姓名
    /// </summary>
    public string SenderName { get; set; }

        /// <summary>
    /// 发送人邮箱
    /// </summary>
    public string? SenderEmail { get; set; }

        /// <summary>
    /// 收件人列表
    /// </summary>
    public string RecipientList { get; set; }

        /// <summary>
    /// 抄送人列表
    /// </summary>
    public string? CcList { get; set; }

        /// <summary>
    /// 密送人列表
    /// </summary>
    public string? BccList { get; set; }

        /// <summary>
    /// 是否重要
    /// </summary>
    public int IsImportant { get; set; }

        /// <summary>
    /// 是否紧急
    /// </summary>
    public int IsUrgent { get; set; }

        /// <summary>
    /// 是否需要回执
    /// </summary>
    public int IsReadReceiptRequired { get; set; }

        /// <summary>
    /// 是否已读回执
    /// </summary>
    public int IsReadReceiptSent { get; set; }

        /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime? SendTime { get; set; }

        /// <summary>
    /// 定时发送时间
    /// </summary>
    public DateTime? ScheduledSendTime { get; set; }

        /// <summary>
    /// 邮件状态
    /// </summary>
    public int MailStatus { get; set; }

        /// <summary>
    /// 发送失败原因
    /// </summary>
    public string? SendFailureReason { get; set; }

        /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新邮件表DTO
/// </summary>
public partial class TaktMailUpdateDto : TaktMailCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailUpdateDto()
    {
    }

        /// <summary>
    /// 邮件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailId { get; set; }
}

/// <summary>
/// 邮件表邮件状态DTO
/// </summary>
public partial class TaktMailStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailStatusDto()
    {
    }

        /// <summary>
    /// 邮件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailId { get; set; }

    /// <summary>
    /// 邮件状态（0=禁用，1=启用）
    /// </summary>
    public int MailStatus { get; set; }
}

/// <summary>
/// 邮件表导入模板DTO
/// </summary>
public partial class TaktMailTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailTemplateDto()
    {
        MailCode = string.Empty;
        MailSubject = string.Empty;
        MailContent = string.Empty;
        SenderName = string.Empty;
        RecipientList = string.Empty;
    }

        /// <summary>
    /// 邮件编码
    /// </summary>
    public string MailCode { get; set; }

        /// <summary>
    /// 邮件主题
    /// </summary>
    public string MailSubject { get; set; }

        /// <summary>
    /// 邮件内容
    /// </summary>
    public string MailContent { get; set; }

        /// <summary>
    /// 邮件类型
    /// </summary>
    public int MailType { get; set; }

        /// <summary>
    /// 发送人ID
    /// </summary>
    public long SenderId { get; set; }

        /// <summary>
    /// 发送人姓名
    /// </summary>
    public string SenderName { get; set; }

        /// <summary>
    /// 发送人邮箱
    /// </summary>
    public string? SenderEmail { get; set; }

        /// <summary>
    /// 收件人列表
    /// </summary>
    public string RecipientList { get; set; }

        /// <summary>
    /// 抄送人列表
    /// </summary>
    public string? CcList { get; set; }

        /// <summary>
    /// 密送人列表
    /// </summary>
    public string? BccList { get; set; }

        /// <summary>
    /// 是否重要
    /// </summary>
    public int IsImportant { get; set; }

        /// <summary>
    /// 是否紧急
    /// </summary>
    public int IsUrgent { get; set; }

        /// <summary>
    /// 是否需要回执
    /// </summary>
    public int IsReadReceiptRequired { get; set; }

        /// <summary>
    /// 是否已读回执
    /// </summary>
    public int IsReadReceiptSent { get; set; }

        /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime? SendTime { get; set; }

        /// <summary>
    /// 定时发送时间
    /// </summary>
    public DateTime? ScheduledSendTime { get; set; }

        /// <summary>
    /// 邮件状态
    /// </summary>
    public int MailStatus { get; set; }

        /// <summary>
    /// 发送失败原因
    /// </summary>
    public string? SendFailureReason { get; set; }

        /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 邮件表导入DTO
/// </summary>
public partial class TaktMailImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailImportDto()
    {
        MailCode = string.Empty;
        MailSubject = string.Empty;
        MailContent = string.Empty;
        SenderName = string.Empty;
        RecipientList = string.Empty;
    }

        /// <summary>
    /// 邮件编码
    /// </summary>
    public string MailCode { get; set; }

        /// <summary>
    /// 邮件主题
    /// </summary>
    public string MailSubject { get; set; }

        /// <summary>
    /// 邮件内容
    /// </summary>
    public string MailContent { get; set; }

        /// <summary>
    /// 邮件类型
    /// </summary>
    public int MailType { get; set; }

        /// <summary>
    /// 发送人ID
    /// </summary>
    public long SenderId { get; set; }

        /// <summary>
    /// 发送人姓名
    /// </summary>
    public string SenderName { get; set; }

        /// <summary>
    /// 发送人邮箱
    /// </summary>
    public string? SenderEmail { get; set; }

        /// <summary>
    /// 收件人列表
    /// </summary>
    public string RecipientList { get; set; }

        /// <summary>
    /// 抄送人列表
    /// </summary>
    public string? CcList { get; set; }

        /// <summary>
    /// 密送人列表
    /// </summary>
    public string? BccList { get; set; }

        /// <summary>
    /// 是否重要
    /// </summary>
    public int IsImportant { get; set; }

        /// <summary>
    /// 是否紧急
    /// </summary>
    public int IsUrgent { get; set; }

        /// <summary>
    /// 是否需要回执
    /// </summary>
    public int IsReadReceiptRequired { get; set; }

        /// <summary>
    /// 是否已读回执
    /// </summary>
    public int IsReadReceiptSent { get; set; }

        /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime? SendTime { get; set; }

        /// <summary>
    /// 定时发送时间
    /// </summary>
    public DateTime? ScheduledSendTime { get; set; }

        /// <summary>
    /// 邮件状态
    /// </summary>
    public int MailStatus { get; set; }

        /// <summary>
    /// 发送失败原因
    /// </summary>
    public string? SendFailureReason { get; set; }

        /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 邮件表导出DTO
/// </summary>
public partial class TaktMailExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailExportDto()
    {
        CreatedAt = DateTime.Now;
        MailCode = string.Empty;
        MailSubject = string.Empty;
        MailContent = string.Empty;
        SenderName = string.Empty;
        RecipientList = string.Empty;
    }

        /// <summary>
    /// 邮件编码
    /// </summary>
    public string MailCode { get; set; }

        /// <summary>
    /// 邮件主题
    /// </summary>
    public string MailSubject { get; set; }

        /// <summary>
    /// 邮件内容
    /// </summary>
    public string MailContent { get; set; }

        /// <summary>
    /// 邮件类型
    /// </summary>
    public int MailType { get; set; }

        /// <summary>
    /// 发送人ID
    /// </summary>
    public long SenderId { get; set; }

        /// <summary>
    /// 发送人姓名
    /// </summary>
    public string SenderName { get; set; }

        /// <summary>
    /// 发送人邮箱
    /// </summary>
    public string? SenderEmail { get; set; }

        /// <summary>
    /// 收件人列表
    /// </summary>
    public string RecipientList { get; set; }

        /// <summary>
    /// 抄送人列表
    /// </summary>
    public string? CcList { get; set; }

        /// <summary>
    /// 密送人列表
    /// </summary>
    public string? BccList { get; set; }

        /// <summary>
    /// 是否重要
    /// </summary>
    public int IsImportant { get; set; }

        /// <summary>
    /// 是否紧急
    /// </summary>
    public int IsUrgent { get; set; }

        /// <summary>
    /// 是否需要回执
    /// </summary>
    public int IsReadReceiptRequired { get; set; }

        /// <summary>
    /// 是否已读回执
    /// </summary>
    public int IsReadReceiptSent { get; set; }

        /// <summary>
    /// 发送时间
    /// </summary>
    public DateTime? SendTime { get; set; }

        /// <summary>
    /// 定时发送时间
    /// </summary>
    public DateTime? ScheduledSendTime { get; set; }

        /// <summary>
    /// 邮件状态
    /// </summary>
    public int MailStatus { get; set; }

        /// <summary>
    /// 发送失败原因
    /// </summary>
    public string? SendFailureReason { get; set; }

        /// <summary>
    /// 附件数量
    /// </summary>
    public int AttachmentCount { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}