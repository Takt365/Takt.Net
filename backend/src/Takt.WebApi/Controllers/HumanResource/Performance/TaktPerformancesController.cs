// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Performance
// 文件名称：TaktPerformancesController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效考核表控制器，提供Performance管理的RESTful API接口
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
/// 绩效考核表控制器
/// </summary>
[Route("api/[controller]", Name = "绩效考核表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:performance", "绩效考核表管理")]
public class TaktPerformancesController : TaktControllerBase
{
    private readonly ITaktPerformanceService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancesController(
        ITaktPerformanceService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取绩效考核表(Performance)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:performance:list", "查询绩效考核表(Performance)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPerformanceDto>>> GetPerformanceListAsync([FromQuery] TaktPerformanceQueryDto queryDto)
    {
        var result = await _service.GetPerformanceListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取绩效考核表(Performance)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:performance:query", "查询绩效考核表(Performance)详情")]
    public async Task<ActionResult<TaktPerformanceDto>> GetPerformanceByIdAsync(long id)
    {
        var item = await _service.GetPerformanceByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取绩效考核表(Performance)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:performance:query", "查询绩效考核表(Performance)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPerformanceOptionsAsync()
    {
        var result = await _service.GetPerformanceOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建绩效考核表(Performance)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:performance:create", "创建绩效考核表(Performance)")]
    public async Task<ActionResult<TaktPerformanceDto>> CreatePerformanceAsync([FromBody] TaktPerformanceCreateDto dto)
    {
        var result = await _service.CreatePerformanceAsync(dto);
        return CreatedAtAction(nameof(GetPerformanceByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新绩效考核表(Performance)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:performance:update", "更新绩效考核表(Performance)")]
    public async Task<ActionResult<TaktPerformanceDto>> UpdatePerformanceAsync(long id, [FromBody] TaktPerformanceUpdateDto dto)
    {
        var result = await _service.UpdatePerformanceAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除绩效考核表(Performance)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:performance:delete", "删除绩效考核表(Performance)")]
    public async Task<ActionResult> DeletePerformanceByIdAsync(long id)
    {
        await _service.DeletePerformanceByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除绩效考核表(Performance)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:performance:delete", "批量删除绩效考核表(Performance)")]
    public async Task<ActionResult> DeletePerformanceBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePerformanceBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新绩效考核表(Performance)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:performance:update", "更新绩效考核表(Performance)状态")]
    public async Task<ActionResult<TaktPerformanceDto>> UpdatePerformanceStatusAsync([FromBody] TaktPerformanceStatusDto dto)
    {
        var result = await _service.UpdatePerformanceStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取绩效考核表(Performance)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:performance:import", "获取绩效考核表(Performance)导入模板")]
    public async Task<IActionResult> GetPerformanceTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPerformanceTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入绩效考核表(Performance)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:performance:import", "导入绩效考核表(Performance)")]
    public async Task<ActionResult<object>> ImportPerformanceAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPerformanceAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出绩效考核表(Performance)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:performance:export", "导出绩效考核表(Performance)")]
    public async Task<IActionResult> ExportPerformanceAsync([FromBody] TaktPerformanceQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPerformanceAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
