// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.News
// 文件名称：TaktNewsCommentLikesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻评论点赞记录表控制器，提供NewsCommentLike管理的RESTful API接口
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
/// 新闻评论点赞记录表控制器
/// </summary>
[Route("api/[controller]", Name = "新闻评论点赞记录表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:news:newscommentlike", "新闻评论点赞记录表管理")]
public class TaktNewsCommentLikesController : TaktControllerBase
{
    private readonly ITaktNewsCommentLikeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentLikesController(
        ITaktNewsCommentLikeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取新闻评论点赞记录表(NewsCommentLike)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:news:newscommentlike:list", "查询新闻评论点赞记录表(NewsCommentLike)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNewsCommentLikeDto>>> GetNewsCommentLikeListAsync([FromQuery] TaktNewsCommentLikeQueryDto queryDto)
    {
        var result = await _service.GetNewsCommentLikeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取新闻评论点赞记录表(NewsCommentLike)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:news:newscommentlike:query", "查询新闻评论点赞记录表(NewsCommentLike)详情")]
    public async Task<ActionResult<TaktNewsCommentLikeDto>> GetNewsCommentLikeByIdAsync(long id)
    {
        var item = await _service.GetNewsCommentLikeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取新闻评论点赞记录表(NewsCommentLike)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:news:newscommentlike:query", "查询新闻评论点赞记录表(NewsCommentLike)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetNewsCommentLikeOptionsAsync()
    {
        var result = await _service.GetNewsCommentLikeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建新闻评论点赞记录表(NewsCommentLike)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:news:newscommentlike:create", "创建新闻评论点赞记录表(NewsCommentLike)")]
    public async Task<ActionResult<TaktNewsCommentLikeDto>> CreateNewsCommentLikeAsync([FromBody] TaktNewsCommentLikeCreateDto dto)
    {
        var result = await _service.CreateNewsCommentLikeAsync(dto);
        return CreatedAtAction(nameof(GetNewsCommentLikeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新新闻评论点赞记录表(NewsCommentLike)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:news:newscommentlike:update", "更新新闻评论点赞记录表(NewsCommentLike)")]
    public async Task<ActionResult<TaktNewsCommentLikeDto>> UpdateNewsCommentLikeAsync(long id, [FromBody] TaktNewsCommentLikeUpdateDto dto)
    {
        var result = await _service.UpdateNewsCommentLikeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除新闻评论点赞记录表(NewsCommentLike)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:news:newscommentlike:delete", "删除新闻评论点赞记录表(NewsCommentLike)")]
    public async Task<ActionResult> DeleteNewsCommentLikeByIdAsync(long id)
    {
        await _service.DeleteNewsCommentLikeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除新闻评论点赞记录表(NewsCommentLike)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:news:newscommentlike:delete", "批量删除新闻评论点赞记录表(NewsCommentLike)")]
    public async Task<ActionResult> DeleteNewsCommentLikeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteNewsCommentLikeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取新闻评论点赞记录表(NewsCommentLike)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:news:newscommentlike:import", "获取新闻评论点赞记录表(NewsCommentLike)导入模板")]
    public async Task<IActionResult> GetNewsCommentLikeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetNewsCommentLikeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入新闻评论点赞记录表(NewsCommentLike)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:news:newscommentlike:import", "导入新闻评论点赞记录表(NewsCommentLike)")]
    public async Task<ActionResult<object>> ImportNewsCommentLikeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportNewsCommentLikeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出新闻评论点赞记录表(NewsCommentLike)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:news:newscommentlike:export", "导出新闻评论点赞记录表(NewsCommentLike)")]
    public async Task<IActionResult> ExportNewsCommentLikeAsync([FromBody] TaktNewsCommentLikeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportNewsCommentLikeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
