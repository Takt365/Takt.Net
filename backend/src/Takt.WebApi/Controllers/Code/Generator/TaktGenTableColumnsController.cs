// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Code.Generator
// 文件名称：TaktGenTableColumnsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成字段配置表控制器，提供GenTableColumn管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Code.Generator;
using Takt.Application.Services.Code.Generator;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Code.Generator;

/// <summary>
/// 代码生成字段配置表控制器
/// </summary>
[Route("api/[controller]", Name = "代码生成字段配置表")]
[ApiModule("Code", "代码生成")]
[TaktPermission("code:generator:gentablecolumn", "代码生成字段配置表管理")]
public class TaktGenTableColumnsController : TaktControllerBase
{
    private readonly ITaktGenTableColumnService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTableColumnsController(
        ITaktGenTableColumnService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取代码生成字段配置表(GenTableColumn)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("code:generator:gentablecolumn:list", "查询代码生成字段配置表(GenTableColumn)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktGenTableColumnDto>>> GetGenTableColumnListAsync([FromQuery] TaktGenTableColumnQueryDto queryDto)
    {
        var result = await _service.GetGenTableColumnListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取代码生成字段配置表(GenTableColumn)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("code:generator:gentablecolumn:query", "查询代码生成字段配置表(GenTableColumn)详情")]
    public async Task<ActionResult<TaktGenTableColumnDto>> GetGenTableColumnByIdAsync(long id)
    {
        var item = await _service.GetGenTableColumnByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取代码生成字段配置表(GenTableColumn)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("code:generator:gentablecolumn:query", "查询代码生成字段配置表(GenTableColumn)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetGenTableColumnOptionsAsync()
    {
        var result = await _service.GetGenTableColumnOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建代码生成字段配置表(GenTableColumn)
    /// </summary>
    [HttpPost]
    [TaktPermission("code:generator:gentablecolumn:create", "创建代码生成字段配置表(GenTableColumn)")]
    public async Task<ActionResult<TaktGenTableColumnDto>> CreateGenTableColumnAsync([FromBody] TaktGenTableColumnCreateDto dto)
    {
        var result = await _service.CreateGenTableColumnAsync(dto);
        return CreatedAtAction(nameof(GetGenTableColumnByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新代码生成字段配置表(GenTableColumn)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("code:generator:gentablecolumn:update", "更新代码生成字段配置表(GenTableColumn)")]
    public async Task<ActionResult<TaktGenTableColumnDto>> UpdateGenTableColumnAsync(long id, [FromBody] TaktGenTableColumnUpdateDto dto)
    {
        var result = await _service.UpdateGenTableColumnAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除代码生成字段配置表(GenTableColumn)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("code:generator:gentablecolumn:delete", "删除代码生成字段配置表(GenTableColumn)")]
    public async Task<ActionResult> DeleteGenTableColumnByIdAsync(long id)
    {
        await _service.DeleteGenTableColumnByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除代码生成字段配置表(GenTableColumn)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("code:generator:gentablecolumn:delete", "批量删除代码生成字段配置表(GenTableColumn)")]
    public async Task<ActionResult> DeleteGenTableColumnBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteGenTableColumnBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新代码生成字段配置表(GenTableColumn)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("code:generator:gentablecolumn:update", "更新代码生成字段配置表(GenTableColumn)排序")]
    public async Task<ActionResult<TaktGenTableColumnDto>> UpdateGenTableColumnSortAsync([FromBody] TaktGenTableColumnSortDto dto)
    {
        var result = await _service.UpdateGenTableColumnSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取代码生成字段配置表(GenTableColumn)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("code:generator:gentablecolumn:import", "获取代码生成字段配置表(GenTableColumn)导入模板")]
    public async Task<IActionResult> GetGenTableColumnTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetGenTableColumnTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入代码生成字段配置表(GenTableColumn)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("code:generator:gentablecolumn:import", "导入代码生成字段配置表(GenTableColumn)")]
    public async Task<ActionResult<object>> ImportGenTableColumnAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportGenTableColumnAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出代码生成字段配置表(GenTableColumn)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("code:generator:gentablecolumn:export", "导出代码生成字段配置表(GenTableColumn)")]
    public async Task<IActionResult> ExportGenTableColumnAsync([FromBody] TaktGenTableColumnQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportGenTableColumnAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
