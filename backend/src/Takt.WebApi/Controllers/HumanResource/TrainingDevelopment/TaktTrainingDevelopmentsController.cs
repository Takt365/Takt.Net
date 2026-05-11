// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingDevelopmentsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：培训发展表控制器，提供TrainingDevelopment管理的RESTful API接口
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
/// 培训发展表控制器
/// </summary>
[Route("api/[controller]", Name = "培训发展表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:trainingdevelopment", "培训发展表管理")]
public class TaktTrainingDevelopmentsController : TaktControllerBase
{
    private readonly ITaktTrainingDevelopmentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingDevelopmentsController(
        ITaktTrainingDevelopmentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取培训发展表(TrainingDevelopment)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:trainingdevelopment:list", "查询培训发展表(TrainingDevelopment)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTrainingDevelopmentDto>>> GetTrainingDevelopmentListAsync([FromQuery] TaktTrainingDevelopmentQueryDto queryDto)
    {
        var result = await _service.GetTrainingDevelopmentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取培训发展表(TrainingDevelopment)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:query", "查询培训发展表(TrainingDevelopment)详情")]
    public async Task<ActionResult<TaktTrainingDevelopmentDto>> GetTrainingDevelopmentByIdAsync(long id)
    {
        var item = await _service.GetTrainingDevelopmentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取培训发展表(TrainingDevelopment)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:trainingdevelopment:query", "查询培训发展表(TrainingDevelopment)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTrainingDevelopmentOptionsAsync()
    {
        var result = await _service.GetTrainingDevelopmentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建培训发展表(TrainingDevelopment)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:trainingdevelopment:create", "创建培训发展表(TrainingDevelopment)")]
    public async Task<ActionResult<TaktTrainingDevelopmentDto>> CreateTrainingDevelopmentAsync([FromBody] TaktTrainingDevelopmentCreateDto dto)
    {
        var result = await _service.CreateTrainingDevelopmentAsync(dto);
        return CreatedAtAction(nameof(GetTrainingDevelopmentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新培训发展表(TrainingDevelopment)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:update", "更新培训发展表(TrainingDevelopment)")]
    public async Task<ActionResult<TaktTrainingDevelopmentDto>> UpdateTrainingDevelopmentAsync(long id, [FromBody] TaktTrainingDevelopmentUpdateDto dto)
    {
        var result = await _service.UpdateTrainingDevelopmentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除培训发展表(TrainingDevelopment)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:delete", "删除培训发展表(TrainingDevelopment)")]
    public async Task<ActionResult> DeleteTrainingDevelopmentByIdAsync(long id)
    {
        await _service.DeleteTrainingDevelopmentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除培训发展表(TrainingDevelopment)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:trainingdevelopment:delete", "批量删除培训发展表(TrainingDevelopment)")]
    public async Task<ActionResult> DeleteTrainingDevelopmentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTrainingDevelopmentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新培训发展表(TrainingDevelopment)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:trainingdevelopment:update", "更新培训发展表(TrainingDevelopment)状态")]
    public async Task<ActionResult<TaktTrainingDevelopmentDto>> UpdateTrainingDevelopmentStatusAsync([FromBody] TaktTrainingDevelopmentStatusDto dto)
    {
        var result = await _service.UpdateTrainingDevelopmentStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取培训发展表(TrainingDevelopment)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:trainingdevelopment:import", "获取培训发展表(TrainingDevelopment)导入模板")]
    public async Task<IActionResult> GetTrainingDevelopmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTrainingDevelopmentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入培训发展表(TrainingDevelopment)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:trainingdevelopment:import", "导入培训发展表(TrainingDevelopment)")]
    public async Task<ActionResult<object>> ImportTrainingDevelopmentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTrainingDevelopmentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出培训发展表(TrainingDevelopment)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:trainingdevelopment:export", "导出培训发展表(TrainingDevelopment)")]
    public async Task<IActionResult> ExportTrainingDevelopmentAsync([FromBody] TaktTrainingDevelopmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTrainingDevelopmentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
