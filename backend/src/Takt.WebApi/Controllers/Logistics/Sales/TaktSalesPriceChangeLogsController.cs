// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Sales
// 文件名称：TaktSalesPriceChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：销售价格变更记录表控制器，提供SalesPriceChangeLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Sales;
using Takt.Application.Services.Logistics.Sales;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Sales;

/// <summary>
/// 销售价格变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "销售价格变更记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:sales:salespricechangelog", "销售价格变更记录表管理")]
public class TaktSalesPriceChangeLogsController : TaktControllerBase
{
    private readonly ITaktSalesPriceChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesPriceChangeLogsController(
        ITaktSalesPriceChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取销售价格变更记录表(SalesPriceChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:sales:salespricechangelog:list", "查询销售价格变更记录表(SalesPriceChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalesPriceChangeLogDto>>> GetSalesPriceChangeLogListAsync([FromQuery] TaktSalesPriceChangeLogQueryDto queryDto)
    {
        var result = await _service.GetSalesPriceChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:sales:salespricechangelog:query", "查询销售价格变更记录表(SalesPriceChangeLog)详情")]
    public async Task<ActionResult<TaktSalesPriceChangeLogDto>> GetSalesPriceChangeLogByIdAsync(long id)
    {
        var item = await _service.GetSalesPriceChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取销售价格变更记录表(SalesPriceChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:sales:salespricechangelog:query", "查询销售价格变更记录表(SalesPriceChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalesPriceChangeLogOptionsAsync()
    {
        var result = await _service.GetSalesPriceChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:sales:salespricechangelog:create", "创建销售价格变更记录表(SalesPriceChangeLog)")]
    public async Task<ActionResult<TaktSalesPriceChangeLogDto>> CreateSalesPriceChangeLogAsync([FromBody] TaktSalesPriceChangeLogCreateDto dto)
    {
        var result = await _service.CreateSalesPriceChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetSalesPriceChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:sales:salespricechangelog:update", "更新销售价格变更记录表(SalesPriceChangeLog)")]
    public async Task<ActionResult<TaktSalesPriceChangeLogDto>> UpdateSalesPriceChangeLogAsync(long id, [FromBody] TaktSalesPriceChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateSalesPriceChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:sales:salespricechangelog:delete", "删除销售价格变更记录表(SalesPriceChangeLog)")]
    public async Task<ActionResult> DeleteSalesPriceChangeLogByIdAsync(long id)
    {
        await _service.DeleteSalesPriceChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:sales:salespricechangelog:delete", "批量删除销售价格变更记录表(SalesPriceChangeLog)")]
    public async Task<ActionResult> DeleteSalesPriceChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalesPriceChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取销售价格变更记录表(SalesPriceChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:sales:salespricechangelog:import", "获取销售价格变更记录表(SalesPriceChangeLog)导入模板")]
    public async Task<IActionResult> GetSalesPriceChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalesPriceChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:sales:salespricechangelog:import", "导入销售价格变更记录表(SalesPriceChangeLog)")]
    public async Task<ActionResult<object>> ImportSalesPriceChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalesPriceChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出销售价格变更记录表(SalesPriceChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:sales:salespricechangelog:export", "导出销售价格变更记录表(SalesPriceChangeLog)")]
    public async Task<IActionResult> ExportSalesPriceChangeLogAsync([FromBody] TaktSalesPriceChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalesPriceChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
