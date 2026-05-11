// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktInspectionStandardItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：检验标准明细表控制器，提供InspectionStandardItem管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Application.Services.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Operation;

/// <summary>
/// 检验标准明细表控制器
/// </summary>
[Route("api/[controller]", Name = "检验标准明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:inspectionstandarditem", "检验标准明细表管理")]
public class TaktInspectionStandardItemsController : TaktControllerBase
{
    private readonly ITaktInspectionStandardItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardItemsController(
        ITaktInspectionStandardItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取检验标准明细表(InspectionStandardItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:list", "查询检验标准明细表(InspectionStandardItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktInspectionStandardItemDto>>> GetInspectionStandardItemListAsync([FromQuery] TaktInspectionStandardItemQueryDto queryDto)
    {
        var result = await _service.GetInspectionStandardItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取检验标准明细表(InspectionStandardItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:query", "查询检验标准明细表(InspectionStandardItem)详情")]
    public async Task<ActionResult<TaktInspectionStandardItemDto>> GetInspectionStandardItemByIdAsync(long id)
    {
        var item = await _service.GetInspectionStandardItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取检验标准明细表(InspectionStandardItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:query", "查询检验标准明细表(InspectionStandardItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetInspectionStandardItemOptionsAsync()
    {
        var result = await _service.GetInspectionStandardItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建检验标准明细表(InspectionStandardItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:create", "创建检验标准明细表(InspectionStandardItem)")]
    public async Task<ActionResult<TaktInspectionStandardItemDto>> CreateInspectionStandardItemAsync([FromBody] TaktInspectionStandardItemCreateDto dto)
    {
        var result = await _service.CreateInspectionStandardItemAsync(dto);
        return CreatedAtAction(nameof(GetInspectionStandardItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新检验标准明细表(InspectionStandardItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:update", "更新检验标准明细表(InspectionStandardItem)")]
    public async Task<ActionResult<TaktInspectionStandardItemDto>> UpdateInspectionStandardItemAsync(long id, [FromBody] TaktInspectionStandardItemUpdateDto dto)
    {
        var result = await _service.UpdateInspectionStandardItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除检验标准明细表(InspectionStandardItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:delete", "删除检验标准明细表(InspectionStandardItem)")]
    public async Task<ActionResult> DeleteInspectionStandardItemByIdAsync(long id)
    {
        await _service.DeleteInspectionStandardItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除检验标准明细表(InspectionStandardItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:delete", "批量删除检验标准明细表(InspectionStandardItem)")]
    public async Task<ActionResult> DeleteInspectionStandardItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteInspectionStandardItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取检验标准明细表(InspectionStandardItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:import", "获取检验标准明细表(InspectionStandardItem)导入模板")]
    public async Task<IActionResult> GetInspectionStandardItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetInspectionStandardItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入检验标准明细表(InspectionStandardItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:import", "导入检验标准明细表(InspectionStandardItem)")]
    public async Task<ActionResult<object>> ImportInspectionStandardItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportInspectionStandardItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出检验标准明细表(InspectionStandardItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:inspectionstandarditem:export", "导出检验标准明细表(InspectionStandardItem)")]
    public async Task<IActionResult> ExportInspectionStandardItemAsync([FromBody] TaktInspectionStandardItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportInspectionStandardItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
