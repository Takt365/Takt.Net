// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.News
// 文件名称：TaktNewsFavoritesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻收藏记录表控制器，提供NewsFavorite管理的RESTful API接口
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
/// 新闻收藏记录表控制器
/// </summary>
[Route("api/[controller]", Name = "新闻收藏记录表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:news:newsfavorite", "新闻收藏记录表管理")]
public class TaktNewsFavoritesController : TaktControllerBase
{
    private readonly ITaktNewsFavoriteService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsFavoritesController(
        ITaktNewsFavoriteService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取新闻收藏记录表(NewsFavorite)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:news:newsfavorite:list", "查询新闻收藏记录表(NewsFavorite)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNewsFavoriteDto>>> GetNewsFavoriteListAsync([FromQuery] TaktNewsFavoriteQueryDto queryDto)
    {
        var result = await _service.GetNewsFavoriteListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取新闻收藏记录表(NewsFavorite)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:news:newsfavorite:query", "查询新闻收藏记录表(NewsFavorite)详情")]
    public async Task<ActionResult<TaktNewsFavoriteDto>> GetNewsFavoriteByIdAsync(long id)
    {
        var item = await _service.GetNewsFavoriteByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取新闻收藏记录表(NewsFavorite)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:news:newsfavorite:query", "查询新闻收藏记录表(NewsFavorite)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetNewsFavoriteOptionsAsync()
    {
        var result = await _service.GetNewsFavoriteOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建新闻收藏记录表(NewsFavorite)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:news:newsfavorite:create", "创建新闻收藏记录表(NewsFavorite)")]
    public async Task<ActionResult<TaktNewsFavoriteDto>> CreateNewsFavoriteAsync([FromBody] TaktNewsFavoriteCreateDto dto)
    {
        var result = await _service.CreateNewsFavoriteAsync(dto);
        return CreatedAtAction(nameof(GetNewsFavoriteByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新新闻收藏记录表(NewsFavorite)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:news:newsfavorite:update", "更新新闻收藏记录表(NewsFavorite)")]
    public async Task<ActionResult<TaktNewsFavoriteDto>> UpdateNewsFavoriteAsync(long id, [FromBody] TaktNewsFavoriteUpdateDto dto)
    {
        var result = await _service.UpdateNewsFavoriteAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除新闻收藏记录表(NewsFavorite)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:news:newsfavorite:delete", "删除新闻收藏记录表(NewsFavorite)")]
    public async Task<ActionResult> DeleteNewsFavoriteByIdAsync(long id)
    {
        await _service.DeleteNewsFavoriteByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除新闻收藏记录表(NewsFavorite)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:news:newsfavorite:delete", "批量删除新闻收藏记录表(NewsFavorite)")]
    public async Task<ActionResult> DeleteNewsFavoriteBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteNewsFavoriteBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取新闻收藏记录表(NewsFavorite)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:news:newsfavorite:import", "获取新闻收藏记录表(NewsFavorite)导入模板")]
    public async Task<IActionResult> GetNewsFavoriteTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetNewsFavoriteTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入新闻收藏记录表(NewsFavorite)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:news:newsfavorite:import", "导入新闻收藏记录表(NewsFavorite)")]
    public async Task<ActionResult<object>> ImportNewsFavoriteAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportNewsFavoriteAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出新闻收藏记录表(NewsFavorite)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:news:newsfavorite:export", "导出新闻收藏记录表(NewsFavorite)")]
    public async Task<IActionResult> ExportNewsFavoriteAsync([FromBody] TaktNewsFavoriteQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportNewsFavoriteAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
