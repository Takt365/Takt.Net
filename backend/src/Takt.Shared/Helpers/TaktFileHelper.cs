// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Helpers
// 文件名称：TaktFileHelper.cs
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt文件帮助类，提供通用的文件增删改查操作
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using System.Security.Cryptography;
using System.Text;
using MimeKit;

namespace Takt.Shared.Helpers;

/// <summary>
/// Takt文件帮助类
/// </summary>
public static class TaktFileHelper
{
    /// <summary>
    /// 文件分类枚举
    /// </summary>
    public enum FileCategory
    {
        /// <summary>
        /// 文档
        /// </summary>
        Document = 0,

        /// <summary>
        /// 图片
        /// </summary>
        Image = 1,

        /// <summary>
        /// 视频
        /// </summary>
        Video = 2,

        /// <summary>
        /// 音频
        /// </summary>
        Audio = 3,

        /// <summary>
        /// 压缩包
        /// </summary>
        Archive = 4,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 5
    }

    #region 文件读取

    /// <summary>
    /// 读取文件内容为字节数组
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>文件字节数组</returns>
    public static async Task<byte[]> ReadFileAsync(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            TaktLogger.Warning("[TaktFileHelper] 读取文件失败，文件不存在: {FilePath}", filePath);
            throw new FileNotFoundException($"文件不存在: {filePath}");
        }

