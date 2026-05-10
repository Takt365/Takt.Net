// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Sales
// 文件名称：TaktSalesPriceScalesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：销售价格阶梯表控制器，提供SalesPriceScale管理的RESTful API接口
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
/// 销售价格阶梯表控制器
/// </summary>
[Route("api/[controller]", Name = "销售价格阶梯表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:sales:salespricescale", "销售价格阶梯表管理")]
public class TaktSalesPriceScalesController : TaktControllerBase
{
    private readonly ITaktSalesPriceScaleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceScalesController(
        ITaktSalesPriceScaleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取销售价格阶梯表(SalesPriceScale)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:sales:salespricescale:list", "查询销售价格阶梯表(SalesPriceScale)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalesPriceScaleDto>>> GetSalesPriceScaleListAsync([FromQuery] TaktSalesPriceScaleQueryDto queryDto)
    {
        var result = await _service.GetSalesPriceScaleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取销售价格阶梯表(SalesPriceScale)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:sales:salespricescale:query", "查询销售价格阶梯表(SalesPriceScale)详情")]
    public async Task<ActionResult<TaktSalesPriceScaleDto>> GetSalesPriceScaleByIdAsync(long id)
    {
        var item = await _service.GetSalesPriceScaleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取销售价格阶梯表(SalesPriceScale)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:sales:salespricescale:query", "查询销售价格阶梯表(SalesPriceScale)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalesPriceScaleOptionsAsync()
    {
        var result = await _service.GetSalesPriceScaleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建销售价格阶梯表(SalesPriceScale)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:sales:salespricescale:create", "创建销售价格阶梯表(SalesPriceScale)")]
    public async Task<ActionResult<TaktSalesPriceScaleDto>> CreateSalesPriceScaleAsync([FromBody] TaktSalesPriceScaleCreateDto dto)
    {
        var result = await _service.CreateSalesPriceScaleAsync(dto);
        return CreatedAtAction(nameof(GetSalesPriceScaleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新销售价格阶梯表(SalesPriceScale)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:sales:salespricescale:update", "更新销售价格阶梯表(SalesPriceScale)")]
    public async Task<ActionResult<TaktSalesPriceScaleDto>> UpdateSalesPriceScaleAsync(long id, [FromBody] TaktSalesPriceScaleUpdateDto dto)
    {
        var result = await _service.UpdateSalesPriceScaleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除销售价格阶梯表(SalesPriceScale)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:sales:salespricescale:delete", "删除销售价格阶梯表(SalesPriceScale)")]
    public async Task<ActionResult> DeleteSalesPriceScaleByIdAsync(long id)
    {
        await _service.DeleteSalesPriceScaleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除销售价格阶梯表(SalesPriceScale)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:sales:salespricescale:delete", "批量删除销售价格阶梯表(SalesPriceScale)")]
    public async Task<ActionResult> DeleteSalesPriceScaleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalesPriceScaleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新销售价格阶梯表(SalesPriceScale)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:sales:salespricescale:update", "更新销售价格阶梯表(SalesPriceScale)排序")]
    public async Task<ActionResult<TaktSalesPriceScaleDto>> UpdateSalesPriceScaleSortAsync([FromBody] TaktSalesPriceScaleSortDto dto)
    {
        var result = await _service.UpdateSalesPriceScaleSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取销售价格阶梯表(SalesPriceScale)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:sales:salespricescale:import", "获取销售价格阶梯表(SalesPriceScale)导入模板")]
    public async Task<IActionResult> GetSalesPriceScaleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalesPriceScaleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入销售价格阶梯表(SalesPriceScale)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:sales:salespricescale:import", "导入销售价格阶梯表(SalesPriceScale)")]
    public async Task<ActionResult<object>> ImportSalesPriceScaleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalesPriceScaleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出销售价格阶梯表(SalesPriceScale)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:sales:salespricescale:export", "导出销售价格阶梯表(SalesPriceScale)")]
    public async Task<IActionResult> ExportSalesPriceScaleAsync([FromBody] TaktSalesPriceScaleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalesPriceScaleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
