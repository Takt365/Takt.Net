// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Code.Generator
// 文件名称：TaktGenTablesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：代码生成表配置表控制器，提供GenTable管理的RESTful API接口
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
/// 代码生成表配置表控制器
/// </summary>
[Route("api/[controller]", Name = "代码生成表配置表")]
[ApiModule("Code", "代码生成")]
[TaktPermission("code:generator:gentable", "代码生成表配置表管理")]
public class TaktGenTablesController : TaktControllerBase
{
    private readonly ITaktGenTableService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTablesController(
        ITaktGenTableService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取代码生成表配置表(GenTable)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("code:generator:gentable:list", "查询代码生成表配置表(GenTable)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktGenTableDto>>> GetGenTableListAsync([FromQuery] TaktGenTableQueryDto queryDto)
    {
        var result = await _service.GetGenTableListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取代码生成表配置表(GenTable)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("code:generator:gentable:query", "查询代码生成表配置表(GenTable)详情")]
    public async Task<ActionResult<TaktGenTableDto>> GetGenTableByIdAsync(long id)
    {
        var item = await _service.GetGenTableByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取代码生成表配置表(GenTable)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("code:generator:gentable:query", "查询代码生成表配置表(GenTable)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetGenTableOptionsAsync()
    {
        var result = await _service.GetGenTableOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建代码生成表配置表(GenTable)
    /// </summary>
    [HttpPost]
    [TaktPermission("code:generator:gentable:create", "创建代码生成表配置表(GenTable)")]
    public async Task<ActionResult<TaktGenTableDto>> CreateGenTableAsync([FromBody] TaktGenTableCreateDto dto)
    {
        var result = await _service.CreateGenTableAsync(dto);
        return CreatedAtAction(nameof(GetGenTableByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新代码生成表配置表(GenTable)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("code:generator:gentable:update", "更新代码生成表配置表(GenTable)")]
    public async Task<ActionResult<TaktGenTableDto>> UpdateGenTableAsync(long id, [FromBody] TaktGenTableUpdateDto dto)
    {
        var result = await _service.UpdateGenTableAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除代码生成表配置表(GenTable)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("code:generator:gentable:delete", "删除代码生成表配置表(GenTable)")]
    public async Task<ActionResult> DeleteGenTableByIdAsync(long id)
    {
        await _service.DeleteGenTableByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除代码生成表配置表(GenTable)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("code:generator:gentable:delete", "批量删除代码生成表配置表(GenTable)")]
    public async Task<ActionResult> DeleteGenTableBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteGenTableBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取代码生成表配置表(GenTable)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("code:generator:gentable:import", "获取代码生成表配置表(GenTable)导入模板")]
    public async Task<IActionResult> GetGenTableTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetGenTableTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入代码生成表配置表(GenTable)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("code:generator:gentable:import", "导入代码生成表配置表(GenTable)")]
    public async Task<ActionResult<object>> ImportGenTableAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportGenTableAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出代码生成表配置表(GenTable)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("code:generator:gentable:export", "导出代码生成表配置表(GenTable)")]
    public async Task<IActionResult> ExportGenTableAsync([FromBody] TaktGenTableQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportGenTableAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
