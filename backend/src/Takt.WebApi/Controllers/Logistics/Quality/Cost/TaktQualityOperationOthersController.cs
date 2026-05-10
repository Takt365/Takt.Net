// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Cost
// 文件名称：TaktQualityOperationOthersController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：品质业务其他通常业务费用明细表控制器，提供QualityOperationOther管理的RESTful API接口
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
/// 品质业务其他通常业务费用明细表控制器
/// </summary>
[Route("api/[controller]", Name = "品质业务其他通常业务费用明细表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:cost:qualityoperationother", "品质业务其他通常业务费用明细表管理")]
public class TaktQualityOperationOthersController : TaktControllerBase
{
    private readonly ITaktQualityOperationOtherService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktQualityOperationOthersController(
        ITaktQualityOperationOtherService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取品质业务其他通常业务费用明细表(QualityOperationOther)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:cost:qualityoperationother:list", "查询品质业务其他通常业务费用明细表(QualityOperationOther)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktQualityOperationOtherDto>>> GetQualityOperationOtherListAsync([FromQuery] TaktQualityOperationOtherQueryDto queryDto)
    {
        var result = await _service.GetQualityOperationOtherListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取品质业务其他通常业务费用明细表(QualityOperationOther)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationother:query", "查询品质业务其他通常业务费用明细表(QualityOperationOther)详情")]
    public async Task<ActionResult<TaktQualityOperationOtherDto>> GetQualityOperationOtherByIdAsync(long id)
    {
        var item = await _service.GetQualityOperationOtherByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取品质业务其他通常业务费用明细表(QualityOperationOther)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:cost:qualityoperationother:query", "查询品质业务其他通常业务费用明细表(QualityOperationOther)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetQualityOperationOtherOptionsAsync()
    {
        var result = await _service.GetQualityOperationOtherOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建品质业务其他通常业务费用明细表(QualityOperationOther)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:cost:qualityoperationother:create", "创建品质业务其他通常业务费用明细表(QualityOperationOther)")]
    public async Task<ActionResult<TaktQualityOperationOtherDto>> CreateQualityOperationOtherAsync([FromBody] TaktQualityOperationOtherCreateDto dto)
    {
        var result = await _service.CreateQualityOperationOtherAsync(dto);
        return CreatedAtAction(nameof(GetQualityOperationOtherByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新品质业务其他通常业务费用明细表(QualityOperationOther)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationother:update", "更新品质业务其他通常业务费用明细表(QualityOperationOther)")]
    public async Task<ActionResult<TaktQualityOperationOtherDto>> UpdateQualityOperationOtherAsync(long id, [FromBody] TaktQualityOperationOtherUpdateDto dto)
    {
        var result = await _service.UpdateQualityOperationOtherAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除品质业务其他通常业务费用明细表(QualityOperationOther)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:cost:qualityoperationother:delete", "删除品质业务其他通常业务费用明细表(QualityOperationOther)")]
    public async Task<ActionResult> DeleteQualityOperationOtherByIdAsync(long id)
    {
        await _service.DeleteQualityOperationOtherByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除品质业务其他通常业务费用明细表(QualityOperationOther)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:cost:qualityoperationother:delete", "批量删除品质业务其他通常业务费用明细表(QualityOperationOther)")]
    public async Task<ActionResult> DeleteQualityOperationOtherBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteQualityOperationOtherBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 获取品质业务其他通常业务费用明细表(QualityOperationOther)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:cost:qualityoperationother:import", "获取品质业务其他通常业务费用明细表(QualityOperationOther)导入模板")]
    public async Task<IActionResult> GetQualityOperationOtherTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetQualityOperationOtherTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入品质业务其他通常业务费用明细表(QualityOperationOther)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:cost:qualityoperationother:import", "导入品质业务其他通常业务费用明细表(QualityOperationOther)")]
    public async Task<ActionResult<object>> ImportQualityOperationOtherAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportQualityOperationOtherAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出品质业务其他通常业务费用明细表(QualityOperationOther)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:cost:qualityoperationother:export", "导出品质业务其他通常业务费用明细表(QualityOperationOther)")]
    public async Task<IActionResult> ExportQualityOperationOtherAsync([FromBody] TaktQualityOperationOtherQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportQualityOperationOtherAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
