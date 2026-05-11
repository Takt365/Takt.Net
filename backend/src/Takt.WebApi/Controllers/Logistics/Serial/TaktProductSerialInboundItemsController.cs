// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Serial
// 文件名称：TaktProductSerialInboundItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号入库明细表控制器，提供ProductSerialInboundItem管理的RESTful API接口
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
/// 产品序列号入库明细表控制器
/// </summary>
[Route("api/[controller]", Name = "产品序列号入库明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:serial:productserialinbounditem", "产品序列号入库明细表管理")]
public class TaktProductSerialInboundItemsController : TaktControllerBase
{
    private readonly ITaktProductSerialInboundItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundItemsController(
        ITaktProductSerialInboundItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取产品序列号入库明细表(ProductSerialInboundItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:serial:productserialinbounditem:list", "查询产品序列号入库明细表(ProductSerialInboundItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktProductSerialInboundItemDto>>> GetProductSerialInboundItemListAsync([FromQuery] TaktProductSerialInboundItemQueryDto queryDto)
    {
        var result = await _service.GetProductSerialInboundItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:serial:productserialinbounditem:query", "查询产品序列号入库明细表(ProductSerialInboundItem)详情")]
    public async Task<ActionResult<TaktProductSerialInboundItemDto>> GetProductSerialInboundItemByIdAsync(long id)
    {
        var item = await _service.GetProductSerialInboundItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取产品序列号入库明细表(ProductSerialInboundItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:serial:productserialinbounditem:query", "查询产品序列号入库明细表(ProductSerialInboundItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetProductSerialInboundItemOptionsAsync()
    {
        var result = await _service.GetProductSerialInboundItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:serial:productserialinbounditem:create", "创建产品序列号入库明细表(ProductSerialInboundItem)")]
    public async Task<ActionResult<TaktProductSerialInboundItemDto>> CreateProductSerialInboundItemAsync([FromBody] TaktProductSerialInboundItemCreateDto dto)
    {
        var result = await _service.CreateProductSerialInboundItemAsync(dto);
        return CreatedAtAction(nameof(GetProductSerialInboundItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:serial:productserialinbounditem:update", "更新产品序列号入库明细表(ProductSerialInboundItem)")]
    public async Task<ActionResult<TaktProductSerialInboundItemDto>> UpdateProductSerialInboundItemAsync(long id, [FromBody] TaktProductSerialInboundItemUpdateDto dto)
    {
        var result = await _service.UpdateProductSerialInboundItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:serial:productserialinbounditem:delete", "删除产品序列号入库明细表(ProductSerialInboundItem)")]
    public async Task<ActionResult> DeleteProductSerialInboundItemByIdAsync(long id)
    {
        await _service.DeleteProductSerialInboundItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:serial:productserialinbounditem:delete", "批量删除产品序列号入库明细表(ProductSerialInboundItem)")]
    public async Task<ActionResult> DeleteProductSerialInboundItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteProductSerialInboundItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取产品序列号入库明细表(ProductSerialInboundItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:serial:productserialinbounditem:import", "获取产品序列号入库明细表(ProductSerialInboundItem)导入模板")]
    public async Task<IActionResult> GetProductSerialInboundItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetProductSerialInboundItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:serial:productserialinbounditem:import", "导入产品序列号入库明细表(ProductSerialInboundItem)")]
    public async Task<ActionResult<object>> ImportProductSerialInboundItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportProductSerialInboundItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出产品序列号入库明细表(ProductSerialInboundItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:serial:productserialinbounditem:export", "导出产品序列号入库明细表(ProductSerialInboundItem)")]
    public async Task<IActionResult> ExportProductSerialInboundItemAsync([FromBody] TaktProductSerialInboundItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportProductSerialInboundItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
