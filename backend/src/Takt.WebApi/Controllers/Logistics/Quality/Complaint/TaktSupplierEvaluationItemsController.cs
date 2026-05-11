// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Complaint
// 文件名称：TaktSupplierEvaluationItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：供应商评价考核项目明细表控制器，提供SupplierEvaluationItem管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Complaint;
using Takt.Application.Services.Logistics.Quality.Complaint;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Complaint;

/// <summary>
/// 供应商评价考核项目明细表控制器
/// </summary>
[Route("api/[controller]", Name = "供应商评价考核项目明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:complaint:supplierevaluationitem", "供应商评价考核项目明细表管理")]
public class TaktSupplierEvaluationItemsController : TaktControllerBase
{
    private readonly ITaktSupplierEvaluationItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSupplierEvaluationItemsController(
        ITaktSupplierEvaluationItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取供应商评价考核项目明细表(SupplierEvaluationItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:list", "查询供应商评价考核项目明细表(SupplierEvaluationItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSupplierEvaluationItemDto>>> GetSupplierEvaluationItemListAsync([FromQuery] TaktSupplierEvaluationItemQueryDto queryDto)
    {
        var result = await _service.GetSupplierEvaluationItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:query", "查询供应商评价考核项目明细表(SupplierEvaluationItem)详情")]
    public async Task<ActionResult<TaktSupplierEvaluationItemDto>> GetSupplierEvaluationItemByIdAsync(long id)
    {
        var item = await _service.GetSupplierEvaluationItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取供应商评价考核项目明细表(SupplierEvaluationItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:query", "查询供应商评价考核项目明细表(SupplierEvaluationItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSupplierEvaluationItemOptionsAsync()
    {
        var result = await _service.GetSupplierEvaluationItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:create", "创建供应商评价考核项目明细表(SupplierEvaluationItem)")]
    public async Task<ActionResult<TaktSupplierEvaluationItemDto>> CreateSupplierEvaluationItemAsync([FromBody] TaktSupplierEvaluationItemCreateDto dto)
    {
        var result = await _service.CreateSupplierEvaluationItemAsync(dto);
        return CreatedAtAction(nameof(GetSupplierEvaluationItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:update", "更新供应商评价考核项目明细表(SupplierEvaluationItem)")]
    public async Task<ActionResult<TaktSupplierEvaluationItemDto>> UpdateSupplierEvaluationItemAsync(long id, [FromBody] TaktSupplierEvaluationItemUpdateDto dto)
    {
        var result = await _service.UpdateSupplierEvaluationItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:delete", "删除供应商评价考核项目明细表(SupplierEvaluationItem)")]
    public async Task<ActionResult> DeleteSupplierEvaluationItemByIdAsync(long id)
    {
        await _service.DeleteSupplierEvaluationItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:delete", "批量删除供应商评价考核项目明细表(SupplierEvaluationItem)")]
    public async Task<ActionResult> DeleteSupplierEvaluationItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSupplierEvaluationItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新供应商评价考核项目明细表(SupplierEvaluationItem)Rectification
    /// </summary>
    [HttpPut("status-rectification")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:update", "更新供应商评价考核项目明细表(SupplierEvaluationItem)Rectification")]
    public async Task<ActionResult<TaktSupplierEvaluationItemDto>> UpdateSupplierEvaluationItemRectificationStatusAsync([FromBody] TaktSupplierEvaluationItemRectificationStatusDto dto)
    {
        var result = await _service.UpdateSupplierEvaluationItemRectificationStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新供应商评价考核项目明细表(SupplierEvaluationItem)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:update", "更新供应商评价考核项目明细表(SupplierEvaluationItem)排序")]
    public async Task<ActionResult<TaktSupplierEvaluationItemDto>> UpdateSupplierEvaluationItemSortAsync([FromBody] TaktSupplierEvaluationItemSortDto dto)
    {
        var result = await _service.UpdateSupplierEvaluationItemSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取供应商评价考核项目明细表(SupplierEvaluationItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:import", "获取供应商评价考核项目明细表(SupplierEvaluationItem)导入模板")]
    public async Task<IActionResult> GetSupplierEvaluationItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSupplierEvaluationItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:import", "导入供应商评价考核项目明细表(SupplierEvaluationItem)")]
    public async Task<ActionResult<object>> ImportSupplierEvaluationItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSupplierEvaluationItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出供应商评价考核项目明细表(SupplierEvaluationItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:complaint:supplierevaluationitem:export", "导出供应商评价考核项目明细表(SupplierEvaluationItem)")]
    public async Task<IActionResult> ExportSupplierEvaluationItemAsync([FromBody] TaktSupplierEvaluationItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSupplierEvaluationItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
