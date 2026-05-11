// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Accounting.Controlling
// 文件名称：TaktCostElementsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：成本要素表控制器，提供CostElement管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Accounting.Controlling;
using Takt.Application.Services.Accounting.Controlling;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Accounting.Controlling;

/// <summary>
/// 成本要素表控制器
/// </summary>
[Route("api/[controller]", Name = "成本要素表")]
[ApiModule("Accounting", "财务管理")]
[TaktPermission("accounting:controlling:costelement", "成本要素表管理")]
public class TaktCostElementsController : TaktControllerBase
{
    private readonly ITaktCostElementService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCostElementsController(
        ITaktCostElementService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取成本要素表(CostElement)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("accounting:controlling:costelement:list", "查询成本要素表(CostElement)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktCostElementDto>>> GetCostElementListAsync([FromQuery] TaktCostElementQueryDto queryDto)
    {
        var result = await _service.GetCostElementListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取成本要素表(CostElement)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("accounting:controlling:costelement:query", "查询成本要素表(CostElement)详情")]
    public async Task<ActionResult<TaktCostElementDto>> GetCostElementByIdAsync(long id)
    {
        var item = await _service.GetCostElementByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取成本要素表(CostElement)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("accounting:controlling:costelement:query", "查询成本要素表(CostElement)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetCostElementOptionsAsync()
    {
        var result = await _service.GetCostElementOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取成本要素表(CostElement)树形选项列表（用于树形下拉框等）
    /// </summary>
    [HttpGet("tree-options")]
    [TaktPermission("accounting:controlling:costelement:query", "查询成本要素表(CostElement)树形选项")]
    public async Task<ActionResult<List<TaktTreeSelectOption>>> GetCostElementTreeOptionsAsync()
    {
        var result = await _service.GetCostElementTreeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 获取成本要素表(CostElement)树形列表
    /// </summary>
    [HttpGet("tree")]
    [TaktPermission("accounting:controlling:costelement:query", "查询成本要素表(CostElement)树形")]
    public async Task<ActionResult<List<TaktCostElementTreeDto>>> GetCostElementTreeAsync([FromQuery] long parentId = 0, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetCostElementTreeAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 获取成本要素表(CostElement)子节点列表
    /// </summary>
    [HttpGet("children")]
    [TaktPermission("accounting:controlling:costelement:query", "查询成本要素表(CostElement)子节点")]
    public async Task<ActionResult<List<TaktCostElementDto>>> GetCostElementChildrenAsync([FromQuery] long parentId, [FromQuery] bool includeDisabled = false)
    {
        var result = await _service.GetCostElementChildrenAsync(parentId, includeDisabled);
        return Ok(result);
    }


    /// <summary>
    /// 创建成本要素表(CostElement)
    /// </summary>
    [HttpPost]
    [TaktPermission("accounting:controlling:costelement:create", "创建成本要素表(CostElement)")]
    public async Task<ActionResult<TaktCostElementDto>> CreateCostElementAsync([FromBody] TaktCostElementCreateDto dto)
    {
        var result = await _service.CreateCostElementAsync(dto);
        return CreatedAtAction(nameof(GetCostElementByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新成本要素表(CostElement)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("accounting:controlling:costelement:update", "更新成本要素表(CostElement)")]
    public async Task<ActionResult<TaktCostElementDto>> UpdateCostElementAsync(long id, [FromBody] TaktCostElementUpdateDto dto)
    {
        var result = await _service.UpdateCostElementAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除成本要素表(CostElement)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("accounting:controlling:costelement:delete", "删除成本要素表(CostElement)")]
    public async Task<ActionResult> DeleteCostElementByIdAsync(long id)
    {
        await _service.DeleteCostElementByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除成本要素表(CostElement)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("accounting:controlling:costelement:delete", "批量删除成本要素表(CostElement)")]
    public async Task<ActionResult> DeleteCostElementBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteCostElementBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新成本要素表(CostElement)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("accounting:controlling:costelement:update", "更新成本要素表(CostElement)状态")]
    public async Task<ActionResult<TaktCostElementDto>> UpdateCostElementStatusAsync([FromBody] TaktCostElementStatusDto dto)
    {
        var result = await _service.UpdateCostElementStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新成本要素表(CostElement)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("accounting:controlling:costelement:update", "更新成本要素表(CostElement)排序")]
    public async Task<ActionResult<TaktCostElementDto>> UpdateCostElementSortAsync([FromBody] TaktCostElementSortDto dto)
    {
        var result = await _service.UpdateCostElementSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取成本要素表(CostElement)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("accounting:controlling:costelement:import", "获取成本要素表(CostElement)导入模板")]
    public async Task<IActionResult> GetCostElementTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetCostElementTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入成本要素表(CostElement)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("accounting:controlling:costelement:import", "导入成本要素表(CostElement)")]
    public async Task<ActionResult<object>> ImportCostElementAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportCostElementAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出成本要素表(CostElement)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("accounting:controlling:costelement:export", "导出成本要素表(CostElement)")]
    public async Task<IActionResult> ExportCostElementAsync([FromBody] TaktCostElementQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportCostElementAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
