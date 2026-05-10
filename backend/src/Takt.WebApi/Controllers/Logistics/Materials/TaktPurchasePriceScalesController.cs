// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchasePriceScalesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：采购价格阶梯表控制器，提供PurchasePriceScale管理的RESTful API接口
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
/// 采购价格阶梯表控制器
/// </summary>
[Route("api/[controller]", Name = "采购价格阶梯表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchasepricescale", "采购价格阶梯表管理")]
public class TaktPurchasePriceScalesController : TaktControllerBase
{
    private readonly ITaktPurchasePriceScaleService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceScalesController(
        ITaktPurchasePriceScaleService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购价格阶梯表(PurchasePriceScale)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchasepricescale:list", "查询采购价格阶梯表(PurchasePriceScale)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchasePriceScaleDto>>> GetPurchasePriceScaleListAsync([FromQuery] TaktPurchasePriceScaleQueryDto queryDto)
    {
        var result = await _service.GetPurchasePriceScaleListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchasepricescale:query", "查询采购价格阶梯表(PurchasePriceScale)详情")]
    public async Task<ActionResult<TaktPurchasePriceScaleDto>> GetPurchasePriceScaleByIdAsync(long id)
    {
        var item = await _service.GetPurchasePriceScaleByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购价格阶梯表(PurchasePriceScale)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchasepricescale:query", "查询采购价格阶梯表(PurchasePriceScale)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchasePriceScaleOptionsAsync()
    {
        var result = await _service.GetPurchasePriceScaleOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchasepricescale:create", "创建采购价格阶梯表(PurchasePriceScale)")]
    public async Task<ActionResult<TaktPurchasePriceScaleDto>> CreatePurchasePriceScaleAsync([FromBody] TaktPurchasePriceScaleCreateDto dto)
    {
        var result = await _service.CreatePurchasePriceScaleAsync(dto);
        return CreatedAtAction(nameof(GetPurchasePriceScaleByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchasepricescale:update", "更新采购价格阶梯表(PurchasePriceScale)")]
    public async Task<ActionResult<TaktPurchasePriceScaleDto>> UpdatePurchasePriceScaleAsync(long id, [FromBody] TaktPurchasePriceScaleUpdateDto dto)
    {
        var result = await _service.UpdatePurchasePriceScaleAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchasepricescale:delete", "删除采购价格阶梯表(PurchasePriceScale)")]
    public async Task<ActionResult> DeletePurchasePriceScaleByIdAsync(long id)
    {
        await _service.DeletePurchasePriceScaleByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchasepricescale:delete", "批量删除采购价格阶梯表(PurchasePriceScale)")]
    public async Task<ActionResult> DeletePurchasePriceScaleBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchasePriceScaleBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新采购价格阶梯表(PurchasePriceScale)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:materials:purchasepricescale:update", "更新采购价格阶梯表(PurchasePriceScale)排序")]
    public async Task<ActionResult<TaktPurchasePriceScaleDto>> UpdatePurchasePriceScaleSortAsync([FromBody] TaktPurchasePriceScaleSortDto dto)
    {
        var result = await _service.UpdatePurchasePriceScaleSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取采购价格阶梯表(PurchasePriceScale)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchasepricescale:import", "获取采购价格阶梯表(PurchasePriceScale)导入模板")]
    public async Task<IActionResult> GetPurchasePriceScaleTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchasePriceScaleTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchasepricescale:import", "导入采购价格阶梯表(PurchasePriceScale)")]
    public async Task<ActionResult<object>> ImportPurchasePriceScaleAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchasePriceScaleAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购价格阶梯表(PurchasePriceScale)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchasepricescale:export", "导出采购价格阶梯表(PurchasePriceScale)")]
    public async Task<IActionResult> ExportPurchasePriceScaleAsync([FromBody] TaktPurchasePriceScaleQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchasePriceScaleAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
