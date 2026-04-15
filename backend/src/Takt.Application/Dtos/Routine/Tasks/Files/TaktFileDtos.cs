// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Dtos.Routine.Files
// 文件名称：TaktFileDtos.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt文件DTO，包含文件相关的数据传输对象（查询、创建、更新）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.Files;

/// <summary>
/// Takt文件DTO
/// </summary>
public class TaktFileDto : TaktDtoBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FileOriginalName = string.Empty;
        FilePath = string.Empty;
        ConfigId = "0";
    }

    /// <summary>
    /// 文件ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件编码（唯一索引）
    /// </summary>
    public string FileCode { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件原始名称（上传时的原始文件名）
    /// </summary>
    public string FileOriginalName { get; set; }

    /// <summary>
    /// 文件路径（相对路径或完整路径）
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    public string? FileType { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

    /// <summary>
    /// 文件哈希值（MD5或SHA256，用于去重和校验）
    /// </summary>
    public string? FileHash { get; set; }

    /// <summary>
    /// 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他）
    /// </summary>
    public int FileCategory { get; set; }

    /// <summary>
    /// 存储方式（0=本地存储，1=OSS对象存储，2=FTP，3=其他）
    /// </summary>
    public int StorageType { get; set; }

    /// <summary>
    /// 存储配置（JSON格式，存储OSS配置、FTP配置等）
    /// </summary>
    public string? StorageConfig { get; set; }

    /// <summary>
    /// 访问地址（文件的访问URL地址）
    /// </summary>
    public string? AccessUrl { get; set; }

    /// <summary>
    /// 下载次数
    /// </summary>
    public int DownloadCount { get; set; }

    /// <summary>
    /// 最后下载时间
    /// </summary>
    public DateTime? LastDownloadTime { get; set; }

    /// <summary>
    /// 文件状态（0=正常，1=禁用）
    /// </summary>
    public int FileStatus { get; set; }

    /// <summary>
    /// 是否公开（0=公开，1=私有）
    /// </summary>
    public int IsPublic { get; set; }

    /// <summary>
    /// 访问权限配置（JSON格式，存储用户ID列表、部门ID列表或角色ID列表）
    /// </summary>
    public string? AccessPermissionConfig { get; set; }

    /// <summary>
    /// 文件描述
    /// </summary>
    public string? FileDescription { get; set; }

    /// <summary>
    /// 文件标签（多个标签用逗号分隔）
    /// </summary>
    public string? FileTags { get; set; }

    /// <summary>
    /// IP地址（上传或访问文件的IP地址）
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 位置（IP地址对应的地理位置信息）
    /// </summary>
    public string? Location { get; set; }
}

/// <summary>
/// Takt文件查询DTO
/// </summary>
public class TaktFileQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于在文件编码、文件名称、文件原始名称中模糊查询

    /// <summary>
    /// 文件编码
    /// </summary>
    public string? FileCode { get; set; }

    /// <summary>
    /// 文件名称（模糊查询）
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他）
    /// </summary>
    public int? FileCategory { get; set; }

    /// <summary>
    /// 存储方式（0=本地存储，1=OSS对象存储，2=FTP，3=其他）
    /// </summary>
    public int? StorageType { get; set; }

    /// <summary>
    /// 文件状态（0=正常，1=禁用）
    /// </summary>
    public int? FileStatus { get; set; }

    /// <summary>
    /// 创建开始时间（上传开始时间）
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }

    /// <summary>
    /// 创建结束时间（上传结束时间）
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

    /// <summary>
    /// 是否公开（0=公开，1=私有）
    /// </summary>
    public int? IsPublic { get; set; }
}

