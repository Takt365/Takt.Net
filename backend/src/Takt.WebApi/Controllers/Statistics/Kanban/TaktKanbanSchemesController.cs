// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Statistics.Kanban
// 文件名称：TaktKanbanSchemesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：看板方案表控制器，提供KanbanScheme管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Statistics.Kanban;
using Takt.Application.Services.Statistics.Kanban;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Statistics.Kanban;

/// <summary>
/// 看板方案表控制器
/// </summary>
[Route("api/[controller]", Name = "看板方案表")]
[ApiModule("Statistics", "统计分析")]
[TaktPermission("statistics:kanban:kanbanscheme", "看板方案表管理")]
public class TaktKanbanSchemesController : TaktControllerBase
{
    private readonly ITaktKanbanSchemeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanSchemesController(
        ITaktKanbanSchemeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取看板方案表(KanbanScheme)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("statistics:kanban:kanbanscheme:list", "查询看板方案表(KanbanScheme)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktKanbanSchemeDto>>> GetKanbanSchemeListAsync([FromQuery] TaktKanbanSchemeQueryDto queryDto)
    {
        var result = await _service.GetKanbanSchemeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取看板方案表(KanbanScheme)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("statistics:kanban:kanbanscheme:query", "查询看板方案表(KanbanScheme)详情")]
    public async Task<ActionResult<TaktKanbanSchemeDto>> GetKanbanSchemeByIdAsync(long id)
    {
        var item = await _service.GetKanbanSchemeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取看板方案表(KanbanScheme)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("statistics:kanban:kanbanscheme:query", "查询看板方案表(KanbanScheme)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetKanbanSchemeOptionsAsync()
    {
        var result = await _service.GetKanbanSchemeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建看板方案表(KanbanScheme)
    /// </summary>
    [HttpPost]
    [TaktPermission("statistics:kanban:kanbanscheme:create", "创建看板方案表(KanbanScheme)")]
    public async Task<ActionResult<TaktKanbanSchemeDto>> CreateKanbanSchemeAsync([FromBody] TaktKanbanSchemeCreateDto dto)
    {
        var result = await _service.CreateKanbanSchemeAsync(dto);
        return CreatedAtAction(nameof(GetKanbanSchemeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新看板方案表(KanbanScheme)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("statistics:kanban:kanbanscheme:update", "更新看板方案表(KanbanScheme)")]
    public async Task<ActionResult<TaktKanbanSchemeDto>> UpdateKanbanSchemeAsync(long id, [FromBody] TaktKanbanSchemeUpdateDto dto)
    {
        var result = await _service.UpdateKanbanSchemeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除看板方案表(KanbanScheme)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("statistics:kanban:kanbanscheme:delete", "删除看板方案表(KanbanScheme)")]
    public async Task<ActionResult> DeleteKanbanSchemeByIdAsync(long id)
    {
        await _service.DeleteKanbanSchemeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除看板方案表(KanbanScheme)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("statistics:kanban:kanbanscheme:delete", "批量删除看板方案表(KanbanScheme)")]
    public async Task<ActionResult> DeleteKanbanSchemeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteKanbanSchemeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新看板方案表(KanbanScheme)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("statistics:kanban:kanbanscheme:update", "更新看板方案表(KanbanScheme)状态")]
    public async Task<ActionResult<TaktKanbanSchemeDto>> UpdateKanbanSchemeStatusAsync([FromBody] TaktKanbanSchemeStatusDto dto)
    {
        var result = await _service.UpdateKanbanSchemeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取看板方案表(KanbanScheme)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("statistics:kanban:kanbanscheme:import", "获取看板方案表(KanbanScheme)导入模板")]
    public async Task<IActionResult> GetKanbanSchemeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetKanbanSchemeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入看板方案表(KanbanScheme)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("statistics:kanban:kanbanscheme:import", "导入看板方案表(KanbanScheme)")]
    public async Task<ActionResult<object>> ImportKanbanSchemeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportKanbanSchemeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出看板方案表(KanbanScheme)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("statistics:kanban:kanbanscheme:export", "导出看板方案表(KanbanScheme)")]
    public async Task<IActionResult> ExportKanbanSchemeAsync([FromBody] TaktKanbanSchemeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportKanbanSchemeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
