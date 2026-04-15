// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Shared.Helpers
// 文件名称：TaktFtpHelper.cs
// 创建时间：2025-01-22
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt FTP帮助类，使用 FluentFTP 进行文件传输
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using FluentFTP;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace Takt.Shared.Helpers;

/// <summary>
/// FTP配置信息
/// </summary>
public class FtpConfig
{
    /// <summary>
    /// FTP服务器地址
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// FTP端口（默认21）
    /// </summary>
    public int Port { get; set; } = 21;

    /// <summary>
    /// FTP用户名
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// FTP密码
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 是否使用SSL/TLS（默认false）
    /// </summary>
    public bool EnableSsl { get; set; } = false;

    /// <summary>
    /// 连接超时时间（秒，默认30）
    /// </summary>
    public int Timeout { get; set; } = 30;

    /// <summary>
    /// 基础路径（FTP服务器上的基础目录，可选）
    /// </summary>
    public string? BasePath { get; set; }
}

/// <summary>
/// Takt FTP帮助类
/// </summary>
public static class TaktFtpHelper
{
    /// <summary>
    /// 从配置中读取FTP设置
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <returns>FTP配置</returns>
    public static FtpConfig GetFtpConfigFromConfiguration(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var config = new FtpConfig
        {
            Host = configuration["Ftp:Host"] ?? string.Empty,
            Port = int.TryParse(configuration["Ftp:Port"], out var port) ? port : 21,
            Username = configuration["Ftp:Username"] ?? string.Empty,
            Password = configuration["Ftp:Password"] ?? string.Empty,
            EnableSsl = bool.TryParse(configuration["Ftp:EnableSsl"], out var ssl) && ssl,
            Timeout = int.TryParse(configuration["Ftp:Timeout"], out var timeout) ? timeout : 30,
            BasePath = configuration["Ftp:BasePath"]
        };

        return config;
    }

