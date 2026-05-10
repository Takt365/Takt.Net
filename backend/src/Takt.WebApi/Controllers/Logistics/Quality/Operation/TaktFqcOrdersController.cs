// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktFqcOrdersController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：出货检验单表控制器，提供FqcOrder管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Application.Services.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Operation;

/// <summary>
/// 出货检验单表控制器
/// </summary>
[Route("api/[controller]", Name = "出货检验单表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:fqcorder", "出货检验单表管理")]
public class TaktFqcOrdersController : TaktControllerBase
{
    private readonly ITaktFqcOrderService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrdersController(
        ITaktFqcOrderService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取出货检验单表(FqcOrder)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:fqcorder:list", "查询出货检验单表(FqcOrder)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFqcOrderDto>>> GetFqcOrderListAsync([FromQuery] TaktFqcOrderQueryDto queryDto)
    {
        var result = await _service.GetFqcOrderListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取出货检验单表(FqcOrder)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:fqcorder:query", "查询出货检验单表(FqcOrder)详情")]
    public async Task<ActionResult<TaktFqcOrderDto>> GetFqcOrderByIdAsync(long id)
    {
        var item = await _service.GetFqcOrderByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取出货检验单表(FqcOrder)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:fqcorder:query", "查询出货检验单表(FqcOrder)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFqcOrderOptionsAsync()
    {
        var result = await _service.GetFqcOrderOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建出货检验单表(FqcOrder)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:fqcorder:create", "创建出货检验单表(FqcOrder)")]
    public async Task<ActionResult<TaktFqcOrderDto>> CreateFqcOrderAsync([FromBody] TaktFqcOrderCreateDto dto)
    {
        var result = await _service.CreateFqcOrderAsync(dto);
        return CreatedAtAction(nameof(GetFqcOrderByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新出货检验单表(FqcOrder)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:fqcorder:update", "更新出货检验单表(FqcOrder)")]
    public async Task<ActionResult<TaktFqcOrderDto>> UpdateFqcOrderAsync(long id, [FromBody] TaktFqcOrderUpdateDto dto)
    {
        var result = await _service.UpdateFqcOrderAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除出货检验单表(FqcOrder)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:fqcorder:delete", "删除出货检验单表(FqcOrder)")]
    public async Task<ActionResult> DeleteFqcOrderByIdAsync(long id)
    {
        await _service.DeleteFqcOrderByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除出货检验单表(FqcOrder)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:fqcorder:delete", "批量删除出货检验单表(FqcOrder)")]
    public async Task<ActionResult> DeleteFqcOrderBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFqcOrderBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新出货检验单表(FqcOrder)Order
    /// </summary>
    [HttpPut("status-order")]
    [TaktPermission("logistics:quality:operation:fqcorder:update", "更新出货检验单表(FqcOrder)Order")]
    public async Task<ActionResult<TaktFqcOrderDto>> UpdateFqcOrderOrderStatusAsync([FromBody] TaktFqcOrderOrderStatusDto dto)
    {
        var result = await _service.UpdateFqcOrderOrderStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取出货检验单表(FqcOrder)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:fqcorder:import", "获取出货检验单表(FqcOrder)导入模板")]
    public async Task<IActionResult> GetFqcOrderTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFqcOrderTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入出货检验单表(FqcOrder)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:fqcorder:import", "导入出货检验单表(FqcOrder)")]
    public async Task<ActionResult<object>> ImportFqcOrderAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFqcOrderAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出出货检验单表(FqcOrder)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:fqcorder:export", "导出出货检验单表(FqcOrder)")]
    public async Task<IActionResult> ExportFqcOrderAsync([FromBody] TaktFqcOrderQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFqcOrderAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
