// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchaseOrdersController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：采购订单表控制器，提供PurchaseOrder管理的RESTful API接口
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
/// 采购订单表控制器
/// </summary>
[Route("api/[controller]", Name = "采购订单表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchaseorder", "采购订单表管理")]
public class TaktPurchaseOrdersController : TaktControllerBase
{
    private readonly ITaktPurchaseOrderService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrdersController(
        ITaktPurchaseOrderService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购订单表(PurchaseOrder)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchaseorder:list", "查询采购订单表(PurchaseOrder)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchaseOrderDto>>> GetPurchaseOrderListAsync([FromQuery] TaktPurchaseOrderQueryDto queryDto)
    {
        var result = await _service.GetPurchaseOrderListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购订单表(PurchaseOrder)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchaseorder:query", "查询采购订单表(PurchaseOrder)详情")]
    public async Task<ActionResult<TaktPurchaseOrderDto>> GetPurchaseOrderByIdAsync(long id)
    {
        var item = await _service.GetPurchaseOrderByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购订单表(PurchaseOrder)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchaseorder:query", "查询采购订单表(PurchaseOrder)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchaseOrderOptionsAsync()
    {
        var result = await _service.GetPurchaseOrderOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购订单表(PurchaseOrder)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchaseorder:create", "创建采购订单表(PurchaseOrder)")]
    public async Task<ActionResult<TaktPurchaseOrderDto>> CreatePurchaseOrderAsync([FromBody] TaktPurchaseOrderCreateDto dto)
    {
        var result = await _service.CreatePurchaseOrderAsync(dto);
        return CreatedAtAction(nameof(GetPurchaseOrderByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购订单表(PurchaseOrder)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchaseorder:update", "更新采购订单表(PurchaseOrder)")]
    public async Task<ActionResult<TaktPurchaseOrderDto>> UpdatePurchaseOrderAsync(long id, [FromBody] TaktPurchaseOrderUpdateDto dto)
    {
        var result = await _service.UpdatePurchaseOrderAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购订单表(PurchaseOrder)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchaseorder:delete", "删除采购订单表(PurchaseOrder)")]
    public async Task<ActionResult> DeletePurchaseOrderByIdAsync(long id)
    {
        await _service.DeletePurchaseOrderByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购订单表(PurchaseOrder)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchaseorder:delete", "批量删除采购订单表(PurchaseOrder)")]
    public async Task<ActionResult> DeletePurchaseOrderBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchaseOrderBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新采购订单表(PurchaseOrder)Order
    /// </summary>
    [HttpPut("status-order")]
    [TaktPermission("logistics:materials:purchaseorder:update", "更新采购订单表(PurchaseOrder)Order")]
    public async Task<ActionResult<TaktPurchaseOrderDto>> UpdatePurchaseOrderOrderStatusAsync([FromBody] TaktPurchaseOrderOrderStatusDto dto)
    {
        var result = await _service.UpdatePurchaseOrderOrderStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新采购订单表(PurchaseOrder)Delivery
    /// </summary>
    [HttpPut("status-delivery")]
    [TaktPermission("logistics:materials:purchaseorder:update", "更新采购订单表(PurchaseOrder)Delivery")]
    public async Task<ActionResult<TaktPurchaseOrderDto>> UpdatePurchaseOrderDeliveryStatusAsync([FromBody] TaktPurchaseOrderDeliveryStatusDto dto)
    {
        var result = await _service.UpdatePurchaseOrderDeliveryStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取采购订单表(PurchaseOrder)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchaseorder:import", "获取采购订单表(PurchaseOrder)导入模板")]
    public async Task<IActionResult> GetPurchaseOrderTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchaseOrderTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购订单表(PurchaseOrder)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchaseorder:import", "导入采购订单表(PurchaseOrder)")]
    public async Task<ActionResult<object>> ImportPurchaseOrderAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchaseOrderAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购订单表(PurchaseOrder)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchaseorder:export", "导出采购订单表(PurchaseOrder)")]
    public async Task<IActionResult> ExportPurchaseOrderAsync([FromBody] TaktPurchaseOrderQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchaseOrderAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
