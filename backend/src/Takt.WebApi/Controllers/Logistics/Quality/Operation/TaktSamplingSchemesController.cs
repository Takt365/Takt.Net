// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.Logistics.Quality.Operation
// 文件名称：TaktSamplingSchemesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：抽样方案表控制器，提供SamplingScheme管理的RESTful API接口
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
/// 抽样方案表控制器
/// </summary>
[Route("api/[controller]", Name = "抽样方案表")]
[ApiModule("Logistics", "物流管理")]
[TaktPermission("logistics:quality:operation:samplingscheme", "抽样方案表管理")]
public class TaktSamplingSchemesController : TaktControllerBase
{
    private readonly ITaktSamplingSchemeService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSamplingSchemesController(
        ITaktSamplingSchemeService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取抽样方案表(SamplingScheme)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("logistics:quality:operation:samplingscheme:list", "查询抽样方案表(SamplingScheme)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSamplingSchemeDto>>> GetSamplingSchemeListAsync([FromQuery] TaktSamplingSchemeQueryDto queryDto)
    {
        var result = await _service.GetSamplingSchemeListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取抽样方案表(SamplingScheme)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("logistics:quality:operation:samplingscheme:query", "查询抽样方案表(SamplingScheme)详情")]
    public async Task<ActionResult<TaktSamplingSchemeDto>> GetSamplingSchemeByIdAsync(long id)
    {
        var item = await _service.GetSamplingSchemeByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取抽样方案表(SamplingScheme)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("logistics:quality:operation:samplingscheme:query", "查询抽样方案表(SamplingScheme)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSamplingSchemeOptionsAsync()
    {
        var result = await _service.GetSamplingSchemeOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建抽样方案表(SamplingScheme)
    /// </summary>
    [HttpPost]
    [TaktPermission("logistics:quality:operation:samplingscheme:create", "创建抽样方案表(SamplingScheme)")]
    public async Task<ActionResult<TaktSamplingSchemeDto>> CreateSamplingSchemeAsync([FromBody] TaktSamplingSchemeCreateDto dto)
    {
        var result = await _service.CreateSamplingSchemeAsync(dto);
        return CreatedAtAction(nameof(GetSamplingSchemeByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新抽样方案表(SamplingScheme)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("logistics:quality:operation:samplingscheme:update", "更新抽样方案表(SamplingScheme)")]
    public async Task<ActionResult<TaktSamplingSchemeDto>> UpdateSamplingSchemeAsync(long id, [FromBody] TaktSamplingSchemeUpdateDto dto)
    {
        var result = await _service.UpdateSamplingSchemeAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除抽样方案表(SamplingScheme)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("logistics:quality:operation:samplingscheme:delete", "删除抽样方案表(SamplingScheme)")]
    public async Task<ActionResult> DeleteSamplingSchemeByIdAsync(long id)
    {
        await _service.DeleteSamplingSchemeByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除抽样方案表(SamplingScheme)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("logistics:quality:operation:samplingscheme:delete", "批量删除抽样方案表(SamplingScheme)")]
    public async Task<ActionResult> DeleteSamplingSchemeBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSamplingSchemeBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新抽样方案表(SamplingScheme)Scheme
    /// </summary>
    [HttpPut("status-scheme")]
    [TaktPermission("logistics:quality:operation:samplingscheme:update", "更新抽样方案表(SamplingScheme)Scheme")]
    public async Task<ActionResult<TaktSamplingSchemeDto>> UpdateSamplingSchemeSchemeStatusAsync([FromBody] TaktSamplingSchemeSchemeStatusDto dto)
    {
        var result = await _service.UpdateSamplingSchemeSchemeStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新抽样方案表(SamplingScheme)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("logistics:quality:operation:samplingscheme:update", "更新抽样方案表(SamplingScheme)排序")]
    public async Task<ActionResult<TaktSamplingSchemeDto>> UpdateSamplingSchemeSortAsync([FromBody] TaktSamplingSchemeSortDto dto)
    {
        var result = await _service.UpdateSamplingSchemeSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取抽样方案表(SamplingScheme)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("logistics:quality:operation:samplingscheme:import", "获取抽样方案表(SamplingScheme)导入模板")]
    public async Task<IActionResult> GetSamplingSchemeTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSamplingSchemeTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入抽样方案表(SamplingScheme)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("logistics:quality:operation:samplingscheme:import", "导入抽样方案表(SamplingScheme)")]
    public async Task<ActionResult<object>> ImportSamplingSchemeAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSamplingSchemeAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出抽样方案表(SamplingScheme)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("logistics:quality:operation:samplingscheme:export", "导出抽样方案表(SamplingScheme)")]
    public async Task<IActionResult> ExportSamplingSchemeAsync([FromBody] TaktSamplingSchemeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSamplingSchemeAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
