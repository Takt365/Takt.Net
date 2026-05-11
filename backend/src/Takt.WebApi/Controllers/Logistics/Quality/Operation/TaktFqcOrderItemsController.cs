// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktFqcOrderItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：出货检验单明细表控制器，提供FqcOrderItem管理的RESTful API接口
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
/// 出货检验单明细表控制器
/// </summary>
[Route("api/[controller]", Name = "出货检验单明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:fqcorderitem", "出货检验单明细表管理")]
public class TaktFqcOrderItemsController : TaktControllerBase
{
    private readonly ITaktFqcOrderItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFqcOrderItemsController(
        ITaktFqcOrderItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取出货检验单明细表(FqcOrderItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:list", "查询出货检验单明细表(FqcOrderItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktFqcOrderItemDto>>> GetFqcOrderItemListAsync([FromQuery] TaktFqcOrderItemQueryDto queryDto)
    {
        var result = await _service.GetFqcOrderItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取出货检验单明细表(FqcOrderItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:query", "查询出货检验单明细表(FqcOrderItem)详情")]
    public async Task<ActionResult<TaktFqcOrderItemDto>> GetFqcOrderItemByIdAsync(long id)
    {
        var item = await _service.GetFqcOrderItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取出货检验单明细表(FqcOrderItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:query", "查询出货检验单明细表(FqcOrderItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetFqcOrderItemOptionsAsync()
    {
        var result = await _service.GetFqcOrderItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建出货检验单明细表(FqcOrderItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:fqcorderitem:create", "创建出货检验单明细表(FqcOrderItem)")]
    public async Task<ActionResult<TaktFqcOrderItemDto>> CreateFqcOrderItemAsync([FromBody] TaktFqcOrderItemCreateDto dto)
    {
        var result = await _service.CreateFqcOrderItemAsync(dto);
        return CreatedAtAction(nameof(GetFqcOrderItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新出货检验单明细表(FqcOrderItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:update", "更新出货检验单明细表(FqcOrderItem)")]
    public async Task<ActionResult<TaktFqcOrderItemDto>> UpdateFqcOrderItemAsync(long id, [FromBody] TaktFqcOrderItemUpdateDto dto)
    {
        var result = await _service.UpdateFqcOrderItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除出货检验单明细表(FqcOrderItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:delete", "删除出货检验单明细表(FqcOrderItem)")]
    public async Task<ActionResult> DeleteFqcOrderItemByIdAsync(long id)
    {
        await _service.DeleteFqcOrderItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除出货检验单明细表(FqcOrderItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:delete", "批量删除出货检验单明细表(FqcOrderItem)")]
    public async Task<ActionResult> DeleteFqcOrderItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteFqcOrderItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新出货检验单明细表(FqcOrderItem)Judge
    /// </summary>
    [HttpPut("status-judge")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:update", "更新出货检验单明细表(FqcOrderItem)Judge")]
    public async Task<ActionResult<TaktFqcOrderItemDto>> UpdateFqcOrderItemJudgeStatusAsync([FromBody] TaktFqcOrderItemJudgeStatusDto dto)
    {
        var result = await _service.UpdateFqcOrderItemJudgeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取出货检验单明细表(FqcOrderItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:import", "获取出货检验单明细表(FqcOrderItem)导入模板")]
    public async Task<IActionResult> GetFqcOrderItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetFqcOrderItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入出货检验单明细表(FqcOrderItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:import", "导入出货检验单明细表(FqcOrderItem)")]
    public async Task<ActionResult<object>> ImportFqcOrderItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportFqcOrderItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出出货检验单明细表(FqcOrderItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:fqcorderitem:export", "导出出货检验单明细表(FqcOrderItem)")]
    public async Task<IActionResult> ExportFqcOrderItemAsync([FromBody] TaktFqcOrderItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportFqcOrderItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
