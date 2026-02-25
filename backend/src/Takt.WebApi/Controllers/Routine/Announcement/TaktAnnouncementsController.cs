// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Announcement
// 文件名称：TaktAnnouncementsController.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：公告控制器，提供公告管理的 RESTful API
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Announcement;
using Takt.Application.Services.Routine.Announcement;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Announcement;

/// <summary>
/// 公告控制器
/// </summary>
[Route("api/[controller]", Name = "公告")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:announcement", "公告管理")]
public class TaktAnnouncementsController : TaktControllerBase
{
    private readonly ITaktAnnouncementService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktAnnouncementsController(
        ITaktAnnouncementService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取公告列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:announcement:list", "查询公告列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAnnouncementDto>>> GetListAsync([FromQuery] TaktAnnouncementQueryDto queryDto)
    {
        var result = await _service.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据ID获取公告
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:announcement:query", "查询公告详情")]
    public async Task<ActionResult<TaktAnnouncementDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 创建公告
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:announcement:create", "创建公告")]
    public async Task<ActionResult<TaktAnnouncementDto>> CreateAsync([FromBody] TaktAnnouncementCreateDto dto)
    {
        var item = await _service.CreateAsync(dto);
        return Ok(item);
    }

    /// <summary>
    /// 更新公告
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:announcement:update", "更新公告")]
    public async Task<ActionResult<TaktAnnouncementDto>> UpdateAsync(long id, [FromBody] TaktAnnouncementUpdateDto dto)
    {
        try
        {
            var item = await _service.UpdateAsync(id, dto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 删除公告
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:announcement:delete", "删除公告")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// 批量删除公告
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:announcement:delete", "批量删除公告")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] IEnumerable<long> ids)
    {
        await _service.DeleteAsync(ids);
        return NoContent();
    }

    /// <summary>
    /// 更新公告状态（发布/撤回等）
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:announcement:update", "更新公告状态")]
    public async Task<ActionResult<TaktAnnouncementDto>> UpdateStatusAsync([FromBody] TaktAnnouncementStatusDto dto)
    {
        var item = await _service.UpdateStatusAsync(dto);
        return Ok(item);
    }

    /// <summary>
    /// 获取公告附件列表
    /// </summary>
    [HttpGet("{id}/attachments")]
    [TaktPermission("routine:announcement:query", "查询公告附件")]
    public async Task<ActionResult<List<TaktAnnouncementAttachmentDto>>> GetAttachmentsAsync(long id)
    {
        var list = await _service.GetAttachmentsAsync(id);
        return Ok(list);
    }

    /// <summary>
    /// 获取公告阅读记录列表（分页）
    /// </summary>
    [HttpGet("{id}/reads")]
    [TaktPermission("routine:announcement:query", "查询公告阅读记录")]
    public async Task<ActionResult<TaktPagedResult<TaktAnnouncementReadDto>>> GetReadsAsync(long id, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _service.GetReadsAsync(id, pageIndex, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// 记录阅读（用户阅读公告时调用）
    /// </summary>
    [HttpPost("{id}/read")]
    [TaktPermission("routine:announcement:read", "阅读公告")]
    public async Task<IActionResult> RecordReadAsync(long id, [FromQuery] int readDurationSeconds = 0)
    {
        await _service.RecordReadAsync(id, readDurationSeconds);
        return NoContent();
    }
}