    /// <summary>
    /// 从JSON字符串解析FTP配置
    /// </summary>
    /// <param name="jsonConfig">JSON配置字符串</param>
    /// <returns>FTP配置</returns>
    public static FtpConfig? GetFtpConfigFromJson(string? jsonConfig)
    {
        if (string.IsNullOrWhiteSpace(jsonConfig))
            return null;

        try
        {
            return JsonConvert.DeserializeObject<FtpConfig>(jsonConfig);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] 解析FTP配置JSON失败: {JsonConfig}", jsonConfig);
            return null;
        }
    }

    /// <summary>
    /// 创建FTP客户端
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <returns>FTP客户端</returns>
    private static AsyncFtpClient CreateFtpClient(FtpConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(config.Host);
        ArgumentException.ThrowIfNullOrWhiteSpace(config.Username);
        ArgumentException.ThrowIfNullOrWhiteSpace(config.Password);

        var client = new AsyncFtpClient(config.Host, config.Username, config.Password, config.Port);
        
        if (config.EnableSsl)
        {
            client.Config.EncryptionMode = FtpEncryptionMode.Explicit;
            client.Config.ValidateAnyCertificate = true; // 生产环境应设置为false并配置证书
        }

        client.Config.ConnectTimeout = config.Timeout * 1000; // 转换为毫秒
        client.Config.ReadTimeout = config.Timeout * 1000;
        client.Config.DataConnectionType = FtpDataConnectionType.AutoPassive;

        return client;
    }

    /// <summary>
    /// 上传文件到FTP服务器
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="localFilePath">本地文件路径</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为true</param>
    /// <returns>任务</returns>
    public static async Task UploadFileAsync(FtpConfig config, string localFilePath, string remoteFilePath, bool overwrite = true)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(localFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteFilePath);

        if (!File.Exists(localFilePath))
        {
            TaktLogger.Warning("[TaktFtpHelper] 本地文件不存在: {LocalFilePath}", localFilePath);
            throw new FileNotFoundException($"本地文件不存在: {localFilePath}");
        }

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteFilePath);

            // 确保远程目录存在
            var remoteDir = Path.GetDirectoryName(fullRemotePath)?.Replace('\\', '/');
            if (!string.IsNullOrWhiteSpace(remoteDir))
            {
                await client.CreateDirectory(remoteDir);
            }

            // 上传文件
            var uploadStatus = await client.UploadFile(localFilePath, fullRemotePath, overwrite ? FtpRemoteExists.Overwrite : FtpRemoteExists.Skip);
            
            if (uploadStatus == FtpStatus.Success)
            {
                var fileSize = new FileInfo(localFilePath).Length;
                TaktLogger.Information("[TaktFtpHelper] FTP文件上传成功: {LocalFilePath} -> {RemoteFilePath}, 大小: {Size} 字节", 
                    localFilePath, fullRemotePath, fileSize);
            }
            else
            {
                TaktLogger.Warning("[TaktFtpHelper] FTP文件上传状态: {Status}, 本地文件: {LocalFilePath}, 远程文件: {RemoteFilePath}", 
                    uploadStatus, localFilePath, fullRemotePath);
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP文件上传失败: {LocalFilePath} -> {RemoteFilePath}", localFilePath, remoteFilePath);
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 从FTP服务器下载文件
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <param name="localFilePath">本地文件路径</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为true</param>
    /// <returns>任务</returns>
    public static async Task DownloadFileAsync(FtpConfig config, string remoteFilePath, string localFilePath, bool overwrite = true)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteFilePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(localFilePath);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteFilePath);

            // 检查远程文件是否存在
            if (!await client.FileExists(fullRemotePath))
            {
                TaktLogger.Warning("[TaktFtpHelper] 远程文件不存在: {RemoteFilePath}", fullRemotePath);
                throw new FileNotFoundException($"远程文件不存在: {fullRemotePath}");
            }

            // 确保本地目录存在
            var localDir = Path.GetDirectoryName(localFilePath);
            if (!string.IsNullOrWhiteSpace(localDir))
            {
                Directory.CreateDirectory(localDir);
            }

            // 下载文件
            var downloadStatus = await client.DownloadFile(localFilePath, fullRemotePath, overwrite ? FtpLocalExists.Overwrite : FtpLocalExists.Skip);
            
            if (downloadStatus == FtpStatus.Success)
            {
                var fileSize = new FileInfo(localFilePath).Length;
                TaktLogger.Information("[TaktFtpHelper] FTP文件下载成功: {RemoteFilePath} -> {LocalFilePath}, 大小: {Size} 字节", 
                    fullRemotePath, localFilePath, fileSize);
            }
            else
            {
                TaktLogger.Warning("[TaktFtpHelper] FTP文件下载状态: {Status}, 远程文件: {RemoteFilePath}, 本地文件: {LocalFilePath}", 
                    downloadStatus, fullRemotePath, localFilePath);
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP文件下载失败: {RemoteFilePath} -> {LocalFilePath}", remoteFilePath, localFilePath);
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 删除FTP服务器上的文件
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <returns>任务</returns>
    public static async Task DeleteFileAsync(FtpConfig config, string remoteFilePath)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteFilePath);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteFilePath);

            // 检查文件是否存在
            if (!await client.FileExists(fullRemotePath))
            {
                TaktLogger.Warning("[TaktFtpHelper] FTP文件不存在，跳过删除: {RemoteFilePath}", fullRemotePath);
                return;
            }

            // 删除文件
            await client.DeleteFile(fullRemotePath);
            TaktLogger.Information("[TaktFtpHelper] FTP文件删除成功: {RemoteFilePath}", fullRemotePath);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP文件删除失败: {RemoteFilePath}", remoteFilePath);
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 检查FTP服务器上的文件是否存在
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <returns>文件是否存在</returns>
    public static async Task<bool> FileExistsAsync(FtpConfig config, string remoteFilePath)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteFilePath);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteFilePath);

            return await client.FileExists(fullRemotePath);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] 检查FTP文件是否存在失败: {RemoteFilePath}", remoteFilePath);
            return false;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 列出FTP服务器上的文件
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录，默认为空表示根目录）</param>
    /// <returns>文件列表</returns>
    public static async Task<List<FtpListItem>> ListFilesAsync(FtpConfig config, string? remoteDirectory = null)
    {
        ArgumentNullException.ThrowIfNull(config);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteDirectory ?? string.Empty);

            var items = await client.GetListing(fullRemotePath);
            var files = items.Where(item => item.Type == FtpObjectType.File).ToList();

            TaktLogger.Information("[TaktFtpHelper] FTP列出文件成功: 目录: {RemoteDirectory}, 文件数: {FileCount}", 
                fullRemotePath, files.Count);

            return files;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP列出文件失败: 目录: {RemoteDirectory}", remoteDirectory ?? "根目录");
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 列出FTP服务器上的目录
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录，默认为空表示根目录）</param>
    /// <returns>目录列表</returns>
    public static async Task<List<FtpListItem>> ListDirectoriesAsync(FtpConfig config, string? remoteDirectory = null)
    {
        ArgumentNullException.ThrowIfNull(config);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteDirectory ?? string.Empty);

            var items = await client.GetListing(fullRemotePath);
            var directories = items.Where(item => item.Type == FtpObjectType.Directory).ToList();

            TaktLogger.Information("[TaktFtpHelper] FTP列出目录成功: 目录: {RemoteDirectory}, 目录数: {DirectoryCount}", 
                fullRemotePath, directories.Count);

            return directories;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP列出目录失败: 目录: {RemoteDirectory}", remoteDirectory ?? "根目录");
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 列出FTP服务器上的所有项目（文件和目录）
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录，默认为空表示根目录）</param>
    /// <returns>所有项目列表</returns>
    public static async Task<List<FtpListItem>> ListAllAsync(FtpConfig config, string? remoteDirectory = null)
    {
        ArgumentNullException.ThrowIfNull(config);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteDirectory ?? string.Empty);

            var items = await client.GetListing(fullRemotePath);
            var allItems = items.ToList();

            TaktLogger.Information("[TaktFtpHelper] FTP列出所有项目成功: 目录: {RemoteDirectory}, 项目数: {ItemCount}", 
                fullRemotePath, allItems.Count);

            return allItems;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP列出所有项目失败: 目录: {RemoteDirectory}", remoteDirectory ?? "根目录");
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 重命名或移动FTP服务器上的文件或目录
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remotePath">远程文件或目录路径（相对于BasePath或FTP根目录）</param>
    /// <param name="newRemotePath">新的远程文件或目录路径（相对于BasePath或FTP根目录）</param>
    /// <returns>任务</returns>
    public static async Task RenameAsync(FtpConfig config, string remotePath, string newRemotePath)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(remotePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(newRemotePath);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remotePath);
            var fullNewRemotePath = BuildRemotePath(config, newRemotePath);

            // 重命名或移动
            await client.Rename(fullRemotePath, fullNewRemotePath);
            TaktLogger.Information("[TaktFtpHelper] FTP重命名/移动成功: {RemotePath} -> {NewRemotePath}", 
                fullRemotePath, fullNewRemotePath);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP重命名/移动失败: {RemotePath} -> {NewRemotePath}", 
                remotePath, newRemotePath);
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 获取FTP服务器上文件或目录的修改时间
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remotePath">远程文件或目录路径（相对于BasePath或FTP根目录）</param>
    /// <returns>修改时间，如果文件或目录不存在返回null</returns>
    public static async Task<DateTime?> GetModifiedTimeAsync(FtpConfig config, string remotePath)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(remotePath);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remotePath);

            if (!await client.FileExists(fullRemotePath) && !await client.DirectoryExists(fullRemotePath))
            {
                TaktLogger.Warning("[TaktFtpHelper] FTP文件或目录不存在: {RemotePath}", fullRemotePath);
                return null;
            }

            var modifiedTime = await client.GetModifiedTime(fullRemotePath);
            TaktLogger.Information("[TaktFtpHelper] FTP获取修改时间成功: {RemotePath}, 修改时间: {ModifiedTime}", 
                fullRemotePath, modifiedTime);
            return modifiedTime;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP获取修改时间失败: {RemotePath}", remotePath);
            return null;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 使用流上传文件到FTP服务器
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="stream">文件流</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为true</param>
    /// <returns>任务</returns>
    public static async Task UploadStreamAsync(FtpConfig config, Stream stream, string remoteFilePath, bool overwrite = true)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(stream);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteFilePath);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteFilePath);

            // 确保远程目录存在
            var remoteDir = Path.GetDirectoryName(fullRemotePath)?.Replace('\\', '/');
            if (!string.IsNullOrWhiteSpace(remoteDir))
            {
                await client.CreateDirectory(remoteDir);
            }

            // 上传流
            var uploadStatus = await client.UploadStream(stream, fullRemotePath, overwrite ? FtpRemoteExists.Overwrite : FtpRemoteExists.Skip);
            
            if (uploadStatus == FtpStatus.Success)
            {
                TaktLogger.Information("[TaktFtpHelper] FTP流上传成功: {RemoteFilePath}, 大小: {Size} 字节", 
                    fullRemotePath, stream.Length);
            }
            else
            {
                TaktLogger.Warning("[TaktFtpHelper] FTP流上传状态: {Status}, 远程文件: {RemoteFilePath}", 
                    uploadStatus, fullRemotePath);
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP流上传失败: {RemoteFilePath}", remoteFilePath);
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 使用流从FTP服务器下载文件
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <param name="stream">目标流</param>
    /// <returns>任务</returns>
    public static async Task DownloadStreamAsync(FtpConfig config, string remoteFilePath, Stream stream)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteFilePath);
        ArgumentNullException.ThrowIfNull(stream);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteFilePath);

            // 检查远程文件是否存在
            if (!await client.FileExists(fullRemotePath))
            {
                TaktLogger.Warning("[TaktFtpHelper] 远程文件不存在: {RemoteFilePath}", fullRemotePath);
                throw new FileNotFoundException($"远程文件不存在: {fullRemotePath}");
            }

            // 下载到流
            var success = await client.DownloadStream(stream, fullRemotePath);
            
            if (success)
            {
                TaktLogger.Information("[TaktFtpHelper] FTP流下载成功: {RemoteFilePath}, 大小: {Size} 字节", 
                    fullRemotePath, stream.Length);
            }
            else
            {
                TaktLogger.Warning("[TaktFtpHelper] FTP流下载失败: {RemoteFilePath}", fullRemotePath);
            }
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP流下载失败: {RemoteFilePath}", remoteFilePath);
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 创建FTP目录
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录）</param>
    /// <returns>任务</returns>
    public static async Task CreateDirectoryAsync(FtpConfig config, string remoteDirectory)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteDirectory);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteDirectory);

            // 创建目录（递归创建父目录）
            await client.CreateDirectory(fullRemotePath);
            TaktLogger.Information("[TaktFtpHelper] FTP目录创建成功: {RemoteDirectory}", fullRemotePath);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP目录创建失败: {RemoteDirectory}", remoteDirectory);
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 删除FTP目录（递归删除）
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录）</param>
    /// <returns>任务</returns>
    public static async Task DeleteDirectoryAsync(FtpConfig config, string remoteDirectory)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteDirectory);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteDirectory);

            // 删除目录（递归删除）
            await client.DeleteDirectory(fullRemotePath);
            TaktLogger.Information("[TaktFtpHelper] FTP目录删除成功: {RemoteDirectory}", fullRemotePath);
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP目录删除失败: {RemoteDirectory}", remoteDirectory);
            throw;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 获取FTP文件大小
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <returns>文件大小（字节），如果文件不存在返回-1</returns>
    public static async Task<long> GetFileSizeAsync(FtpConfig config, string remoteFilePath)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentException.ThrowIfNullOrWhiteSpace(remoteFilePath);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            // 构建完整远程路径
            var fullRemotePath = BuildRemotePath(config, remoteFilePath);

            if (!await client.FileExists(fullRemotePath))
            {
                TaktLogger.Warning("[TaktFtpHelper] FTP文件不存在: {RemoteFilePath}", fullRemotePath);
                return -1;
            }

            var fileSize = await client.GetFileSize(fullRemotePath);
            TaktLogger.Information("[TaktFtpHelper] FTP文件大小: {RemoteFilePath}, 大小: {Size} 字节", fullRemotePath, fileSize);
            return fileSize;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] 获取FTP文件大小失败: {RemoteFilePath}", remoteFilePath);
            return -1;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    /// <summary>
    /// 测试FTP连接
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <returns>连接是否成功</returns>
    public static async Task<bool> TestConnectionAsync(FtpConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        AsyncFtpClient? client = null;
        try
        {
            client = CreateFtpClient(config);
            await client.AutoConnect();

            TaktLogger.Information("[TaktFtpHelper] FTP连接测试成功: {Host}:{Port}", config.Host, config.Port);
            return true;
        }
        catch (Exception ex)
        {
            TaktLogger.Error(ex, "[TaktFtpHelper] FTP连接测试失败: {Host}:{Port}", config.Host, config.Port);
            return false;
        }
        finally
        {
            if (client != null && client.IsConnected)
            {
                await client.Disconnect();
            }
            client?.Dispose();
        }
    }

    // ========== 直接使用 IConfiguration 的重载方法 ==========

    /// <summary>
    /// 上传文件到FTP服务器（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="localFilePath">本地文件路径</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为true</param>
    /// <returns>任务</returns>
    public static async Task UploadFileAsync(IConfiguration configuration, string localFilePath, string remoteFilePath, bool overwrite = true)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        await UploadFileAsync(config, localFilePath, remoteFilePath, overwrite);
    }

    /// <summary>
    /// 从FTP服务器下载文件（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <param name="localFilePath">本地文件路径</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为true</param>
    /// <returns>任务</returns>
    public static async Task DownloadFileAsync(IConfiguration configuration, string remoteFilePath, string localFilePath, bool overwrite = true)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        await DownloadFileAsync(config, remoteFilePath, localFilePath, overwrite);
    }

    /// <summary>
    /// 删除FTP服务器上的文件（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <returns>任务</returns>
    public static async Task DeleteFileAsync(IConfiguration configuration, string remoteFilePath)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        await DeleteFileAsync(config, remoteFilePath);
    }

    /// <summary>
    /// 检查FTP服务器上的文件是否存在（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <returns>文件是否存在</returns>
    public static async Task<bool> FileExistsAsync(IConfiguration configuration, string remoteFilePath)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        return await FileExistsAsync(config, remoteFilePath);
    }

    /// <summary>
    /// 列出FTP服务器上的文件（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录，默认为空表示根目录）</param>
    /// <returns>文件列表</returns>
    public static async Task<List<FtpListItem>> ListFilesAsync(IConfiguration configuration, string? remoteDirectory = null)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        return await ListFilesAsync(config, remoteDirectory);
    }

    /// <summary>
    /// 列出FTP服务器上的目录（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录，默认为空表示根目录）</param>
    /// <returns>目录列表</returns>
    public static async Task<List<FtpListItem>> ListDirectoriesAsync(IConfiguration configuration, string? remoteDirectory = null)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        return await ListDirectoriesAsync(config, remoteDirectory);
    }

    /// <summary>
    /// 列出FTP服务器上的所有项目（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录，默认为空表示根目录）</param>
    /// <returns>所有项目列表</returns>
    public static async Task<List<FtpListItem>> ListAllAsync(IConfiguration configuration, string? remoteDirectory = null)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        return await ListAllAsync(config, remoteDirectory);
    }

    /// <summary>
    /// 创建FTP目录（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录）</param>
    /// <returns>任务</returns>
    public static async Task CreateDirectoryAsync(IConfiguration configuration, string remoteDirectory)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        await CreateDirectoryAsync(config, remoteDirectory);
    }

    /// <summary>
    /// 删除FTP目录（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteDirectory">远程目录路径（相对于BasePath或FTP根目录）</param>
    /// <returns>任务</returns>
    public static async Task DeleteDirectoryAsync(IConfiguration configuration, string remoteDirectory)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        await DeleteDirectoryAsync(config, remoteDirectory);
    }

    /// <summary>
    /// 获取FTP文件大小（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <returns>文件大小（字节），如果文件不存在返回-1</returns>
    public static async Task<long> GetFileSizeAsync(IConfiguration configuration, string remoteFilePath)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        return await GetFileSizeAsync(config, remoteFilePath);
    }

    /// <summary>
    /// 测试FTP连接（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <returns>连接是否成功</returns>
    public static async Task<bool> TestConnectionAsync(IConfiguration configuration)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        return await TestConnectionAsync(config);
    }

    /// <summary>
    /// 重命名或移动FTP服务器上的文件或目录（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remotePath">远程文件或目录路径（相对于BasePath或FTP根目录）</param>
    /// <param name="newRemotePath">新的远程文件或目录路径（相对于BasePath或FTP根目录）</param>
    /// <returns>任务</returns>
    public static async Task RenameAsync(IConfiguration configuration, string remotePath, string newRemotePath)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        await RenameAsync(config, remotePath, newRemotePath);
    }

    /// <summary>
    /// 获取FTP服务器上文件或目录的修改时间（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remotePath">远程文件或目录路径（相对于BasePath或FTP根目录）</param>
    /// <returns>修改时间，如果文件或目录不存在返回null</returns>
    public static async Task<DateTime?> GetModifiedTimeAsync(IConfiguration configuration, string remotePath)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        return await GetModifiedTimeAsync(config, remotePath);
    }

    /// <summary>
    /// 使用流上传文件到FTP服务器（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="stream">文件流</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <param name="overwrite">是否覆盖已存在的文件，默认为true</param>
    /// <returns>任务</returns>
    public static async Task UploadStreamAsync(IConfiguration configuration, Stream stream, string remoteFilePath, bool overwrite = true)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        await UploadStreamAsync(config, stream, remoteFilePath, overwrite);
    }

    /// <summary>
    /// 使用流从FTP服务器下载文件（使用配置）
    /// </summary>
    /// <param name="configuration">配置</param>
    /// <param name="remoteFilePath">远程文件路径（相对于BasePath或FTP根目录）</param>
    /// <param name="stream">目标流</param>
    /// <returns>任务</returns>
    public static async Task DownloadStreamAsync(IConfiguration configuration, string remoteFilePath, Stream stream)
    {
        var config = GetFtpConfigFromConfiguration(configuration);
        await DownloadStreamAsync(config, remoteFilePath, stream);
    }

    /// <summary>
    /// 构建完整远程路径（包含BasePath）
    /// </summary>
    /// <param name="config">FTP配置</param>
    /// <param name="remotePath">远程路径</param>
    /// <returns>完整远程路径</returns>
    private static string BuildRemotePath(FtpConfig config, string remotePath)
    {
        if (string.IsNullOrWhiteSpace(remotePath))
            return config.BasePath ?? "/";

        // 标准化路径分隔符
        remotePath = remotePath.Replace('\\', '/').TrimStart('/');

        // 如果有BasePath，则组合路径
        if (!string.IsNullOrWhiteSpace(config.BasePath))
        {
            var basePath = config.BasePath.TrimEnd('/');
            return $"{basePath}/{remotePath}";
        }

        return remotePath;
    }
}
