// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Files
// 文件名称：ITaktFileUploadService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt文件上传服务接口
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

namespace Takt.Application.Services.Routine.Tasks.Files.Engine;

/// <summary>
/// 文件上传类型
/// </summary>
public enum FileUploadType
{
    /// <summary>
    /// 头像
    /// </summary>
    Avatar = 0,

    /// <summary>
    /// 图片
    /// </summary>
    Image = 1,

    /// <summary>
    /// 文件
    /// </summary>
    File = 2
}

/// <summary>
/// 文件上传结果
/// </summary>
public class FileUploadResult
{
    /// <summary>
    /// 文件编码
    /// </summary>
    public string FileCode { get; set; } = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 文件原始名称
    /// </summary>
    public string FileOriginalName { get; set; } = string.Empty;

    /// <summary>
    /// 文件路径（相对路径）
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
    /// 文件哈希值
    /// </summary>
    public string? FileHash { get; set; }

    /// <summary>
    /// 文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包，5=其他）
    /// </summary>
    public int FileCategory { get; set; }
}

/// <summary>
/// Takt文件上传服务接口
/// </summary>
public interface ITaktFileUploadService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="fileName">文件名</param>
    /// <param name="fileType">文件上传类型</param>
    /// <param name="targetFileName">目标文件名（可选，如果提供则使用该名称保存，否则使用默认的 fileCode.扩展名）</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>文件上传结果</returns>
    Task<FileUploadResult> UploadAsync(Stream fileStream, string fileName, FileUploadType fileType, string? targetFileName = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量上传文件
    /// </summary>
    /// <param name="files">文件列表（文件流和文件名）</param>
    /// <param name="fileType">文件上传类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>文件上传结果列表</returns>
    Task<List<FileUploadResult>> UploadBatchAsync(List<(Stream stream, string fileName)> files, FileUploadType fileType, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath">文件路径（相对路径）</param>
    /// <returns>任务</returns>
    Task DeleteAsync(string filePath);
}
