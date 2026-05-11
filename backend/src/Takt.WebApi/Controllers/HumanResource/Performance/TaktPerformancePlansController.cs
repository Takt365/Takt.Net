// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Performance
// 文件名称：TaktPerformancePlansController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效方案表控制器，提供PerformancePlan管理的RESTful API接口
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
/// 绩效方案表控制器
/// </summary>
[Route("api/[controller]", Name = "绩效方案表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:performance:performanceplan", "绩效方案表管理")]
public class TaktPerformancePlansController : TaktControllerBase
{
    private readonly ITaktPerformancePlanService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformancePlansController(
        ITaktPerformancePlanService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取绩效方案表(PerformancePlan)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:performance:performanceplan:list", "查询绩效方案表(PerformancePlan)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPerformancePlanDto>>> GetPerformancePlanListAsync([FromQuery] TaktPerformancePlanQueryDto queryDto)
    {
        var result = await _service.GetPerformancePlanListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取绩效方案表(PerformancePlan)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:performance:performanceplan:query", "查询绩效方案表(PerformancePlan)详情")]
    public async Task<ActionResult<TaktPerformancePlanDto>> GetPerformancePlanByIdAsync(long id)
    {
        var item = await _service.GetPerformancePlanByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取绩效方案表(PerformancePlan)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:performance:performanceplan:query", "查询绩效方案表(PerformancePlan)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPerformancePlanOptionsAsync()
    {
        var result = await _service.GetPerformancePlanOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建绩效方案表(PerformancePlan)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:performance:performanceplan:create", "创建绩效方案表(PerformancePlan)")]
    public async Task<ActionResult<TaktPerformancePlanDto>> CreatePerformancePlanAsync([FromBody] TaktPerformancePlanCreateDto dto)
    {
        var result = await _service.CreatePerformancePlanAsync(dto);
        return CreatedAtAction(nameof(GetPerformancePlanByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新绩效方案表(PerformancePlan)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:performance:performanceplan:update", "更新绩效方案表(PerformancePlan)")]
    public async Task<ActionResult<TaktPerformancePlanDto>> UpdatePerformancePlanAsync(long id, [FromBody] TaktPerformancePlanUpdateDto dto)
    {
        var result = await _service.UpdatePerformancePlanAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除绩效方案表(PerformancePlan)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:performance:performanceplan:delete", "删除绩效方案表(PerformancePlan)")]
    public async Task<ActionResult> DeletePerformancePlanByIdAsync(long id)
    {
        await _service.DeletePerformancePlanByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除绩效方案表(PerformancePlan)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:performance:performanceplan:delete", "批量删除绩效方案表(PerformancePlan)")]
    public async Task<ActionResult> DeletePerformancePlanBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePerformancePlanBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新绩效方案表(PerformancePlan)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:performance:performanceplan:update", "更新绩效方案表(PerformancePlan)状态")]
    public async Task<ActionResult<TaktPerformancePlanDto>> UpdatePerformancePlanStatusAsync([FromBody] TaktPerformancePlanStatusDto dto)
    {
        var result = await _service.UpdatePerformancePlanStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取绩效方案表(PerformancePlan)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:performance:performanceplan:import", "获取绩效方案表(PerformancePlan)导入模板")]
    public async Task<IActionResult> GetPerformancePlanTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPerformancePlanTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入绩效方案表(PerformancePlan)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:performance:performanceplan:import", "导入绩效方案表(PerformancePlan)")]
    public async Task<ActionResult<object>> ImportPerformancePlanAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPerformancePlanAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出绩效方案表(PerformancePlan)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:performance:performanceplan:export", "导出绩效方案表(PerformancePlan)")]
    public async Task<IActionResult> ExportPerformancePlanAsync([FromBody] TaktPerformancePlanQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPerformancePlanAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
