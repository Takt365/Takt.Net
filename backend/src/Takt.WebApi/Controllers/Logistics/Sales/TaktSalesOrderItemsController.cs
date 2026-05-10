// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Sales
// 文件名称：TaktSalesOrderItemsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：销售订单明细表控制器，提供SalesOrderItem管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Sales;
using Takt.Application.Services.Logistics.Sales;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Sales;

/// <summary>
/// 销售订单明细表控制器
/// </summary>
[Route("api/[controller]", Name = "销售订单明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:sales:salesorderitem", "销售订单明细表管理")]
public class TaktSalesOrderItemsController : TaktControllerBase
{
    private readonly ITaktSalesOrderItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderItemsController(
        ITaktSalesOrderItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取销售订单明细表(SalesOrderItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:sales:salesorderitem:list", "查询销售订单明细表(SalesOrderItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalesOrderItemDto>>> GetSalesOrderItemListAsync([FromQuery] TaktSalesOrderItemQueryDto queryDto)
    {
        var result = await _service.GetSalesOrderItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取销售订单明细表(SalesOrderItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:sales:salesorderitem:query", "查询销售订单明细表(SalesOrderItem)详情")]
    public async Task<ActionResult<TaktSalesOrderItemDto>> GetSalesOrderItemByIdAsync(long id)
    {
        var item = await _service.GetSalesOrderItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取销售订单明细表(SalesOrderItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:sales:salesorderitem:query", "查询销售订单明细表(SalesOrderItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalesOrderItemOptionsAsync()
    {
        var result = await _service.GetSalesOrderItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建销售订单明细表(SalesOrderItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:sales:salesorderitem:create", "创建销售订单明细表(SalesOrderItem)")]
    public async Task<ActionResult<TaktSalesOrderItemDto>> CreateSalesOrderItemAsync([FromBody] TaktSalesOrderItemCreateDto dto)
    {
        var result = await _service.CreateSalesOrderItemAsync(dto);
        return CreatedAtAction(nameof(GetSalesOrderItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新销售订单明细表(SalesOrderItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:sales:salesorderitem:update", "更新销售订单明细表(SalesOrderItem)")]
    public async Task<ActionResult<TaktSalesOrderItemDto>> UpdateSalesOrderItemAsync(long id, [FromBody] TaktSalesOrderItemUpdateDto dto)
    {
        var result = await _service.UpdateSalesOrderItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除销售订单明细表(SalesOrderItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:sales:salesorderitem:delete", "删除销售订单明细表(SalesOrderItem)")]
    public async Task<ActionResult> DeleteSalesOrderItemByIdAsync(long id)
    {
        await _service.DeleteSalesOrderItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除销售订单明细表(SalesOrderItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:sales:salesorderitem:delete", "批量删除销售订单明细表(SalesOrderItem)")]
    public async Task<ActionResult> DeleteSalesOrderItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalesOrderItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新销售订单明细表(SalesOrderItem)Delivery
    /// </summary>
    [HttpPut("status-delivery")]
    [TaktPermission("logistics:sales:salesorderitem:update", "更新销售订单明细表(SalesOrderItem)Delivery")]
    public async Task<ActionResult<TaktSalesOrderItemDto>> UpdateSalesOrderItemDeliveryStatusAsync([FromBody] TaktSalesOrderItemDeliveryStatusDto dto)
    {
        var result = await _service.UpdateSalesOrderItemDeliveryStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取销售订单明细表(SalesOrderItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:sales:salesorderitem:import", "获取销售订单明细表(SalesOrderItem)导入模板")]
    public async Task<IActionResult> GetSalesOrderItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalesOrderItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入销售订单明细表(SalesOrderItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:sales:salesorderitem:import", "导入销售订单明细表(SalesOrderItem)")]
    public async Task<ActionResult<object>> ImportSalesOrderItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalesOrderItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出销售订单明细表(SalesOrderItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:sales:salesorderitem:export", "导出销售订单明细表(SalesOrderItem)")]
    public async Task<IActionResult> ExportSalesOrderItemAsync([FromBody] TaktSalesOrderItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalesOrderItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
