// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktIqcOrderItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：进货检验单明细表控制器，提供IqcOrderItem管理的RESTful API接口
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
/// 进货检验单明细表控制器
/// </summary>
[Route("api/[controller]", Name = "进货检验单明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:iqcorderitem", "进货检验单明细表管理")]
public class TaktIqcOrderItemsController : TaktControllerBase
{
    private readonly ITaktIqcOrderItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIqcOrderItemsController(
        ITaktIqcOrderItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取进货检验单明细表(IqcOrderItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:list", "查询进货检验单明细表(IqcOrderItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktIqcOrderItemDto>>> GetIqcOrderItemListAsync([FromQuery] TaktIqcOrderItemQueryDto queryDto)
    {
        var result = await _service.GetIqcOrderItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取进货检验单明细表(IqcOrderItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:query", "查询进货检验单明细表(IqcOrderItem)详情")]
    public async Task<ActionResult<TaktIqcOrderItemDto>> GetIqcOrderItemByIdAsync(long id)
    {
        var item = await _service.GetIqcOrderItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取进货检验单明细表(IqcOrderItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:query", "查询进货检验单明细表(IqcOrderItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetIqcOrderItemOptionsAsync()
    {
        var result = await _service.GetIqcOrderItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建进货检验单明细表(IqcOrderItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:iqcorderitem:create", "创建进货检验单明细表(IqcOrderItem)")]
    public async Task<ActionResult<TaktIqcOrderItemDto>> CreateIqcOrderItemAsync([FromBody] TaktIqcOrderItemCreateDto dto)
    {
        var result = await _service.CreateIqcOrderItemAsync(dto);
        return CreatedAtAction(nameof(GetIqcOrderItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新进货检验单明细表(IqcOrderItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:update", "更新进货检验单明细表(IqcOrderItem)")]
    public async Task<ActionResult<TaktIqcOrderItemDto>> UpdateIqcOrderItemAsync(long id, [FromBody] TaktIqcOrderItemUpdateDto dto)
    {
        var result = await _service.UpdateIqcOrderItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除进货检验单明细表(IqcOrderItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:delete", "删除进货检验单明细表(IqcOrderItem)")]
    public async Task<ActionResult> DeleteIqcOrderItemByIdAsync(long id)
    {
        await _service.DeleteIqcOrderItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除进货检验单明细表(IqcOrderItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:delete", "批量删除进货检验单明细表(IqcOrderItem)")]
    public async Task<ActionResult> DeleteIqcOrderItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteIqcOrderItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新进货检验单明细表(IqcOrderItem)Judge
    /// </summary>
    [HttpPut("status-judge")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:update", "更新进货检验单明细表(IqcOrderItem)Judge")]
    public async Task<ActionResult<TaktIqcOrderItemDto>> UpdateIqcOrderItemJudgeStatusAsync([FromBody] TaktIqcOrderItemJudgeStatusDto dto)
    {
        var result = await _service.UpdateIqcOrderItemJudgeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取进货检验单明细表(IqcOrderItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:import", "获取进货检验单明细表(IqcOrderItem)导入模板")]
    public async Task<IActionResult> GetIqcOrderItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetIqcOrderItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入进货检验单明细表(IqcOrderItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:import", "导入进货检验单明细表(IqcOrderItem)")]
    public async Task<ActionResult<object>> ImportIqcOrderItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportIqcOrderItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出进货检验单明细表(IqcOrderItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:iqcorderitem:export", "导出进货检验单明细表(IqcOrderItem)")]
    public async Task<IActionResult> ExportIqcOrderItemAsync([FromBody] TaktIqcOrderItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportIqcOrderItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
