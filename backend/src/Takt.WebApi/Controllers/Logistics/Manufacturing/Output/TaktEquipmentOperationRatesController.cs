// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Output
// 文件名称：TaktEquipmentOperationRatesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：机器稼动率表控制器，提供EquipmentOperationRate管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Manufacturing.Output;
using Takt.Application.Services.Logistics.Manufacturing.Output;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Output;

/// <summary>
/// 机器稼动率表控制器
/// </summary>
[Route("api/[controller]", Name = "机器稼动率表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:output:equipmentoperationrate", "机器稼动率表管理")]
public class TaktEquipmentOperationRatesController : TaktControllerBase
{
    private readonly ITaktEquipmentOperationRateService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktEquipmentOperationRatesController(
        ITaktEquipmentOperationRateService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取机器稼动率表(EquipmentOperationRate)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:list", "查询机器稼动率表(EquipmentOperationRate)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktEquipmentOperationRateDto>>> GetEquipmentOperationRateListAsync([FromQuery] TaktEquipmentOperationRateQueryDto queryDto)
    {
        var result = await _service.GetEquipmentOperationRateListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取机器稼动率表(EquipmentOperationRate)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:query", "查询机器稼动率表(EquipmentOperationRate)详情")]
    public async Task<ActionResult<TaktEquipmentOperationRateDto>> GetEquipmentOperationRateByIdAsync(long id)
    {
        var item = await _service.GetEquipmentOperationRateByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取机器稼动率表(EquipmentOperationRate)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:query", "查询机器稼动率表(EquipmentOperationRate)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetEquipmentOperationRateOptionsAsync()
    {
        var result = await _service.GetEquipmentOperationRateOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建机器稼动率表(EquipmentOperationRate)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:create", "创建机器稼动率表(EquipmentOperationRate)")]
    public async Task<ActionResult<TaktEquipmentOperationRateDto>> CreateEquipmentOperationRateAsync([FromBody] TaktEquipmentOperationRateCreateDto dto)
    {
        var result = await _service.CreateEquipmentOperationRateAsync(dto);
        return CreatedAtAction(nameof(GetEquipmentOperationRateByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新机器稼动率表(EquipmentOperationRate)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:update", "更新机器稼动率表(EquipmentOperationRate)")]
    public async Task<ActionResult<TaktEquipmentOperationRateDto>> UpdateEquipmentOperationRateAsync(long id, [FromBody] TaktEquipmentOperationRateUpdateDto dto)
    {
        var result = await _service.UpdateEquipmentOperationRateAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除机器稼动率表(EquipmentOperationRate)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:delete", "删除机器稼动率表(EquipmentOperationRate)")]
    public async Task<ActionResult> DeleteEquipmentOperationRateByIdAsync(long id)
    {
        await _service.DeleteEquipmentOperationRateByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除机器稼动率表(EquipmentOperationRate)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:delete", "批量删除机器稼动率表(EquipmentOperationRate)")]
    public async Task<ActionResult> DeleteEquipmentOperationRateBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteEquipmentOperationRateBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新机器稼动率表(EquipmentOperationRate)Equipment
    /// </summary>
    [HttpPut("status-equipment")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:update", "更新机器稼动率表(EquipmentOperationRate)Equipment")]
    public async Task<ActionResult<TaktEquipmentOperationRateDto>> UpdateEquipmentOperationRateEquipmentStatusAsync([FromBody] TaktEquipmentOperationRateEquipmentStatusDto dto)
    {
        var result = await _service.UpdateEquipmentOperationRateEquipmentStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新机器稼动率表(EquipmentOperationRate)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:update", "更新机器稼动率表(EquipmentOperationRate)状态")]
    public async Task<ActionResult<TaktEquipmentOperationRateDto>> UpdateEquipmentOperationRateStatusAsync([FromBody] TaktEquipmentOperationRateStatusDto dto)
    {
        var result = await _service.UpdateEquipmentOperationRateStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取机器稼动率表(EquipmentOperationRate)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:import", "获取机器稼动率表(EquipmentOperationRate)导入模板")]
    public async Task<IActionResult> GetEquipmentOperationRateTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetEquipmentOperationRateTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入机器稼动率表(EquipmentOperationRate)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:import", "导入机器稼动率表(EquipmentOperationRate)")]
    public async Task<ActionResult<object>> ImportEquipmentOperationRateAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportEquipmentOperationRateAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出机器稼动率表(EquipmentOperationRate)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:output:equipmentoperationrate:export", "导出机器稼动率表(EquipmentOperationRate)")]
    public async Task<IActionResult> ExportEquipmentOperationRateAsync([FromBody] TaktEquipmentOperationRateQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportEquipmentOperationRateAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
