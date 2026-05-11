// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Bom
// 文件名称：TaktBillOfMaterialChangeLogsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：BOM变更记录表控制器，提供BillOfMaterialChangeLog管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Bom;
using Takt.Application.Services.Logistics.Manufacturing.Bom;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Bom;

/// <summary>
/// BOM变更记录表控制器
/// </summary>
[Route("api/[controller]", Name = "BOM变更记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog", "BOM变更记录表管理")]
public class TaktBillOfMaterialChangeLogsController : TaktControllerBase
{
    private readonly ITaktBillOfMaterialChangeLogService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBillOfMaterialChangeLogsController(
        ITaktBillOfMaterialChangeLogService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取BOM变更记录表(BillOfMaterialChangeLog)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:list", "查询BOM变更记录表(BillOfMaterialChangeLog)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktBillOfMaterialChangeLogDto>>> GetBillOfMaterialChangeLogListAsync([FromQuery] TaktBillOfMaterialChangeLogQueryDto queryDto)
    {
        var result = await _service.GetBillOfMaterialChangeLogListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:query", "查询BOM变更记录表(BillOfMaterialChangeLog)详情")]
    public async Task<ActionResult<TaktBillOfMaterialChangeLogDto>> GetBillOfMaterialChangeLogByIdAsync(long id)
    {
        var item = await _service.GetBillOfMaterialChangeLogByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取BOM变更记录表(BillOfMaterialChangeLog)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:query", "查询BOM变更记录表(BillOfMaterialChangeLog)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetBillOfMaterialChangeLogOptionsAsync()
    {
        var result = await _service.GetBillOfMaterialChangeLogOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:create", "创建BOM变更记录表(BillOfMaterialChangeLog)")]
    public async Task<ActionResult<TaktBillOfMaterialChangeLogDto>> CreateBillOfMaterialChangeLogAsync([FromBody] TaktBillOfMaterialChangeLogCreateDto dto)
    {
        var result = await _service.CreateBillOfMaterialChangeLogAsync(dto);
        return CreatedAtAction(nameof(GetBillOfMaterialChangeLogByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:update", "更新BOM变更记录表(BillOfMaterialChangeLog)")]
    public async Task<ActionResult<TaktBillOfMaterialChangeLogDto>> UpdateBillOfMaterialChangeLogAsync(long id, [FromBody] TaktBillOfMaterialChangeLogUpdateDto dto)
    {
        var result = await _service.UpdateBillOfMaterialChangeLogAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:delete", "删除BOM变更记录表(BillOfMaterialChangeLog)")]
    public async Task<ActionResult> DeleteBillOfMaterialChangeLogByIdAsync(long id)
    {
        await _service.DeleteBillOfMaterialChangeLogByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:delete", "批量删除BOM变更记录表(BillOfMaterialChangeLog)")]
    public async Task<ActionResult> DeleteBillOfMaterialChangeLogBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteBillOfMaterialChangeLogBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取BOM变更记录表(BillOfMaterialChangeLog)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:import", "获取BOM变更记录表(BillOfMaterialChangeLog)导入模板")]
    public async Task<IActionResult> GetBillOfMaterialChangeLogTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetBillOfMaterialChangeLogTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:import", "导入BOM变更记录表(BillOfMaterialChangeLog)")]
    public async Task<ActionResult<object>> ImportBillOfMaterialChangeLogAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportBillOfMaterialChangeLogAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出BOM变更记录表(BillOfMaterialChangeLog)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:bom:billofmaterialchangelog:export", "导出BOM变更记录表(BillOfMaterialChangeLog)")]
    public async Task<IActionResult> ExportBillOfMaterialChangeLogAsync([FromBody] TaktBillOfMaterialChangeLogQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportBillOfMaterialChangeLogAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
