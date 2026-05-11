// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.Performance
// 文件名称：TaktPerformanceGoalsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：绩效目标表控制器，提供PerformanceGoal管理的RESTful API接口
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
/// 绩效目标表控制器
/// </summary>
[Route("api/[controller]", Name = "绩效目标表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:performance:performancegoal", "绩效目标表管理")]
public class TaktPerformanceGoalsController : TaktControllerBase
{
    private readonly ITaktPerformanceGoalService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktPerformanceGoalsController(
        ITaktPerformanceGoalService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取绩效目标表(PerformanceGoal)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:performance:performancegoal:list", "查询绩效目标表(PerformanceGoal)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktPerformanceGoalDto>>> GetPerformanceGoalListAsync([FromQuery] TaktPerformanceGoalQueryDto queryDto)
    {
        var result = await _service.GetPerformanceGoalListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取绩效目标表(PerformanceGoal)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:performance:performancegoal:query", "查询绩效目标表(PerformanceGoal)详情")]
    public async Task<ActionResult<TaktPerformanceGoalDto>> GetPerformanceGoalByIdAsync(long id)
    {
        var item = await _service.GetPerformanceGoalByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取绩效目标表(PerformanceGoal)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:performance:performancegoal:query", "查询绩效目标表(PerformanceGoal)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetPerformanceGoalOptionsAsync()
    {
        var result = await _service.GetPerformanceGoalOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建绩效目标表(PerformanceGoal)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:performance:performancegoal:create", "创建绩效目标表(PerformanceGoal)")]
    public async Task<ActionResult<TaktPerformanceGoalDto>> CreatePerformanceGoalAsync([FromBody] TaktPerformanceGoalCreateDto dto)
    {
        var result = await _service.CreatePerformanceGoalAsync(dto);
        return CreatedAtAction(nameof(GetPerformanceGoalByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新绩效目标表(PerformanceGoal)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:performance:performancegoal:update", "更新绩效目标表(PerformanceGoal)")]
    public async Task<ActionResult<TaktPerformanceGoalDto>> UpdatePerformanceGoalAsync(long id, [FromBody] TaktPerformanceGoalUpdateDto dto)
    {
        var result = await _service.UpdatePerformanceGoalAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除绩效目标表(PerformanceGoal)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:performance:performancegoal:delete", "删除绩效目标表(PerformanceGoal)")]
    public async Task<ActionResult> DeletePerformanceGoalByIdAsync(long id)
    {
        await _service.DeletePerformanceGoalByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除绩效目标表(PerformanceGoal)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:performance:performancegoal:delete", "批量删除绩效目标表(PerformanceGoal)")]
    public async Task<ActionResult> DeletePerformanceGoalBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeletePerformanceGoalBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新绩效目标表(PerformanceGoal)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:performance:performancegoal:update", "更新绩效目标表(PerformanceGoal)状态")]
    public async Task<ActionResult<TaktPerformanceGoalDto>> UpdatePerformanceGoalStatusAsync([FromBody] TaktPerformanceGoalStatusDto dto)
    {
        var result = await _service.UpdatePerformanceGoalStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取绩效目标表(PerformanceGoal)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:performance:performancegoal:import", "获取绩效目标表(PerformanceGoal)导入模板")]
    public async Task<IActionResult> GetPerformanceGoalTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetPerformanceGoalTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入绩效目标表(PerformanceGoal)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:performance:performancegoal:import", "导入绩效目标表(PerformanceGoal)")]
    public async Task<ActionResult<object>> ImportPerformanceGoalAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportPerformanceGoalAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出绩效目标表(PerformanceGoal)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:performance:performancegoal:export", "导出绩效目标表(PerformanceGoal)")]
    public async Task<IActionResult> ExportPerformanceGoalAsync([FromBody] TaktPerformanceGoalQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportPerformanceGoalAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
