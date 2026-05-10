// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.News
// 文件名称：TaktNewssController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：新闻表控制器，提供News管理的RESTful API接口
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
/// 新闻表控制器
/// </summary>
[Route("api/[controller]", Name = "新闻表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:news", "新闻表管理")]
public class TaktNewssController : TaktControllerBase
{
    private readonly ITaktNewsService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktNewssController(
        ITaktNewsService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取新闻表(News)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:news:list", "查询新闻表(News)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktNewsDto>>> GetNewsListAsync([FromQuery] TaktNewsQueryDto queryDto)
    {
        var result = await _service.GetNewsListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取新闻表(News)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:news:query", "查询新闻表(News)详情")]
    public async Task<ActionResult<TaktNewsDto>> GetNewsByIdAsync(long id)
    {
        var item = await _service.GetNewsByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取新闻表(News)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:news:query", "查询新闻表(News)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetNewsOptionsAsync()
    {
        var result = await _service.GetNewsOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建新闻表(News)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:news:create", "创建新闻表(News)")]
    public async Task<ActionResult<TaktNewsDto>> CreateNewsAsync([FromBody] TaktNewsCreateDto dto)
    {
        var result = await _service.CreateNewsAsync(dto);
        return CreatedAtAction(nameof(GetNewsByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新新闻表(News)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:news:update", "更新新闻表(News)")]
    public async Task<ActionResult<TaktNewsDto>> UpdateNewsAsync(long id, [FromBody] TaktNewsUpdateDto dto)
    {
        var result = await _service.UpdateNewsAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除新闻表(News)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:news:delete", "删除新闻表(News)")]
    public async Task<ActionResult> DeleteNewsByIdAsync(long id)
    {
        await _service.DeleteNewsByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除新闻表(News)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:news:delete", "批量删除新闻表(News)")]
    public async Task<ActionResult> DeleteNewsBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteNewsBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新新闻表(News)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("routine:business:news:update", "更新新闻表(News)状态")]
    public async Task<ActionResult<TaktNewsDto>> UpdateNewsStatusAsync([FromBody] TaktNewsStatusDto dto)
    {
        var result = await _service.UpdateNewsStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新新闻表(News)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("routine:business:news:update", "更新新闻表(News)排序")]
    public async Task<ActionResult<TaktNewsDto>> UpdateNewsSortAsync([FromBody] TaktNewsSortDto dto)
    {
        var result = await _service.UpdateNewsSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取新闻表(News)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:news:import", "获取新闻表(News)导入模板")]
    public async Task<IActionResult> GetNewsTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetNewsTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入新闻表(News)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:news:import", "导入新闻表(News)")]
    public async Task<ActionResult<object>> ImportNewsAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportNewsAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出新闻表(News)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:news:export", "导出新闻表(News)")]
    public async Task<IActionResult> ExportNewsAsync([FromBody] TaktNewsQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportNewsAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
