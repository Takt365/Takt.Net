// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchaseOrderChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：采购订单变更记录表控制器，提供PurchaseOrderChangeLog管理的RESTful API接口
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
/// 采购订单变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "采购订单变更记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchaseorderchangelog", "采购订单变更记录表管理")]
public class TaktPurchaseOrderChangeLogsController : TaktControllerBase
{
    private readonly ITaktPurchaseOrderChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseOrderChangeLogsController(
        ITaktPurchaseOrderChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购订单变更记录表(PurchaseOrderChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchaseorderchangelog:list", "查询采购订单变更记录表(PurchaseOrderChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchaseOrderChangeLogDto>>> GetPurchaseOrderChangeLogListAsync([FromQuery] TaktPurchaseOrderChangeLogQueryDto queryDto)
    {
        var result = await _service.GetPurchaseOrderChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchaseorderchangelog:query", "查询采购订单变更记录表(PurchaseOrderChangeLog)详情")]
    public async Task<ActionResult<TaktPurchaseOrderChangeLogDto>> GetPurchaseOrderChangeLogByIdAsync(long id)
    {
        var item = await _service.GetPurchaseOrderChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购订单变更记录表(PurchaseOrderChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchaseorderchangelog:query", "查询采购订单变更记录表(PurchaseOrderChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchaseOrderChangeLogOptionsAsync()
    {
        var result = await _service.GetPurchaseOrderChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchaseorderchangelog:create", "创建采购订单变更记录表(PurchaseOrderChangeLog)")]
    public async Task<ActionResult<TaktPurchaseOrderChangeLogDto>> CreatePurchaseOrderChangeLogAsync([FromBody] TaktPurchaseOrderChangeLogCreateDto dto)
    {
        var result = await _service.CreatePurchaseOrderChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetPurchaseOrderChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchaseorderchangelog:update", "更新采购订单变更记录表(PurchaseOrderChangeLog)")]
    public async Task<ActionResult<TaktPurchaseOrderChangeLogDto>> UpdatePurchaseOrderChangeLogAsync(long id, [FromBody] TaktPurchaseOrderChangeLogUpdateDto dto)
    {
        var result = await _service.UpdatePurchaseOrderChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchaseorderchangelog:delete", "删除采购订单变更记录表(PurchaseOrderChangeLog)")]
    public async Task<ActionResult> DeletePurchaseOrderChangeLogByIdAsync(long id)
    {
        await _service.DeletePurchaseOrderChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchaseorderchangelog:delete", "批量删除采购订单变更记录表(PurchaseOrderChangeLog)")]
    public async Task<ActionResult> DeletePurchaseOrderChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchaseOrderChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取采购订单变更记录表(PurchaseOrderChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchaseorderchangelog:import", "获取采购订单变更记录表(PurchaseOrderChangeLog)导入模板")]
    public async Task<IActionResult> GetPurchaseOrderChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchaseOrderChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchaseorderchangelog:import", "导入采购订单变更记录表(PurchaseOrderChangeLog)")]
    public async Task<ActionResult<object>> ImportPurchaseOrderChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchaseOrderChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购订单变更记录表(PurchaseOrderChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchaseorderchangelog:export", "导出采购订单变更记录表(PurchaseOrderChangeLog)")]
    public async Task<IActionResult> ExportPurchaseOrderChangeLogAsync([FromBody] TaktPurchaseOrderChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchaseOrderChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
