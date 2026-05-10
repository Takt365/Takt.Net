// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Manufacturing.Bom
// 文件名称：TaktModelDestinationsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：型号目的地表控制器，提供ModelDestination管理的RESTful API接口
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
/// 型号目的地表控制器
/// </summary>
[Route("api/[controller]", Name = "型号目的地表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:manufacturing:bom:modeldestination", "型号目的地表管理")]
public class TaktModelDestinationsController : TaktControllerBase
{
    private readonly ITaktModelDestinationService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktModelDestinationsController(
        ITaktModelDestinationService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取型号目的地表(ModelDestination)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:list", "查询型号目的地表(ModelDestination)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktModelDestinationDto>>> GetModelDestinationListAsync([FromQuery] TaktModelDestinationQueryDto queryDto)
    {
        var result = await _service.GetModelDestinationListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取型号目的地表(ModelDestination)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:query", "查询型号目的地表(ModelDestination)详情")]
    public async Task<ActionResult<TaktModelDestinationDto>> GetModelDestinationByIdAsync(long id)
    {
        var item = await _service.GetModelDestinationByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取型号目的地表(ModelDestination)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:query", "查询型号目的地表(ModelDestination)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetModelDestinationOptionsAsync()
    {
        var result = await _service.GetModelDestinationOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建型号目的地表(ModelDestination)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:create", "创建型号目的地表(ModelDestination)")]
    public async Task<ActionResult<TaktModelDestinationDto>> CreateModelDestinationAsync([FromBody] TaktModelDestinationCreateDto dto)
    {
        var result = await _service.CreateModelDestinationAsync(dto);
        return CreatedAtAction(nameof(GetModelDestinationByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新型号目的地表(ModelDestination)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:update", "更新型号目的地表(ModelDestination)")]
    public async Task<ActionResult<TaktModelDestinationDto>> UpdateModelDestinationAsync(long id, [FromBody] TaktModelDestinationUpdateDto dto)
    {
        var result = await _service.UpdateModelDestinationAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除型号目的地表(ModelDestination)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:delete", "删除型号目的地表(ModelDestination)")]
    public async Task<ActionResult> DeleteModelDestinationByIdAsync(long id)
    {
        await _service.DeleteModelDestinationByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除型号目的地表(ModelDestination)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:delete", "批量删除型号目的地表(ModelDestination)")]
    public async Task<ActionResult> DeleteModelDestinationBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteModelDestinationBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新型号目的地表(ModelDestination)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:update", "更新型号目的地表(ModelDestination)排序")]
    public async Task<ActionResult<TaktModelDestinationDto>> UpdateModelDestinationSortAsync([FromBody] TaktModelDestinationSortDto dto)
    {
        var result = await _service.UpdateModelDestinationSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取型号目的地表(ModelDestination)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:import", "获取型号目的地表(ModelDestination)导入模板")]
    public async Task<IActionResult> GetModelDestinationTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetModelDestinationTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入型号目的地表(ModelDestination)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:import", "导入型号目的地表(ModelDestination)")]
    public async Task<ActionResult<object>> ImportModelDestinationAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportModelDestinationAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出型号目的地表(ModelDestination)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:manufacturing:bom:modeldestination:export", "导出型号目的地表(ModelDestination)")]
    public async Task<IActionResult> ExportModelDestinationAsync([FromBody] TaktModelDestinationQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportModelDestinationAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
