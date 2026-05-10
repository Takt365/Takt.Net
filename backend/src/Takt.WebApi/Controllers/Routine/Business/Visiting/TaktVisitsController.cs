// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Routine.Business.Visiting
// 文件名称：TaktVisitsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：参访公司表控制器，提供Visit管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Business.Visiting;
using Takt.Application.Services.Routine.Business.Visiting;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Routine.Business.Visiting;

/// <summary>
/// 参访公司表控制器
/// </summary>
[Route("api/[controller]", Name = "参访公司表")]
[ApiModule("Routine", "日常事务")]
[TaktPermission("routine:business:visiting:visit", "参访公司表管理")]
public class TaktVisitsController : TaktControllerBase
{
    private readonly ITaktVisitService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktVisitsController(
        ITaktVisitService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取参访公司表(Visit)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("routine:business:visiting:visit:list", "查询参访公司表(Visit)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktVisitDto>>> GetVisitListAsync([FromQuery] TaktVisitQueryDto queryDto)
    {
        var result = await _service.GetVisitListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取参访公司表(Visit)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("routine:business:visiting:visit:query", "查询参访公司表(Visit)详情")]
    public async Task<ActionResult<TaktVisitDto>> GetVisitByIdAsync(long id)
    {
        var item = await _service.GetVisitByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取参访公司表(Visit)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("routine:business:visiting:visit:query", "查询参访公司表(Visit)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetVisitOptionsAsync()
    {
        var result = await _service.GetVisitOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建参访公司表(Visit)
    /// </summary>
    [HttpPost]
    [TaktPermission("routine:business:visiting:visit:create", "创建参访公司表(Visit)")]
    public async Task<ActionResult<TaktVisitDto>> CreateVisitAsync([FromBody] TaktVisitCreateDto dto)
    {
        var result = await _service.CreateVisitAsync(dto);
        return CreatedAtAction(nameof(GetVisitByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新参访公司表(Visit)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("routine:business:visiting:visit:update", "更新参访公司表(Visit)")]
    public async Task<ActionResult<TaktVisitDto>> UpdateVisitAsync(long id, [FromBody] TaktVisitUpdateDto dto)
    {
        var result = await _service.UpdateVisitAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除参访公司表(Visit)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("routine:business:visiting:visit:delete", "删除参访公司表(Visit)")]
    public async Task<ActionResult> DeleteVisitByIdAsync(long id)
    {
        await _service.DeleteVisitByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除参访公司表(Visit)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("routine:business:visiting:visit:delete", "批量删除参访公司表(Visit)")]
    public async Task<ActionResult> DeleteVisitBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteVisitBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取参访公司表(Visit)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("routine:business:visiting:visit:import", "获取参访公司表(Visit)导入模板")]
    public async Task<IActionResult> GetVisitTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetVisitTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入参访公司表(Visit)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("routine:business:visiting:visit:import", "导入参访公司表(Visit)")]
    public async Task<ActionResult<object>> ImportVisitAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportVisitAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出参访公司表(Visit)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("routine:business:visiting:visit:export", "导出参访公司表(Visit)")]
    public async Task<IActionResult> ExportVisitAsync([FromBody] TaktVisitQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportVisitAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
