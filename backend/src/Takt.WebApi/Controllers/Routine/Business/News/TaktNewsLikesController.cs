// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.News
// 文件名称：TaktNewsLikesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻点赞记录表控制器，提供NewsLike管理的RESTful API接口
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
/// 新闻点赞记录表控制器
/// </summary>
[Route("api/[controller]", Name = "新闻点赞记录表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:news:newslike", "新闻点赞记录表管理")]
public class TaktNewsLikesController : TaktControllerBase
{
    private readonly ITaktNewsLikeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsLikesController(
        ITaktNewsLikeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取新闻点赞记录表(NewsLike)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:news:newslike:list", "查询新闻点赞记录表(NewsLike)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNewsLikeDto>>> GetNewsLikeListAsync([FromQuery] TaktNewsLikeQueryDto queryDto)
    {
        var result = await _service.GetNewsLikeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取新闻点赞记录表(NewsLike)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:news:newslike:query", "查询新闻点赞记录表(NewsLike)详情")]
    public async Task<ActionResult<TaktNewsLikeDto>> GetNewsLikeByIdAsync(long id)
    {
        var item = await _service.GetNewsLikeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取新闻点赞记录表(NewsLike)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:news:newslike:query", "查询新闻点赞记录表(NewsLike)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetNewsLikeOptionsAsync()
    {
        var result = await _service.GetNewsLikeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建新闻点赞记录表(NewsLike)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:news:newslike:create", "创建新闻点赞记录表(NewsLike)")]
    public async Task<ActionResult<TaktNewsLikeDto>> CreateNewsLikeAsync([FromBody] TaktNewsLikeCreateDto dto)
    {
        var result = await _service.CreateNewsLikeAsync(dto);
        return CreatedAtAction(nameof(GetNewsLikeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新新闻点赞记录表(NewsLike)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:news:newslike:update", "更新新闻点赞记录表(NewsLike)")]
    public async Task<ActionResult<TaktNewsLikeDto>> UpdateNewsLikeAsync(long id, [FromBody] TaktNewsLikeUpdateDto dto)
    {
        var result = await _service.UpdateNewsLikeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除新闻点赞记录表(NewsLike)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:news:newslike:delete", "删除新闻点赞记录表(NewsLike)")]
    public async Task<ActionResult> DeleteNewsLikeByIdAsync(long id)
    {
        await _service.DeleteNewsLikeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除新闻点赞记录表(NewsLike)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:news:newslike:delete", "批量删除新闻点赞记录表(NewsLike)")]
    public async Task<ActionResult> DeleteNewsLikeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteNewsLikeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取新闻点赞记录表(NewsLike)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:news:newslike:import", "获取新闻点赞记录表(NewsLike)导入模板")]
    public async Task<IActionResult> GetNewsLikeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetNewsLikeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入新闻点赞记录表(NewsLike)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:news:newslike:import", "导入新闻点赞记录表(NewsLike)")]
    public async Task<ActionResult<object>> ImportNewsLikeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportNewsLikeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出新闻点赞记录表(NewsLike)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:news:newslike:export", "导出新闻点赞记录表(NewsLike)")]
    public async Task<IActionResult> ExportNewsLikeAsync([FromBody] TaktNewsLikeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportNewsLikeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
