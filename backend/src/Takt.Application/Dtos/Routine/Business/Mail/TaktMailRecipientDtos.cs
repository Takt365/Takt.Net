// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.Mail
// 文件名称：TaktMailRecipientDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：邮件收件人表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.Mail;

/// <summary>
/// 邮件收件人表Dto
/// </summary>
public partial class TaktMailRecipientDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailRecipientDto()
    {
        RecipientName = string.Empty;
    }

    /// <summary>
    /// 邮件收件人表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailRecipientId { get; set; }

    /// <summary>
    /// 邮件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailId { get; set; }
    /// <summary>
    /// 收件人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RecipientId { get; set; }
    /// <summary>
    /// 收件人姓名
    /// </summary>
    public string RecipientName { get; set; }
    /// <summary>
    /// 收件人邮箱
    /// </summary>
    public string? RecipientEmail { get; set; }
    /// <summary>
    /// 收件人类型
    /// </summary>
    public int RecipientType { get; set; }
    /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }
    /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }
    /// <summary>
    /// 是否已删除
    /// </summary>
    public int IsRecipientDeleted { get; set; }
    /// <summary>
    /// 是否已收藏
    /// </summary>
    public int IsStarred { get; set; }
    /// <summary>
    /// 是否已标记
    /// </summary>
    public int IsFlagged { get; set; }
}

/// <summary>
/// 邮件收件人表查询DTO
/// </summary>
public partial class TaktMailRecipientQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailRecipientQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 邮件收件人表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailRecipientId { get; set; }

    /// <summary>
    /// 邮件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? MailId { get; set; }
    /// <summary>
    /// 收件人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? RecipientId { get; set; }
    /// <summary>
    /// 收件人姓名
    /// </summary>
    public string? RecipientName { get; set; }
    /// <summary>
    /// 收件人邮箱
    /// </summary>
    public string? RecipientEmail { get; set; }
    /// <summary>
    /// 收件人类型
    /// </summary>
    public int? RecipientType { get; set; }
    /// <summary>
    /// 读取状态
    /// </summary>
    public int? ReadStatus { get; set; }
    /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

    /// <summary>
    /// 读取时间开始时间
    /// </summary>
    public DateTime? ReadTimeStart { get; set; }
    /// <summary>
    /// 读取时间结束时间
    /// </summary>
    public DateTime? ReadTimeEnd { get; set; }
    /// <summary>
    /// 是否已删除
    /// </summary>
    public int? IsRecipientDeleted { get; set; }
    /// <summary>
    /// 是否已收藏
    /// </summary>
    public int? IsStarred { get; set; }
    /// <summary>
    /// 是否已标记
    /// </summary>
    public int? IsFlagged { get; set; }

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
/// Takt创建邮件收件人表DTO
/// </summary>
public partial class TaktMailRecipientCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailRecipientCreateDto()
    {
        RecipientName = string.Empty;
    }

        /// <summary>
    /// 邮件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailId { get; set; }

        /// <summary>
    /// 收件人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long RecipientId { get; set; }

        /// <summary>
    /// 收件人姓名
    /// </summary>
    public string RecipientName { get; set; }

        /// <summary>
    /// 收件人邮箱
    /// </summary>
    public string? RecipientEmail { get; set; }

        /// <summary>
    /// 收件人类型
    /// </summary>
    public int RecipientType { get; set; }

        /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }

        /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

        /// <summary>
    /// 是否已删除
    /// </summary>
    public int IsRecipientDeleted { get; set; }

        /// <summary>
    /// 是否已收藏
    /// </summary>
    public int IsStarred { get; set; }

        /// <summary>
    /// 是否已标记
    /// </summary>
    public int IsFlagged { get; set; }

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
/// Takt更新邮件收件人表DTO
/// </summary>
public partial class TaktMailRecipientUpdateDto : TaktMailRecipientCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailRecipientUpdateDto()
    {
    }

        /// <summary>
    /// 邮件收件人表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailRecipientId { get; set; }
}

/// <summary>
/// 邮件收件人表读取状态DTO
/// </summary>
public partial class TaktMailRecipientReadStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailRecipientReadStatusDto()
    {
    }

        /// <summary>
    /// 邮件收件人表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailRecipientId { get; set; }

    /// <summary>
    /// 读取状态（0=禁用，1=启用）
    /// </summary>
    public int ReadStatus { get; set; }
}

/// <summary>
/// 邮件收件人表导入模板DTO
/// </summary>
public partial class TaktMailRecipientTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailRecipientTemplateDto()
    {
        RecipientName = string.Empty;
    }

        /// <summary>
    /// 邮件ID
    /// </summary>
    public long MailId { get; set; }

        /// <summary>
    /// 收件人ID
    /// </summary>
    public long RecipientId { get; set; }

        /// <summary>
    /// 收件人姓名
    /// </summary>
    public string RecipientName { get; set; }

        /// <summary>
    /// 收件人邮箱
    /// </summary>
    public string? RecipientEmail { get; set; }

        /// <summary>
    /// 收件人类型
    /// </summary>
    public int RecipientType { get; set; }

        /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }

        /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

        /// <summary>
    /// 是否已删除
    /// </summary>
    public int IsRecipientDeleted { get; set; }

        /// <summary>
    /// 是否已收藏
    /// </summary>
    public int IsStarred { get; set; }

        /// <summary>
    /// 是否已标记
    /// </summary>
    public int IsFlagged { get; set; }

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
/// 邮件收件人表导入DTO
/// </summary>
public partial class TaktMailRecipientImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailRecipientImportDto()
    {
        RecipientName = string.Empty;
    }

        /// <summary>
    /// 邮件ID
    /// </summary>
    public long MailId { get; set; }

        /// <summary>
    /// 收件人ID
    /// </summary>
    public long RecipientId { get; set; }

        /// <summary>
    /// 收件人姓名
    /// </summary>
    public string RecipientName { get; set; }

        /// <summary>
    /// 收件人邮箱
    /// </summary>
    public string? RecipientEmail { get; set; }

        /// <summary>
    /// 收件人类型
    /// </summary>
    public int RecipientType { get; set; }

        /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }

        /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

        /// <summary>
    /// 是否已删除
    /// </summary>
    public int IsRecipientDeleted { get; set; }

        /// <summary>
    /// 是否已收藏
    /// </summary>
    public int IsStarred { get; set; }

        /// <summary>
    /// 是否已标记
    /// </summary>
    public int IsFlagged { get; set; }

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
/// 邮件收件人表导出DTO
/// </summary>
public partial class TaktMailRecipientExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailRecipientExportDto()
    {
        CreatedAt = DateTime.Now;
        RecipientName = string.Empty;
    }

        /// <summary>
    /// 邮件ID
    /// </summary>
    public long MailId { get; set; }

        /// <summary>
    /// 收件人ID
    /// </summary>
    public long RecipientId { get; set; }

        /// <summary>
    /// 收件人姓名
    /// </summary>
    public string RecipientName { get; set; }

        /// <summary>
    /// 收件人邮箱
    /// </summary>
    public string? RecipientEmail { get; set; }

        /// <summary>
    /// 收件人类型
    /// </summary>
    public int RecipientType { get; set; }

        /// <summary>
    /// 读取状态
    /// </summary>
    public int ReadStatus { get; set; }

        /// <summary>
    /// 读取时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

        /// <summary>
    /// 是否已删除
    /// </summary>
    public int IsRecipientDeleted { get; set; }

        /// <summary>
    /// 是否已收藏
    /// </summary>
    public int IsStarred { get; set; }

        /// <summary>
    /// 是否已标记
    /// </summary>
    public int IsFlagged { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}