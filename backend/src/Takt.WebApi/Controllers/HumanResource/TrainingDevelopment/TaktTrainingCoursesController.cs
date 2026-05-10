// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.WebApi.Controllers.HumanResource.TrainingDevelopment
// 文件名称：TaktTrainingCoursesController.cs
// 创建时间：2026-05-10
// 创建人：Takt365(Cursor AI)
// 功能描述：培训课程表控制器，提供TrainingCourse管理的RESTful API接口
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
/// 培训课程表控制器
/// </summary>
[Route("api/[controller]", Name = "培训课程表")]
[ApiModule("HumanResource", "人力资源")]
[TaktPermission("humanresource:trainingdevelopment:trainingcourse", "培训课程表管理")]
public class TaktTrainingCoursesController : TaktControllerBase
{
    private readonly ITaktTrainingCourseService _service;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTrainingCoursesController(
        ITaktTrainingCourseService service,
        ITaktUserContext? userContext = null,
        ITaktTenantContext? tenantContext = null,
        ITaktLocalizer? localizer = null)
        : base(userContext, tenantContext, localizer)
    {
        _service = service;
    }


    /// <summary>
    /// 获取培训课程表(TrainingCourse)列表（分页）
    /// </summary>
    [HttpGet("list")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:list", "查询培训课程表(TrainingCourse)列表")]
    public async Task<ActionResult<TaktPagedResult<TaktTrainingCourseDto>>> GetTrainingCourseListAsync([FromQuery] TaktTrainingCourseQueryDto queryDto)
    {
        var result = await _service.GetTrainingCourseListAsync(queryDto);
        return Ok(result);
    }


    /// <summary>
    /// 根据ID获取培训课程表(TrainingCourse)
    /// </summary>
    [HttpGet("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:query", "查询培训课程表(TrainingCourse)详情")]
    public async Task<ActionResult<TaktTrainingCourseDto>> GetTrainingCourseByIdAsync(long id)
    {
        var item = await _service.GetTrainingCourseByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }


    /// <summary>
    /// 获取培训课程表(TrainingCourse)选项列表
    /// </summary>
    [HttpGet("options")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:query", "查询培训课程表(TrainingCourse)选项")]
    public async Task<ActionResult<List<TaktSelectOption>>> GetTrainingCourseOptionsAsync()
    {
        var result = await _service.GetTrainingCourseOptionsAsync();
        return Ok(result);
    }


    /// <summary>
    /// 创建培训课程表(TrainingCourse)
    /// </summary>
    [HttpPost]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:create", "创建培训课程表(TrainingCourse)")]
    public async Task<ActionResult<TaktTrainingCourseDto>> CreateTrainingCourseAsync([FromBody] TaktTrainingCourseCreateDto dto)
    {
        var result = await _service.CreateTrainingCourseAsync(dto);
        return CreatedAtAction(nameof(GetTrainingCourseByIdAsync), new { id = result.Id }, result);
    }


    /// <summary>
    /// 更新培训课程表(TrainingCourse)
    /// </summary>
    [HttpPut("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:update", "更新培训课程表(TrainingCourse)")]
    public async Task<ActionResult<TaktTrainingCourseDto>> UpdateTrainingCourseAsync(long id, [FromBody] TaktTrainingCourseUpdateDto dto)
    {
        var result = await _service.UpdateTrainingCourseAsync(id, dto);
        return Ok(result);
    }


    /// <summary>
    /// 删除培训课程表(TrainingCourse)
    /// </summary>
    [HttpDelete("{id}")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:delete", "删除培训课程表(TrainingCourse)")]
    public async Task<ActionResult> DeleteTrainingCourseByIdAsync(long id)
    {
        await _service.DeleteTrainingCourseByIdAsync(id);
        return Ok();
    }


    /// <summary>
    /// 批量删除培训课程表(TrainingCourse)
    /// </summary>
    [HttpDelete("batch")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:delete", "批量删除培训课程表(TrainingCourse)")]
    public async Task<ActionResult> DeleteTrainingCourseBatchAsync([FromBody] List<long> ids)
    {
        await _service.DeleteTrainingCourseBatchAsync(ids);
        return Ok();
    }


    /// <summary>
    /// 更新培训课程表(TrainingCourse)状态
    /// </summary>
    [HttpPut("status")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:update", "更新培训课程表(TrainingCourse)状态")]
    public async Task<ActionResult<TaktTrainingCourseDto>> UpdateTrainingCourseStatusAsync([FromBody] TaktTrainingCourseStatusDto dto)
    {
        var result = await _service.UpdateTrainingCourseStatusAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 更新培训课程表(TrainingCourse)排序
    /// </summary>
    [HttpPut("sort")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:update", "更新培训课程表(TrainingCourse)排序")]
    public async Task<ActionResult<TaktTrainingCourseDto>> UpdateTrainingCourseSortAsync([FromBody] TaktTrainingCourseSortDto dto)
    {
        var result = await _service.UpdateTrainingCourseSortAsync(dto);
        return Ok(result);
    }


    /// <summary>
    /// 获取培训课程表(TrainingCourse)导入模板
    /// </summary>
    [HttpGet("template")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:import", "获取培训课程表(TrainingCourse)导入模板")]
    public async Task<IActionResult> GetTrainingCourseTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.GetTrainingCourseTemplateAsync(sheetName, fileName);
        return File(content, TaktExcelHelper.ExcelContentType, resultFileName);
    }


    /// <summary>
    /// 导入培训课程表(TrainingCourse)
    /// </summary>
    [HttpPost("import")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:import", "导入培训课程表(TrainingCourse)")]
    public async Task<ActionResult<object>> ImportTrainingCourseAsync(IFormFile file, [FromForm] string? sheetName = null)
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
        var (success, fail, errors) = await _service.ImportTrainingCourseAsync(stream, sheetName);
        return Ok(new { success, fail, errors });
    }


    /// <summary>
    /// 导出培训课程表(TrainingCourse)
    /// </summary>
    [HttpPost("export")]
    [TaktPermission("humanresource:trainingdevelopment:trainingcourse:export", "导出培训课程表(TrainingCourse)")]
    public async Task<IActionResult> ExportTrainingCourseAsync([FromBody] TaktTrainingCourseQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var (resultFileName, content) = await _service.ExportTrainingCourseAsync(query, sheetName, fileName);
        return File(content, TaktExcelHelper.GetExportContentType(resultFileName), resultFileName);
    }

}
