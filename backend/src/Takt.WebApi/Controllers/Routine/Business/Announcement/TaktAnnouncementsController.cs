// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.Announcement
// 文件名称：TaktAnnouncementsController.cs
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt公告通知控制器，提供公告管理的RESTful API接口
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Business.Announcement;
using Takt.Application.Services.Routine.Business.Announcement;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Business.Announcement;

/// <summary>
/// 公告通知控制器
/// </summary>
[Route("api/[controller]", Name = "公告通知")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:announcement", "公告通知管理")]
public class TaktAnnouncementsController : TaktControllerBase
{
    private readonly ITaktAnnouncementService _announcementService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementsController(
        ITaktAnnouncementService announcementService,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _announcementService = announcementService;
    }

    /// <summary>
    /// 获取公告列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:announcement:list", "查询公告列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAnnouncementDto>>> GetAnnouncementListAsync([FromQuery] TaktAnnouncementQueryDto queryDto)
    {
        var result = await _announcementService.GetAnnouncementListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取公告
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:announcement:query", "查询公告详情")]
    public async Task<ActionResult<TaktAnnouncementDto>> GetAnnouncementByIdAsync(long id)
    {
        var item = await _announcementService.GetAnnouncementByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 创建公告
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:announcement:create", "创建公告")]
    public async Task<ActionResult<TaktAnnouncementDto>> CreateAnnouncementAsync([FromBody] TaktAnnouncementCreateDto dto)
    {
        try
        {
            var result = await _announcementService.CreateAnnouncementAsync(dto);
            return CreatedAtAction(nameof(GetAnnouncementByIdAsync), new { id = result.AnnouncementId }, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 更新公告
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:announcement:update", "更新公告")]
    public async Task<ActionResult<TaktAnnouncementDto>> UpdateAnnouncementAsync(long id, [FromBody] TaktAnnouncementUpdateDto dto)
    {
        try
        {
            var result = await _announcementService.UpdateAnnouncementAsync(id, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 删除公告
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:announcement:delete", "删除公告")]
    public async Task<IActionResult> DeleteAnnouncementByIdAsync(long id)
    {
        try
        {
            await _announcementService.DeleteAnnouncementByIdAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 批量删除公告
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:announcement:delete", "批量删除公告")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] List<long> ids)
    {
        await _announcementService.DeleteAnnouncementBatchAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新公告状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:business:announcement:status", "更新公告状态")]
    public async Task<ActionResult<TaktAnnouncementDto>> UpdateAnnouncementStatusAsync([FromBody] TaktAnnouncementStatusDto dto)
    {
        try
        {
            var result = await _announcementService.UpdateAnnouncementStatusAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    /// <summary>
    /// 导出公告
    /// </summary>
    /// <returns>Excel 文件；超过 <c>TaktExcelHelper.ExportAsync</c> 单表行数上限时为 zip 打包（基础设施统一逻辑）</returns>
    [HttpPost("export")]
    [TaktPermission("routine:business:announcement:export", "导出公告")]
    public async Task<IActionResult> ExportAnnouncementAsync([FromBody] TaktAnnouncementQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _announcementService.ExportAnnouncementAsync(query, sheetName, fileName);
            return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
