// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.File.DownloadEngine
// 文件名称：ITaktDownloadEngineService.cs
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：Takt文件下载引擎服务接口
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.File;
using Takt.Domain.Entities.Routine.Tasks.File;

namespace Takt.Application.Services.Routine.Tasks.File.DownloadEngine;

/// <summary>
/// Takt文件下载引擎服务接口
/// </summary>
public interface ITaktDownloadEngineService
{
    /// <summary>
    /// 获取可下载的文件信息（验证文件存在性和权限，自动更新下载统计和位置）
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="location">下载位置（IP地址或地理位置，可选）</param>
    /// <returns>文件DTO</returns>
    Task<TaktFileDto> GetDownloadableFileAsync(long fileId, string? location = null);

    /// <summary>
    /// 变更文件公开状态
    /// </summary>
    /// <param name="dto">文件公开状态变更DTO</param>
    /// <returns>任务</returns>
    Task ChangePublicStatusAsync(TaktFilePublicChangeDto dto);

    /// <summary>
    /// 更新文件标签
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="fileTags">文件标签（逗号分隔）</param>
    /// <returns>任务</returns>
    Task UpdateFileTagsAsync(long fileId, string? fileTags);

    /// <summary>
    /// 更新文件访问权限配置
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="accessPermissionConfig">访问权限配置（JSON格式）</param>
    /// <returns>任务</returns>
    Task UpdateAccessPermissionConfigAsync(long fileId, string? accessPermissionConfig);

    /// <summary>
    /// 检查文件访问权限
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="userId">用户ID（可选，null表示未登录用户）</param>
    /// <returns>是否有权访问</returns>
    Task<bool> CheckFileAccessPermissionAsync(long fileId, long? userId = null);
}
