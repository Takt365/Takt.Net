// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Tasks.Files
// 文件名称：TaktFilesSpecific.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：特殊DTO集合，包含业务特定的数据传输对象（如头像更新、密码重置、用户解锁等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Tasks.Files;

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
/// Takt文件公开状态变更DTO
/// </summary>
public class TaktFilePublicChangeDto
{
    /// <summary>
    /// 文件ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FileId { get; set; }

    /// <summary>
    /// 是否公开（0=否，1=是）
    /// </summary>
    public int IsPublic { get; set; }
}

/// <summary>
/// Takt文件DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFileDto
{
    /// <summary>
    /// 下载URL（非数据库字段，用于展示）
    /// </summary>
    public string? DownloadUrl { get; set; }
}

/// <summary>
/// Takt文件导出DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFileExportDto
{
    /// <summary>
    /// 文件分类字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? FileCategoryString { get; set; }
    
    /// <summary>
    /// 存储类型字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? StorageTypeString { get; set; }
    
    /// <summary>
    /// 文件状态字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? FileStatusString { get; set; }
    
    /// <summary>
    /// 是否公开字符串（非数据库字段，用于Excel导出显示）
    /// </summary>
    public string? IsPublicString { get; set; }
}