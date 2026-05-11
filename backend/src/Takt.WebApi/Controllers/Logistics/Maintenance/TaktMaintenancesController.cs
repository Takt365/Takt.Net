// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Maintenance
// 文件名称：TaktMaintenancesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：设备维护记录表控制器，提供Maintenance管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Maintenance;
using Takt.Application.Services.Logistics.Maintenance;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Maintenance;

/// <summary>
/// 设备维护记录表控制器
/// </summary>
[Route("api/[controller]", Name = "设备维护记录表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:maintenance", "设备维护记录表管理")]
public class TaktMaintenancesController : TaktControllerBase
{
    private readonly ITaktMaintenanceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktMaintenancesController(
        ITaktMaintenanceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取设备维护记录表(Maintenance)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:maintenance:list", "查询设备维护记录表(Maintenance)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktMaintenanceDto>>> GetMaintenanceListAsync([FromQuery] TaktMaintenanceQueryDto queryDto)
    {
        var result = await _service.GetMaintenanceListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取设备维护记录表(Maintenance)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:maintenance:query", "查询设备维护记录表(Maintenance)详情")]
    public async Task<ActionResult<TaktMaintenanceDto>> GetMaintenanceByIdAsync(long id)
    {
        var item = await _service.GetMaintenanceByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取设备维护记录表(Maintenance)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:maintenance:query", "查询设备维护记录表(Maintenance)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetMaintenanceOptionsAsync()
    {
        var result = await _service.GetMaintenanceOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建设备维护记录表(Maintenance)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:maintenance:create", "创建设备维护记录表(Maintenance)")]
    public async Task<ActionResult<TaktMaintenanceDto>> CreateMaintenanceAsync([FromBody] TaktMaintenanceCreateDto dto)
    {
        var result = await _service.CreateMaintenanceAsync(dto);
        return CreatedAtAction(nameof(GetMaintenanceByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新设备维护记录表(Maintenance)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:maintenance:update", "更新设备维护记录表(Maintenance)")]
    public async Task<ActionResult<TaktMaintenanceDto>> UpdateMaintenanceAsync(long id, [FromBody] TaktMaintenanceUpdateDto dto)
    {
        var result = await _service.UpdateMaintenanceAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除设备维护记录表(Maintenance)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:maintenance:delete", "删除设备维护记录表(Maintenance)")]
    public async Task<ActionResult> DeleteMaintenanceByIdAsync(long id)
    {
        await _service.DeleteMaintenanceByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除设备维护记录表(Maintenance)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:maintenance:delete", "批量删除设备维护记录表(Maintenance)")]
    public async Task<ActionResult> DeleteMaintenanceBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteMaintenanceBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新设备维护记录表(Maintenance)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:maintenance:update", "更新设备维护记录表(Maintenance)状态")]
    public async Task<ActionResult<TaktMaintenanceDto>> UpdateMaintenanceStatusAsync([FromBody] TaktMaintenanceStatusDto dto)
    {
        var result = await _service.UpdateMaintenanceStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取设备维护记录表(Maintenance)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:maintenance:import", "获取设备维护记录表(Maintenance)导入模板")]
    public async Task<IActionResult> GetMaintenanceTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetMaintenanceTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入设备维护记录表(Maintenance)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:maintenance:import", "导入设备维护记录表(Maintenance)")]
    public async Task<ActionResult<object>> ImportMaintenanceAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportMaintenanceAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出设备维护记录表(Maintenance)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:maintenance:export", "导出设备维护记录表(Maintenance)")]
    public async Task<IActionResult> ExportMaintenanceAsync([FromBody] TaktMaintenanceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportMaintenanceAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
