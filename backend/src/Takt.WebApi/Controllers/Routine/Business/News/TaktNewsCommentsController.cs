// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.News
// 文件名称：TaktNewsCommentsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻评论表控制器，提供NewsComment管理的RESTful API接口
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
/// 新闻评论表控制器
/// </summary>
[Route("api/[controller]", Name = "新闻评论表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:news:newscomment", "新闻评论表管理")]
public class TaktNewsCommentsController : TaktControllerBase
{
    private readonly ITaktNewsCommentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsCommentsController(
        ITaktNewsCommentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:news:newscomment:list", "查询新闻评论表(NewsComment)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNewsCommentDto>>> GetNewsCommentListAsync([FromQuery] TaktNewsCommentQueryDto queryDto)
    {
        var result = await _service.GetNewsCommentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取新闻评论表(NewsComment)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:news:newscomment:query", "查询新闻评论表(NewsComment)详情")]
    public async Task<ActionResult<TaktNewsCommentDto>> GetNewsCommentByIdAsync(long id)
    {
        var item = await _service.GetNewsCommentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:news:newscomment:query", "查询新闻评论表(NewsComment)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetNewsCommentOptionsAsync()
    {
        var result = await _service.GetNewsCommentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)树形选项列表（用于树形下拉框等）
    /// </summary>
    [HttpGet("tree-options")]
    [TaktPermission("routine:business:news:newscomment:query", "查询新闻评论表(NewsComment)树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetNewsCommentTreeOptionsAsync()
    {
        var result = await _service.GetNewsCommentTreeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)树形列表
    /// </summary>
    [HttpGet("tree")]
    [TaktPermission("routine:business:news:newscomment:query", "查询新闻评论表(NewsComment)树形")]
    public async Task<ActionResult<List<TaktNewsCommentTreeDto>>> GetNewsCommentTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetNewsCommentTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)子节点列表
    /// </summary>
    [HttpGet("children")]
    [TaktPermission("routine:business:news:newscomment:query", "查询新闻评论表(NewsComment)子节点")]
    public async Task<ActionResult<List<TaktNewsCommentDto>>> GetNewsCommentChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetNewsCommentChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 创建新闻评论表(NewsComment)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:news:newscomment:create", "创建新闻评论表(NewsComment)")]
    public async Task<ActionResult<TaktNewsCommentDto>> CreateNewsCommentAsync([FromBody] TaktNewsCommentCreateDto dto)
    {
        var result = await _service.CreateNewsCommentAsync(dto);
        return CreatedAtAction(nameof(GetNewsCommentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新新闻评论表(NewsComment)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:news:newscomment:update", "更新新闻评论表(NewsComment)")]
    public async Task<ActionResult<TaktNewsCommentDto>> UpdateNewsCommentAsync(long id, [FromBody] TaktNewsCommentUpdateDto dto)
    {
        var result = await _service.UpdateNewsCommentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除新闻评论表(NewsComment)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:news:newscomment:delete", "删除新闻评论表(NewsComment)")]
    public async Task<ActionResult> DeleteNewsCommentByIdAsync(long id)
    {
        await _service.DeleteNewsCommentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除新闻评论表(NewsComment)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:news:newscomment:delete", "批量删除新闻评论表(NewsComment)")]
    public async Task<ActionResult> DeleteNewsCommentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteNewsCommentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新新闻评论表(NewsComment)Approval
    /// </summary>
    [HttpPut("status-approval")]
    [TaktPermission("routine:business:news:newscomment:update", "更新新闻评论表(NewsComment)Approval")]
    public async Task<ActionResult<TaktNewsCommentDto>> UpdateNewsCommentApprovalStatusAsync([FromBody] TaktNewsCommentApprovalStatusDto dto)
    {
        var result = await _service.UpdateNewsCommentApprovalStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新新闻评论表(NewsComment)Comment
    /// </summary>
    [HttpPut("status-comment")]
    [TaktPermission("routine:business:news:newscomment:update", "更新新闻评论表(NewsComment)Comment")]
    public async Task<ActionResult<TaktNewsCommentDto>> UpdateNewsCommentCommentStatusAsync([FromBody] TaktNewsCommentCommentStatusDto dto)
    {
        var result = await _service.UpdateNewsCommentCommentStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取新闻评论表(NewsComment)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:news:newscomment:import", "获取新闻评论表(NewsComment)导入模板")]
    public async Task<IActionResult> GetNewsCommentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetNewsCommentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入新闻评论表(NewsComment)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:news:newscomment:import", "导入新闻评论表(NewsComment)")]
    public async Task<ActionResult<object>> ImportNewsCommentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportNewsCommentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出新闻评论表(NewsComment)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:news:newscomment:export", "导出新闻评论表(NewsComment)")]
    public async Task<IActionResult> ExportNewsCommentAsync([FromBody] TaktNewsCommentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportNewsCommentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
