// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.Files
// 文件名称：TaktFileDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：文件表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.Files;

/// <summary>
/// 文件表Dto
/// </summary>
public partial class TaktFileDto : TaktDtosEntityBase
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
    }

    /// <summary>
    /// 文件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件编码
    /// </summary>
    public string FileCode { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }
    /// <summary>
    /// 文件原始名称
    /// </summary>
    public string FileOriginalName { get; set; }
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
    /// 文件哈希值
    /// </summary>
    public string? FileHash { get; set; }
    /// <summary>
    /// 文件分类
    /// </summary>
    public int FileCategory { get; set; }
    /// <summary>
    /// 存储方式
    /// </summary>
    public int StorageType { get; set; }
    /// <summary>
    /// 存储配置
    /// </summary>
    public string? StorageConfig { get; set; }
    /// <summary>
    /// 访问地址
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
    /// 文件状态
    /// </summary>
    public int FileStatus { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }
    /// <summary>
    /// 访问权限配置
    /// </summary>
    public string? AccessPermissionConfig { get; set; }
    /// <summary>
    /// 文件描述
    /// </summary>
    public string? FileDescription { get; set; }
    /// <summary>
    /// 文件标签
    /// </summary>
    public string? FileTags { get; set; }
    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }
    /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }
}

/// <summary>
/// 文件表查询DTO
/// </summary>
public partial class TaktFileQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 文件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件编码
    /// </summary>
    public string? FileCode { get; set; }
    /// <summary>
    /// 文件名称
    /// </summary>
    public string? FileName { get; set; }
    /// <summary>
    /// 文件原始名称
    /// </summary>
    public string? FileOriginalName { get; set; }
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
    /// 文件哈希值
    /// </summary>
    public string? FileHash { get; set; }
    /// <summary>
    /// 文件分类
    /// </summary>
    public int? FileCategory { get; set; }
    /// <summary>
    /// 存储方式
    /// </summary>
    public int? StorageType { get; set; }
    /// <summary>
    /// 存储配置
    /// </summary>
    public string? StorageConfig { get; set; }
    /// <summary>
    /// 访问地址
    /// </summary>
    public string? AccessUrl { get; set; }
    /// <summary>
    /// 下载次数
    /// </summary>
    public int? DownloadCount { get; set; }
    /// <summary>
    /// 最后下载时间
    /// </summary>
    public DateTime? LastDownloadTime { get; set; }

    /// <summary>
    /// 最后下载时间开始时间
    /// </summary>
    public DateTime? LastDownloadTimeStart { get; set; }
    /// <summary>
    /// 最后下载时间结束时间
    /// </summary>
    public DateTime? LastDownloadTimeEnd { get; set; }
    /// <summary>
    /// 文件状态
    /// </summary>
    public int? FileStatus { get; set; }
    /// <summary>
    /// 是否公开
    /// </summary>
    public int? IsPublic { get; set; }
    /// <summary>
    /// 访问权限配置
    /// </summary>
    public string? AccessPermissionConfig { get; set; }
    /// <summary>
    /// 文件描述
    /// </summary>
    public string? FileDescription { get; set; }
    /// <summary>
    /// 文件标签
    /// </summary>
    public string? FileTags { get; set; }
    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }
    /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

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
/// Takt创建文件表DTO
/// </summary>
public partial class TaktFileCreateDto
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
    /// 文件编码
    /// </summary>
    public string FileCode { get; set; }

        /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; }

        /// <summary>
    /// 文件原始名称
    /// </summary>
    public string FileOriginalName { get; set; }

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
    /// 文件哈希值
    /// </summary>
    public string? FileHash { get; set; }

        /// <summary>
    /// 文件分类
    /// </summary>
    public int FileCategory { get; set; }

        /// <summary>
    /// 存储方式
    /// </summary>
    public int StorageType { get; set; }

        /// <summary>
    /// 存储配置
    /// </summary>
    public string? StorageConfig { get; set; }

        /// <summary>
    /// 访问地址
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
    /// 文件状态
    /// </summary>
    public int FileStatus { get; set; }

        /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }

        /// <summary>
    /// 访问权限配置
    /// </summary>
    public string? AccessPermissionConfig { get; set; }

        /// <summary>
    /// 文件描述
    /// </summary>
    public string? FileDescription { get; set; }

        /// <summary>
    /// 文件标签
    /// </summary>
    public string? FileTags { get; set; }

        /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

        /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

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
/// Takt更新文件表DTO
/// </summary>
public partial class TaktFileUpdateDto : TaktFileCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileUpdateDto()
    {
    }

        /// <summary>
    /// 文件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }
}

