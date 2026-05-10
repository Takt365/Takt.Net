// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktInspectionStandardsController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：检验标准表控制器，提供InspectionStandard管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Logistics.Quality.Operation;
using Takt.Application.Services.Logistics.Quality.Operation;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.Logistics.Quality.Operation;

/// <summary>
/// 检验标准表控制器
/// </summary>
[Route("api/[controller]", Name = "检验标准表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:inspectionstandard", "检验标准表管理")]
public class TaktInspectionStandardsController : TaktControllerBase
{
    private readonly ITaktInspectionStandardService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktInspectionStandardsController(
        ITaktInspectionStandardService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取检验标准表(InspectionStandard)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:list", "查询检验标准表(InspectionStandard)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktInspectionStandardDto>>> GetInspectionStandardListAsync([FromQuery] TaktInspectionStandardQueryDto queryDto)
    {
        var result = await _service.GetInspectionStandardListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取检验标准表(InspectionStandard)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:query", "查询检验标准表(InspectionStandard)详情")]
    public async Task<ActionResult<TaktInspectionStandardDto>> GetInspectionStandardByIdAsync(long id)
    {
        var item = await _service.GetInspectionStandardByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取检验标准表(InspectionStandard)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:query", "查询检验标准表(InspectionStandard)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetInspectionStandardOptionsAsync()
    {
        var result = await _service.GetInspectionStandardOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建检验标准表(InspectionStandard)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:inspectionstandard:create", "创建检验标准表(InspectionStandard)")]
    public async Task<ActionResult<TaktInspectionStandardDto>> CreateInspectionStandardAsync([FromBody] TaktInspectionStandardCreateDto dto)
    {
        var result = await _service.CreateInspectionStandardAsync(dto);
        return CreatedAtAction(nameof(GetInspectionStandardByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新检验标准表(InspectionStandard)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:update", "更新检验标准表(InspectionStandard)")]
    public async Task<ActionResult<TaktInspectionStandardDto>> UpdateInspectionStandardAsync(long id, [FromBody] TaktInspectionStandardUpdateDto dto)
    {
        var result = await _service.UpdateInspectionStandardAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除检验标准表(InspectionStandard)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:delete", "删除检验标准表(InspectionStandard)")]
    public async Task<ActionResult> DeleteInspectionStandardByIdAsync(long id)
    {
        await _service.DeleteInspectionStandardByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除检验标准表(InspectionStandard)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:delete", "批量删除检验标准表(InspectionStandard)")]
    public async Task<ActionResult> DeleteInspectionStandardBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteInspectionStandardBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新检验标准表(InspectionStandard)Standard
    /// </summary>
    [HttpPut("status-standard")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:update", "更新检验标准表(InspectionStandard)Standard")]
    public async Task<ActionResult<TaktInspectionStandardDto>> UpdateInspectionStandardStandardStatusAsync([FromBody] TaktInspectionStandardStandardStatusDto dto)
    {
        var result = await _service.UpdateInspectionStandardStandardStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新检验标准表(InspectionStandard)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:update", "更新检验标准表(InspectionStandard)排序")]
    public async Task<ActionResult<TaktInspectionStandardDto>> UpdateInspectionStandardSortAsync([FromBody] TaktInspectionStandardSortDto dto)
    {
        var result = await _service.UpdateInspectionStandardSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取检验标准表(InspectionStandard)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:import", "获取检验标准表(InspectionStandard)导入模板")]
    public async Task<IActionResult> GetInspectionStandardTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetInspectionStandardTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入检验标准表(InspectionStandard)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:import", "导入检验标准表(InspectionStandard)")]
    public async Task<ActionResult<object>> ImportInspectionStandardAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportInspectionStandardAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出检验标准表(InspectionStandard)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:inspectionstandard:export", "导出检验标准表(InspectionStandard)")]
    public async Task<IActionResult> ExportInspectionStandardAsync([FromBody] TaktInspectionStandardQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportInspectionStandardAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
