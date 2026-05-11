// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Performance
// 文件名称：TaktPerformanceIndicatorsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效指标表控制器，提供PerformanceIndicator管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.Performance;
using Takt.Application.Services.HumanResource.Performance;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.Performance;

/// <summary>
/// 绩效指标表控制器
/// </summary>
[Route("api/[controller]", Name = "绩效指标表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:performance:performanceindicator", "绩效指标表管理")]
public class TaktPerformanceIndicatorsController : TaktControllerBase
{
    private readonly ITaktPerformanceIndicatorService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceIndicatorsController(
        ITaktPerformanceIndicatorService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取绩效指标表(PerformanceIndicator)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:performance:performanceindicator:list", "查询绩效指标表(PerformanceIndicator)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPerformanceIndicatorDto>>> GetPerformanceIndicatorListAsync([FromQuery] TaktPerformanceIndicatorQueryDto queryDto)
    {
        var result = await _service.GetPerformanceIndicatorListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取绩效指标表(PerformanceIndicator)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:performance:performanceindicator:query", "查询绩效指标表(PerformanceIndicator)详情")]
    public async Task<ActionResult<TaktPerformanceIndicatorDto>> GetPerformanceIndicatorByIdAsync(long id)
    {
        var item = await _service.GetPerformanceIndicatorByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取绩效指标表(PerformanceIndicator)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:performance:performanceindicator:query", "查询绩效指标表(PerformanceIndicator)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPerformanceIndicatorOptionsAsync()
    {
        var result = await _service.GetPerformanceIndicatorOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建绩效指标表(PerformanceIndicator)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:performance:performanceindicator:create", "创建绩效指标表(PerformanceIndicator)")]
    public async Task<ActionResult<TaktPerformanceIndicatorDto>> CreatePerformanceIndicatorAsync([FromBody] TaktPerformanceIndicatorCreateDto dto)
    {
        var result = await _service.CreatePerformanceIndicatorAsync(dto);
        return CreatedAtAction(nameof(GetPerformanceIndicatorByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新绩效指标表(PerformanceIndicator)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:performance:performanceindicator:update", "更新绩效指标表(PerformanceIndicator)")]
    public async Task<ActionResult<TaktPerformanceIndicatorDto>> UpdatePerformanceIndicatorAsync(long id, [FromBody] TaktPerformanceIndicatorUpdateDto dto)
    {
        var result = await _service.UpdatePerformanceIndicatorAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除绩效指标表(PerformanceIndicator)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:performance:performanceindicator:delete", "删除绩效指标表(PerformanceIndicator)")]
    public async Task<ActionResult> DeletePerformanceIndicatorByIdAsync(long id)
    {
        await _service.DeletePerformanceIndicatorByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除绩效指标表(PerformanceIndicator)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:performance:performanceindicator:delete", "批量删除绩效指标表(PerformanceIndicator)")]
    public async Task<ActionResult> DeletePerformanceIndicatorBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePerformanceIndicatorBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新绩效指标表(PerformanceIndicator)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:performance:performanceindicator:update", "更新绩效指标表(PerformanceIndicator)状态")]
    public async Task<ActionResult<TaktPerformanceIndicatorDto>> UpdatePerformanceIndicatorStatusAsync([FromBody] TaktPerformanceIndicatorStatusDto dto)
    {
        var result = await _service.UpdatePerformanceIndicatorStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新绩效指标表(PerformanceIndicator)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:performance:performanceindicator:update", "更新绩效指标表(PerformanceIndicator)排序")]
    public async Task<ActionResult<TaktPerformanceIndicatorDto>> UpdatePerformanceIndicatorSortAsync([FromBody] TaktPerformanceIndicatorSortDto dto)
    {
        var result = await _service.UpdatePerformanceIndicatorSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取绩效指标表(PerformanceIndicator)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:performance:performanceindicator:import", "获取绩效指标表(PerformanceIndicator)导入模板")]
    public async Task<IActionResult> GetPerformanceIndicatorTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPerformanceIndicatorTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入绩效指标表(PerformanceIndicator)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:performance:performanceindicator:import", "导入绩效指标表(PerformanceIndicator)")]
    public async Task<ActionResult<object>> ImportPerformanceIndicatorAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPerformanceIndicatorAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出绩效指标表(PerformanceIndicator)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:performance:performanceindicator:export", "导出绩效指标表(PerformanceIndicator)")]
    public async Task<IActionResult> ExportPerformanceIndicatorAsync([FromBody] TaktPerformanceIndicatorQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPerformanceIndicatorAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
