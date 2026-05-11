// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationReliabilitysController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务信赖性评价ORT费用明细表控制器，提供QualityOperationReliability管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Cost;
using Takt.Application.Services.Logistics.Quality.Cost;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Cost;

/// <summary>
/// 品质业务信赖性评价ORT费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "品质业务信赖性评价ORT费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityoperationreliability", "品质业务信赖性评价ORT费用明细表管理")]
public class TaktQualityOperationReliabilitysController : TaktControllerBase
{
    private readonly ITaktQualityOperationReliabilityService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationReliabilitysController(
        ITaktQualityOperationReliabilityService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质业务信赖性评价ORT费用明细表(QualityOperationReliability)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:list", "查询品质业务信赖性评价ORT费用明细表(QualityOperationReliability)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityOperationReliabilityDto>>> GetQualityOperationReliabilityListAsync([FromQuery] TaktQualityOperationReliabilityQueryDto queryDto)
    {
        var result = await _service.GetQualityOperationReliabilityListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质业务信赖性评价ORT费用明细表(QualityOperationReliability)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:query", "查询品质业务信赖性评价ORT费用明细表(QualityOperationReliability)详情")]
    public async Task<ActionResult<TaktQualityOperationReliabilityDto>> GetQualityOperationReliabilityByIdAsync(long id)
    {
        var item = await _service.GetQualityOperationReliabilityByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质业务信赖性评价ORT费用明细表(QualityOperationReliability)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:query", "查询品质业务信赖性评价ORT费用明细表(QualityOperationReliability)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityOperationReliabilityOptionsAsync()
    {
        var result = await _service.GetQualityOperationReliabilityOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质业务信赖性评价ORT费用明细表(QualityOperationReliability)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:create", "创建品质业务信赖性评价ORT费用明细表(QualityOperationReliability)")]
    public async Task<ActionResult<TaktQualityOperationReliabilityDto>> CreateQualityOperationReliabilityAsync([FromBody] TaktQualityOperationReliabilityCreateDto dto)
    {
        var result = await _service.CreateQualityOperationReliabilityAsync(dto);
        return CreatedAtAction(nameof(GetQualityOperationReliabilityByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质业务信赖性评价ORT费用明细表(QualityOperationReliability)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:update", "更新品质业务信赖性评价ORT费用明细表(QualityOperationReliability)")]
    public async Task<ActionResult<TaktQualityOperationReliabilityDto>> UpdateQualityOperationReliabilityAsync(long id, [FromBody] TaktQualityOperationReliabilityUpdateDto dto)
    {
        var result = await _service.UpdateQualityOperationReliabilityAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质业务信赖性评价ORT费用明细表(QualityOperationReliability)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:delete", "删除品质业务信赖性评价ORT费用明细表(QualityOperationReliability)")]
    public async Task<ActionResult> DeleteQualityOperationReliabilityByIdAsync(long id)
    {
        await _service.DeleteQualityOperationReliabilityByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质业务信赖性评价ORT费用明细表(QualityOperationReliability)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:delete", "批量删除品质业务信赖性评价ORT费用明细表(QualityOperationReliability)")]
    public async Task<ActionResult> DeleteQualityOperationReliabilityBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityOperationReliabilityBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质业务信赖性评价ORT费用明细表(QualityOperationReliability)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:import", "获取品质业务信赖性评价ORT费用明细表(QualityOperationReliability)导入模板")]
    public async Task<IActionResult> GetQualityOperationReliabilityTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityOperationReliabilityTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质业务信赖性评价ORT费用明细表(QualityOperationReliability)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:import", "导入品质业务信赖性评价ORT费用明细表(QualityOperationReliability)")]
    public async Task<ActionResult<object>> ImportQualityOperationReliabilityAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityOperationReliabilityAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质业务信赖性评价ORT费用明细表(QualityOperationReliability)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityoperationreliability:export", "导出品质业务信赖性评价ORT费用明细表(QualityOperationReliability)")]
    public async Task<IActionResult> ExportQualityOperationReliabilityAsync([FromBody] TaktQualityOperationReliabilityQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityOperationReliabilityAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
