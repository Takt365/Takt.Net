// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityScrapItemsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：品质废弃明细表控制器，提供QualityScrapItem管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Application.Services.Logistics.Quality.Cost;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Cost;

/// <summary>
/// 品质废弃明细表控制器
/// </summary>
[Route("api/[controller]", Name = "品质废弃明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityscrapitem", "品质废弃明细表管理")]
public class TaktQualityScrapItemsController : TaktControllerBase
{
    private readonly ITaktQualityScrapItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityScrapItemsController(
        ITaktQualityScrapItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质废弃明细表(QualityScrapItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:list", "查询品质废弃明细表(QualityScrapItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityScrapItemDto>>> GetQualityScrapItemListAsync([FromQuery] TaktQualityScrapItemQueryDto queryDto)
    {
        var result = await _service.GetQualityScrapItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质废弃明细表(QualityScrapItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:query", "查询品质废弃明细表(QualityScrapItem)详情")]
    public async Task<ActionResult<TaktQualityScrapItemDto>> GetQualityScrapItemByIdAsync(long id)
    {
        var item = await _service.GetQualityScrapItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质废弃明细表(QualityScrapItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:query", "查询品质废弃明细表(QualityScrapItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityScrapItemOptionsAsync()
    {
        var result = await _service.GetQualityScrapItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质废弃明细表(QualityScrapItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:create", "创建品质废弃明细表(QualityScrapItem)")]
    public async Task<ActionResult<TaktQualityScrapItemDto>> CreateQualityScrapItemAsync([FromBody] TaktQualityScrapItemCreateDto dto)
    {
        var result = await _service.CreateQualityScrapItemAsync(dto);
        return CreatedAtAction(nameof(GetQualityScrapItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质废弃明细表(QualityScrapItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:update", "更新品质废弃明细表(QualityScrapItem)")]
    public async Task<ActionResult<TaktQualityScrapItemDto>> UpdateQualityScrapItemAsync(long id, [FromBody] TaktQualityScrapItemUpdateDto dto)
    {
        var result = await _service.UpdateQualityScrapItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质废弃明细表(QualityScrapItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:delete", "删除品质废弃明细表(QualityScrapItem)")]
    public async Task<ActionResult> DeleteQualityScrapItemByIdAsync(long id)
    {
        await _service.DeleteQualityScrapItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质废弃明细表(QualityScrapItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:delete", "批量删除品质废弃明细表(QualityScrapItem)")]
    public async Task<ActionResult> DeleteQualityScrapItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityScrapItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质废弃明细表(QualityScrapItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:import", "获取品质废弃明细表(QualityScrapItem)导入模板")]
    public async Task<IActionResult> GetQualityScrapItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityScrapItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质废弃明细表(QualityScrapItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:import", "导入品质废弃明细表(QualityScrapItem)")]
    public async Task<ActionResult<object>> ImportQualityScrapItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityScrapItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质废弃明细表(QualityScrapItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityscrapitem:export", "导出品质废弃明细表(QualityScrapItem)")]
    public async Task<IActionResult> ExportQualityScrapItemAsync([FromBody] TaktQualityScrapItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityScrapItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
