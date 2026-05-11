// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Sales
// 文件名称：TaktSalesPriceItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：销售价格明细表控制器，提供SalesPriceItem管理的RESTful API接口
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
/// 销售价格明细表控制器
/// </summary>
[Route("api/[controller]", Name = "销售价格明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:sales:salespriceitem", "销售价格明细表管理")]
public class TaktSalesPriceItemsController : TaktControllerBase
{
    private readonly ITaktSalesPriceItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceItemsController(
        ITaktSalesPriceItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取销售价格明细表(SalesPriceItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:sales:salespriceitem:list", "查询销售价格明细表(SalesPriceItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalesPriceItemDto>>> GetSalesPriceItemListAsync([FromQuery] TaktSalesPriceItemQueryDto queryDto)
    {
        var result = await _service.GetSalesPriceItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取销售价格明细表(SalesPriceItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:sales:salespriceitem:query", "查询销售价格明细表(SalesPriceItem)详情")]
    public async Task<ActionResult<TaktSalesPriceItemDto>> GetSalesPriceItemByIdAsync(long id)
    {
        var item = await _service.GetSalesPriceItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取销售价格明细表(SalesPriceItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:sales:salespriceitem:query", "查询销售价格明细表(SalesPriceItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalesPriceItemOptionsAsync()
    {
        var result = await _service.GetSalesPriceItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建销售价格明细表(SalesPriceItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:sales:salespriceitem:create", "创建销售价格明细表(SalesPriceItem)")]
    public async Task<ActionResult<TaktSalesPriceItemDto>> CreateSalesPriceItemAsync([FromBody] TaktSalesPriceItemCreateDto dto)
    {
        var result = await _service.CreateSalesPriceItemAsync(dto);
        return CreatedAtAction(nameof(GetSalesPriceItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新销售价格明细表(SalesPriceItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:sales:salespriceitem:update", "更新销售价格明细表(SalesPriceItem)")]
    public async Task<ActionResult<TaktSalesPriceItemDto>> UpdateSalesPriceItemAsync(long id, [FromBody] TaktSalesPriceItemUpdateDto dto)
    {
        var result = await _service.UpdateSalesPriceItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除销售价格明细表(SalesPriceItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:sales:salespriceitem:delete", "删除销售价格明细表(SalesPriceItem)")]
    public async Task<ActionResult> DeleteSalesPriceItemByIdAsync(long id)
    {
        await _service.DeleteSalesPriceItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除销售价格明细表(SalesPriceItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:sales:salespriceitem:delete", "批量删除销售价格明细表(SalesPriceItem)")]
    public async Task<ActionResult> DeleteSalesPriceItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalesPriceItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取销售价格明细表(SalesPriceItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:sales:salespriceitem:import", "获取销售价格明细表(SalesPriceItem)导入模板")]
    public async Task<IActionResult> GetSalesPriceItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalesPriceItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入销售价格明细表(SalesPriceItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:sales:salespriceitem:import", "导入销售价格明细表(SalesPriceItem)")]
    public async Task<ActionResult<object>> ImportSalesPriceItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalesPriceItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出销售价格明细表(SalesPriceItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:sales:salespriceitem:export", "导出销售价格明细表(SalesPriceItem)")]
    public async Task<IActionResult> ExportSalesPriceItemAsync([FromBody] TaktSalesPriceItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalesPriceItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
