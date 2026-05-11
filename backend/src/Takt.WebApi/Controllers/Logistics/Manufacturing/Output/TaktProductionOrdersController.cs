// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktProductionOrdersController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：生产工单表控制器，提供ProductionOrder管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Application.Services.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Output;

/// <summary>
/// 生产工单表控制器
/// </summary>
[Route("api/[controller]", Name = "生产工单表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:productionorder", "生产工单表管理")]
public class TaktProductionOrdersController : TaktControllerBase
{
    private readonly ITaktProductionOrderService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktProductionOrdersController(
        ITaktProductionOrderService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取生产工单表(ProductionOrder)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:productionorder:list", "查询生产工单表(ProductionOrder)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktProductionOrderDto>>> GetProductionOrderListAsync([FromQuery] TaktProductionOrderQueryDto queryDto)
    {
        var result = await _service.GetProductionOrderListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取生产工单表(ProductionOrder)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:productionorder:query", "查询生产工单表(ProductionOrder)详情")]
    public async Task<ActionResult<TaktProductionOrderDto>> GetProductionOrderByIdAsync(long id)
    {
        var item = await _service.GetProductionOrderByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取生产工单表(ProductionOrder)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:productionorder:query", "查询生产工单表(ProductionOrder)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetProductionOrderOptionsAsync()
    {
        var result = await _service.GetProductionOrderOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建生产工单表(ProductionOrder)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:productionorder:create", "创建生产工单表(ProductionOrder)")]
    public async Task<ActionResult<TaktProductionOrderDto>> CreateProductionOrderAsync([FromBody] TaktProductionOrderCreateDto dto)
    {
        var result = await _service.CreateProductionOrderAsync(dto);
        return CreatedAtAction(nameof(GetProductionOrderByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新生产工单表(ProductionOrder)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:productionorder:update", "更新生产工单表(ProductionOrder)")]
    public async Task<ActionResult<TaktProductionOrderDto>> UpdateProductionOrderAsync(long id, [FromBody] TaktProductionOrderUpdateDto dto)
    {
        var result = await _service.UpdateProductionOrderAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除生产工单表(ProductionOrder)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:productionorder:delete", "删除生产工单表(ProductionOrder)")]
    public async Task<ActionResult> DeleteProductionOrderByIdAsync(long id)
    {
        await _service.DeleteProductionOrderByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除生产工单表(ProductionOrder)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:productionorder:delete", "批量删除生产工单表(ProductionOrder)")]
    public async Task<ActionResult> DeleteProductionOrderBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteProductionOrderBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新生产工单表(ProductionOrder)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:output:productionorder:update", "更新生产工单表(ProductionOrder)状态")]
    public async Task<ActionResult<TaktProductionOrderDto>> UpdateProductionOrderStatusAsync([FromBody] TaktProductionOrderStatusDto dto)
    {
        var result = await _service.UpdateProductionOrderStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取生产工单表(ProductionOrder)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:productionorder:import", "获取生产工单表(ProductionOrder)导入模板")]
    public async Task<IActionResult> GetProductionOrderTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetProductionOrderTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入生产工单表(ProductionOrder)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:productionorder:import", "导入生产工单表(ProductionOrder)")]
    public async Task<ActionResult<object>> ImportProductionOrderAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportProductionOrderAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出生产工单表(ProductionOrder)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:productionorder:export", "导出生产工单表(ProductionOrder)")]
    public async Task<IActionResult> ExportProductionOrderAsync([FromBody] TaktProductionOrderQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportProductionOrderAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
