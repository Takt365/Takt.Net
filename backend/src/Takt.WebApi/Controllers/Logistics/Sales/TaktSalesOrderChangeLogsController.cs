// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Sales
// 文件名称：TaktSalesOrderChangeLogsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：销售订单变更记录表控制器，提供SalesOrderChangeLog管理的RESTful API接口
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
/// 销售订单变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "销售订单变更记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:sales:salesorderchangelog", "销售订单变更记录表管理")]
public class TaktSalesOrderChangeLogsController : TaktControllerBase
{
    private readonly ITaktSalesOrderChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSalesOrderChangeLogsController(
        ITaktSalesOrderChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取销售订单变更记录表(SalesOrderChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:sales:salesorderchangelog:list", "查询销售订单变更记录表(SalesOrderChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSalesOrderChangeLogDto>>> GetSalesOrderChangeLogListAsync([FromQuery] TaktSalesOrderChangeLogQueryDto queryDto)
    {
        var result = await _service.GetSalesOrderChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:sales:salesorderchangelog:query", "查询销售订单变更记录表(SalesOrderChangeLog)详情")]
    public async Task<ActionResult<TaktSalesOrderChangeLogDto>> GetSalesOrderChangeLogByIdAsync(long id)
    {
        var item = await _service.GetSalesOrderChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取销售订单变更记录表(SalesOrderChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:sales:salesorderchangelog:query", "查询销售订单变更记录表(SalesOrderChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSalesOrderChangeLogOptionsAsync()
    {
        var result = await _service.GetSalesOrderChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:sales:salesorderchangelog:create", "创建销售订单变更记录表(SalesOrderChangeLog)")]
    public async Task<ActionResult<TaktSalesOrderChangeLogDto>> CreateSalesOrderChangeLogAsync([FromBody] TaktSalesOrderChangeLogCreateDto dto)
    {
        var result = await _service.CreateSalesOrderChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetSalesOrderChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:sales:salesorderchangelog:update", "更新销售订单变更记录表(SalesOrderChangeLog)")]
    public async Task<ActionResult<TaktSalesOrderChangeLogDto>> UpdateSalesOrderChangeLogAsync(long id, [FromBody] TaktSalesOrderChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateSalesOrderChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:sales:salesorderchangelog:delete", "删除销售订单变更记录表(SalesOrderChangeLog)")]
    public async Task<ActionResult> DeleteSalesOrderChangeLogByIdAsync(long id)
    {
        await _service.DeleteSalesOrderChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:sales:salesorderchangelog:delete", "批量删除销售订单变更记录表(SalesOrderChangeLog)")]
    public async Task<ActionResult> DeleteSalesOrderChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSalesOrderChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取销售订单变更记录表(SalesOrderChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:sales:salesorderchangelog:import", "获取销售订单变更记录表(SalesOrderChangeLog)导入模板")]
    public async Task<IActionResult> GetSalesOrderChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSalesOrderChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:sales:salesorderchangelog:import", "导入销售订单变更记录表(SalesOrderChangeLog)")]
    public async Task<ActionResult<object>> ImportSalesOrderChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSalesOrderChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出销售订单变更记录表(SalesOrderChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:sales:salesorderchangelog:export", "导出销售订单变更记录表(SalesOrderChangeLog)")]
    public async Task<IActionResult> ExportSalesOrderChangeLogAsync([FromBody] TaktSalesOrderChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSalesOrderChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
