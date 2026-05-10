// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Maintenance
// 文件名称：TaktEquipmentsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：工厂设备表控制器，提供Equipment管理的RESTful API接口
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
/// 工厂设备表控制器
/// </summary>
[Route("api/[controller]", Name = "工厂设备表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:maintenance:equipment", "工厂设备表管理")]
public class TaktEquipmentsController : TaktControllerBase
{
    private readonly ITaktEquipmentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentsController(
        ITaktEquipmentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取工厂设备表(Equipment)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:maintenance:equipment:list", "查询工厂设备表(Equipment)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEquipmentDto>>> GetEquipmentListAsync([FromQuery] TaktEquipmentQueryDto queryDto)
    {
        var result = await _service.GetEquipmentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取工厂设备表(Equipment)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:maintenance:equipment:query", "查询工厂设备表(Equipment)详情")]
    public async Task<ActionResult<TaktEquipmentDto>> GetEquipmentByIdAsync(long id)
    {
        var item = await _service.GetEquipmentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取工厂设备表(Equipment)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:maintenance:equipment:query", "查询工厂设备表(Equipment)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEquipmentOptionsAsync()
    {
        var result = await _service.GetEquipmentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建工厂设备表(Equipment)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:maintenance:equipment:create", "创建工厂设备表(Equipment)")]
    public async Task<ActionResult<TaktEquipmentDto>> CreateEquipmentAsync([FromBody] TaktEquipmentCreateDto dto)
    {
        var result = await _service.CreateEquipmentAsync(dto);
        return CreatedAtAction(nameof(GetEquipmentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新工厂设备表(Equipment)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:maintenance:equipment:update", "更新工厂设备表(Equipment)")]
    public async Task<ActionResult<TaktEquipmentDto>> UpdateEquipmentAsync(long id, [FromBody] TaktEquipmentUpdateDto dto)
    {
        var result = await _service.UpdateEquipmentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除工厂设备表(Equipment)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:maintenance:equipment:delete", "删除工厂设备表(Equipment)")]
    public async Task<ActionResult> DeleteEquipmentByIdAsync(long id)
    {
        await _service.DeleteEquipmentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除工厂设备表(Equipment)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:maintenance:equipment:delete", "批量删除工厂设备表(Equipment)")]
    public async Task<ActionResult> DeleteEquipmentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEquipmentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新工厂设备表(Equipment)Warranty
    /// </summary>
    [HttpPut("status-warranty")]
    [TaktPermission("logistics:maintenance:equipment:update", "更新工厂设备表(Equipment)Warranty")]
    public async Task<ActionResult<TaktEquipmentDto>> UpdateEquipmentWarrantyStatusAsync([FromBody] TaktEquipmentWarrantyStatusDto dto)
    {
        var result = await _service.UpdateEquipmentWarrantyStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新工厂设备表(Equipment)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:maintenance:equipment:update", "更新工厂设备表(Equipment)状态")]
    public async Task<ActionResult<TaktEquipmentDto>> UpdateEquipmentStatusAsync([FromBody] TaktEquipmentStatusDto dto)
    {
        var result = await _service.UpdateEquipmentStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取工厂设备表(Equipment)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:maintenance:equipment:import", "获取工厂设备表(Equipment)导入模板")]
    public async Task<IActionResult> GetEquipmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEquipmentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入工厂设备表(Equipment)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:maintenance:equipment:import", "导入工厂设备表(Equipment)")]
    public async Task<ActionResult<object>> ImportEquipmentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEquipmentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出工厂设备表(Equipment)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:maintenance:equipment:export", "导出工厂设备表(Equipment)")]
    public async Task<IActionResult> ExportEquipmentAsync([FromBody] TaktEquipmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEquipmentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