        try
        {
            var data = await File.ReadAllBytesAsync(filePath);
            TaktLogger.Information("[TaktFileHelper] 读取文件成功: {FilePath}, 大小: {Size} 字节", filePath, data.Length);
            return data;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileHelper] 读取文件失败: {FilePath}", filePath);
            throw;
        }
    }

    /// <summary>
    /// 读取文件内容为字符串
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="encoding">编码，默认为UTF-8</param>
    /// <returns>文件内容字符串</returns>
    public static async Task<string> ReadFileTextAsync(string filePath, Encoding? encoding = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            TaktLogger.Warning("[TaktFileHelper] 读取文件文本失败，文件不存在: {FilePath}", filePath);
            throw new FileNotFoundException($"文件不存在: {filePath}");
        }

        encoding ??= Encoding.UTF8;
        try
        {
            var content = await File.ReadAllTextAsync(filePath, encoding);
            TaktLogger.Information("[TaktFileHelper] 读取文件文本成功: {FilePath}, 编码: {Encoding}", filePath, encoding.EncodingName);
            return content;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileHelper] 读取文件文本失败: {FilePath}", filePath);
            throw;
        }
    }

    /// <summary>
    /// 读取文件为流
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>文件流</returns>
    public static FileStream ReadFileStream(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"文件不存在: {filePath}");
        }

        return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    /// <summary>
    /// 检查文件是否存在
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>是否存在</returns>
    public static bool FileExists(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;

        return File.Exists(filePath);
    }

    /// <summary>
    /// 获取文件信息
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>文件信息</returns>
    public static FileInfo? GetFileInfo(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            return null;

        return new FileInfo(filePath);
    }

    #endregion

    #region 文件写入/创建

    /// <summary>
    /// 写入文件（从字节数组）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="data">文件数据</param>
    /// <param name="createDirectory">是否自动创建目录，默认为true</param>
    /// <returns>任务</returns>
    public static async Task WriteFileAsync(string filePath, byte[] data, bool createDirectory = true)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(data);

        if (createDirectory)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        await File.WriteAllBytesAsync(filePath, data);
    }

    /// <summary>
    /// 写入文件（从字符串）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="content">文件内容</param>
    /// <param name="encoding">编码，默认为UTF-8</param>
    /// <param name="createDirectory">是否自动创建目录，默认为true</param>
    /// <returns>任务</returns>
    public static async Task WriteFileTextAsync(string filePath, string content, Encoding? encoding = null, bool createDirectory = true)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(content);

        try
        {
            if (createDirectory)
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }

            encoding ??= Encoding.UTF8;
            await File.WriteAllTextAsync(filePath, content, encoding);
            TaktLogger.Information("[TaktFileHelper] 写入文件文本成功: {FilePath}, 编码: {Encoding}, 长度: {Length} 字符", filePath, encoding.EncodingName, content.Length);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileHelper] 写入文件文本失败: {FilePath}", filePath);
            throw;
        }
    }

    /// <summary>
    /// 写入文件（从流）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="stream">文件流</param>
    /// <param name="createDirectory">是否自动创建目录，默认为true</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>任务</returns>
    public static async Task WriteFileFromStreamAsync(string filePath, Stream stream, bool createDirectory = true, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(stream);

        if (createDirectory)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        await stream.CopyToAsync(fileStream, cancellationToken);
    }

    /// <summary>
    /// 追加内容到文件
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="content">要追加的内容</param>
    /// <param name="encoding">编码，默认为UTF-8</param>
    /// <param name="createDirectory">是否自动创建目录，默认为true</param>
    /// <returns>任务</returns>
    public static async Task AppendFileTextAsync(string filePath, string content, Encoding? encoding = null, bool createDirectory = true)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(content);

        try
        {
            if (createDirectory)
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }

            encoding ??= Encoding.UTF8;
            await File.AppendAllTextAsync(filePath, content, encoding);
            TaktLogger.Information("[TaktFileHelper] 追加文件文本成功: {FilePath}, 追加长度: {Length} 字符", filePath, content.Length);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileHelper] 追加文件文本失败: {FilePath}", filePath);
            throw;
        }
    }

    #endregion

    #region 文件更新/替换

    /// <summary>
    /// 替换文件（先删除旧文件，再写入新文件）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="newData">新文件数据</param>
    /// <param name="backup">是否备份原文件，默认为false</param>
    /// <returns>任务</returns>
    public static async Task ReplaceFileAsync(string filePath, byte[] newData, bool backup = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(newData);

        if (File.Exists(filePath) && backup)
        {
            var backupPath = $"{filePath}.backup.{DateTime.Now:yyyyMMddHHmmss}";
            await CopyFileAsync(filePath, backupPath, true);
        }

        await WriteFileAsync(filePath, newData);
    }

    /// <summary>
    /// 替换文件（从流）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="newStream">新文件流</param>
    /// <param name="backup">是否备份原文件，默认为false</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>任务</returns>
    public static async Task ReplaceFileFromStreamAsync(string filePath, Stream newStream, bool backup = false, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(newStream);

        try
        {
            if (File.Exists(filePath) && backup)
            {
                var backupPath = $"{filePath}.backup.{DateTime.Now:yyyyMMddHHmmss}";
                await CopyFileAsync(filePath, backupPath, true);
                TaktLogger.Information("[TaktFileHelper] 文件备份成功: {FilePath} -> {BackupPath}", filePath, backupPath);
            }

            await WriteFileFromStreamAsync(filePath, newStream, cancellationToken: cancellationToken);
            TaktLogger.Information("[TaktFileHelper] 从流替换文件成功: {FilePath}, 备份: {Backup}", filePath, backup);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileHelper] 从流替换文件失败: {FilePath}", filePath);
            throw;
        }
    }

    #endregion

    #region 文件删除

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="throwIfNotExists">如果文件不存在是否抛出异常，默认为false</param>
    /// <returns>是否删除成功</returns>
    public static bool DeleteFile(string filePath, bool throwIfNotExists = false)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            if (throwIfNotExists)
                throw new ArgumentException("文件路径不能为空", nameof(filePath));
            return false;
        }

        if (!File.Exists(filePath))
        {
            if (throwIfNotExists)
            {
                TaktLogger.Warning("[TaktFileHelper] 删除文件失败，文件不存在: {FilePath}", filePath);
                throw new FileNotFoundException($"文件不存在: {filePath}");
            }
            return false;
        }

        try
        {
            File.Delete(filePath);
            TaktLogger.Information("[TaktFileHelper] 删除文件成功: {FilePath}", filePath);
            return true;
        }
        catch (Exception ex)
        {
            if (throwIfNotExists)
            {
                TaktLogger.Error(ex, "[TaktFileHelper] 删除文件失败: {FilePath}", filePath);
                throw;
            }
            TaktLogger.Warning(ex, "[TaktFileHelper] 删除文件失败: {FilePath}", filePath);
            return false;
        }
    }

    /// <summary>
    /// 删除文件（异步）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="throwIfNotExists">如果文件不存在是否抛出异常，默认为false</param>
    /// <returns>是否删除成功</returns>
    public static async Task<bool> DeleteFileAsync(string filePath, bool throwIfNotExists = false)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            if (throwIfNotExists)
                throw new ArgumentException("文件路径不能为空", nameof(filePath));
            return false;
        }

        if (!File.Exists(filePath))
        {
            if (throwIfNotExists)
            {
                TaktLogger.Warning("[TaktFileHelper] 删除文件失败，文件不存在: {FilePath}", filePath);
                throw new FileNotFoundException($"文件不存在: {filePath}");
            }
            return false;
        }

        try
        {
            await Task.Run(() => File.Delete(filePath));
            TaktLogger.Information("[TaktFileHelper] 删除文件成功: {FilePath}", filePath);
            return true;
        }
        catch (Exception ex)
        {
            if (throwIfNotExists)
            {
                TaktLogger.Error(ex, "[TaktFileHelper] 删除文件失败: {FilePath}", filePath);
                throw;
            }
            TaktLogger.Warning(ex, "[TaktFileHelper] 删除文件失败: {FilePath}", filePath);
            return false;
        }
    }

    /// <summary>
    /// 批量删除文件
    /// </summary>
    /// <param name="filePaths">文件路径列表</param>
    /// <param name="throwOnError">遇到错误是否抛出异常，默认为false</param>
    /// <returns>成功删除的文件数量</returns>
    public static int DeleteFiles(IEnumerable<string> filePaths, bool throwOnError = false)
    {
        ArgumentNullException.ThrowIfNull(filePaths);

        var successCount = 0;
        var totalCount = 0;
        foreach (var filePath in filePaths)
        {
            totalCount++;
            try
            {
                if (DeleteFile(filePath, false))
                {
                    successCount++;
                }
            }
            catch (Exception ex)
            {
                if (throwOnError)
                {
                    TaktLogger.Error(ex, "[TaktFileHelper] 批量删除文件失败: {FilePath}", filePath);
                    throw;
                }
                TaktLogger.Warning(ex, "[TaktFileHelper] 批量删除文件失败: {FilePath}", filePath);
            }
        }

        TaktLogger.Information("[TaktFileHelper] 批量删除文件完成: 总数: {TotalCount}, 成功: {SuccessCount}", totalCount, successCount);
        return successCount;
    }

    /// <summary>
    /// 删除目录及其所有子文件和子目录（递归删除）
    /// </summary>
    /// <param name="directoryPath">目录路径</param>
    /// <param name="throwIfNotExists">如果目录不存在是否抛出异常，默认为false</param>
    /// <returns>是否删除成功</returns>
    public static bool DeleteDirectory(string directoryPath, bool throwIfNotExists = false)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
        {
            if (throwIfNotExists)
                throw new ArgumentException("目录路径不能为空", nameof(directoryPath));
            return false;
        }

        if (!Directory.Exists(directoryPath))
        {
            if (throwIfNotExists)
            {
                TaktLogger.Warning("[TaktFileHelper] 删除目录失败，目录不存在: {DirectoryPath}", directoryPath);
                throw new DirectoryNotFoundException($"目录不存在: {directoryPath}");
            }
            return false;
        }

        try
        {
            Directory.Delete(directoryPath, true);
            TaktLogger.Information("[TaktFileHelper] 删除目录成功: {DirectoryPath}", directoryPath);
            return true;
        }
        catch (Exception ex)
        {
            if (throwIfNotExists)
            {
                TaktLogger.Error(ex, "[TaktFileHelper] 删除目录失败: {DirectoryPath}", directoryPath);
                throw;
            }
            TaktLogger.Warning(ex, "[TaktFileHelper] 删除目录失败: {DirectoryPath}", directoryPath);
            return false;
        }
    }

    /// <summary>
    /// 删除目录及其所有子文件和子目录（异步）
    /// </summary>
    /// <param name="directoryPath">目录路径</param>
    /// <param name="throwIfNotExists">如果目录不存在是否抛出异常，默认为false</param>
    /// <returns>是否删除成功</returns>
    public static Task<bool> DeleteDirectoryAsync(string directoryPath, bool throwIfNotExists = false)
    {
        return Task.FromResult(DeleteDirectory(directoryPath, throwIfNotExists));
    }

    /// <summary>
    /// 按名称模式删除文件或目录（支持递归搜索）
    /// </summary>
    /// <param name="searchPath">搜索路径（目录路径）</param>
    /// <param name="namePattern">名称模式（包含的字符串，区分大小写）</param>
    /// <param name="recursive">是否递归搜索子目录，默认为true</param>
    /// <param name="includeDirectories">是否删除匹配的目录，默认为true</param>
    /// <param name="includeFiles">是否删除匹配的文件，默认为true</param>
    /// <param name="throwOnError">遇到错误是否抛出异常，默认为false</param>
    /// <returns>删除统计信息（文件数、目录数）</returns>
    public static (int fileCount, int directoryCount) DeleteByPattern(
        string searchPath,
        string namePattern,
        bool recursive = true,
        bool includeDirectories = true,
        bool includeFiles = true,
        bool throwOnError = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(searchPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(namePattern);

        if (!Directory.Exists(searchPath))
        {
            TaktLogger.Warning("[TaktFileHelper] 按模式删除失败，搜索路径不存在: {SearchPath}", searchPath);
            if (throwOnError)
                throw new DirectoryNotFoundException($"搜索路径不存在: {searchPath}");
            return (0, 0);
        }

        var fileCount = 0;
        var directoryCount = 0;
        var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        try
        {
            // 删除匹配的文件
            if (includeFiles)
            {
                var files = Directory.GetFiles(searchPath, "*", searchOption)
                    .Where(f => Path.GetFileName(f).Contains(namePattern, StringComparison.Ordinal))
                    .ToList();

                foreach (var file in files)
                {
                    try
                    {
                        if (DeleteFile(file, false))
                        {
                            fileCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (throwOnError)
                        {
                            TaktLogger.Error(ex, "[TaktFileHelper] 按模式删除文件失败: {FilePath}", file);
                            throw;
                        }
                        TaktLogger.Warning(ex, "[TaktFileHelper] 按模式删除文件失败: {FilePath}", file);
                    }
                }
            }

            // 删除匹配的目录（需要从最深层的目录开始删除）
            if (includeDirectories)
            {
                var directories = Directory.GetDirectories(searchPath, "*", searchOption)
                    .Where(d => Path.GetFileName(d).Contains(namePattern, StringComparison.Ordinal))
                    .OrderByDescending(d => d.Length) // 先删除深层目录
                    .ToList();

                foreach (var directory in directories)
                {
                    try
                    {
                        if (DeleteDirectory(directory, false))
                        {
                            directoryCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (throwOnError)
                        {
                            TaktLogger.Error(ex, "[TaktFileHelper] 按模式删除目录失败: {DirectoryPath}", directory);
                            throw;
                        }
                        TaktLogger.Warning(ex, "[TaktFileHelper] 按模式删除目录失败: {DirectoryPath}", directory);
                    }
                }
            }

            TaktLogger.Information("[TaktFileHelper] 按模式删除完成: 搜索路径: {SearchPath}, 模式: {Pattern}, 删除文件数: {FileCount}, 删除目录数: {DirectoryCount}", 
                searchPath, namePattern, fileCount, directoryCount);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileHelper] 按模式删除失败: 搜索路径: {SearchPath}, 模式: {Pattern}", searchPath, namePattern);
            if (throwOnError)
                throw;
        }

        return (fileCount, directoryCount);
    }

    /// <summary>
    /// 按名称模式删除文件或目录（异步，支持递归搜索）
    /// </summary>
    /// <param name="searchPath">搜索路径（目录路径）</param>
    /// <param name="namePattern">名称模式（包含的字符串，区分大小写）</param>
    /// <param name="recursive">是否递归搜索子目录，默认为true</param>
    /// <param name="includeDirectories">是否删除匹配的目录，默认为true</param>
    /// <param name="includeFiles">是否删除匹配的文件，默认为true</param>
    /// <param name="throwOnError">遇到错误是否抛出异常，默认为false</param>
    /// <returns>删除统计信息（文件数、目录数）</returns>
    public static Task<(int fileCount, int directoryCount)> DeleteByPatternAsync(
        string searchPath,
        string namePattern,
        bool recursive = true,
        bool includeDirectories = true,
        bool includeFiles = true,
        bool throwOnError = false)
    {
        return Task.FromResult(DeleteByPattern(searchPath, namePattern, recursive, includeDirectories, includeFiles, throwOnError));
    }

    #endregion

    #region 文件工具方法

    /// <summary>
    /// 获取文件的MIME类型（使用MimeKit库）
    /// </summary>
    /// <param name="fileName">文件名或文件路径</param>
    /// <returns>MIME类型，如果无法识别则返回 "application/octet-stream"</returns>
    public static string GetMimeType(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return "application/octet-stream";

        var extension = Path.GetExtension(fileName)?.TrimStart('.')?.ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(extension))
            return "application/octet-stream";

        // 使用 MimeKit 的 MimeTypes 类获取 MIME 类型
        try
        {
            var mimeType = MimeTypes.GetMimeType(extension);
            return mimeType ?? "application/octet-stream";
        }
        catch
        {
            return "application/octet-stream";
        }
    }

    /// <summary>
    /// 获取文件分类
    /// </summary>
    /// <param name="fileName">文件名或文件路径</param>
    /// <returns>文件分类</returns>
    public static FileCategory GetFileCategory(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return FileCategory.Other;

        var extension = Path.GetExtension(fileName)?.TrimStart('.')?.ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(extension))
            return FileCategory.Other;

        // 图片
        if (new[] { "jpg", "jpeg", "png", "gif", "bmp", "webp", "svg", "ico", "tiff", "tif" }.Contains(extension))
            return FileCategory.Image;

        // 视频
        if (new[] { "mp4", "avi", "mov", "wmv", "flv", "mkv", "webm", "m4v", "3gp", "rm", "rmvb" }.Contains(extension))
            return FileCategory.Video;

        // 音频
        if (new[] { "mp3", "wav", "flac", "aac", "ogg", "wma", "m4a", "ape", "amr" }.Contains(extension))
            return FileCategory.Audio;

        // 压缩包
        if (new[] { "zip", "rar", "7z", "tar", "gz", "bz2", "xz", "cab", "iso" }.Contains(extension))
            return FileCategory.Archive;

        // 文档（默认）
        if (new[] { "pdf", "doc", "docx", "xls", "xlsx", "ppt", "pptx", "txt", "rtf", "odt", "ods", "odp" }.Contains(extension))
            return FileCategory.Document;

        return FileCategory.Other;
    }

    /// <summary>
    /// 计算文件哈希值（MD5）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>MD5哈希值（小写）</returns>
    public static async Task<string> ComputeFileHashAsync(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        if (!File.Exists(filePath))
        {
            TaktLogger.Warning("[TaktFileHelper] 计算文件哈希失败，文件不存在: {FilePath}", filePath);
            throw new FileNotFoundException($"文件不存在: {filePath}");
        }

        try
        {
            await using var stream = ReadFileStream(filePath);
            var hash = await ComputeStreamHashAsync(stream);
            TaktLogger.Information("[TaktFileHelper] 计算文件哈希成功: {FilePath}, 哈希值: {Hash}", filePath, hash);
            return hash;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileHelper] 计算文件哈希失败: {FilePath}", filePath);
            throw;
        }
    }

    /// <summary>
    /// 计算流哈希值（MD5）
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <returns>MD5哈希值（小写）</returns>
    public static async Task<string> ComputeStreamHashAsync(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        using var md5 = MD5.Create();
        var hashBytes = await md5.ComputeHashAsync(stream);
        var hashString = new StringBuilder();
        foreach (var b in hashBytes)
        {
            hashString.Append(b.ToString("x2"));
        }
        return hashString.ToString();
    }

    /// <summary>
    /// 计算字节数组哈希值（MD5）
    /// </summary>
    /// <param name="data">字节数组</param>
    /// <returns>MD5哈希值（小写）</returns>
    public static string ComputeHash(byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);

        using var md5 = MD5.Create();
        var hashBytes = md5.ComputeHash(data);
        var hashString = new StringBuilder();
        foreach (var b in hashBytes)
        {
            hashString.Append(b.ToString("x2"));
        }
        return hashString.ToString();
    }

    /// <summary>
    /// 获取文件大小（字节）
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>文件大小，如果文件不存在返回-1</returns>
    public static long GetFileSize(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            return -1;

        return new FileInfo(filePath).Length;
    }

    /// <summary>
    /// 格式化文件大小
    /// </summary>
    /// <param name="bytes">字节数</param>
    /// <returns>格式化后的文件大小字符串</returns>
    public static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    /// <summary>
    /// 生成唯一文件名（使用GUID）
    /// </summary>
    /// <param name="originalFileName">原始文件名</param>
    /// <returns>唯一文件名</returns>
    public static string GenerateUniqueFileName(string originalFileName)
    {
        if (string.IsNullOrWhiteSpace(originalFileName))
            return $"{Guid.NewGuid():N}";

        var extension = Path.GetExtension(originalFileName);
        var fileCode = Guid.NewGuid().ToString("N");
        return $"{fileCode}{extension}";
    }

    /// <summary>
    /// 生成日期路径（年/月/日）
    /// </summary>
    /// <param name="date">日期，默认为当前日期</param>
    /// <returns>日期路径</returns>
    public static string GenerateDatePath(DateTime? date = null)
    {
        var targetDate = date ?? DateTime.Now;
        return Path.Combine(targetDate.Year.ToString(), targetDate.Month.ToString("D2"), targetDate.Day.ToString("D2"));
    }

    /// <summary>
    /// 确保目录存在
    /// </summary>
    /// <param name="directoryPath">目录路径</param>
    public static void EnsureDirectoryExists(string directoryPath)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
            return;

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    /// <summary>
    /// 复制文件（同步）
    /// </summary>
    /// <param name="sourcePath">源文件路径</param>
    /// <param name="destinationPath">目标文件路径</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为false</param>
    public static void CopyFile(string sourcePath, string destinationPath, bool overwrite = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourcePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationPath);

        if (!File.Exists(sourcePath))
        {
            TaktLogger.Warning("[TaktFileHelper] 复制文件失败，源文件不存在: {SourcePath}", sourcePath);
            throw new FileNotFoundException($"源文件不存在: {sourcePath}");
        }

        try
        {
            var directory = Path.GetDirectoryName(destinationPath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                EnsureDirectoryExists(directory);
            }

            File.Copy(sourcePath, destinationPath, overwrite);
            var fileSize = GetFileSize(destinationPath);
            TaktLogger.Information("[TaktFileHelper] 复制文件成功: {SourcePath} -> {DestinationPath}, 大小: {Size} 字节, 覆盖: {Overwrite}", sourcePath, destinationPath, fileSize, overwrite);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileHelper] 复制文件失败: {SourcePath} -> {DestinationPath}", sourcePath, destinationPath);
            throw;
        }
    }

    /// <summary>
    /// 复制文件（异步）
    /// </summary>
    /// <param name="sourcePath">源文件路径</param>
    /// <param name="destinationPath">目标文件路径</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为false</param>
    /// <returns>任务</returns>
    public static async Task CopyFileAsync(string sourcePath, string destinationPath, bool overwrite = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourcePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationPath);

        if (!File.Exists(sourcePath))
        {
            TaktLogger.Warning("[TaktFileHelper] 复制文件失败，源文件不存在: {SourcePath}", sourcePath);
            throw new FileNotFoundException($"源文件不存在: {sourcePath}");
        }

        var directory = Path.GetDirectoryName(destinationPath);
        if (!string.IsNullOrWhiteSpace(directory))
            EnsureDirectoryExists(directory);

        if (File.Exists(destinationPath) && !overwrite)
            throw new IOException($"目标文件已存在: {destinationPath}");

        await using var source = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, FileOptions.Asynchronous);
        await using var destination = new FileStream(destinationPath, overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, FileOptions.Asynchronous);
        await source.CopyToAsync(destination);
        await destination.FlushAsync();
        var fileSize = GetFileSize(destinationPath);
        TaktLogger.Information("[TaktFileHelper] 复制文件成功: {SourcePath} -> {DestinationPath}, 大小: {Size} 字节, 覆盖: {Overwrite}", sourcePath, destinationPath, fileSize, overwrite);
    }

    /// <summary>
    /// 移动文件（同步）
    /// </summary>
    /// <param name="sourcePath">源文件路径</param>
    /// <param name="destinationPath">目标文件路径</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为false</param>
    public static void MoveFile(string sourcePath, string destinationPath, bool overwrite = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourcePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationPath);

        if (!File.Exists(sourcePath))
        {
            TaktLogger.Warning("[TaktFileHelper] 移动文件失败，源文件不存在: {SourcePath}", sourcePath);
            throw new FileNotFoundException($"源文件不存在: {sourcePath}");
        }

        if (File.Exists(destinationPath) && !overwrite)
        {
            TaktLogger.Warning("[TaktFileHelper] 移动文件失败，目标文件已存在: {DestinationPath}", destinationPath);
            throw new IOException($"目标文件已存在: {destinationPath}");
        }

        try
        {
            var directory = Path.GetDirectoryName(destinationPath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                EnsureDirectoryExists(directory);
            }

            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }

            File.Move(sourcePath, destinationPath);
            var fileSize = GetFileSize(destinationPath);
            TaktLogger.Information("[TaktFileHelper] 移动文件成功: {SourcePath} -> {DestinationPath}, 大小: {Size} 字节, 覆盖: {Overwrite}", sourcePath, destinationPath, fileSize, overwrite);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFileHelper] 移动文件失败: {SourcePath} -> {DestinationPath}", sourcePath, destinationPath);
            throw;
        }
    }

    /// <summary>
    /// 移动文件（异步）
    /// </summary>
    /// <param name="sourcePath">源文件路径</param>
    /// <param name="destinationPath">目标文件路径</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为false</param>
    /// <returns>任务</returns>
    public static async Task MoveFileAsync(string sourcePath, string destinationPath, bool overwrite = false)
    {
        await CopyFileAsync(sourcePath, destinationPath, overwrite);
        await DeleteFileAsync(sourcePath, throwIfNotExists: true);
    }

    #endregion
}
