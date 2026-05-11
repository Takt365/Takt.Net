// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.TrainingDevelopment
// 文件名称：TaktSkillAssessmentsController.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：技能评估表控制器，提供SkillAssessment管理的RESTful API接口
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
/// 技能评估表控制器
/// </summary>
[Route("api/[controller]", Name = "技能评估表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:trainingdevelopment:skillassessment", "技能评估表管理")]
public class TaktSkillAssessmentsController : TaktControllerBase
{
    private readonly ITaktSkillAssessmentService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSkillAssessmentsController(
        ITaktSkillAssessmentService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取技能评估表(SkillAssessment)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:list", "查询技能评估表(SkillAssessment)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktSkillAssessmentDto>>> GetSkillAssessmentListAsync([FromQuery] TaktSkillAssessmentQueryDto queryDto)
    {
        var result = await _service.GetSkillAssessmentListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取技能评估表(SkillAssessment)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:query", "查询技能评估表(SkillAssessment)详情")]
    public async Task<ActionResult<TaktSkillAssessmentDto>> GetSkillAssessmentByIdAsync(long id)
    {
        var item = await _service.GetSkillAssessmentByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取技能评估表(SkillAssessment)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:query", "查询技能评估表(SkillAssessment)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetSkillAssessmentOptionsAsync()
    {
        var result = await _service.GetSkillAssessmentOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建技能评估表(SkillAssessment)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:create", "创建技能评估表(SkillAssessment)")]
    public async Task<ActionResult<TaktSkillAssessmentDto>> CreateSkillAssessmentAsync([FromBody] TaktSkillAssessmentCreateDto dto)
    {
        var result = await _service.CreateSkillAssessmentAsync(dto);
        return CreatedAtAction(nameof(GetSkillAssessmentByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新技能评估表(SkillAssessment)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:update", "更新技能评估表(SkillAssessment)")]
    public async Task<ActionResult<TaktSkillAssessmentDto>> UpdateSkillAssessmentAsync(long id, [FromBody] TaktSkillAssessmentUpdateDto dto)
    {
        var result = await _service.UpdateSkillAssessmentAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除技能评估表(SkillAssessment)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:delete", "删除技能评估表(SkillAssessment)")]
    public async Task<ActionResult> DeleteSkillAssessmentByIdAsync(long id)
    {
        await _service.DeleteSkillAssessmentByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除技能评估表(SkillAssessment)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:delete", "批量删除技能评估表(SkillAssessment)")]
    public async Task<ActionResult> DeleteSkillAssessmentBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteSkillAssessmentBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新技能评估表(SkillAssessment)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:update", "更新技能评估表(SkillAssessment)状态")]
    public async Task<ActionResult<TaktSkillAssessmentDto>> UpdateSkillAssessmentStatusAsync([FromBody] TaktSkillAssessmentStatusDto dto)
    {
        var result = await _service.UpdateSkillAssessmentStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取技能评估表(SkillAssessment)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:import", "获取技能评估表(SkillAssessment)导入模板")]
    public async Task<IActionResult> GetSkillAssessmentTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetSkillAssessmentTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入技能评估表(SkillAssessment)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:import", "导入技能评估表(SkillAssessment)")]
    public async Task<ActionResult<object>> ImportSkillAssessmentAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportSkillAssessmentAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出技能评估表(SkillAssessment)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:trainingdevelopment:skillassessment:export", "导出技能评估表(SkillAssessment)")]
    public async Task<IActionResult> ExportSkillAssessmentAsync([FromBody] TaktSkillAssessmentQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportSkillAssessmentAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
