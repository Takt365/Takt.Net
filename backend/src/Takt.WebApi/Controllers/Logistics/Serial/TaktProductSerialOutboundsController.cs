// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Serial
// 文件名称：TaktProductSerialOutboundsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号出库表控制器，提供ProductSerialOutbound管理的RESTful API接口
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
/// 产品序列号出库表控制器
/// </summary>
[Route("api/[controller]", Name = "产品序列号出库表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:serial:productserialoutbound", "产品序列号出库表管理")]
public class TaktProductSerialOutboundsController : TaktControllerBase
{
    private readonly ITaktProductSerialOutboundService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductSerialOutboundsController(
        ITaktProductSerialOutboundService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取产品序列号出库表(ProductSerialOutbound)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:serial:productserialoutbound:list", "查询产品序列号出库表(ProductSerialOutbound)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktProductSerialOutboundDto>>> GetProductSerialOutboundListAsync([FromQuery] TaktProductSerialOutboundQueryDto queryDto)
    {
        var result = await _service.GetProductSerialOutboundListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:serial:productserialoutbound:query", "查询产品序列号出库表(ProductSerialOutbound)详情")]
    public async Task<ActionResult<TaktProductSerialOutboundDto>> GetProductSerialOutboundByIdAsync(long id)
    {
        var item = await _service.GetProductSerialOutboundByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取产品序列号出库表(ProductSerialOutbound)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:serial:productserialoutbound:query", "查询产品序列号出库表(ProductSerialOutbound)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetProductSerialOutboundOptionsAsync()
    {
        var result = await _service.GetProductSerialOutboundOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:serial:productserialoutbound:create", "创建产品序列号出库表(ProductSerialOutbound)")]
    public async Task<ActionResult<TaktProductSerialOutboundDto>> CreateProductSerialOutboundAsync([FromBody] TaktProductSerialOutboundCreateDto dto)
    {
        var result = await _service.CreateProductSerialOutboundAsync(dto);
        return CreatedAtAction(nameof(GetProductSerialOutboundByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:serial:productserialoutbound:update", "更新产品序列号出库表(ProductSerialOutbound)")]
    public async Task<ActionResult<TaktProductSerialOutboundDto>> UpdateProductSerialOutboundAsync(long id, [FromBody] TaktProductSerialOutboundUpdateDto dto)
    {
        var result = await _service.UpdateProductSerialOutboundAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:serial:productserialoutbound:delete", "删除产品序列号出库表(ProductSerialOutbound)")]
    public async Task<ActionResult> DeleteProductSerialOutboundByIdAsync(long id)
    {
        await _service.DeleteProductSerialOutboundByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:serial:productserialoutbound:delete", "批量删除产品序列号出库表(ProductSerialOutbound)")]
    public async Task<ActionResult> DeleteProductSerialOutboundBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteProductSerialOutboundBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取产品序列号出库表(ProductSerialOutbound)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:serial:productserialoutbound:import", "获取产品序列号出库表(ProductSerialOutbound)导入模板")]
    public async Task<IActionResult> GetProductSerialOutboundTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetProductSerialOutboundTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:serial:productserialoutbound:import", "导入产品序列号出库表(ProductSerialOutbound)")]
    public async Task<ActionResult<object>> ImportProductSerialOutboundAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportProductSerialOutboundAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出产品序列号出库表(ProductSerialOutbound)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:serial:productserialoutbound:export", "导出产品序列号出库表(ProductSerialOutbound)")]
    public async Task<IActionResult> ExportProductSerialOutboundAsync([FromBody] TaktProductSerialOutboundQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportProductSerialOutboundAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
