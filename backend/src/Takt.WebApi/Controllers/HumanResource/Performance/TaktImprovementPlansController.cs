// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Performance
// 文件名称：TaktImprovementPlansController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：改进计划表控制器，提供ImprovementPlan管理的RESTful API接口
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
/// 改进计划表控制器
/// </summary>
[Route("api/[controller]", Name = "改进计划表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:performance:improvementplan", "改进计划表管理")]
public class TaktImprovementPlansController : TaktControllerBase
{
    private readonly ITaktImprovementPlanService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktImprovementPlansController(
        ITaktImprovementPlanService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取改进计划表(ImprovementPlan)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:performance:improvementplan:list", "查询改进计划表(ImprovementPlan)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktImprovementPlanDto>>> GetImprovementPlanListAsync([FromQuery] TaktImprovementPlanQueryDto queryDto)
    {
        var result = await _service.GetImprovementPlanListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取改进计划表(ImprovementPlan)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:performance:improvementplan:query", "查询改进计划表(ImprovementPlan)详情")]
    public async Task<ActionResult<TaktImprovementPlanDto>> GetImprovementPlanByIdAsync(long id)
    {
        var item = await _service.GetImprovementPlanByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取改进计划表(ImprovementPlan)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:performance:improvementplan:query", "查询改进计划表(ImprovementPlan)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetImprovementPlanOptionsAsync()
    {
        var result = await _service.GetImprovementPlanOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建改进计划表(ImprovementPlan)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:performance:improvementplan:create", "创建改进计划表(ImprovementPlan)")]
    public async Task<ActionResult<TaktImprovementPlanDto>> CreateImprovementPlanAsync([FromBody] TaktImprovementPlanCreateDto dto)
    {
        var result = await _service.CreateImprovementPlanAsync(dto);
        return CreatedAtAction(nameof(GetImprovementPlanByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新改进计划表(ImprovementPlan)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:performance:improvementplan:update", "更新改进计划表(ImprovementPlan)")]
    public async Task<ActionResult<TaktImprovementPlanDto>> UpdateImprovementPlanAsync(long id, [FromBody] TaktImprovementPlanUpdateDto dto)
    {
        var result = await _service.UpdateImprovementPlanAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除改进计划表(ImprovementPlan)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:performance:improvementplan:delete", "删除改进计划表(ImprovementPlan)")]
    public async Task<ActionResult> DeleteImprovementPlanByIdAsync(long id)
    {
        await _service.DeleteImprovementPlanByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除改进计划表(ImprovementPlan)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:performance:improvementplan:delete", "批量删除改进计划表(ImprovementPlan)")]
    public async Task<ActionResult> DeleteImprovementPlanBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteImprovementPlanBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新改进计划表(ImprovementPlan)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:performance:improvementplan:update", "更新改进计划表(ImprovementPlan)状态")]
    public async Task<ActionResult<TaktImprovementPlanDto>> UpdateImprovementPlanStatusAsync([FromBody] TaktImprovementPlanStatusDto dto)
    {
        var result = await _service.UpdateImprovementPlanStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取改进计划表(ImprovementPlan)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:performance:improvementplan:import", "获取改进计划表(ImprovementPlan)导入模板")]
    public async Task<IActionResult> GetImprovementPlanTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetImprovementPlanTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入改进计划表(ImprovementPlan)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:performance:improvementplan:import", "导入改进计划表(ImprovementPlan)")]
    public async Task<ActionResult<object>> ImportImprovementPlanAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportImprovementPlanAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出改进计划表(ImprovementPlan)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:performance:improvementplan:export", "导出改进计划表(ImprovementPlan)")]
    public async Task<IActionResult> ExportImprovementPlanAsync([FromBody] TaktImprovementPlanQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportImprovementPlanAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
