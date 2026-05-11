// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchaseRequestItemsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购申请明细表控制器，提供PurchaseRequestItem管理的RESTful API接口
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
/// 采购申请明细表控制器
/// </summary>
[Route("api/[controller]", Name = "采购申请明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchaserequestitem", "采购申请明细表管理")]
public class TaktPurchaseRequestItemsController : TaktControllerBase
{
    private readonly ITaktPurchaseRequestItemService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestItemsController(
        ITaktPurchaseRequestItemService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购申请明细表(PurchaseRequestItem)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchaserequestitem:list", "查询采购申请明细表(PurchaseRequestItem)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchaseRequestItemDto>>> GetPurchaseRequestItemListAsync([FromQuery] TaktPurchaseRequestItemQueryDto queryDto)
    {
        var result = await _service.GetPurchaseRequestItemListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购申请明细表(PurchaseRequestItem)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchaserequestitem:query", "查询采购申请明细表(PurchaseRequestItem)详情")]
    public async Task<ActionResult<TaktPurchaseRequestItemDto>> GetPurchaseRequestItemByIdAsync(long id)
    {
        var item = await _service.GetPurchaseRequestItemByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购申请明细表(PurchaseRequestItem)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchaserequestitem:query", "查询采购申请明细表(PurchaseRequestItem)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchaseRequestItemOptionsAsync()
    {
        var result = await _service.GetPurchaseRequestItemOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购申请明细表(PurchaseRequestItem)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchaserequestitem:create", "创建采购申请明细表(PurchaseRequestItem)")]
    public async Task<ActionResult<TaktPurchaseRequestItemDto>> CreatePurchaseRequestItemAsync([FromBody] TaktPurchaseRequestItemCreateDto dto)
    {
        var result = await _service.CreatePurchaseRequestItemAsync(dto);
        return CreatedAtAction(nameof(GetPurchaseRequestItemByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购申请明细表(PurchaseRequestItem)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchaserequestitem:update", "更新采购申请明细表(PurchaseRequestItem)")]
    public async Task<ActionResult<TaktPurchaseRequestItemDto>> UpdatePurchaseRequestItemAsync(long id, [FromBody] TaktPurchaseRequestItemUpdateDto dto)
    {
        var result = await _service.UpdatePurchaseRequestItemAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购申请明细表(PurchaseRequestItem)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchaserequestitem:delete", "删除采购申请明细表(PurchaseRequestItem)")]
    public async Task<ActionResult> DeletePurchaseRequestItemByIdAsync(long id)
    {
        await _service.DeletePurchaseRequestItemByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购申请明细表(PurchaseRequestItem)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchaserequestitem:delete", "批量删除采购申请明细表(PurchaseRequestItem)")]
    public async Task<ActionResult> DeletePurchaseRequestItemBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchaseRequestItemBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取采购申请明细表(PurchaseRequestItem)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchaserequestitem:import", "获取采购申请明细表(PurchaseRequestItem)导入模板")]
    public async Task<IActionResult> GetPurchaseRequestItemTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchaseRequestItemTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购申请明细表(PurchaseRequestItem)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchaserequestitem:import", "导入采购申请明细表(PurchaseRequestItem)")]
    public async Task<ActionResult<object>> ImportPurchaseRequestItemAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchaseRequestItemAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购申请明细表(PurchaseRequestItem)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchaserequestitem:export", "导出采购申请明细表(PurchaseRequestItem)")]
    public async Task<IActionResult> ExportPurchaseRequestItemAsync([FromBody] TaktPurchaseRequestItemQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchaseRequestItemAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
