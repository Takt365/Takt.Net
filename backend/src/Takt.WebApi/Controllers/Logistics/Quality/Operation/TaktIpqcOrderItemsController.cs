// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktIpqcOrderItemsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：制程检验单明细表控制器，提供IpqcOrderItem管理的RESTful API接口
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
/// 制程检验单明细表控制器
/// </summary>
[Route("api/[controller]", Name = "制程检验单明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:ipqcorderitem", "制程检验单明细表管理")]
public class TaktIpqcOrderItemsController : TaktControllerBase
{
    private readonly ITaktIpqcOrderItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIpqcOrderItemsController(
        ITaktIpqcOrderItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取制程检验单明细表(IpqcOrderItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:list", "查询制程检验单明细表(IpqcOrderItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktIpqcOrderItemDto>>> GetIpqcOrderItemListAsync([FromQuery] TaktIpqcOrderItemQueryDto queryDto)
    {
        var result = await _service.GetIpqcOrderItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取制程检验单明细表(IpqcOrderItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:query", "查询制程检验单明细表(IpqcOrderItem)详情")]
    public async Task<ActionResult<TaktIpqcOrderItemDto>> GetIpqcOrderItemByIdAsync(long id)
    {
        var item = await _service.GetIpqcOrderItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取制程检验单明细表(IpqcOrderItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:query", "查询制程检验单明细表(IpqcOrderItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetIpqcOrderItemOptionsAsync()
    {
        var result = await _service.GetIpqcOrderItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建制程检验单明细表(IpqcOrderItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:create", "创建制程检验单明细表(IpqcOrderItem)")]
    public async Task<ActionResult<TaktIpqcOrderItemDto>> CreateIpqcOrderItemAsync([FromBody] TaktIpqcOrderItemCreateDto dto)
    {
        var result = await _service.CreateIpqcOrderItemAsync(dto);
        return CreatedAtAction(nameof(GetIpqcOrderItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新制程检验单明细表(IpqcOrderItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:update", "更新制程检验单明细表(IpqcOrderItem)")]
    public async Task<ActionResult<TaktIpqcOrderItemDto>> UpdateIpqcOrderItemAsync(long id, [FromBody] TaktIpqcOrderItemUpdateDto dto)
    {
        var result = await _service.UpdateIpqcOrderItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除制程检验单明细表(IpqcOrderItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:delete", "删除制程检验单明细表(IpqcOrderItem)")]
    public async Task<ActionResult> DeleteIpqcOrderItemByIdAsync(long id)
    {
        await _service.DeleteIpqcOrderItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除制程检验单明细表(IpqcOrderItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:delete", "批量删除制程检验单明细表(IpqcOrderItem)")]
    public async Task<ActionResult> DeleteIpqcOrderItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteIpqcOrderItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取制程检验单明细表(IpqcOrderItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:import", "获取制程检验单明细表(IpqcOrderItem)导入模板")]
    public async Task<IActionResult> GetIpqcOrderItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetIpqcOrderItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入制程检验单明细表(IpqcOrderItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:import", "导入制程检验单明细表(IpqcOrderItem)")]
    public async Task<ActionResult<object>> ImportIpqcOrderItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportIpqcOrderItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出制程检验单明细表(IpqcOrderItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:ipqcorderitem:export", "导出制程检验单明细表(IpqcOrderItem)")]
    public async Task<IActionResult> ExportIpqcOrderItemAsync([FromBody] TaktIpqcOrderItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportIpqcOrderItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
