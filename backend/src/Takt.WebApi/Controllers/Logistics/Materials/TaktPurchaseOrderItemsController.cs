// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchaseOrderItemsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：采购订单明细表控制器，提供PurchaseOrderItem管理的RESTful API接口
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
/// 采购订单明细表控制器
/// </summary>
[Route("api/[controller]", Name = "采购订单明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchaseorderitem", "采购订单明细表管理")]
public class TaktPurchaseOrderItemsController : TaktControllerBase
{
    private readonly ITaktPurchaseOrderItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderItemsController(
        ITaktPurchaseOrderItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购订单明细表(PurchaseOrderItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchaseorderitem:list", "查询采购订单明细表(PurchaseOrderItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchaseOrderItemDto>>> GetPurchaseOrderItemListAsync([FromQuery] TaktPurchaseOrderItemQueryDto queryDto)
    {
        var result = await _service.GetPurchaseOrderItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购订单明细表(PurchaseOrderItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchaseorderitem:query", "查询采购订单明细表(PurchaseOrderItem)详情")]
    public async Task<ActionResult<TaktPurchaseOrderItemDto>> GetPurchaseOrderItemByIdAsync(long id)
    {
        var item = await _service.GetPurchaseOrderItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购订单明细表(PurchaseOrderItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchaseorderitem:query", "查询采购订单明细表(PurchaseOrderItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchaseOrderItemOptionsAsync()
    {
        var result = await _service.GetPurchaseOrderItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购订单明细表(PurchaseOrderItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchaseorderitem:create", "创建采购订单明细表(PurchaseOrderItem)")]
    public async Task<ActionResult<TaktPurchaseOrderItemDto>> CreatePurchaseOrderItemAsync([FromBody] TaktPurchaseOrderItemCreateDto dto)
    {
        var result = await _service.CreatePurchaseOrderItemAsync(dto);
        return CreatedAtAction(nameof(GetPurchaseOrderItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购订单明细表(PurchaseOrderItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchaseorderitem:update", "更新采购订单明细表(PurchaseOrderItem)")]
    public async Task<ActionResult<TaktPurchaseOrderItemDto>> UpdatePurchaseOrderItemAsync(long id, [FromBody] TaktPurchaseOrderItemUpdateDto dto)
    {
        var result = await _service.UpdatePurchaseOrderItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购订单明细表(PurchaseOrderItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchaseorderitem:delete", "删除采购订单明细表(PurchaseOrderItem)")]
    public async Task<ActionResult> DeletePurchaseOrderItemByIdAsync(long id)
    {
        await _service.DeletePurchaseOrderItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购订单明细表(PurchaseOrderItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchaseorderitem:delete", "批量删除采购订单明细表(PurchaseOrderItem)")]
    public async Task<ActionResult> DeletePurchaseOrderItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchaseOrderItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新采购订单明细表(PurchaseOrderItem)Delivery
    /// </summary>
    [HttpPut("status-delivery")]
    [TaktPermission("logistics:materials:purchaseorderitem:update", "更新采购订单明细表(PurchaseOrderItem)Delivery")]
    public async Task<ActionResult<TaktPurchaseOrderItemDto>> UpdatePurchaseOrderItemDeliveryStatusAsync([FromBody] TaktPurchaseOrderItemDeliveryStatusDto dto)
    {
        var result = await _service.UpdatePurchaseOrderItemDeliveryStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取采购订单明细表(PurchaseOrderItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchaseorderitem:import", "获取采购订单明细表(PurchaseOrderItem)导入模板")]
    public async Task<IActionResult> GetPurchaseOrderItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchaseOrderItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购订单明细表(PurchaseOrderItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchaseorderitem:import", "导入采购订单明细表(PurchaseOrderItem)")]
    public async Task<ActionResult<object>> ImportPurchaseOrderItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchaseOrderItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购订单明细表(PurchaseOrderItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchaseorderitem:export", "导出采购订单明细表(PurchaseOrderItem)")]
    public async Task<IActionResult> ExportPurchaseOrderItemAsync([FromBody] TaktPurchaseOrderItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchaseOrderItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
