// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchasePriceChangeLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购价格变更记录表控制器，提供PurchasePriceChangeLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Materials;
using Takt.Application.Services.Logistics.Materials;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Materials;

/// <summary>
/// 采购价格变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "采购价格变更记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchasepricechangelog", "采购价格变更记录表管理")]
public class TaktPurchasePriceChangeLogsController : TaktControllerBase
{
    private readonly ITaktPurchasePriceChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchasePriceChangeLogsController(
        ITaktPurchasePriceChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购价格变更记录表(PurchasePriceChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchasepricechangelog:list", "查询采购价格变更记录表(PurchasePriceChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchasePriceChangeLogDto>>> GetPurchasePriceChangeLogListAsync([FromQuery] TaktPurchasePriceChangeLogQueryDto queryDto)
    {
        var result = await _service.GetPurchasePriceChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchasepricechangelog:query", "查询采购价格变更记录表(PurchasePriceChangeLog)详情")]
    public async Task<ActionResult<TaktPurchasePriceChangeLogDto>> GetPurchasePriceChangeLogByIdAsync(long id)
    {
        var item = await _service.GetPurchasePriceChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购价格变更记录表(PurchasePriceChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchasepricechangelog:query", "查询采购价格变更记录表(PurchasePriceChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchasePriceChangeLogOptionsAsync()
    {
        var result = await _service.GetPurchasePriceChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchasepricechangelog:create", "创建采购价格变更记录表(PurchasePriceChangeLog)")]
    public async Task<ActionResult<TaktPurchasePriceChangeLogDto>> CreatePurchasePriceChangeLogAsync([FromBody] TaktPurchasePriceChangeLogCreateDto dto)
    {
        var result = await _service.CreatePurchasePriceChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetPurchasePriceChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchasepricechangelog:update", "更新采购价格变更记录表(PurchasePriceChangeLog)")]
    public async Task<ActionResult<TaktPurchasePriceChangeLogDto>> UpdatePurchasePriceChangeLogAsync(long id, [FromBody] TaktPurchasePriceChangeLogUpdateDto dto)
    {
        var result = await _service.UpdatePurchasePriceChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchasepricechangelog:delete", "删除采购价格变更记录表(PurchasePriceChangeLog)")]
    public async Task<ActionResult> DeletePurchasePriceChangeLogByIdAsync(long id)
    {
        await _service.DeletePurchasePriceChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchasepricechangelog:delete", "批量删除采购价格变更记录表(PurchasePriceChangeLog)")]
    public async Task<ActionResult> DeletePurchasePriceChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchasePriceChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取采购价格变更记录表(PurchasePriceChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchasepricechangelog:import", "获取采购价格变更记录表(PurchasePriceChangeLog)导入模板")]
    public async Task<IActionResult> GetPurchasePriceChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchasePriceChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchasepricechangelog:import", "导入采购价格变更记录表(PurchasePriceChangeLog)")]
    public async Task<ActionResult<object>> ImportPurchasePriceChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchasePriceChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购价格变更记录表(PurchasePriceChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchasepricechangelog:export", "导出采购价格变更记录表(PurchasePriceChangeLog)")]
    public async Task<IActionResult> ExportPurchasePriceChangeLogAsync([FromBody] TaktPurchasePriceChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchasePriceChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
