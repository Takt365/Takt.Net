// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Sales
// 文件名称：TaktSalesOrdersController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：销售订单表控制器，提供SalesOrder管理的RESTful API接口
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
/// 销售订单表控制器
/// </summary>
[Route("api/[controller]", Name = "销售订单表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:sales:salesorder", "销售订单表管理")]
public class TaktSalesOrdersController : TaktControllerBase
{
    private readonly ITaktSalesOrderService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrdersController(
        ITaktSalesOrderService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取销售订单表(SalesOrder)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:sales:salesorder:list", "查询销售订单表(SalesOrder)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalesOrderDto>>> GetSalesOrderListAsync([FromQuery] TaktSalesOrderQueryDto queryDto)
    {
        var result = await _service.GetSalesOrderListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取销售订单表(SalesOrder)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:sales:salesorder:query", "查询销售订单表(SalesOrder)详情")]
    public async Task<ActionResult<TaktSalesOrderDto>> GetSalesOrderByIdAsync(long id)
    {
        var item = await _service.GetSalesOrderByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取销售订单表(SalesOrder)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:sales:salesorder:query", "查询销售订单表(SalesOrder)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalesOrderOptionsAsync()
    {
        var result = await _service.GetSalesOrderOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建销售订单表(SalesOrder)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:sales:salesorder:create", "创建销售订单表(SalesOrder)")]
    public async Task<ActionResult<TaktSalesOrderDto>> CreateSalesOrderAsync([FromBody] TaktSalesOrderCreateDto dto)
    {
        var result = await _service.CreateSalesOrderAsync(dto);
        return CreatedAtAction(nameof(GetSalesOrderByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新销售订单表(SalesOrder)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:sales:salesorder:update", "更新销售订单表(SalesOrder)")]
    public async Task<ActionResult<TaktSalesOrderDto>> UpdateSalesOrderAsync(long id, [FromBody] TaktSalesOrderUpdateDto dto)
    {
        var result = await _service.UpdateSalesOrderAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除销售订单表(SalesOrder)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:sales:salesorder:delete", "删除销售订单表(SalesOrder)")]
    public async Task<ActionResult> DeleteSalesOrderByIdAsync(long id)
    {
        await _service.DeleteSalesOrderByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除销售订单表(SalesOrder)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:sales:salesorder:delete", "批量删除销售订单表(SalesOrder)")]
    public async Task<ActionResult> DeleteSalesOrderBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalesOrderBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新销售订单表(SalesOrder)Order
    /// </summary>
    [HttpPut("status-order")]
    [TaktPermission("logistics:sales:salesorder:update", "更新销售订单表(SalesOrder)Order")]
    public async Task<ActionResult<TaktSalesOrderDto>> UpdateSalesOrderOrderStatusAsync([FromBody] TaktSalesOrderOrderStatusDto dto)
    {
        var result = await _service.UpdateSalesOrderOrderStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新销售订单表(SalesOrder)Delivery
    /// </summary>
    [HttpPut("status-delivery")]
    [TaktPermission("logistics:sales:salesorder:update", "更新销售订单表(SalesOrder)Delivery")]
    public async Task<ActionResult<TaktSalesOrderDto>> UpdateSalesOrderDeliveryStatusAsync([FromBody] TaktSalesOrderDeliveryStatusDto dto)
    {
        var result = await _service.UpdateSalesOrderDeliveryStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取销售订单表(SalesOrder)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:sales:salesorder:import", "获取销售订单表(SalesOrder)导入模板")]
    public async Task<IActionResult> GetSalesOrderTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalesOrderTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入销售订单表(SalesOrder)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:sales:salesorder:import", "导入销售订单表(SalesOrder)")]
    public async Task<ActionResult<object>> ImportSalesOrderAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalesOrderAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出销售订单表(SalesOrder)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:sales:salesorder:export", "导出销售订单表(SalesOrder)")]
    public async Task<IActionResult> ExportSalesOrderAsync([FromBody] TaktSalesOrderQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalesOrderAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
