// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.Announcement
// 文件名称：TaktAnnouncementsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：公告表控制器，提供Announcement管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
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
/// 公告表控制器
/// </summary>
[Route("api/[controller]", Name = "公告表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:announcement", "公告表管理")]
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
    /// 获取公告表(Announcement)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:announcement:list", "查询公告表(Announcement)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktAnnouncementDto>>> GetAnnouncementListAsync([FromQuery] TaktAnnouncementQueryDto queryDto)
    {
        var result = await _service.GetAnnouncementListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取公告表(Announcement)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:announcement:query", "查询公告表(Announcement)详情")]
    public async Task<ActionResult<TaktAnnouncementDto>> GetAnnouncementByIdAsync(long id)
    {
        var item = await _service.GetAnnouncementByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取公告表(Announcement)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:announcement:query", "查询公告表(Announcement)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetAnnouncementOptionsAsync()
    {
        var result = await _service.GetAnnouncementOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建公告表(Announcement)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:announcement:create", "创建公告表(Announcement)")]
    public async Task<ActionResult<TaktAnnouncementDto>> CreateAnnouncementAsync([FromBody] TaktAnnouncementCreateDto dto)
    {
        var result = await _service.CreateAnnouncementAsync(dto);
        return CreatedAtAction(nameof(GetAnnouncementByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新公告表(Announcement)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:announcement:update", "更新公告表(Announcement)")]
    public async Task<ActionResult<TaktAnnouncementDto>> UpdateAnnouncementAsync(long id, [FromBody] TaktAnnouncementUpdateDto dto)
    {
        var result = await _service.UpdateAnnouncementAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除公告表(Announcement)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:announcement:delete", "删除公告表(Announcement)")]
    public async Task<ActionResult> DeleteAnnouncementByIdAsync(long id)
    {
        await _service.DeleteAnnouncementByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除公告表(Announcement)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:announcement:delete", "批量删除公告表(Announcement)")]
    public async Task<ActionResult> DeleteAnnouncementBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteAnnouncementBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新公告表(Announcement)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:business:announcement:update", "更新公告表(Announcement)状态")]
    public async Task<ActionResult<TaktAnnouncementDto>> UpdateAnnouncementStatusAsync([FromBody] TaktAnnouncementStatusDto dto)
    {
        var result = await _service.UpdateAnnouncementStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新公告表(Announcement)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:business:announcement:update", "更新公告表(Announcement)排序")]
    public async Task<ActionResult<TaktAnnouncementDto>> UpdateAnnouncementSortAsync([FromBody] TaktAnnouncementSortDto dto)
    {
        var result = await _service.UpdateAnnouncementSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取公告表(Announcement)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:announcement:import", "获取公告表(Announcement)导入模板")]
    public async Task<IActionResult> GetAnnouncementTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetAnnouncementTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入公告表(Announcement)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:announcement:import", "导入公告表(Announcement)")]
    public async Task<ActionResult<object>> ImportAnnouncementAsync(IFormFile file, [FromForm] string? sheetName = null)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(GetLocalizedString("validation.importExcelFileRequired", "Frontend"));
        }

        if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) &&
            !file.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(GetLocalizedString("validation.importExcelOnlyXlsxOrXls", "Frontend"));
        }

        using var stream = file.OpenReadStream();
        var (success, fail, errors) = await _service.ImportAnnouncementAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出公告表(Announcement)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:announcement:export", "导出公告表(Announcement)")]
    public async Task<IActionResult> ExportAnnouncementAsync([FromBody] TaktAnnouncementQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportAnnouncementAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
