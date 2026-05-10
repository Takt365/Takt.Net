// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Application.Services.Routine.Tasks.File.DownloadEngine
// 文件名称：TaktDownloadEngineService.cs
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：Takt文件下载引擎服务，处理文件下载相关逻辑
// 
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos.Routine.Tasks.File;
using Takt.Domain.Entities.Routine.Tasks.File;
using Takt.Domain.Interfaces;
using Takt.Domain.Repositories;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Routine.Tasks.File.DownloadEngine;

/// <summary>
/// Takt文件下载引擎服务
/// </summary>
public class TaktDownloadEngineService : ITaktDownloadEngineService
{
    private readonly ITaktRepository<TaktFile> _fileRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fileRepository">文件仓储</param>
    /// <param name="userContext">用户上下文（可选）</param>
    /// <param name="tenantContext">租户上下文（可选）</param>
    /// <param name="localizer">本地化器（可选）</param>
    public TaktDownloadEngineService(
        ITaktRepository<TaktFile> fileRepository,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
    {
        _fileRepository = fileRepository;
    }

    /// <summary>
    /// 获取可下载的文件信息（验证文件存在性和权限，自动更新下载统计和位置）
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="location">下载位置（IP地址或地理位置，可选）</param>
    /// <returns>文件DTO</returns>
    public async Task<TaktFileDto> GetDownloadableFileAsync(long fileId, string? location = null)
    {
        var entity = await _fileRepository.GetByIdAsync(fileId)
            ?? throw new TaktBusinessException("validation.fileNotFound");

        // 检查文件状态
        if (entity.FileStatus != 1)
        {
            throw new TaktBusinessException("validation.fileNotAvailable");
        }

        // 检查是否被删除
        if (entity.IsDeleted == 1)
        {
            throw new TaktBusinessException("validation.fileNotFound");
        }

        // 自动更新下载统计和位置
        entity.DownloadCount++;
        entity.LastDownloadTime = DateTime.Now;
        if (!string.IsNullOrWhiteSpace(location))
        {
            entity.Location = location;
        }
        await _fileRepository.UpdateAsync(entity);

        return entity.Adapt<TaktFileDto>();
    }

    /// <summary>
    /// 变更文件公开状态
    /// </summary>
    /// <param name="dto">文件公开状态变更DTO</param>
    /// <returns>任务</returns>
    public async Task ChangePublicStatusAsync(TaktFilePublicChangeDto dto)
    {
        var entity = await _fileRepository.GetByIdAsync(dto.FileId)
            ?? throw new TaktBusinessException("validation.fileNotFound");

        entity.IsPublic = dto.IsPublic;

        await _fileRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 更新文件标签
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="fileTags">文件标签（逗号分隔）</param>
    /// <returns>任务</returns>
    public async Task UpdateFileTagsAsync(long fileId, string? fileTags)
    {
        var entity = await _fileRepository.GetByIdAsync(fileId)
            ?? throw new TaktBusinessException("validation.fileNotFound");

        entity.FileTags = fileTags;
        await _fileRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 更新文件访问权限配置
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="accessPermissionConfig">访问权限配置（JSON格式）</param>
    /// <returns>任务</returns>
    public async Task UpdateAccessPermissionConfigAsync(long fileId, string? accessPermissionConfig)
    {
        var entity = await _fileRepository.GetByIdAsync(fileId)
            ?? throw new TaktBusinessException("validation.fileNotFound");

        entity.AccessPermissionConfig = accessPermissionConfig;
        await _fileRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 检查文件访问权限
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="userId">用户ID（可选，null表示未登录用户）</param>
    /// <returns>是否有权访问</returns>
    public async Task<bool> CheckFileAccessPermissionAsync(long fileId, long? userId = null)
    {
        var entity = await _fileRepository.GetByIdAsync(fileId);
        if (entity == null || entity.IsDeleted == 1 || entity.FileStatus != 1)
        {
            return false;
        }

        // 公开文件，所有人都可访问
        if (entity.IsPublic == 1)
        {
            return true;
        }

        // 非公开文件，需要登录
        if (!userId.HasValue)
        {
            return false;
        }

        // TODO: 解析 AccessPermissionConfig JSON，检查用户是否有权限
        // 可以根据用户ID、角色ID、部门ID等进行权限判断
        // 当前简化处理：如果是文件创建者，则有权限
        return entity.CreatedById == userId.Value;
    }
}
