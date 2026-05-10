// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktIqcOrdersController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：进货检验单表控制器，提供IqcOrder管理的RESTful API接口
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
/// 进货检验单表控制器
/// </summary>
[Route("api/[controller]", Name = "进货检验单表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:iqcorder", "进货检验单表管理")]
public class TaktIqcOrdersController : TaktControllerBase
{
    private readonly ITaktIqcOrderService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIqcOrdersController(
        ITaktIqcOrderService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取进货检验单表(IqcOrder)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:iqcorder:list", "查询进货检验单表(IqcOrder)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktIqcOrderDto>>> GetIqcOrderListAsync([FromQuery] TaktIqcOrderQueryDto queryDto)
    {
        var result = await _service.GetIqcOrderListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取进货检验单表(IqcOrder)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:iqcorder:query", "查询进货检验单表(IqcOrder)详情")]
    public async Task<ActionResult<TaktIqcOrderDto>> GetIqcOrderByIdAsync(long id)
    {
        var item = await _service.GetIqcOrderByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取进货检验单表(IqcOrder)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:iqcorder:query", "查询进货检验单表(IqcOrder)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetIqcOrderOptionsAsync()
    {
        var result = await _service.GetIqcOrderOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建进货检验单表(IqcOrder)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:iqcorder:create", "创建进货检验单表(IqcOrder)")]
    public async Task<ActionResult<TaktIqcOrderDto>> CreateIqcOrderAsync([FromBody] TaktIqcOrderCreateDto dto)
    {
        var result = await _service.CreateIqcOrderAsync(dto);
        return CreatedAtAction(nameof(GetIqcOrderByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新进货检验单表(IqcOrder)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:iqcorder:update", "更新进货检验单表(IqcOrder)")]
    public async Task<ActionResult<TaktIqcOrderDto>> UpdateIqcOrderAsync(long id, [FromBody] TaktIqcOrderUpdateDto dto)
    {
        var result = await _service.UpdateIqcOrderAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除进货检验单表(IqcOrder)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:iqcorder:delete", "删除进货检验单表(IqcOrder)")]
    public async Task<ActionResult> DeleteIqcOrderByIdAsync(long id)
    {
        await _service.DeleteIqcOrderByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除进货检验单表(IqcOrder)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:iqcorder:delete", "批量删除进货检验单表(IqcOrder)")]
    public async Task<ActionResult> DeleteIqcOrderBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteIqcOrderBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新进货检验单表(IqcOrder)Order
    /// </summary>
    [HttpPut("status-order")]
    [TaktPermission("logistics:quality:operation:iqcorder:update", "更新进货检验单表(IqcOrder)Order")]
    public async Task<ActionResult<TaktIqcOrderDto>> UpdateIqcOrderOrderStatusAsync([FromBody] TaktIqcOrderOrderStatusDto dto)
    {
        var result = await _service.UpdateIqcOrderOrderStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取进货检验单表(IqcOrder)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:iqcorder:import", "获取进货检验单表(IqcOrder)导入模板")]
    public async Task<IActionResult> GetIqcOrderTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetIqcOrderTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入进货检验单表(IqcOrder)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:iqcorder:import", "导入进货检验单表(IqcOrder)")]
    public async Task<ActionResult<object>> ImportIqcOrderAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportIqcOrderAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出进货检验单表(IqcOrder)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:iqcorder:export", "导出进货检验单表(IqcOrder)")]
    public async Task<IActionResult> ExportIqcOrderAsync([FromBody] TaktIqcOrderQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportIqcOrderAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
