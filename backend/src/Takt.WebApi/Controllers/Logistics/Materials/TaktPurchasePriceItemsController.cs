// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchasePriceItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购价格明细表控制器，提供PurchasePriceItem管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Materials;

/// <summary>
/// 采购价格明细表控制器
/// </summary>
[Route("api/[controller]", Name = "采购价格明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchasepriceitem", "采购价格明细表管理")]
public class TaktPurchasePriceItemsController : TaktControllerBase
{
    private readonly ITaktPurchasePriceItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceItemsController(
        ITaktPurchasePriceItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购价格明细表(PurchasePriceItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchasepriceitem:list", "查询采购价格明细表(PurchasePriceItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchasePriceItemDto>>> GetPurchasePriceItemListAsync([FromQuery] TaktPurchasePriceItemQueryDto queryDto)
    {
        var result = await _service.GetPurchasePriceItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购价格明细表(PurchasePriceItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchasepriceitem:query", "查询采购价格明细表(PurchasePriceItem)详情")]
    public async Task<ActionResult<TaktPurchasePriceItemDto>> GetPurchasePriceItemByIdAsync(long id)
    {
        var item = await _service.GetPurchasePriceItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购价格明细表(PurchasePriceItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchasepriceitem:query", "查询采购价格明细表(PurchasePriceItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchasePriceItemOptionsAsync()
    {
        var result = await _service.GetPurchasePriceItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购价格明细表(PurchasePriceItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchasepriceitem:create", "创建采购价格明细表(PurchasePriceItem)")]
    public async Task<ActionResult<TaktPurchasePriceItemDto>> CreatePurchasePriceItemAsync([FromBody] TaktPurchasePriceItemCreateDto dto)
    {
        var result = await _service.CreatePurchasePriceItemAsync(dto);
        return CreatedAtAction(nameof(GetPurchasePriceItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购价格明细表(PurchasePriceItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchasepriceitem:update", "更新采购价格明细表(PurchasePriceItem)")]
    public async Task<ActionResult<TaktPurchasePriceItemDto>> UpdatePurchasePriceItemAsync(long id, [FromBody] TaktPurchasePriceItemUpdateDto dto)
    {
        var result = await _service.UpdatePurchasePriceItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购价格明细表(PurchasePriceItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchasepriceitem:delete", "删除采购价格明细表(PurchasePriceItem)")]
    public async Task<ActionResult> DeletePurchasePriceItemByIdAsync(long id)
    {
        await _service.DeletePurchasePriceItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购价格明细表(PurchasePriceItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchasepriceitem:delete", "批量删除采购价格明细表(PurchasePriceItem)")]
    public async Task<ActionResult> DeletePurchasePriceItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchasePriceItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新采购价格明细表(PurchasePriceItem)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:materials:purchasepriceitem:update", "更新采购价格明细表(PurchasePriceItem)排序")]
    public async Task<ActionResult<TaktPurchasePriceItemDto>> UpdatePurchasePriceItemSortAsync([FromBody] TaktPurchasePriceItemSortDto dto)
    {
        var result = await _service.UpdatePurchasePriceItemSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取采购价格明细表(PurchasePriceItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchasepriceitem:import", "获取采购价格明细表(PurchasePriceItem)导入模板")]
    public async Task<IActionResult> GetPurchasePriceItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchasePriceItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购价格明细表(PurchasePriceItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchasepriceitem:import", "导入采购价格明细表(PurchasePriceItem)")]
    public async Task<ActionResult<object>> ImportPurchasePriceItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchasePriceItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购价格明细表(PurchasePriceItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchasepriceitem:export", "导出采购价格明细表(PurchasePriceItem)")]
    public async Task<IActionResult> ExportPurchasePriceItemAsync([FromBody] TaktPurchasePriceItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchasePriceItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
