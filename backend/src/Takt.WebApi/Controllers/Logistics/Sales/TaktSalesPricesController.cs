// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Sales
// 文件名称：TaktSalesPricesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：销售价格表控制器，提供SalesPrice管理的RESTful API接口
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
/// 销售价格表控制器
/// </summary>
[Route("api/[controller]", Name = "销售价格表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:sales:salesprice", "销售价格表管理")]
public class TaktSalesPricesController : TaktControllerBase
{
    private readonly ITaktSalesPriceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPricesController(
        ITaktSalesPriceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取销售价格表(SalesPrice)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:sales:salesprice:list", "查询销售价格表(SalesPrice)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalesPriceDto>>> GetSalesPriceListAsync([FromQuery] TaktSalesPriceQueryDto queryDto)
    {
        var result = await _service.GetSalesPriceListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取销售价格表(SalesPrice)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:sales:salesprice:query", "查询销售价格表(SalesPrice)详情")]
    public async Task<ActionResult<TaktSalesPriceDto>> GetSalesPriceByIdAsync(long id)
    {
        var item = await _service.GetSalesPriceByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取销售价格表(SalesPrice)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:sales:salesprice:query", "查询销售价格表(SalesPrice)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalesPriceOptionsAsync()
    {
        var result = await _service.GetSalesPriceOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建销售价格表(SalesPrice)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:sales:salesprice:create", "创建销售价格表(SalesPrice)")]
    public async Task<ActionResult<TaktSalesPriceDto>> CreateSalesPriceAsync([FromBody] TaktSalesPriceCreateDto dto)
    {
        var result = await _service.CreateSalesPriceAsync(dto);
        return CreatedAtAction(nameof(GetSalesPriceByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新销售价格表(SalesPrice)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:sales:salesprice:update", "更新销售价格表(SalesPrice)")]
    public async Task<ActionResult<TaktSalesPriceDto>> UpdateSalesPriceAsync(long id, [FromBody] TaktSalesPriceUpdateDto dto)
    {
        var result = await _service.UpdateSalesPriceAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除销售价格表(SalesPrice)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:sales:salesprice:delete", "删除销售价格表(SalesPrice)")]
    public async Task<ActionResult> DeleteSalesPriceByIdAsync(long id)
    {
        await _service.DeleteSalesPriceByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除销售价格表(SalesPrice)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:sales:salesprice:delete", "批量删除销售价格表(SalesPrice)")]
    public async Task<ActionResult> DeleteSalesPriceBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalesPriceBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新销售价格表(SalesPrice)Price
    /// </summary>
    [HttpPut("status-price")]
    [TaktPermission("logistics:sales:salesprice:update", "更新销售价格表(SalesPrice)Price")]
    public async Task<ActionResult<TaktSalesPriceDto>> UpdateSalesPricePriceStatusAsync([FromBody] TaktSalesPricePriceStatusDto dto)
    {
        var result = await _service.UpdateSalesPricePriceStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取销售价格表(SalesPrice)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:sales:salesprice:import", "获取销售价格表(SalesPrice)导入模板")]
    public async Task<IActionResult> GetSalesPriceTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalesPriceTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入销售价格表(SalesPrice)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:sales:salesprice:import", "导入销售价格表(SalesPrice)")]
    public async Task<ActionResult<object>> ImportSalesPriceAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalesPriceAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出销售价格表(SalesPrice)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:sales:salesprice:export", "导出销售价格表(SalesPrice)")]
    public async Task<IActionResult> ExportSalesPriceAsync([FromBody] TaktSalesPriceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalesPriceAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
