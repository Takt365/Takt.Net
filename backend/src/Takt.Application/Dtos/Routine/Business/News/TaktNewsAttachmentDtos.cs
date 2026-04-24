// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.News
// 文件名称：TaktNewsAttachmentDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：新闻附件表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.News;

/// <summary>
/// 新闻附件表Dto
/// </summary>
public partial class TaktNewsAttachmentDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsAttachmentDto()
    {
        FileName = string.Empty;
        FilePath = string.Empty;
    }

    /// <summary>
    /// 新闻附件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsAttachmentId { get; set; }

    /// <summary>
    /// 新闻ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }
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
/// 新闻附件表查询DTO
/// </summary>
public partial class TaktNewsAttachmentQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsAttachmentQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 新闻附件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsAttachmentId { get; set; }

    /// <summary>
    /// 新闻ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? NewsId { get; set; }
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
/// Takt创建新闻附件表DTO
/// </summary>
public partial class TaktNewsAttachmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsAttachmentCreateDto()
    {
        FileName = string.Empty;
        FilePath = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsId { get; set; }

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
/// Takt更新新闻附件表DTO
/// </summary>
public partial class TaktNewsAttachmentUpdateDto : TaktNewsAttachmentCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsAttachmentUpdateDto()
    {
    }

        /// <summary>
    /// 新闻附件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long NewsAttachmentId { get; set; }
}

/// <summary>
/// 新闻附件表导入模板DTO
/// </summary>
public partial class TaktNewsAttachmentTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsAttachmentTemplateDto()
    {
        FileName = string.Empty;
        FilePath = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    public long NewsId { get; set; }

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
/// 新闻附件表导入DTO
/// </summary>
public partial class TaktNewsAttachmentImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsAttachmentImportDto()
    {
        FileName = string.Empty;
        FilePath = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    public long NewsId { get; set; }

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
/// 新闻附件表导出DTO
/// </summary>
public partial class TaktNewsAttachmentExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsAttachmentExportDto()
    {
        CreatedAt = DateTime.Now;
        FileName = string.Empty;
        FilePath = string.Empty;
    }

        /// <summary>
    /// 新闻ID
    /// </summary>
    public long NewsId { get; set; }

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