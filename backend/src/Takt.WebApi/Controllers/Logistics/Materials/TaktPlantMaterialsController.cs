// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPlantMaterialsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：工厂物料表控制器，提供PlantMaterial管理的RESTful API接口
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
/// 工厂物料表控制器
/// </summary>
[Route("api/[controller]", Name = "工厂物料表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:plantmaterial", "工厂物料表管理")]
public class TaktPlantMaterialsController : TaktControllerBase
{
    private readonly ITaktPlantMaterialService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantMaterialsController(
        ITaktPlantMaterialService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取工厂物料表(PlantMaterial)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:plantmaterial:list", "查询工厂物料表(PlantMaterial)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPlantMaterialDto>>> GetPlantMaterialListAsync([FromQuery] TaktPlantMaterialQueryDto queryDto)
    {
        var result = await _service.GetPlantMaterialListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取工厂物料表(PlantMaterial)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:plantmaterial:query", "查询工厂物料表(PlantMaterial)详情")]
    public async Task<ActionResult<TaktPlantMaterialDto>> GetPlantMaterialByIdAsync(long id)
    {
        var item = await _service.GetPlantMaterialByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取工厂物料表(PlantMaterial)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:plantmaterial:query", "查询工厂物料表(PlantMaterial)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPlantMaterialOptionsAsync()
    {
        var result = await _service.GetPlantMaterialOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建工厂物料表(PlantMaterial)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:plantmaterial:create", "创建工厂物料表(PlantMaterial)")]
    public async Task<ActionResult<TaktPlantMaterialDto>> CreatePlantMaterialAsync([FromBody] TaktPlantMaterialCreateDto dto)
    {
        var result = await _service.CreatePlantMaterialAsync(dto);
        return CreatedAtAction(nameof(GetPlantMaterialByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新工厂物料表(PlantMaterial)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:plantmaterial:update", "更新工厂物料表(PlantMaterial)")]
    public async Task<ActionResult<TaktPlantMaterialDto>> UpdatePlantMaterialAsync(long id, [FromBody] TaktPlantMaterialUpdateDto dto)
    {
        var result = await _service.UpdatePlantMaterialAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除工厂物料表(PlantMaterial)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:plantmaterial:delete", "删除工厂物料表(PlantMaterial)")]
    public async Task<ActionResult> DeletePlantMaterialByIdAsync(long id)
    {
        await _service.DeletePlantMaterialByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除工厂物料表(PlantMaterial)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:plantmaterial:delete", "批量删除工厂物料表(PlantMaterial)")]
    public async Task<ActionResult> DeletePlantMaterialBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePlantMaterialBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新工厂物料表(PlantMaterial)Material
    /// </summary>
    [HttpPut("status-material")]
    [TaktPermission("logistics:materials:plantmaterial:update", "更新工厂物料表(PlantMaterial)Material")]
    public async Task<ActionResult<TaktPlantMaterialDto>> UpdatePlantMaterialMaterialStatusAsync([FromBody] TaktPlantMaterialMaterialStatusDto dto)
    {
        var result = await _service.UpdatePlantMaterialMaterialStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取工厂物料表(PlantMaterial)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:plantmaterial:import", "获取工厂物料表(PlantMaterial)导入模板")]
    public async Task<IActionResult> GetPlantMaterialTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPlantMaterialTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入工厂物料表(PlantMaterial)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:plantmaterial:import", "导入工厂物料表(PlantMaterial)")]
    public async Task<ActionResult<object>> ImportPlantMaterialAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPlantMaterialAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出工厂物料表(PlantMaterial)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:plantmaterial:export", "导出工厂物料表(PlantMaterial)")]
    public async Task<IActionResult> ExportPlantMaterialAsync([FromBody] TaktPlantMaterialQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPlantMaterialAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
