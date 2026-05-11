// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingActivitysController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：培训活动表控制器，提供TrainingActivity管理的RESTful API接口
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
/// 培训活动表控制器
/// </summary>
[Route("api/[controller]", Name = "培训活动表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:trainingdevelopment:trainingactivity", "培训活动表管理")]
public class TaktTrainingActivitysController : TaktControllerBase
{
    private readonly ITaktTrainingActivityService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingActivitysController(
        ITaktTrainingActivityService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取培训活动表(TrainingActivity)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:list", "查询培训活动表(TrainingActivity)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTrainingActivityDto>>> GetTrainingActivityListAsync([FromQuery] TaktTrainingActivityQueryDto queryDto)
    {
        var result = await _service.GetTrainingActivityListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取培训活动表(TrainingActivity)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:query", "查询培训活动表(TrainingActivity)详情")]
    public async Task<ActionResult<TaktTrainingActivityDto>> GetTrainingActivityByIdAsync(long id)
    {
        var item = await _service.GetTrainingActivityByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取培训活动表(TrainingActivity)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:query", "查询培训活动表(TrainingActivity)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTrainingActivityOptionsAsync()
    {
        var result = await _service.GetTrainingActivityOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建培训活动表(TrainingActivity)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:create", "创建培训活动表(TrainingActivity)")]
    public async Task<ActionResult<TaktTrainingActivityDto>> CreateTrainingActivityAsync([FromBody] TaktTrainingActivityCreateDto dto)
    {
        var result = await _service.CreateTrainingActivityAsync(dto);
        return CreatedAtAction(nameof(GetTrainingActivityByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新培训活动表(TrainingActivity)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:update", "更新培训活动表(TrainingActivity)")]
    public async Task<ActionResult<TaktTrainingActivityDto>> UpdateTrainingActivityAsync(long id, [FromBody] TaktTrainingActivityUpdateDto dto)
    {
        var result = await _service.UpdateTrainingActivityAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除培训活动表(TrainingActivity)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:delete", "删除培训活动表(TrainingActivity)")]
    public async Task<ActionResult> DeleteTrainingActivityByIdAsync(long id)
    {
        await _service.DeleteTrainingActivityByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除培训活动表(TrainingActivity)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:delete", "批量删除培训活动表(TrainingActivity)")]
    public async Task<ActionResult> DeleteTrainingActivityBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTrainingActivityBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新培训活动表(TrainingActivity)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:update", "更新培训活动表(TrainingActivity)状态")]
    public async Task<ActionResult<TaktTrainingActivityDto>> UpdateTrainingActivityStatusAsync([FromBody] TaktTrainingActivityStatusDto dto)
    {
        var result = await _service.UpdateTrainingActivityStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取培训活动表(TrainingActivity)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:import", "获取培训活动表(TrainingActivity)导入模板")]
    public async Task<IActionResult> GetTrainingActivityTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTrainingActivityTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入培训活动表(TrainingActivity)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:import", "导入培训活动表(TrainingActivity)")]
    public async Task<ActionResult<object>> ImportTrainingActivityAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTrainingActivityAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出培训活动表(TrainingActivity)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:trainingdevelopment:trainingactivity:export", "导出培训活动表(TrainingActivity)")]
    public async Task<IActionResult> ExportTrainingActivityAsync([FromBody] TaktTrainingActivityQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTrainingActivityAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
