// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Files
// 文件名称：TaktFileUploadService.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt文件上传服务，处理文件物理存储
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Takt.Shared.Helpers;

namespace Takt.Application.Services.Routine.Tasks.Files.Engine;

/// <summary>
/// Takt文件上传服务
/// </summary>
public class TaktFileUploadService : ITaktFileUploadService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _configuration;
    private readonly string _uploadBasePath;
    private readonly HashSet<string> _imageExtensions;
    private readonly HashSet<string> _videoExtensions;
    private readonly HashSet<string> _audioExtensions;
    private readonly HashSet<string> _archiveExtensions;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="webHostEnvironment">Web主机环境</param>
    /// <param name="configuration">配置</param>
    public TaktFileUploadService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        _webHostEnvironment = webHostEnvironment;
        _configuration = configuration;
        _uploadBasePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "upload");
        
        // 从配置文件读取文件分类
        _imageExtensions = new HashSet<string>(
            _configuration.GetSection("FileCategory:Image").Get<string[]>() ?? Array.Empty<string>(),
            StringComparer.OrdinalIgnoreCase);
        _videoExtensions = new HashSet<string>(
            _configuration.GetSection("FileCategory:Video").Get<string[]>() ?? Array.Empty<string>(),
            StringComparer.OrdinalIgnoreCase);
        _audioExtensions = new HashSet<string>(
            _configuration.GetSection("FileCategory:Audio").Get<string[]>() ?? Array.Empty<string>(),
            StringComparer.OrdinalIgnoreCase);
        _archiveExtensions = new HashSet<string>(
            _configuration.GetSection("FileCategory:Archive").Get<string[]>() ?? Array.Empty<string>(),
            StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="fileStream">文件流</param>
    /// <param name="fileName">文件名</param>
    /// <param name="fileType">文件上传类型</param>
    /// <param name="targetFileName">目标文件名（可选，如果提供则使用该名称保存，否则使用默认的 fileCode.扩展名）</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>文件上传结果</returns>
    public async Task<FileUploadResult> UploadAsync(Stream fileStream, string fileName, FileUploadType fileType, string? targetFileName = null, CancellationToken cancellationToken = default)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new ArgumentException("文件流不能为空", nameof(fileStream));

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("文件名不能为空", nameof(fileName));

        // 生成文件编码（使用GUID）
        var fileCode = Guid.NewGuid().ToString("N");

        // 获取文件扩展名
        var fileExtension = Path.GetExtension(fileName)?.TrimStart('.')?.ToLowerInvariant() ?? string.Empty;

        // 验证：不允许旧格式Office文件（2007以下版本）
        if (fileExtension == "doc" || fileExtension == "xls" || fileExtension == "ppt")
        {
            throw new ArgumentException($"不支持旧格式Office文件（.{fileExtension}），请使用新格式（.{fileExtension}x）", nameof(fileName));
        }

        // 获取MIME类型
        var fileMimeType = GetMimeType(fileExtension);

        // 计算文件哈希值（MD5）
        var fileHash = await ComputeFileHashAsync(fileStream);
        fileStream.Position = 0; // 重置流位置

        // 获取文件分类
        var fileCategory = GetFileCategory(fileExtension);

        // 构建上传路径
        var uploadPath = GetUploadPath(fileType);
        var datePath = GetDatePath();
        var fullUploadDir = Path.Combine(_uploadBasePath, uploadPath, datePath);

        // 确保目录存在（目录 API 无原生 async，转移到后台线程执行）
        await Task.Run(() => Directory.CreateDirectory(fullUploadDir), cancellationToken);

        // 确定最终文件名
        // 如果提供了 targetFileName，使用它；否则使用默认的 fileCode.扩展名
        string finalFileName;
        if (!string.IsNullOrWhiteSpace(targetFileName))
        {
            // 确保 targetFileName 包含扩展名
            if (string.IsNullOrEmpty(Path.GetExtension(targetFileName)) && !string.IsNullOrEmpty(fileExtension))
            {
                finalFileName = $"{targetFileName}.{fileExtension}";
            }
            else
            {
                finalFileName = targetFileName;
            }
        }
        else
        {
            // 默认使用 fileCode.扩展名
            finalFileName = $"{fileCode}.{fileExtension}";
        }

        var fullFilePath = Path.Combine(fullUploadDir, finalFileName);

        // 保存文件
        await using (var fileWriteStream = new FileStream(fullFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await fileStream.CopyToAsync(fileWriteStream, cancellationToken);
        }

        // 构建相对路径
        var relativePath = Path.Combine("upload", uploadPath, datePath, finalFileName).Replace('\\', '/');

        return new FileUploadResult
        {
            FileCode = fileCode,
            FileName = finalFileName,
            FileOriginalName = fileName,
            FilePath = relativePath,
            FileSize = fileStream.Length,
            FileType = fileMimeType,
            FileExtension = fileExtension,
            FileHash = fileHash,
            FileCategory = fileCategory
        };
    }

    /// <summary>
    /// 批量上传文件
    /// </summary>
    /// <param name="files">文件列表（文件流和文件名）</param>
    /// <param name="fileType">文件上传类型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>文件上传结果列表</returns>
    public async Task<List<FileUploadResult>> UploadBatchAsync(List<(Stream stream, string fileName)> files, FileUploadType fileType, CancellationToken cancellationToken = default)
    {
        if (files == null || files.Count == 0)
            return new List<FileUploadResult>();

        var results = new List<FileUploadResult>();

        foreach (var (stream, fileName) in files)
        {
            var result = await UploadAsync(stream, fileName, fileType, null, cancellationToken);
            results.Add(result);
        }

        return results;
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath">文件路径（相对路径）</param>
    /// <returns>任务</returns>
    public async Task DeleteAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return;

        // 构建完整路径
        var fullPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", filePath.Replace('/', Path.DirectorySeparatorChar));

        // 检查文件是否存在
        if (File.Exists(fullPath))
        {
            try
            {
                await TaktFileHelper.DeleteFileAsync(fullPath);
            }
            catch (Exception ex)
            {
                TaktLogger.Warning(ex, $"删除文件失败: {fullPath}");
            }
        }

        return;
    }

    /// <summary>
    /// 获取上传路径（根据文件类型）
    /// </summary>
    /// <param name="fileType">文件上传类型</param>
    /// <returns>上传路径</returns>
    private static string GetUploadPath(FileUploadType fileType)
    {
        return fileType switch
        {
            FileUploadType.Avatar => "avatar",
            FileUploadType.Image => "images",
            FileUploadType.File => "files",
            _ => "files"
        };
    }

    /// <summary>
    /// 获取日期路径（年/月/日）
    /// </summary>
    /// <returns>日期路径</returns>
    private static string GetDatePath()
    {
        var now = DateTime.Now;
        return Path.Combine(now.Year.ToString(), now.Month.ToString("D2"), now.Day.ToString("D2"));
    }

    /// <summary>
    /// 获取MIME类型
    /// </summary>
    /// <param name="fileExtension">文件扩展名</param>
    /// <returns>MIME类型</returns>
    private static string? GetMimeType(string fileExtension)
    {
        return fileExtension.ToLowerInvariant() switch
        {
            "jpg" or "jpeg" => "image/jpeg",
            "png" => "image/png",
            "gif" => "image/gif",
            "tif" or "tiff" => "image/tiff",
            "svg" => "image/svg+xml",
            "pdf" => "application/pdf",
            "docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "zip" => "application/zip",
            "rar" => "application/x-rar-compressed",
            "7z" => "application/x-7z-compressed",
            "txt" => "text/plain",
            "csv" => "text/csv",
            "json" => "application/json",
            "xml" => "application/xml",
            "mp4" => "video/mp4",
            "avi" => "video/x-msvideo",
            "mov" => "video/quicktime",
            "wmv" => "video/x-ms-wmv",
            "flv" => "video/x-flv",
            "mkv" => "video/x-matroska",
            "webm" => "video/webm",
            "mp3" => "audio/mpeg",
            "wav" => "audio/wav",
            "flac" => "audio/flac",
            "aac" => "audio/aac",
            "ogg" => "audio/ogg",
            "wma" => "audio/x-ms-wma",
            "tar" => "application/x-tar",
            "gz" => "application/gzip",
            "bz2" => "application/x-bzip2",
            _ => "application/octet-stream"
        };
    }

    /// <summary>
    /// 获取文件分类（从配置文件读取）
    /// </summary>
    /// <param name="fileExtension">文件扩展名</param>
    /// <returns>文件分类（0=文档，1=图片，2=视频，3=音频，4=压缩包）</returns>
    private int GetFileCategory(string fileExtension)
    {
        var ext = fileExtension.ToLowerInvariant();
        
        // 图片（从配置文件读取）
        if (_imageExtensions.Contains(ext))
            return 1;

        // 视频（从配置文件读取）
        if (_videoExtensions.Contains(ext))
            return 2;

        // 音频（从配置文件读取）
        if (_audioExtensions.Contains(ext))
            return 3;

        // 压缩包（从配置文件读取）
        if (_archiveExtensions.Contains(ext))
            return 4;

        // 文档（默认）
        return 0;
    }

    /// <summary>
    /// 计算文件哈希值（MD5）
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <returns>MD5哈希值</returns>
    private static async Task<string> ComputeFileHashAsync(Stream stream)
    {
        using var md5 = MD5.Create();
        var hashBytes = await md5.ComputeHashAsync(stream);
        var hashString = new StringBuilder();
        foreach (var b in hashBytes)
        {
            hashString.Append(b.ToString("x2"));
        }
        return hashString.ToString();
    }
}
