// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Serial
// 文件名称：TaktProductSerialOutboundItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号出库明细表控制器，提供ProductSerialOutboundItem管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Serial;
using Takt.Application.Services.Logistics.Serial;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Serial;

/// <summary>
/// 产品序列号出库明细表控制器
/// </summary>
[Route("api/[controller]", Name = "产品序列号出库明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:serial:productserialoutbounditem", "产品序列号出库明细表管理")]
public class TaktProductSerialOutboundItemsController : TaktControllerBase
{
    private readonly ITaktProductSerialOutboundItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialOutboundItemsController(
        ITaktProductSerialOutboundItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取产品序列号出库明细表(ProductSerialOutboundItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:serial:productserialoutbounditem:list", "查询产品序列号出库明细表(ProductSerialOutboundItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktProductSerialOutboundItemDto>>> GetProductSerialOutboundItemListAsync([FromQuery] TaktProductSerialOutboundItemQueryDto queryDto)
    {
        var result = await _service.GetProductSerialOutboundItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取产品序列号出库明细表(ProductSerialOutboundItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:serial:productserialoutbounditem:query", "查询产品序列号出库明细表(ProductSerialOutboundItem)详情")]
    public async Task<ActionResult<TaktProductSerialOutboundItemDto>> GetProductSerialOutboundItemByIdAsync(long id)
    {
        var item = await _service.GetProductSerialOutboundItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取产品序列号出库明细表(ProductSerialOutboundItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:serial:productserialoutbounditem:query", "查询产品序列号出库明细表(ProductSerialOutboundItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetProductSerialOutboundItemOptionsAsync()
    {
        var result = await _service.GetProductSerialOutboundItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建产品序列号出库明细表(ProductSerialOutboundItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:serial:productserialoutbounditem:create", "创建产品序列号出库明细表(ProductSerialOutboundItem)")]
    public async Task<ActionResult<TaktProductSerialOutboundItemDto>> CreateProductSerialOutboundItemAsync([FromBody] TaktProductSerialOutboundItemCreateDto dto)
    {
        var result = await _service.CreateProductSerialOutboundItemAsync(dto);
        return CreatedAtAction(nameof(GetProductSerialOutboundItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新产品序列号出库明细表(ProductSerialOutboundItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:serial:productserialoutbounditem:update", "更新产品序列号出库明细表(ProductSerialOutboundItem)")]
    public async Task<ActionResult<TaktProductSerialOutboundItemDto>> UpdateProductSerialOutboundItemAsync(long id, [FromBody] TaktProductSerialOutboundItemUpdateDto dto)
    {
        var result = await _service.UpdateProductSerialOutboundItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除产品序列号出库明细表(ProductSerialOutboundItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:serial:productserialoutbounditem:delete", "删除产品序列号出库明细表(ProductSerialOutboundItem)")]
    public async Task<ActionResult> DeleteProductSerialOutboundItemByIdAsync(long id)
    {
        await _service.DeleteProductSerialOutboundItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除产品序列号出库明细表(ProductSerialOutboundItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:serial:productserialoutbounditem:delete", "批量删除产品序列号出库明细表(ProductSerialOutboundItem)")]
    public async Task<ActionResult> DeleteProductSerialOutboundItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteProductSerialOutboundItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取产品序列号出库明细表(ProductSerialOutboundItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:serial:productserialoutbounditem:import", "获取产品序列号出库明细表(ProductSerialOutboundItem)导入模板")]
    public async Task<IActionResult> GetProductSerialOutboundItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetProductSerialOutboundItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入产品序列号出库明细表(ProductSerialOutboundItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:serial:productserialoutbounditem:import", "导入产品序列号出库明细表(ProductSerialOutboundItem)")]
    public async Task<ActionResult<object>> ImportProductSerialOutboundItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportProductSerialOutboundItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出产品序列号出库明细表(ProductSerialOutboundItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:serial:productserialoutbounditem:export", "导出产品序列号出库明细表(ProductSerialOutboundItem)")]
    public async Task<IActionResult> ExportProductSerialOutboundItemAsync([FromBody] TaktProductSerialOutboundItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportProductSerialOutboundItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
