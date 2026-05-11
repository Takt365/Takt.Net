// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Materials
// 文件名称：TaktPlantsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：工厂表控制器，提供Plant管理的RESTful API接口
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
/// 工厂表控制器
/// </summary>
[Route("api/[controller]", Name = "工厂表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:materials:plant", "工厂表管理")]
public class TaktPlantsController : TaktControllerBase
{
    private readonly ITaktPlantService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPlantsController(
        ITaktPlantService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取工厂表(Plant)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:materials:plant:list", "查询工厂表(Plant)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPlantDto>>> GetPlantListAsync([FromQuery] TaktPlantQueryDto queryDto)
    {
        var result = await _service.GetPlantListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取工厂表(Plant)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:materials:plant:query", "查询工厂表(Plant)详情")]
    public async Task<ActionResult<TaktPlantDto>> GetPlantByIdAsync(long id)
    {
        var item = await _service.GetPlantByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取工厂表(Plant)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:materials:plant:query", "查询工厂表(Plant)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPlantOptionsAsync()
    {
        var result = await _service.GetPlantOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建工厂表(Plant)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:materials:plant:create", "创建工厂表(Plant)")]
    public async Task<ActionResult<TaktPlantDto>> CreatePlantAsync([FromBody] TaktPlantCreateDto dto)
    {
        var result = await _service.CreatePlantAsync(dto);
        return CreatedAtAction(nameof(GetPlantByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新工厂表(Plant)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:materials:plant:update", "更新工厂表(Plant)")]
    public async Task<ActionResult<TaktPlantDto>> UpdatePlantAsync(long id, [FromBody] TaktPlantUpdateDto dto)
    {
        var result = await _service.UpdatePlantAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除工厂表(Plant)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:materials:plant:delete", "删除工厂表(Plant)")]
    public async Task<ActionResult> DeletePlantByIdAsync(long id)
    {
        await _service.DeletePlantByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除工厂表(Plant)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:materials:plant:delete", "批量删除工厂表(Plant)")]
    public async Task<ActionResult> DeletePlantBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePlantBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新工厂表(Plant)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("logistics:materials:plant:update", "更新工厂表(Plant)状态")]
    public async Task<ActionResult<TaktPlantDto>> UpdatePlantStatusAsync([FromBody] TaktPlantStatusDto dto)
    {
        var result = await _service.UpdatePlantStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新工厂表(Plant)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:materials:plant:update", "更新工厂表(Plant)排序")]
    public async Task<ActionResult<TaktPlantDto>> UpdatePlantSortAsync([FromBody] TaktPlantSortDto dto)
    {
        var result = await _service.UpdatePlantSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取工厂表(Plant)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:materials:plant:import", "获取工厂表(Plant)导入模板")]
    public async Task<IActionResult> GetPlantTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPlantTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入工厂表(Plant)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:materials:plant:import", "导入工厂表(Plant)")]
    public async Task<ActionResult<object>> ImportPlantAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPlantAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出工厂表(Plant)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:materials:plant:export", "导出工厂表(Plant)")]
    public async Task<IActionResult> ExportPlantAsync([FromBody] TaktPlantQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPlantAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
