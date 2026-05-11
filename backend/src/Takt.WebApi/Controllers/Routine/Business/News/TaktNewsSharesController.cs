// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.News
// 文件名称：TaktNewsSharesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻分享记录表控制器，提供NewsShare管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Business.News;
using Takt.Application.Services.Routine.Business.News;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Business.News;

/// <summary>
/// 新闻分享记录表控制器
/// </summary>
[Route("api/[controller]", Name = "新闻分享记录表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:news:newsshare", "新闻分享记录表管理")]
public class TaktNewsSharesController : TaktControllerBase
{
    private readonly ITaktNewsShareService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsSharesController(
        ITaktNewsShareService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取新闻分享记录表(NewsShare)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:news:newsshare:list", "查询新闻分享记录表(NewsShare)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNewsShareDto>>> GetNewsShareListAsync([FromQuery] TaktNewsShareQueryDto queryDto)
    {
        var result = await _service.GetNewsShareListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取新闻分享记录表(NewsShare)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:news:newsshare:query", "查询新闻分享记录表(NewsShare)详情")]
    public async Task<ActionResult<TaktNewsShareDto>> GetNewsShareByIdAsync(long id)
    {
        var item = await _service.GetNewsShareByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取新闻分享记录表(NewsShare)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:news:newsshare:query", "查询新闻分享记录表(NewsShare)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetNewsShareOptionsAsync()
    {
        var result = await _service.GetNewsShareOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建新闻分享记录表(NewsShare)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:news:newsshare:create", "创建新闻分享记录表(NewsShare)")]
    public async Task<ActionResult<TaktNewsShareDto>> CreateNewsShareAsync([FromBody] TaktNewsShareCreateDto dto)
    {
        var result = await _service.CreateNewsShareAsync(dto);
        return CreatedAtAction(nameof(GetNewsShareByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新新闻分享记录表(NewsShare)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:news:newsshare:update", "更新新闻分享记录表(NewsShare)")]
    public async Task<ActionResult<TaktNewsShareDto>> UpdateNewsShareAsync(long id, [FromBody] TaktNewsShareUpdateDto dto)
    {
        var result = await _service.UpdateNewsShareAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除新闻分享记录表(NewsShare)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:news:newsshare:delete", "删除新闻分享记录表(NewsShare)")]
    public async Task<ActionResult> DeleteNewsShareByIdAsync(long id)
    {
        await _service.DeleteNewsShareByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除新闻分享记录表(NewsShare)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:news:newsshare:delete", "批量删除新闻分享记录表(NewsShare)")]
    public async Task<ActionResult> DeleteNewsShareBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteNewsShareBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取新闻分享记录表(NewsShare)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:news:newsshare:import", "获取新闻分享记录表(NewsShare)导入模板")]
    public async Task<IActionResult> GetNewsShareTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetNewsShareTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入新闻分享记录表(NewsShare)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:news:newsshare:import", "导入新闻分享记录表(NewsShare)")]
    public async Task<ActionResult<object>> ImportNewsShareAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportNewsShareAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出新闻分享记录表(NewsShare)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:news:newsshare:export", "导出新闻分享记录表(NewsShare)")]
    public async Task<IActionResult> ExportNewsShareAsync([FromBody] TaktNewsShareQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportNewsShareAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
