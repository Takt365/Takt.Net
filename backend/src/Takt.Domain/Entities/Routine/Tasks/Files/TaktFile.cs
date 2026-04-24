// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Routine.Files
// 文件名称：TaktFile.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt文件实体，定义文件领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Tasks.Files;

/// <summary>
/// Takt文件实体
/// </summary>
[SugarTable("takt_routine_files_file", "文件表")]
[SugarIndex("ix_takt_routine_files_file_code", nameof(FileCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_files_file_hash", nameof(FileHash), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_files_file_category", nameof(FileCategory), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_files_file_status", nameof(FileStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_files_file_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_files_file_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFile : TaktEntityBase
{
    /// <summary>
    /// 文件编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "file_code", ColumnDescription = "文件编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string FileCode { get; set; } = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    [SugarColumn(ColumnName = "file_name", ColumnDescription = "文件名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 文件原始名称（上传时的原始文件名）
    /// </summary>
    [SugarColumn(ColumnName = "file_original_name", ColumnDescription = "文件原始名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string FileOriginalName { get; set; } = string.Empty;

    /// <summary>
    /// 文件路径（相对路径或完整路径）
    /// </summary>
    [SugarColumn(ColumnName = "file_path", ColumnDescription = "文件路径", ColumnDataType = "nvarchar", Length = 500, IsNullable = false)]
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// 文件大小（字节）
    /// </summary>
    [SugarColumn(ColumnName = "file_size", ColumnDescription = "文件大小（字节）", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    public long FileSize { get; set; } = 0;

    /// <summary>
    /// 文件类型（MIME类型）
    /// </summary>
    [SugarColumn(ColumnName = "file_type", ColumnDescription = "文件类型", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? FileType { get; set; }

    /// <summary>
    /// 文件扩展名
    /// </summary>
    [SugarColumn(ColumnName = "file_extension", ColumnDescription = "文件扩展名", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? FileExtension { get; set; }

    /// <summary>
    /// 文件哈希值（MD5或SHA256，用于去重和校验）
    /// </summary>
    [SugarColumn(ColumnName = "file_hash", ColumnDescription = "文件哈希值", ColumnDataType = "nvarchar", Length = 64, IsNullable = true)]
    public string? FileHash { get; set; }

    /// <summary>
    /// 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "file_category", ColumnDescription = "文件分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
    public int FileCategory { get; set; } = 5;

    /// <summary>
    /// 存储方式（0=本地存储，1=OSS对象存储，2=FTP，3=其他）
    /// </summary>
    [SugarColumn(ColumnName = "storage_type", ColumnDescription = "存储方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int StorageType { get; set; } = 0;

    /// <summary>
    /// 存储配置（JSON格式，存储OSS配置、FTP配置等）
    /// </summary>
    [SugarColumn(ColumnName = "storage_config", ColumnDescription = "存储配置", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? StorageConfig { get; set; }

    /// <summary>
    /// 访问地址（文件的访问URL地址）
    /// </summary>
    [SugarColumn(ColumnName = "access_url", ColumnDescription = "访问地址", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? AccessUrl { get; set; }

    /// <summary>
    /// 下载次数
    /// </summary>
    [SugarColumn(ColumnName = "download_count", ColumnDescription = "下载次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DownloadCount { get; set; } = 0;

    /// <summary>
    /// 最后下载时间
    /// </summary>
    [SugarColumn(ColumnName = "last_download_time", ColumnDescription = "最后下载时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LastDownloadTime { get; set; }

    /// <summary>
    /// 文件状态（0=正常，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "file_status", ColumnDescription = "文件状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FileStatus { get; set; } = 0;

    /// <summary>
    /// 是否公开（0=公开，1=私有，默认为0）
    /// </summary>
    [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsPublic { get; set; } = 0;

    /// <summary>
    /// 访问权限配置（JSON格式，存储用户ID列表、部门ID列表或角色ID列表）
    /// </summary>
    [SugarColumn(ColumnName = "access_permission_config", ColumnDescription = "访问权限配置", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? AccessPermissionConfig { get; set; }

    /// <summary>
    /// 文件描述
    /// </summary>
    [SugarColumn(ColumnName = "file_description", ColumnDescription = "文件描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? FileDescription { get; set; }

    /// <summary>
    /// 文件标签（多个标签用逗号分隔）
    /// </summary>
    [SugarColumn(ColumnName = "file_tags", ColumnDescription = "文件标签", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? FileTags { get; set; }

    /// <summary>
    /// IP地址（上传或访问文件的IP地址）
    /// </summary>
    [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// 位置（IP地址对应的地理位置信息）
    /// </summary>
    [SugarColumn(ColumnName = "location", ColumnDescription = "位置", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Location { get; set; }
}
