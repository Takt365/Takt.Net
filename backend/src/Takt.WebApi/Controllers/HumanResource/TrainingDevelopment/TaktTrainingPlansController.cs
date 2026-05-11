// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingPlansController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：培训计划表控制器，提供TrainingPlan管理的RESTful API接口
//
// 版权信息：Copyright (c) 2026 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.HumanResource.TrainingDevelopment;
using Takt.Application.Services.HumanResource.TrainingDevelopment;
using Takt.Domain.Interfaces;
using Takt.Infrastructure.Attributes;
using Takt.Shared.Models;
using Takt.WebApi.Controllers;
using Takt.Shared.Helpers;

namespace Takt.WebApi.Controllers.HumanResource.TrainingDevelopment;

/// <summary>
/// 培训计划表控制器
/// </summary>
[Route("api/[controller]", Name = "培训计划表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:trainingdevelopment:trainingplan", "培训计划表管理")]
public class TaktTrainingPlansController : TaktControllerBase
{
    private readonly ITaktTrainingPlanService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingPlansController(
        ITaktTrainingPlanService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取培训计划表(TrainingPlan)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:list", "查询培训计划表(TrainingPlan)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTrainingPlanDto>>> GetTrainingPlanListAsync([FromQuery] TaktTrainingPlanQueryDto queryDto)
    {
        var result = await _service.GetTrainingPlanListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取培训计划表(TrainingPlan)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:query", "查询培训计划表(TrainingPlan)详情")]
    public async Task<ActionResult<TaktTrainingPlanDto>> GetTrainingPlanByIdAsync(long id)
    {
        var item = await _service.GetTrainingPlanByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取培训计划表(TrainingPlan)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:query", "查询培训计划表(TrainingPlan)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTrainingPlanOptionsAsync()
    {
        var result = await _service.GetTrainingPlanOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建培训计划表(TrainingPlan)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:create", "创建培训计划表(TrainingPlan)")]
    public async Task<ActionResult<TaktTrainingPlanDto>> CreateTrainingPlanAsync([FromBody] TaktTrainingPlanCreateDto dto)
    {
        var result = await _service.CreateTrainingPlanAsync(dto);
        return CreatedAtAction(nameof(GetTrainingPlanByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新培训计划表(TrainingPlan)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:update", "更新培训计划表(TrainingPlan)")]
    public async Task<ActionResult<TaktTrainingPlanDto>> UpdateTrainingPlanAsync(long id, [FromBody] TaktTrainingPlanUpdateDto dto)
    {
        var result = await _service.UpdateTrainingPlanAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除培训计划表(TrainingPlan)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:delete", "删除培训计划表(TrainingPlan)")]
    public async Task<ActionResult> DeleteTrainingPlanByIdAsync(long id)
    {
        await _service.DeleteTrainingPlanByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除培训计划表(TrainingPlan)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:delete", "批量删除培训计划表(TrainingPlan)")]
    public async Task<ActionResult> DeleteTrainingPlanBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTrainingPlanBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新培训计划表(TrainingPlan)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:update", "更新培训计划表(TrainingPlan)状态")]
    public async Task<ActionResult<TaktTrainingPlanDto>> UpdateTrainingPlanStatusAsync([FromBody] TaktTrainingPlanStatusDto dto)
    {
        var result = await _service.UpdateTrainingPlanStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取培训计划表(TrainingPlan)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:import", "获取培训计划表(TrainingPlan)导入模板")]
    public async Task<IActionResult> GetTrainingPlanTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTrainingPlanTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入培训计划表(TrainingPlan)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:import", "导入培训计划表(TrainingPlan)")]
    public async Task<ActionResult<object>> ImportTrainingPlanAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTrainingPlanAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出培训计划表(TrainingPlan)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:trainingdevelopment:trainingplan:export", "导出培训计划表(TrainingPlan)")]
    public async Task<IActionResult> ExportTrainingPlanAsync([FromBody] TaktTrainingPlanQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTrainingPlanAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
