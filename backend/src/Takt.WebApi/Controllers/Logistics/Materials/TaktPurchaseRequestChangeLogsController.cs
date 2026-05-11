// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPurchaseRequestChangeLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：采购申请变更记录表控制器，提供PurchaseRequestChangeLog管理的RESTful API接口
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
/// 采购申请变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "采购申请变更记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:purchaserequestchangelog", "采购申请变更记录表管理")]
public class TaktPurchaseRequestChangeLogsController : TaktControllerBase
{
    private readonly ITaktPurchaseRequestChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPurchaseRequestChangeLogsController(
        ITaktPurchaseRequestChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取采购申请变更记录表(PurchaseRequestChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:purchaserequestchangelog:list", "查询采购申请变更记录表(PurchaseRequestChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPurchaseRequestChangeLogDto>>> GetPurchaseRequestChangeLogListAsync([FromQuery] TaktPurchaseRequestChangeLogQueryDto queryDto)
    {
        var result = await _service.GetPurchaseRequestChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取采购申请变更记录表(PurchaseRequestChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:purchaserequestchangelog:query", "查询采购申请变更记录表(PurchaseRequestChangeLog)详情")]
    public async Task<ActionResult<TaktPurchaseRequestChangeLogDto>> GetPurchaseRequestChangeLogByIdAsync(long id)
    {
        var item = await _service.GetPurchaseRequestChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取采购申请变更记录表(PurchaseRequestChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:purchaserequestchangelog:query", "查询采购申请变更记录表(PurchaseRequestChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPurchaseRequestChangeLogOptionsAsync()
    {
        var result = await _service.GetPurchaseRequestChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建采购申请变更记录表(PurchaseRequestChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:purchaserequestchangelog:create", "创建采购申请变更记录表(PurchaseRequestChangeLog)")]
    public async Task<ActionResult<TaktPurchaseRequestChangeLogDto>> CreatePurchaseRequestChangeLogAsync([FromBody] TaktPurchaseRequestChangeLogCreateDto dto)
    {
        var result = await _service.CreatePurchaseRequestChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetPurchaseRequestChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新采购申请变更记录表(PurchaseRequestChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:purchaserequestchangelog:update", "更新采购申请变更记录表(PurchaseRequestChangeLog)")]
    public async Task<ActionResult<TaktPurchaseRequestChangeLogDto>> UpdatePurchaseRequestChangeLogAsync(long id, [FromBody] TaktPurchaseRequestChangeLogUpdateDto dto)
    {
        var result = await _service.UpdatePurchaseRequestChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除采购申请变更记录表(PurchaseRequestChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:purchaserequestchangelog:delete", "删除采购申请变更记录表(PurchaseRequestChangeLog)")]
    public async Task<ActionResult> DeletePurchaseRequestChangeLogByIdAsync(long id)
    {
        await _service.DeletePurchaseRequestChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除采购申请变更记录表(PurchaseRequestChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:purchaserequestchangelog:delete", "批量删除采购申请变更记录表(PurchaseRequestChangeLog)")]
    public async Task<ActionResult> DeletePurchaseRequestChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePurchaseRequestChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取采购申请变更记录表(PurchaseRequestChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:purchaserequestchangelog:import", "获取采购申请变更记录表(PurchaseRequestChangeLog)导入模板")]
    public async Task<IActionResult> GetPurchaseRequestChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPurchaseRequestChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入采购申请变更记录表(PurchaseRequestChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:purchaserequestchangelog:import", "导入采购申请变更记录表(PurchaseRequestChangeLog)")]
    public async Task<ActionResult<object>> ImportPurchaseRequestChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPurchaseRequestChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出采购申请变更记录表(PurchaseRequestChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:purchaserequestchangelog:export", "导出采购申请变更记录表(PurchaseRequestChangeLog)")]
    public async Task<IActionResult> ExportPurchaseRequestChangeLogAsync([FromBody] TaktPurchaseRequestChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPurchaseRequestChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
