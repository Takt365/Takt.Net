// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.News
// 文件名称：TaktNewsAttachmentsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻附件表控制器，提供NewsAttachment管理的RESTful API接口
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
/// 新闻附件表控制器
/// </summary>
[Route("api/[controller]", Name = "新闻附件表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:news:newsattachment", "新闻附件表管理")]
public class TaktNewsAttachmentsController : TaktControllerBase
{
    private readonly ITaktNewsAttachmentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsAttachmentsController(
        ITaktNewsAttachmentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取新闻附件表(NewsAttachment)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:news:newsattachment:list", "查询新闻附件表(NewsAttachment)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNewsAttachmentDto>>> GetNewsAttachmentListAsync([FromQuery] TaktNewsAttachmentQueryDto queryDto)
    {
        var result = await _service.GetNewsAttachmentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取新闻附件表(NewsAttachment)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:news:newsattachment:query", "查询新闻附件表(NewsAttachment)详情")]
    public async Task<ActionResult<TaktNewsAttachmentDto>> GetNewsAttachmentByIdAsync(long id)
    {
        var item = await _service.GetNewsAttachmentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取新闻附件表(NewsAttachment)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:news:newsattachment:query", "查询新闻附件表(NewsAttachment)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetNewsAttachmentOptionsAsync()
    {
        var result = await _service.GetNewsAttachmentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建新闻附件表(NewsAttachment)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:news:newsattachment:create", "创建新闻附件表(NewsAttachment)")]
    public async Task<ActionResult<TaktNewsAttachmentDto>> CreateNewsAttachmentAsync([FromBody] TaktNewsAttachmentCreateDto dto)
    {
        var result = await _service.CreateNewsAttachmentAsync(dto);
        return CreatedAtAction(nameof(GetNewsAttachmentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新新闻附件表(NewsAttachment)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:news:newsattachment:update", "更新新闻附件表(NewsAttachment)")]
    public async Task<ActionResult<TaktNewsAttachmentDto>> UpdateNewsAttachmentAsync(long id, [FromBody] TaktNewsAttachmentUpdateDto dto)
    {
        var result = await _service.UpdateNewsAttachmentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除新闻附件表(NewsAttachment)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:news:newsattachment:delete", "删除新闻附件表(NewsAttachment)")]
    public async Task<ActionResult> DeleteNewsAttachmentByIdAsync(long id)
    {
        await _service.DeleteNewsAttachmentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除新闻附件表(NewsAttachment)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:news:newsattachment:delete", "批量删除新闻附件表(NewsAttachment)")]
    public async Task<ActionResult> DeleteNewsAttachmentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteNewsAttachmentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新新闻附件表(NewsAttachment)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:business:news:newsattachment:update", "更新新闻附件表(NewsAttachment)排序")]
    public async Task<ActionResult<TaktNewsAttachmentDto>> UpdateNewsAttachmentSortAsync([FromBody] TaktNewsAttachmentSortDto dto)
    {
        var result = await _service.UpdateNewsAttachmentSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取新闻附件表(NewsAttachment)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:news:newsattachment:import", "获取新闻附件表(NewsAttachment)导入模板")]
    public async Task<IActionResult> GetNewsAttachmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetNewsAttachmentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入新闻附件表(NewsAttachment)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:news:newsattachment:import", "导入新闻附件表(NewsAttachment)")]
    public async Task<ActionResult<object>> ImportNewsAttachmentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportNewsAttachmentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出新闻附件表(NewsAttachment)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:news:newsattachment:export", "导出新闻附件表(NewsAttachment)")]
    public async Task<IActionResult> ExportNewsAttachmentAsync([FromBody] TaktNewsAttachmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportNewsAttachmentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
