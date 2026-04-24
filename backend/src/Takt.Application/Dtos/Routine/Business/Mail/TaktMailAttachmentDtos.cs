// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.Mail
// 文件名称：TaktMailAttachmentDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：邮件附件表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.Mail;

/// <summary>
/// 邮件附件表Dto
/// </summary>
public partial class TaktMailAttachmentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailAttachmentDto()
    {
        FileName = string.Empty;
        FilePath = string.Empty;
    }

    /// <summary>
    /// 邮件附件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailAttachmentId { get; set; }

    /// <summary>
    /// 邮件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailId { get; set; }
    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }
    /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }
    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }
    /// <summary>
    /// 文件类型
    /// </summary>
    public string? FileType { get; set; }
    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }
}

/// <summary>
/// 邮件附件表查询DTO
/// </summary>
public partial class TaktMailAttachmentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailAttachmentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 邮件附件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailAttachmentId { get; set; }

    /// <summary>
    /// 邮件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? MailId { get; set; }
    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FileId { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }
    /// <summary>
    /// 文件路径
    /// </summary>
    public string? FilePath { get; set; }
    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long? FileSize { get; set; }
    /// <summary>
    /// 文件类型
    /// </summary>
    public string? FileType { get; set; }
    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }
    /// <summary>
    /// 排序号
    /// </summary>
    public int? SortOrder { get; set; }

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
/// Takt创建邮件附件表DTO
/// </summary>
public partial class TaktMailAttachmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailAttachmentCreateDto()
    {
        FileName = string.Empty;
        FilePath = string.Empty;
    }

        /// <summary>
    /// 邮件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailId { get; set; }

        /// <summary>
    /// 文件ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }

        /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

        /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

        /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

        /// <summary>
    /// 文件类型
    /// </summary>
    public string? FileType { get; set; }

        /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// Takt更新邮件附件表DTO
/// </summary>
public partial class TaktMailAttachmentUpdateDto : TaktMailAttachmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailAttachmentUpdateDto()
    {
    }

        /// <summary>
    /// 邮件附件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long MailAttachmentId { get; set; }
}

/// <summary>
/// 邮件附件表导入模板DTO
/// </summary>
public partial class TaktMailAttachmentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailAttachmentTemplateDto()
    {
        FileName = string.Empty;
        FilePath = string.Empty;
    }

        /// <summary>
    /// 邮件ID
    /// </summary>
    public long MailId { get; set; }

        /// <summary>
    /// 文件ID
    /// </summary>
    public long FileId { get; set; }

        /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

        /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

        /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

        /// <summary>
    /// 文件类型
    /// </summary>
    public string? FileType { get; set; }

        /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 邮件附件表导入DTO
/// </summary>
public partial class TaktMailAttachmentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailAttachmentImportDto()
    {
        FileName = string.Empty;
        FilePath = string.Empty;
    }

        /// <summary>
    /// 邮件ID
    /// </summary>
    public long MailId { get; set; }

        /// <summary>
    /// 文件ID
    /// </summary>
    public long FileId { get; set; }

        /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

        /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

        /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

        /// <summary>
    /// 文件类型
    /// </summary>
    public string? FileType { get; set; }

        /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

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
/// 邮件附件表导出DTO
/// </summary>
public partial class TaktMailAttachmentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMailAttachmentExportDto()
    {
        CreatedAt = DateTime.Now;
        FileName = string.Empty;
        FilePath = string.Empty;
    }

        /// <summary>
    /// 邮件ID
    /// </summary>
    public long MailId { get; set; }

        /// <summary>
    /// 文件ID
    /// </summary>
    public long FileId { get; set; }

        /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

        /// <summary>
    /// 文件路径
    /// </summary>
    public string FilePath { get; set; }

        /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

        /// <summary>
    /// 文件类型
    /// </summary>
    public string? FileType { get; set; }

        /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

        /// <summary>
    /// 排序号
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}