// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Serial
// 文件名称：TaktProductSerialInboundsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号入库表控制器，提供ProductSerialInbound管理的RESTful API接口
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
/// 产品序列号入库表控制器
/// </summary>
[Route("api/[controller]", Name = "产品序列号入库表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:serial:productserialinbound", "产品序列号入库表管理")]
public class TaktProductSerialInboundsController : TaktControllerBase
{
    private readonly ITaktProductSerialInboundService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialInboundsController(
        ITaktProductSerialInboundService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取产品序列号入库表(ProductSerialInbound)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:serial:productserialinbound:list", "查询产品序列号入库表(ProductSerialInbound)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktProductSerialInboundDto>>> GetProductSerialInboundListAsync([FromQuery] TaktProductSerialInboundQueryDto queryDto)
    {
        var result = await _service.GetProductSerialInboundListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取产品序列号入库表(ProductSerialInbound)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:serial:productserialinbound:query", "查询产品序列号入库表(ProductSerialInbound)详情")]
    public async Task<ActionResult<TaktProductSerialInboundDto>> GetProductSerialInboundByIdAsync(long id)
    {
        var item = await _service.GetProductSerialInboundByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取产品序列号入库表(ProductSerialInbound)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:serial:productserialinbound:query", "查询产品序列号入库表(ProductSerialInbound)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetProductSerialInboundOptionsAsync()
    {
        var result = await _service.GetProductSerialInboundOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建产品序列号入库表(ProductSerialInbound)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:serial:productserialinbound:create", "创建产品序列号入库表(ProductSerialInbound)")]
    public async Task<ActionResult<TaktProductSerialInboundDto>> CreateProductSerialInboundAsync([FromBody] TaktProductSerialInboundCreateDto dto)
    {
        var result = await _service.CreateProductSerialInboundAsync(dto);
        return CreatedAtAction(nameof(GetProductSerialInboundByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新产品序列号入库表(ProductSerialInbound)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:serial:productserialinbound:update", "更新产品序列号入库表(ProductSerialInbound)")]
    public async Task<ActionResult<TaktProductSerialInboundDto>> UpdateProductSerialInboundAsync(long id, [FromBody] TaktProductSerialInboundUpdateDto dto)
    {
        var result = await _service.UpdateProductSerialInboundAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除产品序列号入库表(ProductSerialInbound)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:serial:productserialinbound:delete", "删除产品序列号入库表(ProductSerialInbound)")]
    public async Task<ActionResult> DeleteProductSerialInboundByIdAsync(long id)
    {
        await _service.DeleteProductSerialInboundByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除产品序列号入库表(ProductSerialInbound)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:serial:productserialinbound:delete", "批量删除产品序列号入库表(ProductSerialInbound)")]
    public async Task<ActionResult> DeleteProductSerialInboundBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteProductSerialInboundBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取产品序列号入库表(ProductSerialInbound)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:serial:productserialinbound:import", "获取产品序列号入库表(ProductSerialInbound)导入模板")]
    public async Task<IActionResult> GetProductSerialInboundTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetProductSerialInboundTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入产品序列号入库表(ProductSerialInbound)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:serial:productserialinbound:import", "导入产品序列号入库表(ProductSerialInbound)")]
    public async Task<ActionResult<object>> ImportProductSerialInboundAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportProductSerialInboundAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出产品序列号入库表(ProductSerialInbound)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:serial:productserialinbound:export", "导出产品序列号入库表(ProductSerialInbound)")]
    public async Task<IActionResult> ExportProductSerialInboundAsync([FromBody] TaktProductSerialInboundQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportProductSerialInboundAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