/// <summary>
/// Takt文件创建DTO
/// </summary>
public class TaktFileCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileCreateDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FileOriginalName = string.Empty;
        FilePath = string.Empty;
    }

    /// <summary>
    /// 文件编码（唯一索引）
    /// </summary>
    public string FileCode { get; set; } = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 文件原始名称（上传时的原始文件名）
    /// </summary>
    public string FileOriginalName { get; set; } = string.Empty;

    /// <summary>
    /// 文件路径（相对路径或完整路径）
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    public string? FileType { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

    /// <summary>
    /// 文件哈希值（MD5或SHA256，用于去重和校验）
    /// </summary>
    public string? FileHash { get; set; }

    /// <summary>
    /// 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他）
    /// </summary>
    public int FileCategory { get; set; } = 5;

    /// <summary>
    /// 存储方式（0=本地存储，1=OSS对象存储，2=FTP，3=其他）
    /// </summary>
    public int StorageType { get; set; } = 0;

    /// <summary>
    /// 存储配置（JSON格式，存储OSS配置、FTP配置等）
    /// </summary>
    public string? StorageConfig { get; set; }

    /// <summary>
    /// 访问地址（文件的访问URL地址）
    /// </summary>
    public string? AccessUrl { get; set; }

    /// <summary>
    /// 是否公开（0=公开，1=私有）
    /// </summary>
    public int IsPublic { get; set; } = 0;

    /// <summary>
    /// 访问权限配置（JSON格式，存储用户ID列表、部门ID列表或角色ID列表）
    /// </summary>
    public string? AccessPermissionConfig { get; set; }

    /// <summary>
    /// 文件描述
    /// </summary>
    public string? FileDescription { get; set; }

    /// <summary>
    /// 文件标签（多个标签用逗号分隔）
    /// </summary>
    public string? FileTags { get; set; }

    /// <summary>
    /// 备注（与 FileDescription 同级，创建/更新时一并持久化）
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// IP地址（上传或访问文件的IP地址）
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 位置（IP地址对应的地理位置信息）
    /// </summary>
    public string? Location { get; set; }
}

/// <summary>
/// Takt文件更新DTO
/// </summary>
public class TaktFileUpdateDto : TaktFileCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileUpdateDto()
    {
    }

    /// <summary>
    /// 文件ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }
}

/// <summary>
/// Takt文件状态DTO
/// </summary>
public class TaktFileStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileStatusDto()
    {
    }

    /// <summary>
    /// 文件ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件状态（0=正常，1=禁用）
    /// </summary>
    public int FileStatus { get; set; }
}

/// <summary>
/// Takt文件增加下载次数DTO
/// </summary>
public class TaktFileIncrementDownloadCountDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileIncrementDownloadCountDto()
    {
    }

    /// <summary>
    /// 文件ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }
}

/// <summary>
/// Takt文件切换DTO（用于切换公开/私有状态）
/// </summary>
public class TaktFileChangeDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileChangeDto()
    {
    }

    /// <summary>
    /// 文件ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 是否公开（0=公开，1=私有）
    /// </summary>
    public int IsPublic { get; set; }
}

/// <summary>
/// Takt文件导出DTO
/// </summary>
public class TaktFileExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileExportDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FileOriginalName = string.Empty;
        FilePath = string.Empty;
        FileCategory = string.Empty;
        StorageType = string.Empty;
        FileStatus = string.Empty;
        IsPublic = string.Empty;
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// 文件编码
    /// </summary>
    public string FileCode { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// 文件原始名称（上传时的原始文件名）
    /// </summary>
    public string FileOriginalName { get; set; }

    /// <summary>
    /// 文件路径（相对路径或完整路径）
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    public string? FileType { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    public string? FileExtension { get; set; }

    /// <summary>
    /// 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他）
    /// </summary>
    public string FileCategory { get; set; }

    /// <summary>
    /// 存储方式（0=本地存储，1=OSS对象存储，2=FTP，3=其他）
    /// </summary>
    public string StorageType { get; set; }

    /// <summary>
    /// 下载次数
    /// </summary>
    public int DownloadCount { get; set; }

    /// <summary>
    /// 文件状态（0=正常，1=禁用）
    /// </summary>
    public string FileStatus { get; set; }

    /// <summary>
    /// 是否公开（0=公开，1=私有）
    /// </summary>
    public string IsPublic { get; set; }

    /// <summary>
    /// 文件描述
    /// </summary>
    public string? FileDescription { get; set; }

    /// <summary>
    /// 文件标签（多个标签用逗号分隔）
    /// </summary>
    public string? FileTags { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