/// <summary>
/// 文件表文件状态DTO
/// </summary>
public partial class TaktFileStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileStatusDto()
    {
    }

        /// <summary>
    /// 文件表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 文件状态（0=禁用，1=启用）
    /// </summary>
    public int FileStatus { get; set; }
}

/// <summary>
/// 文件表导入模板DTO
/// </summary>
public partial class TaktFileTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileTemplateDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FileOriginalName = string.Empty;
        FilePath = string.Empty;
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
    /// 文件原始名称
    /// </summary>
    public string FileOriginalName { get; set; }

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
    /// 文件哈希值
    /// </summary>
    public string? FileHash { get; set; }

        /// <summary>
    /// 文件分类
    /// </summary>
    public int FileCategory { get; set; }

        /// <summary>
    /// 存储方式
    /// </summary>
    public int StorageType { get; set; }

        /// <summary>
    /// 存储配置
    /// </summary>
    public string? StorageConfig { get; set; }

        /// <summary>
    /// 访问地址
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
    /// 文件状态
    /// </summary>
    public int FileStatus { get; set; }

        /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }

        /// <summary>
    /// 访问权限配置
    /// </summary>
    public string? AccessPermissionConfig { get; set; }

        /// <summary>
    /// 文件描述
    /// </summary>
    public string? FileDescription { get; set; }

        /// <summary>
    /// 文件标签
    /// </summary>
    public string? FileTags { get; set; }

        /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

        /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

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
/// 文件表导入DTO
/// </summary>
public partial class TaktFileImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileImportDto()
    {
        FileCode = string.Empty;
        FileName = string.Empty;
        FileOriginalName = string.Empty;
        FilePath = string.Empty;
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
    /// 文件原始名称
    /// </summary>
    public string FileOriginalName { get; set; }

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
    /// 文件哈希值
    /// </summary>
    public string? FileHash { get; set; }

        /// <summary>
    /// 文件分类
    /// </summary>
    public int FileCategory { get; set; }

        /// <summary>
    /// 存储方式
    /// </summary>
    public int StorageType { get; set; }

        /// <summary>
    /// 存储配置
    /// </summary>
    public string? StorageConfig { get; set; }

        /// <summary>
    /// 访问地址
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
    /// 文件状态
    /// </summary>
    public int FileStatus { get; set; }

        /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }

        /// <summary>
    /// 访问权限配置
    /// </summary>
    public string? AccessPermissionConfig { get; set; }

        /// <summary>
    /// 文件描述
    /// </summary>
    public string? FileDescription { get; set; }

        /// <summary>
    /// 文件标签
    /// </summary>
    public string? FileTags { get; set; }

        /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

        /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

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
/// 文件表导出DTO
/// </summary>
public partial class TaktFileExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFileExportDto()
    {
        CreatedAt = DateTime.Now;
        FileCode = string.Empty;
        FileName = string.Empty;
        FileOriginalName = string.Empty;
        FilePath = string.Empty;
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
    /// 文件原始名称
    /// </summary>
    public string FileOriginalName { get; set; }

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
    /// 文件哈希值
    /// </summary>
    public string? FileHash { get; set; }

        /// <summary>
    /// 文件分类
    /// </summary>
    public int FileCategory { get; set; }

        /// <summary>
    /// 存储方式
    /// </summary>
    public int StorageType { get; set; }

        /// <summary>
    /// 存储配置
    /// </summary>
    public string? StorageConfig { get; set; }

        /// <summary>
    /// 访问地址
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
    /// 文件状态
    /// </summary>
    public int FileStatus { get; set; }

        /// <summary>
    /// 是否公开
    /// </summary>
    public int IsPublic { get; set; }

        /// <summary>
    /// 访问权限配置
    /// </summary>
    public string? AccessPermissionConfig { get; set; }

        /// <summary>
    /// 文件描述
    /// </summary>
    public string? FileDescription { get; set; }

        /// <summary>
    /// 文件标签
    /// </summary>
    public string? FileTags { get; set; }

        /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

        /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}