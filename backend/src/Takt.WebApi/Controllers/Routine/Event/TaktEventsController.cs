// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Event
// 文件名称：TaktEventsController.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：活动组织（Event）控制器，提供活动管理的 RESTful API
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Event;
using Takt.Application.Services.Routine.Event;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;

namespace Takt.WebApi.Controllers.Routine.Event;

/// <summary>
/// 活动组织控制器
/// </summary>
[Route("api/[controller]", Name = "活动组织")]
[ApiModule("Routine", "常规管理")]
[TaktPermission("routine:event", "活动组织")]
public class TaktEventsController : TaktControllerBase
{
    private readonly ITaktEventService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEventsController(
        ITaktEventService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }

    /// <summary>
    /// 获取活动列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:event:list", "查询活动列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEventDto>>> GetListAsync([FromQuery] TaktEventQueryDto queryDto)
    {
        var result = await _service.GetListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 根据 ID 获取活动
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:event:query", "查询活动详情")]
    public async Task<ActionResult<TaktEventDto>> GetByIdAsync(long id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// 创建活动
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:event:create", "创建活动")]
    public async Task<ActionResult<TaktEventDto>> CreateAsync([FromBody] TaktEventCreateDto dto)
    {
        try
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.EventId }, item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 更新活动
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:event:update", "更新活动")]
    public async Task<ActionResult<TaktEventDto>> UpdateAsync(long id, [FromBody] TaktEventUpdateDto dto)
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
    /// 删除活动
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:event:delete", "删除活动")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除活动
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:event:delete", "批量删除活动")]
    public async Task<IActionResult> DeleteBatchAsync([FromBody] IEnumerable<long> ids)
    {
        try
        {
            await _service.DeleteAsync(ids ?? Array.Empty<long>());
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 导出活动
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:event:export", "导出活动")]
    public async Task<IActionResult> ExportAsync([FromBody] TaktEventQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        try
        {
            var (resultFileName, content) = await _service.ExportAsync(query, sheetName, fileName);
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", resultFileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
