// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.News
// 文件名称：TaktNewsReadsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻阅读记录表控制器，提供NewsRead管理的RESTful API接口
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
/// 新闻阅读记录表控制器
/// </summary>
[Route("api/[controller]", Name = "新闻阅读记录表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:news:newsread", "新闻阅读记录表管理")]
public class TaktNewsReadsController : TaktControllerBase
{
    private readonly ITaktNewsReadService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewsReadsController(
        ITaktNewsReadService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取新闻阅读记录表(NewsRead)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:news:newsread:list", "查询新闻阅读记录表(NewsRead)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNewsReadDto>>> GetNewsReadListAsync([FromQuery] TaktNewsReadQueryDto queryDto)
    {
        var result = await _service.GetNewsReadListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取新闻阅读记录表(NewsRead)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:news:newsread:query", "查询新闻阅读记录表(NewsRead)详情")]
    public async Task<ActionResult<TaktNewsReadDto>> GetNewsReadByIdAsync(long id)
    {
        var item = await _service.GetNewsReadByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取新闻阅读记录表(NewsRead)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:news:newsread:query", "查询新闻阅读记录表(NewsRead)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetNewsReadOptionsAsync()
    {
        var result = await _service.GetNewsReadOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建新闻阅读记录表(NewsRead)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:news:newsread:create", "创建新闻阅读记录表(NewsRead)")]
    public async Task<ActionResult<TaktNewsReadDto>> CreateNewsReadAsync([FromBody] TaktNewsReadCreateDto dto)
    {
        var result = await _service.CreateNewsReadAsync(dto);
        return CreatedAtAction(nameof(GetNewsReadByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新新闻阅读记录表(NewsRead)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:news:newsread:update", "更新新闻阅读记录表(NewsRead)")]
    public async Task<ActionResult<TaktNewsReadDto>> UpdateNewsReadAsync(long id, [FromBody] TaktNewsReadUpdateDto dto)
    {
        var result = await _service.UpdateNewsReadAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除新闻阅读记录表(NewsRead)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:news:newsread:delete", "删除新闻阅读记录表(NewsRead)")]
    public async Task<ActionResult> DeleteNewsReadByIdAsync(long id)
    {
        await _service.DeleteNewsReadByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除新闻阅读记录表(NewsRead)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:news:newsread:delete", "批量删除新闻阅读记录表(NewsRead)")]
    public async Task<ActionResult> DeleteNewsReadBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteNewsReadBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取新闻阅读记录表(NewsRead)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:news:newsread:import", "获取新闻阅读记录表(NewsRead)导入模板")]
    public async Task<IActionResult> GetNewsReadTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetNewsReadTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入新闻阅读记录表(NewsRead)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:news:newsread:import", "导入新闻阅读记录表(NewsRead)")]
    public async Task<ActionResult<object>> ImportNewsReadAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportNewsReadAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出新闻阅读记录表(NewsRead)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:news:newsread:export", "导出新闻阅读记录表(NewsRead)")]
    public async Task<IActionResult> ExportNewsReadAsync([FromBody] TaktNewsReadQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportNewsReadAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
