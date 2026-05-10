// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Tasks.File.DownloadEngine
// 文件名称：TaktDownloadsController.cs
// 创建时间：2026-05-06
// 创建人：Takt365
// 功能描述：文件下载引擎控制器，提供文件下载相关的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Tasks.File;
using Takt.Application.Services.Routine.Tasks.File.DownloadEngine;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Tasks.File.DownloadEngine;

/// <summary>
/// 文件下载引擎控制器
/// </summary>
[Route("api/[controller]", Name = "文件下载")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:tasks:file:download", "文件下载管理")]
public class TaktDownloadsController : TaktControllerBase
{
    private readonly ITaktDownloadEngineService _downloadEngineService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDownloadsController(
        ITaktDownloadEngineService downloadEngineService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _downloadEngineService = downloadEngineService;
    }

    /// <summary>
    /// 获取文件下载信息（自动更新下载统计和位置）
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="location">下载位置（可选）</param>
    /// <returns>文件信息</returns>
    [HttpGet("{fileId}")]
    [TaktPermission("routine:tasks:file:download", "获取文件下载信息")]
    public async Task<IActionResult> GetDownloadableFileAsync(long fileId, [FromQuery] string? location = null)
    {
        try
        {
            var fileDto = await _downloadEngineService.GetDownloadableFileAsync(fileId, location);
            return Ok(fileDto);
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 变更文件公开状态
    /// </summary>
    /// <param name="dto">文件公开状态变更DTO</param>
    /// <returns>任务</returns>
    [HttpPost("change-public-status")]
    [TaktPermission("routine:tasks:file:manage", "管理文件公开状态")]
    public async Task<IActionResult> ChangePublicStatusAsync([FromBody] TaktFilePublicChangeDto dto)
    {
        try
        {
            await _downloadEngineService.ChangePublicStatusAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 更新文件标签
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="fileTags">文件标签（逗号分隔）</param>
    /// <returns>任务</returns>
    [HttpPut("{fileId}/tags")]
    [TaktPermission("routine:tasks:file:update", "更新文件标签")]
    public async Task<IActionResult> UpdateFileTagsAsync(long fileId, [FromBody] string? fileTags)
    {
        try
        {
            await _downloadEngineService.UpdateFileTagsAsync(fileId, fileTags);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 更新文件访问权限配置
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="accessPermissionConfig">访问权限配置（JSON格式）</param>
    /// <returns>任务</returns>
    [HttpPut("{fileId}/permission-config")]
    [TaktPermission("routine:tasks:file:manage", "管理文件访问权限")]
    public async Task<IActionResult> UpdateAccessPermissionConfigAsync(long fileId, [FromBody] string? accessPermissionConfig)
    {
        try
        {
            await _downloadEngineService.UpdateAccessPermissionConfigAsync(fileId, accessPermissionConfig);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }

    /// <summary>
    /// 检查文件访问权限
    /// </summary>
    /// <param name="fileId">文件ID</param>
    /// <param name="userId">用户ID（可选）</param>
    /// <returns>是否有权访问</returns>
    [HttpGet("{fileId}/check-permission")]
    public async Task<IActionResult> CheckFileAccessPermissionAsync(long fileId, [FromQuery] long? userId = null)
    {
        try
        {
            var hasPermission = await _downloadEngineService.CheckFileAccessPermissionAsync(fileId, userId);
            return Ok(new { hasPermission });
        }
        catch (Exception ex)
        {
            return BadRequest(GetLocalizedExceptionMessage(ex));
        }
    }
}
