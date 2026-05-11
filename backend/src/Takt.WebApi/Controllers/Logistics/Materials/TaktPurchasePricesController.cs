// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchasePricesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购价格表控制器，提供PurchasePrice管理的RESTful API接口
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
/// 采购价格表控制器
/// </summary>
[Route("api/[controller]", Name = "采购价格表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchaseprice", "采购价格表管理")]
public class TaktPurchasePricesController : TaktControllerBase
{
    private readonly ITaktPurchasePriceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePricesController(
        ITaktPurchasePriceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购价格表(PurchasePrice)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchaseprice:list", "查询采购价格表(PurchasePrice)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchasePriceDto>>> GetPurchasePriceListAsync([FromQuery] TaktPurchasePriceQueryDto queryDto)
    {
        var result = await _service.GetPurchasePriceListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购价格表(PurchasePrice)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchaseprice:query", "查询采购价格表(PurchasePrice)详情")]
    public async Task<ActionResult<TaktPurchasePriceDto>> GetPurchasePriceByIdAsync(long id)
    {
        var item = await _service.GetPurchasePriceByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购价格表(PurchasePrice)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchaseprice:query", "查询采购价格表(PurchasePrice)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchasePriceOptionsAsync()
    {
        var result = await _service.GetPurchasePriceOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购价格表(PurchasePrice)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchaseprice:create", "创建采购价格表(PurchasePrice)")]
    public async Task<ActionResult<TaktPurchasePriceDto>> CreatePurchasePriceAsync([FromBody] TaktPurchasePriceCreateDto dto)
    {
        var result = await _service.CreatePurchasePriceAsync(dto);
        return CreatedAtAction(nameof(GetPurchasePriceByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购价格表(PurchasePrice)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchaseprice:update", "更新采购价格表(PurchasePrice)")]
    public async Task<ActionResult<TaktPurchasePriceDto>> UpdatePurchasePriceAsync(long id, [FromBody] TaktPurchasePriceUpdateDto dto)
    {
        var result = await _service.UpdatePurchasePriceAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购价格表(PurchasePrice)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchaseprice:delete", "删除采购价格表(PurchasePrice)")]
    public async Task<ActionResult> DeletePurchasePriceByIdAsync(long id)
    {
        await _service.DeletePurchasePriceByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购价格表(PurchasePrice)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchaseprice:delete", "批量删除采购价格表(PurchasePrice)")]
    public async Task<ActionResult> DeletePurchasePriceBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchasePriceBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新采购价格表(PurchasePrice)Price
    /// </summary>
    [HttpPut("status-price")]
    [TaktPermission("logistics:materials:purchaseprice:update", "更新采购价格表(PurchasePrice)Price")]
    public async Task<ActionResult<TaktPurchasePriceDto>> UpdatePurchasePricePriceStatusAsync([FromBody] TaktPurchasePricePriceStatusDto dto)
    {
        var result = await _service.UpdatePurchasePricePriceStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取采购价格表(PurchasePrice)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchaseprice:import", "获取采购价格表(PurchasePrice)导入模板")]
    public async Task<IActionResult> GetPurchasePriceTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchasePriceTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购价格表(PurchasePrice)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchaseprice:import", "导入采购价格表(PurchasePrice)")]
    public async Task<ActionResult<object>> ImportPurchasePriceAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchasePriceAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购价格表(PurchasePrice)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchaseprice:export", "导出采购价格表(PurchasePrice)")]
    public async Task<IActionResult> ExportPurchasePriceAsync([FromBody] TaktPurchasePriceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchasePriceAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
